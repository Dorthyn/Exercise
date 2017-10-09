//获取进程数
private int GetProcessorCount()
{
    Process[] processes = Process.GetProcesses();
    return processes.Length;
}



//物理内存相关

//获取总数
using Microsoft.VisualBasic.Devices;
ComputerInfo ci = new ComputerInfo();
Console.WriteLine("总数(M):" + ci.TotalPhysicalMemory / 1024 / 1024);

//获取物理内存
Console.WriteLine("可用物理内存(M):" + ci.AvailablePhysicalMemory / 1024 / 1024);

//获取已安装
            double totalCapacity = 0;
            ObjectQuery objectQuery = new ObjectQuery("select * from Win32_PhysicalMemory");
            ManagementObjectSearcher searcher = new
                ManagementObjectSearcher(objectQuery);
            ManagementObjectCollection vals = searcher.Get();

            foreach (ManagementObject val in vals)
            {
                totalCapacity += System.Convert.ToDouble(val.GetPropertyValue("Capacity"));
            }

            //Console.WriteLine("Total Machine Memory = " + totalCapacity.ToString() + " Bytes");
            //Console.WriteLine("Total Machine Memory = " + (totalCapacity / 1024) + " KiloBytes");
            Console.WriteLine("Total Machine Memory = " + (totalCapacity / 1048576) + "    MegaBytes");
            //Console.WriteLine("Total Machine Memory = " + (totalCapacity / 1073741824) + " GigaBytes");
            
//分页、非分页缓存
        private static void getallmemoryusage()
        {
            /*
            PrivateMemorySize
            The number of bytes that the associated process has allocated that cannot be shared with other processes.
            PeakVirtualMemorySize
            The maximum amount of virtual memory that the process has requested.
            PeakPagedMemorySize
            The maximum amount of memory that the associated process has allocated that could be written to the virtual paging file.
            PagedSystemMemorySize
            The amount of memory that the system has allocated on behalf of the associated process that can be written to the virtual memory paging file.
            PagedMemorySize
            The amount of memory that the associated process has allocated that can be written to the virtual memory paging file.
            NonpagedSystemMemorySize
            The amount of memory that the system has allocated on behalf of the associated process that cannot be written to the virtual memory paging file.
            */
            double f = 1024.0;
            Process[] localByName = Process.GetProcesses();
            foreach (Process p in localByName)
            {
                Console.WriteLine("Private memory size64: {0}", (p.PrivateMemorySize64 / f).ToString("#,##0"));
                Console.WriteLine("Working Set size64: {0}", (p.WorkingSet64 / f).ToString("#,##0"));
                Console.WriteLine("Peak virtual memory size64: {0}", (p.PeakVirtualMemorySize64 / f).ToString("#,##0"));
                Console.WriteLine("Peak paged memory size64: {0}", (p.PeakPagedMemorySize64 / f).ToString("#,##0"));
                Console.WriteLine("Paged system memory size64: {0}", (p.PagedSystemMemorySize64 / f).ToString("#,##0"));
                Console.WriteLine("Paged memory size64: {0}", (p.PagedMemorySize64 / f).ToString("#,##0"));
                Console.WriteLine("Nonpaged system memory size64: {0}", (p.NonpagedSystemMemorySize64 / f).ToString("#,##0"));
            }
        }

//句柄数
            Process[] localByName = Process.GetProcesses();
            int sum = 0;
            foreach (Process p in localByName)
            {
                sum += p.HandleCount;
            }
            Console.WriteLine("Sum: {0}", sum.ToString());
            
            
            
            
//线程相关
    class Program
    {
        static void Main(string[] args)
        {
            int i = PerformanceInfo.GetTotalThreadCount();
            Console.WriteLine(i);
        }




        public static class PerformanceInfo
        {
            [DllImport("psapi.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetPerformanceInfo([Out] out PerformanceInformation PerformanceInformation, [In] int Size);

            [StructLayout(LayoutKind.Sequential)]
            public struct PerformanceInformation
            {
                public int Size;
                public IntPtr CommitTotal;
                public IntPtr CommitLimit;
                public IntPtr CommitPeak;
                public IntPtr PhysicalTotal;
                public IntPtr PhysicalAvailable;
                public IntPtr SystemCache;
                public IntPtr KernelTotal;
                public IntPtr KernelPaged;
                public IntPtr KernelNonPaged;
                public IntPtr PageSize;
                public int HandlesCount;
                public int ProcessCount;
                public int ThreadCount;
            }

            public static Int64 GetPhysicalAvailableMemoryInMiB()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return Convert.ToInt64((pi.PhysicalAvailable.ToInt64() * pi.PageSize.ToInt64() / 1048576));
                }
                else
                {
                    return -1;
                }

            }

            public static Int64 GetTotalMemoryInMiB()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return Convert.ToInt64((pi.PhysicalTotal.ToInt64() * pi.PageSize.ToInt64() / 1048576));
                }
                else
                {
                    return -1;
                }

            }

            public static int GetTotalThreadCount()
            {
                PerformanceInformation pi = new PerformanceInformation();
                if (GetPerformanceInfo(out pi, Marshal.SizeOf(pi)))
                {
                    return (pi.ThreadCount);
                }
                else
                {
                    return -1;
                }

            }
        }
    }
    
//开机时间
            DateTime dt = DateTime.Now.AddMilliseconds(-Environment.TickCount);
            Console.WriteLine("开机时间:" + dt.ToString("yyyy-MM-dd HH:mm:ss"));
            
//CPU总利用率
            cpuCounter = new PerformanceCounter();
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";
            cpuCounter.NextValue();

            System.Threading.Thread.Sleep(1000);
            Console.WriteLine(cpuCounter.NextValue());

            
//获取进程用户名

    class Program
    {


        static void Main(string[] args)
        {
            foreach (Process p in Process.GetProcesses())
            {
                Console.Write(p.ProcessName);
                Console.Write("----");
                Console.WriteLine(GetProcessUserName(p.Id));//根据进程的ID获得进程的用户名
            }


            Console.ReadKey();
        }
        //获得进程的用户名——添加引用(using System.Management;)
        private static string GetProcessUserName(int pID)
        {


            string text1 = null;


            SelectQuery query1 =
                new SelectQuery("Select * from Win32_Process WHERE processID=" + pID);
            ManagementObjectSearcher searcher1 = new ManagementObjectSearcher(query1);


            try
            {
                foreach (ManagementObject disk in searcher1.Get())
                {
                    ManagementBaseObject inPar = null;
                    ManagementBaseObject outPar = null;


                    inPar = disk.GetMethodParameters("GetOwner");


                    outPar = disk.InvokeMethod("GetOwner", inPar, null);


                    text1 = outPar["User"].ToString();
                    break;
                }
            }
            catch
            {
                text1 = "SYSTEM";
            }


            return text1;
        }

        //    PerformanceCounter totalProcessorTimeCounter = new PerformanceCounter(
        //     “Process”,
        //     “% Processor Time”,
        //     “_Total”);
        //    totalProcessorTimeCounter.NextValue();
        //System.Threading.Thread.Sleep(1000);// 1 second wait
        //Console.WriteLine(totalProcessorTimeCounter.NextValue());

    }
    
    
//描述
p.MainModule.FileVersionInfo.FileDescription

//获取路径
class Program
	{

		static void Main(string[] args)
		{
			Process[] process = Process.GetProcesses();

			Process p1 = Process.GetProcessById(3856);

			int i = 1;
			//Console.WriteLine(process.Length);
			//foreach (var p in process)
			//{
			//	if (p.Id != 3856 && p.Id != 4 && p.Id != 0)
			//	{
			//		Console.WriteLine(GetExecutablePathAboveVista(p.Id));
			//		Console.WriteLine(i++);
			//	}

			//}
			Console.WriteLine(GetExecutablePathAboveVista(p1.Id));
			Console.ReadKey();

		}

		[Flags]
		public enum ProcessAccessFlags : uint
		{
			PROCESS_ALL_ACCESS = 0x001F0FFF,
			PROCESS_CREATE_PROCESS = 0x00000080,
			PROCESS_CREATE_THREAD = 0x00000002,
			PROCESS_DUP_HANDLE = 0x00000040,
			PROCESS_QUERY_INFORMATION = 0x00000400,
			PROCESS_QUERY_LIMITED_INFORMATION = 0x00001000,
			PROCESS_SET_INFORMATION = 0x00000200,
			PROCESS_SET_QUOTA = 0x00000100,
			PROCESS_SUSPEND_RESUME = 0x00000800,
			PROCESS_TERMINATE = 0x00000001,
			PROCESS_VM_OPERATION = 0x00000008,
			PROCESS_VM_READ = 0x00000010,
			PROCESS_VM_WRITE = 0x00000020
		}

		private static string GetExecutablePath(Process Process)
		{
			//If running on Vista or later use the new function
			if (Environment.OSVersion.Version.Major >= 6)
			{
				return GetExecutablePathAboveVista(Process.Id);
			}

			return Process.MainModule.FileName;
		}

		private static string GetExecutablePathAboveVista(int ProcessId)
		{
			var buffer = new StringBuilder(1024);
			IntPtr hprocess = OpenProcess(ProcessAccessFlags.PROCESS_QUERY_LIMITED_INFORMATION,
				false, ProcessId);
			if (hprocess != IntPtr.Zero)
			{
				try
				{
					int size = buffer.Capacity;
					if (QueryFullProcessImageName(hprocess, 0, buffer, out size))
					{
						return buffer.ToString();
					}
				}
				finally
				{
					CloseHandle(hprocess);
				}
			}
					//throw new Win32Exception(Marshal.GetLastWin32Error());
                    return @"C:\Windows\System32";
		}

		[DllImport("kernel32.dll")]
		private static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags,
			StringBuilder lpExeName, out int size);
		[DllImport("kernel32.dll")]
		private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess,
			bool bInheritHandle, int dwProcessId);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool CloseHandle(IntPtr hHandle);

	}
    
//文件属性
	class Function
	{
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public struct SHELLEXECUTEINFO
		{
			public int cbSize;
			public uint fMask;
			public IntPtr hwnd;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpVerb;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpFile;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpParameters;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpDirectory;
			public int nShow;
			public IntPtr hInstApp;
			public IntPtr lpIDList;
			[MarshalAs(UnmanagedType.LPTStr)]
			public string lpClass;
			public IntPtr hkeyClass;
			public uint dwHotKey;
			public IntPtr hIcon;
			public IntPtr hProcess;
		}

		[DllImport("shell32.dll", CharSet = CharSet.Auto)]
		static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);
		private const int SW_SHOW = 5;
		private const uint SEE_MASK_INVOKEIDLIST = 12;

		public static bool ShowFileProperties(string Filename)
		{
			SHELLEXECUTEINFO info = new SHELLEXECUTEINFO();
			info.cbSize = System.Runtime.InteropServices.Marshal.SizeOf(info);
			info.lpVerb = "properties";
			info.lpFile = Filename;
			info.nShow = SW_SHOW;
			info.fMask = SEE_MASK_INVOKEIDLIST;
			return ShellExecuteEx(ref info);
		}
	}
    
//结束指定进程
        public static void EndProcess(int pid)
        {
            try
            {
                Process process = Process.GetProcessById(pid);
                process.Kill();
            }
            catch { }
        }
        
//结束进程树(待测试)
        public static void KillProcessTree(Process process)
        {
            string taskkill = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "taskkill.exe");
            using (var procKiller = new Process())
            {
                procKiller.StartInfo.FileName = taskkill;
                procKiller.StartInfo.Arguments = string.Format("/PID {0} /T /F", process.Id);
                procKiller.StartInfo.CreateNoWindow = true;
                procKiller.StartInfo.UseShellExecute = false;
                procKiller.Start();
                procKiller.WaitForExit(1000);   // wait 1sec
            }
        }
        
//运行状态——为true则正在运行，FALSE则为已暂停
process.Responding

//名称
process.ProcessName

//私有工作集
	public static float getWorkingSet(string processName)
	{
		//var processName = "taskmgr";
		using (var process = Process.GetProcessesByName(processName)[0])
		using (var p1 = new PerformanceCounter("Process", "Working Set - Private", processName))
		using (var p2 = new PerformanceCounter("Process", "Working Set", processName))
		{
			//Console.WriteLine(process.Id);
			//注意除以CPU数量
			//Console.WriteLine("{0}{1:N} KB", "工作集（进程类）", process.WorkingSet64 / 1024);
			//Console.WriteLine("{0}{1:N} KB", "工作集 ", process.WorkingSet64 / 1024);
			//Console.WriteLine("{0}{1:N} KB", "私有工作集 ", p1.NextValue() / 1024);
			return p1.NextValue() / 1024;
			//Logger("{0}；内存（专用工作集）{1:N}；PID:{2}；程序名：{3}", DateTime.Now, p1.NextValue() / 1024, process.Id.ToString(), processName);

			// Thread.Sleep(1000);
		}
	}

	static void Main(string[] args)
	{
		string ProcessName;
		Process[] ps = Process.GetProcesses();
		foreach (Process p in ps)
		{
			if (p.MainWindowHandle != null)
			{
				ProcessName = p.ProcessName;
				Console.WriteLine(getWorkingSet(ProcessName));
			}
		}
		Console.ReadKey();
	}
    
    
//进程的CPU占有率(通过进程名获取)
	class Program
	{
		static Process[] runningNow = Process.GetProcesses();
		static void Main(string[] args)
		{
			foreach (Process process in runningNow)
			{
				using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", process.ProcessName))
				using (PerformanceCounter memProcess = new PerformanceCounter("Memory", "Available MBytes"))
				{
					pcProcess.NextValue();
					Thread.Sleep(1000);
					Console.WriteLine("");
					Console.ForegroundColor = ConsoleColor.Red;
					float cpuUseage = pcProcess.NextValue();
					Console.WriteLine("Process: '{0}' CPU Usage: {1}%", process.ProcessName, cpuUseage);
					Console.ForegroundColor = ConsoleColor.Green;
					float memUseage = memProcess.NextValue();
					Console.WriteLine("Process: '{0}' RAM Free: {1}MB", process.ProcessName, memUseage);
				}
			}
		}
	}

//进程的CPU占有率(通过ID获取示例)
	class Program
	{
		static void Main(string[] args)
		{
			// grab all Chrome process instances
			var processes = Process.GetProcessesByName("chrome");

			for (int i = 0; i < 10; i++)
			{
				foreach (var p in processes)
				{
					var counter = ProcessCpuCounter.GetPerfCounterForProcessId(p.Id);

					// start capturing
					counter.NextValue();

					Thread.Sleep(200);

					var cpu = counter.NextValue() / (float)Environment.ProcessorCount;
					Console.WriteLine(counter.InstanceName + " -  Cpu: " + cpu);
				}

			}

			Console.WriteLine("Any key to exit...");
			Console.Read();
		}

		private static string GetProcessInstanceName(int pid)
		{
			PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");

			string[] instances = cat.GetInstanceNames();
			foreach (string instance in instances)
			{

				using (PerformanceCounter cnt = new PerformanceCounter("Process",
					"ID Process", instance, true))
				{
					int val = (int)cnt.RawValue;
					if (val == pid)
					{
						return instance;
					}
				}
			}
			throw new Exception("Could not find performance counter " +
			                    "instance name for current process. This is truly strange ...");
		}
	}

	public class ProcessCpuCounter
	{
		public static PerformanceCounter GetPerfCounterForProcessId(int processId, string processCounterName = "% Processor Time")
		{
			string instance = GetInstanceNameForProcessId(processId);
			if (string.IsNullOrEmpty(instance))
				return null;

			return new PerformanceCounter("Process", processCounterName, instance);
		}

		public static string GetInstanceNameForProcessId(int processId)
		{
			var process = Process.GetProcessById(processId);
			string processName = Path.GetFileNameWithoutExtension(process.ProcessName);

			PerformanceCounterCategory cat = new PerformanceCounterCategory("Process");
			string[] instances = cat.GetInstanceNames()
				.Where(inst => inst.StartsWith(processName))
				.ToArray();

			foreach (string instance in instances)
			{
				using (PerformanceCounter cnt = new PerformanceCounter("Process",
					"ID Process", instance, true))
				{
					int val = (int)cnt.RawValue;
					if (val == processId)
					{
						return instance;
					}
				}
			}
			return null;
		}
	}
    
    
//CPU总的利用率
			ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_PerfFormattedData_PerfOS_Processor");

			while (true)
			{
				//Thread.Sleep(1000);
				foreach (ManagementObject obj in searcher.Get())
				{
					var usage = obj["PercentProcessorTime"];
					var name = obj["Name"];
					Console.WriteLine(name + " : " + usage);
				}
			}
            
            
//描述
class Program
{
	[Flags]
	public enum ProcessAccessFlags : uint
	{
		PROCESS_ALL_ACCESS = 0x001F0FFF,
		PROCESS_CREATE_PROCESS = 0x00000080,
		PROCESS_CREATE_THREAD = 0x00000002,
		PROCESS_DUP_HANDLE = 0x00000040,
		PROCESS_QUERY_INFORMATION = 0x00000400,
		PROCESS_QUERY_LIMITED_INFORMATION = 0x00001000,
		PROCESS_SET_INFORMATION = 0x00000200,
		PROCESS_SET_QUOTA = 0x00000100,
		PROCESS_SUSPEND_RESUME = 0x00000800,
		PROCESS_TERMINATE = 0x00000001,
		PROCESS_VM_OPERATION = 0x00000008,
		PROCESS_VM_READ = 0x00000010,
		PROCESS_VM_WRITE = 0x00000020
	}

	static void Main(string[] args)
	{
		Process[] process = Process.GetProcesses();

		//Process p1 = Process.GetProcessById(3856);

		int i = 1;
		//Console.WriteLine(process.Length);
		foreach (var p in process)
		{
			try
			{
				FileVersionInfo myFileVersionInfo =
					FileVersionInfo.GetVersionInfo(GetExecutablePath(p));
				Console.WriteLine(myFileVersionInfo.FileDescription);
			}
			catch (Exception e)
			{
				Console.WriteLine("AAAAAAAAAAAAA");
				//throw;
			}
				Console.WriteLine(i++);

		}
		//Console.WriteLine(GetExecutablePathAboveVista(p1.Id));
		Console.ReadKey();

	}




	private static string GetExecutablePath(Process Process)
	{
		//If running on Vista or later use the new function
		if (Environment.OSVersion.Version.Major >= 6)
		{
			return GetExecutablePathAboveVista(Process.Id);
		}

		return Process.MainModule.FileName;
	}

	private static string GetExecutablePathAboveVista(int ProcessId)
	{
		var buffer = new StringBuilder(1024);
		IntPtr hprocess = OpenProcess(ProcessAccessFlags.PROCESS_QUERY_LIMITED_INFORMATION,
			false, ProcessId);
		if (hprocess != IntPtr.Zero)
		{
			try
			{
				int size = buffer.Capacity;
				if (QueryFullProcessImageName(hprocess, 0, buffer, out size))
				{
					return buffer.ToString();
				}
			}
			finally
			{
				CloseHandle(hprocess);
			}
		}
		//throw new Win32Exception(Marshal.GetLastWin32Error());
		return @"C:\Windows\System32";
	}

	[DllImport("kernel32.dll")]
	private static extern bool QueryFullProcessImageName(IntPtr hprocess, int dwFlags,
		StringBuilder lpExeName, out int size);
	[DllImport("kernel32.dll")]
	private static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess,
		bool bInheritHandle, int dwProcessId);

	[DllImport("kernel32.dll", SetLastError = true)]
	private static extern bool CloseHandle(IntPtr hHandle);
}