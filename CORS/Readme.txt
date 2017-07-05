CORS跨域方法测试

1 需要确保服务端、客户端不在一个域，否则无法实现跨域测试。
2 服务端安装CORS NUGET包; Install-Package Microsoft.AspNet.WebApi.Cors
3 服务端EnableCors， WebApiConfig类，  Register(HttpConfiguration config)方法下，注册 config.EnableCors();
4 服务端Controller前前添加[EnableCors] attribute
[EnableCors(origins: "*", headers: "*", methods: "*")]

测试成功！



参考文献
MSDN原文教程: https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api
<跨域资源共享 CORS 详解>---阮一峰---http://www.ruanyifeng.com/blog/2016/04/cors.html
