using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Transl8or.ProjectSystem
{
    public class ProjectManager
    {
        private Project project;
        public const int MIN_FILTER_LENGTH = 2;
        private string filter;

        public Project ProjectInstance
        {
            get { return project; }
            set { project = value; }
        }
        
        public ProjectInfo Info
        {
            get { return project.Info; }
            set { project.Info = value; }
        }

        public void Create(ProjectInfo project)
        {
            this.project = Project.Create(project);
        }

        public void Load(ProjectInfo project)
        {
            this.project = Project.Load(project);
        }

        public void SetFilter(string text)
        {
            if (text.Length >= MIN_FILTER_LENGTH)
            {
                filter = text;
                ThreadStart filterThreadStart = new ThreadStart(ApplyFilter);
                Thread filterThread = new Thread(filterThreadStart);
                filterThread.Start();
            }
            else
                filter = string.Empty;
        }

        private void ApplyFilter()
        {

        }
    }
}
