using System;

namespace Counter.Domain.Helpers
{
    public interface IXmlSerializer
    {
        string Serialize<T>(T data, Type type);
    }
}
