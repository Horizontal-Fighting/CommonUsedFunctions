MoQ(基于.net3.5,c#3.0的mock框架)简单介绍
http://www.cnblogs.com/nuaalfm/archive/2009/11/25/1610755.html

我们在做单元测试的时候，常常困扰于数据的持久化问题，很多情况下我们不希望单元测试影响到数据库中的内容，而且受数据库的影响有时我们的单元测试的速度会很慢，所以我们往往希望将持久化部分隔离开，
做单元测试的时候不真正的将数据持久化。
这种隔离我们一般使用抽象的方式，也就是利用接口或抽象类将持久化层隔离开，然后利用mock来模拟相应的接口或抽象类来完成相应的持久化类。
MoQ就是这种Mock框架之一，MoQ使用了C#3.0，跟NMock相比MoQ使用起来更简单，而且是强类型的方式的，源码和dll可以到http://code.google.com/p/moq/下载。
现在MoQ最新的发布版本是3.1版，4.0还处在beta版中，所以我们这里使用的是3.1版。

下面我们就来介绍一下MoQ的具体用法：

一、基础知识

在使用MoQ之前我们必须要先在测试程序中引入Moq.dll，使用MoQ的主要命名空间是Moq，其中最重的类就是Mock<T>，我们可以用这个类来模拟接口。

1、方法

   public interface ITest
    {
        string Test();
    }
测试代码：

 


1      [TestMethod()]
2         public void TestTest()
3         {
4             var test = new Mock<ITest>();
5             test.Setup(p => p.Test()).Returns("lfm");
6             Assert.AreEqual("lfm", test.Object.Test());
7         }
 

2、匹配参数

 

 public interface IMatchTest
    {
        string Test(int test);
    }
 


var testMatch = new Mock<IMatchTest>();
            testMatch.Setup(p => p.Test(It.Is<int>(i => i % 2 == 0))).Returns("偶数");
            testMatch.Setup(p => p.Test(It.Is<int>(i => i % 2 != 0))).Returns("奇数");
            Assert.AreEqual("偶数", testMatch.Object.Test(4));
            Assert.AreEqual("奇数", testMatch.Object.Test(3));
 上边测试代码模拟实现IMathTest接口实例，其中如果Test方法的参数是偶数，其返回值为“偶数”。这里的IT用来过滤参数的类，其具体解释可以参见MoQ的文档

3、属性
 public interface IPropertiesTest
    {
         int Test { get; set; }
    }
 

            var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1);
            Assert.AreEqual(1, testProperties.Object.Test);
或者

var testProperties = new Mock<IPropertiesTest>();
            testProperties.SetupProperty(p => p.Test,1);
            Assert.AreEqual(1, testProperties.Object.Test);
 

4、Callback

当执行某方法时调用其内部输入的Action委托

 


int count = 0;
            var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1).Callback(()=>count++);
            Assert.AreEqual(1, testProperties.Object.Test);
            Assert.AreEqual(1, count);
 

在调用Test方法是执行了count++

5、Verification

判断某方法或属性是否执行过

如果代码如下：

 


1 var testProperties = new Mock<IPropertiesTest>();
2             testProperties.Setup(p => p.Test).Returns(1);
3             testProperties.Verify(p => p.Test);
4             Assert.AreEqual(1, testProperties.Object.Test);
 

会抛出异常，因为第3行执行时Test方法还没有被调用过，改为如下代码可以通过测试

 


     var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1);

            Assert.AreEqual(1, testProperties.Object.Test);
            testProperties.Verify(p => p.Test);
其他细节可以查看MoQ文档。