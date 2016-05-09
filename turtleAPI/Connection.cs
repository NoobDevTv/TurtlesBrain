using System;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace turtleAPI
{
    internal class Connection //evtl noch umbenennen
    {
        internal static async Task<Server> Setup(string ip, int port, string username, string password)
        {
            TcpClient tcpClient = new TcpClient();
            await tcpClient.ConnectAsync(ip, port);
            Console.WriteLine("Connected");
            NetworkStream stream = tcpClient.GetStream();
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();

            using (var dh = new ECDiffieHellmanCng())
            {
                byte[] buffer = new byte[1024];
                var read = await stream.ReadAsync(buffer, 0, buffer.Length);

                byte[] otherKey = new byte[read];
                Buffer.BlockCopy(buffer, 0, otherKey, 0, read);
                var key = dh.DeriveKeyMaterial(CngKey.Import(otherKey, CngKeyBlobFormat.EccPublicBlob));

                byte[] pk = dh.PublicKey.ToByteArray();
                await stream.WriteAsync(pk, 0, pk.Length);

                aes.Key = key;
                read = await stream.ReadAsync(buffer, 0, buffer.Length);

                byte[] iv = new byte[read];
                Buffer.BlockCopy(buffer, 0, iv, 0, read);
                aes.IV = iv;

                var encryptor = aes.CreateEncryptor();

                var payload = Encoding.UTF8.GetBytes($"{username}\n{password}");
                buffer = new byte[2048];
                int total = 0;
                if (payload.Length <= encryptor.InputBlockSize)
                {
                    var final = encryptor.TransformFinalBlock(payload, 0, payload.Length);
                    Buffer.BlockCopy(final, 0, buffer, 0, final.Length);
                    total = final.Length;
                }
                else
                {
                    var diff = payload.Length % encryptor.InputBlockSize;

                    var offset = diff == 0 ? encryptor.InputBlockSize : diff;

                    var written = encryptor.TransformBlock(payload, 0, payload.Length - offset, buffer, 0);
                    var final = encryptor.TransformFinalBlock(payload, payload.Length - offset, offset);
                    Buffer.BlockCopy(final, 0, buffer, written, final.Length);
                    total = final.Length + written;
                }

                await stream.WriteAsync(buffer, 0, total);

                return new Server(stream);
            }

            throw new InvalidOperationException();
        }
    }
}
