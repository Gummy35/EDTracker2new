﻿using ArduinoUploader;
using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace EDTrackerUI3
{
    public partial class FlashDialog : Form, IArduinoUploaderLogger
    {
        private string hexFile;
        private string port;
        private bool isBootloader;
        private SerialPortStream mySerialPort;
        private Stopwatch sw;

        public bool touchPort(string port, int baud)
        {
            if (this.mySerialPort == null)
                this.mySerialPort = new SerialPortStream(port);
            try
            {
                this.mySerialPort.Close();
                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                this.rtLog.AppendText("\nCould not close port " + port + ". " + ex.Message);
            }
            this.mySerialPort.Open();
            this.mySerialPort.BaudRate = baud;
            this.mySerialPort.Close();
            Thread.Sleep(100);
            this.mySerialPort.Dispose();
            return true;
        }

        private bool flashFirmware(string bootPort)
        {
            try
            {
                var options = new ArduinoSketchUploaderOptions()
                {
                    ArduinoModel = ArduinoUploader.Hardware.ArduinoModel.Mega1284,
                    FileName = hexFile,
                    PortName = bootPort
                };
                var progress = new Progress<double>(
                    p =>
                    {
                        progressBar1.Value = (int)(p * 100);
                        Info($"Upload progress: {p * 100:F1}% ...");
                    });

                var uploader = new ArduinoSketchUploader(options, this, progress);
                uploader.UploadSketch();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public FlashDialog(string hexFile, string port, bool isBootloader)
        {
            this.InitializeComponent();
            this.hexFile = hexFile;
            this.port = port;
            this.isBootloader = isBootloader;
            this.Shown += new EventHandler(this.ProgressForm_Shown);
        }

        private void ProgressForm_Shown(object sender, EventArgs e)
        {
            bool flag1 = false;
            string[] portNames1 = SerialPortStream.GetPortNames();
            this.progressBar1.Value = 1;
            this.sw = Stopwatch.StartNew();
            bool flag2;
            string bootPort;
            if (!this.isBootloader)
            {
                Info("Kick port " + this.port);
                this.touchPort(this.port, 1200);
                ++this.progressBar1.Value;
                Info("Wait for port " + this.port + " to drop");
                while (this.sw.ElapsedMilliseconds < 5000L && !flag1)
                {
                    if (((IEnumerable<string>)portNames1).Contains<string>(this.port))
                        flag1 = true;
                    else
                        Thread.Sleep(50);
                }
                ++this.progressBar1.Value;
                flag2 = false;
                bootPort = "none";
            }
            else
            {
                flag2 = true;
                bootPort = this.port;
                flag1 = true;
                Thread.Sleep(300);
            }
            if (flag1)
            {
                this.sw.Restart();
                Info("Wait for new port to appear...");
                ++this.progressBar1.Value;
                while (this.sw.ElapsedMilliseconds < 7000L && !flag2)
                {
                    string[] portNames2 = SerialPortStream.GetPortNames();
                    for (int index = 0; index < ((IEnumerable<string>)portNames2).Count<string>() && !flag2; ++index)
                    {
                        if (!((IEnumerable<string>)portNames1).Contains<string>(portNames2[index]))
                        {
                            flag2 = true;
                            bootPort = portNames2[index];
                            Info("Boot port found :" + bootPort);
                        }
                    }
                    Thread.Sleep(100);
                }
                ++this.progressBar1.Value;
                if (flag2)
                {
                    Info("Ready to flash to COM Port [" + bootPort + "]");
                    Thread.Sleep(20);
                    ++this.progressBar1.Value;
                    bool flag3 = this.flashFirmware(bootPort);
                    this.progressBar1.UseWaitCursor = false;
                    Cursor.Current = Cursors.Default;
                    Application.DoEvents();
                    if (flag3)
                    {
                        this.bClose.Enabled = true;
                        this.progressBar1.UseWaitCursor = false;
                        Cursor.Current = Cursors.Default;
                        Application.DoEvents();
                        Thread.Sleep(300);
                        this.Close();
                    }
                }
                else
                {
                    Info("Bootloader Port not found");
                }
            }
            else
            {
                Info("COM Port not dropped");
            }
            this.bClose.Enabled = true;
            this.progressBar1.UseWaitCursor = false;
            Cursor.Current = Cursors.Default;
            Application.DoEvents();
        }

        private void bClose_Click(object sender, EventArgs e) => this.Close();

        public void Error(string message, Exception exception)
        {
            rtLog.AppendText($"\nError : {message} : {exception.Message}");
            Application.DoEvents();
        }

        public void Warn(string message)
        {
            rtLog.AppendText($"\nWarn : {message}");
            Application.DoEvents();
        }

        public void Info(string message)
        {
            rtLog.AppendText($"\n{message}");
            Application.DoEvents();
        }

        public void Debug(string message)
        {
            rtLog.AppendText($"\nDebug : {message}");
            Application.DoEvents();
        }

        public void Trace(string message)
        {
            rtLog.AppendText($"\nTrace : {message}");
            Application.DoEvents();
        }
    }
}
