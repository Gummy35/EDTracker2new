using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EDTrackerUI4.EDTrackerUI3
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public static void dumpres(ComponentResourceManager componentResourceManager, string id)
        {
            var res = componentResourceManager.GetResourceSet(System.Globalization.CultureInfo.CurrentUICulture, false, true);
            foreach (DictionaryEntry item in res)
            {
                if (item.Key.ToString().StartsWith(id))
                {
                    var k = item.Key.ToString();
                    if (k.StartsWith("$"))
                        k = k.Substring(1);
                    Debug.WriteLine($"{k} = new {item.Value.GetType().FullName} ({item.Value});");
                }
            }
        }
    }  
}
