using System;
using MissingLinq.Linq2Management.Context;
using MissingLinq.Linq2Management.Model.CIMv2;
using System.Linq;

namespace Core
{
    public static class Hardware
    {


        private static ManagementObjectContext _mc;
        private static ManagementObjectContext mc
        {
            get
            {
                if (_mc == null) _mc = new ManagementObjectContext();
                return _mc;
            }
        }

        public static void Test()
        {

            // access objects on the local computer
            using (var mc = new ManagementObjectContext())
            {
                var processors = mc.CIMv2.Win32Processors;
              //  var a = mc.CIMv2.Win32MemoryDevices;
                var a1 = mc.CIMv2.Win32MotherboardDevices;
                var a12 = mc.CIMv2.Win32VideoControllers;
                //  var a13 = mc.CIMv2.Win32VideoConfigurations;
                var a4115 = mc.CIMv2.Win32CacheMemories.ToArray();

                // retrieve all shares
                var shares = mc.CIMv2.Win32Shares.ToList();
                foreach (var share in shares)
                {
                    System.Diagnostics.Debug.WriteLine(share.Name);
                }

                // filter
                var drives = mc.CIMv2.Win32Shares.Where(s => s.Type == Win32ShareType.DiskDrive).ToArray();
                foreach (var drive in drives)
                    System.Diagnostics.Debug.WriteLine(drive.Path);

                var cpu = mc.CIMv2.Win32Processors.ToList();
                foreach (var cp in cpu)
                {
                    Console.WriteLine(cp.Name);
                }
            }

            Console.WriteLine(Cpu.currentProcessorName);
            return;
        }
    }
}
