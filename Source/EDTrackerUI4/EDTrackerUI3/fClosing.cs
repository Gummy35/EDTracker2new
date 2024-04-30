// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.fClosing
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace EDTrackerUI3
{
  public class fClosing : Form
  {
    private BackgroundWorker backgroundWorker1;
    private IContainer components;
    private ProgressBar progressBar1;

    public fClosing()
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

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (fClosing));
      this.progressBar1 = new ProgressBar();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.progressBar1, "progressBar1");
      this.progressBar1.MarqueeAnimationSpeed = 50;
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Style = ProgressBarStyle.Marquee;
      this.progressBar1.UseWaitCursor = true;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ControlBox = false;
      this.Controls.Add((Control) this.progressBar1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (fClosing);
      this.UseWaitCursor = true;
      this.ResumeLayout(false);
    }
  }
}
