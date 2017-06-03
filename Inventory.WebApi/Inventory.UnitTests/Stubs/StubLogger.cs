using System;
using Microsoft.Extensions.Logging;

namespace Inventory.UnitTests.Stubs
{
    public class StubLogger<T> : ILogger<T>
    {
        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return null;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {            
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
