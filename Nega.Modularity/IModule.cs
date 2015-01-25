using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Modularity
{

    public interface IModule : IDisposable
    {

        string Name { get; }

        IModuleLicence Licence { get; }

        void Initialize();

        void Destroy();

    }

}
