﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace FolderInfo.Data
{
    public class DataService : IDataService
    {
        public void AddOrUpdateFile(string inFileName)
        {
            try
            {
                using (var db = new FileStoreContext())
                {
                    var file = db.Files.FirstOrDefault(f => f.FileName.Equals(inFileName, StringComparison.InvariantCultureIgnoreCase));
                    if (file == null)
                    {
                        file = new File
                        {
                            FileName = inFileName,
                            DataCreate = DateTime.Now
                        };
                    }

                    file.DataModified = DateTime.Now;
                    db.Files.AddOrUpdate(file);

                    db.SaveChanges();
                }
            }
            catch
            { }
        }

        public void AddOrUpdateFiles(IEnumerable<string> inFileNames)
        {
            using (var db = new FileStoreContext())
            {
                var files = GetFileForAddOrUpdate(db, inFileNames);
                AddOrUpdateInternal(db, files);
            }
         }

        private List<File> GetFileForAddOrUpdate(FileStoreContext inDb, IEnumerable<string> inFileNames)
        {
            var files = new List<File>();

            var filesInDb = inDb.Files.ToList();
            Console.WriteLine($"Files in Db = {filesInDb.Count}");
            foreach (var fileName in inFileNames)
            {
                var file = filesInDb.FirstOrDefault(f => f.FileName.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
                if (file == null)
                {
                    file = new File
                    {
                        FileName = fileName,
                        DataCreate = DateTime.Now
                    };
                }
                file.DataModified = DateTime.Now;

                files.Add(file);
            }

            return files;
        }

        private void AddOrUpdateInternal(FileStoreContext inDb, IEnumerable<File> inFiles)
        {
            const int CHUNK_SIZE = 10000;

            Console.WriteLine($"Files to add or update = {inFiles.Count()}, size of chunk = {CHUNK_SIZE}");
            int indexOfChunk = 0;
            foreach (var fileInChunk in inFiles.Chunk(CHUNK_SIZE))
            {
                Console.WriteLine($"Next fileInChunk = {indexOfChunk++}");
                inDb.Files.AddOrUpdate(inFiles.ToArray());
                inDb.SaveChanges();
            }
        }
    }
}
