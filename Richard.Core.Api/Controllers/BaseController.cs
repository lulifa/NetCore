using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richard.Core.Model;

namespace Richard.Core.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected ApiResult JsonCore(bool success)
        {
            return new ApiResult(success);
        }

        protected ApiResult JsonCore(bool success, object data)
        {
            return new ApiResult(success, data);
        }

        protected ApiResult JsonCore(bool success, string msg)
        {
            return new ApiResult(success, null, msg);
        }

        protected ApiResult JsonCore(bool success, object data, string msg)
        {
            return new ApiResult(success, data, msg);
        }
    }
}
