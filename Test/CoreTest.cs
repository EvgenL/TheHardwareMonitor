using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;

namespace Test
{
    [TestClass]
    public class CoreTest
    {
        [TestMethod]
        public void ProcessorsTest()
        {
            var processors = Cpu.processors;
            foreach (string cpu in processors)
            {
                Console.WriteLine(cpu);
            }
            Assert.IsTrue(processors.Length > 0);
        }
    }
}
