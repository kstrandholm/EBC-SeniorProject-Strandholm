using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace DCCRegistrantFileDownload
{
    public class FTPInformation
    {
        private string FTPHost => ConfigurationManager.AppSettings["RegistrantInformationFilePath"];
        private string FTPUserName => ConfigurationManager.AppSettings["FTPUserName"];
        private string FTPPassword => ConfigurationManager.AppSettings["FTPPassword"];
        private string LocalFileLocation => ConfigurationManager.AppSettings["LocalFileLocation"];

        public void HelloWorld()
        {
            using (var sftp = new SftpClient(FTPHost, FTPUserName, FTPPassword))
            {
                using (Stream fileStream = File.Create(@"C:\target\local\path\file.zip"))
                {
                    sftp.DownloadFile("/source/remote/path/file.zip", fileStream);
                }
            }
        }

        /// <summary>
        /// Provides methods to transfer files to and from a carrier
        /// </summary>
        public abstract class FileTransferBase
        {
            protected string HostName;
            protected string Username;
            protected string Password;

            /// <summary>
            /// Uploads a file to the specified path on the server
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            public void SendFile(string source, string destination)
            {
                using (var client = new SSHNetSFTPClient(null, HostName, Username, Password))
                {
                    client.SendFile(source, destination);
                }
            }

            /// <summary>
            /// Downloads a file from the server and saves it to the desired destination
            /// </summary>
            /// <param name="source"></param>
            /// <param name="destination"></param>
            public void GetFile(string source, string destination)
            {
                using (var client = new SSHNetSFTPClient(null, HostName, Username, Password))
                {
                    client.GetFile(source, destination);
                }
            }

            /// <summary>
            /// Gets the filenames of files in the specified directory on the server
            /// </summary>
            /// <param name="directory"></param>
            /// <returns></returns>
            public IReadOnlyCollection<string> GetFileNames(string directory)
            {
                using (var client = new SSHNetSFTPClient(null, HostName, Username, Password))
                {
                    return client.GetFileNames(directory).ToList();
                }
            }

            /// <summary>
            /// Gets the full information for files in the specified directory on the server
            /// </summary>
            /// <param name="directory"></param>
            /// <returns></returns>
            public IReadOnlyCollection<FtpFileInformation> GetFileList(string directory = null)
            {
                using (var client = new SSHNetSFTPClient(null, HostName, Username, Password))
                {
                    return client.GetFileList(directory).ToList();
                }
            }
        }
    }
}
