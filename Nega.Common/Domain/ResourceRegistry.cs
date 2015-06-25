using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public static class ResourceRegistry
    {

        private static Dictionary<string, RegistryEntry> registry
            = new Dictionary<string, RegistryEntry>();

        public static void Registrate(string name, int methodKey, string methodDescription, string module = null)
        {
            Registrate(name, new ResourceMethod(methodKey, methodDescription), module);
        }

        public static void Registrate(string name, ResourceMethod method, string module = null)
        {
            if (string.IsNullOrWhiteSpace(name) || method == null)
            {
                throw new ArgumentNullException();
            }

            RegistryEntry entry;
            registry.TryGetValue(name, out entry);
            if (entry == null)
            {
                entry = new RegistryEntry(name, module);
                registry.Add(name, entry);
            }
            entry.AddMethod(method);
        }

        public class RegistryEntry
        {
            public string Name { get; private set; }

            public List<ResourceMethod> Methods { get; private set; }

            public string Module { get; private set; }

            public RegistryEntry(string name, string module = null)
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException();
                }
                this.Name = name;
                this.Module = module;
            }

            public void AddMethod(int methodKey, string methodDescription)
            {
                AddMethod(new ResourceMethod(methodKey, methodDescription));
            }

            public void AddMethod(ResourceMethod method)
            {
                if (this.Methods == null)
                {
                    this.Methods = new List<ResourceMethod>();
                }
                if (!this.Methods.Contains(method))
                {
                    this.Methods.Add(method);
                }
            }

        }

    }

}
