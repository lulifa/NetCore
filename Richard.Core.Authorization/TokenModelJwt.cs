using Richard.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Richard.Core.Authorization
{
    /// <summary>
    /// 令牌
    /// </summary>
    public class TokenModelJwt
    {
        public string UserId { get; set; }
        public string Role { get; set; }

        public List<string> RoleItems
        {
            get
            {
                List<string> roles = new List<string>();
                if (!string.IsNullOrEmpty(Role))
                {
                    roles = this.Role.Split(',').ToList();
                }
                return roles;
            }
        }
    }
}
