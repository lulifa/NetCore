using Richard.Core.Entity;
using Richard.Core.IRepository;
using Richard.Core.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Richard.Core.Service
{
    public class BlogServices : BaseServices<BlogArticle>, IBlogServices
    {
        private IBaseRepository<BlogArticle> Dal;
        public BlogServices(IBaseRepository<BlogArticle> dal)
        {
            this.Dal = dal;
        }

        public async Task<List<BlogArticle>> GetList()
        {
            List<BlogArticle> list = await Dal.Query();
            return list;
        }
    }
}
