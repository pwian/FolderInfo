using System.Collections.Generic;

namespace FolderInfo.Data
{
    public interface IDataService
    {
        void AddOrUpdateFile(string inFileName);

        void AddFiles(IEnumerable<string> inFileNames);
    }
}
