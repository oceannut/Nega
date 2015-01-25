using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Modularity
{

    public interface IModuleLicence
    {

        string Sn { get; }

        DateTime Deadline { get; }

        string User { get; }

    }

}
