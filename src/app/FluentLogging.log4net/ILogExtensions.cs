using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using FluentLogging.Core;

namespace FluentLogging.log4net {
    public static class ILogExtensions {

        public static LogEntry NewEntry(this ILog log) {
            return LogEntry.Using(
                new Log4netFacade(log)
            );
        }

    }
}
