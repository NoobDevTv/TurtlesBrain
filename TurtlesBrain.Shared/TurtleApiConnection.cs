using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace TurtlesBrain.Shared
{
    public abstract class TurtleApiConnection : IDisposable
    {
        private readonly NetworkStream stream;
        private byte[] readerBuffer;
        private byte[] writerBuffer;
        private readonly byte[] headerBuffer;

        private readonly ManualResetEventSlim writerReset;

        protected TurtleApiConnection(NetworkStream stream)
        {
            readerBuffer = new byte[4096];
            writerBuffer = new byte[8];
            headerBuffer = new byte[8];

            writerReset = new ManualResetEventSlim(true);

            this.stream = stream;
            this.stream.BeginRead(headerBuffer, 0, headerBuffer.Length, this.OnHeaderRead, null);
        }

        private void OnHeaderRead(IAsyncResult ar)
        {
            int read;
            try
            {
                read = stream.EndRead(ar);
            }
            catch (IOException)
            {
                Dispose();
                return;
            }

            if (read != headerBuffer.Length)
                throw new InvalidOperationException();

            var payloadSize = BitConverter.ToInt32(headerBuffer, 0);
            var messageType = BitConverter.ToInt32(headerBuffer, 4);

            if (payloadSize > 0)
            {
                if (readerBuffer.Length < payloadSize)
                    readerBuffer = new byte[payloadSize];

                stream.BeginRead(readerBuffer, 0, payloadSize, OnRead, messageType);
            }
            else
            {
                var msg = MessageConverter.Read(messageType);
                stream.BeginRead(headerBuffer, 0, headerBuffer.Length, OnHeaderRead, null);
                Dispatch(msg);
            }
        }

        protected abstract void Dispatch(ITurtleApiMessage msg);

        private void OnRead(IAsyncResult ar)
        {
            var read = stream.EndRead(ar);
            var messageType = (int)ar.AsyncState;
            var msg = MessageConverter.Read(messageType, readerBuffer, read);
            stream.BeginRead(headerBuffer, 0, headerBuffer.Length, OnHeaderRead, null);
            Dispatch(msg);
        }

        public async Task WriteAsync(ITurtleApiMessage msg)
        {
            writerReset.Wait();

            try
            {
                int type, len;
                MessageConverter.Write(msg, ref writerBuffer, out type, out len);

                var payload = len - 8;

                writerBuffer[0] = (byte)(payload >> 0);
                writerBuffer[1] = (byte)(payload >> 8);
                writerBuffer[2] = (byte)(payload >> 16);
                writerBuffer[3] = (byte)(payload >> 24);

                writerBuffer[4] = (byte)(type >> 0);
                writerBuffer[5] = (byte)(type >> 8);
                writerBuffer[6] = (byte)(type >> 16);
                writerBuffer[7] = (byte)(type >> 24);

                await stream.WriteAsync(writerBuffer, 0, len);
            }
            finally
            {
                writerReset.Set();
            }
        }

        public void Dispose()
        {
            writerReset.Dispose();
            stream.Dispose();
        }
    }
}

