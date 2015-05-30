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

        private readonly LogWriterFactory logWriterFactory;
        private readonly string category;

        public LoggerFactoryImpl()
        {
            this.logWriterFactory = new LogWriterFactory();
        }

        public LoggerFactoryImpl(string category)
            : this()
        {
            this.category = category;
        }

        public ILogger Create(Type type)
        {
            return new LoggerImpl(this.logWriterFactory.Create(), this.category);
        }

    }
}
