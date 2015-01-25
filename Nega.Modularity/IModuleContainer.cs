using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Modularity
{

    public interface IModuleContainer
    {

        void Registrate(IModule module);

        void Unregistrate(IModule module);

        void Unregistrate(string name);

        bool IsModuleRegistrated(string name);

        IEnumerable<IModule> GetModules(IModuleLicence licence = null, 
            string user = null,
            string[] roles = null);

    }

}
