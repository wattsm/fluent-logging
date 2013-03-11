using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using log4net.Config;
using FluentLogging.log4net;
using System.IO;

namespace FluentLogging.Demo.log4net {
    class Program {

        private static readonly ILog _log = LogManager.GetLogger(typeof(Program));

        static Program() {
            XmlConfigurator.Configure(
                new FileInfo(
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")
                )
            );
        }

        static void Main(string[] args) {

            Console.WriteLine("Starting up...");

            _log
                .NewEntry()
                .WithMessage("Program started")
                .WithItem("ArgCount", args.Length)
                .AsDebug();

            if(args.Contains("-throw")) {
                try {
                    Console.WriteLine("Throwing exception...");
                    throw new Exception("The throw command line switch was used");
                } catch(Exception e) {
                    _log
                        .NewEntry()
                        .WithMessage("An error was encountered during startup")
                        .WithException(e)
                        .AsError();
                }
            }

            Console.WriteLine("Complete.");
            Console.ReadLine();
        }
    }
}
