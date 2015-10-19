using System;
using System.Runtime.InteropServices;
using Operation = ieProxy.NativeTypes.INTERNET_OPTION;

namespace ieProxy
{
    public static class NativeMethods
    {
        [DllImport("WinINet.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InternetQueryOption(IntPtr hInternet, Operation dwOption,
													  IntPtr lpBuffer, ref int lpdwBufferLength);

        [DllImport("WinINet.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool InternetSetOption(IntPtr hInternet, Operation dwOption,
													IntPtr lpBuffer, int dwBufferLength);
    }
}
