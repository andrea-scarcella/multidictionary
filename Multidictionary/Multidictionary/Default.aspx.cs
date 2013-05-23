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
        private VanDale VanDaleDictionary;

        protected void Page_Load(object sender, EventArgs e)
        {
            string key = TextBoxURL.Text;
            VanDaleDictionary = new VanDale();
            var cssTag = VanDaleDictionary.getCssLink();
            hdr.Controls.Add(cssTag);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string key = TextBoxURL.Text;
            string languageKey = LanguageDropDown.SelectedItem.Value;
            resultpanel.Text = VanDaleDictionary.getResult(key, languageKey);
        }

        protected void languagesDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            VanDaleDictionary = new VanDale();
            // set ods object
            e.ObjectInstance = VanDaleDictionary;
        }
    }
}