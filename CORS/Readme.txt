CORS���򷽷�����

1 ��Ҫȷ������ˡ��ͻ��˲���һ���򣬷����޷�ʵ�ֿ�����ԡ�
2 ����˰�װCORS NUGET��; Install-Package Microsoft.AspNet.WebApi.Cors
3 �����EnableCors�� WebApiConfig�࣬  Register(HttpConfiguration config)�����£�ע�� config.EnableCors();
4 �����Controllerǰǰ���[EnableCors] attribute
[EnableCors(origins: "*", headers: "*", methods: "*")]

���Գɹ���



�ο�����
MSDNԭ�Ľ̳�: https://docs.microsoft.com/en-us/aspnet/web-api/overview/security/enabling-cross-origin-requests-in-web-api
<������Դ���� CORS ���>---��һ��---http://www.ruanyifeng.com/blog/2016/04/cors.html
