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

namespace EDTrackerUI3
{
    public partial class MainForm : Form
    {
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
        private DebugForm debugForm;
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
        private LowPassFilter lpX = new LowPassFilter(0.98f);
        private LowPassFilter lpY = new LowPassFilter(0.98f);
        private LowPassFilter lpZ = new LowPassFilter(0.98f);
        private LowPassFilter aX = new LowPassFilter(0.93f);
        private LowPassFilter aY = new LowPassFilter(0.93f);
        private LowPassFilter aZ = new LowPassFilter(0.93f);
        private LowPassFilter gX = new LowPassFilter(0.992f);
        private LowPassFilter gY = new LowPassFilter(0.992f);
        private LowPassFilter gZ = new LowPassFilter(0.992f);
        private LowPassFilter magLP = new LowPassFilter(0.98f);
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
        private static MainForm.myStateType myState = MainForm.myStateType.UNKNOWN;
        private MainForm.DoubleBufferedPanel buffPanel1;
        private MainForm.DoubleBufferedPanel buffPanel2;
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
        private MainForm.myStateType preMinimiseState;
        private ContextMenuStrip contextMenu1;
        private ToolStripMenuItem menuItem1;
        private bool biasHasChanged;
        private Dictionary<string, Keys> hotKeyList = new Dictionary<string, Keys>();
        private BiasCalc fCalBias;
        private bool waitingForBias;
        private bool gBiasGood;
        private bool aBiasGood;
        private bool hotKeyEnabled;
        private string lastKnownPort = "UNKNOWN";
        private string lastSelectAutoCentreOption = "UNKNOWN";
        private bool centringSuppressed;
        private bool firstTimeWarning;
        private MagForm mf;
        private EDTrackerWpfControls.HeadTracker edtracker1;
        private EDTrackerWpfControls.MagCloud magCloud1;

        public MainForm()
        {
            this.buffPanel1 = new MainForm.DoubleBufferedPanel();
            this.buffPanel1.BackColor = System.Drawing.Color.Black;
            this.buffPanel1.Location = new System.Drawing.Point(6, 355);
            this.buffPanel1.Name = "panel1";
            this.buffPanel1.Size = new System.Drawing.Size(315, 180);
            this.buffPanel1.TabIndex = 58;
            this.buffPanel1.Paint += new PaintEventHandler(this.panel1_Paint);
            this.buffPanel2 = new MainForm.DoubleBufferedPanel();
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
            this.setMyState(MainForm.myStateType.UNKNOWN);
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
            //WebClient webClient = new WebClient();
            //webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(this.DownloadStringCompleted);
            //webClient.DownloadStringAsync(new Uri(this.edtUpdateURL));
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
                EDFlash edflash = new EDFlash();
                try
                {
                    EDFlashDownload[] download = ((EDFlash)new XmlSerializer(typeof(EDFlash)).Deserialize((XmlReader)new XmlTextReader((TextReader)new StringReader(result)))).download;
                    MainForm.hexfiles = new StringCollection();
                    this.cbSketches.Items.Clear();
                    string[] strArray = System.Windows.Forms.Application.ProductVersion.Split('.');
                    int num2 = 10000 * (int)short.Parse(strArray[0]) + 100 * (int)short.Parse(strArray[1]) + (int)short.Parse(strArray[2]);
                    foreach (EDFlashDownload edflashDownload in download)
                    {
                        int num3 = 10000 * (int)short.Parse(edflashDownload.minimumgui.major) + 100 * (int)short.Parse(edflashDownload.minimumgui.minor) + (int)short.Parse(edflashDownload.minimumgui.patch);
                        int num4 = 10000 * (int)short.Parse(edflashDownload.major) + 100 * (int)short.Parse(edflashDownload.minor) + (int)short.Parse(edflashDownload.patch);
                        if (num2 >= num3 && (((IEnumerable<string>)edflashDownload.validhardware).Contains<string>("6050") || ((IEnumerable<string>)edflashDownload.validhardware).Contains<string>("9150") && num4 >= 40003) && (this.userMode == "DEV" || this.userMode == "TEST" && edflashDownload.releasestate != "DEV" || this.userMode == "USER" && edflashDownload.releasestate == "PRD"))
                        {
                            this.cbSketches.Items.Add((object)(edflashDownload.name.PadRight(20) + edflashDownload.major + "." + edflashDownload.minor + "." + edflashDownload.patch));
                            MainForm.hexfiles.Add(edflashDownload.imageurl);
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
                new ClosingForm().Show((IWin32Window)this);
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
                    this.hotkey?.Dispose();
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

        private void setMyState(MainForm.myStateType state)
        {
            MainForm.myState = state;
            this.tbMonitor.Text = MainForm.myState.ToString();
            if (state == MainForm.myStateType.MONITORING)
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
                if (MainForm.myState != MainForm.myStateType.DISCONNECTED)
                    this.disconnectArduino();
                string port = this.cbPort.Text.Substring(0, this.cbPort.Text.IndexOf(' '));
                bool isBootloader = this.cbPort.Text.IndexOf("bootloader") >= 0;
                try
                {
                    string hexfile = MainForm.hexfiles[this.cbSketches.SelectedIndex];
                    FlashDialog flashDialog = new FlashDialog(hexfile, port, isBootloader);
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
            catch (TimeoutException timeoutException)
            {
                // We are out of sync with controller. Send a stop, clear buffers, and restart
                this.ardPort.Write("S");
                Thread.Sleep(100);
                serialPort.DiscardInBuffer();
                serialPort.DiscardOutBuffer();
                this.ardPort.Write("H");
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
        private static bool isProcessing = false;
        private void processArduinoData(string datain)
        {
            if (datain.Length == 0)
                return;
            lock (_lock)
            {
                if (isProcessing)
                    return;
                isProcessing = true;
            }
            char ch = datain[0];
            try
            {
                switch (ch)
                {
                    case 'H':
                        MainForm.myState = MainForm.myStateType.CONNECTED;
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
                        this.setMyState(MainForm.myStateType.MONITORING);
                        break;
                    case 'a':
                        this.adjustX = this.adjustY = this.adjustZ = 1;
                        break;
                    case 'h':
                        MainForm.myState = MainForm.myStateType.CONNECTED;
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
                            if (MainForm.myState != MainForm.myStateType.MONITORING)
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
            lock (_lock)
            {
                isProcessing = false;
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
            this.setMyState(MainForm.myStateType.DISCONNECTED);
        }

        private void bMonitor_Click(object sender, EventArgs e)
        {
            if (MainForm.myState == MainForm.myStateType.MONITORING || MainForm.myState == MainForm.myStateType.CONNECTED)
            {
                if (MainForm.myState == MainForm.myStateType.MONITORING)
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
                this.setMyState(MainForm.myStateType.DISCONNECTED);
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
                    this.setMyState(MainForm.myStateType.CONNECTED);
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
                this.fCalBias = new BiasCalc();
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
                if (MainForm.myState != MainForm.myStateType.CONNECTED)
                {
                    if (MainForm.myState != MainForm.myStateType.MONITORING)
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
                this.preMinimiseState = MainForm.myState;
                if (MainForm.myState == MainForm.myStateType.MONITORING)
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
                if (this.preMinimiseState != MainForm.myStateType.MONITORING)
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
                this.debugForm = new DebugForm();
            else if (this.debugForm.IsDisposed)
                this.debugForm = new DebugForm();
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
                this.fCalBias = new BiasCalc();
            this.waitingForBias = true;
            this.fCalBias.Text = "Calculating Gyro Bias. Keep EDTracker Stationary.";
            this.fCalBias.StartPosition = FormStartPosition.CenterParent;
            this.ardPort.Write("r\n");
            this.fCalBias.Show();
        }

        private void btMagAutoBias_Click(object sender, EventArgs e) => this.waitForCalibToFinish();

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e) => new AboutBox().Show();

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