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
    public class CommisionsMap : EntityTypeConfiguration<Commisions>
    {

        public CommisionsMap()
        {
            //主键
            HasKey(t => t.Id);

            //属性名
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //表名
            ToTable("T_Commisions");
        }
    }

    public class CommisionMap : EntityTypeConfiguration<Commision>
    {

        public CommisionMap()
        {
            //主键
            HasKey(t => t.Id);

            //属性名
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.Amount).IsRequired();
            Property(t => t.CurrencyType).IsRequired();
            Property(t => t.StartDate).IsRequired();
            Property(t => t.EndDate).IsRequired();

            //可以忽略的属性
            this.Ignore(x => x.Year);
            

            // 一对多关系， 外键定义在多方
            this.HasOptional<Commisions>(x => x.Commisions)
                .WithMany(x => x.CommisionList);
            //.HasForeignKey(x=>x.ID);//如果一方的Id符合约定，则无需此行;

            //表名
            ToTable("T_Commision");
        }
    }
}
