using System;

namespace EF.Core
{
   public abstract class BaseEntity
    {
        public BaseEntity()
        {
            AuditModel = new AuditModel();
        }

       public AuditModel AuditModel { get; set; }
    }
}
