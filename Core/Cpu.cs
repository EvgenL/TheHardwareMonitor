
using System;
using System.Linq;
using MissingLinq.Linq2Management.Context;
using CIMv2 = MissingLinq.Linq2Management.Model.CIMv2;

namespace Core
{
    public static class Cpu
    {
        private static ManagementObjectContext _mc;
        private static ManagementObjectContext mc =>
            _mc == null ? new ManagementObjectContext() : null;

        public static int currCpuIndex = 0;

        private static MissingLinq.Linq2Management.Model.CIMv2.Win32Processor
            currCpu => mc.CIMv2.Win32Processors.ToArray()[currCpuIndex];

        public static string[] processors
        {
            get
            {
                var cpus = mc.CIMv2.Win32Processors.ToList();
                var a =
                    from cp in cpus select processorNameFromSpec(cp.Name);
                return a.ToArray();
            }
        }

        // processor
        public static string currentProcessorName => processorNameFromSpec(specification);
        public static string processorNameFromSpec(string spec)
        {
            if (spec.Contains("CPU"))
                spec = spec.Substring(0, spec.IndexOf("CPU") - 1);
            return spec;
        }
        public static string codename => "nan";
        public static string heatDiss => "nan";
        public static string package => "nan";
        public static string thickness => "nan";
        public static string voltage => (currCpu.CurrentVoltage / 10f).ToString();
        public static string specification => currCpu.Name;
        public static string familyNumber => currCpu.Family.ToString();
        public static string model => ((int)currCpu.ProcessorType).ToString();
        public static string internalVersion => currCpu.Version;
        public static string extFamily => currCpu.Family.ToString();
        public static string extModel => model;
        public static string coreRevision => currCpu.Version;
        public static string instructions => currCpu.SocketDesignation;

        // frequency
        public static string clock => currCpu.CurrentClockSpeed + " Мгц";
        public static string multiplier => (currCpu.CurrentClockSpeed / Math.Max(currCpu.ExtClock, 1)).ToString("N1");
        public static string outerFreq => currCpu.ExtClock + " Мгц";
        public static string effectiveFreq => currCpu.MaxClockSpeed + " Мгц";

        // chache info
        public static string level1cacheData => Cache.HasData ? Cache.Data.InstalledSize + " Кб" : "";
        public static string level1cacheInst => Cache.HasInstruction ? Cache.Instruction.InstalledSize + " Кб" : "";
        public static string level1cache => Cache.HasL1 ? Cache.L1.InstalledSize + " Кб" : "";
        public static string level2cache => Cache.HasL2 ? Cache.L2.InstalledSize + " Кб" : "";
        public static string level3cache => Cache.HasL3 ? Cache.L3.InstalledSize / 1024 + " МБ" : "";
        public static string level1cacheDataAssociativity => Cache.HasData ? Cache.AsscToStr(Cache.Data.Associativity) : "";
        public static string level1cacheInstAssociativity => Cache.HasInstruction ? Cache.AsscToStr(Cache.Instruction.Associativity) : "";
        public static string level1cacheAssociativity => Cache.HasL1 ? Cache.AsscToStr(Cache.L1.Associativity) : "";
        public static string level2cacheAssociativity => Cache.HasL2 ? Cache.AsscToStr(Cache.L2.Associativity) : "";
        public static string level3cacheAssociativity => Cache.HasL3 ? Cache.AsscToStr(Cache.L3.Associativity) : "";

        // other
        public static string coreCount => currCpu.NumberOfCores.ToString();
        public static string logicalCoreCount => currCpu.NumberOfLogicalProcessors.ToString();


        private static class Cache
        {
            public static bool HasAny = caches.Length > 0;
            public static bool HasData = HasAny && Data != null;
            public static bool HasInstruction = HasAny && Instruction != null;
            public static bool HasL1 = HasAny && L1 != null;
            public static bool HasL2 = HasAny && L2 != null;
            public static bool HasL3 = HasAny && L3 != null;

            private static CIMv2.Win32CacheMemory[] caches
                => mc.CIMv2.Win32CacheMemories.ToArray();

            public static CIMv2.Win32CacheMemory Data 
                => HasAny ? caches.SingleOrDefault(c => c.CacheType == CIMv2.Win32CacheMemoryCacheType.Data)
                : null;
            public static CIMv2.Win32CacheMemory Instruction 
                => HasAny ? caches.SingleOrDefault(c => c.CacheType == CIMv2.Win32CacheMemoryCacheType.Instruction)
                : null;
            public static CIMv2.Win32CacheMemory L1
                => HasAny ? caches.SingleOrDefault(c => c.Level == CIMv2.Win32CacheMemoryLevel.Primary)
                : null;
            public static CIMv2.Win32CacheMemory L2
                => HasAny ? caches.SingleOrDefault(c => c.Level == CIMv2.Win32CacheMemoryLevel.Secondary)
                : null;
            public static CIMv2.Win32CacheMemory L3
                => HasAny ? caches.SingleOrDefault(c => c.Level == CIMv2.Win32CacheMemoryLevel.Tertiary)
                : null;

            public static string AsscToStr(CIMv2.Win32CacheMemoryAssociativity assc)
            {
                switch (assc)
                {
                    case CIMv2.Win32CacheMemoryAssociativity.Other: return "Other";
                    case CIMv2.Win32CacheMemoryAssociativity.Unknown: return "Unknown";
                    case CIMv2.Win32CacheMemoryAssociativity.DirectMapped: return "Direct";
                    case CIMv2.Win32CacheMemoryAssociativity.FullyAssociative: return "Full";
                    case CIMv2.Win32CacheMemoryAssociativity._2WaySetAssociative: return "2-way";
                    case CIMv2.Win32CacheMemoryAssociativity._4WaySetAssociative: return "4-way";
                    case CIMv2.Win32CacheMemoryAssociativity._8WaySetAssociative: return "8-way";
                    case CIMv2.Win32CacheMemoryAssociativity._16WaySetAssociative: return "16-way";
                    default: return "";
                }
            }
        }
    }
}