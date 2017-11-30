using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;
using EF.Core;
using EF.Core.Data;
using System.Collections.Generic;

namespace EF.Data
{
    public class EFDbContext : DbContext
    {
       public EFDbContext()
           : base("name=DbConnectionString")
       {
            Database.SetInitializer(new EFDbInitializer());
        }

       public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
       {
           return base.Set<TEntity>();
       }
       
       
       protected override void OnModelCreating(DbModelBuilder modelBuilder)
       {
           var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
          .Where(type => !String.IsNullOrEmpty(type.Namespace))
          .Where(type => type.BaseType != null && type.BaseType.IsGenericType
               && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
           foreach (var type in typesToRegister)
           {
               dynamic configurationInstance = Activator.CreateInstance(type);
               modelBuilder.Configurations.Add(configurationInstance);
           }
           base.OnModelCreating(modelBuilder);
       }
    }



    public class EFDbInitializer : DropCreateDatabaseAlways<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            //在这里初始化数据,经测试：注释部分均为非必要代码
            CashAccount cashAccount = new CashAccount();
            var singleCurrencyCashAccounts = new List<SingleCurrencyCashAccount>();
            singleCurrencyCashAccounts.Add(new SingleCurrencyCashAccount() { Balance = 0, CurrencyType = CurrencyType.USD, CashAccount = cashAccount });
            singleCurrencyCashAccounts.Add(new SingleCurrencyCashAccount() { Balance = 0, CurrencyType = CurrencyType.HDK, CashAccount = cashAccount });
            singleCurrencyCashAccounts.Add(new SingleCurrencyCashAccount() { Balance = 0, CurrencyType = CurrencyType.RMB, CashAccount = cashAccount });
            cashAccount.SingleCurrencyCashAccounts = singleCurrencyCashAccounts;

            Account account = new Account();
            account.AccountType = AccountType.Main;
            account.CashAccount = cashAccount;
            cashAccount.Account = account;

            //以下注释部分为非必要代码
            //foreach (var tmp in singleCurrencyCashAccounts)
            //    context.Set<SingleCurrencyCashAccount>().Add(tmp);

            //context.Set<CashAccount>().Add(cashAccount);

            context.Set<Account>().Add(account);

            base.Seed(context);
        }
    }

}
