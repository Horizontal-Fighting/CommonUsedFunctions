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
    public class SingleCurrencyCashAccountMap : EntityTypeConfiguration<SingleCurrencyCashAccount>
    {
        public SingleCurrencyCashAccountMap()
        {
            //主键
            HasKey(t => t.Id);

            //属性名
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Balance).IsRequired();

            // 一对多关系， 外键定义在多方
            this.HasOptional<CashAccount>(x => x.CashAccount)
                .WithMany(x => x.SingleCurrencyCashAccounts);
                //.HasForeignKey(x=>x.ID);//如果一方的Id符合约定，则无需此行;

            //表名
            ToTable("T_SingleCurrencyCashAccount");
        }
    }
}
