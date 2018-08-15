using System;

namespace CheckoutCS.Tests.Unit
{
    internal class LoggerSpy : ILogger
    {
        public bool InformationWasCalled { get; private set; }
        public bool ErrorWasCalled { get; private set; }
        public bool TraceWasCalled { get; private set; }

        public void Error(Exception ex)
        {
            ErrorWasCalled = true;
        }

        public void Information(string s)
        {
            InformationWasCalled = true;
        }

        public void Trace(string s)
        {
            TraceWasCalled = true;
        }
    }
}