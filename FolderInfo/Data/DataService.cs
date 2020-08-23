using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
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

        public void AddFiles(IEnumerable<string> inFileNames)
        {
            using (var db = new FileStoreContext())
            {
                var files = GetFileForAdd(db, inFileNames);
                AddInternal(db, files);
            }
         }

        private List<File> GetFileForAdd(FileStoreContext inDb, IEnumerable<string> inFileNames)
        {
            var files = new List<File>();

            var filesInDb = inDb.Files.ToList();
            Console.WriteLine($"Files in Db = {filesInDb.Count}");

            var maxFileNameLengthAttribute = typeof(File)
                .GetProperty(nameof(File.FileName))
                .GetCustomAttributes(typeof(StringLengthAttribute), false)
                .Cast<StringLengthAttribute>()
                .SingleOrDefault();
            var maxFileNameLength = maxFileNameLengthAttribute != null ? maxFileNameLengthAttribute.MaximumLength : 450;

            foreach (var fileName in inFileNames.Skip(1300000))
            {
                if (fileName.Length > maxFileNameLength)
                {
                    Console.WriteLine($"{fileName} is very long, skip this");
                    continue;
                }

                var file = filesInDb.FirstOrDefault(f => f.FileName.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
                if (file == null)
                {
                    file = new File
                    {
                        FileName = fileName,
                        DataCreate = DateTime.Now,
                        DataModified = DateTime.Now
                    };
                    files.Add(file);
                }                
            }

            return files;
        }

        private void AddInternal(FileStoreContext inDb, IEnumerable<File> inFiles)
        {
            const int CHUNK_SIZE = 10000;

            var totalChunks = (inFiles.Count() - 1) / CHUNK_SIZE + 1;
            int indexOfChunk = 0;
            foreach (var fileInChunk in inFiles.Chunk(CHUNK_SIZE))
            {
                Console.WriteLine($"Index Chunk = {indexOfChunk++}, size in chunk = {CHUNK_SIZE}, total chunks = {totalChunks}");
                inDb.Files.AddRange(fileInChunk.ToArray());

                try
                {
                    inDb.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    Console.WriteLine(ex);
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            Console.WriteLine(message);
                        }
                    }
                }
                catch (Exception) { };
            }
        }
    }
}
