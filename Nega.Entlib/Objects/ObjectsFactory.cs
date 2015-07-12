using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Entlib
{

    public class ObjectsFactory
    {

        private static readonly ObjectsFactory soloInstance = new ObjectsFactory();
        public static ObjectsFactory SoloInstance
        {
            get { return soloInstance; }
        }

        private List<IObjectsConfig> configs;

        private ObjectsFactory()
        {
            configs = new List<IObjectsConfig>();
        }

        public void AddConfig(IObjectsConfig config)
        {
            if (!this.configs.Contains(config))
            {
                this.configs.Add(config);
            }
        }

        public void Registrate(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException();
            }
            foreach (var config in this.configs)
            {
                config.Config(container);
            }
        }

    }

}
