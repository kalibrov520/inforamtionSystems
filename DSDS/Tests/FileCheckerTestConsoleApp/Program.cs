using FileLoader.FTP;
using System;

namespace FileCheckerTestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var loader = new FtpFileLoader("ftp://spb-mdspoc01.internal.corp", "ftpUser", "password123");
            var items = loader.GetFiles();
            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
            }

            Console.ReadLine();
        }
    }
}
