using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentLogging.Core {
    public class ConsoleFacade : SystemFacade {

        public static readonly ILogFacade Instance = new ConsoleFacade();

        protected override void WriteLine(string category, string message) {
            Console.WriteLine("{0}> {1}", category, message);
        }
    }
}
