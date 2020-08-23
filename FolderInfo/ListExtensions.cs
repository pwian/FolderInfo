using System;
using System.Collections.Generic;
using System.Linq;

namespace FolderInfo
{
    public static class ListExtensions
    {
        public static IEnumerable<IEnumerable<T>> Chunk<T>(this IEnumerable<T> inSource, int inChunkSize)
        {
            if (inChunkSize <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(inChunkSize)} cannot be less or equal zero");
            }

            while (inSource.Any())
            {
                yield return inSource.Take(inChunkSize);
                inSource = inSource.Skip(inChunkSize);
            }
        }
    }
}
