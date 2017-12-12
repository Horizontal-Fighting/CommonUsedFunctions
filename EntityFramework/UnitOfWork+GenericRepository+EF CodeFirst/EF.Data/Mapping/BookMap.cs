using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using EF.Core.Data;

namespace EF.Data.Mapping
{
   public class  BookMap : EntityTypeConfiguration<Book>
   {
       public BookMap()
       {
           //主键
           HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //属性名
           Property(t => t.Title).IsRequired();
           Property(t => t.Author).IsRequired();
           Property(t => t.ISBN).IsRequired();
           Property(t => t.Published).IsRequired();
           Property(t => t.CurrencyType).IsRequired();
           //表名
           ToTable("Books");
       }
    }
}
