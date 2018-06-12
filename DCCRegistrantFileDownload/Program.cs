using System;

namespace DCCRegistrantFileDownload
{
    public class Program
    {
        public void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Getting File...");

                FTPOperations.GetFileUsingSFTPClient("SampleFile.txt");

                Console.WriteLine("File retrieved.");
#if DEBUG
                Console.WriteLine("Press Any Key to Continue...");
                Console.ReadKey();
#endif
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
            }
        }
    }
}
