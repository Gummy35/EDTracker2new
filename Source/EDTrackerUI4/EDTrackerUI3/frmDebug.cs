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

    public frmDebug() => InitializeComponent();

    public void logMessage(string mess)
    {
      rtbInfo.Items.Add((object) mess);
      while (rtbInfo.Items.Count > 20)
        rtbInfo.Items.RemoveAt(0);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && components != null)
        components.Dispose();
      base.Dispose(disposing);
    }

        private void InitializeComponent()
        {
            rtbInfo = new ListBox();
            SuspendLayout();
            // 
            // rtbInfo
            // 
            rtbInfo.CausesValidation = false;
            rtbInfo.Enabled = false;
            rtbInfo.FormattingEnabled = true;
            rtbInfo.Items.AddRange(new object[] { ">Log Messages" });
            rtbInfo.Location = new Point(-4, -1);
            rtbInfo.Margin = new Padding(4, 5, 4, 5);
            rtbInfo.Name = "rtbInfo";
            rtbInfo.Size = new Size(664, 264);
            rtbInfo.TabIndex = 63;
            rtbInfo.TabStop = false;
            rtbInfo.UseTabStops = false;
            // 
            // frmDebug
            // 
            AutoScaleDimensions = new SizeF(7F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoValidate = AutoValidate.Disable;
            ClientSize = new Size(660, 262);
            Controls.Add(rtbInfo);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "frmDebug";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Log Messages";
            TopMost = true;
            ResumeLayout(false);
        }
    }
}
