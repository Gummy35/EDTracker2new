// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.Form1
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using GlobalHotkeys;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media.Media3D;
using System.Xml;
using System.Xml.Serialization;

#nullable disable
namespace EDTrackerUI3
{
    public class Form1 : Form
    {
        private const int maxmagsamples = 2000;
        private const int hs = 315;
        private float rad2deg = 57.29578f;
        private float deg2rad = (float)Math.PI / 180f;
        private float rad2FSR = 10430.06f;
        private float maxGX;
        private float maxGY;
        private float maxGZ;
        private float maxAX;
        private float maxAY;
        private float maxAZ;
        private float heading = 999f;
        private float magSensitivity;
        private float pitchScale;
        private float yawScale;
        private float outputLPF = 50f;
        private string orientation = "Unknown";
        private double orientationAngle;
        private string scaleMode = "Unknown";
        private string info = "Unknown Device";
        private string lastKnownDevice = "Unknown Device";
        private float startTemp;
        private float temperature;
        private float tempSlope;
        private frmDebug debugForm;
        private Matrix<double> rawMagCalibMatrix;
        private Matrix<double> orientatedMagCalibMatrix;
        private float[] magOffsets = new float[3];
        private bool magDataValid;
        private string lastKnownUI = "UNKNOWN";
        private string userMode = "UNKNOWN";
        private long lastPing;
        private int portNumber = 2;
        private string portName;
        private string buffer;
        private float DMPRoll;
        private float DMPPitch;
        private float DMPYaw;
        private bool expScaleMode;
        private bool pollMPU;
        private float scaleFactor = 1f;
        private int rawGyroX;
        private int rawGyroY;
        private int rawGyroZ;
        private int rawAccelX;
        private int rawAccelY;
        private int rawAccelZ;
        private float driftScale = 10f;
        private float yawDrift;
        private float pitchDrift;
        private bool fineAdj;
        private int autocentre;
        private bool tempStable;
        private float yawDriftComp;
        private bool driftComp;
        private bool biasValuesRecalced;
        private int adjustX = 1;
        private int adjustY = 1;
        private int adjustZ = 1;
        private int gBiasX;
        private int gBiasY;
        private int gBiasZ;
        private int aBiasX;
        private int aBiasY;
        private int aBiasZ;
        private long lastPress;
        private long lastSerialEvent;
        private lowPassFilter lpX = new lowPassFilter(0.98f);
        private lowPassFilter lpY = new lowPassFilter(0.98f);
        private lowPassFilter lpZ = new lowPassFilter(0.98f);
        private lowPassFilter aX = new lowPassFilter(0.93f);
        private lowPassFilter aY = new lowPassFilter(0.93f);
        private lowPassFilter aZ = new lowPassFilter(0.93f);
        private lowPassFilter gX = new lowPassFilter(0.992f);
        private lowPassFilter gY = new lowPassFilter(0.992f);
        private lowPassFilter gZ = new lowPassFilter(0.992f);
        private lowPassFilter magLP = new lowPassFilter(0.98f);
        private Vector3D[] magPoints = new Vector3D[2000];
        private int magSamples;
        private int magSamplesHWM;
        private double minMagX;
        private double minMagY;
        private double minMagZ;
        private double maxMagX;
        private double maxMagY;
        private double maxMagZ;
        private Stopwatch appTime = new Stopwatch();
        private static Form1.myStateType myState = Form1.myStateType.UNKNOWN;
        private Form1.DoubleBufferedPanel buffPanel1;
        private Form1.DoubleBufferedPanel buffPanel2;
        private int r;
        private bool portThreadRunning;
        private bool closePending;
        private volatile bool _shouldStop;
        private System.Drawing.Point[] tempHist = new System.Drawing.Point[315];
        private System.Drawing.Point[] tDeltaHist = new System.Drawing.Point[315];
        private System.Drawing.Point[] yawHist = new System.Drawing.Point[315];
        private System.Drawing.Point[] yawDriftHist = new System.Drawing.Point[315];
        private System.Drawing.Point[] pitchHist = new System.Drawing.Point[315];
        private System.Drawing.Point[] gyroXHist = new System.Drawing.Point[315];
        private System.Drawing.Point[] gyroYHist = new System.Drawing.Point[315];
        private System.Drawing.Point[] gyroZHist = new System.Drawing.Point[315];
        private Pen whitePen = new Pen(System.Drawing.Color.White);
        private Pen redPen = new Pen(System.Drawing.Color.Red);
        private Pen greenPen = new Pen(System.Drawing.Color.LawnGreen);
        private Pen bluePen = new Pen(System.Drawing.Color.LightSkyBlue);
        private Pen yellowPen = new Pen(System.Drawing.Color.Yellow);
        private string edtUpdateURL = "http://www.edtracker.org.uk/update/edflash.xml";
        private static StringCollection hexfiles;
        private BackgroundWorker scanForPortsWorker = new BackgroundWorker();
        private SerialPort ardPort;
        private GlobalHotkey hotkey;
        private RegistryKey rkCurrentUser;
        private Form1.myStateType preMinimiseState;
        private ContextMenuStrip contextMenu1;
        private ToolStripMenuItem menuItem1;
        private bool biasHasChanged;
        private Dictionary<string, Keys> hotKeyList = new Dictionary<string, Keys>();
        private frmBiasCalc fCalBias;
        private bool waitingForBias;
        private bool gBiasGood;
        private bool aBiasGood;
        private bool hotKeyEnabled;
        private string lastKnownPort = "UNKNOWN";
        private string lastSelectAutoCentreOption = "UNKNOWN";
        private bool centringSuppressed;
        private bool firstTimeWarning;
        private magForm mf;
        private IContainer components;
        private Button bWipeAll;
        private ElementHost elementHost1;
        private EDTrackerWpfControls.HeadTracker edtracker1;
        private ComboBox cbSketches;
        private ComboBox cbPort;
        private Button bFlash;
        private GroupBox groupBox1;
        private Button bMonitor;
        private GroupBox gbBias;
        private GroupBox gbTrackerConfig;
        private Button bOrientate;
        private TextBox tbOrientation;
        private Button bSensorMode;
        private TextBox tbSensorMode;
        private TextBox biasAX;
        private Button axminus;
        private Button axplus;
        private TextBox udAX;
        private Label label5;
        private TextBox biasAY;
        private Button ayminus;
        private Button ayplus;
        private TextBox udAY;
        private Label label6;
        private TextBox biasAZ;
        private Button azminus;
        private Button azplus;
        private TextBox udAZ;
        private Label label7;
        private TextBox biasGZ;
        private Button gzminus;
        private Button gzplus;
        private TextBox udGZ;
        private Label label4;
        private TextBox biasGY;
        private Button gyminus;
        private Button gyplus;
        private TextBox udGY;
        private Label label3;
        private TextBox biasGX;
        private Button gxminus;
        private TextBox udGX;
        private Button gxplus;
        private Label label2;
        private Button bCalcBias;
        private Button bScanPorts;
        private TextBox tbSketch;
        private GroupBox gbDriftComp;
        private TextBox tbDriftComp;
        private Label lbComp;
        private TextBox tbYawDrift;
        private Label lbYawDrift;
        private Button btCompDown;
        private Button btCompUp;
        private Button bResetView;
        private GroupBox gbScaling;
        private TextBox tbRespMode;
        private Button bRespMode;
        private TextBox tbPitchScaling;
        private Label label11;
        private TextBox tbYawScaling;
        private Label label10;
        private CheckBox cbFineAdjust;
        private Button btYawScaleDown;
        private Button btPitchScaleDown;
        private Button btPitchScaleUp;
        private Button btYawScaleUp;
        private GroupBox gbHotKey;
        private ComboBox cbCOntrollerButtons;
        private CheckBox cbControllerButton;
        private CheckBox cbHotKey;
        private TextBox tbTemps;
        private Label lbTemps;
        private TextBox tbMonitor;
        private NotifyIcon notifyIcon1;
        private ComboBox cbHKey;
        private Label lbAxis4;
        private Label lbAxis3;
        private Label lbAxis2;
        private Label lbAxis1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem toolStripMenuItem1;
        private TextBox tbTime;
        private System.Windows.Forms.Timer timer1;
        private Label lbTimer;
        private ToolStripMenuItem Help;
        private Button bSaveDriftComp;
        private TextBox tbAutoCentre;
        private Button btToggleAutoCentre;
        private ToolStripMenuItem debugToolStripMenuItem;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem hideToolStripMenuItem;
        private Button btEnableAC;
        private GroupBox gbMagCalib;
        private Label label9;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private TextBox tbMaxZ;
        private TextBox tbMinZ;
        private TextBox tbMaxY;
        private TextBox tbMinY;
        private TextBox tbMaxX;
        private TextBox tbMinX;
        private Button btSave;
        private ElementHost elementHost2;
        private EDTrackerWpfControls.MagCloud magCloud1;
        private TextBox tbMagSamples;
        private Label label16;
        private Button button1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TextBox tbSmoothing;
        private Label lbSmooth;
        private TrackBar sliderSmoothing;
        private TrackBar tbarMagSens;
        private Label label18;
        private GroupBox groupBox2;
        private TextBox tbMagMat6;
        private TextBox tbMagMat5;
        private TextBox tbMagMat4;
        private TextBox tbMagMat1;
        private TextBox tbMagMat2;
        private TextBox tbMagMat3;
        private GroupBox gbMagGyro;
        private Button btMagResetView;
        private Button btMagAutoBias;
        private TextBox tbGyroX;
        private Label lbGX;
        private TextBox tbGyroZ;
        private TextBox tbGyroY;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem modeToolStripMenuItem;
        private ToolStripMenuItem uSERToolStripMenuItem;
        private ToolStripMenuItem tESTToolStripMenuItem;
        private ToolStripMenuItem dEVToolStripMenuItem;
        private TextBox tbMagMat9;
        private TextBox tbMagMat8;
        private TextBox tbMagMat7;
        private Label label1;
        private TextBox tbOffZ;
        private TextBox tbOffY;
        private TextBox tbOffX;

        public Form1()
        {
            this.buffPanel1 = new Form1.DoubleBufferedPanel();
            this.buffPanel1.BackColor = System.Drawing.Color.Black;
            this.buffPanel1.Location = new System.Drawing.Point(6, 355);
            this.buffPanel1.Name = "panel1";
            this.buffPanel1.Size = new System.Drawing.Size(315, 180);
            this.buffPanel1.TabIndex = 58;
            this.buffPanel1.Paint += new PaintEventHandler(this.panel1_Paint);
            this.buffPanel2 = new Form1.DoubleBufferedPanel();
            this.buffPanel2.BackColor = System.Drawing.Color.Transparent;
            this.buffPanel2.Location = new System.Drawing.Point(30, 460);
            this.buffPanel2.Name = "panel2";
            this.buffPanel2.Size = new System.Drawing.Size(162, 162);
            this.buffPanel2.TabIndex = 58;
            this.buffPanel2.Paint += new PaintEventHandler(this.panel2_Paint);
            this.Controls.Add((Control)this.buffPanel2);
            this.InitializeComponent();

            edtracker1 = new EDTrackerWpfControls.HeadTracker();
            elementHost1.Child = (UIElement)this.edtracker1;
            magCloud1 = new EDTrackerWpfControls.MagCloud();
            elementHost2.Child = (UIElement)this.magCloud1;

            this.tabPage1.Controls.Add((Control)this.buffPanel1);
            this.tabPage2.Enter += new EventHandler(this.tabPage2_Enter);
            this.tabPage1.Enter += new EventHandler(this.tabPage1_Enter);
            this.tabControl1.Controls.Remove((Control)this.tabPage2);
            this.buffPanel1.SendToBack();
            this.hotKeyList["F1"] = Keys.F1;
            this.hotKeyList["F2"] = Keys.F2;
            this.hotKeyList["F3"] = Keys.F3;
            this.hotKeyList["F4"] = Keys.F4;
            this.hotKeyList["F5"] = Keys.F5;
            this.hotKeyList["F6"] = Keys.F6;
            this.hotKeyList["F7"] = Keys.F7;
            this.hotKeyList["F8"] = Keys.F8;
            this.hotKeyList["F9"] = Keys.F9;
            this.hotKeyList["F10"] = Keys.F10;
            this.hotKeyList["F11"] = Keys.F11;
            this.cbHKey.Items.Clear();
            foreach (KeyValuePair<string, Keys> hotKey in this.hotKeyList)
                this.cbHKey.Items.Add((object)hotKey.Key);
            this.gbTrackerConfig.Enabled = false;
            this.gbScaling.Enabled = false;
            this.gbScaling.Hide();
            this.gbHotKey.Hide();
            this.elementHost2.Hide();
            this.magCloud1.Hide();
            this.gbBias.Enabled = false;
            this.gbBias.Show();
            this.gbDriftComp.Hide();
            this.buffPanel2.Show();
            this.bMonitor.Enabled = false;
            this.btEnableAC.Enabled = false;
            this.btEnableAC.Hide();
            this.setMyState(Form1.myStateType.UNKNOWN);
            this.scanForPortsWorker.DoWork += new DoWorkEventHandler(this.scanForPortsWorker_DoWork);
            this.scanForPortsWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.scanForPortsWorker_RunWorkerCompleted);
            for (int index = 0; index < 315; ++index)
            {
                this.yawDriftHist[index].X = this.yawHist[index].X = this.pitchHist[index].X = this.tempHist[index].X = this.tDeltaHist[index].X = index;
                this.tempHist[index].Y = 36;
                this.tDeltaHist[index].Y = 36;
                this.yawHist[index].Y = 108;
                this.pitchHist[index].Y = 144;
                this.yawDriftHist[index].Y = 72;
                this.gyroXHist[index].X = this.gyroYHist[index].X = this.gyroZHist[index].X = index;
                this.gyroXHist[index].Y = 72;
                this.gyroYHist[index].Y = 108;
                this.gyroZHist[index].Y = 144;
            }
            this.Resize += new EventHandler(this.Form1_ResizeEnd);
            this.contextMenu1 = new ContextMenuStrip();
            this.menuItem1 = new ToolStripMenuItem();
            this.contextMenu1.Items.AddRange(new ToolStripMenuItem[1]
            {
                this.menuItem1
            });
            //this.menuItem1.Index = 0;
            this.menuItem1.Text = "E&xit";
            this.menuItem1.Click += new EventHandler(this.close_clicked);
            this.notifyIcon1.ContextMenuStrip = this.contextMenu1;
            this.portThreadRunning = true;
            this.scanForPortsWorker.RunWorkerAsync();
            this.cbPort.SelectedIndex = 0;
            this.cbSketches.SelectedIndex = 0;
            this.maxGX = this.maxGY = this.maxGZ = 0.0f;
            this.maxAX = this.maxAY = this.maxAZ = 0.0f;
            this.minMagX = this.minMagY = this.minMagZ = 999.99;
            this.maxMagX = this.maxMagY = this.maxMagZ = -999.99;
            this.rawMagCalibMatrix = Matrix<double>.Build.Dense(3, 3);
            this.orientatedMagCalibMatrix = Matrix<double>.Build.Dense(3, 3);
            try
            {
                this.rkCurrentUser = Registry.CurrentUser;
                RegistryKey registryKey = this.rkCurrentUser.OpenSubKey("Software\\EDTracker", true);
                if (registryKey == null)
                {
                    RegistryKey subKey = this.rkCurrentUser.CreateSubKey("Software\\EDTracker");
                    subKey.SetValue("HOTKEY", (object)-1);
                    subKey.SetValue("HKENABLED", (object)0);
                    subKey.SetValue("LASTKNOWNPORT", (object)"UNKNOWN");
                    subKey.Close();
                }
                else
                {
                    int num = (int)registryKey.GetValue("HOTKEY");
                    if (num >= 0)
                        this.cbHKey.SelectedIndex = num;
                    if ((int)registryKey.GetValue("HKENABLED") > 0)
                        this.hotKeyEnabled = true;
                    this.lastKnownPort = (string)registryKey.GetValue("LASTKNOWNPORT");
                    this.userMode = (string)registryKey.GetValue("USERMODE");
                    if (this.userMode == null)
                        this.userMode = "USER";
                }
                this.rkCurrentUser.Close();
            }
            catch (Exception ex)
            {
            }
            this.Text = this.Text + " " + System.Windows.Forms.Application.ProductVersion;
            this.appTime.Stop();
            this.gbMagGyro.Location = new System.Drawing.Point(3, 474);
            this.magSensitivity = (float)this.tbarMagSens.Maximum;
            this.tbarMagSens.Value = 0;
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(this.edtUpdateURL));
        }

        private void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                int num1 = (int)System.Windows.Forms.MessageBox.Show(e.Error.Message + "\nYou will not be able to flash sketches to the device. Please check your firewall settings or internet connection.", "Unable to contact edtracker.org.uk", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {
                string result = e.Result;
                new XmlDocument().LoadXml(result);
                edflash edflash = new edflash();
                try
                {
                    edflashDownload[] download = ((edflash)new XmlSerializer(typeof(edflash)).Deserialize((XmlReader)new XmlTextReader((TextReader)new StringReader(result)))).download;
                    Form1.hexfiles = new StringCollection();
                    this.cbSketches.Items.Clear();
                    string[] strArray = System.Windows.Forms.Application.ProductVersion.Split('.');
                    int num2 = 10000 * (int)short.Parse(strArray[0]) + 100 * (int)short.Parse(strArray[1]) + (int)short.Parse(strArray[2]);
                    foreach (edflashDownload edflashDownload in download)
                    {
                        int num3 = 10000 * (int)short.Parse(edflashDownload.minimumgui.major) + 100 * (int)short.Parse(edflashDownload.minimumgui.minor) + (int)short.Parse(edflashDownload.minimumgui.patch);
                        int num4 = 10000 * (int)short.Parse(edflashDownload.major) + 100 * (int)short.Parse(edflashDownload.minor) + (int)short.Parse(edflashDownload.patch);
                        if (num2 >= num3 && (((IEnumerable<string>)edflashDownload.validhardware).Contains<string>("6050") || ((IEnumerable<string>)edflashDownload.validhardware).Contains<string>("9150") && num4 >= 40003) && (this.userMode == "DEV" || this.userMode == "TEST" && edflashDownload.releasestate != "DEV" || this.userMode == "USER" && edflashDownload.releasestate == "PRD"))
                        {
                            this.cbSketches.Items.Add((object)(edflashDownload.name.PadRight(20) + edflashDownload.major + "." + edflashDownload.minor + "." + edflashDownload.patch));
                            Form1.hexfiles.Add(edflashDownload.imageurl);
                        }
                    }
                    this.cbSketches.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    int num5 = (int)System.Windows.Forms.MessageBox.Show("Unable to contact edtracker.org.uk\n" + ex.InnerException.Message + "\nYou will not be able to flash sketches to the device.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(this.whitePen, 0, 36, 320, 36);
            e.Graphics.DrawLine(this.whitePen, 0, 72, 320, 72);
            e.Graphics.DrawLine(this.whitePen, 0, 108, 320, 108);
            e.Graphics.DrawLine(this.whitePen, 0, 144, 320, 144);
            e.Graphics.SmoothingMode = SmoothingMode.HighSpeed;
            e.Graphics.DrawLines(this.redPen, this.tDeltaHist);
            if (this.info.Contains("Calib"))
            {
                e.Graphics.DrawLines(this.yellowPen, this.gyroXHist);
                e.Graphics.DrawLines(this.greenPen, this.gyroYHist);
                e.Graphics.DrawLines(this.bluePen, this.gyroZHist);
            }
            else
            {
                e.Graphics.DrawLines(this.greenPen, this.yawHist);
                e.Graphics.DrawLines(this.bluePen, this.pitchHist);
                e.Graphics.DrawLines(this.yellowPen, this.yawDriftHist);
            }
        }

        private int constrain(int n, int lim)
        {
            if (n < -lim)
                return -lim;
            return n > lim ? lim : n;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(System.Drawing.Color.Black);
            graphics.DrawLine(pen, 80, 0, 80, 160);
            graphics.DrawLine(pen, 0, 80, 160, 80);
            graphics.DrawEllipse(pen, 0, 0, 160, 160);
            graphics.DrawEllipse(pen, 40, 40, 80, 80);
            graphics.DrawEllipse(pen, 60, 60, 40, 40);
            SolidBrush solidBrush1 = new SolidBrush(System.Drawing.Color.White);
            SolidBrush solidBrush2 = new SolidBrush(System.Drawing.Color.Gold);
            graphics.FillEllipse((Brush)solidBrush1, 76 + this.constrain(this.rawAccelX, 70), 76 + this.constrain(this.rawAccelY, 70), 9, 9);
            graphics.DrawEllipse(pen, 76 + this.constrain(this.rawAccelX, 70), 76 + this.constrain(this.rawAccelY, 70), 9, 9);
            graphics.FillEllipse((Brush)solidBrush2, 76 + this.constrain(this.rawGyroX, 70), 76 + this.constrain(this.rawGyroY, 70), 9, 9);
            graphics.DrawEllipse(pen, 76 + this.constrain(this.rawGyroX, 70), 76 + this.constrain(this.rawGyroY, 70), 9, 9);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ardPort.Write("D\n");
            Thread.Sleep(100);
            this.ardPort.Write("R\n");
        }

        private void button3_Click(object sender, EventArgs e) => this.ardPort.Write("P\n");

        private void scanForTracker(object sender, EventArgs e)
        {
            this.cbPort.Items.Clear();
            //            foreach (object portName in SerialPortStream.GetPortNames())
            foreach (object portName in RJCP.IO.Ports.SerialPortStream.GetPortNames())
                this.cbPort.Items.Add(portName);
        }

        private void scanForPortsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var desc = RJCP.IO.Ports.SerialPortStream.GetPortDescriptions();
            var list = new List<string>();
            foreach (RJCP.IO.Ports.PortDescription description in desc)
            {
                list.Add($"{description.Port} - {description.Description}");
            }
            e.Result = (object)list;
        }

        private void scanForPortsWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                int num = (int)System.Windows.Forms.MessageBox.Show(e.Error.Message);
            }
            else if (!e.Cancelled)
            {
                this.cbPort.Items.Clear();
                foreach (string str in (List<string>)e.Result)
                {
                    if (str.ToUpper().Contains("ARDUINO") || str.ToUpper().Contains("EDTRACK") || str.ToUpper().Contains("LEONARD"))
                        this.cbPort.Items.Add((object)str);
                }
                foreach (string str in (List<string>)e.Result)
                {
                    if (!str.ToUpper().Contains("ARDUINO") && !str.ToUpper().Contains("EDTRACK") && !str.ToUpper().Contains("LEONARD"))
                        this.cbPort.Items.Add((object)str);
                }
                if (this.cbPort.Items.Count > 0)
                {
                    this.cbPort.SelectedIndex = 0;
                    this.bMonitor.Enabled = true;
                    for (int index = 0; index < this.cbPort.Items.Count; ++index)
                    {
                        if (this.cbPort.Items[index].ToString().Contains(this.lastKnownPort))
                        {
                            this.cbPort.SelectedIndex = index;
                            this.bMonitor_Click((object)null, (EventArgs)null);
                            index = 99;
                        }
                    }
                }
                else
                    this.bMonitor.Enabled = false;
            }
            this.portThreadRunning = false;
            if (this.closePending)
            {
                this.Close();
            }
            else
            {
                this.bFlash.Enabled = true;
                this.bMonitor.Enabled = true;
            }
        }

        private void elementHost1_ChildChanged(object sender, ChildChangedEventArgs e)
        {
        }

        private void getInfo_Click(object sender, EventArgs e) => this.ardPort.Write("I\n");

        protected override void OnPaint(PaintEventArgs e) => base.OnPaint(e);

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void button8_Click(object sender, EventArgs e) => System.Windows.Forms.Application.Exit();

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (this.tbAutoCentre.Text == "Off")
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Auto-centre is disabled. Residual Yaw drift will not be corrected", "Auto-centre Is Off", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            if (this.biasHasChanged)
            {
                if (this.biasGX.BackColor != System.Drawing.Color.LightGreen || this.biasGY.BackColor != System.Drawing.Color.LightGreen || this.biasGZ.BackColor != System.Drawing.Color.LightGreen || this.biasAX.BackColor != System.Drawing.Color.LightGreen || this.biasAY.BackColor != System.Drawing.Color.LightGreen || this.biasAZ.BackColor != System.Drawing.Color.LightGreen)
                {
                    if (System.Windows.Forms.MessageBox.Show("Please repeat bias calculation and adjust using +/- buttons where necessary until all bias values are close to zero and all number in left hand column are green. Do you still wish to Exit ?", "Bias Is Not Optimal", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                else if (System.Windows.Forms.MessageBox.Show("It is recommended that you now load the main sketch and perform Drift Compensation. Do you still wish to Exit ?", "Bias Has Changed", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            if (this.portThreadRunning)
            {
                this.Enabled = false;
                this.closePending = true;
                e.Cancel = true;
                new fClosing().Show((IWin32Window)this);
                this.Enabled = false;
            }
            else
            {
                this.disconnectArduino();
                Thread.Sleep(100);
                try
                {
                    RegistryKey registryKey = this.rkCurrentUser.OpenSubKey("Software\\EDTracker", true);
                    if (this.cbHotKey.Checked)
                        registryKey.SetValue("HKENABLED", (object)1);
                    else
                        registryKey.SetValue("HKENABLED", (object)0);
                    registryKey.Close();
                }
                catch (GlobalHotkeyException ex)
                {
                }
                try
                {
                    this.hotkey.Dispose();
                }
                catch (Exception ex)
                {
                }
                base.OnFormClosing(e);
            }
        }

        private void StartGraphThread()
        {
            this._shouldStop = false;
            new Thread(new ThreadStart(this.ThreadJob))
            {
                IsBackground = true
            }.Start();
        }

        private void StopGraphThread() => this._shouldStop = true;

        private void ThreadJob()
        {
            while (!this._shouldStop)
            {
                try
                {
                    this.Invoke((Delegate)(() =>
                    {
                        this.buffPanel1.Refresh();
                        this.buffPanel2.Refresh();
                    }));
                }
                catch (Exception ex)
                {
                }
                Thread.Sleep(15);
            }
        }

        private void cbSketches_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void setButtonStates()
        {
            this.bSaveDriftComp.Text = "Save Drift Compensation";
            this.bResetView.Text = "Reset View / Drift Tracking";
            this.sliderSmoothing.Enabled = false;
            this.lbSmooth.Enabled = false;
            this.tbSmoothing.Enabled = false;
            this.tbDriftComp.Enabled = true;
            this.btCompUp.Show();
            this.btCompDown.Show();
            this.gbMagGyro.Hide();
            this.tabControl1.Controls.Remove((Control)this.tabPage2);
            if (this.info.Contains("V4"))
            {
                this.sliderSmoothing.Enabled = true;
                this.lbSmooth.Enabled = true;
                this.tbSmoothing.Enabled = true;
            }
            if (this.info.Contains("TrackerMag"))
            {
                this.gbDriftComp.Hide();
                this.gbDriftComp.Enabled = false;
                this.gbMagCalib.Show();
                this.gbMagCalib.Enabled = true;
                this.gbMagGyro.Show();
                this.gbMagGyro.Enabled = true;
                this.tabControl1.Controls.Add((Control)this.tabPage2);
                this.gbHotKey.Show();
                this.gbHotKey.BringToFront();
                this.sliderSmoothing.Enabled = true;
                this.lbSmooth.Enabled = true;
                this.tbSmoothing.Enabled = true;
                this.bSaveDriftComp.Text = "Auto Gyro Calibration";
                this.bResetView.Text = "Reset View";
                this.biasHasChanged = false;
                this.bWipeAll.Enabled = true;
                //this.bWipeAll.Show();
                this.btToggleAutoCentre.Enabled = false;
                this.gbTrackerConfig.Show();
                this.gbTrackerConfig.Enabled = true;
                this.bSensorMode.Enabled = false;
                this.bOrientate.Enabled = true;
                this.gbScaling.Enabled = true;
                this.gbScaling.Show();
                this.gbHotKey.Show();
                this.gbHotKey.BringToFront();
                this.gbBias.Enabled = false;
                this.gbBias.Hide();
                this.buffPanel2.Hide();
                this.bCalcBias.Hide();
                this.buffPanel2.Hide();
                this.lbAxis1.Text = "Temperature Change";
                this.lbAxis2.Text = "Yaw Drift";
                this.lbAxis3.Text = "Yaw";
                this.lbAxis4.Text = "Pitch";
                this.lastKnownUI = "MAIN";
                if (this.hotKeyEnabled)
                    this.cbHotKey.Checked = true;
                this.elementHost2.Show();
                this.magCloud1.Show();
            }
            else if (this.info.Contains("Calib"))
            {
                this.gbMagCalib.Hide();
                this.gbMagCalib.Enabled = false;
                this.bWipeAll.Enabled = true;
                this.elementHost2.Hide();
                this.magCloud1.Hide();
                this.bWipeAll.Show();
                this.gbTrackerConfig.Show();
                this.gbTrackerConfig.Enabled = true;
                this.bSensorMode.Enabled = true;
                this.bOrientate.Enabled = false;
                this.gbScaling.Enabled = false;
                this.gbScaling.Hide();
                this.gbHotKey.Hide();
                this.bCalcBias.Show();
                this.lastKnownUI = "CALIB";
                this.gbBias.Enabled = true;
                this.gbBias.Show();
                this.buffPanel2.Show();
                this.gbDriftComp.Hide();
                this.buffPanel2.Show();
                this.lbAxis1.Text = "Temperature Change";
                this.lbAxis2.Text = "X Gyro";
                this.lbAxis3.Text = "Y Gyro";
                this.lbAxis4.Text = "Z Gyro";
            }
            else
            {
                this.gbMagCalib.Hide();
                this.gbMagCalib.Enabled = false;
                this.elementHost2.Hide();
                this.magCloud1.Hide();
                this.biasHasChanged = false;
                this.bWipeAll.Enabled = false;
                this.bWipeAll.Hide();
                this.btToggleAutoCentre.Enabled = true;
                this.gbTrackerConfig.Show();
                this.gbTrackerConfig.Enabled = true;
                this.bSensorMode.Enabled = false;
                this.bOrientate.Enabled = true;
                this.gbScaling.Enabled = true;
                this.gbScaling.Show();
                this.gbHotKey.Show();
                this.gbHotKey.BringToFront();
                this.gbBias.Enabled = false;
                this.gbBias.Hide();
                this.buffPanel2.Hide();
                this.bCalcBias.Hide();
                this.gbDriftComp.Enabled = true;
                this.gbDriftComp.Show();
                this.buffPanel2.Hide();
                this.lbAxis1.Text = "Temperature Change";
                this.lbAxis2.Text = "Yaw Drift";
                this.lbAxis3.Text = "Yaw";
                this.lbAxis4.Text = "Pitch";
                this.lastKnownUI = "MAIN";
                if (this.hotKeyEnabled)
                    this.cbHotKey.Checked = true;
            }
            try
            {
                RegistryKey registryKey = this.rkCurrentUser.OpenSubKey("Software\\EDTracker", true);
                registryKey.SetValue("LASTKNOWNPORT", (object)this.ardPort.PortName);
                registryKey.Close();
            }
            catch (GlobalHotkeyException ex)
            {
                int num = (int)System.Windows.Forms.MessageBox.Show(ex.Message);
                this.cbHotKey.Checked = false;
            }
        }

        private void setMyState(Form1.myStateType state)
        {
            Form1.myState = state;
            this.tbMonitor.Text = Form1.myState.ToString();
            if (state == Form1.myStateType.MONITORING)
            {
                this.tbSketch.Text = this.info;
                this.bFlash.Enabled = true;
                this.StartGraphThread();
                this.bMonitor.Text = "DISCONNECT";
                this.ardPort.Write("I\n");
                this.appTime.Reset();
            }
            else
            {
                this.StopGraphThread();
                this.tbSketch.Text = "NOT CONNECTED";
                this.bMonitor.Text = "CONNECT TO TRACKER";
                this.bFlash.Enabled = true;
                this.bWipeAll.Enabled = false;
                this.gbTrackerConfig.Enabled = false;
                this.gbScaling.Enabled = false;
                this.gbBias.Enabled = false;
                this.gbDriftComp.Enabled = false;
                this.btToggleAutoCentre.Enabled = false;
                this.gbMagCalib.Enabled = false;
                this.appTime.Stop();
            }
        }

        private void bFlash_Click(object sender, EventArgs e)
        {
            Thread.Sleep(250);
            if (this.cbPort.SelectedIndex < 0 || this.cbSketches.SelectedIndex < 0)
            {
                int num1 = (int)System.Windows.Forms.MessageBox.Show("Please select both a sketch and a valid COM Port.", "Sketch and COM Port Required to Flash", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (Form1.myState != Form1.myStateType.DISCONNECTED)
                    this.disconnectArduino();
                string port = this.cbPort.Text.Substring(0, this.cbPort.Text.IndexOf(' '));
                bool isBootloader = this.cbPort.Text.IndexOf("bootloader") >= 0;
                try
                {
                    string hexfile = Form1.hexfiles[this.cbSketches.SelectedIndex];
                    flashDialog flashDialog = new flashDialog(hexfile, port, isBootloader);
                    //flashDialog flashDialog = new flashDialog(IntelHexUtils.extractHexData(webClient.DownloadString(hexfile)), port, isBootloader);
                    flashDialog.StartPosition = FormStartPosition.CenterParent;
                    int num2 = (int)flashDialog.ShowDialog();
                    Thread.Sleep(200);
                    this.bMonitor_Click(sender, e);
                    this.biasHasChanged = true;
                }
                catch (Exception ex)
                {
                    int num3 = (int)System.Windows.Forms.MessageBox.Show("Unable to download sketch.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
        }

        private void bScanForTracker_Click(object sender, EventArgs e)
        {
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort serialPort = (SerialPort)sender;
            try
            {
                string indata = serialPort.ReadLine();
                this.BeginInvoke((Delegate)(() => this.processArduinoData(indata)));
            }
            catch (Exception ex)
            {
            }
        }

        private float getSlope()
        {
            int num1 = 20;
            float num2 = 0.0f;
            float num3 = 0.0f;
            float num4 = 0.0f;
            for (int index = 0; index < num1; ++index)
                num2 += (float)index;
            for (int index = 0; index < num1; ++index)
                num4 += (float)(index * index);
            for (int index = 0; index < num1; ++index)
                num3 += (float)this.tempHist[315 - index - 1].Y;
            float num5 = num2 / (float)num1;
            float num6 = num3 / (float)num1;
            float num7 = 0.0f;
            float num8 = 0.0f;
            float num9 = 0.0f;
            for (int index = 0; index < num1; ++index)
            {
                num7 += (float)(((double)index - (double)num5) * ((double)index - (double)num5));
                num8 += (float)(((double)this.tempHist[315 - index - 1].Y - (double)num6) * ((double)this.tempHist[315 - index - 1].Y - (double)num6));
                num9 += (float)(((double)index - (double)num5) * ((double)this.tempHist[315 - index - 1].Y - (double)num6));
            }
            return num9 / num7;
        }

        private void logMessage(string mess)
        {
            if (this.debugForm == null)
                return;
            this.debugForm.logMessage(mess.Replace('\t', '/'));
        }

        private System.Drawing.Color V2RGB(int v, int l, int h)
        {
            if (Math.Abs(v) < l)
                return System.Drawing.Color.LightGreen;
            return Math.Abs(v) > h ? System.Drawing.Color.Red : System.Drawing.Color.LightYellow;
        }

        private static object _lock = new object();
        private void processArduinoData(string datain)
        {
            lock (_lock)
            {
                if (datain.Length == 0)
                    return;
                char ch = datain[0];
                try
                {
                    switch (ch)
                    {
                        case 'H':
                            Form1.myState = Form1.myStateType.CONNECTED;
                            this.tbMonitor.Text = "CONNECTED";
                            this.ardPort.DiscardOutBuffer();
                            this.ardPort.Write("V");
                            Thread.Sleep(300);
                            this.ardPort.Write("I");
                            Thread.Sleep(100);
                            break;
                        case 'S':
                            break;
                        case 'V':
                            this.setMyState(Form1.myStateType.MONITORING);
                            break;
                        case 'a':
                            this.adjustX = this.adjustY = this.adjustZ = 1;
                            break;
                        case 'h':
                            Form1.myState = Form1.myStateType.CONNECTED;
                            this.tbMonitor.Text = "CONNECTED";
                            this.waitForCalibToFinish();
                            break;
                        case 'x':
                            this.adjustX = 1;
                            this.adjustY = this.adjustZ = 0;
                            break;
                        case 'y':
                            this.adjustY = 1;
                            this.adjustX = this.adjustZ = 0;
                            break;
                        case 'z':
                            this.adjustX = this.adjustY = 0;
                            this.adjustZ = 1;
                            break;
                        default:
                            string[] strArray = datain.Split('\t');
                            if (strArray[0].Equals("M"))
                            {
                                this.logMessage(strArray[1]);
                                break;
                            }
                            if (strArray[0].Equals("q"))
                            {
                                float.TryParse(strArray[1], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.magOffsets[0]);
                                float.TryParse(strArray[2], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.magOffsets[1]);
                                float.TryParse(strArray[3], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.magOffsets[2]);
                                double result;
                                double.TryParse(strArray[4], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[0, 0] = result;
                                double.TryParse(strArray[5], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[0, 1] = result;
                                double.TryParse(strArray[6], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[0, 2] = result;
                                double.TryParse(strArray[7], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[1, 0] = result;
                                double.TryParse(strArray[8], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[1, 1] = result;
                                double.TryParse(strArray[9], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[1, 2] = result;
                                double.TryParse(strArray[10], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[2, 0] = result;
                                double.TryParse(strArray[11], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[2, 1] = result;
                                double.TryParse(strArray[12], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out result);
                                this.rawMagCalibMatrix[2, 2] = result;
                                this.tbOffX.Text = strArray[1].Substring(0, 5);
                                this.tbOffY.Text = strArray[2].Substring(0, 5);
                                this.tbOffZ.Text = strArray[3].Substring(0, 5);
                                this.tbMagMat1.Text = strArray[4].Substring(0, 4);
                                this.tbMagMat2.Text = strArray[5].Substring(0, 4);
                                this.tbMagMat3.Text = strArray[6].Substring(0, 4);
                                this.tbMagMat4.Text = strArray[7].Substring(0, 4);
                                this.tbMagMat5.Text = strArray[8].Substring(0, 4);
                                this.tbMagMat6.Text = strArray[9].Substring(0, 4);
                                this.tbMagMat7.Text = strArray[10].Substring(0, 4);
                                this.tbMagMat8.Text = strArray[11].Substring(0, 4);
                                this.tbMagMat9.Text = strArray[12].Substring(0, 4);
                                this.logMessage("Raw Mag Calib Data " + datain);
                                break;
                            }
                            if (strArray[0].Equals("Q"))
                            {
                                this.magPoints[this.magSamples].X = (double)float.Parse(strArray[1]);
                                this.magPoints[this.magSamples].Y = (double)float.Parse(strArray[2]);
                                this.magPoints[this.magSamples].Z = (double)float.Parse(strArray[3]);
                                this.minMagX = Math.Min(this.minMagX, this.magPoints[this.magSamples].X);
                                this.maxMagX = Math.Max(this.maxMagX, this.magPoints[this.magSamples].X);
                                this.minMagY = Math.Min(this.minMagY, this.magPoints[this.magSamples].Y);
                                this.maxMagY = Math.Max(this.maxMagY, this.magPoints[this.magSamples].Y);
                                this.minMagZ = Math.Min(this.minMagZ, this.magPoints[this.magSamples].Z);
                                this.maxMagZ = Math.Max(this.maxMagZ, this.magPoints[this.magSamples].Z);
                                this.tbMinX.Text = this.minMagX.ToString();
                                this.tbMaxX.Text = this.maxMagX.ToString();
                                this.tbMinY.Text = this.minMagY.ToString();
                                this.tbMaxY.Text = this.maxMagY.ToString();
                                this.tbMinZ.Text = this.minMagZ.ToString();
                                this.tbMaxZ.Text = this.maxMagZ.ToString();
                                if (this.magSamplesHWM > 0)
                                {
                                    int index = this.magSamples - 1;
                                    if (index < 0)
                                        index = 1999;
                                    if (Vector3D.AngleBetween(new Vector3D(this.magPoints[this.magSamples].X, this.magPoints[this.magSamples].Y, this.magPoints[this.magSamples].Z), new Vector3D(this.magPoints[index].X, this.magPoints[index].Y, this.magPoints[index].Z)) > (double)this.magSensitivity)
                                        ++this.magSamples;
                                }
                                else
                                    ++this.magSamples;
                                if (this.magSamplesHWM >= 500)
                                    this.btSave.Enabled = true;
                                else
                                    this.btSave.Enabled = false;
                                if (this.magSamples >= 2000)
                                    this.magSamples = 0;
                                if (this.magSamples > this.magSamplesHWM)
                                    this.magSamplesHWM = this.magSamples;
                                this.tbMagSamples.Text = this.magSamplesHWM.ToString();
                                break;
                            }
                            if (strArray[0].Equals("s"))
                            {
                                int result = 0;
                                int.TryParse(strArray[1].Trim(), out result);
                                this.expScaleMode = result == 1;
                                float.TryParse(strArray[2], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.yawScale);
                                float.TryParse(strArray[3], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.pitchScale);
                                this.outputLPF = strArray.Length < 5 ? 0.0f : float.Parse(strArray[4], (IFormatProvider)CultureInfo.InvariantCulture) * 100f;
                                this.tbYawScaling.Text = this.yawScale.ToString("0.00");
                                this.tbPitchScaling.Text = this.pitchScale.ToString("0.00");
                                this.sliderSmoothing.Value = (int)Math.Min((double)this.outputLPF, 99.0);
                                this.tbSmoothing.Text = this.sliderSmoothing.Value.ToString();
                                if (this.expScaleMode)
                                    this.tbRespMode.Text = "Exponential";
                                else
                                    this.tbRespMode.Text = "Linear";
                                this.logMessage("Scale Mode/Values " + datain);
                                break;
                            }
                            if (strArray[0].Equals("p"))
                            {
                                this.pollMPU = int.Parse(strArray[1].Trim()) == 1;
                                if (this.pollMPU)
                                {
                                    this.tbSensorMode.Text = "Polling";
                                    break;
                                }
                                this.tbSensorMode.Text = "Interrupt";
                                break;
                            }
                            if (strArray[0].Equals("#"))
                            {
                                this.autocentre = int.Parse(strArray[1].Trim());
                                if (this.autocentre == 0)
                                    this.tbAutoCentre.Text = "Off";
                                else if (this.autocentre == 1)
                                    this.tbAutoCentre.Text = "Soft";
                                else if (this.autocentre == 2)
                                    this.tbAutoCentre.Text = "Medium";
                                else
                                    this.tbAutoCentre.Text = "Strong";
                                this.lastSelectAutoCentreOption = this.tbAutoCentre.Text;
                                break;
                            }
                            if (strArray[0].Equals("I"))
                            {
                                if (this.waitingForBias && this.fCalBias != null)
                                {
                                    this.fCalBias.stopProg();
                                    this.fCalBias.Hide();
                                }
                                this.waitingForBias = false;
                                this.info = strArray[1].Substring(0, strArray[1].Length - 1);
                                this.tbSketch.Text = this.info;
                                this.setButtonStates();
                                this.logMessage("Info " + datain);
                                break;
                            }
                            if (strArray[0].Equals("D"))
                            {
                                if (this.waitingForBias && this.fCalBias != null)
                                {
                                    this.fCalBias.stopProg();
                                    this.fCalBias.Hide();
                                }
                                this.waitingForBias = false;
                                float.TryParse(strArray[1], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.yawDrift);
                                float.TryParse(strArray[2], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.yawDriftComp);
                                for (int index = 0; index < 314; ++index)
                                    this.yawDriftHist[index].Y = this.yawDriftHist[index + 1].Y;
                                this.yawDriftHist[314].Y = (int)((double)this.yawDrift * -80.0) + 72;
                                this.tbYawDrift.Text = this.yawDrift.ToString("0.00");
                                if ((double)Math.Abs(this.yawDrift) < 0.15)
                                    this.tbYawDrift.BackColor = System.Drawing.Color.PaleGreen;
                                else if ((double)Math.Abs(this.yawDrift) > 0.5)
                                    this.tbYawDrift.BackColor = System.Drawing.Color.Red;
                                else
                                    this.tbYawDrift.BackColor = System.Drawing.Color.Yellow;
                                this.tbDriftComp.Text = this.yawDriftComp.ToString("0.00");
                                break;
                            }
                            if (strArray[0].Equals("T"))
                            {
                                if ((double)this.startTemp == 0.0)
                                {
                                    this.temperature = float.Parse(strArray[1]) / 65536f;
                                    this.startTemp = this.temperature;
                                    for (int index = 0; index < 314; ++index)
                                        this.tempHist[index].Y = (int)((double)this.startTemp * 1000.0);
                                }
                                else
                                    this.temperature = (float)((double)this.temperature * 0.93000000715255737 + 0.070000000298023224 * ((double)float.Parse(strArray[1]) / 65536.0));
                                for (int index = 0; index < 314; ++index)
                                    this.tempHist[index].Y = this.tempHist[index + 1].Y;
                                this.tempHist[314].Y = (int)((double)this.temperature * 1000.0);
                                for (int index = 0; index < 314; ++index)
                                    this.tDeltaHist[index].Y = this.tDeltaHist[index + 1].Y;
                                this.tempSlope = this.getSlope();
                                this.tDeltaHist[314].Y = (double)this.tempSlope < 0.0 ? (int)(Math.Sqrt((double)Math.Abs(this.tempSlope)) * -5.0) + 36 : (int)(Math.Sqrt((double)this.tempSlope) * 5.0) + 36;
                                this.tempSlope = Math.Abs(this.tempSlope);
                                if (Form1.myState != Form1.myStateType.MONITORING)
                                {
                                    this.tempStable = false;
                                    this.lbTemps.BackColor = System.Drawing.Color.Gray;
                                }
                                else if ((double)this.tempSlope < 0.8)
                                {
                                    this.tempStable = true;
                                    this.lbTemps.BackColor = System.Drawing.Color.PaleGreen;
                                }
                                else
                                {
                                    this.tempStable = false;
                                    this.lbTemps.BackColor = System.Drawing.Color.Pink;
                                }
                                this.tbTemps.Text = this.temperature.ToString("0.00");
                                break;
                            }
                            if (strArray[0].Equals("O"))
                            {
                                switch (int.Parse(strArray[1].Trim()))
                                {
                                    case 0:
                                        this.orientation = "Top/USB Right";
                                        this.orientationAngle = Math.PI;
                                        break;
                                    case 1:
                                        this.orientation = "Top/USB Front";
                                        this.orientationAngle = Math.PI / 2.0;
                                        break;
                                    case 2:
                                        this.orientation = "Top/USB Left";
                                        this.orientationAngle = 0.0;
                                        break;
                                    case 3:
                                        this.orientation = "Top/USB Rear";
                                        this.orientationAngle = -1.0 * Math.PI / 2.0;
                                        break;
                                    case 4:
                                        this.orientation = "Left/USB Down";
                                        break;
                                    default:
                                        this.orientation = "Right/USB Down";
                                        break;
                                }
                                this.tbOrientation.Text = this.orientation;
                                this.sendMagCalibData();
                                this.logMessage("Orientation " + datain);
                                break;
                            }
                            if (strArray[0].Equals("R"))
                            {
                                this.yawDrift = 0.0f;
                                this.pitchDrift = 0.0f;
                                break;
                            }
                            if (strArray[0].Equals("B"))
                            {
                                this.gBiasX = int.Parse(strArray[1].Trim());
                                this.gBiasY = int.Parse(strArray[2].Trim());
                                this.gBiasZ = int.Parse(strArray[3].Trim());
                                this.aBiasX = -1 * int.Parse(strArray[4].Trim());
                                this.aBiasY = -1 * int.Parse(strArray[5].Trim());
                                this.aBiasZ = -1 * int.Parse(strArray[6].Trim());
                                this.biasGX.Text = this.gBiasX.ToString();
                                this.biasGY.Text = this.gBiasY.ToString();
                                this.biasGZ.Text = this.gBiasZ.ToString();
                                this.biasAX.Text = this.aBiasX.ToString();
                                this.biasAY.Text = this.aBiasY.ToString();
                                this.biasAZ.Text = this.aBiasZ.ToString();
                                break;
                            }
                            try
                            {
                                if (strArray.Length < 6)
                                    break;
                                float.TryParse(strArray[0], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.DMPYaw);
                                float.TryParse(strArray[1], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.DMPPitch);
                                float.TryParse(strArray[2], NumberStyles.Any, (IFormatProvider)CultureInfo.InvariantCulture, out this.DMPRoll);
                                this.DMPYaw /= 10430.06f;
                                this.DMPPitch /= -10430.06f;
                                this.DMPRoll /= -10430.06f;
                                this.rawAccelX = (int)this.lpX.filter(float.Parse(strArray[3]) / 10f);
                                this.rawAccelY = (int)this.lpY.filter(float.Parse(strArray[4]) / 10f);
                                this.rawAccelZ = (int)this.lpZ.filter((float)(((double)float.Parse(strArray[5]) - 16383.0) / 10.0));
                                float result;
                                if (float.TryParse(strArray[6], out result))
                                {
                                    result = this.gX.filter(result);
                                    this.rawGyroX = (int)((double)result * 3.0);
                                }
                                float num1 = this.gY.filter(float.Parse(strArray[7]));
                                this.rawGyroY = (int)((double)num1 * 3.0);
                                float num2 = this.gZ.filter(float.Parse(strArray[8]));
                                this.rawGyroZ = (int)((double)num2 * 3.0);
                                this.udGX.Text = this.rawGyroX.ToString();
                                this.udGY.Text = this.rawGyroY.ToString();
                                this.udGZ.Text = this.rawGyroZ.ToString();
                                this.tbGyroX.Text = (this.rawGyroX / 2).ToString();
                                this.tbGyroY.Text = (this.rawGyroY / 2).ToString();
                                this.tbGyroZ.Text = (this.rawGyroZ / 2).ToString();
                                this.udAX.Text = this.rawAccelX.ToString();
                                this.udAY.Text = this.rawAccelY.ToString();
                                this.udAZ.Text = this.rawAccelZ.ToString();
                                this.biasGX.BackColor = this.V2RGB(this.rawGyroX, 2, 5);
                                this.biasGY.BackColor = this.V2RGB(this.rawGyroY, 2, 5);
                                this.biasGZ.BackColor = this.V2RGB(this.rawGyroZ, 2, 5);
                                this.tbGyroX.BackColor = this.V2RGB(this.rawGyroX, 4, 10);
                                this.tbGyroY.BackColor = this.V2RGB(this.rawGyroY, 4, 10);
                                this.tbGyroZ.BackColor = this.V2RGB(this.rawGyroZ, 4, 10);
                                this.biasAX.BackColor = this.V2RGB(this.rawAccelX, 4, 10);
                                this.biasAY.BackColor = this.V2RGB(this.rawAccelY, 4, 10);
                                this.biasAZ.BackColor = this.V2RGB(this.rawAccelZ, 4, 10);
                                this.edtracker1.rotateHead(this.DMPYaw * this.rad2deg, (float)((double)this.DMPPitch * (double)this.rad2deg - 90.0));
                                for (int index = 0; index < 314; ++index)
                                {
                                    this.pitchHist[index].Y = this.pitchHist[index + 1].Y;
                                    this.yawHist[index].Y = this.yawHist[index + 1].Y;
                                    this.gyroXHist[index].Y = this.gyroXHist[index + 1].Y;
                                    this.gyroYHist[index].Y = this.gyroYHist[index + 1].Y;
                                    this.gyroZHist[index].Y = this.gyroZHist[index + 1].Y;
                                }
                                this.pitchHist[314].Y = (int)((double)this.DMPPitch * 10.0) + 144;
                                this.yawHist[314].Y = (int)((double)this.DMPYaw * 100.0) + 108;
                                this.gyroXHist[314].Y = (int)((double)result * -20.0) + 72;
                                this.gyroYHist[314].Y = (int)((double)num1 * -20.0) + 108;
                                this.gyroZHist[314].Y = (int)((double)num2 * -20.0) + 144;
                                break;
                            }
                            catch (Exception ex)
                            {
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void disconnectArduino()
        {
            try
            {
                this.ardPort.Write("S");
                Thread.Sleep(200);
                this.ardPort.Close();
                Thread.Sleep(1000);
                this.ardPort.Dispose();
            }
            catch (Exception ex)
            {
            }
            this.setMyState(Form1.myStateType.DISCONNECTED);
        }

        private void bMonitor_Click(object sender, EventArgs e)
        {
            if (Form1.myState == Form1.myStateType.MONITORING || Form1.myState == Form1.myStateType.CONNECTED)
            {
                if (Form1.myState == Form1.myStateType.MONITORING)
                {
                    try
                    {
                        this.startTemp = 0.0f;
                        this.ardPort.Write("S");
                        Thread.Sleep(200);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                if (this.ardPort != null)
                {
                    if (this.ardPort.IsOpen)
                    {
                        this.ardPort.DiscardInBuffer();
                        this.ardPort.DiscardOutBuffer();
                        try
                        {
                            Thread.Sleep(200);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    try
                    {
                        this.ardPort.Dispose();
                    }
                    catch (Exception ex)
                    {
                    }
                }
                this.setMyState(Form1.myStateType.DISCONNECTED);
            }
            else
            {
                int length = this.cbPort.Text.IndexOf(' ');
                if (length < 0)
                    return;
                this.ardPort = new SerialPort(this.cbPort.Text.Substring(0, length));
                this.ardPort.BaudRate = 115200;
                this.ardPort.DataBits = 8;
                this.ardPort.ReceivedBytesThreshold = 2;
                this.ardPort.DtrEnable = true;
                this.ardPort.DataReceived += new SerialDataReceivedEventHandler(this.DataReceivedHandler);
                this.ardPort.ReadTimeout = 100;
                this.ardPort.WriteTimeout = 200;
                try
                {
                    this.ardPort.Open();
                    this.setMyState(Form1.myStateType.CONNECTED);
                    Thread.Sleep(100);
                    try
                    {
                        this.ardPort.Write("H");
                    }
                    catch (Exception ex)
                    {
                    }
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                    this.logMessage(ex.Message);
                }
            }
        }

        private void bCalcBias_Click(object sender, EventArgs e)
        {
            if (!this.tempStable && System.Windows.Forms.MessageBox.Show("Tracker temperature is not yet stable.\nDo you wish to continue with the bias calc anyway?", "Tracker Not Warmed Up", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            this.ardPort.Write("B\n");
            this.biasValuesRecalced = true;
            this.biasHasChanged = true;
            if (this.fCalBias == null)
                this.fCalBias = new frmBiasCalc();
            this.fCalBias.Text = "Calculating Gyro Bias Values";
            this.waitingForBias = true;
            this.fCalBias.StartPosition = FormStartPosition.CenterParent;
            int num = (int)this.fCalBias.ShowDialog();
        }

        private void bRespMode_Click(object sender, EventArgs e) => this.ardPort.Write("t\n");

        private void bSensorMode_Click(object sender, EventArgs e) => this.ardPort.Write("p\n");

        private void bGraphScale_Click(object sender, EventArgs e)
        {
            this.driftScale *= 2f;
            if ((double)this.driftScale <= 2000.0)
                return;
            this.driftScale = 1f;
        }

        private void bWipeAll_Click(object sender, EventArgs e)
        {
            if (this.info.Contains("Calib"))
            {
                this.ardPort.Write("0\n");
            }
            else
            {
                if (System.Windows.Forms.MessageBox.Show("This will wipe all current settings and magnetic calibration data. Do you still wish to continue ?", "Wipe Settings", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    return;
                byte[] buffer = new byte[88]
                {
          (byte) 87,
          (byte) 0,
          (byte) 1,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 2,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 20,
          (byte) 20,
          (byte) 64,
          (byte) 64,
          (byte) 0,
          (byte) 2,
          (byte) 0,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          byte.MaxValue,
          (byte) 0,
          (byte) 32,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 32,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 32,
          (byte) 0,
          (byte) 32,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 32,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 0,
          (byte) 32
                };
                buffer[1] = Convert.ToByte(buffer.Length - 2);
                this.ardPort.Write(buffer, 0, buffer.Length);
                this.magDataValid = true;
            }
            Thread.Sleep(200);
            this.ardPort.Write("I\n");
        }

        private void gxplus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("M\n");
            this.biasHasChanged = true;
        }

        private void gxminus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("m\n");
            this.biasHasChanged = true;
        }

        private void gyplus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("N\n");
            this.biasHasChanged = true;
        }

        private void gyminus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("n\n");
            this.biasHasChanged = true;
        }

        private void gzplus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("O\n");
            this.biasHasChanged = true;
        }

        private void gzminus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("o\n");
            this.biasHasChanged = true;
        }

        private void axplus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("j\n");
            this.biasHasChanged = true;
        }

        private void axminus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("J\n");
            this.biasHasChanged = true;
        }

        private void ayplus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("k\n");
            this.biasHasChanged = true;
        }

        private void ayminus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("K\n");
            this.biasHasChanged = true;
        }

        private void azplus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("l\n");
            this.biasHasChanged = true;
        }

        private void azminus_Click_1(object sender, EventArgs e)
        {
            this.ardPort.Write("L\n");
            this.biasHasChanged = true;
        }

        private void bResetView_Click(object sender, EventArgs e)
        {
            this.ardPort.Write("R\n");
            this.biasHasChanged = false;
            this.appTime.Restart();
            Thread.Sleep(100);
        }

        private void bScanPorts_Click(object sender, EventArgs e)
        {
            if (this.portThreadRunning)
                return;
            this.portThreadRunning = true;
            this.scanForPortsWorker.RunWorkerAsync();
        }

        private void bSaveDriftComp_Click(object sender, EventArgs e)
        {
            if (!this.tempStable && System.Windows.Forms.MessageBox.Show("Tracker temperature is not yet stable.\nDo you wish to continue and save the drift compensation anyway?", "Tracker Not Warmed Up", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            this.biasHasChanged = false;
            this.ardPort.Write("D\n");
            Thread.Sleep(200);
            this.ardPort.Write("R\n");
            this.appTime.Restart();
        }

        private void lbYawDrift_Click(object sender, EventArgs e)
        {
        }

        private void tbYawDrift_TextChanged(object sender, EventArgs e)
        {
        }

        private void bOrientate_Click(object sender, EventArgs e)
        {
            this.ardPort.Write("P\n");
            this.magDataValid = true;
        }

        private void tbOrientation_TextChanged(object sender, EventArgs e)
        {
        }

        private void btCompUp_Click(object sender, EventArgs e) => this.ardPort.Write("A\n");

        private void btCompDown_Click(object sender, EventArgs e) => this.ardPort.Write("a\n");

        private void sendResponseValues()
        {
            byte[] buffer = new byte[8];
            int num1 = 0;
            byte[] numArray1 = buffer;
            int index1 = num1;
            int num2 = index1 + 1;
            numArray1[index1] = (byte)67;
            byte[] bytes1 = BitConverter.GetBytes((short)((double)this.yawScale * 256.0));
            byte[] numArray2 = buffer;
            int index2 = num2;
            int num3 = index2 + 1;
            int num4 = (int)bytes1[0];
            numArray2[index2] = (byte)num4;
            byte[] numArray3 = buffer;
            int index3 = num3;
            int num5 = index3 + 1;
            int num6 = (int)bytes1[1];
            numArray3[index3] = (byte)num6;
            byte[] bytes2 = BitConverter.GetBytes((short)((double)this.pitchScale * 256.0));
            byte[] numArray4 = buffer;
            int index4 = num5;
            int num7 = index4 + 1;
            int num8 = (int)bytes2[0];
            numArray4[index4] = (byte)num8;
            byte[] numArray5 = buffer;
            int index5 = num7;
            int num9 = index5 + 1;
            int num10 = (int)bytes2[1];
            numArray5[index5] = (byte)num10;
            byte[] bytes3 = BitConverter.GetBytes((short)((double)this.outputLPF * 327.67));
            byte[] numArray6 = buffer;
            int index6 = num9;
            int num11 = index6 + 1;
            int num12 = (int)bytes3[0];
            numArray6[index6] = (byte)num12;
            byte[] numArray7 = buffer;
            int index7 = num11;
            int num13 = index7 + 1;
            int num14 = (int)bytes3[1];
            numArray7[index7] = (byte)num14;
            byte[] numArray8 = buffer;
            int index8 = num13;
            int num15 = index8 + 1;
            numArray8[index8] = (byte)10;
            this.ardPort.Write(buffer, 0, 8);
        }

        private void btYawScaleUp_Click(object sender, EventArgs e)
        {
            if (this.info.Contains("TrackerMag") || this.info.Contains("Pro"))
            {
                if (this.cbFineAdjust.Checked)
                    this.yawScale += 0.25f;
                else
                    ++this.yawScale;
                this.sendResponseValues();
            }
            else if (this.cbFineAdjust.Checked)
                this.ardPort.Write("c\n");
            else
                this.ardPort.Write("C\n");
        }

        private void btYawScaleDown_Click(object sender, EventArgs e)
        {
            if (this.info.Contains("TrackerMag"))
            {
                if (this.cbFineAdjust.Checked)
                    this.yawScale -= 0.25f;
                else
                    --this.yawScale;
                this.sendResponseValues();
            }
            else if (this.cbFineAdjust.Checked)
                this.ardPort.Write("d\n");
            else
                this.ardPort.Write("G\n");
        }

        private void btPitchScaleUp_Click(object sender, EventArgs e)
        {
            if (this.info.Contains("TrackerMag"))
            {
                if (this.cbFineAdjust.Checked)
                    this.pitchScale += 0.25f;
                else
                    ++this.pitchScale;
                this.sendResponseValues();
            }
            else if (this.cbFineAdjust.Checked)
                this.ardPort.Write("e\n");
            else
                this.ardPort.Write("E\n");
        }

        private void btPitchScaleDown_Click(object sender, EventArgs e)
        {
            if (this.info.Contains("TrackerMag"))
            {
                if (this.cbFineAdjust.Checked)
                    this.pitchScale -= 0.25f;
                else
                    --this.pitchScale;
                this.sendResponseValues();
            }
            else if (this.cbFineAdjust.Checked)
                this.ardPort.Write("f\n");
            else
                this.ardPort.Write("F\n");
        }

        private void cbHKey_SelectedIndexChanged(object sender, EventArgs e) => this.setHotKey();

        private void cbHotKey_CheckedChanged(object sender, EventArgs e) => this.setHotKey();

        private void setHotKey()
        {
            if (this.hotkey != null)
                this.hotkey.Unregister();
            if (!this.cbHotKey.Checked)
                return;
            if (this.cbHKey.SelectedIndex < 0)
            {
                int num = (int)System.Windows.Forms.MessageBox.Show("Invalid Hotkey entered", "Invalid HotKey", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                this.cbHotKey.Checked = false;
            }
            else
            {
                Keys hotKey = this.hotKeyList[this.cbHKey.Text];
                Modifiers modifier = Modifiers.NoMod;
                try
                {
                    this.hotkey = new GlobalHotkey(modifier, hotKey, (IWin32Window)this, true);
                    RegistryKey registryKey = this.rkCurrentUser.OpenSubKey("Software\\EDTracker", true);
                    registryKey.SetValue("HOTKEY", (object)this.cbHKey.SelectedIndex);
                    registryKey.Close();
                }
                catch (GlobalHotkeyException ex)
                {
                    int num = (int)System.Windows.Forms.MessageBox.Show(ex.Message);
                    this.cbHotKey.Checked = false;
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (HotkeyInfo.GetFromMessage(m) != null)
            {
                if (Form1.myState != Form1.myStateType.CONNECTED)
                {
                    if (Form1.myState != Form1.myStateType.MONITORING)
                        goto label_5;
                }
                try
                {
                    this.ardPort.Write("R\n");
                }
                catch (Exception ex)
                {
                }
            }
        label_5:
            base.WndProc(ref m);
        }

        private void close_clicked(object Sender, EventArgs e) => this.Close();

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == this.WindowState)
            {
                this.preMinimiseState = Form1.myState;
                if (Form1.myState == Form1.myStateType.MONITORING)
                {
                    try
                    {
                        this.ardPort.Write("S");
                        Thread.Sleep(100);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.ShowBalloonTip(500);
                this.Hide();
            }
            else
            {
                if (this.WindowState != FormWindowState.Normal)
                    return;
                this.notifyIcon1.Visible = false;
                if (this.preMinimiseState != Form1.myStateType.MONITORING)
                    return;
                try
                {
                    this.ardPort.Write("V");
                    Thread.Sleep(100);
                }
                catch (Exception ex)
                {
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.tbTime.Text = new DateTime(this.appTime.Elapsed.Ticks).ToString("mm:ss");
        }

        private void Help_Click(object sender, EventArgs e)
        {
            Process.Start("http://edtracker.org.uk/index.php/faq");
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.debugForm == null)
                this.debugForm = new frmDebug();
            else if (this.debugForm.IsDisposed)
                this.debugForm = new frmDebug();
            this.debugForm.Show();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.debugForm == null || this.debugForm.IsDisposed)
                return;
            this.debugForm.Hide();
        }

        private void btToggleAutoCentre_Click(object sender, EventArgs e) => this.ardPort.Write("#\n");

        private void btEnableAC_Click(object sender, EventArgs e)
        {
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            this.magCloud1.doItToIt(this.magPoints, this.magSamplesHWM, this.magOffsets, this.rawMagCalibMatrix);
            this.tbMagMat1.Text = this.rawMagCalibMatrix[0, 0].ToString();
            this.tbMagMat2.Text = this.rawMagCalibMatrix[0, 1].ToString();
            this.tbMagMat3.Text = this.rawMagCalibMatrix[0, 2].ToString();
            this.tbMagMat4.Text = this.rawMagCalibMatrix[1, 0].ToString();
            this.tbMagMat5.Text = this.rawMagCalibMatrix[1, 1].ToString();
            this.tbMagMat6.Text = this.rawMagCalibMatrix[1, 2].ToString();
            this.tbMagMat7.Text = this.rawMagCalibMatrix[2, 0].ToString();
            this.tbMagMat8.Text = this.rawMagCalibMatrix[2, 1].ToString();
            this.tbMagMat9.Text = this.rawMagCalibMatrix[2, 2].ToString();
            this.magDataValid = true;
            this.sendMagCalibData();
            this.tbarMagSens.Value = 0;
            this.magSensitivity = (float)this.tbarMagSens.Maximum;
        }

        private void sendMagCalibData()
        {
            if (!this.magDataValid)
                return;
            Matrix<double> matrix = this.rawMagCalibMatrix * Matrix<double>.Build.DenseOfColumnArrays(new double[3]
            {
        Math.Cos(this.orientationAngle),
        Math.Sin(this.orientationAngle),
        0.0
            }, new double[3]
            {
        -Math.Sin(this.orientationAngle),
        Math.Cos(this.orientationAngle),
        0.0
            }, new double[3] { 0.0, 0.0, 1.0 });
            short[] numArray1 = new short[3];
            short[] numArray2 = new short[9];
            short[] numArray3 = new short[9];
            numArray2[0] = (short)(matrix[0, 0] * 8192.0);
            numArray2[1] = (short)(matrix[0, 1] * 8192.0);
            numArray2[2] = (short)(matrix[0, 2] * 8192.0);
            numArray2[3] = (short)(matrix[1, 0] * 8192.0);
            numArray2[4] = (short)(matrix[1, 1] * 8192.0);
            numArray2[5] = (short)(matrix[1, 2] * 8192.0);
            numArray2[6] = (short)(matrix[2, 0] * 8192.0);
            numArray2[7] = (short)(matrix[2, 1] * 8192.0);
            numArray2[8] = (short)(matrix[2, 2] * 8192.0);
            numArray3[0] = (short)(this.rawMagCalibMatrix[0, 0] * 8192.0);
            numArray3[1] = (short)(this.rawMagCalibMatrix[0, 1] * 8192.0);
            numArray3[2] = (short)(this.rawMagCalibMatrix[0, 2] * 8192.0);
            numArray3[3] = (short)(this.rawMagCalibMatrix[1, 0] * 8192.0);
            numArray3[4] = (short)(this.rawMagCalibMatrix[1, 1] * 8192.0);
            numArray3[5] = (short)(this.rawMagCalibMatrix[1, 2] * 8192.0);
            numArray3[6] = (short)(this.rawMagCalibMatrix[2, 0] * 8192.0);
            numArray3[7] = (short)(this.rawMagCalibMatrix[2, 1] * 8192.0);
            numArray3[8] = (short)(this.rawMagCalibMatrix[2, 2] * 8192.0);
            for (int index = 0; index < 3; ++index)
                numArray1[index] = (short)((double)this.magOffsets[index] * 64.0);
            byte[] buffer = new byte[44];
            int num1 = 0;
            byte[] numArray4 = buffer;
            int index1 = num1;
            int num2 = index1 + 1;
            numArray4[index1] = (byte)36;
            for (int index2 = 0; index2 < 3; ++index2)
            {
                byte[] bytes = BitConverter.GetBytes(numArray1[index2]);
                byte[] numArray5 = buffer;
                int index3 = num2;
                int num3 = index3 + 1;
                int num4 = (int)bytes[0];
                numArray5[index3] = (byte)num4;
                byte[] numArray6 = buffer;
                int index4 = num3;
                num2 = index4 + 1;
                int num5 = (int)bytes[1];
                numArray6[index4] = (byte)num5;
            }
            for (int index5 = 0; index5 < 9; ++index5)
            {
                byte[] bytes = BitConverter.GetBytes(numArray2[index5]);
                byte[] numArray7 = buffer;
                int index6 = num2;
                int num6 = index6 + 1;
                int num7 = (int)bytes[0];
                numArray7[index6] = (byte)num7;
                byte[] numArray8 = buffer;
                int index7 = num6;
                num2 = index7 + 1;
                int num8 = (int)bytes[1];
                numArray8[index7] = (byte)num8;
            }
            for (int index8 = 0; index8 < 9; ++index8)
            {
                byte[] bytes = BitConverter.GetBytes(numArray3[index8]);
                byte[] numArray9 = buffer;
                int index9 = num2;
                int num9 = index9 + 1;
                int num10 = (int)bytes[0];
                numArray9[index9] = (byte)num10;
                byte[] numArray10 = buffer;
                int index10 = num9;
                num2 = index10 + 1;
                int num11 = (int)bytes[1];
                numArray10[index10] = (byte)num11;
            }
            byte[] numArray11 = buffer;
            int index11 = num2;
            int num12 = index11 + 1;
            numArray11[index11] = (byte)10;
            this.ardPort.Write(buffer, 0, 44);
        }

        private void elementHost2_ChildChanged(object sender, ChildChangedEventArgs e)
        {
        }

        private void label16_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.magSamples = 0;
            this.magSamplesHWM = 0;
            this.minMagX = this.minMagY = this.minMagZ = 999.0;
            this.maxMagX = this.maxMagY = this.maxMagZ = -999.0;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.outputLPF = (float)this.sliderSmoothing.Value;
            this.tbSmoothing.Text = this.sliderSmoothing.Value.ToString();
        }

        private void trackBar1_MUP(object sender, EventArgs e) => this.sendResponseValues();

        private void tabPage2_Enter(object sender, EventArgs e) => this.magCloud1.Show();

        private void tabPage1_Enter(object sender, EventArgs e) => this.magCloud1.Hide();

        private void trackBar1_Scroll_1(object sender, EventArgs e)
        {
            this.magSensitivity = (float)(this.tbarMagSens.Maximum - this.tbarMagSens.Value);
        }

        private void btMagResetView_Click(object sender, EventArgs e)
        {
            this.ardPort.Write("R\n");
            this.biasHasChanged = false;
            this.appTime.Restart();
            Thread.Sleep(100);
        }

        private void waitForCalibToFinish()
        {
            if (this.fCalBias == null)
                this.fCalBias = new frmBiasCalc();
            this.waitingForBias = true;
            this.fCalBias.Text = "Calculating Gyro Bias. Keep EDTracker Stationary.";
            this.fCalBias.StartPosition = FormStartPosition.CenterParent;
            this.ardPort.Write("r\n");
            this.fCalBias.Show();
        }

        private void btMagAutoBias_Click(object sender, EventArgs e) => this.waitForCalibToFinish();

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new AboutBox1().Show();

        private void uSERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!(this.userMode != "USER"))
                return;
            this.setUserMode("USER");
        }

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = (int)System.Windows.Forms.MessageBox.Show("By setting 'TEST' mode you may now have access to test firmware which may not function correctly and which the EDTracker team cannot support.", "TEST Mode Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (!(this.userMode != "TEST"))
                return;
            this.setUserMode("TEST");
        }

        private void dEVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = (int)System.Windows.Forms.MessageBox.Show("By setting 'DEV' mode you may have access to features and firmware that may not function correctly and which are not supported by the EDTracker team.", "DEV Mode Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            if (!(this.userMode != "DEV"))
                return;
            this.setUserMode("DEV");
        }

        private void setUserMode(string mode)
        {
            this.userMode = mode;
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.DownloadStringCompleted);
            webClient.DownloadStringAsync(new Uri(this.edtUpdateURL));
            RegistryKey registryKey = this.rkCurrentUser.OpenSubKey("Software\\EDTracker", true);
            registryKey.SetValue("USERMODE", (object)mode);
            registryKey.Close();
        }

        private void label12_Click(object sender, EventArgs e)
        {
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

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
            Name = "Form1";
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

        public class DoubleBufferedPanel : Panel
        {
            public DoubleBufferedPanel() => this.DoubleBuffered = true;
        }

        private enum myStateType
        {
            UNKNOWN,
            DISCONNECTED,
            CONNECTED,
            MONITORING,
        }
    }
}
