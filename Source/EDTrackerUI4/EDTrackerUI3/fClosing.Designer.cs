using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace EDTrackerUI3
{
    public partial class fClosing : Form
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
            progressBar1 = new ProgressBar();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new System.Drawing.Point(16, 18);
            progressBar1.Margin = new Padding(4, 5, 4, 5);
            progressBar1.MarqueeAnimationSpeed = 50;
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new System.Drawing.Size(337, 35);
            progressBar1.Style = ProgressBarStyle.Marquee;
            progressBar1.TabIndex = 0;
            progressBar1.UseWaitCursor = true;
            // 
            // fClosing
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(369, 69);
            ControlBox = false;
            Controls.Add(progressBar1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 5, 4, 5);
            Name = "fClosing";
            Text = "Application Closing...";
            UseWaitCursor = true;
            ResumeLayout(false);
        }
        #endregion
    }
}