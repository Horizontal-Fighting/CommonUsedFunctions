using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlursightSight.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);

            string userName = "scott@scottbrad91.com";
            string password = "Password123!";

            //为了userID, 需要找到User
            IdentityUser user = userManager.FindByName(userName);

            //创建用户，create user
            //var creationResult = userManager.Create(new IdentityUser("scott@scottbrad91.com"),"Password123!");
            //Console.WriteLine("Created:{0}",creationResult.Succeeded);
            //Console.ReadKey();

            // 给用户创建Claim， Create Claims
            ////为了userID, 需要找到User
            //IdentityUser user = userManager.FindByName(userName);
            ////给用户添加Claims，后台自动使用EF存储到数据库
            //IdentityResult claimResult = userManager.AddClaim(user.Id, new System.Security.Claims.Claim("given_name","Scott"));
            //Console.WriteLine("Created:{0}", claimResult.Succeeded);
            //Console.ReadKey();

            //验证用户密码 verify user password
            bool isMatch = userManager.CheckPassword(user,password);
            Console.WriteLine("Created:{0}", isMatch);
            Console.ReadKey();

        }

        

        
    }
}
