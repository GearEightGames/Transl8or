using System;

namespace Transl8or.ProjectSystem
{
    public class StandaloneProject : Project
    {
        public StandaloneProject(ProjectInfo info) : base(info)
        {
        }

        protected override Project Create()
        {
            return new StandaloneProject(null);
        }

        protected override Project Load()
        {
            return new StandaloneProject(null);
        }
    }
}