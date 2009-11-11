using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;

using Microsoft.Win32.SafeHandles;

using DeviceIOLib;

namespace HidLib
{
    using USAGE = UInt16;
    using USHORT = UInt16;
    using ULONG = UInt32;
    using BOOLEAN = Byte;
    using LONG = Int32;
    using UCHAR = Byte;

    public class HidSdi
    {
        [DllImport("hid", SetLastError=true)]
        public extern static void HidD_GetHidGuid(out Guid lpguid);
        [DllImport("hid", SetLastError = true)]
        public extern static bool HidD_GetAttributes(SafeFileHandle HidDeviceObject, out HIDD_ATTRIBUTES Attributes);
        [DllImport("hid", SetLastError = true)]
        public extern static bool HidD_GetInputReport(SafeFileHandle HidDeviceObject, byte[] ReportBuffer, UInt32 ReportBufferLength);
        [DllImport("hid", SetLastError = true)]
        public extern static bool HidD_GetFeature(SafeFileHandle HidDeviceObject, byte[] ReportBuffer, UInt32 ReportBufferLength);
        [DllImport("hid", SetLastError = true)]
        public extern static bool HidD_SetFeature(SafeFileHandle HidDeviceObject, byte[] ReportBuffer, UInt32 ReportBufferLength);
        [DllImport("hid", SetLastError = true)]
        public extern static bool HidD_GetPreparsedData(SafeFileHandle HidDeviceObject, out IntPtr PreparsedData);
        [DllImport("hid", SetLastError = true)]
        public extern static bool HidD_FreePreparsedData(IntPtr PreparsedData);
        [DllImport("hid", SetLastError = true)]
        public extern static bool HidD_GetManufacturerString(SafeFileHandle HidDeviceObject, byte[] Buffer, UInt32 BufferLength);
        [DllImport("hid", SetLastError = true)]
        public extern static bool HidD_GetProductString(SafeFileHandle HidDeviceObject, byte[] Buffer, UInt32 BufferLength);

        [StructLayout(LayoutKind.Sequential, Pack=1)]
        public struct HIDD_ATTRIBUTES 
        {
            public UInt32 Size;
            public UInt16 VendorID;
            public UInt16 ProductID;
            public UInt16 VersionNumber;
        }
    }

    public class HidPi
    {
        public const uint STATUS_SUCCESS = 0x00000000;
        public const uint STATUS_BUFFER_TOO_SMALL = 0xC0000023;

        [DllImport("hid")]
        public extern static bool HidP_GetCaps(IntPtr PreparsedData, out HIDP_CAPS Capabilities);
        [DllImport("hid")]
        public extern static uint HidP_GetLinkCollectionNodes(HIDP_LINK_COLLECTION_NODE[] LinkCollectionNodes, ref int LinkCollectionNodesLength, IntPtr PreparsedData);
        [DllImport("hid")]
        public extern static uint HidP_GetValueCaps(HIDP_REPORT_TYPE ReportType, IntPtr ValueCaps, ref int ValueCapsLength, IntPtr PreparsedData);
        
        public enum HIDP_REPORT_TYPE
        {
            HidP_Input = 0x00,
            HidP_Output,
            HidP_Feature
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct HIDP_VALUE_CAPS
        {
            [FieldOffset(0)] 
            public USAGE UsagePage;
            [FieldOffset(2)]
            public UCHAR ReportID;
            [FieldOffset(3)] 
            public BOOLEAN  IsAlias;
            [FieldOffset(4)] 
            public USHORT  BitField;
            [FieldOffset(6)] 
            public USHORT  LinkCollection;
            [FieldOffset(8)] 
            public USAGE  LinkUsage;
            [FieldOffset(10)] 
            public USAGE  LinkUsagePage;
            [FieldOffset(12)] 
            public BOOLEAN  IsRange;
            [FieldOffset(13)] 
            public BOOLEAN  IsStringRange;
            [FieldOffset(14)] 
            public BOOLEAN  IsDesignatorRange;
            [FieldOffset(15)] 
            public BOOLEAN  IsAbsolute;
            [FieldOffset(16)] 
            public BOOLEAN  HasNull;
            [FieldOffset(17)] 
            public UCHAR  Reserved;
            [FieldOffset(18)] 
            public USHORT  BitSize;
            [FieldOffset(20)] 
            public USHORT  ReportCount;
            [FieldOffset(22)] 
            public USHORT  Reserved2_1;
            [FieldOffset(24)]
            public USHORT Reserved2_2;
            [FieldOffset(26)]
            public USHORT Reserved2_3;
            [FieldOffset(28)]
            public USHORT Reserved2_4;
            [FieldOffset(30)]
            public USHORT Reserved2_5;
            [FieldOffset(32)] 
            public ULONG  UnitsExp;
            [FieldOffset(36)] 
            public ULONG  Units;
            [FieldOffset(40)] 
            public LONG  LogicalMin, LogicalMax;
            [FieldOffset(48)] 
            public LONG  PhysicalMin, PhysicalMax;

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct RangeStruct
            {
                public USAGE  UsageMin, UsageMax;
                public USHORT  StringMin, StringMax;
                public USHORT  DesignatorMin, DesignatorMax;
                public USHORT  DataIndexMin, DataIndexMax;
            }
            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct NotRangeStruct
            {
                USAGE  Usage, Reserved1;
                USHORT  StringIndex, Reserved2;
                USHORT  DesignatorIndex, Reserved3;
                USHORT  DataIndex, Reserved4;
            }

            [FieldOffset(56)]
            [MarshalAs(UnmanagedType.Struct)]
            public RangeStruct Range;
            [FieldOffset(56)]
            [MarshalAs(UnmanagedType.Struct)]
            public NotRangeStruct NotRange;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HIDP_CAPS
        {
            public ushort Usage;
            public ushort UsagePage;
            public ushort InputReportByteLength;
            public ushort OutputReportByteLength;
            public ushort FeatureReportByteLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
            public ushort[] Reserved;
            public ushort NumberLinkCollectionNodes;
            public ushort NumberInputButtonCaps;
            public ushort NumberInputValueCaps;
            public ushort NumberInputDataIndices;
            public ushort NumberOutputButtonCaps;
            public ushort NumberOutputValueCaps;
            public ushort NumberOutputDataIndices;
            public ushort NumberFeatureButtonCaps;
            public ushort NumberFeatureValueCaps;
            public ushort NumberFeatureDataIndices;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HIDP_LINK_COLLECTION_NODE 
        {
            public ushort LinkUsage;
            public ushort LinkUsagePage;
            public ushort Parent;
            public ushort NumberOfChildren;
            public ushort NextSibling;
            public ushort FirstChild;
            public uint Fields;              // MSB [Reserved:23, IsAlias:1, CollectionType:8] LSB
            public IntPtr UserContext;
        } 
    }

    public class HIDReport
    {
        private HID device;
        private int reportId;
        private int inputReportLength;
        private int outputReportLength;
        private int featureReportLength;

        private List<HidPi.HIDP_VALUE_CAPS> inputValueList = new List<HidPi.HIDP_VALUE_CAPS>();
        private List<HidPi.HIDP_VALUE_CAPS> outputValueList = new List<HidPi.HIDP_VALUE_CAPS>();
        private List<HidPi.HIDP_VALUE_CAPS> featureValueList = new List<HidPi.HIDP_VALUE_CAPS>();

        public int Id
        {
            get { return this.reportId; }
        }

        public HID Device
        {
            get { return this.device; }
        }

        public int InputReportLength
        {
            get { return this.inputReportLength; }
        }
        public int OutputReportLength
        {
            get { return this.outputReportLength; }
        }
        public int FeatureReportLength
        {
            get { return this.featureReportLength; }
        }

        internal void BeginInit()
        {
            this.inputValueList.Clear();
            this.outputValueList.Clear();
            this.featureValueList.Clear();
            this.inputReportLength = 0;
            this.outputReportLength = 0;
            this.featureReportLength = 0;
        }

        internal void AddValueCaps(HidPi.HIDP_REPORT_TYPE reportType, HidPi.HIDP_VALUE_CAPS valueCaps)
        {
            if (valueCaps.ReportID != this.reportId) return;

            if( reportType == HidPi.HIDP_REPORT_TYPE.HidP_Input )
                this.inputValueList.Add(valueCaps);
            else if (reportType == HidPi.HIDP_REPORT_TYPE.HidP_Output)
                this.outputValueList.Add(valueCaps);
            else if (reportType == HidPi.HIDP_REPORT_TYPE.HidP_Feature)
                this.featureValueList.Add(valueCaps);
        }

        internal void EndInit()
        {
            foreach (HidPi.HIDP_VALUE_CAPS valueCaps in this.inputValueList)
                this.inputReportLength += valueCaps.BitSize * valueCaps.ReportCount;
            foreach (HidPi.HIDP_VALUE_CAPS valueCaps in this.outputValueList)
                this.outputReportLength += valueCaps.BitSize * valueCaps.ReportCount;
            foreach (HidPi.HIDP_VALUE_CAPS valueCaps in this.featureValueList)
                this.featureReportLength += valueCaps.BitSize * valueCaps.ReportCount;

            this.inputReportLength = (this.inputReportLength + 7) >> 3;
            this.outputReportLength = (this.outputReportLength + 7) >> 3;
            this.featureReportLength = (this.featureReportLength + 7) >> 3;
        }

        internal HIDReport(HID device, int reportId)
        {
            this.device = device;
            this.reportId = reportId;
        }
    }

    public sealed class HID : IDisposable, IEnumerable<KeyValuePair<int, HIDReport>>
    {
        /// <summary>
        /// HIDクラスのGUID
        /// </summary>
        public static readonly Guid Guid;

        static HID()
        {
            Guid guid = new Guid();
            HidSdi.HidD_GetHidGuid(out guid);
            Guid = guid;
        }

        private SafeFileHandle handle;
        private SafeFileHandle inHandle;
        private SafeFileHandle outHandle;

        private int vendorId;
        private string vendorString;
        private int productId;
        private string productString;
        private int version;
        private string devicePath;

        private int inputReportLength;
        private int outputReportLength;
        private int featureReportLength;

        private Dictionary<int, HIDReport> reports;

        public int VendorId
        {
            get { return this.vendorId; }
        }
        public string VendorString
        {
            get { return this.vendorString; }
        }
        public int ProductId
        {
            get { return this.productId; }
        }
        public string ProductString
        {
            get { return this.productString; }
        }
        public int Version
        {
            get { return this.version; }
        }
        public string DevicePath
        {
            get { return this.devicePath; }
        }
        public SafeFileHandle Handle
        {
            get { return this.handle; }
        }
        public SafeFileHandle InputHandle
        {
            get { return this.inHandle; }
        }
        public SafeFileHandle OutputHandle
        {
            get { return this.outHandle; }
        }

        public int InputReportLength
        {
            get { return this.inputReportLength; }
        }
        public int OutputReportLength
        {
            get { return this.outputReportLength; }
        }
        public int FeatureReportLength
        {
            get { return this.featureReportLength; }
        }

        public HIDReport this[int id]
        {
            get { return this.reports[id]; }
        }

        public HID(string devicePath)
        {
            this.devicePath = devicePath;

            DeviceIO.SECURITY_ATTRIBUTES securityAttributes = new DeviceIO.SECURITY_ATTRIBUTES();
            securityAttributes.nLength = Marshal.SizeOf(securityAttributes);
            securityAttributes.lpSecurityDescriptor = IntPtr.Zero;
            securityAttributes.bInheritHandle = true;
            GlobalBuffer saBuffer = new GlobalBuffer(securityAttributes.nLength);
            using (saBuffer)
            {
                Marshal.StructureToPtr(securityAttributes, saBuffer, false);
                this.handle = DeviceIO.CreateFile(this.devicePath, 0, DeviceIO.FILE_SHARE_READ | DeviceIO.FILE_SHARE_WRITE, IntPtr.Zero, DeviceIO.OPEN_EXISTING, 0, IntPtr.Zero);
                if (this.handle.IsInvalid)
                    throw new FileNotFoundException();

                this.inHandle = DeviceIO.CreateFile(this.devicePath, DeviceIO.GENERIC_READ, DeviceIO.FILE_SHARE_READ | DeviceIO.FILE_SHARE_WRITE, saBuffer, DeviceIO.OPEN_EXISTING, 0, IntPtr.Zero);
                this.outHandle = DeviceIO.CreateFile(this.devicePath, DeviceIO.GENERIC_WRITE, DeviceIO.FILE_SHARE_READ | DeviceIO.FILE_SHARE_WRITE, saBuffer, DeviceIO.OPEN_EXISTING, 0, IntPtr.Zero);
                
                this.SetAttributes();
                this.SetCapabilities();
            }
        }

        
        private void SetAttributes()
        {
            if (!this.handle.IsInvalid)
            {
                HidSdi.HIDD_ATTRIBUTES attributes = new HidSdi.HIDD_ATTRIBUTES();
                attributes.Size = (UInt32)Marshal.SizeOf(attributes);
                if (HidSdi.HidD_GetAttributes(this.handle, out attributes))
                {
                    this.vendorId = (int)attributes.VendorID;
                    this.productId = (int)attributes.ProductID;
                    this.version = (int)attributes.VersionNumber;
                }
                byte[] buf = new byte[256];
                if (HidSdi.HidD_GetManufacturerString(this.handle, buf, (uint)buf.Length))
                {
                    String s = Encoding.Unicode.GetString(buf, 0, buf.Length);
                    int length = s.IndexOf('\0');
                    this.vendorString = s.Substring(0, length);
                }
                if (HidSdi.HidD_GetProductString(this.handle, buf, (uint)buf.Length))
                {
                    String s = Encoding.Unicode.GetString(buf, 0, buf.Length);
                    int length = s.IndexOf('\0');
                    this.productString = s.Substring(0, length);
                }
            }
        }

        private void SetCapabilities()
        {
            IntPtr preparsedData;
            HidPi.HIDP_CAPS caps = new HidPi.HIDP_CAPS();
            caps.Reserved = new ushort[17];

            HidSdi.HidD_GetPreparsedData(this.handle, out preparsedData);
            HidPi.HidP_GetCaps(preparsedData, out caps);
            
            this.inputReportLength = caps.InputReportByteLength;
            this.outputReportLength = caps.OutputReportByteLength;
            this.featureReportLength = caps.FeatureReportByteLength;

            Dictionary<int, HIDReport> reportMap = new Dictionary<int, HIDReport>();
            SetupReports(reportMap, HidPi.HIDP_REPORT_TYPE.HidP_Input, caps, preparsedData);
            SetupReports(reportMap, HidPi.HIDP_REPORT_TYPE.HidP_Output, caps, preparsedData);
            SetupReports(reportMap, HidPi.HIDP_REPORT_TYPE.HidP_Feature, caps, preparsedData);

            this.reports = reportMap;
            
            foreach (HIDReport report in this.reports.Values)
                report.EndInit();

            HidSdi.HidD_FreePreparsedData(preparsedData);
        }

        private void SetupReports(Dictionary<int, HIDReport> reportMap, HidPi.HIDP_REPORT_TYPE reportType, HidPi.HIDP_CAPS caps, IntPtr preparsedData)
        {
            foreach (HidPi.HIDP_VALUE_CAPS valueCaps in GetValueCaps(reportType, caps, preparsedData))
            {
                int id = (int)valueCaps.ReportID;
                if (!reportMap.ContainsKey(id))
                {
                    reportMap[id] = new HIDReport(this, id);
                    reportMap[id].BeginInit();
                }
                reportMap[id].AddValueCaps(reportType, valueCaps);
            }
        }

        private HidPi.HIDP_VALUE_CAPS[] GetValueCaps(HidPi.HIDP_REPORT_TYPE reportType, HidPi.HIDP_CAPS caps, IntPtr preparsedData)
        {
            int length = 0;
            if (reportType == HidPi.HIDP_REPORT_TYPE.HidP_Feature)
                length = caps.NumberFeatureValueCaps;
            else if (reportType == HidPi.HIDP_REPORT_TYPE.HidP_Input)
                length = caps.NumberInputValueCaps;
            else if (reportType == HidPi.HIDP_REPORT_TYPE.HidP_Output)
                length = caps.NumberOutputValueCaps;

            HidPi.HIDP_VALUE_CAPS[] valueCaps = new HidPi.HIDP_VALUE_CAPS[length];
            int valueCapsSize = Marshal.SizeOf(typeof(HidPi.HIDP_VALUE_CAPS));
            GlobalBuffer valueCapsBuffer = new GlobalBuffer(length * valueCapsSize);

            HidPi.HidP_GetValueCaps(reportType, valueCapsBuffer, ref length, preparsedData);

            for (int i = 0; i < length; i++)
            {
                valueCaps[i] = (HidPi.HIDP_VALUE_CAPS)Marshal.PtrToStructure((IntPtr)((int)valueCapsBuffer.Pointer + (i * valueCapsSize)), typeof(HidPi.HIDP_VALUE_CAPS));
            }
            return valueCaps;
        }

        /// <summary>
        /// Featureレポートを用いてデバイスとの通信を行うStreamを返す．
        /// </summary>
        /// <returns></returns>
        public Stream GetFeatureStream()
        {
            return new FeatureStream(this);
        }

        /// <summary>
        /// 指定したレポートIDを持つFeatureレポートを用いてデバイスとの通信を行うStreamを返す．
        /// </summary>
        /// <param name="reportId">レポートID</param>
        /// <returns></returns>
        public Stream GetFeatureStream(int reportId)
        {
            return new FeatureStream(this.reports[reportId]);
        }

        #region IDisposable

        bool disposed = false;

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (disposed)
                {
                    disposed = true;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~HID()
        {
            this.Dispose(false);
        }
        #endregion

        /// <summary>
        /// Featureレポートを用いてデバイスとの通信を行うStream
        /// </summary>
        private class FeatureStream : Stream
        {
            private HID device;
            private HIDReport report;
            private int reportId;

            /*
            byte[] readBuffer;
            int readBufferPtr = 0;
            byte[] writeBuffer;
            int writeBufferPtr = 0;
            */

            /// <summary>
            /// 指定したHIDに対してFeatureレポートによる通信を行うストリームを作成する．
            /// </summary>
            /// <param name="device"></param>
            public FeatureStream(HID device)
            {
                if (device.FeatureReportLength <= 0)
                    throw new NotSupportedException("このデバイスはFeatureレポートをサポートしていません．");

                this.device = device;
                this.report = null;
                this.reportId = 0;
            }

            /// <summary>
            /// 指定したHIDに対して指定したレポートIDのFeatureレポートによる通信を行うストリームを作成する．
            /// </summary>
            /// <param name="device"></param>
            /// <param name="reportId"></param>
            public FeatureStream(HIDReport report)
            {
                if (report.FeatureReportLength <= 0)
                    throw new NotSupportedException("指定されたレポートIDはFeatureレポートをサポートしていません．");

                this.device = report.Device;
                this.report = report;
                this.reportId = report.Id;
            }

            public override bool CanRead
            {
                get { return !this.device.Handle.IsInvalid; }
            }

            public override bool CanSeek
            {
                get { return false; }
            }

            public override bool CanWrite
            {
                get { return !this.device.Handle.IsInvalid; }
            }

            public override void Flush()
            {
                throw new InvalidOperationException();
            }

            public override long Length
            {
                get { throw new InvalidOperationException(); }
            }

            public override long Position
            {
                get
                {
                    throw new InvalidOperationException();
                }
                set
                {
                    throw new InvalidOperationException();
                }
            }

            private int GetLength()
            {
                return this.report == null ? this.device.FeatureReportLength - 1: this.report.FeatureReportLength;
            }
            /// <summary>
            /// デバイスからFeatureレポートを用いてデータを取得する．
            /// </summary>
            /// <param name="buffer">取得したデータを格納するバッファ</param>
            /// <param name="offset">取得したデータの格納を開始するバッファ内でのオフセット</param>
            /// <param name="count">取得するデータの長さ．デバイスのFeatureReportLength - 1でなければならない．</param>
            /// <returns>読み取ったバイト数</returns>
            public override int Read(byte[] buffer, int offset, int count)
            {
                if (count != this.GetLength())
                    throw new InvalidOperationException("Featureレポートを用いた転送のサイズがFeatureレポートの長さではありません．");
                byte[] buf = new byte[this.GetLength() + 1];
                buf[0] = (byte)this.reportId;
                if (!HidSdi.HidD_GetFeature(this.device.Handle, buf, (uint)this.GetLength() + 1))
                    throw new IOException();
                 Array.Copy(buf, 0, buffer, offset, count);

                return count;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                throw new InvalidOperationException();
            }

            public override void SetLength(long value)
            {
                throw new InvalidOperationException();
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                if (count != this.GetLength())
                    throw new InvalidOperationException("Featureレポートを用いた転送のサイズがFeatureレポートの長さではありません．");
                byte[] buf = new byte[this.GetLength() + 1];
                buf[0] = (byte)this.reportId;
                Array.Copy(buffer, offset, buf, 1, count);
                if (!HidSdi.HidD_SetFeature(this.device.Handle, buf, (uint)this.GetLength() + 1))
                    throw new IOException();
                
            }
        }

        #region IEnumerable<KeyValuePair<int,HIDReport>> メンバ

        public IEnumerator<KeyValuePair<int, HIDReport>> GetEnumerator()
        {
            return this.reports.GetEnumerator();
        }

        #endregion

        #region IEnumerable メンバ

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<int, HIDReport>>)this).GetEnumerator();
        }

        #endregion
    }
}
