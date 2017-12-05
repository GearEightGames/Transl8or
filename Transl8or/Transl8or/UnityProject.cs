using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transl8or.ProjectSystem
{
    public class UnityProject : Project
    {
        public UnityProject(ProjectInfo info) : base(info)
        {

        }

        protected override Project Create()
        {
            return new UnityProject(null);
        }

        protected override Project Load()
        {
            return new UnityProject(null);
        }
    }
}
