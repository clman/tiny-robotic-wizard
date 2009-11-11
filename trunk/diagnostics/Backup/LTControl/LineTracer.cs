using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using DeviceIOLib;
using HidLib;

namespace LTControl
{
    public class LineTracer : IDisposable
    {
        public const int DefaultVendorId = 0x16c0;
        public const int DefaultProductId = 0x05df;
        public const string DefaultVendorString = "rohm.drm.doshisha.ac.jp";
        public const string DefaultProductString = "LTRACE";
        public const int SensorReportId = 0x01;
        public const int MotorReportId = 0x02;
        public const int SensorCount = 3;

        /// <summary>
        /// LTRACEデバイスを列挙し，そのデバイスのパスを返す．
        /// </summary>
        /// <returns>
        /// </returns>
        public static String[] Enumerate()
        {
            List<String> targetDevices = new List<string>();
            String[] devices = DeviceEnumerator.EnumDevices(HID.Guid);
            foreach (String device in devices)
            {
                try
                {
                    HID hid = new HID(device);
                    if (hid.VendorId == DefaultVendorId && hid.ProductId == DefaultProductId)
                    {
                        if( hid.VendorString == DefaultVendorString && hid.ProductString == DefaultProductString )
                            targetDevices.Add(device);
                    }
                }
                catch (Exception)
                {
                }
            }
            return targetDevices.ToArray();
        }

        private HID hid;
        private Stream sensorReport;
        private Stream motorReport;
        private int[] sensorValue = new int[SensorCount];
        private int[] sensorMax = new int[SensorCount];
        private int[] sensorMin = new int[SensorCount];

        private int motorModeL;
        private int motorOutputL;
        private int motorModeR;
        private int motorOutputR;

        private bool run = false;

        public LineTracer(String devicePath)
        {
            this.hid = new HID(devicePath);
            this.Initialize();
        }
        public LineTracer(HID device)
        {
            this.hid = device;
            this.Initialize();
        }

        public int GetSensorValue(int channel)
        {
            return this.GetSensorValue(channel, true);
        }
        public int GetSensorValue(int channel, bool update)
        {
            if( update ) this.UpdateSensor();
            return this.sensorValue[channel];
        }

        public int GetNormalizedValue(int channel)
        {
            int value = this.GetSensorValue(channel, false);
            if (value > this.sensorMax[channel]) return 255;
            if (value <= this.sensorMin[channel]) return 0;

            return (value - this.sensorMin[channel]) * 255 / (this.sensorMax[channel] - this.sensorMin[channel]);
        }

        public int[] SensorMax
        {
            get { return this.sensorMax; }
        }

        public int[] SensorMin
        {
            get { return this.sensorMin; }
        }

        public void UpdateSensor()
        {
            byte[] buf = new byte[this.hid[SensorReportId].FeatureReportLength];
            this.sensorReport.Read(buf, 0, buf.Length);
            for (int i = 0; i < SensorCount; i++)
                this.sensorValue[i] = (int)buf[i];
        }

        public void SetMotorOutput(bool right, int output, bool brake)
        {
            if (!right)
            {
                if (brake)
                {
                    this.motorModeL = 2;
                }
                else
                {
                    if (output < 0)
                    {
                        this.motorModeL = 0;
                        this.motorOutputL = -output;
                    }
                    else
                    {
                        this.motorModeL = 1;
                        this.motorOutputL = output;
                    }
                }
            }
            else
            {
                if (brake)
                {
                    this.motorModeR = 2;
                }
                else
                {
                    if (output < 0)
                    {
                        this.motorModeR = 0;
                        this.motorOutputR = -output;
                    }
                    else
                    {
                        this.motorModeR = 1;
                        this.motorOutputR = output;
                    }
                }
            }
            this.UpdateMotor();
        }

        public void Run()
        {
            this.run = true;
            this.UpdateMotor();
        }

        private void UpdateMotor()
        {
            byte[] buf = new byte[this.hid[MotorReportId].FeatureReportLength];
            buf[0] = (byte)this.motorModeL;
            buf[1] = (byte)this.motorOutputL;
            buf[2] = (byte)this.motorModeR;
            buf[3] = (byte)this.motorOutputR;
            buf[4] = (byte)(this.run ? 1 : 0);
            this.motorReport.Write(buf, 0, buf.Length);
        }

        private void Initialize()
        {
            this.sensorReport = this.hid.GetFeatureStream(SensorReportId);
            this.motorReport = this.hid.GetFeatureStream(MotorReportId);

            for (int i = 0; i < SensorCount; i++)
            {
                this.sensorMin[i] = 0;
                this.sensorMax[i] = 255;
            }
        }

        #region IDisposable メンバ

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.sensorReport.Dispose();
                    this.hid.Dispose();
                }

                this.disposed = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~LineTracer()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
