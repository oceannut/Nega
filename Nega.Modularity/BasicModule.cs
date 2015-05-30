using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Modularity
{

    public abstract class BasicModule : IModule
    {

        private readonly object lockObj = new object();

        private bool isInitialized;
        public bool IsInitialized
        {
            get { return isInitialized; }
        }

        public abstract string Name { get; }

        public virtual IModuleLicence Licence
        {
            get { return Unlimited.Solo; }
        }

        public BasicModule() { }

        public void Initialize()
        {
            if (!this.isInitialized)
            {
                lock (lockObj)
                {
                    if (!this.isInitialized)
                    {
                        OnInitialize();
                        this.isInitialized = true;
                    }
                }
            }
        }

        public void Destroy()
        {
            if (this.isInitialized)
            {
                lock (lockObj)
                {
                    if (this.isInitialized)
                    {
                        this.isInitialized = false;
                        OnDestroy();
                    }
                }
            }
        }

        public void Dispose()
        {
            Destroy();
        }

        protected abstract void OnInitialize();

        protected abstract void OnDestroy();

    }
}
