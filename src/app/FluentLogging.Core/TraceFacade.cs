using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentLogging.Core {
    public class TraceFacade : SystemFacade {

        public static readonly ILogFacade Instance = new TraceFacade();

        protected override void WriteLine(string category, string message) {
            Trace.WriteLine(message, category);
        }
    }
}
