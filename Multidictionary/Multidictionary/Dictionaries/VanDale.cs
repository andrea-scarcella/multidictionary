using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace Multidictionary.Dictionaries
{
    public class VanDale
    {
        private string url;

        public VanDale(string url)
        {
            // TODO: Complete member initialization
            this.url = url;
        }

        public VanDale()
        {
            // TODO: Complete member initialization
        }

        public IEnumerable<FromLanguageToLanguage> getLanguages() {
            IEnumerable<FromLanguageToLanguage> listOfOptionElemnts = null;
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
           
            listOfOptionElemnts = from el in document.DocumentNode.SelectNodes("//select[@id='translate-options']/option") select new FromLanguageToLanguage { key = el.Attributes["value"].Value, value = el.NextSibling.OuterHtml };
            return listOfOptionElemnts;
        }
        public string key { get; set; }
        public string value{ get; set; }
    }
    
}