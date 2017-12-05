using System;
using System.IO;
using System.Linq;

namespace Transl8or.Globals
{
    public static class Validation
    {
        public static bool IsUnityDirectory(DirectoryInfo directory)
        {
            if (!directory.Exists)
                return false;

            return directory.GetDirectories().Where(d => d.Name == "Assets" || d.Name == "ProjectSettings").ToArray().Length == 2;
        }

        public static bool IsValidDirectory(string directory, bool relativeAllowed = false)
        {
            try
            {
                Path.GetFullPath(directory);
                if (relativeAllowed)
                    return true;
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
                return false;
            }

            return Path.IsPathRooted(directory);
        }

        public static bool ContainsProject(DirectoryInfo directory)
        {
            if (!directory.Exists)
                return false;

            return directory.EnumerateFiles().Where(f => f.Extension == Constants.FILE_EXTENSION).ToArray().Length > 0;
        }
    }
}
