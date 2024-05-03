using System.Drawing;
using System.Windows.Forms;
using System;

namespace EDTrackerUI3
{
    partial class BiasCalc
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
            biasProgress = new ProgressBar();
            button1 = new Button();
            SuspendLayout();
            // 
            // biasProgress
            // 
            biasProgress.Cursor = Cursors.WaitCursor;
            biasProgress.Location = new Point(6, 12);
            biasProgress.Margin = new Padding(4, 3, 4, 3);
            biasProgress.MarqueeAnimationSpeed = 20;
            biasProgress.Name = "biasProgress";
            biasProgress.Size = new Size(358, 27);
            biasProgress.Style = ProgressBarStyle.Marquee;
            biasProgress.TabIndex = 0;
            biasProgress.UseWaitCursor = true;
            // 
            // button1
            // 
            button1.DialogResult = DialogResult.Cancel;
            button1.Location = new Point(371, 12);
            button1.Margin = new Padding(4, 3, 4, 3);
            button1.Name = "button1";
            button1.Size = new Size(88, 27);
            button1.TabIndex = 1;
            button1.Text = "Dismiss";
            button1.UseVisualStyleBackColor = true;
            button1.UseWaitCursor = true;
            button1.Click += button1_Click;
            // 
            // BiasCalc
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button1;
            CausesValidation = false;
            ClientSize = new Size(468, 52);
            ControlBox = false;
            Controls.Add(button1);
            Controls.Add(biasProgress);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Margin = new Padding(4, 3, 4, 3);
            Name = "BiasCalc";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "Calculating Bias Values";
            UseWaitCursor = true;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar biasProgress;
    }
}