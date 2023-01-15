using System;

namespace B2Broker.TechnicalTask.Abstraction
{
    public interface IFileReader : IDisposable
    {
        void InitializeReading(string inputFilePath);
        bool IsEndOfFile();
        string ReadLine();
    }
}
