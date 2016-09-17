automated testing framework

benefits
higher quality code
reduced costs
regression safety net

NUNIT Youtube tuturial：https://www.youtube.com/watch?v=0VbLAh51IoI
NUNIT Chinese tutorial：http://www.cnblogs.com/zwt-blog/p/5788222.html

NUnit一共有五个断言类，分别是Assert、StringAssert、FileAssert、DirectoryAssert、CollectionAssert，它们都在NUnit.Framework命名空间，其中Assert是常用的，而另外四个断言类，顾名思义，分别对应于字符串的断言、文件的断言、目录的断言、集合的断言。

核心dll
nunit.Framework.dll
nunit.mocks.dll

1 Downloadnunit 
https://www.nuget.org/packages/NUnit/

vs2015->extensions and updates->online->nunit->NUnit Templates for Visual Studio->DownLoad

vs2015->extensions and updates->online->nunit->Nunit3 Test Adapter

2 new class  Project->  TestSetupIn2015

3 new Project->Test-> NUnit3 Test->name is UnitTest

4 UnitTest: TestClass.cs，  VS Menu bar->Test->Run->All Tests

assertEquals：Judge value equal; 用于判断实际值和期望值是否相同
assertSame：Judge reference equal; 判断实际值和期望值是否为同一个对象
