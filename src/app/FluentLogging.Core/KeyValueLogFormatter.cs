using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentLogging.Core {
    public class KeyValueLogFormatter : ILogFormatter {

        public const string Delimiter = ", ";
        public const string NullMarker = "(NULL)";

        #region IFormatter Members

        public string Format(IDictionary<string, object> items) {

            if(items == null) { throw new ArgumentNullException("items"); }

            return String.Join(
                Delimiter, 
                from item in this.GetOrderedItems(items)
                select this.FormatItem(item)
            );
        }

        #endregion

        private IEnumerable<KeyValuePair<string, object>> GetOrderedItems(IDictionary<string, object> items) {

            var first = from item in items
                        where (item.Key == LogEntry.MessageKey || item.Key == LogEntry.ExceptionKey)
                        orderby item.Key descending
                        select item;

            var second = from item in items
                         where (item.Key != LogEntry.MessageKey && item.Key != LogEntry.ExceptionKey)
                         orderby item.Key
                         select item;

            return first.Union(second);
        }

        private string FormatItem(KeyValuePair<string, object> item) {

            var value = (item.Value == null)
                            ? NullMarker
                            : item.Value;

            return String.Format("[{0} = {1}]", item.Key, value);
        }
    }
}
