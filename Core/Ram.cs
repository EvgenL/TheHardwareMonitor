using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIMv2 = MissingLinq.Linq2Management.Model.CIMv2;

namespace Core
{
    public static class Ram
    {
        public static int selectedDevice = 0;

        private static CIMv2.Win32PhysicalMemory ram
        {
            get
            {
                if (rams.Length > 0) return rams[selectedDevice];
                return null;
            }
        }
        private static CIMv2.Win32PhysicalMemory[] rams => Hardware.context.CIMv2.Win32PhysicalMemories.ToArray();
        public static string[] ramsNames => rams.Length > 0 ? rams.Select(x => x.Name).ToArray() : new string[0];

        public static string memType => ram != null ? ram.MemoryType.ToString() : "";
        public static string memSize => ram != null ? ram.Capacity / 1024 / 1024 + " Мб" : "";
        public static string manufacturer => ram != null ? ram.Manufacturer : "";
        public static string number => ram != null ? ram.BankLabel : "";
        public static string controllerClock => ram != null ? ram.Speed + " Мгц" : "";
        public static string ramClock => ram != null ? ram.Speed + " Мгц" : "";
        public static string wordWidth => ram != null ? ram.DataWidth / 8 + " Б" : "";
        public static string formFactor => ram != null ? ram.FormFactor.ToString() : "";
    }
}
