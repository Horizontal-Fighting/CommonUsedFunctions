using System;

namespace EF.Core
{
   public abstract class BaseEntity
    {
        public BaseEntity()
        {
            AuditModel = new AuditModel();
        }
        public Int64 Id { get; set; }

       public AuditModel AuditModel { get; set; }
    }
}
