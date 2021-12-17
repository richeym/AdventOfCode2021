using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Util
{
    public static class FileUtil
    {
        public static IList<T> ParseRecords<T>(string path)
        {
            return File.ReadAllLines(path).Select(x => (T)Convert.ChangeType(x, typeof(T))).ToList();
        }
    }
}