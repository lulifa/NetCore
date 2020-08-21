using Richard.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Richard.Core.IService
{
    public interface IBlogServices:IBaseServices<BlogArticle>
    {
        Task<List<BlogArticle>> GetList();
    }
}
