using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentLogging.Core {
    public interface ILogFormatter {

        string Format(IDictionary<string, object> items);

    }
}
