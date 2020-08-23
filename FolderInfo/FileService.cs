using FolderInfo.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FolderInfo
{
    public class FileService : IFileService
    {
        public HashSet<string> GetFiles(string inFolder, string inPattern = "*")
        {
            var files = new List<string>();

            try
            {
                files.AddRange(Directory.GetFiles(inFolder, inPattern, SearchOption.TopDirectoryOnly));
                var directories = Directory.GetDirectories(inFolder);

                foreach (var directory in directories)
                {
                    try
                    {
                        files.AddRange(GetFiles(directory, inPattern));
                    }
                    catch (UnauthorizedAccessException) { }
                    catch (Exception) { }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception) { }

            return files.ToHashSet();
        }

        public HashSet<string> GetFiles(IEnumerable<string> inFolders, string inPattern = "*")
        {
            var files = new HashSet<string>();
            foreach (var folder in inFolders)
            {
                files.UnionWith(GetFiles(folder, inPattern));
            }

            return files;
        }

        public void AddOrUpdateData(IEnumerable<string> inFiles)
        {
            IDataService dataService = new DataService();
            foreach (var file in inFiles)
            {
                dataService.AddOrUpdateFile(file);
            }
        }
    }
}
