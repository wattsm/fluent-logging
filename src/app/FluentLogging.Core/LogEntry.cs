using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentLogging.Core {
    public class LogEntry {

        public const string MessageKey = "Message";
        public const string ExceptionKey = "Exception";

        private readonly ILogFacade _log;
        private readonly IDictionary<string, object> _items = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        private ILogFormatter _formatter = LogFormatter.Get();
        private Exception _exception = null;

        public LogEntry(ILogFacade log) {
            _log = log;
        }

        public override string ToString() {
            return _formatter.Format(_items);
        }

        #region Methods (Fluent)

        public static LogEntry Using(ILogFacade log) {

            if(log == null) { throw new ArgumentNullException("log"); }

            return new LogEntry(log);
        }

        public LogEntry WithItem(string key, object value) {

            if(String.IsNullOrWhiteSpace(key)) { throw new ArgumentException("key"); }

            _items[key] = value;

            return this;
        }

        public LogEntry WithMessage(string message) {
            return this.WithItem(
                MessageKey, 
                message
            );
        }

        public LogEntry WithMessage(string message, params object[] args) {
            return this.WithMessage(
                String.Format(message, args)
            );
        }

        public LogEntry WithException(Exception e) {

            _exception = e;

            return this.WithItem(
                ExceptionKey,
                e.Message
            );
        }

        #endregion

        #region Methods (Writing)

        public void AsDebug() {
            _log.Debug(this.ToString());
        }

        public void AsInfo() {
            _log.Info(this.ToString());
        }

        public void AsWarn() {
            _log.Warn(this.ToString());
        }

        public void AsError() {
            _log.Error(
                this.ToString(), 
                _exception
            );
        }

        #endregion
    }
}
