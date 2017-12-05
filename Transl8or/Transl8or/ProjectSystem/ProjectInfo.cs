using System;
using System.ComponentModel;
using System.IO;
using Transl8or.Globals;

namespace Transl8or.ProjectSystem
{
    public class ProjectInfo : INotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name")); }
        }

        private string path;

        public string Path
        {
            get { return path; }
            set { path = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Path")); }
        }

        private DateTime creation;
        private FileInfo projectFile;
        private bool corrupted;
        private ProjectType type;

        public ProjectInfo(FileInfo projectFile)
        {
            corrupted = false;
            this.projectFile = projectFile;
            Parse(projectFile);
        }

        public ProjectInfo(string Name, string Directory)
        {
            name = Name;
            path = Directory;
            type = Validation.IsUnityDirectory(new DirectoryInfo(path)) ? ProjectType.Unity : ProjectType.Standalone;

        }

        private void Parse(FileInfo projectFile)
        {
            path = projectFile.FullName;
            foreach (var line in File.ReadAllLines(path))
            {
                string[] parse = line.Split(':');
                bool parsable = parse.Length > 0;
                bool multielement = parse.Length > 1;

                string key = string.Empty, value = string.Empty;

                if (parsable)
                    key = parse[0];
                else
                    continue;
                key = key.Trim();

                if (multielement)
                    value = string.Join(":", parse, 1, parse.Length - 1);
                value = value.Trim();

                switch (key)
                {
                    case "name":
                        if (multielement)
                            name = value;
                        else
                            corrupted = true;
                        break;
                    case "creation":
                        if (multielement)
                        {
                            DateTime result;
                            if (DateTime.TryParse(value, out result))
                                creation = result;
                            else
                                corrupted = true;
                        }
                        else
                            corrupted = true;
                        break;
                }
            }
        }

        public DateTime Creation
        {
            get { return creation; }
            set { creation = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Creation")); }
        }

        public bool Corrupted { get { return corrupted; } }

        public ProjectType Type
        {
            get
            {
                return type;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
