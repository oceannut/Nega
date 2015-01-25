using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Modularity
{
    public class SimpleModuleContainer : IModuleContainer
    {

        private readonly Dictionary<string, IModule> dict;

        public SimpleModuleContainer()
        {
            dict = new Dictionary<string, IModule>();
        }

        public void Registrate(IModule module)
        {
            dict.Add(module.Name, module);
        }

        public void Unregistrate(IModule module)
        {
            throw new NotImplementedException();
        }

        public void Unregistrate(string name)
        {
            throw new NotImplementedException();
        }

        public bool IsModuleRegistrated(string name)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IModule> GetModules(IModuleLicence licence = null, 
            string user = null, 
            string[] roles = null)
        {
            return dict.Values;
        }
    }
}
