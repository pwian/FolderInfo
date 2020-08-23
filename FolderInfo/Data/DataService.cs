using System;
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
    }
}
