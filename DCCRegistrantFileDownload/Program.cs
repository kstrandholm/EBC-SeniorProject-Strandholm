using System;

namespace DCCRegistrantFileDownload
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Getting File...");

                // Download the file
                SFTPDownload.DownloadFileUsingSftpClient(fileName, localFileLocation);

                // If it suceeded, write so to the console
                // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
                Console.WriteLine("\nFile retrieved.");
            }
            catch (Exception ex)
            {
                // Something went wrong - indicate so
                // TODO: Since this will hopefully become an automated process, find some better way to indicate a success vs. failure
                Console.WriteLine("\nOh no, something went wrong!~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\nHere's what I know:");
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }

#if DEBUG
            Console.WriteLine("\nPress any key to Continue...");
            Console.ReadKey();
#endif
        }
    }
}
