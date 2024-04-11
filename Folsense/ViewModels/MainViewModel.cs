using Folsense.Bases;
using Folsense.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Folsense.ViewModels
{
    public class MainViewModel : BaseClass
    {
        private Dictionary<Type, UserControl> _viewInstances;
        public Dictionary<Type, UserControl> ViewInstances
        {
            get { return _viewInstances; }
            set { SetProperty(ref _viewInstances, value); }
        }

        private UserControl? _currentView;

        public UserControl? CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView, value); }
        }

        public DelegateCommand NavigateCommand { get; set; }

        public MainViewModel()
        {
            NavigateCommand = new DelegateCommand(Navigate);
            ViewInstances = new Dictionary<Type, UserControl>()
            {
                { typeof(DashboardView), new DashboardView() },
                { typeof(AccountView), new AccountView() },
                { typeof(SettingsView), new SettingsView() }
            };
            CurrentView = ViewInstances[typeof(DashboardView)];

            uint PID = Tools.Process.GetProcessPID("");
            string mod_name = "mono-2.0-bdwgc.dll";

            IntPtr moduleBase = Tools.Process.GetModuleBaseAddress(PID, mod_name);
            IntPtr pHandle = External.Kernel32.OpenProcess(
                (uint)External.Kernel32.ProcessAccessFlags.All,
                false,
                PID
            );
            if (pHandle == IntPtr.Zero)
            {
                Console.WriteLine("Failed to open process.");
                return;
            }

            IntPtr dynamicPtrBaseAddr = (IntPtr)(moduleBase.ToInt32() + 0x009F1A00);
            int[] monstOffsets = { 0x270, 0xA00, 0x338, 0x208, 0x10, 0x68, 0x498 };
            int[] moneyOffsets = { 0x270, 0xA00, 0x338, 0x208, 0x10, 0x68, 0x4e8 };
            int[] spirOffsets = { 0x270, 0xA00, 0x338, 0x208, 0x10, 0x68, 0x4b8 };

            IntPtr monstAddr = Tools.Process.FindDMAAddy(pHandle, dynamicPtrBaseAddr, monstOffsets);
            IntPtr moneyAddr = Tools.Process.FindDMAAddy(pHandle, dynamicPtrBaseAddr, moneyOffsets);
            IntPtr spirAddr = Tools.Process.FindDMAAddy(pHandle, dynamicPtrBaseAddr, spirOffsets);

            double spirAmount = 1337;
        }

        private void Navigate(object viewType)
        {
            CurrentView = ViewInstances[(Type)viewType];
        }
    }
}
