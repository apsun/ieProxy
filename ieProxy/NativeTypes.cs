using System;
using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;
using DWORD = System.Int32;

namespace ieProxy
{
    public static class NativeTypes
    {
        // ReSharper disable InconsistentNaming

        [StructLayout(LayoutKind.Sequential)]
        public struct INTERNET_PER_CONN_OPTION_LIST
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/aa385146%28v=vs.85%29.aspx
            /// <summary>
            /// Equal to the size of the list, which is always 20. 
            /// You can get it using Marshal.SizeOf(typeof(INTERNET_PER_CONN_OPTION_LIST)).
            /// </summary>
            public DWORD dwSize;
            /// <summary>
            /// Set to IntPtr.Zero (NULL) for the default connection.
            /// </summary>
            public IntPtr pszConnection;
            /// <summary>
            /// The number of elements in the options array (see below).
            /// </summary>
            public DWORD dwOptionCount;
            /// <summary>
            /// Gets the number of options that failed.
            /// </summary>
            public DWORD dwOptionError;
            /// <summary>
            /// Pointer to an array of INTERNET_PER_CONN_OPTION (INTERNET_PER_CONN_OPTION*).
            /// </summary>
            public IntPtr pOptions;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INTERNET_PER_CONN_OPTION
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/aa385145%28v=vs.85%29.aspx
            /// <summary>
            /// The option to get or set. Also specifies the return type.
            /// </summary>
            [FieldOffset(0)] public INTERNET_PER_CONN_OPTON dwOption;
            /// <summary>
            /// If the return type was an integer (enum value), use this.
            /// </summary>
            [FieldOffset(4)] public DWORD dwValue;
            /// <summary>
            /// If the return type was a string, call Marshal.PtrToString on this.
            /// </summary>
            [FieldOffset(4)] public IntPtr pszValue;
            /// <summary>
            /// If the return type was a time, call DateTime.FromFileTime on this.
            /// </summary>
            [FieldOffset(4)] public FILETIME ftValue;
        }

        public enum INTERNET_OPTION
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/aa385328%28v=vs.85%29.aspx
            INTERNET_OPTION_REFRESH = 37, 
            INTERNET_OPTION_PER_CONNECTION_OPTION = 75
        }

        public enum INTERNET_PER_CONN_OPTON
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/aa385145%28v=vs.85%29.aspx
            /// <summary>
            /// Returns a INTERNET_PER_CONN_FLAGS (DWORD) with one or more flags set.
            /// </summary>
            INTERNET_PER_CONN_FLAGS = 1,
            /// <summary>
            /// Returns a string with the proxy server and port.
            /// </summary>
            INTERNET_PER_CONN_PROXY_SERVER = 2,
            /// <summary>
            /// Returns a string with the URLs that bypass the proxy.
            /// </summary>
            INTERNET_PER_CONN_PROXY_BYPASS = 3,
            /// <summary>
            /// Returns a string with the URL to the proxy autoconfig file.
            /// </summary>
            INTERNET_PER_CONN_AUTOCONFIG_URL = 4,
            /// <summary>
            /// Returns a PER_CONN_AUTODISCOVERY_FLAGS (DWORD) with one or more flags set.
            /// </summary>
            INTERNET_PER_CONN_AUTODISCOVERY_FLAGS = 5,
            /// <summary>
            /// Returns a string with the URL to the secondary proxy autoconfig file.
            /// </summary>
            INTERNET_PER_CONN_AUTOCONFIG_SECONDARY_URL = 6,
            /// <summary>
            /// Returns a DWORD with the interval in minutes betweeen proxy autoconfig reloads.
            /// </summary>
            INTERNET_PER_CONN_AUTOCONFIG_RELOAD_DELAY_MINS = 7,
            /// <summary>
            /// Returns a FILETIME with the time the last good autoconfig was found with autodiscovery.
            /// </summary>
            INTERNET_PER_CONN_AUTOCONFIG_LAST_DETECT_TIME = 8,
            /// <summary>
            /// Returns a string with the URL of the last good autoconfig found with autodiscovery.
            /// </summary>
            INTERNET_PER_CONN_AUTOCONFIG_LAST_DETECT_URL = 9,
            /// <summary>
            /// Returns a INTERNET_PER_CONN_FLAGS (DWORD) with one or more flags set.
            /// </summary>
            INTERNET_PER_CONN_FLAGS_UI = 10,
        }

        [Flags]
        public enum INTERNET_PER_CONN_FLAGS
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/aa385145%28v=vs.85%29.aspx
            PROXY_TYPE_DIRECT         = 0x00000001, //0001
            PROXY_TYPE_PROXY          = 0x00000002, //0010
            PROXY_TYPE_AUTO_PROXY_URL = 0x00000004, //0100
            PROXY_TYPE_AUTO_DETECT    = 0x00000008, //1000
        }

        [Flags]
        public enum PER_CONN_AUTODISCOVERY_FLAGS
        {
            //http://msdn.microsoft.com/en-us/library/windows/desktop/aa385145%28v=vs.85%29.aspx
            AUTO_PROXY_FLAG_USER_SET                = 0x00000001, //0000001
            AUTO_PROXY_FLAG_ALWAYS_DETECT           = 0x00000002, //0000010
            AUTO_PROXY_FLAG_DETECTION_RUN           = 0x00000004, //0000100
            AUTO_PROXY_FLAG_MIGRATED                = 0x00000008, //0001000
            AUTO_PROXY_FLAG_DONT_CACHE_PROXY_RESULT = 0x00000010, //0010000
            AUTO_PROXY_FLAG_CACHE_INIT_RUN          = 0x00000020, //0100000
            AUTO_PROXY_FLAG_DETECTION_SUSPECT       = 0x00000040, //1000000
        }
    }
}