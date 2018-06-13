using System;

namespace EF.Model
{
   public class Book : AuditModel
    {
       public string Title { get; set; }
       public string Author { get; set; }
       public string ISBN { get; set; }
       public DateTime Published { get; set; }
       public decimal Price { get; set; }
       public string Remark { get; set; }
    }
}
