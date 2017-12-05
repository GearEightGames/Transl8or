using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transl8or.Globals;
using Transl8or.ProjectSystem.Translation;

namespace Transl8or.ProjectSystem
{
    public abstract class Project
    {
        protected ProjectInfo info;

        public Project(ProjectInfo info)
        {
            this.info = info;
            keys = new KeyTree();
        }

        public ProjectInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        public static Project Create(ProjectInfo project)
        {
            Project p;
            switch (project.Type)
            {
                default:
                    p = new StandaloneProject(project);
                    break;
                case ProjectType.Unity:
                    p = new UnityProject(project);
                    break;
            }

            return p.Create().Load();
        }

        protected abstract Project Create();

        public static Project Load(ProjectInfo project)
        {
            Project p;
            switch (project.Type)
            {
                case ProjectType.Unity:
                    p = new UnityProject(project);
                    break;
                default:
                    p = new StandaloneProject(project);
                    break;
            }

            return p.Load();
        }

        protected abstract Project Load();


        KeyTree keys;

        public KeyTree Keys { get { return keys; } }

        public void AddKey(string text)
        {
            keys.AddKey(text);
        }
    }
}
