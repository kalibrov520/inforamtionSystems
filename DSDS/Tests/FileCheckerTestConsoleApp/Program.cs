﻿using FileLoader.FTP;
using System;
using System.Collections.Generic;

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
                Console.WriteLine($"{item.LastModified} {item.FullPath}");
            }

            Console.WriteLine("===================");

            var patterns = new List<string>
            {
                "*.csv"
            };

            var results = loader.GetFilesWithPattern(patterns);

            foreach (var item in results)
            {
                Console.WriteLine($"{item.LastModified} {item.FullPath}");
            }

            Console.ReadLine();
        }
    }
}
