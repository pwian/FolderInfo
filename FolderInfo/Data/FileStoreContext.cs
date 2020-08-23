using System.Data.Entity;

namespace FolderInfo.Data
{
    public class FileStoreContext : DbContext
    {
        public FileStoreContext() : base("connectionString")
        {
        }

        public DbSet<File> Files { get; set; }
    }
}
