using Autofac;
using ToDoApp.Business.Abstract;
using ToDoApp.Business.Concrate;
using ToDoApp.Business.Repositories;
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
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();


        }

    }
}
