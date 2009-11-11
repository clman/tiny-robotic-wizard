using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using DeviceIO;
using DeviceIO.Hid;

namespace LTControl
{
    public class LineTracer : IDisposable
    {
        public const int DefaultVendorId = 0x16c0;
        public const int DefaultProductId = 0x05df;
        public const string DefaultVendorString = "rohm.drm.doshisha.ac.jp";
        public const string DefaultProductString = "LTRACE";
        public const int StatusReportId = 0x01;
        public const int CommandReportId = 0x02;
        
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
        private Stream statusReport;
        private Stream commandReport;

        /// <summary>
        /// モータのモードを表す
        /// </summary>
        public enum MotorMode : byte
        {
            Stop = 0,
            Forward = 1,
            Backward = 2,
            Brake = 3,
        }

        
        private MotorMode motorL = MotorMode.Stop;
        private MotorMode motorR = MotorMode.Stop;

        private bool ledRed = false;
        private bool ledGreen = false;
        private bool ledBlue = false;

        private bool lineL = false;
        private bool lineR = false;
        private int distance = 0;

        public bool LedRed
        {
            get { return this.ledRed; }
            set { this.ledRed = value; this.SendCommand(); }
        }
        public bool LedGreen
        {
            get { return this.ledGreen; }
            set { this.ledGreen = value; this.SendCommand(); }
        }
        public bool LedBlue
        {
            get { return this.ledBlue; }
            set { this.ledBlue = value; this.SendCommand(); }
        }

        public MotorMode MotorL
        {
            get { return this.motorL; }
            set { this.motorL = value; this.SendCommand(); }
        }

        public MotorMode MotorR
        {
            get { return this.motorR; }
            set { this.motorR = value; this.SendCommand(); }
        }

        public bool LineL
        {
            get { this.UpdateStatus(); return this.lineL; }
        }
        public bool LineR
        {
            get { this.UpdateStatus(); return this.lineR; }
        }
        public int Distance
        {
            get { this.UpdateStatus(); return this.distance; }
        }

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

        private void Initialize()
        {
            this.statusReport = this.hid.GetFeatureStream(StatusReportId);
            this.commandReport = this.hid.GetFeatureStream(CommandReportId);
        }

        private void SendCommand()
        {
            byte[] command = new byte[3];
            if (this.ledRed) command[0] |= 1;
            if (this.ledGreen) command[0] |= 2;
            if (this.ledBlue) command[0] |= 4;

            command[1] = (byte)this.motorL;
            command[2] = (byte)this.motorR;

            this.commandReport.Write(command, 0, command.Length);
        }

        private void UpdateStatus()
        {
            byte[] status = new byte[2];
            this.statusReport.Read(status, 0, status.Length);
            if ((status[0] & 1) == 0) this.lineL = false; else this.lineL = true;
            if ((status[0] & 2) == 0) this.lineR = false; else this.lineR = true;
            this.distance = status[1];
        }

        #region IDisposable メンバ

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.statusReport.Dispose();
                    this.commandReport.Dispose();
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
