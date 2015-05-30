using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public interface ILogger
    {

        void Debug(string log);

        void Info(string log);

        void Warn(string log);

        void Error(string log);

        void Fatal(string log);

        void Log(Exception ex);

    }

}
