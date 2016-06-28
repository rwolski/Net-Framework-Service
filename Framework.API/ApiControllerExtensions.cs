using Autofac;
using Autofac.Integration.WebApi;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace WebApp.API
{
    public static class ApiControllerExtensions
    {
        public static ILifetimeScope BeginScope(this ApiController controller)
        {
            return controller.Configuration.DependencyResolver.BeginScope().GetRequestLifetimeScope();
        }

        public static T GetService<T>(this IDependencyScope scope)
        {
            var dep = scope.GetService(typeof(T));
            if (dep == null)
                return default(T);
            return (T)dep;
        }

        public static IEnumerable<T> GetServices<T>(this IDependencyScope scope)
        {
            var dep = scope.GetServices(typeof(T));
            if (dep == null)
                return new List<T>();
            return dep.Select(x => (T)dep);
        }
    }
}
