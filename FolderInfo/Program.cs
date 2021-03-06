﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FolderInfo
{
    class Program
    {
        static void Main()
        {
            IFileService service = new FileService();
            var drives = DriveInfo.GetDrives().Select(drive => drive.Name);

            var watch = Stopwatch.StartNew();
            var files = service.GetFiles(drives);
            Console.WriteLine($"files founded = {files.Count}");
            watch.Stop();
            Console.WriteLine($"GetFiles. Execution time: {watch.ElapsedMilliseconds} ms");

            watch = Stopwatch.StartNew();
            service.AddData(files);
            watch.Stop();
            Console.WriteLine($"AddData. Execution time: {watch.ElapsedMilliseconds} ms");

            Console.WriteLine($"The End, files : {files.Count}");
            Console.ReadKey();
        }
    }
}
