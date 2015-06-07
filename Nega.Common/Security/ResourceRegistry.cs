using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public static class ResourceRegistry
    {

        private static List<Resource> resources = new List<Resource>();
        public static IEnumerable<Resource> Resources
        {
            get { return resources; }
        }

        public static void Registrate(string name, string method, string description, string module = null)
        {
            resources.Add(new Resource
            {
                Name = name,
                Method = method,
                Description = description,
                Module = module
            });
        }

    }

}
