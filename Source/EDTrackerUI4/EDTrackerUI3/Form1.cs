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
using System.Management;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media.Media3D;
using System.Xml;
using System.Xml.Serialization;
using TestFetchXMLandHex;

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
            foreach (object portName in SerialPort.GetPortNames())
                this.cbPort.Items.Add(portName);
        }

        private void scanForPortsWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT * FROM WIN32_SerialPort"))
            {
                List<string> list = ((IEnumerable<string>)SerialPort.GetPortNames()).Join<string, ManagementBaseObject, string, string>((IEnumerable<ManagementBaseObject>)managementObjectSearcher.Get().Cast<ManagementBaseObject>().ToList<ManagementBaseObject>(), (Func<string, string>)(n => n), (Func<ManagementBaseObject, string>)(p => p["DeviceID"].ToString()), (Func<string, ManagementBaseObject, string>)((n, p) => n + " - " + p["Caption"])).ToList<string>();
                e.Result = (object)list;
            }
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
                WebClient webClient = new WebClient();
                try
                {
                    string hexfile = Form1.hexfiles[this.cbSketches.SelectedIndex];
                    flashDialog flashDialog = new flashDialog(IntelHexUtils.extractHexData(webClient.DownloadString(hexfile)), port, isBootloader);
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

        private void processArduinoData(string datain)
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
            this.components = (IContainer)new System.ComponentModel.Container();
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form1));
            this.bWipeAll = new Button();
            this.cbSketches = new ComboBox();
            this.cbPort = new ComboBox();
            this.bFlash = new Button();
            this.groupBox1 = new GroupBox();
            this.bScanPorts = new Button();
            this.bMonitor = new Button();
            this.gbBias = new GroupBox();
            this.biasAX = new TextBox();
            this.axminus = new Button();
            this.axplus = new Button();
            this.udAX = new TextBox();
            this.label5 = new Label();
            this.biasAY = new TextBox();
            this.ayminus = new Button();
            this.ayplus = new Button();
            this.udAY = new TextBox();
            this.label6 = new Label();
            this.biasAZ = new TextBox();
            this.azminus = new Button();
            this.azplus = new Button();
            this.udAZ = new TextBox();
            this.label7 = new Label();
            this.biasGZ = new TextBox();
            this.gzminus = new Button();
            this.gzplus = new Button();
            this.udGZ = new TextBox();
            this.label4 = new Label();
            this.biasGY = new TextBox();
            this.gyminus = new Button();
            this.gyplus = new Button();
            this.udGY = new TextBox();
            this.label3 = new Label();
            this.biasGX = new TextBox();
            this.gxminus = new Button();
            this.udGX = new TextBox();
            this.gxplus = new Button();
            this.label2 = new Label();
            this.bCalcBias = new Button();
            this.gbHotKey = new GroupBox();
            this.btEnableAC = new Button();
            this.tbAutoCentre = new TextBox();
            this.btToggleAutoCentre = new Button();
            this.cbHKey = new ComboBox();
            this.cbCOntrollerButtons = new ComboBox();
            this.cbControllerButton = new CheckBox();
            this.cbHotKey = new CheckBox();
            this.gbTrackerConfig = new GroupBox();
            this.bOrientate = new Button();
            this.tbOrientation = new TextBox();
            this.bSensorMode = new Button();
            this.tbSensorMode = new TextBox();
            this.tbSketch = new TextBox();
            this.gbDriftComp = new GroupBox();
            this.lbTimer = new Label();
            this.tbTime = new TextBox();
            this.bSaveDriftComp = new Button();
            this.bResetView = new Button();
            this.tbDriftComp = new TextBox();
            this.lbComp = new Label();
            this.tbYawDrift = new TextBox();
            this.lbYawDrift = new Label();
            this.btCompDown = new Button();
            this.btCompUp = new Button();
            this.gbScaling = new GroupBox();
            this.tbSmoothing = new TextBox();
            this.lbSmooth = new Label();
            this.sliderSmoothing = new TrackBar();
            this.tbRespMode = new TextBox();
            this.bRespMode = new Button();
            this.tbPitchScaling = new TextBox();
            this.label11 = new Label();
            this.tbYawScaling = new TextBox();
            this.label10 = new Label();
            this.cbFineAdjust = new CheckBox();
            this.btYawScaleDown = new Button();
            this.btPitchScaleDown = new Button();
            this.btPitchScaleUp = new Button();
            this.btYawScaleUp = new Button();
            this.tbTemps = new TextBox();
            this.lbTemps = new Label();
            this.tbMonitor = new TextBox();
            this.notifyIcon1 = new NotifyIcon(this.components);
            this.lbAxis4 = new Label();
            this.lbAxis3 = new Label();
            this.lbAxis2 = new Label();
            this.lbAxis1 = new Label();
            this.menuStrip1 = new MenuStrip();
            this.toolStripMenuItem1 = new ToolStripMenuItem();
            this.aboutToolStripMenuItem = new ToolStripMenuItem();
            this.modeToolStripMenuItem = new ToolStripMenuItem();
            this.uSERToolStripMenuItem = new ToolStripMenuItem();
            this.tESTToolStripMenuItem = new ToolStripMenuItem();
            this.dEVToolStripMenuItem = new ToolStripMenuItem();
            this.Help = new ToolStripMenuItem();
            this.debugToolStripMenuItem = new ToolStripMenuItem();
            this.showToolStripMenuItem = new ToolStripMenuItem();
            this.hideToolStripMenuItem = new ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbMagCalib = new GroupBox();
            this.label1 = new Label();
            this.tbOffZ = new TextBox();
            this.tbOffY = new TextBox();
            this.tbOffX = new TextBox();
            this.label18 = new Label();
            this.tbarMagSens = new TrackBar();
            this.button1 = new Button();
            this.label16 = new Label();
            this.tbMagSamples = new TextBox();
            this.tbMinX = new TextBox();
            this.label9 = new Label();
            this.label12 = new Label();
            this.label13 = new Label();
            this.label14 = new Label();
            this.label15 = new Label();
            this.tbMaxZ = new TextBox();
            this.tbMinZ = new TextBox();
            this.tbMaxY = new TextBox();
            this.tbMinY = new TextBox();
            this.tbMaxX = new TextBox();
            this.btSave = new Button();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.elementHost1 = new ElementHost();
            this.edtracker1 = new EDTrackerWpfControls.HeadTracker();
            this.tabPage2 = new TabPage();
            this.groupBox2 = new GroupBox();
            this.tbMagMat9 = new TextBox();
            this.tbMagMat8 = new TextBox();
            this.tbMagMat7 = new TextBox();
            this.tbMagMat6 = new TextBox();
            this.tbMagMat5 = new TextBox();
            this.tbMagMat4 = new TextBox();
            this.tbMagMat1 = new TextBox();
            this.tbMagMat2 = new TextBox();
            this.tbMagMat3 = new TextBox();
            this.elementHost2 = new ElementHost();
            this.magCloud1 = new EDTrackerWpfControls.MagCloud();
            this.gbMagGyro = new GroupBox();
            this.btMagResetView = new Button();
            this.btMagAutoBias = new Button();
            this.tbGyroX = new TextBox();
            this.lbGX = new Label();
            this.tbGyroZ = new TextBox();
            this.tbGyroY = new TextBox();
            this.groupBox1.SuspendLayout();
            this.gbBias.SuspendLayout();
            this.gbHotKey.SuspendLayout();
            this.gbTrackerConfig.SuspendLayout();
            this.gbDriftComp.SuspendLayout();
            this.gbScaling.SuspendLayout();
            this.sliderSmoothing.BeginInit();
            this.menuStrip1.SuspendLayout();
            this.gbMagCalib.SuspendLayout();
            this.tbarMagSens.BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.gbMagGyro.SuspendLayout();
            this.SuspendLayout();
            this.bWipeAll.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.bWipeAll.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonHighlight;
            this.bWipeAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bWipeAll.ForeColor = System.Drawing.Color.Black;
            this.bWipeAll.Location = new System.Drawing.Point(17, 627);
            this.bWipeAll.Margin = new Padding(2, 3, 2, 3);
            this.bWipeAll.Name = "bWipeAll";
            this.bWipeAll.Size = new System.Drawing.Size(173, 23);
            this.bWipeAll.TabIndex = 9;
            this.bWipeAll.Text = "Restore Factory Defaults";
            this.bWipeAll.UseVisualStyleBackColor = true;
            this.bWipeAll.Click += new EventHandler(this.bWipeAll_Click);
            this.cbSketches.BackColor = System.Drawing.SystemColors.Window;
            this.cbSketches.DisplayMember = "0";
            this.cbSketches.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbSketches.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.cbSketches.Items.AddRange(new object[1]
            {
            (object) "Select Sketch..."
            });
            this.cbSketches.Location = new System.Drawing.Point(6, 19);
            this.cbSketches.Name = "cbSketches";
            this.cbSketches.Size = new System.Drawing.Size(271, 23);
            this.cbSketches.TabIndex = 53;
            this.cbSketches.SelectedIndexChanged += new EventHandler(this.cbSketches_SelectedIndexChanged);
            this.cbPort.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbPort.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.cbPort.Items.AddRange(new object[1]
            {
            (object) "Retrieving COM Ports..."
            });
            this.cbPort.Location = new System.Drawing.Point(6, 47);
            this.cbPort.Name = "cbPort";
            this.cbPort.Size = new System.Drawing.Size(271, 23);
            this.cbPort.TabIndex = 55;
            this.bFlash.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bFlash.Enabled = false;
            this.bFlash.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bFlash.FlatAppearance.BorderSize = 2;
            this.bFlash.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bFlash.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.bFlash.Location = new System.Drawing.Point(6, 75);
            this.bFlash.Margin = new Padding(2, 3, 2, 3);
            this.bFlash.Name = "bFlash";
            this.bFlash.Size = new System.Drawing.Size(172, 23);
            this.bFlash.TabIndex = 57;
            this.bFlash.Text = "Flash";
            this.bFlash.UseVisualStyleBackColor = true;
            this.bFlash.Click += new EventHandler(this.bFlash_Click);
            this.groupBox1.Controls.Add((Control)this.bScanPorts);
            this.groupBox1.Controls.Add((Control)this.cbSketches);
            this.groupBox1.Controls.Add((Control)this.bFlash);
            this.groupBox1.Controls.Add((Control)this.cbPort);
            this.groupBox1.Location = new System.Drawing.Point(3, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 107);
            this.groupBox1.TabIndex = 58;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Program EDTracker";
            this.bScanPorts.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bScanPorts.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bScanPorts.FlatAppearance.BorderSize = 2;
            this.bScanPorts.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bScanPorts.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.bScanPorts.Location = new System.Drawing.Point(185, 75);
            this.bScanPorts.Margin = new Padding(2, 3, 2, 3);
            this.bScanPorts.Name = "bScanPorts";
            this.bScanPorts.Size = new System.Drawing.Size(92, 23);
            this.bScanPorts.TabIndex = 58;
            this.bScanPorts.Text = "Scan Ports";
            this.bScanPorts.UseVisualStyleBackColor = true;
            this.bScanPorts.Click += new EventHandler(this.bScanPorts_Click);
            this.bMonitor.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bMonitor.BackgroundImageLayout = ImageLayout.None;
            this.bMonitor.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bMonitor.FlatAppearance.BorderSize = 2;
            this.bMonitor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bMonitor.Font = new Font("Arial", 11f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.bMonitor.Location = new System.Drawing.Point(309, 65);
            this.bMonitor.Margin = new Padding(2, 3, 2, 3);
            this.bMonitor.Name = "bMonitor";
            this.bMonitor.Size = new System.Drawing.Size(315, 31);
            this.bMonitor.TabIndex = 60;
            this.bMonitor.Text = "Connect to Tracker";
            this.bMonitor.UseVisualStyleBackColor = true;
            this.bMonitor.Click += new EventHandler(this.bMonitor_Click);
            this.gbBias.Controls.Add((Control)this.biasAX);
            this.gbBias.Controls.Add((Control)this.axminus);
            this.gbBias.Controls.Add((Control)this.axplus);
            this.gbBias.Controls.Add((Control)this.udAX);
            this.gbBias.Controls.Add((Control)this.label5);
            this.gbBias.Controls.Add((Control)this.biasAY);
            this.gbBias.Controls.Add((Control)this.ayminus);
            this.gbBias.Controls.Add((Control)this.ayplus);
            this.gbBias.Controls.Add((Control)this.udAY);
            this.gbBias.Controls.Add((Control)this.label6);
            this.gbBias.Controls.Add((Control)this.biasAZ);
            this.gbBias.Controls.Add((Control)this.azminus);
            this.gbBias.Controls.Add((Control)this.azplus);
            this.gbBias.Controls.Add((Control)this.udAZ);
            this.gbBias.Controls.Add((Control)this.label7);
            this.gbBias.Controls.Add((Control)this.biasGZ);
            this.gbBias.Controls.Add((Control)this.gzminus);
            this.gbBias.Controls.Add((Control)this.gzplus);
            this.gbBias.Controls.Add((Control)this.udGZ);
            this.gbBias.Controls.Add((Control)this.label4);
            this.gbBias.Controls.Add((Control)this.biasGY);
            this.gbBias.Controls.Add((Control)this.gyminus);
            this.gbBias.Controls.Add((Control)this.gyplus);
            this.gbBias.Controls.Add((Control)this.udGY);
            this.gbBias.Controls.Add((Control)this.label3);
            this.gbBias.Controls.Add((Control)this.biasGX);
            this.gbBias.Controls.Add((Control)this.gxminus);
            this.gbBias.Controls.Add((Control)this.udGX);
            this.gbBias.Controls.Add((Control)this.gxplus);
            this.gbBias.Controls.Add((Control)this.label2);
            this.gbBias.Controls.Add((Control)this.bCalcBias);
            this.gbBias.Location = new System.Drawing.Point(3, 229);
            this.gbBias.Name = "gbBias";
            this.gbBias.Size = new System.Drawing.Size(218, 210);
            this.gbBias.TabIndex = 88;
            this.gbBias.TabStop = false;
            this.gbBias.Text = "Bias Value";
            this.biasAX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.biasAX.Location = new System.Drawing.Point(68, 100);
            this.biasAX.Name = "biasAX";
            this.biasAX.ReadOnly = true;
            this.biasAX.Size = new System.Drawing.Size(39, 21);
            this.biasAX.TabIndex = 140;
            this.biasAX.Text = "0";
            this.biasAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.axminus.BackColor = System.Drawing.Color.Azure;
            this.axminus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.axminus.FlatAppearance.BorderSize = 2;
            this.axminus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.axminus.FlatStyle = FlatStyle.Flat;
            this.axminus.Location = new System.Drawing.Point(136, 99);
            this.axminus.Margin = new Padding(2, 3, 2, 3);
            this.axminus.Name = "axminus";
            this.axminus.Size = new System.Drawing.Size(20, 23);
            this.axminus.TabIndex = 139;
            this.axminus.Text = "-";
            this.axminus.UseVisualStyleBackColor = false;
            this.axminus.Click += new EventHandler(this.axminus_Click_1);
            this.axplus.BackColor = System.Drawing.Color.Azure;
            this.axplus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.axplus.FlatAppearance.BorderSize = 2;
            this.axplus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.axplus.FlatStyle = FlatStyle.Flat;
            this.axplus.Location = new System.Drawing.Point(112, 99);
            this.axplus.Margin = new Padding(2, 3, 2, 3);
            this.axplus.Name = "axplus";
            this.axplus.Size = new System.Drawing.Size(20, 23);
            this.axplus.TabIndex = 138;
            this.axplus.Text = "+";
            this.axplus.TextAlign = ContentAlignment.TopCenter;
            this.axplus.UseVisualStyleBackColor = false;
            this.axplus.Click += new EventHandler(this.axplus_Click_1);
            this.udAX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.udAX.Location = new System.Drawing.Point(160, 100);
            this.udAX.Name = "udAX";
            this.udAX.ReadOnly = true;
            this.udAX.Size = new System.Drawing.Size(39, 21);
            this.udAX.TabIndex = 137;
            this.udAX.Text = "0";
            this.udAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.GhostWhite;
            this.label5.Location = new System.Drawing.Point(17, 103);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 136;
            this.label5.Text = "Accel X";
            this.biasAY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.biasAY.Location = new System.Drawing.Point(68, 128);
            this.biasAY.Name = "biasAY";
            this.biasAY.ReadOnly = true;
            this.biasAY.Size = new System.Drawing.Size(39, 21);
            this.biasAY.TabIndex = 135;
            this.biasAY.Text = "0";
            this.biasAY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ayminus.BackColor = System.Drawing.Color.Azure;
            this.ayminus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.ayminus.FlatAppearance.BorderSize = 2;
            this.ayminus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.ayminus.FlatStyle = FlatStyle.Flat;
            this.ayminus.Location = new System.Drawing.Point(136, (int)sbyte.MaxValue);
            this.ayminus.Margin = new Padding(2, 3, 2, 3);
            this.ayminus.Name = "ayminus";
            this.ayminus.Size = new System.Drawing.Size(20, 23);
            this.ayminus.TabIndex = 134;
            this.ayminus.Text = "-";
            this.ayminus.UseVisualStyleBackColor = false;
            this.ayminus.Click += new EventHandler(this.ayminus_Click_1);
            this.ayplus.BackColor = System.Drawing.Color.Azure;
            this.ayplus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.ayplus.FlatAppearance.BorderSize = 2;
            this.ayplus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.ayplus.FlatStyle = FlatStyle.Flat;
            this.ayplus.Location = new System.Drawing.Point(112, (int)sbyte.MaxValue);
            this.ayplus.Margin = new Padding(2, 3, 2, 3);
            this.ayplus.Name = "ayplus";
            this.ayplus.Size = new System.Drawing.Size(20, 23);
            this.ayplus.TabIndex = 133;
            this.ayplus.Text = "+";
            this.ayplus.TextAlign = ContentAlignment.TopCenter;
            this.ayplus.UseVisualStyleBackColor = false;
            this.ayplus.Click += new EventHandler(this.ayplus_Click_1);
            this.udAY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.udAY.Location = new System.Drawing.Point(160, 128);
            this.udAY.Name = "udAY";
            this.udAY.ReadOnly = true;
            this.udAY.Size = new System.Drawing.Size(39, 21);
            this.udAY.TabIndex = 132;
            this.udAY.Text = "0";
            this.udAY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.GhostWhite;
            this.label6.Location = new System.Drawing.Point(17, 131);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 131;
            this.label6.Text = "Accel Y";
            this.biasAZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.biasAZ.Location = new System.Drawing.Point(68, 156);
            this.biasAZ.Name = "biasAZ";
            this.biasAZ.ReadOnly = true;
            this.biasAZ.Size = new System.Drawing.Size(39, 21);
            this.biasAZ.TabIndex = 130;
            this.biasAZ.Text = "0";
            this.biasAZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.azminus.BackColor = System.Drawing.Color.Azure;
            this.azminus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.azminus.FlatAppearance.BorderSize = 2;
            this.azminus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.azminus.FlatStyle = FlatStyle.Flat;
            this.azminus.Location = new System.Drawing.Point(136, 155);
            this.azminus.Margin = new Padding(2, 3, 2, 3);
            this.azminus.Name = "azminus";
            this.azminus.Size = new System.Drawing.Size(20, 23);
            this.azminus.TabIndex = 129;
            this.azminus.Text = "-";
            this.azminus.UseVisualStyleBackColor = false;
            this.azminus.Click += new EventHandler(this.azminus_Click_1);
            this.azplus.BackColor = System.Drawing.Color.Azure;
            this.azplus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.azplus.FlatAppearance.BorderSize = 2;
            this.azplus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.azplus.FlatStyle = FlatStyle.Flat;
            this.azplus.Location = new System.Drawing.Point(112, 155);
            this.azplus.Margin = new Padding(2, 3, 2, 3);
            this.azplus.Name = "azplus";
            this.azplus.Size = new System.Drawing.Size(20, 23);
            this.azplus.TabIndex = 128;
            this.azplus.Text = "+";
            this.azplus.TextAlign = ContentAlignment.TopCenter;
            this.azplus.UseVisualStyleBackColor = false;
            this.azplus.Click += new EventHandler(this.azplus_Click_1);
            this.udAZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.udAZ.Location = new System.Drawing.Point(160, 156);
            this.udAZ.Name = "udAZ";
            this.udAZ.ReadOnly = true;
            this.udAZ.Size = new System.Drawing.Size(39, 21);
            this.udAZ.TabIndex = (int)sbyte.MaxValue;
            this.udAZ.Text = "0";
            this.udAZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.GhostWhite;
            this.label7.Location = new System.Drawing.Point(17, 159);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 15);
            this.label7.TabIndex = 126;
            this.label7.Text = "Accel Z";
            this.biasGZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.biasGZ.Location = new System.Drawing.Point(68, 72);
            this.biasGZ.Name = "biasGZ";
            this.biasGZ.ReadOnly = true;
            this.biasGZ.Size = new System.Drawing.Size(39, 21);
            this.biasGZ.TabIndex = 115;
            this.biasGZ.Text = "0";
            this.biasGZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gzminus.BackColor = System.Drawing.Color.Azure;
            this.gzminus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.gzminus.FlatAppearance.BorderSize = 2;
            this.gzminus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.gzminus.FlatStyle = FlatStyle.Flat;
            this.gzminus.Location = new System.Drawing.Point(136, 71);
            this.gzminus.Margin = new Padding(2, 3, 2, 3);
            this.gzminus.Name = "gzminus";
            this.gzminus.Size = new System.Drawing.Size(20, 23);
            this.gzminus.TabIndex = 114;
            this.gzminus.Text = "-";
            this.gzminus.UseVisualStyleBackColor = false;
            this.gzminus.Click += new EventHandler(this.gzminus_Click_1);
            this.gzplus.BackColor = System.Drawing.Color.Azure;
            this.gzplus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.gzplus.FlatAppearance.BorderSize = 2;
            this.gzplus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.gzplus.FlatStyle = FlatStyle.Flat;
            this.gzplus.Location = new System.Drawing.Point(112, 71);
            this.gzplus.Margin = new Padding(2, 3, 2, 3);
            this.gzplus.Name = "gzplus";
            this.gzplus.Size = new System.Drawing.Size(20, 23);
            this.gzplus.TabIndex = 113;
            this.gzplus.Text = "+";
            this.gzplus.TextAlign = ContentAlignment.TopCenter;
            this.gzplus.UseVisualStyleBackColor = false;
            this.gzplus.Click += new EventHandler(this.gzplus_Click_1);
            this.udGZ.BackColor = System.Drawing.Color.LightSkyBlue;
            this.udGZ.Location = new System.Drawing.Point(160, 72);
            this.udGZ.Name = "udGZ";
            this.udGZ.ReadOnly = true;
            this.udGZ.Size = new System.Drawing.Size(39, 21);
            this.udGZ.TabIndex = 112;
            this.udGZ.Text = "0";
            this.udGZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Gold;
            this.label4.Location = new System.Drawing.Point(17, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 15);
            this.label4.TabIndex = 111;
            this.label4.Text = "Gyro Z";
            this.biasGY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.biasGY.Location = new System.Drawing.Point(68, 44);
            this.biasGY.Name = "biasGY";
            this.biasGY.ReadOnly = true;
            this.biasGY.Size = new System.Drawing.Size(39, 21);
            this.biasGY.TabIndex = 110;
            this.biasGY.Text = "0";
            this.biasGY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gyminus.BackColor = System.Drawing.Color.Azure;
            this.gyminus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.gyminus.FlatAppearance.BorderSize = 2;
            this.gyminus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.gyminus.FlatStyle = FlatStyle.Flat;
            this.gyminus.Location = new System.Drawing.Point(136, 43);
            this.gyminus.Margin = new Padding(2, 3, 2, 3);
            this.gyminus.Name = "gyminus";
            this.gyminus.Size = new System.Drawing.Size(20, 23);
            this.gyminus.TabIndex = 109;
            this.gyminus.Text = "-";
            this.gyminus.UseVisualStyleBackColor = false;
            this.gyminus.Click += new EventHandler(this.gyminus_Click_1);
            this.gyplus.BackColor = System.Drawing.Color.Azure;
            this.gyplus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.gyplus.FlatAppearance.BorderSize = 2;
            this.gyplus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.gyplus.FlatStyle = FlatStyle.Flat;
            this.gyplus.Location = new System.Drawing.Point(112, 43);
            this.gyplus.Margin = new Padding(2, 3, 2, 3);
            this.gyplus.Name = "gyplus";
            this.gyplus.Size = new System.Drawing.Size(20, 23);
            this.gyplus.TabIndex = 108;
            this.gyplus.Text = "+";
            this.gyplus.TextAlign = ContentAlignment.TopCenter;
            this.gyplus.UseVisualStyleBackColor = false;
            this.gyplus.Click += new EventHandler(this.gyplus_Click_1);
            this.udGY.BackColor = System.Drawing.Color.MediumSpringGreen;
            this.udGY.Location = new System.Drawing.Point(160, 44);
            this.udGY.Name = "udGY";
            this.udGY.ReadOnly = true;
            this.udGY.Size = new System.Drawing.Size(39, 21);
            this.udGY.TabIndex = 107;
            this.udGY.Text = "0";
            this.udGY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Gold;
            this.label3.Location = new System.Drawing.Point(17, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 15);
            this.label3.TabIndex = 106;
            this.label3.Text = "Gyro Y";
            this.biasGX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.biasGX.Location = new System.Drawing.Point(68, 16);
            this.biasGX.Name = "biasGX";
            this.biasGX.ReadOnly = true;
            this.biasGX.Size = new System.Drawing.Size(39, 21);
            this.biasGX.TabIndex = 105;
            this.biasGX.Text = "0";
            this.biasGX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gxminus.BackColor = System.Drawing.Color.Azure;
            this.gxminus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.gxminus.FlatAppearance.BorderSize = 2;
            this.gxminus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.gxminus.FlatStyle = FlatStyle.Flat;
            this.gxminus.Location = new System.Drawing.Point(136, 15);
            this.gxminus.Margin = new Padding(2, 3, 2, 3);
            this.gxminus.Name = "gxminus";
            this.gxminus.Size = new System.Drawing.Size(20, 23);
            this.gxminus.TabIndex = 104;
            this.gxminus.Text = "-";
            this.gxminus.UseVisualStyleBackColor = false;
            this.gxminus.Click += new EventHandler(this.gxminus_Click_1);
            this.udGX.BackColor = System.Drawing.Color.LemonChiffon;
            this.udGX.Location = new System.Drawing.Point(160, 16);
            this.udGX.Name = "udGX";
            this.udGX.ReadOnly = true;
            this.udGX.Size = new System.Drawing.Size(39, 21);
            this.udGX.TabIndex = 102;
            this.udGX.Text = "0";
            this.udGX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.gxplus.BackColor = System.Drawing.Color.Azure;
            this.gxplus.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.gxplus.FlatAppearance.BorderSize = 2;
            this.gxplus.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.gxplus.FlatStyle = FlatStyle.Flat;
            this.gxplus.Location = new System.Drawing.Point(112, 15);
            this.gxplus.Margin = new Padding(2, 3, 2, 3);
            this.gxplus.Name = "gxplus";
            this.gxplus.Size = new System.Drawing.Size(20, 23);
            this.gxplus.TabIndex = 103;
            this.gxplus.Text = "+";
            this.gxplus.TextAlign = ContentAlignment.TopCenter;
            this.gxplus.UseVisualStyleBackColor = false;
            this.gxplus.Click += new EventHandler(this.gxplus_Click_1);
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Gold;
            this.label2.Location = new System.Drawing.Point(17, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 15);
            this.label2.TabIndex = 101;
            this.label2.Text = "Gyro X";
            this.bCalcBias.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bCalcBias.CausesValidation = false;
            this.bCalcBias.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bCalcBias.FlatAppearance.BorderSize = 2;
            this.bCalcBias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bCalcBias.Location = new System.Drawing.Point(46, 180);
            this.bCalcBias.Margin = new Padding(2, 3, 2, 3);
            this.bCalcBias.Name = "bCalcBias";
            this.bCalcBias.Size = new System.Drawing.Size(153, 23);
            this.bCalcBias.TabIndex = 11;
            this.bCalcBias.Text = "Calculate Bias Values";
            this.bCalcBias.UseVisualStyleBackColor = true;
            this.bCalcBias.Click += new EventHandler(this.bCalcBias_Click);
            this.gbHotKey.Controls.Add((Control)this.btEnableAC);
            this.gbHotKey.Controls.Add((Control)this.tbAutoCentre);
            this.gbHotKey.Controls.Add((Control)this.btToggleAutoCentre);
            this.gbHotKey.Controls.Add((Control)this.cbHKey);
            this.gbHotKey.Controls.Add((Control)this.cbCOntrollerButtons);
            this.gbHotKey.Controls.Add((Control)this.cbControllerButton);
            this.gbHotKey.Controls.Add((Control)this.cbHotKey);
            this.gbHotKey.Location = new System.Drawing.Point(3, 396);
            this.gbHotKey.Name = "gbHotKey";
            this.gbHotKey.Size = new System.Drawing.Size(292, 76);
            this.gbHotKey.TabIndex = 143;
            this.gbHotKey.TabStop = false;
            this.gbHotKey.Text = "Recentre";
            this.btEnableAC.Enabled = false;
            this.btEnableAC.Location = new System.Drawing.Point(218, 44);
            this.btEnableAC.Name = "btEnableAC";
            this.btEnableAC.Size = new System.Drawing.Size(59, 23);
            this.btEnableAC.TabIndex = 152;
            this.btEnableAC.Text = "Enable";
            this.btEnableAC.UseVisualStyleBackColor = true;
            this.btEnableAC.Visible = false;
            this.btEnableAC.Click += new EventHandler(this.btEnableAC_Click);
            this.tbAutoCentre.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbAutoCentre.BorderStyle = BorderStyle.None;
            this.tbAutoCentre.Font = new Font("Arial", 11f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbAutoCentre.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbAutoCentre.Location = new System.Drawing.Point(135, 46);
            this.tbAutoCentre.Multiline = true;
            this.tbAutoCentre.Name = "tbAutoCentre";
            this.tbAutoCentre.ReadOnly = true;
            this.tbAutoCentre.Size = new System.Drawing.Size(74, 20);
            this.tbAutoCentre.TabIndex = 157;
            this.tbAutoCentre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btToggleAutoCentre.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btToggleAutoCentre.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btToggleAutoCentre.FlatAppearance.BorderSize = 2;
            this.btToggleAutoCentre.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btToggleAutoCentre.Location = new System.Drawing.Point(12, 44);
            this.btToggleAutoCentre.Margin = new Padding(2, 3, 2, 3);
            this.btToggleAutoCentre.Name = "btToggleAutoCentre";
            this.btToggleAutoCentre.Size = new System.Drawing.Size(118, 23);
            this.btToggleAutoCentre.TabIndex = 156;
            this.btToggleAutoCentre.Text = "Toggle Auto-centre";
            this.btToggleAutoCentre.UseVisualStyleBackColor = true;
            this.btToggleAutoCentre.Click += new EventHandler(this.btToggleAutoCentre_Click);
            this.cbHKey.FormattingEnabled = true;
            this.cbHKey.Items.AddRange(new object[4]
            {
            (object) "Button 1",
            (object) "Button 2",
            (object) "Button 3",
            (object) "Button 4"
            });
            this.cbHKey.Location = new System.Drawing.Point(135, 19);
            this.cbHKey.Name = "cbHKey";
            this.cbHKey.Size = new System.Drawing.Size(74, 23);
            this.cbHKey.TabIndex = 41;
            this.cbHKey.SelectedIndexChanged += new EventHandler(this.cbHKey_SelectedIndexChanged);
            this.cbCOntrollerButtons.FormattingEnabled = true;
            this.cbCOntrollerButtons.Items.AddRange(new object[4]
            {
            (object) "Button 1",
            (object) "Button 2",
            (object) "Button 3",
            (object) "Button 4"
            });
            this.cbCOntrollerButtons.Location = new System.Drawing.Point(153, 44);
            this.cbCOntrollerButtons.Name = "cbCOntrollerButtons";
            this.cbCOntrollerButtons.Size = new System.Drawing.Size(121, 23);
            this.cbCOntrollerButtons.TabIndex = 40;
            this.cbCOntrollerButtons.Visible = false;
            this.cbControllerButton.AutoSize = true;
            this.cbControllerButton.Location = new System.Drawing.Point(6, 46);
            this.cbControllerButton.Name = "cbControllerButton";
            this.cbControllerButton.RightToLeft = RightToLeft.Yes;
            this.cbControllerButton.Size = new System.Drawing.Size(160, 19);
            this.cbControllerButton.TabIndex = 35;
            this.cbControllerButton.Text = "Enable Controller Button";
            this.cbControllerButton.UseVisualStyleBackColor = true;
            this.cbControllerButton.Visible = false;
            this.cbHotKey.AutoSize = true;
            this.cbHotKey.Location = new System.Drawing.Point(12, 21);
            this.cbHotKey.Name = "cbHotKey";
            this.cbHotKey.RightToLeft = RightToLeft.Yes;
            this.cbHotKey.Size = new System.Drawing.Size(105, 19);
            this.cbHotKey.TabIndex = 34;
            this.cbHotKey.Text = "Enable Hotkey";
            this.cbHotKey.TextAlign = ContentAlignment.MiddleRight;
            this.cbHotKey.UseVisualStyleBackColor = true;
            this.cbHotKey.CheckedChanged += new EventHandler(this.cbHotKey_CheckedChanged);
            this.gbTrackerConfig.Controls.Add((Control)this.bOrientate);
            this.gbTrackerConfig.Controls.Add((Control)this.tbOrientation);
            this.gbTrackerConfig.Controls.Add((Control)this.bSensorMode);
            this.gbTrackerConfig.Controls.Add((Control)this.tbSensorMode);
            this.gbTrackerConfig.Location = new System.Drawing.Point(3, 139);
            this.gbTrackerConfig.Name = "gbTrackerConfig";
            this.gbTrackerConfig.Size = new System.Drawing.Size(292, 83);
            this.gbTrackerConfig.TabIndex = 89;
            this.gbTrackerConfig.TabStop = false;
            this.gbTrackerConfig.Text = "Tracker Config";
            this.bOrientate.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bOrientate.Enabled = false;
            this.bOrientate.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bOrientate.FlatAppearance.BorderSize = 2;
            this.bOrientate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bOrientate.Location = new System.Drawing.Point(6, 19);
            this.bOrientate.Margin = new Padding(2, 3, 2, 3);
            this.bOrientate.Name = "bOrientate";
            this.bOrientate.Size = new System.Drawing.Size(149, 23);
            this.bOrientate.TabIndex = 53;
            this.bOrientate.Text = "Rotate Mounting Axis";
            this.bOrientate.UseVisualStyleBackColor = true;
            this.bOrientate.Click += new EventHandler(this.bOrientate_Click);
            this.tbOrientation.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbOrientation.BorderStyle = BorderStyle.None;
            this.tbOrientation.Font = new Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbOrientation.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbOrientation.Location = new System.Drawing.Point(164, 21);
            this.tbOrientation.Multiline = true;
            this.tbOrientation.Name = "tbOrientation";
            this.tbOrientation.ReadOnly = true;
            this.tbOrientation.Size = new System.Drawing.Size(113, 20);
            this.tbOrientation.TabIndex = 56;
            this.tbOrientation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbOrientation.TextChanged += new EventHandler(this.tbOrientation_TextChanged);
            this.bSensorMode.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bSensorMode.Enabled = false;
            this.bSensorMode.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bSensorMode.FlatAppearance.BorderSize = 2;
            this.bSensorMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bSensorMode.Location = new System.Drawing.Point(6, 53);
            this.bSensorMode.Margin = new Padding(2, 3, 2, 3);
            this.bSensorMode.Name = "bSensorMode";
            this.bSensorMode.Size = new System.Drawing.Size(149, 23);
            this.bSensorMode.TabIndex = 55;
            this.bSensorMode.Text = "Toggle Sensor Mode";
            this.bSensorMode.UseVisualStyleBackColor = true;
            this.bSensorMode.Click += new EventHandler(this.bSensorMode_Click);
            this.tbSensorMode.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbSensorMode.BorderStyle = BorderStyle.None;
            this.tbSensorMode.Font = new Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbSensorMode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbSensorMode.Location = new System.Drawing.Point(164, 55);
            this.tbSensorMode.Multiline = true;
            this.tbSensorMode.Name = "tbSensorMode";
            this.tbSensorMode.ReadOnly = true;
            this.tbSensorMode.Size = new System.Drawing.Size(113, 20);
            this.tbSensorMode.TabIndex = 57;
            this.tbSensorMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSketch.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.tbSketch.CausesValidation = false;
            this.tbSketch.Font = new Font("Arial", 16f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbSketch.Location = new System.Drawing.Point(309, 31);
            this.tbSketch.Name = "tbSketch";
            this.tbSketch.ReadOnly = true;
            this.tbSketch.Size = new System.Drawing.Size(314, 32);
            this.tbSketch.TabIndex = 140;
            this.tbSketch.Text = "EDTracker Sketch";
            this.tbSketch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.gbDriftComp.Controls.Add((Control)this.lbTimer);
            this.gbDriftComp.Controls.Add((Control)this.tbTime);
            this.gbDriftComp.Controls.Add((Control)this.bSaveDriftComp);
            this.gbDriftComp.Controls.Add((Control)this.bResetView);
            this.gbDriftComp.Controls.Add((Control)this.tbDriftComp);
            this.gbDriftComp.Controls.Add((Control)this.lbComp);
            this.gbDriftComp.Controls.Add((Control)this.tbYawDrift);
            this.gbDriftComp.Controls.Add((Control)this.lbYawDrift);
            this.gbDriftComp.Controls.Add((Control)this.btCompDown);
            this.gbDriftComp.Controls.Add((Control)this.btCompUp);
            this.gbDriftComp.Location = new System.Drawing.Point(3, 474);
            this.gbDriftComp.Name = "gbDriftComp";
            this.gbDriftComp.Size = new System.Drawing.Size(292, 134);
            this.gbDriftComp.TabIndex = 141;
            this.gbDriftComp.TabStop = false;
            this.gbDriftComp.Text = "Drift Compensation";
            this.lbTimer.AutoSize = true;
            this.lbTimer.Location = new System.Drawing.Point(143, 18);
            this.lbTimer.Name = "lbTimer";
            this.lbTimer.Size = new System.Drawing.Size(39, 15);
            this.lbTimer.TabIndex = 154;
            this.lbTimer.Text = "Timer";
            this.lbTimer.TextAlign = ContentAlignment.MiddleRight;
            this.tbTime.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbTime.Font = new Font("Courier New", 8.25f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbTime.Location = new System.Drawing.Point(180, 15);
            this.tbTime.Name = "tbTime";
            this.tbTime.ReadOnly = true;
            this.tbTime.Size = new System.Drawing.Size(44, 20);
            this.tbTime.TabIndex = 153;
            this.tbTime.Text = "00:00";
            this.tbTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.bSaveDriftComp.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bSaveDriftComp.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bSaveDriftComp.FlatAppearance.BorderSize = 2;
            this.bSaveDriftComp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bSaveDriftComp.Location = new System.Drawing.Point(14, 99);
            this.bSaveDriftComp.Margin = new Padding(2, 3, 2, 3);
            this.bSaveDriftComp.Name = "bSaveDriftComp";
            this.bSaveDriftComp.Size = new System.Drawing.Size(173, 23);
            this.bSaveDriftComp.TabIndex = 145;
            this.bSaveDriftComp.Text = "Save Drift Compensation";
            this.bSaveDriftComp.UseVisualStyleBackColor = true;
            this.bSaveDriftComp.Click += new EventHandler(this.bSaveDriftComp_Click);
            this.bResetView.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bResetView.BackgroundImageLayout = ImageLayout.None;
            this.bResetView.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bResetView.FlatAppearance.BorderSize = 2;
            this.bResetView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bResetView.Location = new System.Drawing.Point(14, 71);
            this.bResetView.Margin = new Padding(2, 3, 2, 3);
            this.bResetView.Name = "bResetView";
            this.bResetView.Size = new System.Drawing.Size(173, 23);
            this.bResetView.TabIndex = 144;
            this.bResetView.Text = "Reset View / Drift Tracking";
            this.bResetView.UseVisualStyleBackColor = true;
            this.bResetView.Click += new EventHandler(this.bResetView_Click);
            this.tbDriftComp.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbDriftComp.Location = new System.Drawing.Point(94, 39);
            this.tbDriftComp.Name = "tbDriftComp";
            this.tbDriftComp.ReadOnly = true;
            this.tbDriftComp.Size = new System.Drawing.Size(39, 21);
            this.tbDriftComp.TabIndex = 141;
            this.tbDriftComp.Text = "0";
            this.tbDriftComp.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lbComp.AutoSize = true;
            this.lbComp.Location = new System.Drawing.Point(6, 44);
            this.lbComp.Name = "lbComp";
            this.lbComp.Size = new System.Drawing.Size(89, 15);
            this.lbComp.TabIndex = 140;
            this.lbComp.Text = "Compensation";
            this.lbComp.TextAlign = ContentAlignment.MiddleRight;
            this.tbYawDrift.BackColor = System.Drawing.Color.LemonChiffon;
            this.tbYawDrift.Location = new System.Drawing.Point(93, 15);
            this.tbYawDrift.Name = "tbYawDrift";
            this.tbYawDrift.ReadOnly = true;
            this.tbYawDrift.Size = new System.Drawing.Size(39, 21);
            this.tbYawDrift.TabIndex = 139;
            this.tbYawDrift.Text = "0";
            this.tbYawDrift.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lbYawDrift.AutoSize = true;
            this.lbYawDrift.Location = new System.Drawing.Point(36, 18);
            this.lbYawDrift.Name = "lbYawDrift";
            this.lbYawDrift.Size = new System.Drawing.Size(54, 15);
            this.lbYawDrift.TabIndex = 138;
            this.lbYawDrift.Text = "Yaw Drift";
            this.lbYawDrift.TextAlign = ContentAlignment.MiddleRight;
            this.btCompDown.BackColor = System.Drawing.Color.Azure;
            this.btCompDown.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btCompDown.FlatAppearance.BorderSize = 2;
            this.btCompDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btCompDown.FlatStyle = FlatStyle.Flat;
            this.btCompDown.Location = new System.Drawing.Point(168, 39);
            this.btCompDown.Margin = new Padding(2, 3, 2, 3);
            this.btCompDown.Name = "btCompDown";
            this.btCompDown.Size = new System.Drawing.Size(20, 23);
            this.btCompDown.TabIndex = 137;
            this.btCompDown.Text = "-";
            this.btCompDown.UseVisualStyleBackColor = false;
            this.btCompDown.Click += new EventHandler(this.btCompDown_Click);
            this.btCompUp.BackColor = System.Drawing.Color.Azure;
            this.btCompUp.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btCompUp.FlatAppearance.BorderSize = 2;
            this.btCompUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btCompUp.FlatStyle = FlatStyle.Flat;
            this.btCompUp.Location = new System.Drawing.Point(144, 39);
            this.btCompUp.Margin = new Padding(2, 3, 2, 3);
            this.btCompUp.Name = "btCompUp";
            this.btCompUp.Size = new System.Drawing.Size(20, 23);
            this.btCompUp.TabIndex = 136;
            this.btCompUp.Text = "+";
            this.btCompUp.UseVisualStyleBackColor = false;
            this.btCompUp.Click += new EventHandler(this.btCompUp_Click);
            this.gbScaling.Controls.Add((Control)this.tbSmoothing);
            this.gbScaling.Controls.Add((Control)this.lbSmooth);
            this.gbScaling.Controls.Add((Control)this.sliderSmoothing);
            this.gbScaling.Controls.Add((Control)this.tbRespMode);
            this.gbScaling.Controls.Add((Control)this.bRespMode);
            this.gbScaling.Controls.Add((Control)this.tbPitchScaling);
            this.gbScaling.Controls.Add((Control)this.label11);
            this.gbScaling.Controls.Add((Control)this.tbYawScaling);
            this.gbScaling.Controls.Add((Control)this.label10);
            this.gbScaling.Controls.Add((Control)this.cbFineAdjust);
            this.gbScaling.Controls.Add((Control)this.btYawScaleDown);
            this.gbScaling.Controls.Add((Control)this.btPitchScaleDown);
            this.gbScaling.Controls.Add((Control)this.btPitchScaleUp);
            this.gbScaling.Controls.Add((Control)this.btYawScaleUp);
            this.gbScaling.Location = new System.Drawing.Point(3, 227);
            this.gbScaling.Name = "gbScaling";
            this.gbScaling.Size = new System.Drawing.Size(292, 165);
            this.gbScaling.TabIndex = 142;
            this.gbScaling.TabStop = false;
            this.gbScaling.Text = "Response Scaling";
            this.tbSmoothing.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbSmoothing.Location = new System.Drawing.Point(95, 78);
            this.tbSmoothing.Name = "tbSmoothing";
            this.tbSmoothing.ReadOnly = true;
            this.tbSmoothing.Size = new System.Drawing.Size(39, 21);
            this.tbSmoothing.TabIndex = 154;
            this.tbSmoothing.Text = "0";
            this.tbSmoothing.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lbSmooth.AutoSize = true;
            this.lbSmooth.Location = new System.Drawing.Point(9, 80);
            this.lbSmooth.Name = "lbSmooth";
            this.lbSmooth.Size = new System.Drawing.Size(67, 15);
            this.lbSmooth.TabIndex = 153;
            this.lbSmooth.Text = "Smoothing";
            this.lbSmooth.TextAlign = ContentAlignment.MiddleRight;
            this.sliderSmoothing.AutoSize = false;
            this.sliderSmoothing.Location = new System.Drawing.Point(139, 78);
            this.sliderSmoothing.Maximum = 99;
            this.sliderSmoothing.Name = "sliderSmoothing";
            this.sliderSmoothing.Size = new System.Drawing.Size(138, 27);
            this.sliderSmoothing.TabIndex = 152;
            this.sliderSmoothing.Scroll += new EventHandler(this.trackBar1_Scroll);
            this.sliderSmoothing.MouseUp += new MouseEventHandler(this.trackBar1_MUP);
            this.tbRespMode.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbRespMode.BorderStyle = BorderStyle.None;
            this.tbRespMode.Font = new Font("Arial", 9.75f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbRespMode.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbRespMode.Location = new System.Drawing.Point(164, (int)sbyte.MaxValue);
            this.tbRespMode.Multiline = true;
            this.tbRespMode.Name = "tbRespMode";
            this.tbRespMode.ReadOnly = true;
            this.tbRespMode.Size = new System.Drawing.Size(113, 20);
            this.tbRespMode.TabIndex = 150;
            this.tbRespMode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bRespMode.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.bRespMode.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.bRespMode.FlatAppearance.BorderSize = 2;
            this.bRespMode.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.bRespMode.Location = new System.Drawing.Point(6, 125);
            this.bRespMode.Margin = new Padding(2, 3, 2, 3);
            this.bRespMode.Name = "bRespMode";
            this.bRespMode.Size = new System.Drawing.Size(150, 23);
            this.bRespMode.TabIndex = 149;
            this.bRespMode.Text = "Toggle Response Mode";
            this.bRespMode.UseVisualStyleBackColor = true;
            this.bRespMode.Click += new EventHandler(this.bRespMode_Click);
            this.tbPitchScaling.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbPitchScaling.Location = new System.Drawing.Point(95, 52);
            this.tbPitchScaling.Name = "tbPitchScaling";
            this.tbPitchScaling.ReadOnly = true;
            this.tbPitchScaling.Size = new System.Drawing.Size(39, 21);
            this.tbPitchScaling.TabIndex = 148;
            this.tbPitchScaling.Text = "0";
            this.tbPitchScaling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 55);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 15);
            this.label11.TabIndex = 147;
            this.label11.Text = "Pitch Scaling";
            this.label11.TextAlign = ContentAlignment.MiddleRight;
            this.tbYawScaling.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbYawScaling.Location = new System.Drawing.Point(95, 24);
            this.tbYawScaling.Name = "tbYawScaling";
            this.tbYawScaling.ReadOnly = true;
            this.tbYawScaling.Size = new System.Drawing.Size(39, 21);
            this.tbYawScaling.TabIndex = 146;
            this.tbYawScaling.Text = "0";
            this.tbYawScaling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 27);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 15);
            this.label10.TabIndex = 145;
            this.label10.Text = "Yaw Scaling";
            this.label10.TextAlign = ContentAlignment.MiddleRight;
            this.cbFineAdjust.AutoSize = true;
            this.cbFineAdjust.Location = new System.Drawing.Point(188, 22);
            this.cbFineAdjust.Name = "cbFineAdjust";
            this.cbFineAdjust.Size = new System.Drawing.Size(86, 19);
            this.cbFineAdjust.TabIndex = 144;
            this.cbFineAdjust.Text = "Fine Adjust";
            this.cbFineAdjust.UseVisualStyleBackColor = true;
            this.btYawScaleDown.BackColor = System.Drawing.Color.Azure;
            this.btYawScaleDown.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btYawScaleDown.FlatAppearance.BorderSize = 2;
            this.btYawScaleDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btYawScaleDown.FlatStyle = FlatStyle.Flat;
            this.btYawScaleDown.Location = new System.Drawing.Point(163, 22);
            this.btYawScaleDown.Margin = new Padding(2, 3, 2, 3);
            this.btYawScaleDown.Name = "btYawScaleDown";
            this.btYawScaleDown.Size = new System.Drawing.Size(20, 23);
            this.btYawScaleDown.TabIndex = 143;
            this.btYawScaleDown.Text = "-";
            this.btYawScaleDown.UseVisualStyleBackColor = false;
            this.btYawScaleDown.Click += new EventHandler(this.btYawScaleDown_Click);
            this.btPitchScaleDown.BackColor = System.Drawing.Color.Azure;
            this.btPitchScaleDown.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btPitchScaleDown.FlatAppearance.BorderSize = 2;
            this.btPitchScaleDown.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btPitchScaleDown.FlatStyle = FlatStyle.Flat;
            this.btPitchScaleDown.Location = new System.Drawing.Point(163, 49);
            this.btPitchScaleDown.Margin = new Padding(2, 3, 2, 3);
            this.btPitchScaleDown.Name = "btPitchScaleDown";
            this.btPitchScaleDown.Size = new System.Drawing.Size(20, 23);
            this.btPitchScaleDown.TabIndex = 142;
            this.btPitchScaleDown.Text = "-";
            this.btPitchScaleDown.UseVisualStyleBackColor = false;
            this.btPitchScaleDown.Click += new EventHandler(this.btPitchScaleDown_Click);
            this.btPitchScaleUp.BackColor = System.Drawing.Color.Azure;
            this.btPitchScaleUp.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btPitchScaleUp.FlatAppearance.BorderSize = 2;
            this.btPitchScaleUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btPitchScaleUp.FlatStyle = FlatStyle.Flat;
            this.btPitchScaleUp.Location = new System.Drawing.Point(139, 49);
            this.btPitchScaleUp.Margin = new Padding(2, 3, 2, 3);
            this.btPitchScaleUp.Name = "btPitchScaleUp";
            this.btPitchScaleUp.Size = new System.Drawing.Size(20, 23);
            this.btPitchScaleUp.TabIndex = 141;
            this.btPitchScaleUp.Text = "+";
            this.btPitchScaleUp.UseVisualStyleBackColor = false;
            this.btPitchScaleUp.Click += new EventHandler(this.btPitchScaleUp_Click);
            this.btYawScaleUp.BackColor = System.Drawing.Color.Azure;
            this.btYawScaleUp.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btYawScaleUp.FlatAppearance.BorderSize = 2;
            this.btYawScaleUp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btYawScaleUp.FlatStyle = FlatStyle.Flat;
            this.btYawScaleUp.Location = new System.Drawing.Point(139, 22);
            this.btYawScaleUp.Margin = new Padding(2, 3, 2, 3);
            this.btYawScaleUp.Name = "btYawScaleUp";
            this.btYawScaleUp.Size = new System.Drawing.Size(20, 23);
            this.btYawScaleUp.TabIndex = 140;
            this.btYawScaleUp.Text = "+";
            this.btYawScaleUp.UseVisualStyleBackColor = false;
            this.btYawScaleUp.Click += new EventHandler(this.btYawScaleUp_Click);
            this.tbTemps.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbTemps.Location = new System.Drawing.Point(267, 17);
            this.tbTemps.Name = "tbTemps";
            this.tbTemps.ReadOnly = true;
            this.tbTemps.Size = new System.Drawing.Size(39, 21);
            this.tbTemps.TabIndex = 145;
            this.tbTemps.Text = "0";
            this.tbTemps.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lbTemps.AutoSize = true;
            this.lbTemps.BackColor = System.Drawing.Color.PaleGreen;
            this.lbTemps.Location = new System.Drawing.Point(185, 20);
            this.lbTemps.Name = "lbTemps";
            this.lbTemps.Size = new System.Drawing.Size(77, 15);
            this.lbTemps.TabIndex = 144;
            this.lbTemps.Text = "Temperature";
            this.lbTemps.TextAlign = ContentAlignment.MiddleRight;
            this.tbMonitor.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMonitor.BorderStyle = BorderStyle.None;
            this.tbMonitor.CausesValidation = false;
            this.tbMonitor.Font = new Font("Arial", 11f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMonitor.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tbMonitor.Location = new System.Drawing.Point(18, 17);
            this.tbMonitor.Name = "tbMonitor";
            this.tbMonitor.ReadOnly = true;
            this.tbMonitor.Size = new System.Drawing.Size(136, 17);
            this.tbMonitor.TabIndex = 61;
            this.tbMonitor.Text = "OFF";
            this.tbMonitor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.notifyIcon1.BalloonTipText = "EDTracker is minimised";
            this.notifyIcon1.BalloonTipTitle = "EDTracker UI";
            this.notifyIcon1.Icon = (Icon)componentResourceManager.GetObject("notifyIcon1.Icon");
            this.notifyIcon1.Text = "EDTracker";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            this.lbAxis4.BackColor = System.Drawing.Color.Black;
            this.lbAxis4.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.lbAxis4.Location = new System.Drawing.Point(15, 484);
            this.lbAxis4.Name = "lbAxis4";
            this.lbAxis4.Size = new System.Drawing.Size(107, 14);
            this.lbAxis4.TabIndex = 149;
            this.lbAxis4.Text = "Axis 4";
            this.lbAxis3.BackColor = System.Drawing.Color.Black;
            this.lbAxis3.ForeColor = System.Drawing.Color.LawnGreen;
            this.lbAxis3.Location = new System.Drawing.Point(15, 443);
            this.lbAxis3.Name = "lbAxis3";
            this.lbAxis3.Size = new System.Drawing.Size(107, 14);
            this.lbAxis3.TabIndex = 150;
            this.lbAxis3.Text = "Axis 3";
            this.lbAxis2.BackColor = System.Drawing.Color.Black;
            this.lbAxis2.ForeColor = System.Drawing.Color.Yellow;
            this.lbAxis2.Location = new System.Drawing.Point(15, 404);
            this.lbAxis2.Name = "lbAxis2";
            this.lbAxis2.Size = new System.Drawing.Size(107, 14);
            this.lbAxis2.TabIndex = 148;
            this.lbAxis2.Text = "Axis 2";
            this.lbAxis1.BackColor = System.Drawing.Color.Black;
            this.lbAxis1.CausesValidation = false;
            this.lbAxis1.ForeColor = System.Drawing.Color.Red;
            this.lbAxis1.Location = new System.Drawing.Point(15, 363);
            this.lbAxis1.Name = "lbAxis1";
            this.lbAxis1.Size = new System.Drawing.Size(107, 14);
            this.lbAxis1.TabIndex = 147;
            this.lbAxis1.Text = "Axis 1";
            this.menuStrip1.Items.AddRange(new ToolStripItem[3]
            {
            (ToolStripItem) this.toolStripMenuItem1,
            (ToolStripItem) this.Help,
            (ToolStripItem) this.debugToolStripMenuItem
            });
            this.menuStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(654, 24);
            this.menuStrip1.Stretch = false;
            this.menuStrip1.TabIndex = 151;
            this.menuStrip1.Text = "menuStrip1";
            this.toolStripMenuItem1.Alignment = ToolStripItemAlignment.Right;
            this.toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[2]
            {
            (ToolStripItem) this.aboutToolStripMenuItem,
            (ToolStripItem) this.modeToolStripMenuItem
            });
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.toolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
            this.modeToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
            {
            (ToolStripItem) this.uSERToolStripMenuItem,
            (ToolStripItem) this.tESTToolStripMenuItem,
            (ToolStripItem) this.dEVToolStripMenuItem
            });
            this.modeToolStripMenuItem.Name = "modeToolStripMenuItem";
            this.modeToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.modeToolStripMenuItem.Text = "Mode";
            this.uSERToolStripMenuItem.Name = "uSERToolStripMenuItem";
            this.uSERToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.uSERToolStripMenuItem.Text = "USER";
            this.uSERToolStripMenuItem.Click += new EventHandler(this.uSERToolStripMenuItem_Click);
            this.tESTToolStripMenuItem.Name = "tESTToolStripMenuItem";
            this.tESTToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.tESTToolStripMenuItem.Text = "TEST";
            this.tESTToolStripMenuItem.Click += new EventHandler(this.tESTToolStripMenuItem_Click);
            this.dEVToolStripMenuItem.Name = "dEVToolStripMenuItem";
            this.dEVToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.dEVToolStripMenuItem.Text = "DEV";
            this.dEVToolStripMenuItem.Click += new EventHandler(this.dEVToolStripMenuItem_Click);
            this.Help.Alignment = ToolStripItemAlignment.Right;
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(44, 20);
            this.Help.Text = "Help";
            this.Help.Click += new EventHandler(this.Help_Click);
            this.debugToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            this.debugToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
            {
            (ToolStripItem) this.showToolStripMenuItem,
            (ToolStripItem) this.hideToolStripMenuItem
            });
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
            this.debugToolStripMenuItem.Text = "Log";
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new EventHandler(this.showToolStripMenuItem_Click);
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.hideToolStripMenuItem.Text = "Hide";
            this.hideToolStripMenuItem.Click += new EventHandler(this.hideToolStripMenuItem_Click);
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new EventHandler(this.timer1_Tick);
            this.gbMagCalib.Controls.Add((Control)this.label1);
            this.gbMagCalib.Controls.Add((Control)this.tbOffZ);
            this.gbMagCalib.Controls.Add((Control)this.tbOffY);
            this.gbMagCalib.Controls.Add((Control)this.tbOffX);
            this.gbMagCalib.Controls.Add((Control)this.label18);
            this.gbMagCalib.Controls.Add((Control)this.tbarMagSens);
            this.gbMagCalib.Controls.Add((Control)this.button1);
            this.gbMagCalib.Controls.Add((Control)this.label16);
            this.gbMagCalib.Controls.Add((Control)this.tbMagSamples);
            this.gbMagCalib.Controls.Add((Control)this.tbMinX);
            this.gbMagCalib.Controls.Add((Control)this.label9);
            this.gbMagCalib.Controls.Add((Control)this.label12);
            this.gbMagCalib.Controls.Add((Control)this.label13);
            this.gbMagCalib.Controls.Add((Control)this.label14);
            this.gbMagCalib.Controls.Add((Control)this.label15);
            this.gbMagCalib.Controls.Add((Control)this.tbMaxZ);
            this.gbMagCalib.Controls.Add((Control)this.tbMinZ);
            this.gbMagCalib.Controls.Add((Control)this.tbMaxY);
            this.gbMagCalib.Controls.Add((Control)this.tbMinY);
            this.gbMagCalib.Controls.Add((Control)this.tbMaxX);
            this.gbMagCalib.Location = new System.Drawing.Point(6, 315);
            this.gbMagCalib.Name = "gbMagCalib";
            this.gbMagCalib.Size = new System.Drawing.Size(174, 205);
            this.gbMagCalib.TabIndex = 152;
            this.gbMagCalib.TabStop = false;
            this.gbMagCalib.Text = "Calibration";
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(121, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 15);
            this.label1.TabIndex = 171;
            this.label1.Text = "Offset";
            this.label1.TextAlign = ContentAlignment.MiddleRight;
            this.tbOffZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbOffZ.BorderStyle = BorderStyle.None;
            this.tbOffZ.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbOffZ.ForeColor = System.Drawing.Color.Black;
            this.tbOffZ.Location = new System.Drawing.Point(122, 87);
            this.tbOffZ.Multiline = true;
            this.tbOffZ.Name = "tbOffZ";
            this.tbOffZ.ReadOnly = true;
            this.tbOffZ.Size = new System.Drawing.Size(44, 20);
            this.tbOffZ.TabIndex = 170;
            this.tbOffZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbOffY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbOffY.BorderStyle = BorderStyle.None;
            this.tbOffY.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbOffY.ForeColor = System.Drawing.Color.Black;
            this.tbOffY.Location = new System.Drawing.Point(122, 61);
            this.tbOffY.Multiline = true;
            this.tbOffY.Name = "tbOffY";
            this.tbOffY.ReadOnly = true;
            this.tbOffY.Size = new System.Drawing.Size(44, 20);
            this.tbOffY.TabIndex = 169;
            this.tbOffY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbOffX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbOffX.BorderStyle = BorderStyle.None;
            this.tbOffX.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbOffX.ForeColor = System.Drawing.Color.Black;
            this.tbOffX.Location = new System.Drawing.Point(122, 35);
            this.tbOffX.Multiline = true;
            this.tbOffX.Name = "tbOffX";
            this.tbOffX.ReadOnly = true;
            this.tbOffX.Size = new System.Drawing.Size(44, 20);
            this.tbOffX.TabIndex = 168;
            this.tbOffX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(4, 152);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(61, 15);
            this.label18.TabIndex = 167;
            this.label18.Text = "Sensitivity";
            this.label18.TextAlign = ContentAlignment.MiddleRight;
            this.tbarMagSens.AutoSize = false;
            this.tbarMagSens.Location = new System.Drawing.Point(6, 173);
            this.tbarMagSens.Maximum = 20;
            this.tbarMagSens.Name = "tbarMagSens";
            this.tbarMagSens.Size = new System.Drawing.Size(162, 23);
            this.tbarMagSens.TabIndex = 166;
            this.tbarMagSens.TickStyle = TickStyle.None;
            this.tbarMagSens.Value = 15;
            this.tbarMagSens.Scroll += new EventHandler(this.trackBar1_Scroll_1);
            this.button1.Location = new System.Drawing.Point(98, 114);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(71, 23);
            this.button1.TabIndex = 165;
            this.button1.Text = "Restart";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new EventHandler(this.button1_Click);
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 116);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(42, 15);
            this.label16.TabIndex = 164;
            this.label16.Text = "Points";
            this.label16.TextAlign = ContentAlignment.MiddleRight;
            this.label16.Click += new EventHandler(this.label16_Click);
            this.tbMagSamples.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagSamples.BorderStyle = BorderStyle.None;
            this.tbMagSamples.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagSamples.ForeColor = System.Drawing.Color.Black;
            this.tbMagSamples.Location = new System.Drawing.Point(47, 114);
            this.tbMagSamples.Multiline = true;
            this.tbMagSamples.Name = "tbMagSamples";
            this.tbMagSamples.ReadOnly = true;
            this.tbMagSamples.Size = new System.Drawing.Size(45, 20);
            this.tbMagSamples.TabIndex = 163;
            this.tbMagSamples.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMinX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMinX.BorderStyle = BorderStyle.None;
            this.tbMinX.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMinX.ForeColor = System.Drawing.Color.Black;
            this.tbMinX.Location = new System.Drawing.Point(21, 35);
            this.tbMinX.Multiline = true;
            this.tbMinX.Name = "tbMinX";
            this.tbMinX.ReadOnly = true;
            this.tbMinX.Size = new System.Drawing.Size(46, 20);
            this.tbMinX.TabIndex = 152;
            this.tbMinX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(73, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 15);
            this.label9.TabIndex = 162;
            this.label9.Text = "Max";
            this.label9.TextAlign = ContentAlignment.MiddleRight;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(26, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 15);
            this.label12.TabIndex = 161;
            this.label12.Text = "Min";
            this.label12.TextAlign = ContentAlignment.MiddleRight;
            this.label12.Click += new EventHandler(this.label12_Click);
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(4, 89);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 15);
            this.label13.TabIndex = 160;
            this.label13.Text = "Z";
            this.label13.TextAlign = ContentAlignment.MiddleRight;
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(4, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(14, 15);
            this.label14.TabIndex = 159;
            this.label14.Text = "Y";
            this.label14.TextAlign = ContentAlignment.MiddleRight;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(4, 37);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(14, 15);
            this.label15.TabIndex = 158;
            this.label15.Text = "X";
            this.label15.TextAlign = ContentAlignment.MiddleRight;
            this.tbMaxZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMaxZ.BorderStyle = BorderStyle.None;
            this.tbMaxZ.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMaxZ.ForeColor = System.Drawing.Color.Black;
            this.tbMaxZ.Location = new System.Drawing.Point(73, 87);
            this.tbMaxZ.Multiline = true;
            this.tbMaxZ.Name = "tbMaxZ";
            this.tbMaxZ.ReadOnly = true;
            this.tbMaxZ.Size = new System.Drawing.Size(44, 20);
            this.tbMaxZ.TabIndex = 157;
            this.tbMaxZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMinZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMinZ.BorderStyle = BorderStyle.None;
            this.tbMinZ.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMinZ.ForeColor = System.Drawing.Color.Black;
            this.tbMinZ.Location = new System.Drawing.Point(21, 87);
            this.tbMinZ.Multiline = true;
            this.tbMinZ.Name = "tbMinZ";
            this.tbMinZ.ReadOnly = true;
            this.tbMinZ.Size = new System.Drawing.Size(46, 20);
            this.tbMinZ.TabIndex = 156;
            this.tbMinZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMaxY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMaxY.BorderStyle = BorderStyle.None;
            this.tbMaxY.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMaxY.ForeColor = System.Drawing.Color.Black;
            this.tbMaxY.Location = new System.Drawing.Point(73, 61);
            this.tbMaxY.Multiline = true;
            this.tbMaxY.Name = "tbMaxY";
            this.tbMaxY.ReadOnly = true;
            this.tbMaxY.Size = new System.Drawing.Size(44, 20);
            this.tbMaxY.TabIndex = 155;
            this.tbMaxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMinY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMinY.BorderStyle = BorderStyle.None;
            this.tbMinY.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMinY.ForeColor = System.Drawing.Color.Black;
            this.tbMinY.Location = new System.Drawing.Point(21, 61);
            this.tbMinY.Multiline = true;
            this.tbMinY.Name = "tbMinY";
            this.tbMinY.ReadOnly = true;
            this.tbMinY.Size = new System.Drawing.Size(46, 20);
            this.tbMinY.TabIndex = 154;
            this.tbMinY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMaxX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMaxX.BorderStyle = BorderStyle.None;
            this.tbMaxX.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMaxX.ForeColor = System.Drawing.Color.Black;
            this.tbMaxX.Location = new System.Drawing.Point(73, 35);
            this.tbMaxX.Multiline = true;
            this.tbMaxX.Name = "tbMaxX";
            this.tbMaxX.ReadOnly = true;
            this.tbMaxX.Size = new System.Drawing.Size(44, 20);
            this.tbMaxX.TabIndex = 153;
            this.tbMaxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.btSave.DialogResult = DialogResult.Cancel;
            this.btSave.Location = new System.Drawing.Point(11, 176);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(110, 23);
            this.btSave.TabIndex = 151;
            this.btSave.Text = "Save Calibration";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new EventHandler(this.btSave_Click);
            this.tabControl1.Controls.Add((Control)this.tabPage1);
            this.tabControl1.Controls.Add((Control)this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(309, 102);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(335, 553);
            this.tabControl1.TabIndex = 154;
            this.tabPage1.Controls.Add((Control)this.tbTemps);
            this.tabPage1.Controls.Add((Control)this.lbAxis1);
            this.tabPage1.Controls.Add((Control)this.lbAxis3);
            this.tabPage1.Controls.Add((Control)this.tbMonitor);
            this.tabPage1.Controls.Add((Control)this.lbAxis4);
            this.tabPage1.Controls.Add((Control)this.lbTemps);
            this.tabPage1.Controls.Add((Control)this.lbAxis2);
            this.tabPage1.Controls.Add((Control)this.elementHost1);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(327, 525);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Head";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.elementHost1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.elementHost1.CausesValidation = false;
            this.elementHost1.Enabled = false;
            this.elementHost1.ForeColor = System.Drawing.Color.FloralWhite;
            this.elementHost1.Location = new System.Drawing.Point(6, 10);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(314, 343);
            this.elementHost1.TabIndex = 30;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = (UIElement)this.edtracker1;
            this.tabPage2.Controls.Add((Control)this.groupBox2);
            this.tabPage2.Controls.Add((Control)this.gbMagCalib);
            this.tabPage2.Controls.Add((Control)this.elementHost2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(327, 527);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Magnetometer";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.groupBox2.CausesValidation = false;
            this.groupBox2.Controls.Add((Control)this.tbMagMat9);
            this.groupBox2.Controls.Add((Control)this.tbMagMat8);
            this.groupBox2.Controls.Add((Control)this.tbMagMat7);
            this.groupBox2.Controls.Add((Control)this.tbMagMat6);
            this.groupBox2.Controls.Add((Control)this.tbMagMat5);
            this.groupBox2.Controls.Add((Control)this.tbMagMat4);
            this.groupBox2.Controls.Add((Control)this.tbMagMat1);
            this.groupBox2.Controls.Add((Control)this.tbMagMat2);
            this.groupBox2.Controls.Add((Control)this.tbMagMat3);
            this.groupBox2.Controls.Add((Control)this.btSave);
            this.groupBox2.Location = new System.Drawing.Point(186, 315);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(134, 205);
            this.groupBox2.TabIndex = 154;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Matrix";
            this.tbMagMat9.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat9.BorderStyle = BorderStyle.None;
            this.tbMagMat9.CausesValidation = false;
            this.tbMagMat9.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat9.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat9.Location = new System.Drawing.Point(92, 80);
            this.tbMagMat9.Multiline = true;
            this.tbMagMat9.Name = "tbMagMat9";
            this.tbMagMat9.ReadOnly = true;
            this.tbMagMat9.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat9.TabIndex = 162;
            this.tbMagMat9.TabStop = false;
            this.tbMagMat9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMagMat8.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat8.BorderStyle = BorderStyle.None;
            this.tbMagMat8.CausesValidation = false;
            this.tbMagMat8.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat8.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat8.Location = new System.Drawing.Point(49, 80);
            this.tbMagMat8.Multiline = true;
            this.tbMagMat8.Name = "tbMagMat8";
            this.tbMagMat8.ReadOnly = true;
            this.tbMagMat8.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat8.TabIndex = 161;
            this.tbMagMat8.TabStop = false;
            this.tbMagMat8.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMagMat7.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat7.BorderStyle = BorderStyle.None;
            this.tbMagMat7.CausesValidation = false;
            this.tbMagMat7.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat7.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat7.Location = new System.Drawing.Point(6, 80);
            this.tbMagMat7.Multiline = true;
            this.tbMagMat7.Name = "tbMagMat7";
            this.tbMagMat7.ReadOnly = true;
            this.tbMagMat7.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat7.TabIndex = 160;
            this.tbMagMat7.TabStop = false;
            this.tbMagMat7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMagMat6.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat6.BorderStyle = BorderStyle.None;
            this.tbMagMat6.CausesValidation = false;
            this.tbMagMat6.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat6.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat6.Location = new System.Drawing.Point(92, 54);
            this.tbMagMat6.Multiline = true;
            this.tbMagMat6.Name = "tbMagMat6";
            this.tbMagMat6.ReadOnly = true;
            this.tbMagMat6.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat6.TabIndex = 159;
            this.tbMagMat6.TabStop = false;
            this.tbMagMat6.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMagMat5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat5.BorderStyle = BorderStyle.None;
            this.tbMagMat5.CausesValidation = false;
            this.tbMagMat5.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat5.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat5.Location = new System.Drawing.Point(49, 54);
            this.tbMagMat5.Multiline = true;
            this.tbMagMat5.Name = "tbMagMat5";
            this.tbMagMat5.ReadOnly = true;
            this.tbMagMat5.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat5.TabIndex = 158;
            this.tbMagMat5.TabStop = false;
            this.tbMagMat5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMagMat4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat4.BorderStyle = BorderStyle.None;
            this.tbMagMat4.CausesValidation = false;
            this.tbMagMat4.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat4.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat4.Location = new System.Drawing.Point(6, 54);
            this.tbMagMat4.Multiline = true;
            this.tbMagMat4.Name = "tbMagMat4";
            this.tbMagMat4.ReadOnly = true;
            this.tbMagMat4.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat4.TabIndex = 157;
            this.tbMagMat4.TabStop = false;
            this.tbMagMat4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMagMat1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat1.BorderStyle = BorderStyle.None;
            this.tbMagMat1.CausesValidation = false;
            this.tbMagMat1.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat1.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat1.Location = new System.Drawing.Point(6, 28);
            this.tbMagMat1.Multiline = true;
            this.tbMagMat1.Name = "tbMagMat1";
            this.tbMagMat1.ReadOnly = true;
            this.tbMagMat1.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat1.TabIndex = 0;
            this.tbMagMat1.TabStop = false;
            this.tbMagMat1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMagMat2.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat2.BorderStyle = BorderStyle.None;
            this.tbMagMat2.CausesValidation = false;
            this.tbMagMat2.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat2.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat2.Location = new System.Drawing.Point(49, 28);
            this.tbMagMat2.Multiline = true;
            this.tbMagMat2.Name = "tbMagMat2";
            this.tbMagMat2.ReadOnly = true;
            this.tbMagMat2.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat2.TabIndex = 155;
            this.tbMagMat2.TabStop = false;
            this.tbMagMat2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbMagMat3.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbMagMat3.BorderStyle = BorderStyle.None;
            this.tbMagMat3.CausesValidation = false;
            this.tbMagMat3.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.tbMagMat3.ForeColor = System.Drawing.Color.Black;
            this.tbMagMat3.Location = new System.Drawing.Point(92, 28);
            this.tbMagMat3.Multiline = true;
            this.tbMagMat3.Name = "tbMagMat3";
            this.tbMagMat3.ReadOnly = true;
            this.tbMagMat3.Size = new System.Drawing.Size(39, 20);
            this.tbMagMat3.TabIndex = 159;
            this.tbMagMat3.TabStop = false;
            this.tbMagMat3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.elementHost2.BackColor = System.Drawing.Color.White;
            this.elementHost2.CausesValidation = false;
            this.elementHost2.ImeMode = ImeMode.NoControl;
            this.elementHost2.Location = new System.Drawing.Point(6, 6);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(315, 300);
            this.elementHost2.TabIndex = 153;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = (UIElement)this.magCloud1;
            this.gbMagGyro.Controls.Add((Control)this.btMagResetView);
            this.gbMagGyro.Controls.Add((Control)this.btMagAutoBias);
            this.gbMagGyro.Controls.Add((Control)this.tbGyroX);
            this.gbMagGyro.Controls.Add((Control)this.lbGX);
            this.gbMagGyro.Controls.Add((Control)this.tbGyroZ);
            this.gbMagGyro.Controls.Add((Control)this.tbGyroY);
            this.gbMagGyro.Cursor = Cursors.Default;
            this.gbMagGyro.Location = new System.Drawing.Point(181, 269);
            this.gbMagGyro.Name = "gbMagGyro";
            this.gbMagGyro.Size = new System.Drawing.Size(293, 124);
            this.gbMagGyro.TabIndex = 157;
            this.gbMagGyro.TabStop = false;
            this.gbMagGyro.Text = "Auto Gyro Bias";
            this.btMagResetView.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btMagResetView.BackgroundImageLayout = ImageLayout.None;
            this.btMagResetView.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btMagResetView.FlatAppearance.BorderSize = 2;
            this.btMagResetView.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btMagResetView.Location = new System.Drawing.Point(7, 24);
            this.btMagResetView.Margin = new Padding(2, 3, 2, 3);
            this.btMagResetView.Name = "btMagResetView";
            this.btMagResetView.Size = new System.Drawing.Size(173, 23);
            this.btMagResetView.TabIndex = 168;
            this.btMagResetView.Text = "Reset View";
            this.btMagResetView.TextImageRelation = TextImageRelation.TextBeforeImage;
            this.btMagResetView.UseVisualStyleBackColor = true;
            this.btMagResetView.Click += new EventHandler(this.btMagResetView_Click);
            this.btMagAutoBias.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.btMagAutoBias.FlatAppearance.BorderColor = System.Drawing.Color.MidnightBlue;
            this.btMagAutoBias.FlatAppearance.BorderSize = 2;
            this.btMagAutoBias.FlatAppearance.MouseOverBackColor = System.Drawing.Color.CornflowerBlue;
            this.btMagAutoBias.Location = new System.Drawing.Point(7, 53);
            this.btMagAutoBias.Margin = new Padding(2, 3, 2, 3);
            this.btMagAutoBias.Name = "btMagAutoBias";
            this.btMagAutoBias.Size = new System.Drawing.Size(173, 23);
            this.btMagAutoBias.TabIndex = 167;
            this.btMagAutoBias.Text = "Auto Gyro Bias";
            this.btMagAutoBias.UseVisualStyleBackColor = true;
            this.btMagAutoBias.Click += new EventHandler(this.btMagAutoBias_Click);
            this.tbGyroX.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbGyroX.Location = new System.Drawing.Point(48, 85);
            this.tbGyroX.Name = "tbGyroX";
            this.tbGyroX.ReadOnly = true;
            this.tbGyroX.Size = new System.Drawing.Size(39, 21);
            this.tbGyroX.TabIndex = 166;
            this.tbGyroX.Text = "0";
            this.tbGyroX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.lbGX.AutoSize = true;
            this.lbGX.Location = new System.Drawing.Point(12, 87);
            this.lbGX.Name = "lbGX";
            this.lbGX.Size = new System.Drawing.Size(32, 15);
            this.lbGX.TabIndex = 165;
            this.lbGX.Text = "Gyro";
            this.lbGX.TextAlign = ContentAlignment.MiddleRight;
            this.tbGyroZ.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbGyroZ.Location = new System.Drawing.Point(137, 85);
            this.tbGyroZ.Name = "tbGyroZ";
            this.tbGyroZ.ReadOnly = true;
            this.tbGyroZ.Size = new System.Drawing.Size(39, 21);
            this.tbGyroZ.TabIndex = 164;
            this.tbGyroZ.Text = "0";
            this.tbGyroZ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbGyroY.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.tbGyroY.Location = new System.Drawing.Point(92, 85);
            this.tbGyroY.Name = "tbGyroY";
            this.tbGyroY.ReadOnly = true;
            this.tbGyroY.Size = new System.Drawing.Size(39, 21);
            this.tbGyroY.TabIndex = 162;
            this.tbGyroY.Text = "0";
            this.tbGyroY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.AutoScaleMode = AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.BackgroundImageLayout = ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(654, 662);
            this.Controls.Add((Control)this.gbMagGyro);
            this.Controls.Add((Control)this.tabControl1);
            this.Controls.Add((Control)this.gbHotKey);
            this.Controls.Add((Control)this.gbScaling);
            this.Controls.Add((Control)this.gbDriftComp);
            this.Controls.Add((Control)this.tbSketch);
            this.Controls.Add((Control)this.bMonitor);
            this.Controls.Add((Control)this.bWipeAll);
            this.Controls.Add((Control)this.groupBox1);
            this.Controls.Add((Control)this.gbBias);
            this.Controls.Add((Control)this.gbTrackerConfig);
            this.Controls.Add((Control)this.menuStrip1);
            this.Font = new Font("Arial", 9f, System.Drawing.FontStyle.Regular, GraphicsUnit.Point, (byte)0);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.HelpButton = true;
            this.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(670, 700);
            this.MinimumSize = new System.Drawing.Size(670, 700);
            this.Name = nameof(Form1);
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "EDTracker UI";
            this.groupBox1.ResumeLayout(false);
            this.gbBias.ResumeLayout(false);
            this.gbBias.PerformLayout();
            this.gbHotKey.ResumeLayout(false);
            this.gbHotKey.PerformLayout();
            this.gbTrackerConfig.ResumeLayout(false);
            this.gbTrackerConfig.PerformLayout();
            this.gbDriftComp.ResumeLayout(false);
            this.gbDriftComp.PerformLayout();
            this.gbScaling.ResumeLayout(false);
            this.gbScaling.PerformLayout();
            this.sliderSmoothing.EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.gbMagCalib.ResumeLayout(false);
            this.gbMagCalib.PerformLayout();
            this.tbarMagSens.EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gbMagGyro.ResumeLayout(false);
            this.gbMagGyro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
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
