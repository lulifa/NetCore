using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richard.Core.Authorization;
using Richard.Core.Model;

namespace Richard.Core.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class LoginController : BaseController
    {
        public LoginController()
        {

        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetJwtStr(string name,string password)
        {
            string[] userIds = new string[] { "lulifa" };
            List<string> ids = userIds.ToList();
            if (!ids.Contains(name))
            {
                return JsonCore(false,"用户名和密码不正确");
            }
            TokenModelJwt tokenModelJwt = new TokenModelJwt();
            tokenModelJwt.UserId = name;
            tokenModelJwt.Role = "Admin,User";
            string jwtStr = JwtHelper.IssueJwt(tokenModelJwt);
            return JsonCore(true,new { jwtStr });
        }
    }
}
