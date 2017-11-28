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
           HasKey(t => t.ID);
             
           //属性名
           Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
           Property(t => t.Title).IsRequired();
           Property(t => t.Author).IsRequired();
           Property(t => t.ISBN).IsRequired();
           Property(t => t.Published).IsRequired();
           
           //表名
           ToTable("Books");
       }
    }
}
