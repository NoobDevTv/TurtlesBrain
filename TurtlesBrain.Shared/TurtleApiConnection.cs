using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TurtlesBrain.Shared
{
    

    public abstract class TurtleApiConnection : IDisposable
    {
        private readonly NetworkStream _stream;
        private byte[] _readerBuffer = new byte[4096];
        private byte[] _writerBuffer = new byte[8];
        private readonly byte[] _headerBuffer = new byte[8];

        private readonly ManualResetEventSlim _writerReset = new ManualResetEventSlim(true);

        protected TurtleApiConnection(NetworkStream stream)
        {
            _stream = stream;
            _stream.BeginRead(_headerBuffer, 0, _headerBuffer.Length, OnHeaderRead, null);
        }

        private void OnHeaderRead(IAsyncResult ar)
        {
            int read;
            try
            {
                read = _stream.EndRead(ar);
            }
            catch (IOException)
            {
                Dispose();
                return;
            }

            if (read != _headerBuffer.Length)
                throw new InvalidOperationException();

            var payloadSize = BitConverter.ToInt32(_headerBuffer, 0);
            var messageType = BitConverter.ToInt32(_headerBuffer, 4);

            if (payloadSize > 0)
            {
                if (_readerBuffer.Length < payloadSize)
                    _readerBuffer = new byte[payloadSize];

                _stream.BeginRead(_readerBuffer, 0, payloadSize, OnRead, messageType);
            }
            else
            {
                var msg = MessageConverter.Read(messageType);
                _stream.BeginRead(_headerBuffer, 0, _headerBuffer.Length, OnHeaderRead, null);
                Dispatch(msg);
            }
        }

        protected abstract void Dispatch(ITurtleApiMessage msg);

        private void OnRead(IAsyncResult ar)
        {
            var read = _stream.EndRead(ar);
            var messageType = (int)ar.AsyncState;
            var msg = MessageConverter.Read(messageType, _readerBuffer, read);
            _stream.BeginRead(_headerBuffer, 0, _headerBuffer.Length, OnHeaderRead, null);
            Dispatch(msg);
        }

        public async Task WriteAsync(ITurtleApiMessage msg)
        {
            _writerReset.Wait();

            try
            {
                int type, len;
                MessageConverter.Write(msg, ref _writerBuffer, out type, out len);

                var payload = len - 8;
                
                _writerBuffer[0] = (byte)(payload >> 0);
                _writerBuffer[1] = (byte)(payload >> 8);
                _writerBuffer[2] = (byte)(payload >> 16);
                _writerBuffer[3] = (byte)(payload >> 24);

                _writerBuffer[4] = (byte)(type >> 0);
                _writerBuffer[5] = (byte)(type >> 8);
                _writerBuffer[6] = (byte)(type >> 16);
                _writerBuffer[7] = (byte)(type >> 24);

                await _stream.WriteAsync(_writerBuffer, 0, len);
            }
            finally
            {
                _writerReset.Set();
            }
        }

        public void Dispose()
        {
            _writerReset.Dispose();
            _stream.Dispose();
        }
    }
}

