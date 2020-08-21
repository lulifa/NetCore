using Autofac;
using Autofac.Extras.DynamicProxy;
using Richard.Core.Aop;
using Richard.Core.IRepository;
using Richard.Core.Repository;
using System;
using System.IO;
using System.Reflection;

namespace Richard.Core.Setup
{
    public class AutoFacSetup: Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            string basePath = AppContext.BaseDirectory;
            string pathRes = Path.Combine(basePath, "Richard.Core.Repository.dll");
            string pathSer = Path.Combine(basePath, "Richard.Core.Service.dll");
            var dllRes = Assembly.LoadFrom(pathRes);
            var dllSer = Assembly.LoadFrom(pathSer);
            builder.RegisterType<BlogLogAop>();
            builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>)).InstancePerDependency();//注册仓储
            builder.RegisterAssemblyTypes(dllRes)
                .AsImplementedInterfaces()
                .InstancePerDependency();
            //builder.RegisterAssemblyTypes(dllSer)
            //    .AsImplementedInterfaces()
            //    .InstancePerDependency()
            //    .EnableInterfaceInterceptors();
            builder.RegisterAssemblyTypes(dllSer)
                .AsImplementedInterfaces()
                .InstancePerDependency()
                .EnableInterfaceInterceptors()
                .InterceptedBy(typeof(BlogLogAop));
        }
    }
}
