using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIMv2 = MissingLinq.Linq2Management.Model.CIMv2;

namespace Core
{
    public static class Mainboard
    {
        private static CIMv2.Win32MotherboardDevice motheroard
        {
            get {
                var boards = Hardware.context.CIMv2.Win32MotherboardDevices.ToList();
                if (boards.Count > 0) return boards[0];
                return null;
            }
        } 
        private static CIMv2.Win32BaseBoard baseBoard {
            get {
                var boards = Hardware.context.CIMv2.Win32BaseBoards.ToList();
                if (boards.Count > 0) return boards[0];
                return null;
            }
        } 
        private static CIMv2.Win32BIOS bios
        {
            get {
                var bios = Hardware.context.CIMv2.Win32BIOSs.GetEnumerator().Current;
                if (bios != null) return bios;
                return null;
            }
        }
        public static string manufacturer => baseBoard != null ? baseBoard.Manufacturer : "";
        public static string model => baseBoard != null ? baseBoard.Product : "";
        public static string modelRev => baseBoard != null ? baseBoard.Version : "";
        public static string chipsetName => "nan";
        public static string chipsetNumber => "nan";
        public static string chipsetRev => "nan";
        public static string southBridgeName => "";
        public static string southBridgeNumber => "";
        public static string southBridgeRev => "";
        public static string lpcioBridgeName => "";
        public static string lpcioBridgeNumber => "";
        public static string biosBrand => bios != null ? bios.Manufacturer : "";
        public static string biosVersion => bios != null ? bios.SMBIOSBIOSVersion : "";
        public static string biosDate => bios != null ? bios.ReleaseDate.ToString() : "";
        public static string gbusVersion => motheroard != null ? motheroard.PrimaryBusType : "";
    }
}
