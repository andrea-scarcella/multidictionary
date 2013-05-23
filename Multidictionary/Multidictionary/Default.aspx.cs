using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Web.UI.HtmlControls;
using Multidictionary.Dictionaries;

namespace Multidictionary
{
    public partial class Default : System.Web.UI.Page
    {
        private string dictionaryUrl = "http://www.vandale.nl/";
        private string dictionaryQueryString = "opzoeken?pattern=~&lang=nn";
        private IEnumerable<HtmlNode> csslink;

        protected void Page_Load(object sender, EventArgs e)
        {
            //your code
            string key = TextBoxURL.Text;
            string url = "";
            url = buildUrl(key);
            //getVandaleSupportedLanguages(url);
            injectVanDaleCss(url);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string key = TextBoxURL.Text;
            string url = "";
            url = buildUrl(key);
            injectVanDaleResult(url);

        }

        private string buildUrl(string key)
        {
            string outp = "";
            outp += dictionaryUrl;
            outp += (dictionaryQueryString.Replace("~", key));
            return outp;
        }

        private void injectVanDaleCss(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            csslink = (from el in document.DocumentNode.SelectNodes("//link[@type='text/css']")
                       where
                       (el.Attributes["rel"] != null) && ("stylesheet".Equals(el.Attributes["rel"].Value))
                       select el);
            if (csslink != null && csslink.Count() > 0)
            {
                HtmlLink cssLink = new HtmlLink();
                cssLink.Href = dictionaryUrl + csslink.FirstOrDefault().Attributes["href"].Value;//"path to CSS";
                cssLink.Attributes["type"] = "text/css";
                cssLink.Attributes["media"] = "all";
                cssLink.Attributes.Add("rel", "stylesheet");//without this attribute it does not work...
                hdr.Controls.Add(cssLink);
            }
        }

        private void injectVanDaleResult(string url)
        {
            var webGet = new HtmlWeb();
            var document = webGet.Load(url);
            var definitieDiv = from el in document.DocumentNode.Descendants()
                               where (el.Attributes["id"] != null) && ("content-area".Equals(el.Attributes["id"].Value))
                               select el;
            if (definitieDiv != null && definitieDiv.Count() > 0)
            {
                resultpanel.Text = definitieDiv.FirstOrDefault().OuterHtml;
            }
        }

        protected void languagesDataSource_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {

        }
        protected void languagesDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            VanDale myDictionary = new VanDale(dictionaryUrl);
            // set ods object
            e.ObjectInstance = myDictionary;
        }
    }
}