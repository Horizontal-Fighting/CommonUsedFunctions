MoQ(����.net3.5,c#3.0��mock���)�򵥽���
http://www.cnblogs.com/nuaalfm/archive/2009/11/25/1610755.html

����������Ԫ���Ե�ʱ�򣬳������������ݵĳ־û����⣬�ܶ���������ǲ�ϣ����Ԫ����Ӱ�쵽���ݿ��е����ݣ����������ݿ��Ӱ����ʱ���ǵĵ�Ԫ���Ե��ٶȻ������������������ϣ�����־û����ָ��뿪��
����Ԫ���Ե�ʱ�������Ľ����ݳ־û���
���ָ�������һ��ʹ�ó���ķ�ʽ��Ҳ�������ýӿڻ�����ཫ�־û�����뿪��Ȼ������mock��ģ����Ӧ�Ľӿڻ�������������Ӧ�ĳ־û��ࡣ
MoQ��������Mock���֮һ��MoQʹ����C#3.0����NMock���MoQʹ���������򵥣�������ǿ���͵ķ�ʽ�ģ�Դ���dll���Ե�http://code.google.com/p/moq/���ء�
����MoQ���µķ����汾��3.1�棬4.0������beta���У�������������ʹ�õ���3.1�档

�������Ǿ�������һ��MoQ�ľ����÷���

һ������֪ʶ

��ʹ��MoQ֮ǰ���Ǳ���Ҫ���ڲ��Գ���������Moq.dll��ʹ��MoQ����Ҫ�����ռ���Moq���������ص������Mock<T>�����ǿ������������ģ��ӿڡ�

1������

   public interface ITest
    {
        string Test();
    }
���Դ��룺

 


1      [TestMethod()]
2         public void TestTest()
3         {
4             var test = new Mock<ITest>();
5             test.Setup(p => p.Test()).Returns("lfm");
6             Assert.AreEqual("lfm", test.Object.Test());
7         }
 

2��ƥ�����

 

 public interface IMatchTest
    {
        string Test(int test);
    }
 


var testMatch = new Mock<IMatchTest>();
            testMatch.Setup(p => p.Test(It.Is<int>(i => i % 2 == 0))).Returns("ż��");
            testMatch.Setup(p => p.Test(It.Is<int>(i => i % 2 != 0))).Returns("����");
            Assert.AreEqual("ż��", testMatch.Object.Test(4));
            Assert.AreEqual("����", testMatch.Object.Test(3));
 �ϱ߲��Դ���ģ��ʵ��IMathTest�ӿ�ʵ�����������Test�����Ĳ�����ż�����䷵��ֵΪ��ż�����������IT�������˲������࣬�������Ϳ��Բμ�MoQ���ĵ�

3������
 public interface IPropertiesTest
    {
         int Test { get; set; }
    }
 

            var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1);
            Assert.AreEqual(1, testProperties.Object.Test);
����

var testProperties = new Mock<IPropertiesTest>();
            testProperties.SetupProperty(p => p.Test,1);
            Assert.AreEqual(1, testProperties.Object.Test);
 

4��Callback

��ִ��ĳ����ʱ�������ڲ������Actionί��

 


int count = 0;
            var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1).Callback(()=>count++);
            Assert.AreEqual(1, testProperties.Object.Test);
            Assert.AreEqual(1, count);
 

�ڵ���Test������ִ����count++

5��Verification

�ж�ĳ�����������Ƿ�ִ�й�

����������£�

 


1 var testProperties = new Mock<IPropertiesTest>();
2             testProperties.Setup(p => p.Test).Returns(1);
3             testProperties.Verify(p => p.Test);
4             Assert.AreEqual(1, testProperties.Object.Test);
 

���׳��쳣����Ϊ��3��ִ��ʱTest������û�б����ù�����Ϊ���´������ͨ������

 


     var testProperties = new Mock<IPropertiesTest>();
            testProperties.Setup(p => p.Test).Returns(1);

            Assert.AreEqual(1, testProperties.Object.Test);
            testProperties.Verify(p => p.Test);
����ϸ�ڿ��Բ鿴MoQ�ĵ���