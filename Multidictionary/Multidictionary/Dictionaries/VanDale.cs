using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using System.Web.UI.HtmlControls;

namespace Multidictionary.Dictionaries
{
    public class VanDale
    {
        const string searchKeyPlaceholderPattern = "~";
        const string languagesKeyPlaceholderPattern = "!";
        private string baseUrl = "http://www.vandale.nl/";
        private string dictionaryQueryString = "opzoeken?pattern=" + searchKeyPlaceholderPattern + "&lang=" + languagesKeyPlaceholderPattern;

        public VanDale(string url)
        {
            // TODO: Complete member initialization
            this.baseUrl = url;
        }

        public VanDale()
        {
            // TODO: Complete member initialization
        }

        public IEnumerable<FromLanguageToLanguage> getLanguages()
        {
            IEnumerable<FromLanguageToLanguage> listOfOptionElemnts = null;
            var webGet = new HtmlWeb();
            var document = webGet.Load(baseUrl);

            listOfOptionElemnts = from el in document.DocumentNode.SelectNodes("//select[@id='translate-options']/option") select new FromLanguageToLanguage { id = el.Attributes["value"].Value, name = el.NextSibling.OuterHtml };
            return listOfOptionElemnts;
        }

        private string buildUrl(string key, string lan = null)
        {
            string outp = "";
            outp += baseUrl;
            outp += (dictionaryQueryString.Replace(searchKeyPlaceholderPattern, key));
            if (null == lan)
            {
                lan = (getLanguages().FirstOrDefault() ?? new FromLanguageToLanguage()).id;//ther should always be at least one language option.

            }
            outp = outp.Replace(languagesKeyPlaceholderPattern, lan);
            return outp;
        }

        public string getResult(string key, string lan)
        {
            string url = buildUrl(key, lan);
            string outp = "";
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            var definitieDiv = from el in document.DocumentNode.Descendants()
                               where (el.Attributes["id"] != null) && ("content-area".Equals(el.Attributes["id"].Value))
                               select el;
            if (definitieDiv != null && definitieDiv.Count() > 0)
            {
                outp = definitieDiv.FirstOrDefault().OuterHtml;
            }
            return outp;
        }

        public HtmlLink getCssLink()
        {
            var webGet = new HtmlWeb();
            var url = buildUrl("");
            var document = webGet.Load(url);
            var csslink = (from el in document.DocumentNode.SelectNodes("//link[@type='text/css']")
                           where
                           (el.Attributes["rel"] != null) && ("stylesheet".Equals(el.Attributes["rel"].Value))
                           select el);
            HtmlLink cssLink = new HtmlLink();
            if (csslink != null && csslink.Count() > 0)
            {
                cssLink.Href = baseUrl + csslink.FirstOrDefault().Attributes["href"].Value;//"path to CSS";
                cssLink.Attributes["type"] = "text/css";
                cssLink.Attributes["media"] = "all";
                cssLink.Attributes.Add("rel", "stylesheet");//without this attribute it does not work...
            }
            return cssLink;
        }

    }

}