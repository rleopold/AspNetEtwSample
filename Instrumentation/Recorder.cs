using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Instrumentation
{
    public class Telemetry : IDisposable
    {
        private string _member;
        private string _file;
        private string _line;

        public Telemetry([CallerMemberName] string member = "",
                        [CallerFilePath] string file = "",
                        [CallerLineNumber] int line = 0)
        {
            _member = member;
            _file = file;
            _line = line.ToString();
            TelemetryEventSource.Log.StartTelemetry(_member, _file, _line);
        }

        public void Dispose()
        {
            TelemetryEventSource.Log.StopTelemetry(_member, _file, _line);
        }

        public static void Capture(Action action,
                                 [CallerMemberName]string method = "", 
                                 [CallerFilePath]string file = "", 
                                 [CallerLineNumber]int line = 0)
        {
            TelemetryEventSource.Log.StartTelemetry(method, file, line.ToString());
            action();
            TelemetryEventSource.Log.StopTelemetry(method, file, line.ToString());
        }
        public static void Info(string info)
        {
            TelemetryEventSource.Log.Info(info);
        }
    }
}
