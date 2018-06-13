using System;
using System.Configuration;
using System.IO;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace DubuqueCodeCamp.Downloader
{
    public class SFTPDownload
    {
        private static string FTPHost => ConfigurationManager.AppSettings["RegistrantInformationFilePath"];
        private static string FTPUserName => ConfigurationManager.AppSettings["FTPUserName"];
        private static string FTPPassword => ConfigurationManager.AppSettings["FTPPassword"];
        private static string FTPFileLocation => ConfigurationManager.AppSettings["FTPFileLocation"];



        public static bool DownloadFileUsingSftpClient(string fileToDownload, string localFileLocation)
        {
            Console.WriteLine("Local File Location: " + localFileLocation);
            Console.WriteLine("File Name: " + fileToDownload);
            Console.WriteLine("FTP File Location: " + FTPFileLocation);

            var fileDownloaded = false;

            using (var sftp = new SftpClient(FTPHost, FTPUserName, FTPPassword))
            {
                try
                {
                    sftp.Connect();

                    fileDownloaded = DownloadFileUsingFileStream(fileToDownload, sftp, localFileLocation);
                }
                finally
                {
                    sftp.Dispose();
                }
            }

            return fileDownloaded;
        }

        private static bool DownloadFileUsingFileStream(string fileToDownload, SftpClient sftp, string localFileLocation)
        {
            var fullLocalPath = localFileLocation + fileToDownload;

            // Open an existing file or create a new one
            using (Stream fileStream = File.OpenWrite(fullLocalPath))
            {
                var newFileInfo = sftp.GetAttributes(FTPFileLocation + fileToDownload);

                // If the file already exists in the local directory, compare the dates on the two files
                if (File.Exists(fullLocalPath))
                {
                    var existingFile = new FileInfo(fullLocalPath);

                    // If the file on the FTP site is not newer than our current file, the file won't be downloaded
                    if (newFileInfo.LastWriteTime < existingFile.LastWriteTime)
                        return false;
                }

                sftp.DownloadFile(FTPFileLocation + fileToDownload, fileStream);
                return true;    // Indicates the file was downloaded
            }
        }
    }
}
