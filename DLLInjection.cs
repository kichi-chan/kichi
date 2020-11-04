using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace SynpasteX
{

	internal class DLLInjection
	{
		public enum DllInjectionResult
		{
			DllNotFound,
			GameProcessNotFound,
			InjectionFailed,
			Success
		}

		public sealed class DllInjector
		{
			public static DLLInjection.DllInjector GetInstance
			{
				get
				{
					if (DLLInjection.DllInjector._instance == null)
					{
						DLLInjection.DllInjector._instance = new DLLInjection.DllInjector();
					}
					return DLLInjection.DllInjector._instance;
				}
			}

			private DllInjector()
			{
			}

			private bool bInject(uint pToBeInjected, string sDllPath)
			{
				IntPtr intPtr = DLLInjection.DllInjector.OpenProcess(1082U, 1, pToBeInjected);
				if (intPtr == DLLInjection.DllInjector.INTPTR_ZERO)
				{
					return false;
				}
				IntPtr procAddress = DLLInjection.DllInjector.GetProcAddress(DLLInjection.DllInjector.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
				if (procAddress == DLLInjection.DllInjector.INTPTR_ZERO)
				{
					return false;
				}
				IntPtr intPtr2 = DLLInjection.DllInjector.VirtualAllocEx(intPtr, (IntPtr)0, (IntPtr)sDllPath.Length, 12288U, 64U);
				if (intPtr2 == DLLInjection.DllInjector.INTPTR_ZERO)
				{
					return false;
				}
				byte[] bytes = Encoding.ASCII.GetBytes(sDllPath);
				if (DLLInjection.DllInjector.WriteProcessMemory(intPtr, intPtr2, bytes, (uint)bytes.Length, 0) == 0)
				{
					return false;
				}
				if (DLLInjection.DllInjector.CreateRemoteThread(intPtr, (IntPtr)0, DLLInjection.DllInjector.INTPTR_ZERO, procAddress, intPtr2, 0U, (IntPtr)0) == DLLInjection.DllInjector.INTPTR_ZERO)
				{
					return false;
				}
				DLLInjection.DllInjector.CloseHandle(intPtr);
				return true;
			}

			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern int CloseHandle(IntPtr hObject);

			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, IntPtr dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr GetModuleHandle(string lpModuleName);

			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

			public DLLInjection.DllInjectionResult Inject(string sProcName, string sDllPath)
			{
				if (!File.Exists(sDllPath))
				{
					return DLLInjection.DllInjectionResult.DllNotFound;
				}
				uint num = 0U;
				Process[] processes = Process.GetProcesses();
				for (int i = 0; i < processes.Length; i++)
				{
					if (!(processes[i].ProcessName != sProcName))
					{
						num = (uint)processes[i].Id;
						break;
					}
				}
				if (num == 0U)
				{
					return DLLInjection.DllInjectionResult.GameProcessNotFound;
				}
				if (!this.bInject(num, sDllPath))
				{
					return DLLInjection.DllInjectionResult.InjectionFailed;
				}
				return DLLInjection.DllInjectionResult.Success;
			}

			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr OpenProcess(uint dwDesiredAccess, int bInheritHandle, uint dwProcessId);

			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, IntPtr dwSize, uint flAllocationType, uint flProtect);

			[DllImport("kernel32.dll", SetLastError = true)]
			private static extern int WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesWritten);

			private static readonly IntPtr INTPTR_ZERO = (IntPtr)0;

			private static DLLInjection.DllInjector _instance;
		}
	}
}
