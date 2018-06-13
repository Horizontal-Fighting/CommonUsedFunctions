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
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<EFDbContext, Configuration>("DbConnectionString"));
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



    public class EFDbInitializer : DropCreateDatabaseIfModelChanges<EFDbContext>
    {
        protected override void Seed(EFDbContext context)
        {
            //在这里初始化数据,经测试：注释部分均为非必要代码
            CashAccount cashAccount = new CashAccount();
            var singleCurrencyCashAccounts = new List<SingleCurrencyCashAccount>();
            singleCurrencyCashAccounts.Add(new SingleCurrencyCashAccount() { Balance = 0, CurrencyType = CurrencyType.USD, CashAccount = cashAccount });
            singleCurrencyCashAccounts.Add(new SingleCurrencyCashAccount() { Balance = 1000m, CurrencyType = CurrencyType.HKD, CashAccount = cashAccount });
            singleCurrencyCashAccounts.Add(new SingleCurrencyCashAccount() { Balance = 0, CurrencyType = CurrencyType.CNY, CashAccount = cashAccount });
            cashAccount.SingleCurrencyCashAccounts = singleCurrencyCashAccounts;

            MarginAccount marginAccount = new MarginAccount();
            marginAccount.CashBalance = 0m;
            marginAccount.TotalAssets = 0;
            marginAccount.PositionValue = 0;
            marginAccount.PositionValue = 0;
            marginAccount.CashAccount = new CashAccount();

            MainAccount mainAccount = new MainAccount();
            mainAccount.CashAccount = cashAccount;
            cashAccount.Account = mainAccount;
            marginAccount.MainAccount = mainAccount;
            mainAccount.MarginAccount = marginAccount;

            //以下注释部分为非必要代码
            //foreach (var tmp in singleCurrencyCashAccounts)
            //    context.Set<SingleCurrencyCashAccount>().Add(tmp);

            //context.Set<CashAccount>().Add(cashAccount);

            context.Set<Account>().Add(mainAccount);

            base.Seed(context);
        }
    }

}
