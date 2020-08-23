using System.Collections.Generic;

namespace FolderInfo
{
    interface IFileService
    {
        HashSet<string> GetFiles(string inFolder, string inPattern = "*");

        HashSet<string> GetFiles(IEnumerable<string> inFolders, string inPattern = "*");

        void AddOrUpdateData(IEnumerable<string> inFiles);
    }
}
