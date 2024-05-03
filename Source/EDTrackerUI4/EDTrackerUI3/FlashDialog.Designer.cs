using System.Drawing;
using System.Windows.Forms;

namespace EDTrackerUI3
{
    partial class FlashDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rtLog = new RichTextBox();
            progressBar1 = new ProgressBar();
            bClose = new Button();
            SuspendLayout();
            // 
            // rtLog
            // 
            rtLog.BorderStyle = BorderStyle.FixedSingle;
            rtLog.DetectUrls = false;
            rtLog.Font = new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            rtLog.Location = new Point(7, 12);
            rtLog.Name = "rtLog";
            rtLog.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            rtLog.Size = new Size(517, 273);
            rtLog.TabIndex = 0;
            rtLog.Text = "";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(7, 291);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(436, 25);
            progressBar1.TabIndex = 1;
            // 
            // bClose
            // 
            bClose.Enabled = false;
            bClose.Location = new Point(449, 291);
            bClose.Name = "bClose";
            bClose.Size = new Size(75, 25);
            bClose.TabIndex = 2;
            bClose.Tag = "1";
            bClose.Text = "Close";
            bClose.UseVisualStyleBackColor = true;
            bClose.Click += bClose_Click;
            // 
            // flashDialog
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(536, 328);
            ControlBox = false;
            Controls.Add(bClose);
            Controls.Add(progressBar1);
            Controls.Add(rtLog);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FlashDialog";
            Text = "Flash EDTracker";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox rtLog;
    }
}