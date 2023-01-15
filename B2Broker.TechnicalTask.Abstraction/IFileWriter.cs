using System;

namespace B2Broker.TechnicalTask.Abstraction
{
    public interface IFileWriter : IDisposable
    {
        void InitializeWriting(string inputFilePath);
        void WriteLine(string line);
        void WriteLine(int line);

    }
}
