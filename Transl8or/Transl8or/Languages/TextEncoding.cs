using System;

namespace Transl8or.ProjectSystem.Languages
{
    [Serializable]
    public enum TextEncoding : byte
    {
        ASCII,
        ANSI,
        UTF7,
        UTF8,
        UTF16,
        UTF32
    }
}