using FluentLogging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentLogging.System {
    public abstract class SystemFacade : ILogFacade {

        private const string DebugCategory = "Debug";
        private const string WarnCategory = "Warn";
        private const string InfoCategory = "Info";
        private const string ErrorCategory = "Error";

        protected abstract void WriteLine(string category, string message);

        public void Debug(string message) {
            this.WriteLine(DebugCategory, message);
        }

        public void Info(string message) {
            this.WriteLine(InfoCategory, message);
        }

        public void Warn(string message) {
            this.WriteLine(WarnCategory, message);
        }

        public void Error(string message, Exception e) {
            this.WriteLine(ErrorCategory, message);

            if(e != null) {
                this.WriteLine(ErrorCategory, e.Message);
            }
        }
    }
}
