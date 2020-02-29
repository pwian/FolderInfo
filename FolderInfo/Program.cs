using System;
using System.IO;

namespace FolderInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"D:\AMM\Film";
            var pattern = "*";

            var files = Directory.GetFiles(path, pattern, SearchOption.AllDirectories);
            var outputFile = Path.Combine(path, "Files.txt");
            File.WriteAllLines(outputFile, files);
            Console.WriteLine($"The End, files : {files.Length}");
            Console.ReadKey();
        }
    }
}
