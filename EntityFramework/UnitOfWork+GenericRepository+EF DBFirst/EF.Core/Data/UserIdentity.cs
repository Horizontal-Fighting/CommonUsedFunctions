using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.Core.Data
{
    /// <summary>
    /// 用于在MVC和WEBAPI之间传输身份时使用
    /// </summary>
    public class UserIdentity : BaseEntity
    {
        public UserIdentity()
        {
            Roles = new List<RoleType>();
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public ICollection<RoleType> Roles { get; set; }
    }
}
