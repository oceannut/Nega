using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nega.Common
{

    public class ConsoleLogger : ILogger
    {

        private Type type;

        public ConsoleLogger() { }

        public ConsoleLogger(Type type)
        {
            this.type = type;
        }

        public void Debug(string log)
        {
            Write(ConsoleColor.White, log);
        }

        public void Info(string log)
        {
            Write(ConsoleColor.Green, log);
        }

        public void Warn(string log)
        {
            Write(ConsoleColor.Yellow, log);
        }

        public void Error(string log)
        {
            Write(ConsoleColor.Red, log);
        }

        public void Fatal(string log)
        {
            Write(ConsoleColor.Cyan, log);
        }

        public void Log(Exception ex)
        {
            Write(ConsoleColor.Red, ex);
        }

        private void Write(ConsoleColor color, object log)
        {
            Console.ForegroundColor = color;
            Console.WriteLine();
            Console.Write(DateTimeOffset.Now);
            if (type != null)
            {
                Console.Write(string.Format(" - {0}", type.FullName));
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------------");
            Console.Write(log);
            Console.WriteLine();
        }

    }

}
