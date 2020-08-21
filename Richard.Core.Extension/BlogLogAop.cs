using Castle.DynamicProxy;
using Newtonsoft.Json;
using StackExchange.Profiling;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Richard.Core.Aop
{
    /// <summary>
    /// 拦截器BlogLogAOP 继承IInterceptor接口
    /// </summary>
    public class BlogLogAop : IInterceptor
    {
        public BlogLogAop()
        {

        }


        /// <summary>
        /// 实例化IInterceptor唯一方法 
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {
            try
            {
                // 就是这里！！
                MiniProfiler.Current.Step($"执行Service方法：{invocation.Method.Name}() -> ");

                invocation.Proceed();
            }
            catch (Exception e)
            {
                //执行的 service 中，捕获异常
                //dataIntercept += ($"方法执行中出现异常：{e.Message + e.InnerException}");
            }
        }
    }
}
