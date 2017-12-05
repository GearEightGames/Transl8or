using System;
using System.Collections.Generic;
using System.Linq;
using Transl8or.Globals;

namespace Transl8or.ProjectSystem.Translation
{
    public class KeyTree : IKey
    {
        private List<Key> keys;

        public KeyTree()
        {
            keys = new List<Key>();
        }

        public void AddKey(string possibleKeys)
        {
            if (possibleKeys.Length == 0)
                return;

            string[] possibleKeyCollection = possibleKeys.Split(Settings.Separator);
            if (!possibleKeyCollection.All(s => s.Length > 0))
                return;
            IKey parent = this;
            for (int i = 0; i < possibleKeyCollection.Length; i++)
            {
                string item = possibleKeyCollection[i];
                Key existingkey = parent.Keys.FirstOrDefault(k => k.Name == item);
                parent = existingkey == null ? new Key(parent, item) : existingkey;
            }

            OnKeyAdded();
        }

        protected void OnKeyAdded()
        {
            KeyAdded?.Invoke();
        }

        public delegate void KeyAddedEventHandler();
        public event KeyAddedEventHandler KeyAdded;

        public void AddChild(string name)
        {
            new Key(this, name);
        }

        public string Name
        {
            get
            {
                return string.Empty;
            }

            set
            {

            }
        }

        public string FullName
        {
            get
            {
                return string.Empty;
            }
        }

        public List<Key> Keys
        {
            get
            {
                return keys;
            }
        }
    }
}