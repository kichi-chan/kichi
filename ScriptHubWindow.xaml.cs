using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace SynpasteX
{
    /// <summary>
    /// Lógica interna para ScriptHubWindow.xaml
    /// </summary>
    public partial class ScriptHubWindow : Window
    {
        Module r = new Module();
        public ScriptHubWindow()
        {
            InitializeComponent();
            char[] letters = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm123456789".ToCharArray();
            Random r = new Random();
            string rs = "";
            for (int i = 0; i < 20; i++)
            {
                rs += letters[r.Next(0, 35)].ToString();
            }
            Title = rs;
            loads();
        }

        void loads()
        {
            string json = File.ReadAllText($"./bin/ScriptHub/ScriptHub.json");
            List<JToken> list = JsonDecode(json)["scripts"].Children().Children<JToken>().ToList<JToken>();
            LoadedScripts = list;
            foreach (JToken jtoken in list)
            {
                ScriptBox.Items.Add(jtoken["Name"].ToString());
            }
        }

        private void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            string text = "print'noscript'";
            string json = File.ReadAllText($"./bin/ScriptHub/ScriptHub.json");
            JObject.Parse(json);
            text = ScriptBox.SelectedItem.ToString();
            foreach (JToken jtoken in this.LoadedScripts)
            {
                if (jtoken["Name"].ToString() == text)
                {
                    text = jtoken["Script"].ToString();
                }
            }
            if (r.isEasyReady())
            {
                r.ExecuteScript(text);
            }
            else
            {

            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private List<JToken> LoadedScripts;

        private JObject JsonDecode(string Json)
        {
            return JObject.Parse(Json);
        }

        private void ScriptBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string b = ScriptBox.SelectedItem.ToString();
            foreach (JToken jtoken in this.LoadedScripts)
            {
                if (jtoken["Name"].ToString() == b)
                {
                    DescriptionBox.Text = jtoken["Desc"].ToString();
                    ScriptPictureBox.Source = new BitmapImage(new Uri(jtoken["Picture"].ToString()));
                }
            }
        }

        private void MiniButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TopBox_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
