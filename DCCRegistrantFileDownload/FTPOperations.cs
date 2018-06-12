using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using Renci.SshNet;

namespace DCCRegistrantFileDownload
{
    public static class FTPOperations
    {
        private static string FTPHost => ConfigurationManager.AppSettings["RegistrantInformationFilePath"];
        private static string FTPUserName => ConfigurationManager.AppSettings["FTPUserName"];
        private static string FTPPassword => ConfigurationManager.AppSettings["FTPPassword"];
        private static string FTPFileLocation => ConfigurationManager.AppSettings["FTPFileLocation"];
        private static string LocalFileLocation => ConfigurationManager.AppSettings["LocalFileLocation"];


        public static void GetFileUsingSFTPClient(string fileToDownload)
        {
            Console.WriteLine("Local File Location: " + LocalFileLocation);
            Console.WriteLine("File Name: " + fileToDownload);
            Console.WriteLine("FTP File Location: " + FTPFileLocation);

            using (var sftp = new SftpClient(FTPHost, FTPUserName, FTPPassword))
            {
                sftp.Connect();

                GetFileUsingFileStream(fileToDownload, sftp);
            }
        }

        private static void GetFileUsingFileStream(string fileToDownload, SftpClient sftp)
        {
            using (Stream fileStream = File.OpenWrite(LocalFileLocation + fileToDownload))
            {
                sftp.DownloadFile(FTPFileLocation + fileToDownload, fileStream);
            }
        }
    }
}
