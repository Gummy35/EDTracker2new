using System.Drawing;
using System.Windows.Forms;

namespace EDTrackerUI3
{
    partial class DebugForm
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
            Name = "DebugForm";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Log Messages";
            TopMost = true;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ListBox rtbInfo;
    }
}