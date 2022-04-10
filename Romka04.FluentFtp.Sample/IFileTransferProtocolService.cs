using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentFTP;

namespace Romka04.FluentFtp.Sample
{
    public interface IFileTransferProtocolService
    {
        Task WriteFileAsync(string fileName, Stream file, bool leaveOpen, CancellationToken cancellationToken);
    }

    public class FileTransferProtocolService
        : IFileTransferProtocolService
    {
        public async Task WriteFileAsync(string fileName, Stream file, bool leaveOpen,
            CancellationToken cancellationToken)
        {
            AssertAndSeekStream(file);

            // create an FTP client and specify the host, username and password
            FtpClient client = new FtpClient("localhost", "username", "mypass");

            // connect to the server and automatically detect working FTP settings
            await client.AutoConnectAsync(cancellationToken);

            // upload a file
            await client.UploadAsync(file, fileName, token: cancellationToken);
        }

        private void AssertAndSeekStream(Stream file)
        {
            if (null == file || Stream.Null == file)
                throw new ArgumentNullException(nameof(file));

            if (!file.CanSeek)
                throw new ArgumentException("Unable to Seek stream", nameof(file));

            file.Seek(0, SeekOrigin.Begin);
        }
    }
}