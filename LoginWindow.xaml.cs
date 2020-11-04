//Source by https://github.com/kichi-chan/kichi | http://kiwiexploits.com/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SynpasteX
{
    /// <summary>
    /// Lógica interna para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            
            char[] letters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm123456789".ToCharArray();
            Random r = new Random();
            string rs = "";
            for (int i = 0; i < 20; i++)
            {
                rs += letters[r.Next(0, 35)].ToString();
            }
            Title = rs;
            Topmost = true;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e) // Check key and proceed
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            WebClient web = new WebClient();
            if (PasswordBox.Password == "youradminkeyhere")
            {
                MessageBox.Show("Valid Key! Click ok.");
                var Loadw = new MainWindow();
                Loadw.Show();
                Close();
            }
            else if (web.DownloadString("keysystemverificationlink" + PasswordBox.Password) != "yes")
            {
                MessageBox.Show("Invalid key, please try again");
            }
            else
            {
                MessageBox.Show("Valid Key! Click ok.");
                var Loadw = new MainWindow();
                Loadw.Show();
                Close();
            }
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(1);
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("keysystemstartlink");
        }

        private void TopBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
