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
    public class MarginAccountMap:EntityTypeConfiguration<MarginAccount>
    {
        public MarginAccountMap()
        {
            //主键
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //属性名

            //表名
            ToTable("T_MarginAccount");
        }
    }
}
