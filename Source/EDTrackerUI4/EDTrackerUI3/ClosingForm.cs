using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace EDTrackerUI3
{
    public partial class ClosingForm : Form
    {
        private BackgroundWorker backgroundWorker1;
        private ProgressBar progressBar1;

        public ClosingForm()
        {
            this.InitializeComponent();
            this.backgroundWorker1 = new BackgroundWorker();
            this.backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int percentProgress = 1; percentProgress <= 100; ++percentProgress)
            {
                Thread.Sleep(100);
                this.backgroundWorker1.ReportProgress(percentProgress);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar1.Value = e.ProgressPercentage;
        }
    }
}
