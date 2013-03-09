using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentLogging.Core {
    public static class LogFormatter {

        public static ILogFormatter Default = new KeyValueLogFormatter();

        private static readonly object _lock = new object();
        private static ILogFormatter _current = LogFormatter.Default;

        public static ILogFormatter Get() {
            return _current;
        }

        public static void Set(ILogFormatter formatter) {
            lock(_lock) {
                _current = formatter;
            }
        }
    }
}
