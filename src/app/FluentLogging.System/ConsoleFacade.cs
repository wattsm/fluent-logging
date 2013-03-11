using FluentLogging.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentLogging.System {
    public class ConsoleFacade : SystemFacade {

        protected override void WriteLine(string category, string message) {
            Console.WriteLine("{0}> {1}", category, message);
        }
    }
}
