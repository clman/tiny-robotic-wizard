using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using Microsoft.Win32.SafeHandles;

namespace DeviceIOLib
{
    public class DeviceEnumerator
    {

        private class SafeDevInfoHandle : SafeHandleZeroOrMinusOneIsInvalid 
        {
            [DllImport("setupapi.dll")]
            private extern static bool SetupDiDestroyDeviceInfoList(IntPtr DeviceInfoSet);
    
            protected override bool ReleaseHandle()
            {
 	            if( handle != IntPtr.Zero )
                {
                    SetupDiDestroyDeviceInfoList(handle);
                    handle = IntPtr.Zero;

                    return true;
                }
                return false;
            }

            internal SafeDevInfoHandle() : base(true) { }
            internal SafeDevInfoHandle(IntPtr _handle, bool ownHandle) : base(ownHandle) 
            {
                SetHandle(_handle);
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        private struct SP_DEVINFO_DATA {
            public int cbSize;
            public Guid ClassGuid;
            public int DevInst;
            public IntPtr Reserved;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct SP_DEVICE_INTERFACE_DATA {
            public int cbSize;
            public Guid InterfaceClassGuid;
            public int Flags;
            public IntPtr Reserved;
        }

        [DllImport("setupapi.dll", SetLastError = true)]
        private extern static bool SetupDiEnumDeviceInfo( SafeDevInfoHandle DeviceInfoSet, int MemberIndex, ref SP_DEVINFO_DATA DeviceInfoData);
        [DllImport("setupapi.dll", SetLastError=true)]
        private extern static bool SetupDiEnumDeviceInterfaces( SafeDevInfoHandle DeviceInfoSet,IntPtr DeviceInfoData, ref Guid InterfaceClassGuid, int MemberIndex, ref SP_DEVICE_INTERFACE_DATA  DeviceInterfaceData);
        [DllImport("setupapi.dll", SetLastError = true)]
        private extern static SafeDevInfoHandle SetupDiGetClassDevs(ref Guid ClassGuid, string Enumerator, IntPtr hwndParent, int Flags );
        [DllImport("setupapi.dll", SetLastError = true)]
        private extern static bool SetupDiGetDeviceInterfaceDetail(SafeDevInfoHandle DeviceInfoSet, ref SP_DEVICE_INTERFACE_DATA DeviceInterfaceData, byte[] DeviceInterfaceDetailData,  int DeviceInterfaceDetailDataSize, ref int RequiredSize, ref SP_DEVINFO_DATA  DeviceInfoData);

        // Device Class GUIDs
        public static readonly Guid GUID_DEVCLASS_1394 = new Guid(0x6bdd1fc1, 0x810f, 0x11d0, 0xbe, 0xc7, 0x08, 0x00, 0x2b, 0xe2, 0x09, 0x2f);
        public static readonly Guid GUID_DEVCLASS_ADAPTER = new Guid(0x4d36e964, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_APMSUPPORT = new Guid(0xd45b1c18, 0xc8fa, 0x11d1, 0x9f, 0x77, 0x00, 0x00, 0xf8, 0x05, 0xf5, 0x30 );
        public static readonly Guid GUID_DEVCLASS_BATTERY = new Guid(0x72631e54, 0x78a4, 0x11d0, 0xbc, 0xf7, 0x00, 0xaa, 0x00, 0xb7, 0xb3, 0x2a );
        public static readonly Guid GUID_DEVCLASS_CDROM = new Guid(0x4d36e965, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_COMPUTER = new Guid(0x4d36e966, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_DECODER = new Guid(0x6bdd1fc2, 0x810f, 0x11d0, 0xbe, 0xc7, 0x08, 0x00, 0x2b, 0xe2, 0x09, 0x2f );
        public static readonly Guid GUID_DEVCLASS_DISKDRIVE = new Guid(0x4d36e967, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_DISPLAY = new Guid(0x4d36e968, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_FDC = new Guid(0x4d36e969, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_FLOPPYDISK = new Guid(0x4d36e980, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_GPS = new Guid(0x6bdd1fc3, 0x810f, 0x11d0, 0xbe, 0xc7, 0x08, 0x00, 0x2b, 0xe2, 0x09, 0x2f );
        public static readonly Guid GUID_DEVCLASS_HDC = new Guid(0x4d36e96a, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_HIDCLASS = new Guid(0x745a17a0, 0x74d3, 0x11d0, 0xb6, 0xfe, 0x00, 0xa0, 0xc9, 0x0f, 0x57, 0xda );
        public static readonly Guid GUID_DEVCLASS_IMAGE = new Guid(0x6bdd1fc6, 0x810f, 0x11d0, 0xbe, 0xc7, 0x08, 0x00, 0x2b, 0xe2, 0x09, 0x2f );
        public static readonly Guid GUID_DEVCLASS_INFRARED = new Guid(0x6bdd1fc5, 0x810f, 0x11d0, 0xbe, 0xc7, 0x08, 0x00, 0x2b, 0xe2, 0x09, 0x2f );
        public static readonly Guid GUID_DEVCLASS_KEYBOARD = new Guid(0x4d36e96b, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_LEGACYDRIVER = new Guid(0x8ecc055d, 0x047f, 0x11d1, 0xa5, 0x37, 0x00, 0x00, 0xf8, 0x75, 0x3e, 0xd1 );
        public static readonly Guid GUID_DEVCLASS_MEDIA = new Guid(0x4d36e96c, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_MEDIUM_CHANGER = new Guid(0xce5939ae, 0xebde, 0x11d0, 0xb1, 0x81, 0x00, 0x00, 0xf8, 0x75, 0x3e, 0xc4 );
        public static readonly Guid GUID_DEVCLASS_MODEM = new Guid(0x4d36e96d, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_MONITOR = new Guid(0x4d36e96e, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_MOUSE = new Guid(0x4d36e96f, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_MTD = new Guid(0x4d36e970, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_MULTIFUNCTION = new Guid(0x4d36e971, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_MULTIPORTSERIAL = new Guid(0x50906cb8, 0xba12, 0x11d1, 0xbf, 0x5d, 0x00, 0x00, 0xf8, 0x05, 0xf5, 0x30 );
        public static readonly Guid GUID_DEVCLASS_NET = new Guid(0x4d36e972, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_NETCLIENT = new Guid(0x4d36e973, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_NETSERVICE = new Guid(0x4d36e974, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_NETTRANS = new Guid(0x4d36e975, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_NODRIVER = new Guid(0x4d36e976, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_PCMCIA = new Guid(0x4d36e977, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_PORTS = new Guid(0x4d36e978, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_PRINTER = new Guid(0x4d36e979, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_PRINTERUPGRADE = new Guid(0x4d36e97a, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_SCSIADAPTER = new Guid(0x4d36e97b, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_SMARTCARDREADER = new Guid(0x50dd5230, 0xba8a, 0x11d1, 0xbf, 0x5d, 0x00, 0x00, 0xf8, 0x05, 0xf5, 0x30 );
        public static readonly Guid GUID_DEVCLASS_SOUND = new Guid(0x4d36e97c, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_SYSTEM = new Guid(0x4d36e97d, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_TAPEDRIVE = new Guid(0x6d807884, 0x7d21, 0x11cf, 0x80, 0x1c, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_UNKNOWN = new Guid(0x4d36e97e, 0xe325, 0x11ce, 0xbf, 0xc1, 0x08, 0x00, 0x2b, 0xe1, 0x03, 0x18 );
        public static readonly Guid GUID_DEVCLASS_USB = new Guid(0x36fc9e60, 0xc465, 0x11cf, 0x80, 0x56, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00 );
        public static readonly Guid GUID_DEVCLASS_VOLUME = new Guid(0x71a27cdd, 0x812a, 0x11d0, 0xbe, 0xc7, 0x08, 0x00, 0x2b, 0xe2, 0x09, 0x2f);
        
        public static readonly Guid DiskClassGuid = new Guid(0x53f56307, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid CdRomClassGuid = new Guid(0x53f56308, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid PartitionClassGuid = new Guid(0x53f5630a, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid TapeClassGuid = new Guid(0x53f5630b, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid WriteOnceDiskClassGuid = new Guid(0x53f5630c, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid VolumeClassGuid = new Guid(0x53f5630d, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid MediumChangerClassGuid = new Guid(0x53f56310, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid FloppyClassGuid = new Guid(0x53f56311, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid CdChangerClassGuid = new Guid(0x53f56312, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid StoragePortClassGuid = new Guid(0x2accfe60, 0xc130, 0x11d2, 0xb0, 0x82, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);

        public static readonly Guid ScsiRawInterfaceGuid = new Guid(0x53f56309, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);
        public static readonly Guid WmiScsiAddressGuid = new Guid(0x53f5630f, 0xb6bf, 0x11d0, 0x94, 0xf2, 0x00, 0xa0, 0xc9, 0x1e, 0xfb, 0x8b);

        // Flags for SetupDiGetClassDevs function
        private const int DIGCF_DEFAULT = 0x00000001;  // only valid with DIGCF_DEVICEINTERFACE
        private const int DIGCF_PRESENT = 0x00000002;
        private const int DIGCF_ALLCLASSES = 0x00000004;
        private const int DIGCF_PROFILE = 0x00000008;
        private const int DIGCF_DEVICEINTERFACE = 0x00000010;

        private const int ERROR_NO_MORE_ITEMS = 259;

        /// <summary>
        /// 指定されたクラスGUIDを持つデバイスを列挙し，そのファイル名を返す
        /// </summary>
        /// <param name="classGuid">列挙するデバイスのクラスGUID</param>
        /// <returns></returns>
        public static string[] EnumDevices(Guid classGuid)
        {
            Guid guid = classGuid;
            SafeDevInfoHandle hDevIfInfo = SetupDiGetClassDevs(ref guid, null, IntPtr.Zero, DIGCF_PRESENT | DIGCF_DEVICEINTERFACE);
            using(hDevIfInfo)
            {
                if (hDevIfInfo.IsInvalid) return null;

                SP_DEVICE_INTERFACE_DATA devIf = new SP_DEVICE_INTERFACE_DATA();
                devIf.cbSize = Marshal.SizeOf(devIf);
                SP_DEVINFO_DATA devInfo = new SP_DEVINFO_DATA();
                devInfo.cbSize = Marshal.SizeOf(devInfo);

                List<string> devices = new List<string>();
                int index = 0;
                byte[] buf;
                int reqSize;
                while (SetupDiEnumDeviceInterfaces(hDevIfInfo, IntPtr.Zero, ref guid, index, ref devIf))
                {
                    reqSize = 0;
                    SetupDiGetDeviceInterfaceDetail(hDevIfInfo, ref devIf, null, 0, ref reqSize, ref devInfo);
                    buf = new byte[reqSize];
                    buf[0] = 5;
                    if (!SetupDiGetDeviceInterfaceDetail(hDevIfInfo, ref devIf, buf, buf.Length, ref reqSize, ref devInfo))
                        break;

                    devices.Add(Encoding.Default.GetString(buf, 4, buf.Length - 4 - 1));
                    index++;
                }

                int errCode = Marshal.GetLastWin32Error();
                if (errCode != ERROR_NO_MORE_ITEMS)
                    throw new Exception("デバイスの列挙に失敗しました。 Error #" + errCode.ToString());

                return devices.ToArray();
            }
        }

    }
}
