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
            richTextBox1 = new RichTextBox();
            SuspendLayout();
            // 
            // richTextBox1
            // 
            richTextBox1.Location = new Point(11, 12);
            richTextBox1.Margin = new Padding(4, 5, 4, 5);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.Size = new Size(218, 46);
            richTextBox1.TabIndex = 64;
            richTextBox1.Text = "Rotate you EDTracker in all axis until values no longer change.";
            // 
            // magForm
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(245, 207);
            Controls.Add(richTextBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "magForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Magnetometer Calibration";
            ResumeLayout(false);
        }
    }
}
