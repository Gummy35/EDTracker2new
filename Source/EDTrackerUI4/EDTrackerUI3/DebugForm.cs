using System.Windows.Forms;

namespace EDTrackerUI3
{
    public partial class DebugForm : Form
    {
        public DebugForm() => InitializeComponent();

        public void logMessage(string mess)
        {
            rtbInfo.Items.Add((object)mess);
            while (rtbInfo.Items.Count > 20)
                rtbInfo.Items.RemoveAt(0);
        }
    }
}
