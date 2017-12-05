using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Transl8or.ProjectSystem.Languages
{
    [Serializable]
    public class CultureLink
    {
        [NonSerialized]
        private string twoLCC;

        [XmlAttribute("TwoLCC")]
        public string TwoLetterCountryCode
        {
            get { return twoLCC; }
            set { twoLCC = value; }
        }

        [NonSerialized]
        private string threeLCC;

        [XmlAttribute("ThreeLCC")]
        public string ThreeLetterCountyCode
        {
            get { return threeLCC; }
            set { threeLCC = value; }
        }

        [NonSerialized]
        private string country;

        [XmlAttribute]
        public string Country
        {
            get { return country; }
            set { country = value; }
        }

        [NonSerialized]
        private string twoLLC;

        [XmlAttribute("TwoLLC")]
        public string TwoLetterLanguageCode
        {
            get { return twoLLC; }
            set { twoLLC = value; }
        }

        [NonSerialized]
        private string threeLLC;

        [XmlAttribute("ThreeLLC")]
        public string ThreeLetterLanguageCode
        {
            get { return threeLLC; }
            set { threeLLC = value; }
        }

        [NonSerialized]
        private string language;

        [XmlAttribute]
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        [NonSerialized]
        private string cultureInfoCode;

        [NonSerialized]
        private static Dictionary<string, CultureLink> cultures;

        [XmlAttribute]
        public string CultureInfoCode
        {
            get { return cultureInfoCode; }
            set { cultureInfoCode = value; }
        }
        
        [XmlIgnore]
        public static Dictionary<string, CultureLink> Cultures
        {
            get
            {
                if (cultures == null)
                    LoadCultures();
                return cultures;
            }
        }

        private const string CULTURES = "Transl8or.ProjectSystem.Languages.cultures.txt";

        private CultureLink(string[] langData)
        {
            country = langData[0];
            twoLLC = langData[1];
            threeLLC = langData[2];
            language = langData[3];
            twoLCC = langData[4];
            threeLCC = langData[5];
            cultureInfoCode = langData[6];
        }

        public CultureLink() { }

        private static void LoadCultures()
        {
            Dictionary<string, CultureLink> cultures = new Dictionary<string, CultureLink>();
            using (Stream s = Assembly.GetCallingAssembly().GetManifestResourceStream(CULTURES))
            {
                using (StreamReader r = new StreamReader(s))
                {
                    while (!r.EndOfStream)
                    {
                        string langInfo = r.ReadLine();
                        string[] langData = langInfo.Split(':');
                        cultures.Add(langData[langData.Length - 1], new CultureLink(langData));
                    }
                }
            }

            CultureLink.cultures = cultures;
        }
    }
}
