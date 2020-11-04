//Source by https://github.com/kichi-chan/kichi | http://kiwiexploits.com/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// Lógica interna para OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ResetLabel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void IngameChatBox_Checked(object sender, RoutedEventArgs e)
        {
            Process[] roblox = Process.GetProcesses();
            foreach (Process openedroblox in roblox)
            {
                bool flag = openedroblox.ProcessName == "RobloxPlayerBeta";
                if (flag)
                {
                    openedroblox.Kill();
                }
            }
        }

        private void Jdisc_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("http://example.com/"); // button link here
        }

        private void AutoLaunchBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void AutoAttachBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void UnlockBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void girhub_Click(object sender, RoutedEventArgs e)
        {

            Process.Start("http://example.com/"); // button link here
        }
    }
}
