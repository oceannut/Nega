﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public static class LogManager
    {

        private static ILogger defaultLogger;

        private static ILoggerFactory factory;
        public static ILoggerFactory Factory
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                factory = value;
                defaultLogger = factory.Create();
            }
        }

        static LogManager()
        {
            Factory = new ConsoleLoggerFactory();
        }

        public static ILogger GetLogger(bool? singleton = true)
        {
            if (singleton.HasValue && singleton.Value)
            {
                return defaultLogger;
            }
            else
            {
                return factory.Create();
            }
        }

    }

}
