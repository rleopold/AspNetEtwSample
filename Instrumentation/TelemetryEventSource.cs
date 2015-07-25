using Microsoft.Diagnostics.Tracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instrumentation
{
    [EventSource(Name = "ETW-Instrumentation-Events")]
    public sealed class TelemetryEventSource : EventSource
    {
        public static readonly TelemetryEventSource Log = new TelemetryEventSource();

        [Event(1, Opcode = EventOpcode.Start, Task = Tasks.Telemetry, Message = "START telemetry: method:{0} file:{1} line:{2}")]
        public void StartTelemetry(string member, string file, string line)
        {
            WriteEvent(1, member, file, line);
        }

        [Event(2, Opcode = EventOpcode.Stop, Task = Tasks.Telemetry, Message = "STOP telemetry: method:{0} file:{1} line:{2}")]
        public void StopTelemetry(string member, string file, string line)
        {
            WriteEvent(2, member, file, line);
        }

        [Event(3)]
        public void Info(string message)
        {
            WriteEvent(3, message);
        }
    }

    public class Tasks
    {
        public const EventTask Telemetry = (EventTask)0x01;
    }
}
