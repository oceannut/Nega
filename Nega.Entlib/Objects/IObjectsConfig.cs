using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

namespace Nega.Entlib
{

    public interface IObjectsConfig
    {

        void Config(IUnityContainer container);

    }

}
