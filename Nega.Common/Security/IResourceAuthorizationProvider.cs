﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface IResourceAuthorizationProvider
    {

        IEnumerable<ResourceAccess> ListResourceAccess(string name, string method);

    }

}
