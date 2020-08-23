using System.Collections.Generic;

namespace FolderInfo.Data
{
    public interface IDataService
    {
        void AddOrUpdateFile(string inFileName);

        void AddOrUpdateFiles(IEnumerable<string> inFileNames);
    }
}
