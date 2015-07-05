using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.WcfCommon
{

    public interface IClientFinder
    {

        Client OperationClient { get; }

    }

}
