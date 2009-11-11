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

        private TextBox[] sensorMinText = new TextBox[LineTracer.SensorCount];
        private TextBox[] sensorMaxText = new TextBox[LineTracer.SensorCount];
        private TextBox[] sensorValueText = new TextBox[LineTracer.SensorCount];
        private TextBox[] sensorNormalizedText = new TextBox[LineTracer.SensorCount];

        public MainForm()
        {
            InitializeComponent();

            for (int i = 0; i < LineTracer.SensorCount; i++)
            {
                this.sensorMinText[i] = (TextBox)this.Controls["sensorMin" + i.ToString()];
                this.sensorMaxText[i] = (TextBox)this.Controls["sensorMax" + i.ToString()];
                this.sensorValueText[i] = (TextBox)this.Controls["sensorValue" + i.ToString()];
                this.sensorNormalizedText[i] = (TextBox)this.Controls["sensorNormalized" + i.ToString()];
            }
        }

        private void SetState(State state)
        {
            switch (state)
            {
                case State.NotConnected:
                    this.connectButton.Enabled = true;
                    this.Text = "ライントレーサ コントロール [未接続]";
                    this.sensorTimer.Stop();
                    break;
                case State.Connecting:
                    this.connectButton.Enabled = false;
                    this.Text = "ライントレーサ コントロール [接続中...]";
                    break;
                case State.Connected:
                    this.connectButton.Enabled = false;
                    this.Text = "ライントレーサ コントロール [接続]";
                    this.sensorTimer.Start();
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

        private void sensorTimer_Tick(object sender, EventArgs e)
        {
            if (this.lineTracer != null)
            {
                this.lineTracer.UpdateSensor();
                for (int i = 0; i < LineTracer.SensorCount; i++)
                {
                    this.sensorMinText[i].Text = this.lineTracer.SensorMin[i].ToString();
                    this.sensorMaxText[i].Text = this.lineTracer.SensorMax[i].ToString();
                    this.sensorValueText[i].Text = this.lineTracer.GetSensorValue(i, false).ToString();
                    this.sensorNormalizedText[i].Text = this.lineTracer.GetNormalizedValue(i).ToString();
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SetState(State.NotConnected);
        }

        private void motorOutputLTrackbar_Scroll(object sender, EventArgs e)
        {
            if (this.lineTracer != null)
            {
                this.lineTracer.SetMotorOutput(false, this.motorOutputLTrackbar.Value, false);
            }
        }

        private void brakeLButton_Click(object sender, EventArgs e)
        {
            if (this.lineTracer != null)
            {
                this.motorOutputLTrackbar.Value = 0;
                this.lineTracer.SetMotorOutput(false, 0, true);
            }
        }

        private void setLowerButton_Click(object sender, EventArgs e)
        {
            if (this.lineTracer != null)
            {
                this.lineTracer.UpdateSensor();
                for (int i = 0; i < LineTracer.SensorCount; i++)
                {
                    this.lineTracer.SensorMin[i] = this.lineTracer.GetSensorValue(i, false);
                }
            }
        }

        private void setUpperButton_Click(object sender, EventArgs e)
        {
            if (this.lineTracer != null)
            {
                this.lineTracer.UpdateSensor();
                for (int i = 0; i < LineTracer.SensorCount; i++)
                {
                    this.lineTracer.SensorMax[i] = this.lineTracer.GetSensorValue(i, false);
                }
            }
        }

        private void motorOutputRTrackbar_Scroll(object sender, EventArgs e)
        {
            if (this.lineTracer != null)
            {
                this.lineTracer.SetMotorOutput(true, this.motorOutputRTrackbar.Value, false);
            }
        }

        private void brakeRButton_Click(object sender, EventArgs e)
        {
            if (this.lineTracer != null)
            {
                this.motorOutputRTrackbar.Value = 0;
                this.lineTracer.SetMotorOutput(true, 0, true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.lineTracer != null)
            {
                this.lineTracer.Run();
            }
        }
    }
}