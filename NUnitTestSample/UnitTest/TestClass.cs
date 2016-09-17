using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    [TestFixture]
    public class TestClass
    {
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            TestSetupIn2015.Class1 a = new TestSetupIn2015.Class1();
            int res = a.Add(1,2);
            Assert.AreEqual(3,res);
        }
    }
}
