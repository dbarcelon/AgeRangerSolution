using PostSharp.Aspects;
using System;

namespace AgeRanger.Utils
{
    [Serializable]
    public class HandleException : OnExceptionAspect
    {
        private string ApplicationName { get; set; }

        public HandleException(string applicationName)
        {
            this.ApplicationName = applicationName;
        }

        public override void OnException(MethodExecutionArgs args)
        {
            if (!System.Diagnostics.EventLog.SourceExists(".NET Runtime"))
            {
                System.Diagnostics.EventLog.CreateEventSource(".NET Runtime", "Application");
            }

            System.Diagnostics.EventLog log = new System.Diagnostics.EventLog() { Source = ".NET Runtime" };
            log.WriteEntry(String.Format("PMCS {0}: Method --> {1}, Message--> {2}, Stack Trace: {3}", this.ApplicationName, args.Method.Name, args.Exception.Message, args.Exception.StackTrace), System.Diagnostics.EventLogEntryType.Error);

            args.FlowBehavior = FlowBehavior.Default;
        }
    }
}
