using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Xunit.Extensions;

namespace FluentLogging.Core.Tests {

    #region Support

    public class FakeFormatter : ILogFormatter {

        public const string Output = "formatter-output";

        public IDictionary<string, object> Items { get; private set; }

        #region ILogFormatter Members

        public string Format(IDictionary<string, object> items) {
            this.Items = items;

            return Output;
        }

        #endregion
    }

    public class FakeFacade : ILogFacade {

        public bool DebugCalled { get; set; }
        public bool InfoCalled { get; set; }
        public bool WarnCalled { get; set; }
        public bool ErrorCalled { get; set ;}
        public string Message { get; set; }
        public Exception Exception { get; set; }

        #region ILogFacade Members

        public void Debug(string message) {
            this.DebugCalled = true;
            this.Message = message;
        }

        public void Info(string message) {
            this.InfoCalled = true;
            this.Message = message;
        }

        public void Warn(string message) {
            this.WarnCalled = true;
            this.Message = message;
        }

        public void Error(string message, Exception e) {
            this.ErrorCalled = true;
            this.Message = message;
            this.Exception = e;
        }

        #endregion
    }

    public class LogEntry_Using {

        private FakeFacade _facade = new FakeFacade();
        private FakeFormatter _formatter = new FakeFormatter();

        public LogEntry_Using() {
            LogFormatter.Set(_formatter);
        }

        protected FakeFacade Facade { get { return _facade; } }
        protected FakeFormatter Formatter { get { return _formatter; } }
    }

    #endregion

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_Using_Facts {

        [Fact]
        public void null_log_facade_throws_exception() {
            Assert.Throws<ArgumentNullException>(() => {
                var entry = LogEntry.Using(null);
            });
        }

    }

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_ToString_Facts : LogEntry_Using {

        [Fact]
        public void returns_formatted_items() {

            var entry = LogEntry.Using(this.Facade);

            var expected = FakeFormatter.Output;
            var actual = entry.ToString();

            Assert.Equal(expected, actual);
        }

    }

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_WithItem_Facts : LogEntry_Using {

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData(" ")]
        public void null_empty_or_whitespace_key_throws_argument_exception(string key) {
            Assert.Throws<ArgumentException>(() => {

                var entry = LogEntry
                                .Using(this.Facade)
                                .WithItem(key, "value");

            });
        }

        [Fact]
        public void item_is_added_to_formatted_collection() {

            LogEntry
                .Using(this.Facade)
                .WithItem("key", "value")
                .AsDebug();

            Assert.NotNull(this.Formatter.Items);
            Assert.Equal(1, this.Formatter.Items.Count);
            Assert.Equal("value", this.Formatter.Items["key"]);
        }
    }

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_WithMessage_Facts : LogEntry_Using {

        [Fact]
        public void message_item_is_added_to_formatted_collection() {

            LogEntry
                .Using(this.Facade)
                .WithMessage("a message")
                .AsDebug();

            Assert.NotNull(this.Formatter.Items);
            Assert.Equal(1, this.Formatter.Items.Count);
            Assert.Equal("a message", this.Formatter.Items[LogEntry.MessageKey]);
        }

    }

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_WithException_Facts : LogEntry_Using {

        [Fact]
        public void exception_item_is_added_to_formatted_collection() {

            LogEntry
                .Using(this.Facade)
                .WithException(new Exception("an exception"))
                .AsDebug();

            Assert.NotNull(this.Formatter.Items);
            Assert.Equal(1, this.Formatter.Items.Count);
            Assert.Equal("an exception", this.Formatter.Items[LogEntry.ExceptionKey]);
        }
    }

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_AsDebug_Facts : LogEntry_Using {

        [Fact]
        public void calls_facade_debug() {

            LogEntry
                .Using(this.Facade)
                .AsDebug();

            Assert.True(this.Facade.DebugCalled);
        }

        [Fact]
        public void passes_correct_message_to_facade() {

            LogEntry
                .Using(this.Facade)
                .AsDebug();

            Assert.Equal(FakeFormatter.Output, this.Facade.Message);
        }
    }

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_AsInfo_Facts : LogEntry_Using {

        [Fact]
        public void calls_facade_info() {

            LogEntry
                .Using(this.Facade)
                .AsInfo();

            Assert.True(this.Facade.InfoCalled);
        }

        [Fact]
        public void passes_correct_message_to_facade() {

            LogEntry
                .Using(this.Facade)
                .AsInfo();

            Assert.Equal(FakeFormatter.Output, this.Facade.Message);
        }
    }

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_AsWarn_Facts : LogEntry_Using {

        [Fact]
        public void calls_facade_warn() {

            LogEntry
                .Using(this.Facade)
                .AsWarn();

            Assert.True(this.Facade.WarnCalled);
        }

        [Fact]
        public void passes_correct_message_to_facade() {

            LogEntry
                .Using(this.Facade)
                .AsWarn();

            Assert.Equal(FakeFormatter.Output, this.Facade.Message);
        }
    }

    [Trait(XunitConstants.CategoryTrait, XunitConstants.Categories.Core)]
    public class LogEntry_AsError_Facts : LogEntry_Using {

        [Fact]
        public void calls_facade_error() {

            LogEntry
                .Using(this.Facade)
                .AsError();

            Assert.True(this.Facade.ErrorCalled);
        }

        [Fact]
        public void passes_correct_message_to_facade() {

            LogEntry
                .Using(this.Facade)
                .AsError();

            Assert.Equal(FakeFormatter.Output, this.Facade.Message);
        }

        [Fact]
        public void passes_exception_to_facade() {

            var exception = new Exception("an exception");

            LogEntry
                .Using(this.Facade)
                .WithException(exception)
                .AsError();

            Assert.Equal(exception, this.Facade.Exception);
        }
    }
}
