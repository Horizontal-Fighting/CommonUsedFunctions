using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//link：http://www.cnblogs.com/nuaalfm/archive/2009/11/25/1610755.html
namespace NUnit.TestsMoq
{
    [TestFixture]
    public class TestClass
    {
        /// <summary>
        /// 方法
        /// </summary>
        [Test]
        public void TestMethod1()
        {
            var test = new Mock<ITest>();
            test.Setup(r => r.Test()).Returns("lfm");
            Assert.AreEqual("lfm",test.Object.Test());
        }

        /// <summary>
        /// 匹配参数2
        /// 模拟实现IMathTest接口实例，其中如果Test方法的参数是偶数，其返回值为“偶数”。
        /// 这里的IT用来过滤参数的类，其具体解释可以参见MoQ的文档
        /// </summary>
        [Test]
        public void TestMethod2()
        {
            var testMatch = new Mock<IMatchTest>();
            testMatch.Setup(p => p.Test(It.Is<int>(i => i % 2 == 0))).Returns("偶数");
            testMatch.Setup(p => p.Test(It.Is<int>(i => i % 2 != 0))).Returns("奇数");
            Assert.AreEqual("偶数", testMatch.Object.Test(4));
            Assert.AreEqual("奇数", testMatch.Object.Test(3));
        }

        /// <summary>
        /// 匹配参数3
        /// </summary>
        [Test]
        public void TestMethod3()
        {
            var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1);
            Assert.AreEqual(1, testProperties.Object.Test);
        }

        /// <summary>
        /// 匹配参数4
        /// 当执行某方法时调用其内部输入的Action委托
        /// </summary>
        [Test]
        public void TestMethod4()
        {
            int count = 0;
            var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1).Callback(() => count++);
            Assert.AreEqual(1, testProperties.Object.Test);
            Assert.AreEqual(1, count);
        }


        /// <summary>
        /// Verification,判断某方法或属性是否执行过
        /// </summary>
        [Test]
        public void TestMethod5()
        {
            var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1);

            Assert.AreEqual(1, testProperties.Object.Test);
            testProperties.Verify(p => p.Test);
        }

    }
}
