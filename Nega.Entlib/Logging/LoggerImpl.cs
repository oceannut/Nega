using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using Nega.Common;

namespace Nega.Entlib
{
    public class LoggerImpl : ILogger
    {

        private LogWriter logWriter;
        private string category;

        public LoggerImpl(LogWriter logWriter)
        {
            this.logWriter = logWriter;
        }

        public LoggerImpl(LogWriter logWriter,
            string category)
            : this(logWriter)
        {
            this.category = category;
        }

        public void Debug(string log)
        {
            LogEntry logEntry = new LogEntry
            {
                Message = log,
                Severity= TraceEventType.Verbose
            };
            Write(logEntry);
        }

        public void Info(string log)
        {
            LogEntry logEntry = new LogEntry
            {
                Message = log,
                Severity = TraceEventType.Information
            };
            Write(logEntry);
        }

        public void Warn(string log)
        {
            LogEntry logEntry = new LogEntry
            {
                Message = log,
                Severity = TraceEventType.Warning
            };
            Write(logEntry);
        }

        public void Error(string log)
        {
            LogEntry logEntry = new LogEntry
            {
                Message = log,
                Severity = TraceEventType.Error
            };
            Write(logEntry);
        }

        public void Fatal(string log)
        {
            LogEntry logEntry = new LogEntry
            {
                Message = log,
                Severity = TraceEventType.Critical
            };
            Write(logEntry);
        }

        public void Log(Exception ex)
        {
            LogEntry logEntry = new LogEntry
            {
                Message = ex.ToString(),
                Severity = TraceEventType.Error
            };
            Write(logEntry);
        }

        private void Write(LogEntry logEntry)
        {
            if (!string.IsNullOrWhiteSpace(this.category)) 
            {
                logEntry.Categories.Add(category);
            }
            logWriter.Write(logEntry);
        }

    }

}
