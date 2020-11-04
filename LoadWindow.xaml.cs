//Source by https://github.com/kichi-chan/kichi | http://kiwiexploits.com/
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

namespace SynpasteX
{
    /// <summary>
    /// Lógica interna para LoadWindow.xaml
    /// </summary>
    public partial class LoadWindow : Window
    {
        //string letter = "kkjbkbiwi"; // updater #001 version (same on pastebin raw)//
        private WebClient client = new WebClient();
        string opp;
        Module R = new Module();
        private string ReadURL(string url)
        {
            return client.DownloadString(url);
        }
        public LoadWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            //scheck();
            char[] letters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm123456789".ToCharArray();
            Random r = new Random();
            string rs = "";
            for (int i = 0; i < 20; i++)
            {
                rs += letters[r.Next(0, 35)].ToString();
            }
            Title = rs;
            Topmost = true;
            R.IsUpdated();
            wlist();
            dfiles();
            loadbarAsync();           
        }

        bool result = false;
        void wlist()
        {
            string HFEW8IHFG8 = "https://pastebin.com/raw/tfJUS1hf"; // Check key (Auto login)
            string k = client.DownloadString(HFEW8IHFG8);
            TextReader file = new StreamReader(@"bin\Auth.ini");
            opp = file.ReadToEnd();
            file.Close();

            if (opp == k)
            {
                result = true;
            }
            else
            {
                result = false;
            }
        }

        public void SetStatusText(string Status, int Percentage)
        {
            Dispatcher.Invoke(() =>
            {
                StatusBox.Content = Status;
                ProgressBox.Value = Percentage;
            });
        }

        void dfiles()
        {
            string HFEW8IHFG8 = "https://pastebin.com/raw/FWbenFcu"; // Script Hub
            string k = client.DownloadString(HFEW8IHFG8);
            StreamWriter file = new StreamWriter(@"bin\ScriptHub\ScriptHub.json");
            file.Write(k);
            file.Close();
        }

        void ino()
        {
            var Loginw = new LoginWindow();
            Loginw.Show();
            Close();
        }

        void voltano()
        {
            var Reg = new MainWindow();
            Reg.Show();
            Close();
        }

        //void scheck() // updater #001 Check version on pastebin & download
        //{
        //    WebClient j = new WebClient();
        //    string g = j.DownloadString("https://pastebin.com/raw/aQdk5Fui"); // version

        //    if (g == letter)
        //    {
                
        //    }
        //    else
        //    {
        //        string fname = "Updater.exe";
        //        if (File.Exists(fname))
        //        {
        //            File.Delete(fname);
        //        }
        //        string updtr = "https://pastebin.com/raw/n1YHsy1H"; // Updater executable
        //        string text = this.ReadURL(updtr);
        //        if (text.Length > 0)
        //        {
        //            this.client.DownloadFile(text.Split(new char[]
        //            {
        //            ' '
        //            })[1], fname);
        //        }
        //        if (MessageBox.Show("We will start the update now.", "Synapse X", MessageBoxButton.OK, MessageBoxImage.Information) == MessageBoxResult.OK)
        //        {
        //            Process.Start("https://youtube.com/ejraniuns");
        //            Process.Start(fname);
        //            Environment.Exit(1);
        //        }
        //        Environment.Exit(1);
        //    }
        //}

        async Task loadbarAsync()
        {
            StatusBox.Content = "Initializing...";
            await Task.Delay(1200);
            StatusBox.Content = "Checking whitelist...";
            ProgressBox.Value = 30;
            if(result == true)
            {
                await Task.Delay(1000);
                StatusBox.Content = "Whitelisted!";
                ProgressBox.Value = 40;
                await Task.Delay(1100);
                StatusBox.Content = "Downloading data...";
                ProgressBox.Value = 60;
                await Task.Delay(1100);
                StatusBox.Content = "Checking data...";
                ProgressBox.Value = 80;
                await Task.Delay(1000);
                StatusBox.Content = "Ready to launch!";
                ProgressBox.Value = 100;
                await Task.Delay(500);
                voltano(); 
            }
            else
            {
                ino();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void ProgressBox_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
