using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlursightSight.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //var userStore = new UserStore<IdentityUser>();
            //var userManager = new UserManager<IdentityUser>(userStore);

            string userName = "scott@scottbrad91.com";
            string password = "Password123!";

            var userStore = new CustomerUserStore(new CustomerUserDbContext());
            var userManager = new UserManager<CustomerUser, int>(userStore);

            var creationResult = userManager.Create(new CustomerUser { UserName=userName}, password);
            Console.WriteLine("Creation: {0}", creationResult.Succeeded);

            var user = userManager.FindByName(userName);

            //验证用户密码 || verify user password
            bool isMatch = userManager.CheckPassword(user, password);
            Console.WriteLine("Created:{0}", isMatch);
            Console.ReadKey();

            //为了userID, 需要找到User||find user by it's name
            //IdentityUser user = userManager.FindByName(userName);

            //创建用户|| create user
            //var creationResult = userManager.Create(new IdentityUser("scott@scottbrad91.com"),"Password123!");
            //Console.WriteLine("Created:{0}",creationResult.Succeeded);
            //Console.ReadKey();

            // 给用户创建Claim||Create Claims
            ////为了userID, 需要找到User
            //IdentityUser user = userManager.FindByName(userName);
            ////给用户添加Claims，后台自动使用EF存储到数据库
            //IdentityResult claimResult = userManager.AddClaim(user.Id, new System.Security.Claims.Claim("given_name","Scott"));
            //Console.WriteLine("Created:{0}", claimResult.Succeeded);
            //Console.ReadKey();

            //验证用户密码 || verify user password
            //bool isMatch = userManager.CheckPassword(user,password);
            //Console.WriteLine("Created:{0}", isMatch);
            //Console.ReadKey();

        }

        public class CustomerUser : IUser<int>
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string PasswordHash { get; set; }
        }

        public class CustomerUserDbContext : DbContext
        {
            public CustomerUserDbContext() : base("DefaultConnection") { }
            public DbSet<CustomerUser> Users { get; set; }

            protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                var user = modelBuilder.Entity<CustomerUser>();
                user.ToTable("Users");
                user.HasKey(x=>x.Id);
                user.Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

                user.Property(x => x.UserName).IsRequired().HasMaxLength(256)
                    .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("UserNameIndex") { IsUnique = true }));

                base.OnModelCreating(modelBuilder);
            }
        }

        public class CustomerUserStore : IUserPasswordStore<CustomerUser, int>
        {
            private readonly CustomerUserDbContext context;
            public CustomerUserStore(CustomerUserDbContext context)
            {
                this.context = context;
            }
            public void Dispose()
            {
                context.Dispose();
            }
            public Task CreateAsync(CustomerUser user)
            {
                context.Users.Add(user);
                return context.SaveChangesAsync();
            }
            public Task UpdateAsync(CustomerUser user)
            {
                context.Users.Attach(user);
                return context.SaveChangesAsync();
            }
            public Task DeleteAsync(CustomerUser user)
            {
                context.Users.Remove(user);
                return context.SaveChangesAsync();
            }
            public Task<CustomerUser> FindByIdAsync(int userId)
            {
                return context.Users.FirstOrDefaultAsync(x=>x.Id==userId);
            }
            public Task<CustomerUser> FindByNameAsync(string userName)
            {
                return context.Users.FirstOrDefaultAsync(x => x.UserName==userName);
            }
            public Task SetPasswordHashAsync(CustomerUser user, string passwordHash)
            {
                user.PasswordHash = passwordHash;
                return Task.FromResult(user);
            }
            public Task<string> GetPasswordHashAsync(CustomerUser user)
            {
                return Task.FromResult(user.PasswordHash);
            }
            public Task<bool> HasPasswordAsync(CustomerUser user)
            {
                return Task.FromResult(user.PasswordHash != null);
            }
        }
    }
}
