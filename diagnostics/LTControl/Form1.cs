using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

        public MainForm()
        {
            InitializeComponent();
        }

        private void SetState(State state)
        {
            switch (state)
            {
                case State.NotConnected:
                    this.connectButton.Enabled = true;
                    this.Text = "ライントレーサ コントロール [未接続]";
                    this.statusTimer.Stop();
                    break;
                case State.Connecting:
                    this.connectButton.Enabled = false;
                    this.Text = "ライントレーサ コントロール [接続中...]";
                    break;
                case State.Connected:
                    this.connectButton.Enabled = false;
                    this.Text = "ライントレーサ コントロール [接続]";
                    this.statusTimer.Start();
                    break;
            }
        }
        private LineTracer lineTracer;
        private void connectButton_Click(object sender, EventArgs e)
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
    }
        
}