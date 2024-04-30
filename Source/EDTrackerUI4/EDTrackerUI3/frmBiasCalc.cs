// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.frmBiasCalc
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace EDTrackerUI3
{
  public class frmBiasCalc : Form
  {
    private IContainer components;
    private ProgressBar biasProgress;
    private Button button1;

    public frmBiasCalc() => this.InitializeComponent();

    public void stopProg() => this.biasProgress.Enabled = false;

    public void startProg() => this.biasProgress.Enabled = true;

    private void button1_Click(object sender, EventArgs e)
    {
      this.stopProg();
      this.Hide();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.biasProgress = new ProgressBar();
      this.button1 = new Button();
      this.SuspendLayout();
      this.biasProgress.Cursor = Cursors.WaitCursor;
      this.biasProgress.Location = new Point(5, 10);
      this.biasProgress.MarqueeAnimationSpeed = 20;
      this.biasProgress.Name = "biasProgress";
      this.biasProgress.Size = new Size(307, 23);
      this.biasProgress.Style = ProgressBarStyle.Marquee;
      this.biasProgress.TabIndex = 0;
      this.biasProgress.UseWaitCursor = true;
      this.button1.DialogResult = DialogResult.Cancel;
      this.button1.Location = new Point(318, 10);
      this.button1.Name = "button1";
      this.button1.Size = new Size(75, 23);
      this.button1.TabIndex = 1;
      this.button1.Text = "Dismiss";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.CancelButton = (IButtonControl) this.button1;
      this.CausesValidation = false;
      this.ClientSize = new Size(401, 45);
      this.ControlBox = false;
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.biasProgress);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (frmBiasCalc);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Calculating Bias Values";
      this.UseWaitCursor = true;
      this.ResumeLayout(false);
    }
  }
}
