// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.magForm
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace EDTrackerUI3
{
  public class magForm : Form
  {
    private IContainer components;
    private RichTextBox richTextBox1;

    public magForm() => this.InitializeComponent();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.richTextBox1 = new RichTextBox();
      this.SuspendLayout();
      this.richTextBox1.Location = new Point(11, 12);
      this.richTextBox1.Name = "richTextBox1";
      this.richTextBox1.Size = new Size(218, 46);
      this.richTextBox1.TabIndex = 64;
      this.richTextBox1.Text = "Rotate you EDTracker in all axis until values no longer change.";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(245, 207);
      this.Controls.Add((Control) this.richTextBox1);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (magForm);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = "Magnetometer Calibration";
      this.ResumeLayout(false);
    }
  }
}
