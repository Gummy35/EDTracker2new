using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace EDTrackerUI3
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            this.InitializeComponent();
            this.Text = string.Format("About {0}", (object)this.AssemblyTitle);
            this.labelProductName.Text = this.AssemblyProduct;
            this.labelVersion.Text = string.Format("Version {0}", (object)this.AssemblyVersion);
            this.labelCopyright.Text = this.AssemblyCopyright;
            this.labelCompanyName.Text = this.AssemblyCompany;
        }

        public string AssemblyTitle
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (customAttributes.Length > 0)
                {
                    AssemblyTitleAttribute assemblyTitleAttribute = (AssemblyTitleAttribute)customAttributes[0];
                    if (assemblyTitleAttribute.Title != "")
                        return assemblyTitleAttribute.Title;
                }
                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string AssemblyDescription
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return customAttributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)customAttributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return customAttributes.Length == 0 ? "" : ((AssemblyProductAttribute)customAttributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return customAttributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)customAttributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] customAttributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return customAttributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)customAttributes[0]).Company;
            }
        }

        private void okButton_Click(object sender, EventArgs e) => this.Close();

        private void tableLayoutPanel_Paint(object sender, PaintEventArgs e)
        {
        }
    }
}
