using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    public class RoleTypeModel : BaseEntity
    {
        public RoleType RoleType { get; set; }
    }

    public class RoleTypeModels : BaseEntity
    {
        public RoleTypeModels()
        {
            RoleTypeList = new List<RoleType>();
        }

        ICollection<RoleType> RoleTypeList { get; set; }
    }
}
