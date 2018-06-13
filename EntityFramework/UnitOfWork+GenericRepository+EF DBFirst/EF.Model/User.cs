using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Model
{
    public class User: AuditModel
    {
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserStatusID { get; set; }
        public string IdentityType { get; set; }
        public string IdentityNumber { get; set; }
        public string ContactWay { get; set; }
        public string Comment { get; set; }

    }
}
