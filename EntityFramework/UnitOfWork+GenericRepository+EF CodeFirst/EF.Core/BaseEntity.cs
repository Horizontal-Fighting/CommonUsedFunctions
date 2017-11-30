using System;

namespace EF.Core
{
   public abstract class BaseEntity
    {
        public Int64 Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string UpdatedBy { get; set; }
    }
}
