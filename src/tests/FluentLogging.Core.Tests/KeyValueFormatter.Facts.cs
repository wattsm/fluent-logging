using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FluentLogging.Core.Tests {

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Formatters)]
    public class KeyValueFormatter_Facts {

        #region Test Setup / Helpers

        private ILogFormatter Create() {
            return new KeyValueLogFormatter();
        }

        private void AssertCorrectness(IDictionary<string, object> items, string expected) {

            var formatter = this.Create();
            var actual = formatter.Format(items);

            Assert.Equal(expected, actual);
        }

        #endregion

        [Fact]
        public void null_items_throws_argument_null_exception() {

            var formatter = this.Create();

            Assert.Throws<ArgumentNullException>(() => {
                var result = formatter.Format(null);
            });
        }

        [Fact]
        public void items_appear_in_alphabetical_order() {
            this.AssertCorrectness(
                new Dictionary<string, object>() { { "a-item", "value" }, { "b-item", "value" } },
                "[a-item = value], [b-item = value]"
            );
        }

        [Fact]
        public void null_marker_used_for_items_with_null_value() {
            this.AssertCorrectness(
                new Dictionary<string, object>() { { "is-null", null } },
                "[is-null = (NULL)]"
            );
        }

        [Fact]
        public void message_item_appears_first_if_present() {
            this.AssertCorrectness(
                new Dictionary<string, object>() { { "a-item", "value" }, { LogEntry.MessageKey, "message" } },
                String.Format("[{0} = message], [a-item = value]", LogEntry.MessageKey)
            );
        }

        [Fact]
        public void exception_item_appears_first_if_present() {
            this.AssertCorrectness(
                new Dictionary<string, object>() { { "a-item", "value" }, { LogEntry.ExceptionKey, "exception" } },
                String.Format("[{0} = exception], [a-item = value]", LogEntry.ExceptionKey)
            );
        }

        [Fact]
        public void message_appears_before_exception_if_both_present() {
            this.AssertCorrectness(
                new Dictionary<string, object>() { { "a-item", "value" }, { LogEntry.ExceptionKey, "exception" }, { LogEntry.MessageKey, "message" } },
                String.Format("[{0} = message], [{1} = exception], [a-item = value]", LogEntry.MessageKey, LogEntry.ExceptionKey)
            );
        }

    }
}
