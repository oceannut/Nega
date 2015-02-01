using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.Unity;

namespace Nega.WcfUnity
{

    public class ObjectsRegistry
    {

        private readonly IUnityContainer container;
        public IUnityContainer Container
        {
            get { return container; }
        }

        private static readonly ObjectsRegistry soloInstance = new ObjectsRegistry();
        public static ObjectsRegistry SoloInstance
        {
            get { return soloInstance; }
        }

        private ObjectsRegistry()
        {
            container = new UnityContainer();
        }

    }

}
