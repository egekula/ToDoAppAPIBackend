using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;
using ToDoApp.Core.Utilities.Interceptors;
using ToDoApp.Core.Utilities.IoC;
using ToDoApp.Business.Constants;
using Microsoft.Extensions.DependencyInjection;
using ToDoApp.Core.Extensions;

namespace ToDoApp.Business.BusinessAspect.Autofac
{
    public class SecuredOperation : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;

        public SecuredOperation(string roles)
        {
            _roles = roles.Split(',');
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
            foreach (var role in _roles)
            {
                if (roleClaims.Contains(role))
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);
        }
    }
}
