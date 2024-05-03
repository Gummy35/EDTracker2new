using System;
using System.Windows.Forms;

namespace EDTrackerUI3
{
    public partial class BiasCalc : Form
    {
        public BiasCalc() => this.InitializeComponent();

        public void stopProg() => this.biasProgress.Enabled = false;

        public void startProg() => this.biasProgress.Enabled = true;

        private void button1_Click(object sender, EventArgs e)
        {
            this.stopProg();
            this.Hide();
        }
    }
}
