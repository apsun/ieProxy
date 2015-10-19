using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Option = ieProxy.NativeTypes.INTERNET_PER_CONN_OPTION;
using OptionType = ieProxy.NativeTypes.INTERNET_PER_CONN_OPTON;
using ProxyFlags = ieProxy.NativeTypes.INTERNET_PER_CONN_FLAGS;

namespace ieProxy
{
    public partial class Form1 : Form
    {
        private string _bypassUrls;

        public Form1()
        {
            InitializeComponent();
            proxyAddressTextBox.AdditionalChecks = delegate(string text)
            {
                return true;
                if (text == string.Empty) return true;
                IPAddress ip;
                bool isIp = IPAddress.TryParse(text, out ip);
                if (!isIp)
                {
                    if (text.Length > 2 && text[0] == '[' && text[text.Length - 1] == ']')
                    {
                        string textv6 = text.Substring(1, text.Length - 2);
                        isIp = IPAddress.TryParse(textv6, out ip);
                        if (!isIp) return false;
                    }
                    return false; //Not accepting URLs yet, sorry
                }
                if (ip.AddressFamily == AddressFamily.InterNetwork) return true; //IPv4
                return false; //IPv6 without [] or something else
            };

            LoadSettings();
        }

        private void LoadSettings()
        {
            var options = new[]
            {
                OptionType.INTERNET_PER_CONN_FLAGS_UI, 
                OptionType.INTERNET_PER_CONN_PROXY_SERVER, 
                OptionType.INTERNET_PER_CONN_AUTOCONFIG_URL, 
                OptionType.INTERNET_PER_CONN_PROXY_BYPASS
            };

            OptionValuePair[] results = Bridge.GetOptionValues(options);
            var proxyFlags = (ProxyFlags)results[0].Value;
            var proxyServer = (string)results[1].Value;
            var autoConfigUrl = (string)results[2].Value;
            var bypassUrls = (string)results[3].Value;
            bool proxyEnabled = (proxyFlags & ProxyFlags.PROXY_TYPE_PROXY) != 0;
            bool useAutoConfig = (proxyFlags & ProxyFlags.PROXY_TYPE_AUTO_PROXY_URL) != 0;
            bool useAutoDetect = (proxyFlags & ProxyFlags.PROXY_TYPE_AUTO_DETECT) != 0;

            string[] split = bypassUrls.Split(Constants.ProxyBypassSeparator);
            bool bypassLocal = Array.IndexOf(split, Constants.ProxyBypassLocalTag) >= 0;
         
            proxyEnableCheckBox.Checked = proxyEnabled;
            pacEnableCheckBox.Checked = useAutoConfig;
            pacAutoDetectCheckBox.Checked = useAutoDetect;
            proxyBypassLocalCheckBox.Checked = bypassLocal;

            int portDelim = proxyServer.LastIndexOf(Constants.ProxyPortSeparator);

            proxyAddressTextBox.Text = portDelim < 0 ? proxyServer : proxyServer.Substring(0, portDelim);
            proxyPortTextBox.Text = portDelim < 0 ? string.Empty : proxyServer.Substring(portDelim + 1);

            pacPathTextBox.Text = autoConfigUrl;
            _bypassUrls = bypassUrls;
        }

        private void SaveSettings()
        {
            bool proxyEnabled = proxyEnableCheckBox.Checked;
            bool useAutoConfig = pacEnableCheckBox.Checked;
            bool useAutoDetect = pacAutoDetectCheckBox.Checked;
            bool bypassLocal = proxyBypassLocalCheckBox.Checked;
            string proxyServer = proxyAddressTextBox.Text + Constants.ProxyPortSeparator + proxyPortTextBox.Text;
            string autoConfigUrl = pacPathTextBox.Text;
            string bypassUrls = _bypassUrls;
            
            int bypassLocalIndex = bypassUrls.IndexOf(Constants.ProxyBypassLocalTag, StringComparison.Ordinal);
            if (bypassLocal)
            {
                if (bypassLocalIndex < 0)
                {
                    bypassUrls += Constants.ProxyBypassSeparator + Constants.ProxyBypassLocalTag;
                }
            }
            else
            {
                if (bypassLocalIndex >= 0)
                {
                    //WARNING: ASSUMES <local> IS ALWAYS AT THE END
                    if (bypassLocalIndex == 0)
                    {
                        bypassUrls = string.Empty;
                    }
                    else
                    {
                        bypassUrls = bypassUrls.Substring(0, bypassLocalIndex - 1);
                    }
                }
            }

            var proxyFlags = ProxyFlags.PROXY_TYPE_DIRECT; //Always set for some reason
            if (proxyEnabled) proxyFlags |= ProxyFlags.PROXY_TYPE_PROXY;
            if (useAutoConfig) proxyFlags |= ProxyFlags.PROXY_TYPE_AUTO_PROXY_URL;
            if (useAutoDetect) proxyFlags |= ProxyFlags.PROXY_TYPE_AUTO_DETECT;

            var options = new[]
            {
                new OptionValuePair(OptionType.INTERNET_PER_CONN_FLAGS, proxyFlags), 
                new OptionValuePair(OptionType.INTERNET_PER_CONN_PROXY_SERVER, proxyServer), 
                new OptionValuePair(OptionType.INTERNET_PER_CONN_AUTOCONFIG_URL, autoConfigUrl), 
                new OptionValuePair(OptionType.INTERNET_PER_CONN_PROXY_BYPASS, bypassUrls)
            };

            Bridge.SetOptionValues(options);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void reloadButton_Click(object sender, EventArgs e)
        {
            LoadSettings();
        }

        private void aboutButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("twilight->Sparkle(42);");
        }

        private void proxyGoAgentButton_Click(object sender, EventArgs e)
        {
            proxyAddressTextBox.Text = Constants.GoAgentProxyAddress;
            proxyPortTextBox.Text = Constants.GoAgentProxyPort;
        }

        private void proxyShsidButton_Click(object sender, EventArgs e)
        {
            proxyAddressTextBox.Text = Constants.ShsidProxyAddress;
            proxyPortTextBox.Text = Constants.ShsidProxyPort;
        }

        private void pacBrowseButton_FileSelect(object sender, FileSelectEventArgs e)
        {
            pacPathTextBox.Text = e.FileName;
        }
    }
}
