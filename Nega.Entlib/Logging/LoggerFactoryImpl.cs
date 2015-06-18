using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using Nega.Common;

namespace Nega.Entlib
{
    public class LoggerFactoryImpl : ILoggerFactory
    {

        private readonly LoggerImpl _default;

        public LoggerFactoryImpl()
        {
            this._default = new LoggerImpl(new LogWriterFactory().Create());
        }

        public LoggerFactoryImpl(string category)
            : this()
        {
            this._default = new LoggerImpl(new LogWriterFactory().Create(), category);
        }

        public ILogger Create()
        {
            return this._default;
        }

    }
}
