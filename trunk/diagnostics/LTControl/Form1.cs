using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using AvrLib.Image;
using HidBootLib;

namespace LTControl
{
    public partial class MainForm : Form
    {
        private enum State
        {
            NotConnected,
            Connecting,
            Connected,
        }

        private State state;

        public MainForm()
        {
            InitializeComponent();

            this.SetState(State.NotConnected);
        }

        private void SetState(State state)
        {
            switch (state)
            {
                case State.NotConnected:
                    this.connectButton.Enabled = true;
                    this.connectButton.Text = "接続";
                    this.writeButton.Enabled = true;
                    this.ledGroup.Enabled = false;
                    this.sensorGroup.Enabled = false;
                    this.leftWheelGroup.Enabled = false;
                    this.rightWheelGroup.Enabled = false;

                    this.Text = "ライントレーサ コントロール [未接続]";
                    this.statusTimer.Stop();
                    break;
                case State.Connecting:
                    this.connectButton.Enabled = false;
                    this.connectButton.Text = "接続";
                    this.writeButton.Enabled = false;
                    this.ledGroup.Enabled = false;
                    this.sensorGroup.Enabled = false;
                    this.leftWheelGroup.Enabled = false;
                    this.rightWheelGroup.Enabled = false;

                    this.Text = "ライントレーサ コントロール [接続中...]";
                    break;
                case State.Connected:
                    this.connectButton.Enabled = true;
                    this.connectButton.Text = "切断";
                    this.writeButton.Enabled = false;
                    this.ledGroup.Enabled = true;
                    this.sensorGroup.Enabled = true;
                    this.leftWheelGroup.Enabled = true;
                    this.rightWheelGroup.Enabled = true;

                    this.Text = "ライントレーサ コントロール [接続]";
                    this.statusTimer.Start();
                    break;
            }

            this.state = state;
        }
        private LineTracer lineTracer;
        private void connectButton_Click(object sender, EventArgs e)
        {
            if (this.state == State.NotConnected)
            {
                String[] devices = LineTracer.Enumerate();
                if (devices.Length == 0)
                {
                    MessageBox.Show("ライントレーサが接続されていません．接続を確認してください．");
                    return;
                }
                this.SetState(State.Connecting);
                try
                {
                    this.lineTracer = new LineTracer(devices[0]);
                    this.SetState(State.Connected);
                }
                catch (Exception)
                {
                    if (this.lineTracer != null)
                        this.lineTracer.Dispose();
                    this.lineTracer = null;
                    this.SetState(State.NotConnected);
                }
            }
            else if (this.state == State.Connected)
            {
                try
                {
                    this.lineTracer.Dispose();
                }
                catch (Exception)
                {
                }
                this.lineTracer = null;
                this.SetState(State.NotConnected);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SetState(State.NotConnected);
        }

        private void ChangeMotorModeButton_Click(object sender, EventArgs e)
        {
            string name = ((Control)sender).Name;
            string modeName = name.Substring(0, name.Length - 7);
            string position = name.Substring(name.Length - 7, 1);
            LineTracer.MotorMode mode = LineTracer.MotorMode.Stop;
            if (modeName == "stop")
                mode = LineTracer.MotorMode.Stop;
            else if (modeName == "forward")
                mode = LineTracer.MotorMode.Forward;
            else if (modeName == "backward")
                mode = LineTracer.MotorMode.Backward;
            else
                mode = LineTracer.MotorMode.Brake;

            if (position == "L")
                this.lineTracer.MotorL = mode;
            else
                this.lineTracer.MotorR = mode;
        }

        private void redCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.lineTracer.LedRed = ((CheckBox)sender).Checked;
        }

        private void greenCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.lineTracer.LedGreen = ((CheckBox)sender).Checked;
        }

        private void blueCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.lineTracer.LedBlue = ((CheckBox)sender).Checked;
        }

        private void statusTimer_Tick(object sender, EventArgs e)
        {
            if (this.lineTracer == null) return;
            this.lineLCheck.Checked = this.lineTracer.LineL;
            this.lineRCheck.Checked = this.lineTracer.LineR;
            this.distanceText.Text = this.lineTracer.Distance.ToString();
        }

        private void writeButton_Click(object sender, EventArgs e)
        {
            try
            {
                SparseImage image;
                using (FileStream stream = new FileStream("firmware.hex", FileMode.Open))
                {
                    image = HexLoader.Load(stream);
                }
                byte[] block = image.ToBlockImage();
                string[] devicePaths = HidBoot.Enumerate();
                if (devicePaths.Length == 0)
                {
                    throw new Exception("ブートローダが見つかりません．");
                }
                HidBoot device = new HidBoot(devicePaths[0]);
                device.WriteApplication(block, (int)image.MinimumAddress, (int)image.MaximumAddress);
                device.RunApplication();
                MessageBox.Show("書き込み完了", "ファームウェア書き込み", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.state == State.Connected)
            {
                if (e.KeyCode == Keys.Left)
                {
                    this.lineTracer.MotorL = LineTracer.MotorMode.Backward;
                    this.lineTracer.MotorR = LineTracer.MotorMode.Forward;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    this.lineTracer.MotorL = LineTracer.MotorMode.Forward;
                    this.lineTracer.MotorR = LineTracer.MotorMode.Backward;
                }
                else if (e.KeyCode == Keys.Up)
                {
                    this.lineTracer.MotorL = LineTracer.MotorMode.Forward;
                    this.lineTracer.MotorR = LineTracer.MotorMode.Forward;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    this.lineTracer.MotorL = LineTracer.MotorMode.Backward;
                    this.lineTracer.MotorR = LineTracer.MotorMode.Backward;
                }
                else if (e.KeyCode == Keys.Space)
                {
                    this.lineTracer.MotorL = LineTracer.MotorMode.Brake;
                    this.lineTracer.MotorR = LineTracer.MotorMode.Brake;
                }
            }
        }
    }
        
}