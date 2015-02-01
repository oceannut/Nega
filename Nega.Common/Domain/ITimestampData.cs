using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface ITimestampData
    {

        DateTime Creation { get; set; }

        DateTime Modification { get; set; }

    }

}
