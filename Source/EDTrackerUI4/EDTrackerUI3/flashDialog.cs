// Decompiled with JetBrains decompiler
// Type: EDTrackerUI3.flashDialog
// Assembly: EDTrackerUI4, Version=4.0.4.0, Culture=neutral, PublicKeyToken=null
// MVID: 4F8CD7B6-F2A3-4E24-A180-C66D5752CCC0
// Assembly location: C:\Users\frue10674\Downloads\DIYEDTrackerUI404\DIYEDTrackerUI404\EDTrackerUI4.exe

using CommStudio.Connections;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace EDTrackerUI3
{
  public class flashDialog : Form
  {
    private string m_rawHEX;
    private string m_port;
    private bool m_isBootloader;
    private SerialConnection serialConnection1;
    private SerialPort mySerialPort;
    private Stopwatch sw;
    private IContainer components;
    private RichTextBox rtLog;
    private ProgressBar progressBar1;
    private Button bClose;

    public bool touchPort(string port, int baud)
    {
      if (this.mySerialPort == null)
        this.mySerialPort = new SerialPort(port);
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

    public void WriteByte(SerialPort p, byte data)
    {
      this.mySerialPort.Write(new byte[1]{ data }, 0, 1);
    }

    public void WriteChar(SerialPort p, char data) => this.WriteByte(p, Convert.ToByte(data));

    private int sendByteWait(byte b, int waitms)
    {
      this.sw.Restart();
      this.serialConnection1.Write(b);
      int num;
      for (num = this.serialConnection1.Read(); num == -1 && this.sw.ElapsedMilliseconds < (long) waitms; num = this.serialConnection1.Read())
        Thread.Sleep(10);
      return num;
    }

    private int sendBytesWait(byte[] b, int waitms)
    {
      this.serialConnection1.Write(b, 0, b.Length);
      this.sw.Restart();
      int num;
      for (num = this.serialConnection1.Read(); num == -1 && this.sw.ElapsedMilliseconds < (long) waitms; num = this.serialConnection1.Read())
        Thread.Sleep(10);
      return num;
    }

    private bool flashit(string bootPort)
    {
      bool flag = true;
      this.components = (IContainer) new System.ComponentModel.Container();
      this.serialConnection1 = new SerialConnection(this.components);
      ++this.progressBar1.Value;
      Application.DoEvents();
      try
      {
        this.rtLog.AppendText("\nOpen Bootloader COM Port...");
        this.serialConnection1.Options = new SerialOptions(bootPort, 57600, CommStudio.Connections.Parity.None, 8, CommStopBits.One, false, false, true, false, true, true);
        this.serialConnection1.ReadTimeOut = 0;
        this.serialConnection1.WriteTimeOut = 0;
        this.serialConnection1.Open();
        Thread.Sleep(50);
      }
      catch (IOException ex)
      {
        this.rtLog.AppendText(ex.Message);
      }
      ++this.progressBar1.Value;
      Application.DoEvents();
      this.serialConnection1.Write(Convert.ToByte('S'));
      Thread.Sleep(50);
      string str1 = this.serialConnection1.ReadLine((int) byte.MaxValue);
      if (str1.Contains("CATERIN"))
      {
        this.rtLog.AppendText("\nConnected to bootloader : reported software [" + str1 + "]");
      }
      else
      {
        flag = false;
        this.rtLog.AppendText("\nBootloader negotiation failed : reported software [" + str1 + "]");
      }
      Application.DoEvents();
      this.serialConnection1.Write(Convert.ToByte('V'));
      Thread.Sleep(50);
      this.rtLog.AppendText("\nReported version [" + (((char) this.serialConnection1.Read()).ToString() + "." + (object) (char) this.serialConnection1.Read()) + "]");
      ++this.progressBar1.Value;
      Application.DoEvents();
      this.serialConnection1.Write(Convert.ToByte('a'));
      Thread.Sleep(50);
      this.rtLog.AppendText("\nAutoAddrIncrement = " + (object) (char) this.serialConnection1.Read());
      Application.DoEvents();
      ++this.progressBar1.Value;
      Application.DoEvents();
      this.serialConnection1.Write(Convert.ToByte('b'));
      Thread.Sleep(50);
      char ch = (char) this.serialConnection1.Read();
      int num1 = 0;
      if (ch == 'Y')
        num1 = ((this.serialConnection1.Read() & (int) byte.MaxValue) << 8) + (this.serialConnection1.Read() & (int) byte.MaxValue);
      this.rtLog.AppendText("\nBuffSize = " + num1.ToString());
      ++this.progressBar1.Value;
      Application.DoEvents();
      this.serialConnection1.Write(Convert.ToByte('t'));
      Thread.Sleep(50);
      byte[] numArray = new byte[(int) byte.MaxValue];
      int num2 = this.serialConnection1.Read();
      int num3 = 0;
      while (num2 != -1 && num2 != 0)
      {
        numArray[num3++] = (byte) num2;
        num2 = this.serialConnection1.Read();
        Thread.Sleep(10);
      }
      ++this.progressBar1.Value;
      int num4 = (int) numArray[0];
      this.rtLog.AppendText("\nDevice List:");
      this.rtLog.AppendText(BitConverter.ToString(numArray).Substring(0, 3));
      Application.DoEvents();
      ++this.progressBar1.Value;
      int num5 = 3;
      int num6 = 0;
      while (num5 > 0)
      {
        this.serialConnection1.Write(new byte[2]
        {
          (byte) 84,
          (byte) 68
        }, 0, 2);
        this.sw.Start();
        for (num6 = this.serialConnection1.Read(); num6 == -1 && this.sw.ElapsedMilliseconds < 2000L; num6 = this.serialConnection1.Read())
          Thread.Sleep(10);
        if (num6 != 13)
          --num5;
        else
          num5 = 0;
      }
      if (num6 != 13)
      {
        this.rtLog.AppendText("\nBootloader responded with Error on selection of device: " + (object) num6);
        flag = false;
      }
      else
        this.rtLog.AppendText("\nBootloader responded with OK on selection of device");
      ++this.progressBar1.Value;
      Application.DoEvents();
      if (this.sendByteWait((byte) 80, 3000) != 13)
      {
        flag = false;
        this.rtLog.AppendText("\nBootloader responded with Error on set Programme mode");
      }
      else
        this.rtLog.AppendText("\nPROG mode set");
      ++this.progressBar1.Value;
      Application.DoEvents();
      if (this.sendByteWait((byte) 101, 10000) != 13)
      {
        this.rtLog.AppendText("\nBootloader responded with Error on Erase");
        flag = false;
      }
      else
        this.rtLog.AppendText("\nErased OK");
      ++this.progressBar1.Value;
      Application.DoEvents();
      if (this.sendByteWait((byte) 80, 3000) != 13)
      {
        this.rtLog.AppendText("\nBootloader responded with Error on set Programme mode");
        flag = false;
      }
      else
        this.rtLog.AppendText("\nPROG mode set");
      ++this.progressBar1.Value;
      Application.DoEvents();
      this.serialConnection1.Write(Convert.ToByte('A'));
      this.serialConnection1.Write((byte) 0);
      if (this.sendByteWait((byte) 0, 3000) != 13)
      {
        this.rtLog.AppendText("\nBootloader responded with Error on Set Address 0000");
        flag = false;
      }
      else
        this.rtLog.AppendText("\nAddress set to 0000");
      ++this.progressBar1.Value;
      Application.DoEvents();
      string rawHex = this.m_rawHEX;
      this.progressBar1.Value = 0;
      for (int index1 = 0; index1 < rawHex.Length; index1 += num1 * 2)
      {
        this.progressBar1.Value = (int) Math.Min(95.0, 99.0 * ((double) (index1 + 1) / (double) rawHex.Length));
        int num7 = index1 + num1 * 2 <= rawHex.Length ? num1 : (rawHex.Length - index1) / 2;
        byte[] b = new byte[4 + num7];
        b[0] = (byte) 66;
        b[1] = (byte) 0;
        b[2] = (byte) num7;
        b[3] = (byte) 70;
        for (int index2 = 0; index2 < num7 * 2; index2 += 2)
        {
          string str2 = rawHex.Substring(index1 + index2, 2);
          b[4 + index2 / 2] = Convert.ToByte(str2, 16);
        }
        if (this.sendBytesWait(b, 500) != 13)
        {
          this.rtLog.AppendText("\nBootloader responded with Error on Write Block");
          flag = false;
        }
        Application.DoEvents();
      }
      this.progressBar1.Value = 100;
      this.sendByteWait((byte) 76, 3000);
      Thread.Sleep(200);
      int num8 = this.sendByteWait((byte) 69, 3000);
      Thread.Sleep(200);
      if (num8 != 13)
      {
        this.rtLog.AppendText("\nBootloader responded with Error on END");
        flag = false;
      }
      else
        this.rtLog.AppendText("\nBootloader OK on END ");
      Application.DoEvents();
      this.serialConnection1.Write(Convert.ToByte('S'));
      Thread.Sleep(50);
      this.rtLog.AppendText("\nReported software [" + this.serialConnection1.ReadLine((int) byte.MaxValue) + "]");
      this.rtLog.AppendText("\nClosing port");
      Application.DoEvents();
      this.serialConnection1.Close();
      this.serialConnection1.Dispose();
      if (flag)
      {
        this.rtLog.AppendText("\n** FLASH COMPLETED **");
        Application.DoEvents();
        Thread.Sleep(1000);
        return true;
      }
      this.rtLog.AppendText("\n** ERRORS DETECTED DURING FLASH **");
      Application.DoEvents();
      Thread.Sleep(1000);
      return false;
    }

    public flashDialog(string HEX, string port, bool isBootloader)
    {
      this.InitializeComponent();
      this.m_rawHEX = HEX;
      this.m_port = port;
      this.m_isBootloader = isBootloader;
      this.Shown += new EventHandler(this.ProgressForm_Shown);
    }

    private void ProgressForm_Shown(object sender, EventArgs e)
    {
      bool flag1 = false;
      string[] portNames1 = SerialPort.GetPortNames();
      this.progressBar1.Value = 1;
      this.sw = Stopwatch.StartNew();
      bool flag2;
      string bootPort;
      if (!this.m_isBootloader)
      {
        this.rtLog.AppendText("\nKick port " + this.m_port);
        Application.DoEvents();
        this.touchPort(this.m_port, 1200);
        ++this.progressBar1.Value;
        this.rtLog.AppendText("\nWait for port " + this.m_port + " to drop");
        Application.DoEvents();
        while (this.sw.ElapsedMilliseconds < 5000L && !flag1)
        {
          if (((IEnumerable<string>) portNames1).Contains<string>(this.m_port))
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
        bootPort = this.m_port;
        flag1 = true;
        Thread.Sleep(300);
      }
      if (flag1)
      {
        this.sw.Restart();
        this.rtLog.AppendText("\nWait for new port to appear...");
        Application.DoEvents();
        ++this.progressBar1.Value;
        while (this.sw.ElapsedMilliseconds < 7000L && !flag2)
        {
          string[] portNames2 = SerialPort.GetPortNames();
          for (int index = 0; index < ((IEnumerable<string>) portNames2).Count<string>() && !flag2; ++index)
          {
            if (!((IEnumerable<string>) portNames1).Contains<string>(portNames2[index]))
            {
              flag2 = true;
              bootPort = portNames2[index];
              this.rtLog.AppendText("\nBoot port found :" + bootPort);
              Application.DoEvents();
            }
          }
          Thread.Sleep(100);
        }
        ++this.progressBar1.Value;
        if (flag2)
        {
          this.rtLog.AppendText("\nReady to flash to COM Port [" + bootPort + "]");
          Application.DoEvents();
          Thread.Sleep(20);
          ++this.progressBar1.Value;
          bool flag3 = this.flashit(bootPort);
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
          this.rtLog.AppendText("\nBootloader Port not found");
          Application.DoEvents();
        }
      }
      else
      {
        this.rtLog.AppendText("\nCOM Port not dropped");
        Application.DoEvents();
      }
      this.bClose.Enabled = true;
      this.progressBar1.UseWaitCursor = false;
      Cursor.Current = Cursors.Default;
      Application.DoEvents();
    }

    private void bClose_Click(object sender, EventArgs e) => this.Close();

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

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
            Name = "flashDialog";
            Text = "Flash EDTracker";
            TopMost = true;
            ResumeLayout(false);
        }
    }
}
