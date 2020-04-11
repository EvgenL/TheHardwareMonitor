using System;
using MissingLinq.Linq2Management.Context;
using MissingLinq.Linq2Management.Model.CIMv2;
using System.Linq;

namespace Core
{
    public static class Hardware
    {

        private static ManagementObjectContext _mc;
        public static ManagementObjectContext context =>
            _mc == null ? new ManagementObjectContext() : null;
    }
}
