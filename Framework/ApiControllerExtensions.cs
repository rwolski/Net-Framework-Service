using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Framework
{
    public static class ApiControllerExtensions
    {
        public static T GetService<T>(this ApiController controller)
        {
            var dep = controller.Configuration.DependencyResolver.GetService(typeof(T));
            if (dep == null)
                return default(T);
            return (T)dep;
        }

        public static IEnumerable<T> GetServices<T>(this ApiController controller)
        {
            var dep = controller.Configuration.DependencyResolver.GetServices(typeof(T));
            if (dep == null)
                return new List<T>();
            return dep.Select(x => (T)dep);
        }
    }
}
