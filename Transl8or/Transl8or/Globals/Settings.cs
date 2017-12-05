using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Transl8or.ProjectSystem;

namespace Transl8or.Globals
{
    public static class Settings
    {
        private static List<ProjectInfo> projects;
        private static char separator = '/';

        public static char Separator
        {
            get { return separator; }
            set { separator = value; }
        }

        public static int ProjectsCount { get { return projects.Count; } }
        public static List<ProjectInfo> Projects { get { return projects; } }

        static Settings()
        {
            projects = new List<ProjectInfo>();
            Load();
        }

        private static void Load()
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string path = Path.Combine(appdata, "GearEight Games", "Transl8or");
            DirectoryInfo directoryInfo = Directory.CreateDirectory(path);
            FileInfo[] files = directoryInfo.GetFiles();
            if (files.Length == 0)
                return;

            foreach (var file in files)
            {
                switch (file.Name)
                {
                    case "recent.txt":
                        ReadRecent(file.FullName);
                        break;
                }
            }
        }

        private static void ReadRecent(string file)
        {
            string[] content = File.ReadAllLines(file);
            List<string> garbage = new List<string>();
            foreach (var line in content)
            {
                DirectoryInfo directory = new DirectoryInfo(line);
                if (!directory.Exists)
                {
                    garbage.Add(line);
                    continue;
                }
                FileInfo projectFile = directory.EnumerateFiles().First(f => f.Extension == Constants.FILE_EXTENSION);

                ProjectInfo p = new ProjectInfo(projectFile);
                if (!p.Corrupted)
                    projects.Add(p);
                else
                    garbage.Add(line);
            }

            projects = projects.OrderBy(p => p.Creation).ToList();

            var data = content.ToList();
            data.RemoveAll(l => garbage.Contains(l));
            File.WriteAllLines(file, data);
        }

    }
}
