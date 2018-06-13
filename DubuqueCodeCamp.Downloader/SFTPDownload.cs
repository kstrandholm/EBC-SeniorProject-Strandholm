using System;
using System.Configuration;
using System.IO;
using Renci.SshNet;

namespace DubuqueCodeCamp.Downloader
{
    public class SFTPDownload
    {
        private static string FTPHost => ConfigurationManager.AppSettings["RegistrantInformationFilePath"];
        private static string FTPUserName => ConfigurationManager.AppSettings["FTPUserName"];
        private static string FTPPassword => ConfigurationManager.AppSettings["FTPPassword"];
        private static string FTPFileLocation => ConfigurationManager.AppSettings["FTPFileLocation"];


        public static void DownloadFileUsingSftpClient(string fileToDownload, string localFileLocation)
        {
            Console.WriteLine("Local File Location: " + localFileLocation);
            Console.WriteLine("File Name: " + fileToDownload);
            Console.WriteLine("FTP File Location: " + FTPFileLocation);

            using (var sftp = new SftpClient(FTPHost, FTPUserName, FTPPassword))
            {
                try
                {
                    sftp.Connect();

                    GetFileUsingFileStream(fileToDownload, sftp, localFileLocation);
                }
                finally
                {
                    sftp.Dispose();
                }
            }
        }

        private static void GetFileUsingFileStream(string fileToDownload, SftpClient sftp, string localFileLocation)
        {
            using (Stream fileStream = File.Create(localFileLocation + fileToDownload))
            {
                sftp.DownloadFile(FTPFileLocation + fileToDownload, fileStream);
            }
        }
    }
}
