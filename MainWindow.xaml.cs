//Source by https://github.com/kichi-chan/kichi | http://kiwiexploits.com/
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using mshtml;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SynpasteX
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            createAE();
            char[] letters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm123456789".ToCharArray();
            Random r = new Random();
            string rs = "";
            for (int i = 0; i < 20; i++)
            {
                rs += letters[r.Next(0, 35)].ToString();
            }
            Title = rs;
            loadver();
            Topmost = true;
            string curDir = Directory.GetCurrentDirectory();
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                string friendlyName = AppDomain.CurrentDomain.FriendlyName;
                if (registryKey.GetValue(friendlyName) == null)
                {
                    registryKey.SetValue(friendlyName, 11001, RegistryValueKind.DWord);
                }
                webBrowser1.Source = new Uri(string.Format("file:///{0}/bin/Monaco.html", curDir));
                webBrowser1.Navigated += new NavigatedEventHandler(wbMain_Navigated);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading Monaco Editor! Error: " + ex.Message + "\nThis error has been copied.", "Synapse X", MessageBoxButton.OK);
                Clipboard.SetText(ex.Message + " ++ SYNAPSE X ERROR ++");
                Environment.Exit(1);
            }
        }

        void wbMain_Navigated(object sender, NavigationEventArgs e)
        {
            SetSilent(webBrowser1, true);
        }

        Module r = new Module();

        string sver;
        void loadver()
        {
            WebClient w = new WebClient();
            sver = w.DownloadString("https://pastebin.com/raw/0DkGtwGd"); // version in top
            TitleBox.Content = sver;
        }

        public void HideScriptErrors(WebBrowser webBrowser1, bool Hide)
        {
            FieldInfo fiComWebBrowser = typeof(WebBrowser)
                .GetField("_axIWebBrowser2",
                          BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(webBrowser1);
            if (objComWebBrowser == null) return;
            objComWebBrowser.GetType().InvokeMember(
                "Silent", BindingFlags.SetProperty, null, objComWebBrowser,
                new object[] { Hide });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ScriptBox.Items.Clear();
            Functions.PopulateListBox(ScriptBox, "./scripts", "*.txt");
            Functions.PopulateListBox(ScriptBox, "./scripts", "*.lua");
        }

        void atc()
        {
            r.LaunchExploit();
        }

        async Task attachAsync()
        {
            Process[] processesByName = Process.GetProcessesByName("RobloxPlayerBeta");
            if (processesByName.Length == 0)
            {
                TitleBox.Content = sver + " (failed to find Roblox)";
                await Task.Delay(1200);
                TitleBox.Content = sver;
            }
            else
            {
                if (r.isEasyReady())
                {
                    TitleBox.Content = sver + " (already injected!)";
                    await Task.Delay(1200);
                    TitleBox.Content = sver;
                }
                else
                {
                    atc();
                    await Task.Delay(500);
                    TitleBox.Content = sver + " (scanning...)";
                    await Task.Delay(3000);
                    TitleBox.Content = sver + " (injecting...)";
                    await Task.Delay(2500);
                    TitleBox.Content = sver + " (checking...)";
                    await Task.Delay(1500);
                    if (r.isEasyReady())
                    {
                        TitleBox.Content = sver + " (ready!)";
                        r.ExecuteScript(Monaco.Era());
                        await Task.Delay(2000);
                        TitleBox.Content = sver;
                        Autoexec();
                    }
                    else
                    {
                        TitleBox.Content = sver + " (failed to attach!)";
                        await Task.Delay(2000);
                        atc();
                        TitleBox.Content = sver + " (trying again...)";        
                        await Task.Delay(3000);
                        if (r.isEasyReady())
                        {
                            TitleBox.Content = sver + " (ready!)";
                            r.ExecuteScript(Monaco.Era());
                            await Task.Delay(2000);
                            TitleBox.Content = sver;
                            Autoexec();
                        }
                        else if(r.isEasyReady())
                        {
                            TitleBox.Content = sver + " (failed to attach!)";
                            await Task.Delay(2000);
                            TitleBox.Content = sver;
                        }
                    }
                }
            }
        }

        string ae = $"./AutoExec/Script.lua";

        void createAE()
        {
            Directory.CreateDirectory("./AutoExec");
            if (File.Exists(ae))
            {

            }
            else
            {
                File.Create(ae);
            }
        }

        void Autoexec()
        {
            string cunt = File.ReadAllText(ae);
            if (r.isEasyReady())
            {
                r.ExecuteScript(cunt);
            }
            else
            {

            }
        }

        async Task injectfAsync()
        {
            TitleBox.Content = sver + " (inject first!)";
            await Task.Delay(1200);
            TitleBox.Content = sver;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = webBrowser1.InvokeScript(scriptName, args);
            string script = obj.ToString();
            if (r.isEasyReady())
            {
                
                r.ExecuteScript(script);
            }
            else
            {
                injectfAsync();
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.InvokeScript("SetText", new object[]
            {
                ""
            });
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == true)
            {
                try
                {

                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    webBrowser1.InvokeScript("SetText", new object[]
                    {
                          MainText
                    });

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ExecuteFileButton_Click(object sender, RoutedEventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == true)
            {
                try
                {

                    string MainText = File.ReadAllText(Functions.openfiledialog.FileName);
                    if (r.isEasyReady())
                    {
                        r.ExecuteScript(MainText);
                    }
                    else
                    {
                        injectfAsync();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            string scriptName = "GetText";
            object[] args = new string[0];
            object obj = webBrowser1.InvokeScript(scriptName, args);
            string script = obj.ToString();
            SaveFileDialog save = new SaveFileDialog()
            {
                Filter = "Text Files (.txt)|*.txt",
                Title = "Save Script"
            };

            if(save.ShowDialog() == true)
            {
                StreamWriter sw = new StreamWriter(save.FileName);
                sw.Write(script);
                sw.Dispose();
            }
        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            var Opw = new OptionsWindow();
            Opw.Show();
        }

        private void AttachButton_Click(object sender, RoutedEventArgs e)
        {
            attachAsync();
        }

        private void ScriptHubButton_Click(object sender, RoutedEventArgs e)
        {
            var Shub = new ScriptHubWindow();
            Shub.Show();
        }

        private void IconBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ExecuteItem_Click(object sender, RoutedEventArgs e)
        {
            string MainText = File.ReadAllText($"./scripts/{ScriptBox.SelectedItem}");
            if (r.isEasyReady())
            {
                r.ExecuteScript(MainText);
            }
            else
            {
                injectfAsync();
            }
        }

        private void LoadItem_Click(object sender, RoutedEventArgs e)
        {
            string MainText = File.ReadAllText($"./scripts/{ScriptBox.SelectedItem}");
            webBrowser1.InvokeScript("SetText", new object[]
            {
                          MainText
            });
        }

        private void RefreshItem_Click(object sender, RoutedEventArgs e)
        {
            ScriptBox.Items.Clear();
            Functions.PopulateListBox(ScriptBox, "./scripts", "*.txt");
            Functions.PopulateListBox(ScriptBox, "./scripts", "*.lua");
        }

        private void TopBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void ScriptBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string MainText = File.ReadAllText($"./scripts/{ScriptBox.SelectedItem}");
            webBrowser1.InvokeScript("SetText", new object[]
            {
                          MainText
            });
        }

        public static void SetSilent(WebBrowser browser, bool silent)
        {
            if (browser == null)
                throw new ArgumentNullException("browser");

            IOleServiceProvider sp = browser.Document as IOleServiceProvider;
            if (sp != null)
            {
                Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");

                object webBrowser;
                sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out webBrowser);
                if (webBrowser != null)
                {
                    webBrowser.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser, new object[] { silent });
                }
            }
        }

        void Browser_OnLoadCompleted(object sender, NavigationEventArgs e)
        {
            var browser = sender as WebBrowser;

            if (browser == null || browser.Document == null)
                return;

            dynamic document = browser.Document;

            if (document.readyState != "complete")
                return;

            dynamic script = document.createElement("script");
            script.type = @"text/javascript";
            script.text = @"window.onerror = function(msg,url,line){return true;}";
            document.head.appendChild(script);
        }

        [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IOleServiceProvider
        {
            [PreserveSig]
            int QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
        }
    }
}
