using Autofac;
using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using ToDoApp.Business.Abstract;
using ToDoApp.Business.Concrate;
using ToDoApp.Business.Repositories;
using ToDoApp.Core.Utilities.Interceptors;
using ToDoApp.Core.Utilities.Security.JWT;
using ToDoApp.DataAccess.Abstract;
using ToDoApp.DataAccess.Concrate;

namespace ToDoApp.Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();

            builder.RegisterType<UserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<ToDoItemDal>().As<IToDoItemDal>().SingleInstance();
            builder.RegisterType<ToDoItemManager>().As<IToDoItemService>().SingleInstance();
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();

        }

    }
}
