using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace Folsense.Services
{
    public class HWID : BaseClass
    {
        private string? _id;
        public string? Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        public HWID()
        {
            Id = GetHardwareID();
        }

        private string GetHardwareID()
        {
            string cpu = GetCPUID();
            string disk = GetDiskID();
            string user = GetSessionName();

            string combinedID = cpu + disk + user;

            byte[] hashBytes;
            using (SHA256 sha256 = SHA256.Create())
            {
                hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combinedID));
            }

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("X2"));
            }

            return (sb.ToString());
        }

        private string GetCPUID()
        {
            ManagementClass mc = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject m in moc)
            {
                return (m.Properties["ProcessorId"].Value.ToString());
            }

            return (null);
        }

        private string GetDiskID()
        {
            ManagementClass mc = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject m in moc)
            {
                return (
                    m.Properties["Model"].Value.ToString() +
                    m.Properties["SerialNumber"].Value.ToString()
                );
            }
                
            return (null);
        }

        private string GetSessionName()
        {
            return (Environment.UserName);
        }
    }
}
