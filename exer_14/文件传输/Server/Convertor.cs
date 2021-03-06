﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace trans
{
    public class Convertor
    {
        //静态方法，可以将结构体转换为字节数组
        public static Byte[] StructToBytes(Object structure)
        {
            Int32 size = Marshal.SizeOf(structure);
            Console.WriteLine(size);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(structure, buffer, false);
                Byte[] bytes = new Byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        //将字节流重新转换为object对象，之后使用需要用对应结构体强转
        public static Object BytesToStruct(Byte[] bytes, Type strcutType)
        {
            Int32 size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return Marshal.PtrToStructure(buffer, strcutType);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        
    }
}
