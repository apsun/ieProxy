using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using FILETIME = System.Runtime.InteropServices.ComTypes.FILETIME;
using DWORD = System.Int32;
using OptionList = ieProxy.NativeTypes.INTERNET_PER_CONN_OPTION_LIST;
using Option = ieProxy.NativeTypes.INTERNET_PER_CONN_OPTION;
using Operation = ieProxy.NativeTypes.INTERNET_OPTION;
using OptionType = ieProxy.NativeTypes.INTERNET_PER_CONN_OPTON;
using ProxyFlags = ieProxy.NativeTypes.INTERNET_PER_CONN_FLAGS;
using AutoDiscoveryFlags = ieProxy.NativeTypes.PER_CONN_AUTODISCOVERY_FLAGS;

namespace ieProxy
{
    public enum OptionTypePlus
    {
        ProxyFlags          = 1, 
        ProxyServer         = 2, 
        ProxyBypassUrls     = 3, 
        ProxyAutoConfigPath = 4, 
        ProxyFlagsIE8       = 10
    }

    [Flags]
    public enum ProxyFlagsPlus
    {
        Direct     = 0x00000001, 
        Proxy      = 0x00000002, 
        AutoConfig = 0x00000004, 
        AutoDetect = 0x00000008
    }

    public struct OptionValuePair
    {
        public readonly OptionType Option;
        public readonly object Value;

        public OptionValuePair(OptionType option, object value)
        {
            EnsurePairTypesMatch(option, value);

            Option = option;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("Option: {0}\nValue: {1}", Option, Value);
        }

        private static void EnsurePairTypesMatch(OptionType option, object value)
        {
            switch (option)
            {
                case OptionType.INTERNET_PER_CONN_FLAGS:
                case OptionType.INTERNET_PER_CONN_FLAGS_UI:
                    EnsureIsType<ProxyFlags>(value);
                    break;
                case OptionType.INTERNET_PER_CONN_AUTODISCOVERY_FLAGS:
                    EnsureIsType<AutoDiscoveryFlags>(value);
                    break;
                case OptionType.INTERNET_PER_CONN_PROXY_SERVER:
                case OptionType.INTERNET_PER_CONN_PROXY_BYPASS:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_URL:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_SECONDARY_URL:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_LAST_DETECT_URL:
                    EnsureIsType<string>(value);
                    break;
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_RELOAD_DELAY_MINS:
                    EnsureIsType<int>(value);
                    break;
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_LAST_DETECT_TIME:
                    EnsureIsType<DateTime>(value);
                    break;
            }
        }

        private static void EnsureIsType<T>(object value)
        {
            if (value is T) return;
            throw new ArgumentException("Option type does not match with value type");
        }
    }

    public static class Bridge
    {
        public static void SetOptionValues(params OptionValuePair[] options)
        {
            int listSize = Marshal.SizeOf(typeof(OptionList));
            var optionArr = new Option[options.Length];
            for (int i = 0; i < optionArr.Length; ++i)
            {
                optionArr[i] = CreateOptionWithValue(options[i]);
            }

            GCHandle optionsHandle = GCHandle.Alloc(optionArr, GCHandleType.Pinned);
            IntPtr pOptions = optionsHandle.AddrOfPinnedObject();

            var optionList = new OptionList
            {
                dwSize = listSize,
                dwOptionCount = optionArr.Length,
                pOptions = pOptions
            };

            GCHandle listHandle = GCHandle.Alloc(optionList, GCHandleType.Pinned);
            IntPtr pList = listHandle.AddrOfPinnedObject();

            try
            {
                const Operation queryType = Operation.INTERNET_OPTION_PER_CONNECTION_OPTION;
                if (!NativeMethods.InternetSetOption(IntPtr.Zero, queryType, pList, listSize))
                {
                    throw new Win32Exception();
                }
            }
            finally
            {
                listHandle.Free();
                optionsHandle.Free();
            }
        }

        public static OptionValuePair[] GetOptionValues(params OptionType[] options)
        {
            int listSize = Marshal.SizeOf(typeof(OptionList));
            var optionArr = new Option[options.Length];
            for (int i = 0; i < optionArr.Length; ++i)
            {
                optionArr[i].dwOption = options[i];
            }

            GCHandle optionsHandle = GCHandle.Alloc(optionArr, GCHandleType.Pinned);
            IntPtr pOptions = optionsHandle.AddrOfPinnedObject();

            var optionList = new OptionList
            {
                dwSize = listSize,
                dwOptionCount = optionArr.Length,
                pOptions = pOptions
            };

            GCHandle listHandle = GCHandle.Alloc(optionList, GCHandleType.Pinned);
            IntPtr pList = listHandle.AddrOfPinnedObject();

            try
            {
                const Operation queryType = Operation.INTERNET_OPTION_PER_CONNECTION_OPTION;
                if (!NativeMethods.InternetQueryOption(IntPtr.Zero, queryType, pList, ref listSize))
                {
                    throw new Win32Exception();
                }
            }
            finally
            {
                listHandle.Free();
                optionsHandle.Free();
            }

            var values = new OptionValuePair[optionArr.Length];
            for (int i = 0; i < values.Length; ++i)
            {
                values[i] = new OptionValuePair(options[i], GetOptionValue(optionArr[i]));
            }

            return values;
        }

        private static Option CreateOptionWithValue(OptionValuePair pair)
        {
            OptionType opt = pair.Option;
            object value = pair.Value;

            var option = new Option { dwOption = opt };
            switch (opt)
            {
                case OptionType.INTERNET_PER_CONN_FLAGS:
                case OptionType.INTERNET_PER_CONN_FLAGS_UI:
                    option.dwValue = (DWORD)value;
                    break;
                case OptionType.INTERNET_PER_CONN_AUTODISCOVERY_FLAGS:
                    option.dwValue = (DWORD)value;
                    break;
                case OptionType.INTERNET_PER_CONN_PROXY_SERVER:
                case OptionType.INTERNET_PER_CONN_PROXY_BYPASS:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_URL:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_SECONDARY_URL:
                    option.pszValue = StringToPointer((string)value);
                    break;
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_RELOAD_DELAY_MINS:
                    option.dwValue = (DWORD)value;
                    break;
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_LAST_DETECT_URL:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_LAST_DETECT_TIME:
                    throw new InvalidOperationException("Option is read-only");
            }
            return option;
        }

        private static object GetOptionValue(Option option)
        {
            switch (option.dwOption)
            {
                case OptionType.INTERNET_PER_CONN_FLAGS:
                case OptionType.INTERNET_PER_CONN_FLAGS_UI:
                    return (ProxyFlags)option.dwValue;
                case OptionType.INTERNET_PER_CONN_AUTODISCOVERY_FLAGS:
                    return (AutoDiscoveryFlags)option.dwValue;
                case OptionType.INTERNET_PER_CONN_PROXY_SERVER:
                case OptionType.INTERNET_PER_CONN_PROXY_BYPASS:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_URL:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_SECONDARY_URL:
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_LAST_DETECT_URL:
                    return PointerToString(option.pszValue);
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_RELOAD_DELAY_MINS:
                    return option.dwValue;
                case OptionType.INTERNET_PER_CONN_AUTOCONFIG_LAST_DETECT_TIME:
                    return FileTimeToDateTime(option.ftValue);
            }

            //This will never be reached
            return null;
        }

        private static IntPtr StringToPointer(string str)
        {
            //TODO: This has a memory leak! Auto-clean up strings after setting!
            return Marshal.StringToHGlobalAnsi(str);
        }

        private static string PointerToString(IntPtr strPtr)
        {
            string str = Marshal.PtrToStringAnsi(strPtr);
            Marshal.FreeHGlobal(strPtr);
            return str ?? string.Empty;
        }

        private static DateTime FileTimeToDateTime(FILETIME time)
        {
            long fileTime = time.dwHighDateTime;
            fileTime <<= 32;
            fileTime |= (uint)time.dwLowDateTime;
            DateTime dt = DateTime.FromFileTime(fileTime);
            return dt;
        }
    }
}
