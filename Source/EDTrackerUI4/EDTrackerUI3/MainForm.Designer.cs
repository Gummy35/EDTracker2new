using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace EDTrackerUI3
{
    partial class MainForm
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
            components = new Container();
            bWipeAll = new Button();
            cbSketches = new ComboBox();
            cbPort = new ComboBox();
            bFlash = new Button();
            groupBox1 = new GroupBox();
            bScanPorts = new Button();
            bMonitor = new Button();
            gbBias = new GroupBox();
            biasAX = new TextBox();
            axminus = new Button();
            axplus = new Button();
            udAX = new TextBox();
            label5 = new Label();
            biasAY = new TextBox();
            ayminus = new Button();
            ayplus = new Button();
            udAY = new TextBox();
            label6 = new Label();
            biasAZ = new TextBox();
            azminus = new Button();
            azplus = new Button();
            udAZ = new TextBox();
            label7 = new Label();
            biasGZ = new TextBox();
            gzminus = new Button();
            gzplus = new Button();
            udGZ = new TextBox();
            label4 = new Label();
            biasGY = new TextBox();
            gyminus = new Button();
            gyplus = new Button();
            udGY = new TextBox();
            label3 = new Label();
            biasGX = new TextBox();
            gxminus = new Button();
            udGX = new TextBox();
            gxplus = new Button();
            label2 = new Label();
            bCalcBias = new Button();
            gbHotKey = new GroupBox();
            btEnableAC = new Button();
            tbAutoCentre = new TextBox();
            btToggleAutoCentre = new Button();
            cbHKey = new ComboBox();
            cbCOntrollerButtons = new ComboBox();
            cbControllerButton = new CheckBox();
            cbHotKey = new CheckBox();
            gbTrackerConfig = new GroupBox();
            bOrientate = new Button();
            tbOrientation = new TextBox();
            bSensorMode = new Button();
            tbSensorMode = new TextBox();
            tbSketch = new TextBox();
            gbDriftComp = new GroupBox();
            lbTimer = new Label();
            tbTime = new TextBox();
            bSaveDriftComp = new Button();
            bResetView = new Button();
            tbDriftComp = new TextBox();
            lbComp = new Label();
            tbYawDrift = new TextBox();
            lbYawDrift = new Label();
            btCompDown = new Button();
            btCompUp = new Button();
            gbScaling = new GroupBox();
            tbSmoothing = new TextBox();
            lbSmooth = new Label();
            sliderSmoothing = new TrackBar();
            tbRespMode = new TextBox();
            bRespMode = new Button();
            tbPitchScaling = new TextBox();
            label11 = new Label();
            tbYawScaling = new TextBox();
            label10 = new Label();
            cbFineAdjust = new CheckBox();
            btYawScaleDown = new Button();
            btPitchScaleDown = new Button();
            btPitchScaleUp = new Button();
            btYawScaleUp = new Button();
            tbTemps = new TextBox();
            lbTemps = new Label();
            tbMonitor = new TextBox();
            notifyIcon1 = new NotifyIcon(components);
            lbAxis4 = new Label();
            lbAxis3 = new Label();
            lbAxis2 = new Label();
            lbAxis1 = new Label();
            menuStrip1 = new MenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            modeToolStripMenuItem = new ToolStripMenuItem();
            uSERToolStripMenuItem = new ToolStripMenuItem();
            tESTToolStripMenuItem = new ToolStripMenuItem();
            dEVToolStripMenuItem = new ToolStripMenuItem();
            Help = new ToolStripMenuItem();
            debugToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem = new ToolStripMenuItem();
            hideToolStripMenuItem = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            gbMagCalib = new GroupBox();
            label1 = new Label();
            tbOffZ = new TextBox();
            tbOffY = new TextBox();
            tbOffX = new TextBox();
            label18 = new Label();
            tbarMagSens = new TrackBar();
            button1 = new Button();
            label16 = new Label();
            tbMagSamples = new TextBox();
            tbMinX = new TextBox();
            label9 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            label15 = new Label();
            tbMaxZ = new TextBox();
            tbMinZ = new TextBox();
            tbMaxY = new TextBox();
            tbMinY = new TextBox();
            tbMaxX = new TextBox();
            btSave = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            elementHost1 = new ElementHost();
            tabPage2 = new TabPage();
            groupBox2 = new GroupBox();
            tbMagMat9 = new TextBox();
            tbMagMat8 = new TextBox();
            tbMagMat7 = new TextBox();
            tbMagMat6 = new TextBox();
            tbMagMat5 = new TextBox();
            tbMagMat4 = new TextBox();
            tbMagMat1 = new TextBox();
            tbMagMat2 = new TextBox();
            tbMagMat3 = new TextBox();
            elementHost2 = new ElementHost();
            gbMagGyro = new GroupBox();
            btMagResetView = new Button();
            btMagAutoBias = new Button();
            tbGyroX = new TextBox();
            lbGX = new Label();
            tbGyroZ = new TextBox();
            tbGyroY = new TextBox();
            groupBox1.SuspendLayout();
            gbBias.SuspendLayout();
            gbHotKey.SuspendLayout();
            gbTrackerConfig.SuspendLayout();
            gbDriftComp.SuspendLayout();
            gbScaling.SuspendLayout();
            ((ISupportInitialize)sliderSmoothing).BeginInit();
            menuStrip1.SuspendLayout();
            gbMagCalib.SuspendLayout();
            ((ISupportInitialize)tbarMagSens).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox2.SuspendLayout();
            gbMagGyro.SuspendLayout();
            SuspendLayout();
            // 
            // bWipeAll
            // 
            bWipeAll.BackColor = System.Drawing.SystemColors.InactiveCaption;
            bWipeAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            bWipeAll.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bWipeAll.ForeColor = Color.Black;
            bWipeAll.Location = new System.Drawing.Point(17, 627);
            bWipeAll.Margin = new Padding(2, 3, 2, 3);
            bWipeAll.Name = "bWipeAll";
            bWipeAll.Size = new System.Drawing.Size(173, 23);
            bWipeAll.TabIndex = 9;
            bWipeAll.Text = "Restore Factory Defaults";
            bWipeAll.UseVisualStyleBackColor = true;
            bWipeAll.Click += bWipeAll_Click;
            // 
            // cbSketches
            // 
            cbSketches.BackColor = System.Drawing.SystemColors.Window;
            cbSketches.DisplayMember = "0";
            cbSketches.DropDownStyle = ComboBoxStyle.DropDownList;
            cbSketches.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            cbSketches.Items.AddRange(new object[] { "Select Sketch..." });
            cbSketches.Location = new System.Drawing.Point(6, 19);
            cbSketches.Name = "cbSketches";
            cbSketches.Size = new System.Drawing.Size(271, 23);
            cbSketches.TabIndex = 53;
            cbSketches.SelectedIndexChanged += cbSketches_SelectedIndexChanged;
            // 
            // cbPort
            // 
            cbPort.DropDownStyle = ComboBoxStyle.DropDownList;
            cbPort.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            cbPort.Items.AddRange(new object[] { "Retrieving COM Ports..." });
            cbPort.Location = new System.Drawing.Point(6, 47);
            cbPort.Name = "cbPort";
            cbPort.Size = new System.Drawing.Size(271, 23);
            cbPort.TabIndex = 55;
            // 
            // bFlash
            // 
            bFlash.BackColor = System.Drawing.SystemColors.ButtonFace;
            bFlash.Enabled = false;
            bFlash.FlatAppearance.BorderColor = Color.MidnightBlue;
            bFlash.FlatAppearance.BorderSize = 2;
            bFlash.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bFlash.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            bFlash.Location = new System.Drawing.Point(6, 75);
            bFlash.Margin = new Padding(2, 3, 2, 3);
            bFlash.Name = "bFlash";
            bFlash.Size = new System.Drawing.Size(172, 23);
            bFlash.TabIndex = 57;
            bFlash.Text = "Flash";
            bFlash.UseVisualStyleBackColor = true;
            bFlash.Click += bFlash_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(bScanPorts);
            groupBox1.Controls.Add(cbSketches);
            groupBox1.Controls.Add(bFlash);
            groupBox1.Controls.Add(cbPort);
            groupBox1.Location = new System.Drawing.Point(3, 27);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(292, 107);
            groupBox1.TabIndex = 58;
            groupBox1.TabStop = false;
            groupBox1.Text = "Program EDTracker";
            // 
            // bScanPorts
            // 
            bScanPorts.BackColor = System.Drawing.SystemColors.ButtonFace;
            bScanPorts.FlatAppearance.BorderColor = Color.MidnightBlue;
            bScanPorts.FlatAppearance.BorderSize = 2;
            bScanPorts.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bScanPorts.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            bScanPorts.Location = new System.Drawing.Point(185, 75);
            bScanPorts.Margin = new Padding(2, 3, 2, 3);
            bScanPorts.Name = "bScanPorts";
            bScanPorts.Size = new System.Drawing.Size(92, 23);
            bScanPorts.TabIndex = 58;
            bScanPorts.Text = "Scan Ports";
            bScanPorts.UseVisualStyleBackColor = true;
            bScanPorts.Click += bScanPorts_Click;
            // 
            // bMonitor
            // 
            bMonitor.BackColor = System.Drawing.SystemColors.ButtonFace;
            bMonitor.BackgroundImageLayout = ImageLayout.None;
            bMonitor.FlatAppearance.BorderColor = Color.MidnightBlue;
            bMonitor.FlatAppearance.BorderSize = 2;
            bMonitor.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bMonitor.Font = new Font("Arial", 11F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            bMonitor.Location = new System.Drawing.Point(309, 65);
            bMonitor.Margin = new Padding(2, 3, 2, 3);
            bMonitor.Name = "bMonitor";
            bMonitor.Size = new System.Drawing.Size(315, 31);
            bMonitor.TabIndex = 60;
            bMonitor.Text = "Connect to Tracker";
            bMonitor.UseVisualStyleBackColor = true;
            bMonitor.Click += bMonitor_Click;
            // 
            // gbBias
            // 
            gbBias.Controls.Add(biasAX);
            gbBias.Controls.Add(axminus);
            gbBias.Controls.Add(axplus);
            gbBias.Controls.Add(udAX);
            gbBias.Controls.Add(label5);
            gbBias.Controls.Add(biasAY);
            gbBias.Controls.Add(ayminus);
            gbBias.Controls.Add(ayplus);
            gbBias.Controls.Add(udAY);
            gbBias.Controls.Add(label6);
            gbBias.Controls.Add(biasAZ);
            gbBias.Controls.Add(azminus);
            gbBias.Controls.Add(azplus);
            gbBias.Controls.Add(udAZ);
            gbBias.Controls.Add(label7);
            gbBias.Controls.Add(biasGZ);
            gbBias.Controls.Add(gzminus);
            gbBias.Controls.Add(gzplus);
            gbBias.Controls.Add(udGZ);
            gbBias.Controls.Add(label4);
            gbBias.Controls.Add(biasGY);
            gbBias.Controls.Add(gyminus);
            gbBias.Controls.Add(gyplus);
            gbBias.Controls.Add(udGY);
            gbBias.Controls.Add(label3);
            gbBias.Controls.Add(biasGX);
            gbBias.Controls.Add(gxminus);
            gbBias.Controls.Add(udGX);
            gbBias.Controls.Add(gxplus);
            gbBias.Controls.Add(label2);
            gbBias.Controls.Add(bCalcBias);
            gbBias.Location = new System.Drawing.Point(3, 229);
            gbBias.Name = "gbBias";
            gbBias.Size = new System.Drawing.Size(218, 210);
            gbBias.TabIndex = 88;
            gbBias.TabStop = false;
            gbBias.Text = "Bias Value";
            // 
            // biasAX
            // 
            biasAX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            biasAX.Location = new System.Drawing.Point(68, 100);
            biasAX.Name = "biasAX";
            biasAX.ReadOnly = true;
            biasAX.Size = new System.Drawing.Size(39, 21);
            biasAX.TabIndex = 140;
            biasAX.Text = "0";
            biasAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // axminus
            // 
            axminus.BackColor = Color.Azure;
            axminus.FlatAppearance.BorderColor = Color.MidnightBlue;
            axminus.FlatAppearance.BorderSize = 2;
            axminus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            axminus.FlatStyle = FlatStyle.Flat;
            axminus.Location = new System.Drawing.Point(136, 99);
            axminus.Margin = new Padding(2, 3, 2, 3);
            axminus.Name = "axminus";
            axminus.Size = new System.Drawing.Size(20, 23);
            axminus.TabIndex = 139;
            axminus.Text = "-";
            axminus.UseVisualStyleBackColor = false;
            axminus.Click += axminus_Click_1;
            // 
            // axplus
            // 
            axplus.BackColor = Color.Azure;
            axplus.FlatAppearance.BorderColor = Color.MidnightBlue;
            axplus.FlatAppearance.BorderSize = 2;
            axplus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            axplus.FlatStyle = FlatStyle.Flat;
            axplus.Location = new System.Drawing.Point(112, 99);
            axplus.Margin = new Padding(2, 3, 2, 3);
            axplus.Name = "axplus";
            axplus.Size = new System.Drawing.Size(20, 23);
            axplus.TabIndex = 138;
            axplus.Text = "+";
            axplus.TextAlign = ContentAlignment.TopCenter;
            axplus.UseVisualStyleBackColor = false;
            axplus.Click += axplus_Click_1;
            // 
            // udAX
            // 
            udAX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            udAX.Location = new System.Drawing.Point(160, 100);
            udAX.Name = "udAX";
            udAX.ReadOnly = true;
            udAX.Size = new System.Drawing.Size(39, 21);
            udAX.TabIndex = 137;
            udAX.Text = "0";
            udAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.GhostWhite;
            label5.Location = new System.Drawing.Point(17, 103);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(46, 15);
            label5.TabIndex = 136;
            label5.Text = "Accel X";
            // 
            // biasAY
            // 
            biasAY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            biasAY.Location = new System.Drawing.Point(68, 128);
            biasAY.Name = "biasAY";
            biasAY.ReadOnly = true;
            biasAY.Size = new System.Drawing.Size(39, 21);
            biasAY.TabIndex = 135;
            biasAY.Text = "0";
            biasAY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ayminus
            // 
            ayminus.BackColor = Color.Azure;
            ayminus.FlatAppearance.BorderColor = Color.MidnightBlue;
            ayminus.FlatAppearance.BorderSize = 2;
            ayminus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            ayminus.FlatStyle = FlatStyle.Flat;
            ayminus.Location = new System.Drawing.Point(136, 127);
            ayminus.Margin = new Padding(2, 3, 2, 3);
            ayminus.Name = "ayminus";
            ayminus.Size = new System.Drawing.Size(20, 23);
            ayminus.TabIndex = 134;
            ayminus.Text = "-";
            ayminus.UseVisualStyleBackColor = false;
            ayminus.Click += ayminus_Click_1;
            // 
            // ayplus
            // 
            ayplus.BackColor = Color.Azure;
            ayplus.FlatAppearance.BorderColor = Color.MidnightBlue;
            ayplus.FlatAppearance.BorderSize = 2;
            ayplus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            ayplus.FlatStyle = FlatStyle.Flat;
            ayplus.Location = new System.Drawing.Point(112, 127);
            ayplus.Margin = new Padding(2, 3, 2, 3);
            ayplus.Name = "ayplus";
            ayplus.Size = new System.Drawing.Size(20, 23);
            ayplus.TabIndex = 133;
            ayplus.Text = "+";
            ayplus.TextAlign = ContentAlignment.TopCenter;
            ayplus.UseVisualStyleBackColor = false;
            ayplus.Click += ayplus_Click_1;
            // 
            // udAY
            // 
            udAY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            udAY.Location = new System.Drawing.Point(160, 128);
            udAY.Name = "udAY";
            udAY.ReadOnly = true;
            udAY.Size = new System.Drawing.Size(39, 21);
            udAY.TabIndex = 132;
            udAY.Text = "0";
            udAY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.GhostWhite;
            label6.Location = new System.Drawing.Point(17, 131);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(46, 15);
            label6.TabIndex = 131;
            label6.Text = "Accel Y";
            // 
            // biasAZ
            // 
            biasAZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            biasAZ.Location = new System.Drawing.Point(68, 156);
            biasAZ.Name = "biasAZ";
            biasAZ.ReadOnly = true;
            biasAZ.Size = new System.Drawing.Size(39, 21);
            biasAZ.TabIndex = 130;
            biasAZ.Text = "0";
            biasAZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // azminus
            // 
            azminus.BackColor = Color.Azure;
            azminus.FlatAppearance.BorderColor = Color.MidnightBlue;
            azminus.FlatAppearance.BorderSize = 2;
            azminus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            azminus.FlatStyle = FlatStyle.Flat;
            azminus.Location = new System.Drawing.Point(136, 155);
            azminus.Margin = new Padding(2, 3, 2, 3);
            azminus.Name = "azminus";
            azminus.Size = new System.Drawing.Size(20, 23);
            azminus.TabIndex = 129;
            azminus.Text = "-";
            azminus.UseVisualStyleBackColor = false;
            azminus.Click += azminus_Click_1;
            // 
            // azplus
            // 
            azplus.BackColor = Color.Azure;
            azplus.FlatAppearance.BorderColor = Color.MidnightBlue;
            azplus.FlatAppearance.BorderSize = 2;
            azplus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            azplus.FlatStyle = FlatStyle.Flat;
            azplus.Location = new System.Drawing.Point(112, 155);
            azplus.Margin = new Padding(2, 3, 2, 3);
            azplus.Name = "azplus";
            azplus.Size = new System.Drawing.Size(20, 23);
            azplus.TabIndex = 128;
            azplus.Text = "+";
            azplus.TextAlign = ContentAlignment.TopCenter;
            azplus.UseVisualStyleBackColor = false;
            azplus.Click += azplus_Click_1;
            // 
            // udAZ
            // 
            udAZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            udAZ.Location = new System.Drawing.Point(160, 156);
            udAZ.Name = "udAZ";
            udAZ.ReadOnly = true;
            udAZ.Size = new System.Drawing.Size(39, 21);
            udAZ.TabIndex = 127;
            udAZ.Text = "0";
            udAZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.GhostWhite;
            label7.Location = new System.Drawing.Point(17, 159);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(46, 15);
            label7.TabIndex = 126;
            label7.Text = "Accel Z";
            // 
            // biasGZ
            // 
            biasGZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            biasGZ.Location = new System.Drawing.Point(68, 72);
            biasGZ.Name = "biasGZ";
            biasGZ.ReadOnly = true;
            biasGZ.Size = new System.Drawing.Size(39, 21);
            biasGZ.TabIndex = 115;
            biasGZ.Text = "0";
            biasGZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gzminus
            // 
            gzminus.BackColor = Color.Azure;
            gzminus.FlatAppearance.BorderColor = Color.MidnightBlue;
            gzminus.FlatAppearance.BorderSize = 2;
            gzminus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            gzminus.FlatStyle = FlatStyle.Flat;
            gzminus.Location = new System.Drawing.Point(136, 71);
            gzminus.Margin = new Padding(2, 3, 2, 3);
            gzminus.Name = "gzminus";
            gzminus.Size = new System.Drawing.Size(20, 23);
            gzminus.TabIndex = 114;
            gzminus.Text = "-";
            gzminus.UseVisualStyleBackColor = false;
            gzminus.Click += gzminus_Click_1;
            // 
            // gzplus
            // 
            gzplus.BackColor = Color.Azure;
            gzplus.FlatAppearance.BorderColor = Color.MidnightBlue;
            gzplus.FlatAppearance.BorderSize = 2;
            gzplus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            gzplus.FlatStyle = FlatStyle.Flat;
            gzplus.Location = new System.Drawing.Point(112, 71);
            gzplus.Margin = new Padding(2, 3, 2, 3);
            gzplus.Name = "gzplus";
            gzplus.Size = new System.Drawing.Size(20, 23);
            gzplus.TabIndex = 113;
            gzplus.Text = "+";
            gzplus.TextAlign = ContentAlignment.TopCenter;
            gzplus.UseVisualStyleBackColor = false;
            gzplus.Click += gzplus_Click_1;
            // 
            // udGZ
            // 
            udGZ.BackColor = Color.LightSkyBlue;
            udGZ.Location = new System.Drawing.Point(160, 72);
            udGZ.Name = "udGZ";
            udGZ.ReadOnly = true;
            udGZ.Size = new System.Drawing.Size(39, 21);
            udGZ.TabIndex = 112;
            udGZ.Text = "0";
            udGZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Gold;
            label4.Location = new System.Drawing.Point(17, 75);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(42, 15);
            label4.TabIndex = 111;
            label4.Text = "Gyro Z";
            // 
            // biasGY
            // 
            biasGY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            biasGY.Location = new System.Drawing.Point(68, 44);
            biasGY.Name = "biasGY";
            biasGY.ReadOnly = true;
            biasGY.Size = new System.Drawing.Size(39, 21);
            biasGY.TabIndex = 110;
            biasGY.Text = "0";
            biasGY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gyminus
            // 
            gyminus.BackColor = Color.Azure;
            gyminus.FlatAppearance.BorderColor = Color.MidnightBlue;
            gyminus.FlatAppearance.BorderSize = 2;
            gyminus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            gyminus.FlatStyle = FlatStyle.Flat;
            gyminus.Location = new System.Drawing.Point(136, 43);
            gyminus.Margin = new Padding(2, 3, 2, 3);
            gyminus.Name = "gyminus";
            gyminus.Size = new System.Drawing.Size(20, 23);
            gyminus.TabIndex = 109;
            gyminus.Text = "-";
            gyminus.UseVisualStyleBackColor = false;
            gyminus.Click += gyminus_Click_1;
            // 
            // gyplus
            // 
            gyplus.BackColor = Color.Azure;
            gyplus.FlatAppearance.BorderColor = Color.MidnightBlue;
            gyplus.FlatAppearance.BorderSize = 2;
            gyplus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            gyplus.FlatStyle = FlatStyle.Flat;
            gyplus.Location = new System.Drawing.Point(112, 43);
            gyplus.Margin = new Padding(2, 3, 2, 3);
            gyplus.Name = "gyplus";
            gyplus.Size = new System.Drawing.Size(20, 23);
            gyplus.TabIndex = 108;
            gyplus.Text = "+";
            gyplus.TextAlign = ContentAlignment.TopCenter;
            gyplus.UseVisualStyleBackColor = false;
            gyplus.Click += gyplus_Click_1;
            // 
            // udGY
            // 
            udGY.BackColor = Color.MediumSpringGreen;
            udGY.Location = new System.Drawing.Point(160, 44);
            udGY.Name = "udGY";
            udGY.ReadOnly = true;
            udGY.Size = new System.Drawing.Size(39, 21);
            udGY.TabIndex = 107;
            udGY.Text = "0";
            udGY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Gold;
            label3.Location = new System.Drawing.Point(17, 47);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(42, 15);
            label3.TabIndex = 106;
            label3.Text = "Gyro Y";
            // 
            // biasGX
            // 
            biasGX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            biasGX.Location = new System.Drawing.Point(68, 16);
            biasGX.Name = "biasGX";
            biasGX.ReadOnly = true;
            biasGX.Size = new System.Drawing.Size(39, 21);
            biasGX.TabIndex = 105;
            biasGX.Text = "0";
            biasGX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gxminus
            // 
            gxminus.BackColor = Color.Azure;
            gxminus.FlatAppearance.BorderColor = Color.MidnightBlue;
            gxminus.FlatAppearance.BorderSize = 2;
            gxminus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            gxminus.FlatStyle = FlatStyle.Flat;
            gxminus.Location = new System.Drawing.Point(136, 15);
            gxminus.Margin = new Padding(2, 3, 2, 3);
            gxminus.Name = "gxminus";
            gxminus.Size = new System.Drawing.Size(20, 23);
            gxminus.TabIndex = 104;
            gxminus.Text = "-";
            gxminus.UseVisualStyleBackColor = false;
            gxminus.Click += gxminus_Click_1;
            // 
            // udGX
            // 
            udGX.BackColor = Color.LemonChiffon;
            udGX.Location = new System.Drawing.Point(160, 16);
            udGX.Name = "udGX";
            udGX.ReadOnly = true;
            udGX.Size = new System.Drawing.Size(39, 21);
            udGX.TabIndex = 102;
            udGX.Text = "0";
            udGX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // gxplus
            // 
            gxplus.BackColor = Color.Azure;
            gxplus.FlatAppearance.BorderColor = Color.MidnightBlue;
            gxplus.FlatAppearance.BorderSize = 2;
            gxplus.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            gxplus.FlatStyle = FlatStyle.Flat;
            gxplus.Location = new System.Drawing.Point(112, 15);
            gxplus.Margin = new Padding(2, 3, 2, 3);
            gxplus.Name = "gxplus";
            gxplus.Size = new System.Drawing.Size(20, 23);
            gxplus.TabIndex = 103;
            gxplus.Text = "+";
            gxplus.TextAlign = ContentAlignment.TopCenter;
            gxplus.UseVisualStyleBackColor = false;
            gxplus.Click += gxplus_Click_1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Gold;
            label2.Location = new System.Drawing.Point(17, 19);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(42, 15);
            label2.TabIndex = 101;
            label2.Text = "Gyro X";
            // 
            // bCalcBias
            // 
            bCalcBias.BackColor = System.Drawing.SystemColors.ButtonFace;
            bCalcBias.CausesValidation = false;
            bCalcBias.FlatAppearance.BorderColor = Color.MidnightBlue;
            bCalcBias.FlatAppearance.BorderSize = 2;
            bCalcBias.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bCalcBias.Location = new System.Drawing.Point(46, 180);
            bCalcBias.Margin = new Padding(2, 3, 2, 3);
            bCalcBias.Name = "bCalcBias";
            bCalcBias.Size = new System.Drawing.Size(153, 23);
            bCalcBias.TabIndex = 11;
            bCalcBias.Text = "Calculate Bias Values";
            bCalcBias.UseVisualStyleBackColor = true;
            bCalcBias.Click += bCalcBias_Click;
            // 
            // gbHotKey
            // 
            gbHotKey.Controls.Add(btEnableAC);
            gbHotKey.Controls.Add(tbAutoCentre);
            gbHotKey.Controls.Add(btToggleAutoCentre);
            gbHotKey.Controls.Add(cbHKey);
            gbHotKey.Controls.Add(cbCOntrollerButtons);
            gbHotKey.Controls.Add(cbControllerButton);
            gbHotKey.Controls.Add(cbHotKey);
            gbHotKey.Location = new System.Drawing.Point(3, 396);
            gbHotKey.Name = "gbHotKey";
            gbHotKey.Size = new System.Drawing.Size(292, 76);
            gbHotKey.TabIndex = 143;
            gbHotKey.TabStop = false;
            gbHotKey.Text = "Recentre";
            // 
            // btEnableAC
            // 
            btEnableAC.Enabled = false;
            btEnableAC.Location = new System.Drawing.Point(218, 44);
            btEnableAC.Name = "btEnableAC";
            btEnableAC.Size = new System.Drawing.Size(59, 23);
            btEnableAC.TabIndex = 152;
            btEnableAC.Text = "Enable";
            btEnableAC.UseVisualStyleBackColor = true;
            btEnableAC.Visible = false;
            btEnableAC.Click += btEnableAC_Click;
            // 
            // tbAutoCentre
            // 
            tbAutoCentre.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbAutoCentre.BorderStyle = BorderStyle.None;
            tbAutoCentre.Font = new Font("Arial", 11F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbAutoCentre.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            tbAutoCentre.Location = new System.Drawing.Point(135, 46);
            tbAutoCentre.Multiline = true;
            tbAutoCentre.Name = "tbAutoCentre";
            tbAutoCentre.ReadOnly = true;
            tbAutoCentre.Size = new System.Drawing.Size(74, 20);
            tbAutoCentre.TabIndex = 157;
            tbAutoCentre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btToggleAutoCentre
            // 
            btToggleAutoCentre.BackColor = System.Drawing.SystemColors.ButtonFace;
            btToggleAutoCentre.FlatAppearance.BorderColor = Color.MidnightBlue;
            btToggleAutoCentre.FlatAppearance.BorderSize = 2;
            btToggleAutoCentre.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btToggleAutoCentre.Location = new System.Drawing.Point(12, 44);
            btToggleAutoCentre.Margin = new Padding(2, 3, 2, 3);
            btToggleAutoCentre.Name = "btToggleAutoCentre";
            btToggleAutoCentre.Size = new System.Drawing.Size(118, 23);
            btToggleAutoCentre.TabIndex = 156;
            btToggleAutoCentre.Text = "Toggle Auto-centre";
            btToggleAutoCentre.UseVisualStyleBackColor = true;
            btToggleAutoCentre.Click += btToggleAutoCentre_Click;
            // 
            // cbHKey
            // 
            cbHKey.FormattingEnabled = true;
            cbHKey.Items.AddRange(new object[] { "Button 1", "Button 2", "Button 3", "Button 4" });
            cbHKey.Location = new System.Drawing.Point(135, 19);
            cbHKey.Name = "cbHKey";
            cbHKey.Size = new System.Drawing.Size(74, 23);
            cbHKey.TabIndex = 41;
            cbHKey.SelectedIndexChanged += cbHKey_SelectedIndexChanged;
            // 
            // cbCOntrollerButtons
            // 
            cbCOntrollerButtons.FormattingEnabled = true;
            cbCOntrollerButtons.Items.AddRange(new object[] { "Button 1", "Button 2", "Button 3", "Button 4" });
            cbCOntrollerButtons.Location = new System.Drawing.Point(153, 44);
            cbCOntrollerButtons.Name = "cbCOntrollerButtons";
            cbCOntrollerButtons.Size = new System.Drawing.Size(121, 23);
            cbCOntrollerButtons.TabIndex = 40;
            cbCOntrollerButtons.Visible = false;
            // 
            // cbControllerButton
            // 
            cbControllerButton.AutoSize = true;
            cbControllerButton.Location = new System.Drawing.Point(6, 46);
            cbControllerButton.Name = "cbControllerButton";
            cbControllerButton.RightToLeft = RightToLeft.Yes;
            cbControllerButton.Size = new System.Drawing.Size(160, 19);
            cbControllerButton.TabIndex = 35;
            cbControllerButton.Text = "Enable Controller Button";
            cbControllerButton.UseVisualStyleBackColor = true;
            cbControllerButton.Visible = false;
            // 
            // cbHotKey
            // 
            cbHotKey.AutoSize = true;
            cbHotKey.Location = new System.Drawing.Point(12, 21);
            cbHotKey.Name = "cbHotKey";
            cbHotKey.RightToLeft = RightToLeft.Yes;
            cbHotKey.Size = new System.Drawing.Size(105, 19);
            cbHotKey.TabIndex = 34;
            cbHotKey.Text = "Enable Hotkey";
            cbHotKey.TextAlign = ContentAlignment.MiddleRight;
            cbHotKey.UseVisualStyleBackColor = true;
            cbHotKey.CheckedChanged += cbHotKey_CheckedChanged;
            // 
            // gbTrackerConfig
            // 
            gbTrackerConfig.Controls.Add(bOrientate);
            gbTrackerConfig.Controls.Add(tbOrientation);
            gbTrackerConfig.Controls.Add(bSensorMode);
            gbTrackerConfig.Controls.Add(tbSensorMode);
            gbTrackerConfig.Location = new System.Drawing.Point(3, 139);
            gbTrackerConfig.Name = "gbTrackerConfig";
            gbTrackerConfig.Size = new System.Drawing.Size(292, 83);
            gbTrackerConfig.TabIndex = 89;
            gbTrackerConfig.TabStop = false;
            gbTrackerConfig.Text = "Tracker Config";
            // 
            // bOrientate
            // 
            bOrientate.BackColor = System.Drawing.SystemColors.ButtonFace;
            bOrientate.Enabled = false;
            bOrientate.FlatAppearance.BorderColor = Color.MidnightBlue;
            bOrientate.FlatAppearance.BorderSize = 2;
            bOrientate.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bOrientate.Location = new System.Drawing.Point(6, 19);
            bOrientate.Margin = new Padding(2, 3, 2, 3);
            bOrientate.Name = "bOrientate";
            bOrientate.Size = new System.Drawing.Size(149, 23);
            bOrientate.TabIndex = 53;
            bOrientate.Text = "Rotate Mounting Axis";
            bOrientate.UseVisualStyleBackColor = true;
            bOrientate.Click += bOrientate_Click;
            // 
            // tbOrientation
            // 
            tbOrientation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbOrientation.BorderStyle = BorderStyle.None;
            tbOrientation.Font = new Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbOrientation.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            tbOrientation.Location = new System.Drawing.Point(164, 21);
            tbOrientation.Multiline = true;
            tbOrientation.Name = "tbOrientation";
            tbOrientation.ReadOnly = true;
            tbOrientation.Size = new System.Drawing.Size(113, 20);
            tbOrientation.TabIndex = 56;
            tbOrientation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            tbOrientation.TextChanged += tbOrientation_TextChanged;
            // 
            // bSensorMode
            // 
            bSensorMode.BackColor = System.Drawing.SystemColors.ButtonFace;
            bSensorMode.Enabled = false;
            bSensorMode.FlatAppearance.BorderColor = Color.MidnightBlue;
            bSensorMode.FlatAppearance.BorderSize = 2;
            bSensorMode.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bSensorMode.Location = new System.Drawing.Point(6, 53);
            bSensorMode.Margin = new Padding(2, 3, 2, 3);
            bSensorMode.Name = "bSensorMode";
            bSensorMode.Size = new System.Drawing.Size(149, 23);
            bSensorMode.TabIndex = 55;
            bSensorMode.Text = "Toggle Sensor Mode";
            bSensorMode.UseVisualStyleBackColor = true;
            bSensorMode.Click += bSensorMode_Click;
            // 
            // tbSensorMode
            // 
            tbSensorMode.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbSensorMode.BorderStyle = BorderStyle.None;
            tbSensorMode.Font = new Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbSensorMode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            tbSensorMode.Location = new System.Drawing.Point(164, 55);
            tbSensorMode.Multiline = true;
            tbSensorMode.Name = "tbSensorMode";
            tbSensorMode.ReadOnly = true;
            tbSensorMode.Size = new System.Drawing.Size(113, 20);
            tbSensorMode.TabIndex = 57;
            tbSensorMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbSketch
            // 
            tbSketch.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            tbSketch.CausesValidation = false;
            tbSketch.Font = new Font("Arial", 16F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbSketch.Location = new System.Drawing.Point(309, 31);
            tbSketch.Name = "tbSketch";
            tbSketch.ReadOnly = true;
            tbSketch.Size = new System.Drawing.Size(314, 32);
            tbSketch.TabIndex = 140;
            tbSketch.Text = "EDTracker Sketch";
            tbSketch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbDriftComp
            // 
            gbDriftComp.Controls.Add(lbTimer);
            gbDriftComp.Controls.Add(tbTime);
            gbDriftComp.Controls.Add(bSaveDriftComp);
            gbDriftComp.Controls.Add(bResetView);
            gbDriftComp.Controls.Add(tbDriftComp);
            gbDriftComp.Controls.Add(lbComp);
            gbDriftComp.Controls.Add(tbYawDrift);
            gbDriftComp.Controls.Add(lbYawDrift);
            gbDriftComp.Controls.Add(btCompDown);
            gbDriftComp.Controls.Add(btCompUp);
            gbDriftComp.Location = new System.Drawing.Point(3, 474);
            gbDriftComp.Name = "gbDriftComp";
            gbDriftComp.Size = new System.Drawing.Size(292, 134);
            gbDriftComp.TabIndex = 141;
            gbDriftComp.TabStop = false;
            gbDriftComp.Text = "Drift Compensation";
            // 
            // lbTimer
            // 
            lbTimer.AutoSize = true;
            lbTimer.Location = new System.Drawing.Point(143, 18);
            lbTimer.Name = "lbTimer";
            lbTimer.Size = new System.Drawing.Size(39, 15);
            lbTimer.TabIndex = 154;
            lbTimer.Text = "Timer";
            lbTimer.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbTime
            // 
            tbTime.BackColor = Color.LemonChiffon;
            tbTime.Font = new Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbTime.Location = new System.Drawing.Point(180, 15);
            tbTime.Name = "tbTime";
            tbTime.ReadOnly = true;
            tbTime.Size = new System.Drawing.Size(44, 20);
            tbTime.TabIndex = 153;
            tbTime.Text = "00:00";
            tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bSaveDriftComp
            // 
            bSaveDriftComp.BackColor = System.Drawing.SystemColors.ButtonFace;
            bSaveDriftComp.FlatAppearance.BorderColor = Color.MidnightBlue;
            bSaveDriftComp.FlatAppearance.BorderSize = 2;
            bSaveDriftComp.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bSaveDriftComp.Location = new System.Drawing.Point(14, 99);
            bSaveDriftComp.Margin = new Padding(2, 3, 2, 3);
            bSaveDriftComp.Name = "bSaveDriftComp";
            bSaveDriftComp.Size = new System.Drawing.Size(173, 23);
            bSaveDriftComp.TabIndex = 145;
            bSaveDriftComp.Text = "Save Drift Compensation";
            bSaveDriftComp.UseVisualStyleBackColor = true;
            bSaveDriftComp.Click += bSaveDriftComp_Click;
            // 
            // bResetView
            // 
            bResetView.BackColor = System.Drawing.SystemColors.ButtonFace;
            bResetView.BackgroundImageLayout = ImageLayout.None;
            bResetView.FlatAppearance.BorderColor = Color.MidnightBlue;
            bResetView.FlatAppearance.BorderSize = 2;
            bResetView.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bResetView.Location = new System.Drawing.Point(14, 71);
            bResetView.Margin = new Padding(2, 3, 2, 3);
            bResetView.Name = "bResetView";
            bResetView.Size = new System.Drawing.Size(173, 23);
            bResetView.TabIndex = 144;
            bResetView.Text = "Reset View / Drift Tracking";
            bResetView.UseVisualStyleBackColor = true;
            bResetView.Click += bResetView_Click;
            // 
            // tbDriftComp
            // 
            tbDriftComp.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbDriftComp.Location = new System.Drawing.Point(94, 39);
            tbDriftComp.Name = "tbDriftComp";
            tbDriftComp.ReadOnly = true;
            tbDriftComp.Size = new System.Drawing.Size(39, 21);
            tbDriftComp.TabIndex = 141;
            tbDriftComp.Text = "0";
            tbDriftComp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbComp
            // 
            lbComp.AutoSize = true;
            lbComp.Location = new System.Drawing.Point(6, 44);
            lbComp.Name = "lbComp";
            lbComp.Size = new System.Drawing.Size(89, 15);
            lbComp.TabIndex = 140;
            lbComp.Text = "Compensation";
            lbComp.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbYawDrift
            // 
            tbYawDrift.BackColor = Color.LemonChiffon;
            tbYawDrift.Location = new System.Drawing.Point(93, 15);
            tbYawDrift.Name = "tbYawDrift";
            tbYawDrift.ReadOnly = true;
            tbYawDrift.Size = new System.Drawing.Size(39, 21);
            tbYawDrift.TabIndex = 139;
            tbYawDrift.Text = "0";
            tbYawDrift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbYawDrift
            // 
            lbYawDrift.AutoSize = true;
            lbYawDrift.Location = new System.Drawing.Point(36, 18);
            lbYawDrift.Name = "lbYawDrift";
            lbYawDrift.Size = new System.Drawing.Size(54, 15);
            lbYawDrift.TabIndex = 138;
            lbYawDrift.Text = "Yaw Drift";
            lbYawDrift.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btCompDown
            // 
            btCompDown.BackColor = Color.Azure;
            btCompDown.FlatAppearance.BorderColor = Color.MidnightBlue;
            btCompDown.FlatAppearance.BorderSize = 2;
            btCompDown.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btCompDown.FlatStyle = FlatStyle.Flat;
            btCompDown.Location = new System.Drawing.Point(168, 39);
            btCompDown.Margin = new Padding(2, 3, 2, 3);
            btCompDown.Name = "btCompDown";
            btCompDown.Size = new System.Drawing.Size(20, 23);
            btCompDown.TabIndex = 137;
            btCompDown.Text = "-";
            btCompDown.UseVisualStyleBackColor = false;
            btCompDown.Click += btCompDown_Click;
            // 
            // btCompUp
            // 
            btCompUp.BackColor = Color.Azure;
            btCompUp.FlatAppearance.BorderColor = Color.MidnightBlue;
            btCompUp.FlatAppearance.BorderSize = 2;
            btCompUp.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btCompUp.FlatStyle = FlatStyle.Flat;
            btCompUp.Location = new System.Drawing.Point(144, 39);
            btCompUp.Margin = new Padding(2, 3, 2, 3);
            btCompUp.Name = "btCompUp";
            btCompUp.Size = new System.Drawing.Size(20, 23);
            btCompUp.TabIndex = 136;
            btCompUp.Text = "+";
            btCompUp.UseVisualStyleBackColor = false;
            btCompUp.Click += btCompUp_Click;
            // 
            // gbScaling
            // 
            gbScaling.Controls.Add(tbSmoothing);
            gbScaling.Controls.Add(lbSmooth);
            gbScaling.Controls.Add(sliderSmoothing);
            gbScaling.Controls.Add(tbRespMode);
            gbScaling.Controls.Add(bRespMode);
            gbScaling.Controls.Add(tbPitchScaling);
            gbScaling.Controls.Add(label11);
            gbScaling.Controls.Add(tbYawScaling);
            gbScaling.Controls.Add(label10);
            gbScaling.Controls.Add(cbFineAdjust);
            gbScaling.Controls.Add(btYawScaleDown);
            gbScaling.Controls.Add(btPitchScaleDown);
            gbScaling.Controls.Add(btPitchScaleUp);
            gbScaling.Controls.Add(btYawScaleUp);
            gbScaling.Location = new System.Drawing.Point(3, 227);
            gbScaling.Name = "gbScaling";
            gbScaling.Size = new System.Drawing.Size(292, 165);
            gbScaling.TabIndex = 142;
            gbScaling.TabStop = false;
            gbScaling.Text = "Response Scaling";
            // 
            // tbSmoothing
            // 
            tbSmoothing.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbSmoothing.Location = new System.Drawing.Point(95, 78);
            tbSmoothing.Name = "tbSmoothing";
            tbSmoothing.ReadOnly = true;
            tbSmoothing.Size = new System.Drawing.Size(39, 21);
            tbSmoothing.TabIndex = 154;
            tbSmoothing.Text = "0";
            tbSmoothing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbSmooth
            // 
            lbSmooth.AutoSize = true;
            lbSmooth.Location = new System.Drawing.Point(9, 80);
            lbSmooth.Name = "lbSmooth";
            lbSmooth.Size = new System.Drawing.Size(67, 15);
            lbSmooth.TabIndex = 153;
            lbSmooth.Text = "Smoothing";
            lbSmooth.TextAlign = ContentAlignment.MiddleRight;
            // 
            // sliderSmoothing
            // 
            sliderSmoothing.AutoSize = false;
            sliderSmoothing.Location = new System.Drawing.Point(139, 78);
            sliderSmoothing.Maximum = 99;
            sliderSmoothing.Name = "sliderSmoothing";
            sliderSmoothing.Size = new System.Drawing.Size(138, 27);
            sliderSmoothing.TabIndex = 152;
            sliderSmoothing.Scroll += trackBar1_Scroll;
            sliderSmoothing.MouseUp += trackBar1_MUP;
            // 
            // tbRespMode
            // 
            tbRespMode.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbRespMode.BorderStyle = BorderStyle.None;
            tbRespMode.Font = new Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbRespMode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            tbRespMode.Location = new System.Drawing.Point(164, 127);
            tbRespMode.Multiline = true;
            tbRespMode.Name = "tbRespMode";
            tbRespMode.ReadOnly = true;
            tbRespMode.Size = new System.Drawing.Size(113, 20);
            tbRespMode.TabIndex = 150;
            tbRespMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bRespMode
            // 
            bRespMode.BackColor = System.Drawing.SystemColors.ButtonFace;
            bRespMode.FlatAppearance.BorderColor = Color.MidnightBlue;
            bRespMode.FlatAppearance.BorderSize = 2;
            bRespMode.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            bRespMode.Location = new System.Drawing.Point(6, 125);
            bRespMode.Margin = new Padding(2, 3, 2, 3);
            bRespMode.Name = "bRespMode";
            bRespMode.Size = new System.Drawing.Size(150, 23);
            bRespMode.TabIndex = 149;
            bRespMode.Text = "Toggle Response Mode";
            bRespMode.UseVisualStyleBackColor = true;
            bRespMode.Click += bRespMode_Click;
            // 
            // tbPitchScaling
            // 
            tbPitchScaling.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbPitchScaling.Location = new System.Drawing.Point(95, 52);
            tbPitchScaling.Name = "tbPitchScaling";
            tbPitchScaling.ReadOnly = true;
            tbPitchScaling.Size = new System.Drawing.Size(39, 21);
            tbPitchScaling.TabIndex = 148;
            tbPitchScaling.Text = "0";
            tbPitchScaling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(9, 55);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(78, 15);
            label11.TabIndex = 147;
            label11.Text = "Pitch Scaling";
            label11.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbYawScaling
            // 
            tbYawScaling.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbYawScaling.Location = new System.Drawing.Point(95, 24);
            tbYawScaling.Name = "tbYawScaling";
            tbYawScaling.ReadOnly = true;
            tbYawScaling.Size = new System.Drawing.Size(39, 21);
            tbYawScaling.TabIndex = 146;
            tbYawScaling.Text = "0";
            tbYawScaling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(9, 27);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(73, 15);
            label10.TabIndex = 145;
            label10.Text = "Yaw Scaling";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // cbFineAdjust
            // 
            cbFineAdjust.AutoSize = true;
            cbFineAdjust.Location = new System.Drawing.Point(188, 22);
            cbFineAdjust.Name = "cbFineAdjust";
            cbFineAdjust.Size = new System.Drawing.Size(86, 19);
            cbFineAdjust.TabIndex = 144;
            cbFineAdjust.Text = "Fine Adjust";
            cbFineAdjust.UseVisualStyleBackColor = true;
            // 
            // btYawScaleDown
            // 
            btYawScaleDown.BackColor = Color.Azure;
            btYawScaleDown.FlatAppearance.BorderColor = Color.MidnightBlue;
            btYawScaleDown.FlatAppearance.BorderSize = 2;
            btYawScaleDown.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btYawScaleDown.FlatStyle = FlatStyle.Flat;
            btYawScaleDown.Location = new System.Drawing.Point(163, 22);
            btYawScaleDown.Margin = new Padding(2, 3, 2, 3);
            btYawScaleDown.Name = "btYawScaleDown";
            btYawScaleDown.Size = new System.Drawing.Size(20, 23);
            btYawScaleDown.TabIndex = 143;
            btYawScaleDown.Text = "-";
            btYawScaleDown.UseVisualStyleBackColor = false;
            btYawScaleDown.Click += btYawScaleDown_Click;
            // 
            // btPitchScaleDown
            // 
            btPitchScaleDown.BackColor = Color.Azure;
            btPitchScaleDown.FlatAppearance.BorderColor = Color.MidnightBlue;
            btPitchScaleDown.FlatAppearance.BorderSize = 2;
            btPitchScaleDown.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btPitchScaleDown.FlatStyle = FlatStyle.Flat;
            btPitchScaleDown.Location = new System.Drawing.Point(163, 49);
            btPitchScaleDown.Margin = new Padding(2, 3, 2, 3);
            btPitchScaleDown.Name = "btPitchScaleDown";
            btPitchScaleDown.Size = new System.Drawing.Size(20, 23);
            btPitchScaleDown.TabIndex = 142;
            btPitchScaleDown.Text = "-";
            btPitchScaleDown.UseVisualStyleBackColor = false;
            btPitchScaleDown.Click += btPitchScaleDown_Click;
            // 
            // btPitchScaleUp
            // 
            btPitchScaleUp.BackColor = Color.Azure;
            btPitchScaleUp.FlatAppearance.BorderColor = Color.MidnightBlue;
            btPitchScaleUp.FlatAppearance.BorderSize = 2;
            btPitchScaleUp.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btPitchScaleUp.FlatStyle = FlatStyle.Flat;
            btPitchScaleUp.Location = new System.Drawing.Point(139, 49);
            btPitchScaleUp.Margin = new Padding(2, 3, 2, 3);
            btPitchScaleUp.Name = "btPitchScaleUp";
            btPitchScaleUp.Size = new System.Drawing.Size(20, 23);
            btPitchScaleUp.TabIndex = 141;
            btPitchScaleUp.Text = "+";
            btPitchScaleUp.UseVisualStyleBackColor = false;
            btPitchScaleUp.Click += btPitchScaleUp_Click;
            // 
            // btYawScaleUp
            // 
            btYawScaleUp.BackColor = Color.Azure;
            btYawScaleUp.FlatAppearance.BorderColor = Color.MidnightBlue;
            btYawScaleUp.FlatAppearance.BorderSize = 2;
            btYawScaleUp.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btYawScaleUp.FlatStyle = FlatStyle.Flat;
            btYawScaleUp.Location = new System.Drawing.Point(139, 22);
            btYawScaleUp.Margin = new Padding(2, 3, 2, 3);
            btYawScaleUp.Name = "btYawScaleUp";
            btYawScaleUp.Size = new System.Drawing.Size(20, 23);
            btYawScaleUp.TabIndex = 140;
            btYawScaleUp.Text = "+";
            btYawScaleUp.UseVisualStyleBackColor = false;
            btYawScaleUp.Click += btYawScaleUp_Click;
            // 
            // tbTemps
            // 
            tbTemps.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbTemps.Location = new System.Drawing.Point(267, 17);
            tbTemps.Name = "tbTemps";
            tbTemps.ReadOnly = true;
            tbTemps.Size = new System.Drawing.Size(39, 21);
            tbTemps.TabIndex = 145;
            tbTemps.Text = "0";
            tbTemps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbTemps
            // 
            lbTemps.AutoSize = true;
            lbTemps.BackColor = Color.PaleGreen;
            lbTemps.Location = new System.Drawing.Point(185, 20);
            lbTemps.Name = "lbTemps";
            lbTemps.Size = new System.Drawing.Size(77, 15);
            lbTemps.TabIndex = 144;
            lbTemps.Text = "Temperature";
            lbTemps.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbMonitor
            // 
            tbMonitor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMonitor.BorderStyle = BorderStyle.None;
            tbMonitor.CausesValidation = false;
            tbMonitor.Font = new Font("Arial", 11F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMonitor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            tbMonitor.Location = new System.Drawing.Point(18, 17);
            tbMonitor.Name = "tbMonitor";
            tbMonitor.ReadOnly = true;
            tbMonitor.Size = new System.Drawing.Size(136, 17);
            tbMonitor.TabIndex = 61;
            tbMonitor.Text = "OFF";
            tbMonitor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // notifyIcon1
            // 
            notifyIcon1.BalloonTipText = "EDTracker is minimised";
            notifyIcon1.BalloonTipTitle = "EDTracker UI";
            notifyIcon1.Text = "EDTracker";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // lbAxis4
            // 
            lbAxis4.BackColor = Color.Black;
            lbAxis4.ForeColor = Color.LightSkyBlue;
            lbAxis4.Location = new System.Drawing.Point(15, 484);
            lbAxis4.Name = "lbAxis4";
            lbAxis4.Size = new System.Drawing.Size(107, 14);
            lbAxis4.TabIndex = 149;
            lbAxis4.Text = "Axis 4";
            // 
            // lbAxis3
            // 
            lbAxis3.BackColor = Color.Black;
            lbAxis3.ForeColor = Color.LawnGreen;
            lbAxis3.Location = new System.Drawing.Point(15, 443);
            lbAxis3.Name = "lbAxis3";
            lbAxis3.Size = new System.Drawing.Size(107, 14);
            lbAxis3.TabIndex = 150;
            lbAxis3.Text = "Axis 3";
            // 
            // lbAxis2
            // 
            lbAxis2.BackColor = Color.Black;
            lbAxis2.ForeColor = Color.Yellow;
            lbAxis2.Location = new System.Drawing.Point(15, 404);
            lbAxis2.Name = "lbAxis2";
            lbAxis2.Size = new System.Drawing.Size(107, 14);
            lbAxis2.TabIndex = 148;
            lbAxis2.Text = "Axis 2";
            // 
            // lbAxis1
            // 
            lbAxis1.BackColor = Color.Black;
            lbAxis1.CausesValidation = false;
            lbAxis1.ForeColor = Color.Red;
            lbAxis1.Location = new System.Drawing.Point(15, 363);
            lbAxis1.Name = "lbAxis1";
            lbAxis1.Size = new System.Drawing.Size(107, 14);
            lbAxis1.TabIndex = 147;
            lbAxis1.Text = "Axis 1";
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, Help, debugToolStripMenuItem });
            menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(654, 24);
            menuStrip1.Stretch = false;
            menuStrip1.TabIndex = 151;
            menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.Alignment = ToolStripItemAlignment.Right;
            toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem, modeToolStripMenuItem });
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            toolStripMenuItem1.Text = "About";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // modeToolStripMenuItem
            // 
            modeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { uSERToolStripMenuItem, tESTToolStripMenuItem, dEVToolStripMenuItem });
            modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            modeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            modeToolStripMenuItem.Text = "Mode";
            // 
            // uSERToolStripMenuItem
            // 
            uSERToolStripMenuItem.Name = "uSERToolStripMenuItem";
            uSERToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            uSERToolStripMenuItem.Text = "USER";
            uSERToolStripMenuItem.Click += uSERToolStripMenuItem_Click;
            // 
            // tESTToolStripMenuItem
            // 
            tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            tESTToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            tESTToolStripMenuItem.Text = "TEST";
            tESTToolStripMenuItem.Click += tESTToolStripMenuItem_Click;
            // 
            // dEVToolStripMenuItem
            // 
            dEVToolStripMenuItem.Name = "dEVToolStripMenuItem";
            dEVToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            dEVToolStripMenuItem.Text = "DEV";
            dEVToolStripMenuItem.Click += dEVToolStripMenuItem_Click;
            // 
            // Help
            // 
            Help.Alignment = ToolStripItemAlignment.Right;
            Help.Name = "Help";
            Help.Size = new System.Drawing.Size(44, 20);
            Help.Text = "Help";
            Help.Click += Help_Click;
            // 
            // debugToolStripMenuItem
            // 
            debugToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            debugToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { showToolStripMenuItem, hideToolStripMenuItem });
            debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            debugToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            debugToolStripMenuItem.Text = "Log";
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            showToolStripMenuItem.Text = "Show";
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            // 
            // hideToolStripMenuItem
            // 
            hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            hideToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            hideToolStripMenuItem.Text = "Hide";
            hideToolStripMenuItem.Click += hideToolStripMenuItem_Click;
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // gbMagCalib
            // 
            gbMagCalib.Controls.Add(label1);
            gbMagCalib.Controls.Add(tbOffZ);
            gbMagCalib.Controls.Add(tbOffY);
            gbMagCalib.Controls.Add(tbOffX);
            gbMagCalib.Controls.Add(label18);
            gbMagCalib.Controls.Add(tbarMagSens);
            gbMagCalib.Controls.Add(button1);
            gbMagCalib.Controls.Add(label16);
            gbMagCalib.Controls.Add(tbMagSamples);
            gbMagCalib.Controls.Add(tbMinX);
            gbMagCalib.Controls.Add(label9);
            gbMagCalib.Controls.Add(label12);
            gbMagCalib.Controls.Add(label13);
            gbMagCalib.Controls.Add(label14);
            gbMagCalib.Controls.Add(label15);
            gbMagCalib.Controls.Add(tbMaxZ);
            gbMagCalib.Controls.Add(tbMinZ);
            gbMagCalib.Controls.Add(tbMaxY);
            gbMagCalib.Controls.Add(tbMinY);
            gbMagCalib.Controls.Add(tbMaxX);
            gbMagCalib.Location = new System.Drawing.Point(6, 315);
            gbMagCalib.Name = "gbMagCalib";
            gbMagCalib.Size = new System.Drawing.Size(174, 205);
            gbMagCalib.TabIndex = 152;
            gbMagCalib.TabStop = false;
            gbMagCalib.Text = "Calibration";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(121, 17);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(39, 15);
            label1.TabIndex = 171;
            label1.Text = "Offset";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbOffZ
            // 
            tbOffZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbOffZ.BorderStyle = BorderStyle.None;
            tbOffZ.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbOffZ.ForeColor = Color.Black;
            tbOffZ.Location = new System.Drawing.Point(122, 87);
            tbOffZ.Multiline = true;
            tbOffZ.Name = "tbOffZ";
            tbOffZ.ReadOnly = true;
            tbOffZ.Size = new System.Drawing.Size(44, 20);
            tbOffZ.TabIndex = 170;
            tbOffZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbOffY
            // 
            tbOffY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbOffY.BorderStyle = BorderStyle.None;
            tbOffY.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbOffY.ForeColor = Color.Black;
            tbOffY.Location = new System.Drawing.Point(122, 61);
            tbOffY.Multiline = true;
            tbOffY.Name = "tbOffY";
            tbOffY.ReadOnly = true;
            tbOffY.Size = new System.Drawing.Size(44, 20);
            tbOffY.TabIndex = 169;
            tbOffY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbOffX
            // 
            tbOffX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbOffX.BorderStyle = BorderStyle.None;
            tbOffX.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbOffX.ForeColor = Color.Black;
            tbOffX.Location = new System.Drawing.Point(122, 35);
            tbOffX.Multiline = true;
            tbOffX.Name = "tbOffX";
            tbOffX.ReadOnly = true;
            tbOffX.Size = new System.Drawing.Size(44, 20);
            tbOffX.TabIndex = 168;
            tbOffX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new System.Drawing.Point(4, 152);
            label18.Name = "label18";
            label18.Size = new System.Drawing.Size(61, 15);
            label18.TabIndex = 167;
            label18.Text = "Sensitivity";
            label18.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbarMagSens
            // 
            tbarMagSens.AutoSize = false;
            tbarMagSens.Location = new System.Drawing.Point(6, 173);
            tbarMagSens.Maximum = 20;
            tbarMagSens.Name = "tbarMagSens";
            tbarMagSens.Size = new System.Drawing.Size(162, 23);
            tbarMagSens.TabIndex = 166;
            tbarMagSens.TickStyle = TickStyle.None;
            tbarMagSens.Value = 15;
            tbarMagSens.Scroll += trackBar1_Scroll_1;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(98, 114);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(71, 23);
            button1.TabIndex = 165;
            button1.Text = "Restart";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new System.Drawing.Point(4, 116);
            label16.Name = "label16";
            label16.Size = new System.Drawing.Size(42, 15);
            label16.TabIndex = 164;
            label16.Text = "Points";
            label16.TextAlign = ContentAlignment.MiddleRight;
            label16.Click += label16_Click;
            // 
            // tbMagSamples
            // 
            tbMagSamples.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagSamples.BorderStyle = BorderStyle.None;
            tbMagSamples.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagSamples.ForeColor = Color.Black;
            tbMagSamples.Location = new System.Drawing.Point(47, 114);
            tbMagSamples.Multiline = true;
            tbMagSamples.Name = "tbMagSamples";
            tbMagSamples.ReadOnly = true;
            tbMagSamples.Size = new System.Drawing.Size(45, 20);
            tbMagSamples.TabIndex = 163;
            tbMagSamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMinX
            // 
            tbMinX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMinX.BorderStyle = BorderStyle.None;
            tbMinX.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMinX.ForeColor = Color.Black;
            tbMinX.Location = new System.Drawing.Point(21, 35);
            tbMinX.Multiline = true;
            tbMinX.Name = "tbMinX";
            tbMinX.ReadOnly = true;
            tbMinX.Size = new System.Drawing.Size(46, 20);
            tbMinX.TabIndex = 152;
            tbMinX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(73, 16);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(28, 15);
            label9.TabIndex = 162;
            label9.Text = "Max";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(26, 16);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(26, 15);
            label12.TabIndex = 161;
            label12.Text = "Min";
            label12.TextAlign = ContentAlignment.MiddleRight;
            label12.Click += label12_Click;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(4, 89);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(14, 15);
            label13.TabIndex = 160;
            label13.Text = "Z";
            label13.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new System.Drawing.Point(4, 63);
            label14.Name = "label14";
            label14.Size = new System.Drawing.Size(14, 15);
            label14.TabIndex = 159;
            label14.Text = "Y";
            label14.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new System.Drawing.Point(4, 37);
            label15.Name = "label15";
            label15.Size = new System.Drawing.Size(14, 15);
            label15.TabIndex = 158;
            label15.Text = "X";
            label15.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbMaxZ
            // 
            tbMaxZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMaxZ.BorderStyle = BorderStyle.None;
            tbMaxZ.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMaxZ.ForeColor = Color.Black;
            tbMaxZ.Location = new System.Drawing.Point(73, 87);
            tbMaxZ.Multiline = true;
            tbMaxZ.Name = "tbMaxZ";
            tbMaxZ.ReadOnly = true;
            tbMaxZ.Size = new System.Drawing.Size(44, 20);
            tbMaxZ.TabIndex = 157;
            tbMaxZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMinZ
            // 
            tbMinZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMinZ.BorderStyle = BorderStyle.None;
            tbMinZ.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMinZ.ForeColor = Color.Black;
            tbMinZ.Location = new System.Drawing.Point(21, 87);
            tbMinZ.Multiline = true;
            tbMinZ.Name = "tbMinZ";
            tbMinZ.ReadOnly = true;
            tbMinZ.Size = new System.Drawing.Size(46, 20);
            tbMinZ.TabIndex = 156;
            tbMinZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMaxY
            // 
            tbMaxY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMaxY.BorderStyle = BorderStyle.None;
            tbMaxY.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMaxY.ForeColor = Color.Black;
            tbMaxY.Location = new System.Drawing.Point(73, 61);
            tbMaxY.Multiline = true;
            tbMaxY.Name = "tbMaxY";
            tbMaxY.ReadOnly = true;
            tbMaxY.Size = new System.Drawing.Size(44, 20);
            tbMaxY.TabIndex = 155;
            tbMaxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMinY
            // 
            tbMinY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMinY.BorderStyle = BorderStyle.None;
            tbMinY.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMinY.ForeColor = Color.Black;
            tbMinY.Location = new System.Drawing.Point(21, 61);
            tbMinY.Multiline = true;
            tbMinY.Name = "tbMinY";
            tbMinY.ReadOnly = true;
            tbMinY.Size = new System.Drawing.Size(46, 20);
            tbMinY.TabIndex = 154;
            tbMinY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMaxX
            // 
            tbMaxX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMaxX.BorderStyle = BorderStyle.None;
            tbMaxX.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMaxX.ForeColor = Color.Black;
            tbMaxX.Location = new System.Drawing.Point(73, 35);
            tbMaxX.Multiline = true;
            tbMaxX.Name = "tbMaxX";
            tbMaxX.ReadOnly = true;
            tbMaxX.Size = new System.Drawing.Size(44, 20);
            tbMaxX.TabIndex = 153;
            tbMaxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btSave
            // 
            btSave.DialogResult = DialogResult.Cancel;
            btSave.Location = new System.Drawing.Point(11, 176);
            btSave.Name = "btSave";
            btSave.Size = new System.Drawing.Size(110, 23);
            btSave.TabIndex = 151;
            btSave.Text = "Save Calibration";
            btSave.UseVisualStyleBackColor = true;
            btSave.Click += btSave_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new System.Drawing.Point(309, 102);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(335, 553);
            tabControl1.TabIndex = 154;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tbTemps);
            tabPage1.Controls.Add(lbAxis1);
            tabPage1.Controls.Add(lbAxis3);
            tabPage1.Controls.Add(tbMonitor);
            tabPage1.Controls.Add(lbAxis4);
            tabPage1.Controls.Add(lbTemps);
            tabPage1.Controls.Add(lbAxis2);
            tabPage1.Controls.Add(elementHost1);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new System.Drawing.Size(327, 525);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Head";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // elementHost1
            // 
            elementHost1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            elementHost1.CausesValidation = false;
            elementHost1.Enabled = false;
            elementHost1.ForeColor = Color.FloralWhite;
            elementHost1.Location = new System.Drawing.Point(6, 10);
            elementHost1.Name = "elementHost1";
            elementHost1.Size = new System.Drawing.Size(314, 343);
            elementHost1.TabIndex = 30;
            elementHost1.Text = "elementHost1";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(groupBox2);
            tabPage2.Controls.Add(gbMagCalib);
            tabPage2.Controls.Add(elementHost2);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new System.Drawing.Size(327, 525);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Magnetometer";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.CausesValidation = false;
            groupBox2.Controls.Add(tbMagMat9);
            groupBox2.Controls.Add(tbMagMat8);
            groupBox2.Controls.Add(tbMagMat7);
            groupBox2.Controls.Add(tbMagMat6);
            groupBox2.Controls.Add(tbMagMat5);
            groupBox2.Controls.Add(tbMagMat4);
            groupBox2.Controls.Add(tbMagMat1);
            groupBox2.Controls.Add(tbMagMat2);
            groupBox2.Controls.Add(tbMagMat3);
            groupBox2.Controls.Add(btSave);
            groupBox2.Location = new System.Drawing.Point(186, 315);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(134, 205);
            groupBox2.TabIndex = 154;
            groupBox2.TabStop = false;
            groupBox2.Text = "Matrix";
            // 
            // tbMagMat9
            // 
            tbMagMat9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat9.BorderStyle = BorderStyle.None;
            tbMagMat9.CausesValidation = false;
            tbMagMat9.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat9.ForeColor = Color.Black;
            tbMagMat9.Location = new System.Drawing.Point(92, 80);
            tbMagMat9.Multiline = true;
            tbMagMat9.Name = "tbMagMat9";
            tbMagMat9.ReadOnly = true;
            tbMagMat9.Size = new System.Drawing.Size(39, 20);
            tbMagMat9.TabIndex = 162;
            tbMagMat9.TabStop = false;
            tbMagMat9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMagMat8
            // 
            tbMagMat8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat8.BorderStyle = BorderStyle.None;
            tbMagMat8.CausesValidation = false;
            tbMagMat8.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat8.ForeColor = Color.Black;
            tbMagMat8.Location = new System.Drawing.Point(49, 80);
            tbMagMat8.Multiline = true;
            tbMagMat8.Name = "tbMagMat8";
            tbMagMat8.ReadOnly = true;
            tbMagMat8.Size = new System.Drawing.Size(39, 20);
            tbMagMat8.TabIndex = 161;
            tbMagMat8.TabStop = false;
            tbMagMat8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMagMat7
            // 
            tbMagMat7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat7.BorderStyle = BorderStyle.None;
            tbMagMat7.CausesValidation = false;
            tbMagMat7.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat7.ForeColor = Color.Black;
            tbMagMat7.Location = new System.Drawing.Point(6, 80);
            tbMagMat7.Multiline = true;
            tbMagMat7.Name = "tbMagMat7";
            tbMagMat7.ReadOnly = true;
            tbMagMat7.Size = new System.Drawing.Size(39, 20);
            tbMagMat7.TabIndex = 160;
            tbMagMat7.TabStop = false;
            tbMagMat7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMagMat6
            // 
            tbMagMat6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat6.BorderStyle = BorderStyle.None;
            tbMagMat6.CausesValidation = false;
            tbMagMat6.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat6.ForeColor = Color.Black;
            tbMagMat6.Location = new System.Drawing.Point(92, 54);
            tbMagMat6.Multiline = true;
            tbMagMat6.Name = "tbMagMat6";
            tbMagMat6.ReadOnly = true;
            tbMagMat6.Size = new System.Drawing.Size(39, 20);
            tbMagMat6.TabIndex = 159;
            tbMagMat6.TabStop = false;
            tbMagMat6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMagMat5
            // 
            tbMagMat5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat5.BorderStyle = BorderStyle.None;
            tbMagMat5.CausesValidation = false;
            tbMagMat5.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat5.ForeColor = Color.Black;
            tbMagMat5.Location = new System.Drawing.Point(49, 54);
            tbMagMat5.Multiline = true;
            tbMagMat5.Name = "tbMagMat5";
            tbMagMat5.ReadOnly = true;
            tbMagMat5.Size = new System.Drawing.Size(39, 20);
            tbMagMat5.TabIndex = 158;
            tbMagMat5.TabStop = false;
            tbMagMat5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMagMat4
            // 
            tbMagMat4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat4.BorderStyle = BorderStyle.None;
            tbMagMat4.CausesValidation = false;
            tbMagMat4.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat4.ForeColor = Color.Black;
            tbMagMat4.Location = new System.Drawing.Point(6, 54);
            tbMagMat4.Multiline = true;
            tbMagMat4.Name = "tbMagMat4";
            tbMagMat4.ReadOnly = true;
            tbMagMat4.Size = new System.Drawing.Size(39, 20);
            tbMagMat4.TabIndex = 157;
            tbMagMat4.TabStop = false;
            tbMagMat4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMagMat1
            // 
            tbMagMat1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat1.BorderStyle = BorderStyle.None;
            tbMagMat1.CausesValidation = false;
            tbMagMat1.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat1.ForeColor = Color.Black;
            tbMagMat1.Location = new System.Drawing.Point(6, 28);
            tbMagMat1.Multiline = true;
            tbMagMat1.Name = "tbMagMat1";
            tbMagMat1.ReadOnly = true;
            tbMagMat1.Size = new System.Drawing.Size(39, 20);
            tbMagMat1.TabIndex = 0;
            tbMagMat1.TabStop = false;
            tbMagMat1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMagMat2
            // 
            tbMagMat2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat2.BorderStyle = BorderStyle.None;
            tbMagMat2.CausesValidation = false;
            tbMagMat2.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat2.ForeColor = Color.Black;
            tbMagMat2.Location = new System.Drawing.Point(49, 28);
            tbMagMat2.Multiline = true;
            tbMagMat2.Name = "tbMagMat2";
            tbMagMat2.ReadOnly = true;
            tbMagMat2.Size = new System.Drawing.Size(39, 20);
            tbMagMat2.TabIndex = 155;
            tbMagMat2.TabStop = false;
            tbMagMat2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbMagMat3
            // 
            tbMagMat3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbMagMat3.BorderStyle = BorderStyle.None;
            tbMagMat3.CausesValidation = false;
            tbMagMat3.Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            tbMagMat3.ForeColor = Color.Black;
            tbMagMat3.Location = new System.Drawing.Point(92, 28);
            tbMagMat3.Multiline = true;
            tbMagMat3.Name = "tbMagMat3";
            tbMagMat3.ReadOnly = true;
            tbMagMat3.Size = new System.Drawing.Size(39, 20);
            tbMagMat3.TabIndex = 159;
            tbMagMat3.TabStop = false;
            tbMagMat3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // elementHost2
            // 
            elementHost2.BackColor = Color.White;
            elementHost2.CausesValidation = false;
            elementHost2.ImeMode = ImeMode.NoControl;
            elementHost2.Location = new System.Drawing.Point(6, 6);
            elementHost2.Name = "elementHost2";
            elementHost2.Size = new System.Drawing.Size(315, 300);
            elementHost2.TabIndex = 153;
            elementHost2.Text = "elementHost2";
            // 
            // gbMagGyro
            // 
            gbMagGyro.Controls.Add(btMagResetView);
            gbMagGyro.Controls.Add(btMagAutoBias);
            gbMagGyro.Controls.Add(tbGyroX);
            gbMagGyro.Controls.Add(lbGX);
            gbMagGyro.Controls.Add(tbGyroZ);
            gbMagGyro.Controls.Add(tbGyroY);
            gbMagGyro.Location = new System.Drawing.Point(181, 269);
            gbMagGyro.Name = "gbMagGyro";
            gbMagGyro.Size = new System.Drawing.Size(293, 124);
            gbMagGyro.TabIndex = 157;
            gbMagGyro.TabStop = false;
            gbMagGyro.Text = "Auto Gyro Bias";
            // 
            // btMagResetView
            // 
            btMagResetView.BackColor = System.Drawing.SystemColors.ButtonFace;
            btMagResetView.BackgroundImageLayout = ImageLayout.None;
            btMagResetView.FlatAppearance.BorderColor = Color.MidnightBlue;
            btMagResetView.FlatAppearance.BorderSize = 2;
            btMagResetView.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btMagResetView.Location = new System.Drawing.Point(7, 24);
            btMagResetView.Margin = new Padding(2, 3, 2, 3);
            btMagResetView.Name = "btMagResetView";
            btMagResetView.Size = new System.Drawing.Size(173, 23);
            btMagResetView.TabIndex = 168;
            btMagResetView.Text = "Reset View";
            btMagResetView.TextImageRelation = TextImageRelation.TextBeforeImage;
            btMagResetView.UseVisualStyleBackColor = true;
            btMagResetView.Click += btMagResetView_Click;
            // 
            // btMagAutoBias
            // 
            btMagAutoBias.BackColor = System.Drawing.SystemColors.ButtonFace;
            btMagAutoBias.FlatAppearance.BorderColor = Color.MidnightBlue;
            btMagAutoBias.FlatAppearance.BorderSize = 2;
            btMagAutoBias.FlatAppearance.MouseOverBackColor = Color.CornflowerBlue;
            btMagAutoBias.Location = new System.Drawing.Point(7, 53);
            btMagAutoBias.Margin = new Padding(2, 3, 2, 3);
            btMagAutoBias.Name = "btMagAutoBias";
            btMagAutoBias.Size = new System.Drawing.Size(173, 23);
            btMagAutoBias.TabIndex = 167;
            btMagAutoBias.Text = "Auto Gyro Bias";
            btMagAutoBias.UseVisualStyleBackColor = true;
            btMagAutoBias.Click += btMagAutoBias_Click;
            // 
            // tbGyroX
            // 
            tbGyroX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbGyroX.Location = new System.Drawing.Point(48, 85);
            tbGyroX.Name = "tbGyroX";
            tbGyroX.ReadOnly = true;
            tbGyroX.Size = new System.Drawing.Size(39, 21);
            tbGyroX.TabIndex = 166;
            tbGyroX.Text = "0";
            tbGyroX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbGX
            // 
            lbGX.AutoSize = true;
            lbGX.Location = new System.Drawing.Point(12, 87);
            lbGX.Name = "lbGX";
            lbGX.Size = new System.Drawing.Size(32, 15);
            lbGX.TabIndex = 165;
            lbGX.Text = "Gyro";
            lbGX.TextAlign = ContentAlignment.MiddleRight;
            // 
            // tbGyroZ
            // 
            tbGyroZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbGyroZ.Location = new System.Drawing.Point(137, 85);
            tbGyroZ.Name = "tbGyroZ";
            tbGyroZ.ReadOnly = true;
            tbGyroZ.Size = new System.Drawing.Size(39, 21);
            tbGyroZ.TabIndex = 164;
            tbGyroZ.Text = "0";
            tbGyroZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbGyroY
            // 
            tbGyroY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tbGyroY.Location = new System.Drawing.Point(92, 85);
            tbGyroY.Name = "tbGyroY";
            tbGyroY.ReadOnly = true;
            tbGyroY.Size = new System.Drawing.Size(39, 21);
            tbGyroY.TabIndex = 162;
            tbGyroY.Text = "0";
            tbGyroY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            BackColor = System.Drawing.SystemColors.InactiveCaption;
            BackgroundImageLayout = ImageLayout.Center;
            ClientSize = new System.Drawing.Size(654, 661);
            Controls.Add(gbMagGyro);
            Controls.Add(tabControl1);
            Controls.Add(gbHotKey);
            Controls.Add(gbScaling);
            Controls.Add(gbDriftComp);
            Controls.Add(tbSketch);
            Controls.Add(bMonitor);
            Controls.Add(bWipeAll);
            Controls.Add(groupBox1);
            Controls.Add(gbBias);
            Controls.Add(gbTrackerConfig);
            Controls.Add(menuStrip1);
            Font = new Font("Arial", 9F, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, 0);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            HelpButton = true;
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4, 5, 4, 5);
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(670, 700);
            MinimumSize = new System.Drawing.Size(670, 700);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "EDTracker UI";
            groupBox1.ResumeLayout(false);
            gbBias.ResumeLayout(false);
            gbBias.PerformLayout();
            gbHotKey.ResumeLayout(false);
            gbHotKey.PerformLayout();
            gbTrackerConfig.ResumeLayout(false);
            gbTrackerConfig.PerformLayout();
            gbDriftComp.ResumeLayout(false);
            gbDriftComp.PerformLayout();
            gbScaling.ResumeLayout(false);
            gbScaling.PerformLayout();
            ((ISupportInitialize)sliderSmoothing).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            gbMagCalib.ResumeLayout(false);
            gbMagCalib.PerformLayout();
            ((ISupportInitialize)tbarMagSens).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            gbMagGyro.ResumeLayout(false);
            gbMagGyro.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbOffZ;
        private System.Windows.Forms.TextBox tbOffY;
        private System.Windows.Forms.TextBox tbOffX;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gbMagCalib;
        private System.Windows.Forms.TrackBar tbarMagSens;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tbMagSamples;
        private System.Windows.Forms.TextBox tbMinX;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox tbMaxZ;
        private System.Windows.Forms.TextBox tbMinZ;
        private System.Windows.Forms.TextBox tbMaxY;
        private System.Windows.Forms.TextBox tbMinY;
        private System.Windows.Forms.TextBox tbMaxX;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem Help;
        private System.Windows.Forms.ToolStripMenuItem dEVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tESTToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem uSERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TrackBar sliderSmoothing;
        private System.Windows.Forms.Button bRespMode;
        private System.Windows.Forms.Label lbAxis1;
        private System.Windows.Forms.Label lbSmooth;
        private System.Windows.Forms.Label lbAxis3;
        private System.Windows.Forms.Label lbAxis4;
        private System.Windows.Forms.TextBox tbSmoothing;
        private System.Windows.Forms.TextBox tbMonitor;
        private System.Windows.Forms.TextBox tbPitchScaling;
        private System.Windows.Forms.TextBox tbTemps;
        private System.Windows.Forms.Label lbTemps;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label lbAxis2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.TextBox tbGyroZ;
        private System.Windows.Forms.Button btMagResetView;
        private System.Windows.Forms.Button btMagAutoBias;
        private System.Windows.Forms.TextBox tbGyroX;
        private System.Windows.Forms.Label lbGX;
        private System.Windows.Forms.TextBox tbGyroY;
        private System.Windows.Forms.GroupBox gbMagGyro;
        private System.Windows.Forms.TextBox tbMagMat9;
        private System.Windows.Forms.TextBox tbMagMat8;
        private System.Windows.Forms.TextBox tbMagMat7;
        private System.Windows.Forms.TextBox tbMagMat6;
        private System.Windows.Forms.TextBox tbMagMat5;
        private System.Windows.Forms.TextBox tbMagMat4;
        private System.Windows.Forms.TextBox tbMagMat1;
        private System.Windows.Forms.TextBox tbMagMat2;
        private System.Windows.Forms.TextBox tbMagMat3;
        private System.Windows.Forms.TextBox tbRespMode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox biasAX;
        private System.Windows.Forms.Button axminus;
        private System.Windows.Forms.Button axplus;
        private System.Windows.Forms.TextBox udAX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox biasAY;
        private System.Windows.Forms.Button ayminus;
        private System.Windows.Forms.Button ayplus;
        private System.Windows.Forms.TextBox udAY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox biasAZ;
        private System.Windows.Forms.Button azminus;
        private System.Windows.Forms.Button azplus;
        private System.Windows.Forms.TextBox udAZ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox biasGZ;
        private System.Windows.Forms.Button gzminus;
        private System.Windows.Forms.Button gzplus;
        private System.Windows.Forms.TextBox udGZ;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox biasGY;
        private System.Windows.Forms.Button gyminus;
        private System.Windows.Forms.Button gyplus;
        private System.Windows.Forms.TextBox udGY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox biasGX;
        private System.Windows.Forms.Button gxminus;
        private System.Windows.Forms.Button bMonitor;
        private System.Windows.Forms.Button bScanPorts;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbSketches;
        private System.Windows.Forms.Button bFlash;
        private System.Windows.Forms.ComboBox cbPort;
        private System.Windows.Forms.Button bWipeAll;
        private System.Windows.Forms.GroupBox gbBias;
        private System.Windows.Forms.TextBox udGX;
        private System.Windows.Forms.Button gxplus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bCalcBias;
        private System.Windows.Forms.GroupBox gbScaling;
        private System.Windows.Forms.TextBox tbYawScaling;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox cbFineAdjust;
        private System.Windows.Forms.Button btYawScaleDown;
        private System.Windows.Forms.Button btPitchScaleDown;
        private System.Windows.Forms.Button btPitchScaleUp;
        private System.Windows.Forms.Button btYawScaleUp;
        private System.Windows.Forms.Button bSaveDriftComp;
        private System.Windows.Forms.Button bResetView;
        private System.Windows.Forms.TextBox tbDriftComp;
        private System.Windows.Forms.Label lbComp;
        private System.Windows.Forms.TextBox tbTime;
        private System.Windows.Forms.Label lbYawDrift;
        private System.Windows.Forms.Button btCompDown;
        private System.Windows.Forms.GroupBox gbHotKey;
        private System.Windows.Forms.Button btEnableAC;
        private System.Windows.Forms.TextBox tbAutoCentre;
        private System.Windows.Forms.Button btToggleAutoCentre;
        private System.Windows.Forms.ComboBox cbHKey;
        private System.Windows.Forms.ComboBox cbCOntrollerButtons;
        private System.Windows.Forms.CheckBox cbControllerButton;
        private System.Windows.Forms.CheckBox cbHotKey;
        private System.Windows.Forms.TextBox tbYawDrift;
        private System.Windows.Forms.Button btCompUp;
        private System.Windows.Forms.GroupBox gbDriftComp;
        private System.Windows.Forms.Label lbTimer;
        private System.Windows.Forms.GroupBox gbTrackerConfig;
        private System.Windows.Forms.Button bOrientate;
        private System.Windows.Forms.TextBox tbOrientation;
        private System.Windows.Forms.Button bSensorMode;
        private System.Windows.Forms.TextBox tbSensorMode;
        private System.Windows.Forms.TextBox tbSketch;
    }
}