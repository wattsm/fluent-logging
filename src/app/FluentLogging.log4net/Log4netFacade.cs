using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using FluentLogging.Core;

namespace FluentLogging.log4net {
    public class Log4netFacade : ILogFacade {

        private readonly ILog _log;

        public Log4netFacade(ILog log) {

            if(log == null) { throw new ArgumentNullException("log"); }

            _log = log;
        }

        #region ILogFacade Members

        public void Debug(string message) {
            if(_log.IsDebugEnabled) {
                _log.Debug(message);
            }
        }

        public void Info(string message) {
            if(_log.IsInfoEnabled) {
                _log.Info(message);
            }
        }

        public void Warn(string message) {
            if(_log.IsWarnEnabled) {
                _log.Warn(message);
            }
        }

        public void Error(string message, Exception e) {
            if(_log.IsErrorEnabled) {
                _log.Error(message, e);
            }
        }

        #endregion
    }
}
