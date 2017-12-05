using System.Collections.Generic;

namespace Transl8or.ProjectSystem.Translation
{
    public interface IKey
    {
        List<Key> Keys { get; }

        string Name { get; set; }

        string FullName { get; }

        void AddChild(string name);
    }
}