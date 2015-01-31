﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Modularity
{

    public class BasicModule : IModule
    {

        protected bool initialized;
        public bool Initialized
        {
            get { return initialized; }
        }

        protected string name;
        public string Name
        {
            get { return name; }
        }

        public IModuleLicence Licence
        {
            get { return Unlimited.Solo; }
        }

        public BasicModule() { }

        public BasicModule(string name)
        {
            this.name = name;
        }

        public void Initialize()
        {
            this.initialized = true;
        }

        public void Destroy()
        {
            this.initialized = false;
        }

        public void Dispose()
        {
            Destroy();
        }

    }
}