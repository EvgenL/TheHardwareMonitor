using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIMv2 = MissingLinq.Linq2Management.Model.CIMv2;

namespace Core
{
    public static class Gpu
    {
        public static int currGpuIndex = 0;
        public static int currGpuLevel = 0;

        private static CIMv2.Win32VideoController[] cards
            => Hardware.context.CIMv2.Win32VideoControllers.ToArray();
        private static CIMv2.Win32VideoController currGpu
            => cards.Length > 0 ? cards[currGpuIndex] : null;
        public static string[] cardsNames => cards.Length > 0  ? cards.Select(x => x.Name).ToArray() : new string[0];
        public static string name => currGpu != null ? currGpu.Name : "";
        public static string manufacturer => currGpu != null ? currGpu.AdapterCompatibility : "";
        public static string codename => currGpu != null ? currGpu.VideoProcessor : "";
        public static string rev => currGpu != null ? currGpu.SpecificationVersion.ToString() : "";
        public static string thicc => "";
        public static string coreClock => "";
        public static string shaders => "";
        public static string videoClock => "";
        public static string memSize => currGpu != null ? currGpu.MaxMemorySupported != 0 ? currGpu.MaxMemorySupported / 1024 / 1024 + " Мб" : "" : "";
        public static string memType => Enum.GetName(typeof(CIMv2.Win32VideoControllerVideoMemoryType), currGpu.VideoMemoryType);
        public static string memWidth => Hardware.context.CIMv2.Win32PhysicalMemories.ToList().Count > 0 ? Hardware.context.CIMv2.Win32PhysicalMemories.ToList()[0].DataWidth / 8 + " Байт" : "";

    }
}
