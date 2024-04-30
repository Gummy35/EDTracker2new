// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.frmDebug
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace EDTrackerUI3
{
  public class frmDebug : Form
  {
    private IContainer components;
    private ListBox rtbInfo;

    public frmDebug() => this.InitializeComponent();

    public void logMessage(string mess)
    {
      this.rtbInfo.Items.Add((object) mess);
      while (this.rtbInfo.Items.Count > 20)
        this.rtbInfo.Items.RemoveAt(0);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.rtbInfo = new ListBox();
      this.SuspendLayout();
      this.rtbInfo.CausesValidation = false;
      this.rtbInfo.Enabled = false;
      this.rtbInfo.FormattingEnabled = true;
      this.rtbInfo.Items.AddRange(new object[1]
      {
        (object) ">Log Messages"
      });
      this.rtbInfo.Location = new Point(-4, -1);
      this.rtbInfo.Name = "rtbInfo";
      this.rtbInfo.Size = new Size(664, 264);
      this.rtbInfo.TabIndex = 63;
      this.rtbInfo.TabStop = false;
      this.rtbInfo.UseTabStops = false;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoValidate = AutoValidate.Disable;
      this.ClientSize = new Size(660, 262);
      this.Controls.Add((Control) this.rtbInfo);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (frmDebug);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Log Messages";
      this.TopMost = true;
      this.ResumeLayout(false);
    }
  }
}
