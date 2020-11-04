using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;

namespace SynpasteX
{
    class Functions
    {
        public static OpenFileDialog openfiledialog = new OpenFileDialog
        {
            Filter = "Lua Script Txt (*.txt)|*.txt (*.*)|*.*|Lua Script (*.lua)|*.lua (*.*)|*.*",
            FilterIndex = 1,
            RestoreDirectory = true,
            Title = "Synapse X"
        };

        public static void PopulateListBox(ListBox lsb, string Folder, string FileType)
        {
            DirectoryInfo dinfo = new DirectoryInfo(Folder);
            FileInfo[] Files = dinfo.GetFiles(FileType);
            foreach (FileInfo file in Files)
            {
                lsb.Items.Add(file.Name);
            }
        }
    }
}
