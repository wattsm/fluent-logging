using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FluentLogging.Core;
using System.Diagnostics;

namespace FluentLogging.Demo.log4net {
    class Program {

        static void Main(string[] args) {

            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;

            Console.WriteLine("Starting up...");

            LogEntry.UsingCsonole()
                .WithMessage("Program started")
                .WithItem("Logger", "Console")
                .WithItem("ArgCount", args.Length)
                .AsDebug();

            LogEntry.UsingTrace()
                .WithMessage("Program started")
                .WithItem("Logger", "Trace")
                .WithItem("ArgCount", args.Length)
                .AsDebug();

            if(args.Contains("-throw")) {
                try {
                    Console.WriteLine("Throwing exception...");
                    throw new Exception("The throw command line switch was used");
                } catch(Exception e) {

                    LogEntry.UsingCsonole()
                        .WithMessage("An error was encountered during startup")
                        .WithItem("Logger", "Console")
                        .WithException(e)
                        .AsError();

                    LogEntry.UsingTrace()
                        .WithMessage("An error was encountered during startup")
                        .WithItem("Logger", "Trace")
                        .WithException(e)
                        .AsError();
                }
            }

            Console.WriteLine("Complete.");
            Console.ReadLine();
        }
    }
}
