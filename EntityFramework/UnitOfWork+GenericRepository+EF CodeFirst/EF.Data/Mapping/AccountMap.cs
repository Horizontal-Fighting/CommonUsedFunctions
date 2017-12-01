using EF.Core.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Data.Mapping
{
    public class AccountMap: EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            //主键
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //属性名
            Property(t => t.AccountType).IsRequired();

            //描述Account与CashAccount一对多的关系; 这里Account依赖的实体是CashAccount; 
            //一对一关系，如果左依赖右;建议写在左侧;
            this.HasOptional(x => x.CashAccount)
                .WithOptionalPrincipal(x => x.Account);

            this.HasOptional(x => x.HistoricalCommisions)
                .WithOptionalPrincipal(x=>x.Account);

            //表名
            ToTable("T_Account");
        }
    }

    public class MainAccountMap : EntityTypeConfiguration<MainAccount>
    {
        public MainAccountMap()
        {
            //主键
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //属性名
            Property(t => t.AccountType).IsRequired();           

            //描述Account与CashAccount一对多的关系; 这里Account依赖的实体是CashAccount; 
            //一对一关系，如果左依赖右;建议写在左侧;
            this.HasOptional(x => x.MarginAccount)
                .WithOptionalPrincipal(x => x.MainAccount);

            Map(x => x.MapInheritedProperties());

            //表名
            ToTable("T_MainAccount");
        }
    }

    public class PersonalAccountMap : EntityTypeConfiguration<PersonalAccount>
    {
        public PersonalAccountMap()
        {
            //主键
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //属性名
            Property(t => t.AccountType).IsRequired();

            //描述Account与CashAccount一对多的关系; 这里Account依赖的实体是CashAccount; 
            //一对一关系，如果左依赖右;建议写在左侧;
            //this.HasOptional(x => x.MarginAccount)
            //    .WithOptionalPrincipal(x => x.MainAccount);

            Map(x => x.MapInheritedProperties());

            //表名
            ToTable("T_PersonalAccount");

        }
    }
}
