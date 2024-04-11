using Folsense.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Folsense.Models
{
    public class Tlhelp32 : BaseClass
    {
        public static uint TH32CS_INHERIT = 0x80000000;
        public static uint TH32CS_SNAPHEAPLIST = 0x00000001;
        public static uint TH32CS_SNAPMODULE = 0x00000008;
        public static uint TH32CS_SNAPMODULE32 = 0x00000010;
        public static uint TH32CS_SNAPPROCESS = 0x00000002;
        public static uint TH32CS_SNAPTHREAD = 0x00000004;
        public static uint TH32CS_SNAPALL = (TH32CS_SNAPHEAPLIST | TH32CS_SNAPMODULE | TH32CS_SNAPPROCESS | TH32CS_SNAPTHREAD);
    }
}
