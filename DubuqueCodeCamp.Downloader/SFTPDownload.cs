using Renci.SshNet;
using System;
using System.Configuration;
using System.IO;
using Renci.SshNet.Sftp;
using Serilog;

namespace DubuqueCodeCamp.Downloader
{
    /// <summary>
    /// Class that handles downloading the specified file from the 3rd party's FTP site
    /// </summary>
    public class SftpDownload
    {
        private static string FTPHost => ConfigurationManager.AppSettings["RegistrantInformationFilePath"];
        private static string FTPUserName => ConfigurationManager.AppSettings["FTPUserName"];
        private static string FTPPassword => ConfigurationManager.AppSettings["FTPPassword"];
        private static string FTPFileLocation => ConfigurationManager.AppSettings["FTPFileLocation"];

        private readonly ILogger _logger;

        public SftpDownload()
        {
            _logger = LoggingInitializer.GetLogger();
        }

        /// <summary>
        /// Download the specified file from the 3rd party's FTP site
        /// </summary>
        /// <param name="fileToDownload">Name of the file to download</param>
        /// <param name="localFileLocation">Path to the local location to save the file to</param>
        /// <returns></returns>
        public bool DownloadFileUsingSftpClient(string fileToDownload, string localFileLocation)
        {
            _logger.Verbose($"Local File Location: {localFileLocation}", localFileLocation);
            _logger.Verbose($"File Name: {fileToDownload}", fileToDownload);
            _logger.Verbose($"FTP File Location: {FTPFileLocation}", FTPFileLocation);

            using (var sftp = new SftpClient(FTPHost, FTPUserName, FTPPassword))
            {
                sftp.Connect();

                var fullLocalPath = localFileLocation + fileToDownload;
                var fullSftpPath = FTPFileLocation + fileToDownload;

                // If the file already exists in the local directory, compare the dates on the two files
                if (File.Exists(fullLocalPath))
                {
                    var newFileInfo = sftp.GetAttributes(fullSftpPath);
                    var existingFileInfo = new FileInfo(fullLocalPath);

                    if (!ShouldDownloadNewFile(existingFileInfo, newFileInfo))
                        return false;
                }

                return DownloadFile(sftp, fullLocalPath, fullSftpPath);
            }
        }

        private bool DownloadFile(SftpClient sftp, string fullLocalPath, string fullSftpPath)
        {
            try
            {
                // Open new file, overwriting any existing file
                using (Stream fileStream = File.Create(fullLocalPath))
                {
                    sftp.DownloadFile(fullSftpPath, fileStream);
                    return true; // Indicates the file was downloaded
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Unable to download the file {fullSftpPath} to {fullLocalPath}", fullSftpPath, fullLocalPath);
                return false;
            }
        }

        private bool ShouldDownloadNewFile(FileSystemInfo existingFileInfo, SftpFileAttributes newFileInfo)
        {
            try
            {
                // If the file on the FTP site is not newer than our current file, the file won't be downloaded
                if (newFileInfo.LastWriteTime < existingFileInfo.LastWriteTime)
                {
                    _logger.Information("File already exists and does not need to be re-downloaded.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Unable to compare the existing file to the file to download.  Not downloading file.");
                return false;
            }

            return true;
        }
    }
}
