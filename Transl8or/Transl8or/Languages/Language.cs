using System;
using System.Xml.Serialization;

namespace Transl8or.ProjectSystem.Languages
{
    [Serializable]
    public class Language
    {
        [NonSerialized]
        private string abbreviation;

        [XmlAttribute("abbreviation")]
        public string Abbreviation
        {
            get { return abbreviation; }
            set { abbreviation = value; }
        }

        [NonSerialized]
        private string name;

        [XmlAttribute("name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [NonSerialized]
        private TextEncoding encoding;

        [XmlAttribute("encoding")]
        public TextEncoding Encoding
        {
            get { return encoding; }
            set { encoding = value; }
        }

        [NonSerialized]
        private CultureLink culture;

        [XmlElement]
        public CultureLink Culture
        {
            get { return culture; }
            set { culture = value; }
        }

        public static Language FromCulture(CultureLink culture)
        {
            return new Language() { name = culture.Language, abbreviation = culture.CultureInfoCode, culture = culture, encoding = TextEncoding.UTF32 };
        }
    }
}
