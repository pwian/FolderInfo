using System;
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
            var files = new List<File>();

            using (var db = new FileStoreContext())
            {
                var filesInDb = db.Files.ToList();
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

                Console.WriteLine($"Files to add or update = {files.Count}");
                
                db.Files.AddOrUpdate(files.ToArray());
                db.SaveChanges();                    
            }
         }
    }
}
