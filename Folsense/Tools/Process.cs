using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Folsense.External;
using Folsense.Models;

namespace Folsense.Tools
{
    public class Process
    {
        public static IntPtr GetModuleBaseAddress(uint procId, string modName)
        {
            IntPtr modBaseAddr = IntPtr.Zero;
            IntPtr hSnap = Kernel32.CreateToolhelp32Snapshot(Tlhelp32.TH32CS_SNAPMODULE | Tlhelp32.TH32CS_SNAPMODULE32, procId);
            if (hSnap != IntPtr.Zero)
            {
                Kernel32.MODULEENTRY32 modEntry = new Kernel32.MODULEENTRY32();
                modEntry.dwSize = (uint)Marshal.SizeOf(typeof(Kernel32.MODULEENTRY32));
                if (Kernel32.Module32First(hSnap, ref modEntry))
                {
                    do
                    {
                        if (modEntry.szModule == modName)
                        {
                            modBaseAddr = modEntry.modBaseAddr;
                            break;
                        }
                    } while (Kernel32.Module32Next(hSnap, ref modEntry));
                }
            }
            return modBaseAddr;
        }

        public static IntPtr FindDMAAddy(IntPtr hProc, IntPtr ptr, int[] offsets)
        {
            IntPtr addr = ptr;
            byte[]? bytes = null;

            foreach (int offset in offsets)
            {
                Kernel32.ReadProcessMemory(hProc, addr, bytes, 4, out _);
                addr = (IntPtr)(addr.ToInt32() + offset);
            }

            return addr;
        }

        public static uint GetProcessPID(string name)
        {
            System.Diagnostics.Process[] processes = System.Diagnostics.Process.GetProcessesByName(name);

            if (processes.Length == 0)
            {
                return (0);
            }

            return ((uint)processes[0].Id);
        }
    }
}
