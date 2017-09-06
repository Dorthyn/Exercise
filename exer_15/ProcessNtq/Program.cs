using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace ProcessNtquery
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(GetHandles(Process.GetProcessesByName("chrome")[0]).Count);
            var processList = Win32Processes.GetProcess();
            foreach (var processInformation in processList)
            {
                int nLength = processInformation.ImageName.Length;
                byte[] baTemp = new byte[nLength];

                //读到ImageName中的是地址，但现在存的是数，所以需要进行还原为指针的操作
                IntPtr ptr = (IntPtr) processInformation.ImageName.Buffer;
                if (ptr!=IntPtr.Zero)
                {
                    //从指针起始位置位置开始读取指定长度字节数到字节数组
                    Marshal.Copy(ptr, baTemp, 0, nLength);

                    //将字节数组还原为字符串，注意Unicode编码
                    string str = Encoding.Unicode.GetString(baTemp);
                    Console.WriteLine(processInformation.UniqueProcessId + "\t\t" + str);
                }
                else
                {
                    Console.WriteLine(processInformation.UniqueProcessId + "\t\t" + "" );
                }
            }
            //Console.WriteLine(tmp);
            Console.ReadLine();
        }
    }

    public class Win32Processes
    {
        /// <summary>
        /// Return a list of processes that hold on the given file.
        /// </summary>
        public static List<Process> GetProcessesLockingFile(string filePath)
        {
            var procs = new List<Process>();

            var processListSnapshot = Process.GetProcesses();
            foreach (var process in processListSnapshot)
            {
                if (process.Id <= 4) { continue; } // system processes
                var files = GetFilesLockedBy(process);
                if (files.Contains(filePath)) procs.Add(process);
            }
            return procs;
        }

        #region C


        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct UNICODE_STRING
        {
            //[MarshalAs(UnmanagedType.U2)]
            public UInt16 Length;
            //[MarshalAs(UnmanagedType.U2)]
            public UInt16 MaximumLength;
            //[MarshalAs(UnmanagedType.U4)]
            public UInt32 Buffer;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SYSTEM_PROCESS_INFORMATION
        {
            public UInt32 NextEntryOffset;

            public UInt32 NumberOfThreads;

            public UInt64 WorkingSetPrivateSize; // since VISTA

            public UInt32 HardFaultCount; // since WIN7

            public UInt32 NumberOfThreadsHighWatermark; // since WIN7

            public UInt64 CycleTime; // since WIN7

            public UInt64 CreateTime;

            public UInt64 UserTime;

            public UInt64 KernelTime;

            public UNICODE_STRING ImageName;

            public UInt32 BasePriority;

            public UInt32 UniqueProcessId;

            public UInt32 InheritedFromUniqueProcessId;

            public UInt32 HandleCount;

            public UInt32 SessionId;

            public UInt32 UniqueProcessKey; // since VISTA (requires SystemExtendedProcessInformation)

            public UInt32 PeakVirtualSize;

            public UInt32 VirtualSize;

            public UInt32 PageFaultCount;

            public UInt32 PeakWorkingSetSize;

            public UInt32 WorkingSetSize;

            public UInt32 QuotaPeakPagedPoolUsage;

            public UInt32 QuotaPagedPoolUsage;

            public UInt32 QuotaPeakNonPagedPoolUsage;

            public UInt32 QuotaNonPagedPoolUsage;

            public UInt32 PagefileUsage;

            public UInt32 PeakPagefileUsage;

            public UInt32 PrivatePageCount;

            public UInt64 ReadOperationCount;

            public UInt64 WriteOperationCount;

            public UInt64 OtherOperationCount;

            public UInt64 ReadTransferCount;

            public UInt64 WriteTransferCount;

            public UInt64 OtherTransferCount;

            public ulong Threads;
        }


        struct CLIENT_ID
        {
            IntPtr UniqueProcess;
            IntPtr UniqueThread;
        }

        enum KWAIT_REASON
        {
            Executive,
            FreePage,
            PageIn,
            PoolAllocation,
            DelayExecution,
            Suspended,
            UserRequest,
            WrExecutive,
            WrFreePage,
            WrPageIn,
            WrPoolAllocation,
            WrDelayExecution,
            WrSuspended,
            WrUserRequest,
            WrEventPair,
            WrQueue,
            WrLpcReceive,
            WrLpcReply,
            WrVirtualMemory,
            WrPageOut,
            WrRendezvous,
            WrKeyedEvent,
            WrTerminated,
            WrProcessInSwap,
            WrCpuRateControl,
            WrCalloutStack,
            WrKernel,
            WrResource,
            WrPushLock,
            WrMutex,
            WrQuantumEnd,
            WrDispatchInt,
            WrPreempted,
            WrYieldExecution,
            WrFastMutex,
            WrGuardedMutex,
            WrRundown,
            WrAlertByThreadId,
            WrDeferredPreempt,
            MaximumWaitReason
        }

        #endregion

        /// <summary>
        /// Return a list of file locks held by the process.
        /// </summary>
        public static List<string> GetFilesLockedBy(Process process)
        {
            var outp = new List<string>();

            ThreadStart ts = delegate
            {
                try
                {
                    outp = UnsafeGetFilesLockedBy(process);
                }
                catch { Ignore(); }
            };

            try
            {
                var t = new Thread(ts);
                t.IsBackground = true;
                t.Start();
                if (!t.Join(250))
                {
                    try
                    {
                        t.Interrupt();
                        t.Abort();
                    }
                    catch { Ignore(); }
                }
            }
            catch { Ignore(); }

            return outp;
        }


        #region Inner Workings
        private static void Ignore() { }
        private static List<string> UnsafeGetFilesLockedBy(Process process)
        {
            try
            {
                var handles = GetHandles(process);
                var files = new List<string>();

                foreach (var handle in handles)
                {
                    var file = GetFilePath(handle, process);
                    if (file != null) files.Add(file);
                }

                return files;
            }
            catch
            {
                return new List<string>();
            }
        }

        const int CNST_SYSTEM_HANDLE_INFORMATION = 16;
        const int CNST_SYSTEM_PROCESS_INFORMATION = 5;
        private static string GetFilePath(Win32API.SYSTEM_HANDLE_INFORMATION systemHandleInformation, Process process)
        {
            var ipProcessHwnd = Win32API.OpenProcess(Win32API.ProcessAccessFlags.All, false, process.Id);
            var objBasic = new Win32API.OBJECT_BASIC_INFORMATION();
            var objObjectType = new Win32API.OBJECT_TYPE_INFORMATION();
            var objObjectName = new Win32API.OBJECT_NAME_INFORMATION();
            var strObjectName = "";
            var nLength = 0;
            IntPtr ipTemp, ipHandle;

            if (!Win32API.DuplicateHandle(ipProcessHwnd, systemHandleInformation.Handle, Win32API.GetCurrentProcess(), out ipHandle, 0, false, Win32API.DUPLICATE_SAME_ACCESS))
                return null;

            IntPtr ipBasic = Marshal.AllocHGlobal(Marshal.SizeOf(objBasic));
            Win32API.NtQueryObject(ipHandle, (int)Win32API.ObjectInformationClass.ObjectBasicInformation, ipBasic, Marshal.SizeOf(objBasic), ref nLength);
            objBasic = (Win32API.OBJECT_BASIC_INFORMATION)Marshal.PtrToStructure(ipBasic, objBasic.GetType());
            Marshal.FreeHGlobal(ipBasic);

            IntPtr ipObjectType = Marshal.AllocHGlobal(objBasic.TypeInformationLength);
            nLength = objBasic.TypeInformationLength;
            // this one never locks...
            while ((uint)(Win32API.NtQueryObject(ipHandle, (int)Win32API.ObjectInformationClass.ObjectTypeInformation, ipObjectType, nLength, ref nLength)) == Win32API.STATUS_INFO_LENGTH_MISMATCH)
            {
                if (nLength == 0)
                {
                    Console.WriteLine("nLength returned at zero! ");
                    return null;
                }
                Marshal.FreeHGlobal(ipObjectType);
                ipObjectType = Marshal.AllocHGlobal(nLength);
            }

            objObjectType = (Win32API.OBJECT_TYPE_INFORMATION)Marshal.PtrToStructure(ipObjectType, objObjectType.GetType());
            if (Is64Bits())
            {
                ipTemp = new IntPtr(Convert.ToInt64(objObjectType.Name.Buffer.ToString(), 10) >> 32);
            }
            else
            {
                ipTemp = objObjectType.Name.Buffer;
            }

            var strObjectTypeName = Marshal.PtrToStringUni(ipTemp, objObjectType.Name.Length >> 1);
            Marshal.FreeHGlobal(ipObjectType);
            if (strObjectTypeName != "File")
                return null;

            nLength = objBasic.NameInformationLength;

            var ipObjectName = Marshal.AllocHGlobal(nLength);

            // ...this call sometimes hangs. Is a Windows error.
            while ((uint)(Win32API.NtQueryObject(ipHandle, (int)Win32API.ObjectInformationClass.ObjectNameInformation, ipObjectName, nLength, ref nLength)) == Win32API.STATUS_INFO_LENGTH_MISMATCH)
            {
                Marshal.FreeHGlobal(ipObjectName);
                if (nLength == 0)
                {
                    Console.WriteLine("nLength returned at zero! " + strObjectTypeName);
                    return null;
                }
                ipObjectName = Marshal.AllocHGlobal(nLength);
            }
            objObjectName = (Win32API.OBJECT_NAME_INFORMATION)Marshal.PtrToStructure(ipObjectName, objObjectName.GetType());

            if (Is64Bits())
            {
                ipTemp = new IntPtr(Convert.ToInt64(objObjectName.Name.Buffer.ToString(), 10) >> 32);
            }
            else
            {
                ipTemp = objObjectName.Name.Buffer;
            }

            if (ipTemp != IntPtr.Zero)
            {

                var baTemp = new byte[nLength];
                try
                {
                    Marshal.Copy(ipTemp, baTemp, 0, nLength);

                    strObjectName = Marshal.PtrToStringUni(Is64Bits() ? new IntPtr(ipTemp.ToInt64()) : new IntPtr(ipTemp.ToInt32()));
                }
                catch (AccessViolationException)
                {
                    return null;
                }
                finally
                {
                    Marshal.FreeHGlobal(ipObjectName);
                    Win32API.CloseHandle(ipHandle);
                }
            }

            string path = GetRegularFileNameFromDevice(strObjectName);
            try
            {
                return path;
            }
            catch
            {
                return null;
            }
        }

        private static string GetRegularFileNameFromDevice(string strRawName)
        {
            string strFileName = strRawName;
            foreach (string strDrivePath in Environment.GetLogicalDrives())
            {
                var sbTargetPath = new StringBuilder(Win32API.MAX_PATH);
                if (Win32API.QueryDosDevice(strDrivePath.Substring(0, 2), sbTargetPath, Win32API.MAX_PATH) == 0)
                {
                    return strRawName;
                }
                string strTargetPath = sbTargetPath.ToString();
                if (strFileName.StartsWith(strTargetPath))
                {
                    strFileName = strFileName.Replace(strTargetPath, strDrivePath.Substring(0, 2));
                    break;
                }
            }
            return strFileName;
        }

        /// <summary>
        /// 获取线程信息
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        private static IEnumerable<Win32API.SYSTEM_HANDLE_INFORMATION> GetHandles(Process process)
        {
            var nHandleInfoSize = 0x10000;
            var ipHandlePointer = Marshal.AllocHGlobal(nHandleInfoSize);
            var nLength = 0;
            IntPtr ipHandle;

            while (Win32API.NtQuerySystemInformation(CNST_SYSTEM_HANDLE_INFORMATION, ipHandlePointer, nHandleInfoSize, ref nLength) == Win32API.STATUS_INFO_LENGTH_MISMATCH)
            {
                nHandleInfoSize = nLength;
                Marshal.FreeHGlobal(ipHandlePointer);
                ipHandlePointer = Marshal.AllocHGlobal(nLength);
            }

            var baTemp = new byte[nLength];
            Marshal.Copy(ipHandlePointer, baTemp, 0, nLength);

            long lHandleCount;
            if (Is64Bits())
            {
                lHandleCount = Marshal.ReadInt64(ipHandlePointer);
                ipHandle = new IntPtr(ipHandlePointer.ToInt64() + 8);
            }
            else
            {
                lHandleCount = Marshal.ReadInt32(ipHandlePointer);
                ipHandle = new IntPtr(ipHandlePointer.ToInt32() + 4);
            }

            var lstHandles = new List<Win32API.SYSTEM_HANDLE_INFORMATION>();

            for (long lIndex = 0; lIndex < lHandleCount; lIndex++)
            {
                var shHandle = new Win32API.SYSTEM_HANDLE_INFORMATION();
                if (Is64Bits())
                {
                    shHandle = (Win32API.SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle) + 8);
                }
                else
                {
                    ipHandle = new IntPtr(ipHandle.ToInt64() + Marshal.SizeOf(shHandle));
                    shHandle = (Win32API.SYSTEM_HANDLE_INFORMATION)Marshal.PtrToStructure(ipHandle, shHandle.GetType());
                }
                if (shHandle.ProcessID != process.Id) continue;
                lstHandles.Add(shHandle);
            }
            return lstHandles;
        }

        //获取进程信息
        public static IEnumerable<Win32Processes.SYSTEM_PROCESS_INFORMATION> GetProcess()
        {
            var nProcessInfoSize = 0x100;
            var ipProcessPointer = Marshal.AllocHGlobal(nProcessInfoSize);
            var nLength = 0;
            var lstProcess = new List<Win32Processes.SYSTEM_PROCESS_INFORMATION>();
            IntPtr ipProcess;
            //
            while (Win32API.NtQuerySystemInformation(CNST_SYSTEM_PROCESS_INFORMATION, ipProcessPointer, nProcessInfoSize, ref nLength) == Win32API.STATUS_INFO_LENGTH_MISMATCH)
            {
                nProcessInfoSize = nLength;
                Marshal.FreeHGlobal(ipProcessPointer);
                ipProcessPointer = Marshal.AllocHGlobal(nLength);
            }

            var baTemp = new byte[nLength];
            SYSTEM_PROCESS_INFORMATION systemProcessInformation = new SYSTEM_PROCESS_INFORMATION();
            Marshal.Copy(ipProcessPointer, baTemp, 0, nLength);
            do
            {
                systemProcessInformation = (SYSTEM_PROCESS_INFORMATION)Marshal.PtrToStructure(ipProcessPointer, systemProcessInformation.GetType());
                lstProcess.Add(systemProcessInformation);
                ipProcessPointer = (IntPtr)((int)ipProcessPointer + systemProcessInformation.NextEntryOffset);
            } while (0 != systemProcessInformation.NextEntryOffset);


            //long lProcessCount;
            //if (Is64Bits())
            //{
            //    lProcessCount = Marshal.ReadInt64(ipProcessPointer);
            //    ipProcess = new IntPtr(ipProcessPointer.ToInt64() + 8);
            //}
            //else
            //{
            //    lProcessCount = Marshal.ReadInt32(ipProcessPointer);
            //    ipProcess = new IntPtr(ipProcessPointer.ToInt32() + 4);
            //}



            //for (long lIndex = 0; lIndex < lProcessCount; lIndex++)
            //{
            //    var shProcess = new Win32Processes.SYSTEM_PROCESS_INFORMATION();
            //    if (Is64Bits())
            //    {
            //        shProcess = (Win32Processes.SYSTEM_PROCESS_INFORMATION)Marshal.PtrToStructure(ipProcess, shProcess.GetType());
            //        ipProcess = new IntPtr(ipProcess.ToInt64() + Marshal.SizeOf(shProcess) + 8);
            //    }
            //    else
            //    {
            //        ipProcess = new IntPtr(ipProcess.ToInt64() + Marshal.SizeOf(shProcess));
            //        shProcess = (Win32Processes.SYSTEM_PROCESS_INFORMATION)Marshal.PtrToStructure(ipProcess, shProcess.GetType());
            //    }
            //    if ((int)shProcess.UniqueProcessId != process.Id) continue;
            //    lstProcess.Add(shProcess);
            //}
            return lstProcess;
        }


        private static bool Is64Bits()
        {
            return Marshal.SizeOf(typeof(IntPtr)) == 8;
        }

        internal class Win32API
        {
            [DllImport("ntdll.dll")]
            public static extern int NtQueryObject(IntPtr ObjectHandle, int
                    ObjectInformationClass, IntPtr ObjectInformation, int ObjectInformationLength,
                ref int returnLength);

            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);

            [DllImport("ntdll.dll")]
            public static extern uint NtQuerySystemInformation(int
                    SystemInformationClass, IntPtr SystemInformation, int SystemInformationLength,
                ref int returnLength);

            [DllImport("kernel32.dll")]
            public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);
            [DllImport("kernel32.dll")]
            public static extern int CloseHandle(IntPtr hObject);
            [DllImport("kernel32.dll", SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool DuplicateHandle(IntPtr hSourceProcessHandle,
                ushort hSourceHandle, IntPtr hTargetProcessHandle, out IntPtr lpTargetHandle,
                uint dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwOptions);
            [DllImport("kernel32.dll")]
            public static extern IntPtr GetCurrentProcess();

            public enum ObjectInformationClass
            {
                ObjectBasicInformation = 0,
                ObjectNameInformation = 1,
                ObjectTypeInformation = 2,
                ObjectAllTypesInformation = 3,
                ObjectHandleInformation = 4
            }

            [Flags]
            public enum ProcessAccessFlags : uint
            {
                All = 0x001F0FFF,
                Terminate = 0x00000001,
                CreateThread = 0x00000002,
                VMOperation = 0x00000008,
                VMRead = 0x00000010,
                VMWrite = 0x00000020,
                DupHandle = 0x00000040,
                SetInformation = 0x00000200,
                QueryInformation = 0x00000400,
                Synchronize = 0x00100000
            }

            internal enum SYSTEM_INFORMATION_CLASS
            {
                SystemBasicInformation = 0,
                SystemPerformanceInformation = 2,
                SystemTimeOfDayInformation = 3,
                SystemProcessInformation = 5,
                SystemProcessorPerformanceInformation = 8,
                SystemHandleInformation = 16,
                SystemInterruptInformation = 23,
                SystemExceptionInformation = 33,
                SystemRegistryQuotaInformation = 37,
                SystemLookasideInformation = 45
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct OBJECT_BASIC_INFORMATION
            { // Information Class 0
                public int Attributes;
                public int GrantedAccess;
                public int HandleCount;
                public int PointerCount;
                public int PagedPoolUsage;
                public int NonPagedPoolUsage;
                public int Reserved1;
                public int Reserved2;
                public int Reserved3;
                public int NameInformationLength;
                public int TypeInformationLength;
                public int SecurityDescriptorLength;
                public System.Runtime.InteropServices.ComTypes.FILETIME CreateTime;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct OBJECT_TYPE_INFORMATION
            { // Information Class 2
                public UNICODE_STRING Name;
                public int ObjectCount;
                public int HandleCount;
                public int Reserved1;
                public int Reserved2;
                public int Reserved3;
                public int Reserved4;
                public int PeakObjectCount;
                public int PeakHandleCount;
                public int Reserved5;
                public int Reserved6;
                public int Reserved7;
                public int Reserved8;
                public int InvalidAttributes;
                public GENERIC_MAPPING GenericMapping;
                public int ValidAccess;
                public byte Unknown;
                public byte MaintainHandleDatabase;
                public int PoolType;
                public int PagedPoolUsage;
                public int NonPagedPoolUsage;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct OBJECT_NAME_INFORMATION
            { // Information Class 1
                public UNICODE_STRING Name;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct UNICODE_STRING
            {
                public ushort Length;
                public ushort MaximumLength;
                public IntPtr Buffer;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct GENERIC_MAPPING
            {
                public int GenericRead;
                public int GenericWrite;
                public int GenericExecute;
                public int GenericAll;
            }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            public struct SYSTEM_HANDLE_INFORMATION
            { // Information Class 16
                public int ProcessID;
                public byte ObjectTypeNumber;
                public byte Flags; // 0x01 = PROTECT_FROM_CLOSE, 0x02 = INHERIT
                public ushort Handle;
                public int Object_Pointer;
                public UInt32 GrantedAccess;
            }

            public const int MAX_PATH = 260;
            public const uint STATUS_INFO_LENGTH_MISMATCH = 0xC0000004;
            public const int DUPLICATE_SAME_ACCESS = 0x2;
            public const uint FILE_SEQUENTIAL_ONLY = 0x00000004;
        }
        #endregion
    }
}
