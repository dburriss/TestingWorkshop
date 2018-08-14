using System;

namespace CheckoutCS
{
    public interface ILogger
    {
        void Error(Exception ex);
        void Information(string s);
        void Trace(string s);
    }
}