using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Richard.Core.Entity;
using Richard.Core.IService;
using Richard.Core.Model;
using StackExchange.Profiling;

namespace Richard.Core.Api.Controllers
{
    /// <summary>
    /// 博客列表
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseController
    {
        private readonly IBlogServices BlogServices;
        private readonly ICacheService CacheService;
        public BlogController(IBlogServices blogServices,ICacheService cacheService)
        {
            BlogServices = blogServices;
            CacheService = cacheService;
        }

        /// <summary>
        /// 获取博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiResult> GetList()
        {
            CacheService.GetValue("lulifa");
            using (MiniProfiler.Current.Step("立法试试"))
            {
                List<BlogArticle> blogArticles = await BlogServices.GetList();
                return JsonCore(true, blogArticles);
            }

        }

        [HttpPost]
        public async Task<ApiResult> GetDetail()
        {
            using (MiniProfiler.Current.Step("Post立法试试"))
            {
                List<BlogArticle> blogArticles = await BlogServices.GetList();
                return JsonCore(true, blogArticles);
            }
        }
    }
}
