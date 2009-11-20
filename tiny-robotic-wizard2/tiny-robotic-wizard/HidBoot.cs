using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using DeviceIO;
using DeviceIO.Hid;

namespace HidBootLib
{
    public class HidBoot : IDisposable
    {
        public const int DefaultVendorId = 0x16c0;
        public const int DefaultProductId = 0x05df;
        public const string DefaultVendorString = "subspace.dyndns.info";
        public const string DefaultProductString = "HIDBL";
        public const int InfoReportId = 0x01;
        public const int DataReportId = 0x02;

        /// <summary>
        /// HIDBootデバイスを列挙し，そのデバイスのパスを返す．
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
        private Stream infoReport;
        private Stream dataReport;

        private int signature;
        private int pageSize;
        private int flashSize;

        public int Signature
        {
            get { return this.signature; }
        }
        public int PageSize
        {
            get { return this.pageSize; }
        }
        public int FlashSize
        {
            get { return this.flashSize; }
        }

        public HidBoot(String devicePath)
        {
            this.hid = new HID(devicePath);
            this.Initialize();
        }
        public HidBoot(HID device)
        {
            this.hid = device;
            this.Initialize();
        }

        private void Initialize()
        {
            this.infoReport = this.hid.GetFeatureStream(InfoReportId);
            this.dataReport = this.hid.GetFeatureStream(DataReportId);

            byte[] buf = new byte[this.hid[InfoReportId].FeatureReportLength];
            this.infoReport.Read(buf, 0, buf.Length);

            this.signature = ((int)buf[0] << 16) | ((int)buf[1] << 8) | (int)buf[2];
            this.pageSize = (int)buf[3];
            this.flashSize = (((int)buf[5] << 8) | (int)buf[4]) + 1;
        }

        /// <summary>
        /// ユーザーアプリケーションに処理を移行する．
        /// </summary>
        public void RunApplication()
        {
            try
            {
                // InfoReportに書き込みを行うとユーザーアプリケーションが実行される．
                byte[] buf = new byte[this.hid[InfoReportId].FeatureReportLength];
                this.infoReport.Write(buf, 0, buf.Length);
            }
            catch (IOException)
            {
            }
        }

        /// <summary>
        /// アプリケーションプログラムを書き込む
        /// </summary>
        /// <param name="program"></param>
        public void WriteApplication(byte[] prog, int minaddr, int maxaddr)
        {
            byte[] buf = new byte[this.hid[DataReportId].FeatureReportLength];
            int pagesize = this.PageSize;
            int pagemask = ~(pagesize - 1);
            for (int page = minaddr & pagemask; page < ((maxaddr + pagesize) & pagemask); page += pagesize)
            {
                int start = page - minaddr;
                int end = page + pagesize - minaddr;
                if (page < minaddr)
                {
                    Array.Clear(buf, 2, minaddr - page);
                    start = 0;
                }
                if (page + pagesize > maxaddr)
                {
                    Array.Clear(buf, 2 + maxaddr - page, page + pagesize - maxaddr);
                    end = maxaddr + 1;
                }
                Array.Copy(prog, start, buf, 2, end - start);

                buf[0] = (byte)(page & 0xff);
                buf[1] = (byte)(page >> 8);
                this.dataReport.Write(buf, 0, buf.Length);
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
                    this.infoReport.Dispose();
                    this.dataReport.Dispose();
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
        ~HidBoot()
        {
            this.Dispose(false);
        }

        #endregion
    }
}
