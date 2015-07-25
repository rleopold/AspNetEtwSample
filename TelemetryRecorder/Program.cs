using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reactive.Linq;

namespace TelemetryRecorder
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = new TraceEventSession("TelemetrySession"))
            {
                session.EnableProvider("ETW-Insturmentation-Events");
                session.Source.Dynamic.AddCallbackForProviderEvent("ETW-Insturmentation-Events", "StartTelemetry/Start", (data) =>
                    {
                        Console.WriteLine(data.ToString());
                    });

                session.Source.Dynamic.AddCallbackForProviderEvent("ETW-Insturmentation-Events", "StartTelemetry/Stop", (data) =>
                {
                    Console.WriteLine(data.ToString());
                });

                session.Source.Dynamic.AddCallbackForProviderEvent("ETW-Insturmentation-Events", "Info", (data) =>
                {
                    Console.WriteLine(data.ToString());
                });

                session.Source.Process();
            }
        }
    }
}
