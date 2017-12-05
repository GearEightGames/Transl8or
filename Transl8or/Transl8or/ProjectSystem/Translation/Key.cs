using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transl8or.ProjectSystem.Translation
{
    public class Key : IKey
    {
        private List<Key> childKeys;
        private IKey parent;
        private string name;

        public string Name { get { return name; } set { name = value; } }

        public string FullName { get { return parent?.FullName + "/" + Name; } }

        public List<Key> Keys { get { return childKeys; } }
        
        public void AddChild(string name)
        {
            new Key(this, name);
        }

        public Key(IKey parent, string name)
        {
            this.name = name;
            this.parent = parent;
            childKeys = new List<Key>();
            parent.Keys.Add(this);
        }
    }
}
