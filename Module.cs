using System;
using System.IO;
using System.IO.Pipes;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace SynpasteX
{
    class Module
    {
		private bool CheckLastestDll(RegistryKey registryKey)
		{
			string[] array = this.wc.DownloadString("https://raw.githubusercontent.com/GreenMs02/Update/master/Module.txt").Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			if (!(array[2] == "true"))
			{
				return false;
			}
			if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Roblox\\Versions\\" + array[3]))
			{
				registryKey.SetValue("Ver", array[3]);
				return true;
			}
			return false;
		}

		private bool CheckDllUpdate()
		{
			string[] array = this.wc.DownloadString("https://raw.githubusercontent.com/GreenMs02/Update/master/Module.txt").Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\\\CoCO", true);
			if (registryKey == null)
			{
				registryKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\\\CoCO");
				registryKey.SetValue("Ver", "0");
			}
			else
			{
				if (registryKey.GetValue("Ver").ToString() != array[0])
				{
					registryKey.SetValue("Ver", array[0]);
					return true;
				}
				if (registryKey.GetValue("Ver").ToString() != array[3] && this.CheckLastestDll(registryKey))
				{
					return true;
				}
			}
			return !File.Exists("EasyExploitsDLL.dll");
		}

		public bool DownloadDLL()
		{
			RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\\\CoCO", true);
			string[] array = this.wc.DownloadString("https://raw.githubusercontent.com/GreenMs02/Update/master/Module.txt").Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			if (registryKey.GetValue("Ver").ToString() == array[3] && this.CheckLastestDll(registryKey))
			{
				//this.wc.DownloadFile(array[4], "EasyExploitsDLL.dll"); OLD SHIT Slowed opening time
				this.wc.DownloadFileAsync(new Uri(array[4]), "EasyExploitsDLL.dll");
			}
			else
			{
				//this.wc.DownloadFile(array[1], "EasyExploitsDLL.dll"); OLD SHIT Slowed opening time
				this.wc.DownloadFileAsync(new Uri(array[1]), "EasyExploitsDLL.dll");
			}
			return File.Exists("EasyExploitsDLL.dll");
		}

		public void ExecuteScript(string Script)
		{
			if (Module.namedPipeExist("ocybedam"))
			{
				using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", "ocybedam", PipeDirection.Out))
				{
					namedPipeClientStream.Connect();
					using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream, Encoding.Default, 999999))
					{
						streamWriter.Write(Script);
						streamWriter.Dispose();
					}
					namedPipeClientStream.Dispose();
				}
				return;
			}
			if (File.Exists("EasyExploitsDLL.dll"))
			{
				MessageBox.Show("Please attach!", "NamedPipeDoesntExist", MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			MessageBox.Show("Please turn off your antivirus! bruh", "DLLDoesntExist", MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		private void InjectDLL()
		{
			DLLInjection.DllInjectionResult dllInjectionResult = DLLInjection.DllInjector.GetInstance.Inject("RobloxPlayerBeta", AppDomain.CurrentDomain.BaseDirectory + "\\EasyExploitsDLL.dll");
			if (dllInjectionResult == DLLInjection.DllInjectionResult.Success)
			{
				return;
			}
			switch (dllInjectionResult)
			{
				case DLLInjection.DllInjectionResult.DllNotFound:
					MessageBox.Show("Couldn't find the dll!", "Error: Dll Not Found", MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				case DLLInjection.DllInjectionResult.GameProcessNotFound:
					MessageBox.Show("No ROBLOX process found!", "Game Process Not Found", MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				case DLLInjection.DllInjectionResult.InjectionFailed:
					MessageBox.Show("Injection failed!", "Injection Failed", MessageBoxButton.OK, MessageBoxImage.Hand);
					return;
				default:
					return;
			}
		}

		public void LaunchExploit()
		{
			if (Module.namedPipeExist("ocybedam")) //checking if the api is attached or not
			{
				MessageBox.Show("API Already Attached!", "Kichi", MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			if (!this.CheckDllUpdate() && File.Exists("EasyExploitsDLL.dll")) //checks if the dll is updated and easyexploitsdll.dll is downloaded
			{
				this.InjectDLL();
				return;
			}
			if (this.DownloadDLL())
			{
				this.InjectDLL();
				return;
			}
			MessageBox.Show("Cant download the lastest version!", "Kichi", MessageBoxButton.OK, MessageBoxImage.Hand);
		}

		private static bool namedPipeExist(string pipeName)
		{
			bool result;
			try
			{
				if (!Module.WaitNamedPipe(Path.GetFullPath(string.Format("\\\\.\\pipe\\{0}", pipeName)), 0))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error == 0)
					{
						result = false;
						return result;
					}
					if (lastWin32Error == 2)
					{
						result = false;
						return result;
					}
				}
				result = true;
			}
			catch (Exception)
			{
				result = false;
			}
			return result;
		}

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool WaitNamedPipe(string name, int timeout);

		private WebClient wc = new WebClient();

		public bool isEasyReady()
		{
			return Module.namedPipeExist("ocybedam");
		}

		public void IsUpdated()
		{
			if (CheckDllUpdate() && File.Exists("EasyExploitsDLL.dll"))
			{

			}
			else
			{
				DownloadDLL();
			}
		}

	}
}
