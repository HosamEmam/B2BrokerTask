using B2Broker.TechnicalTask.Abstraction;
using System;
using System.IO;

namespace B2Broker.TechnicalTask.FileHandler
{
    public class TextFileWriter:IFileWriter,IDisposable
    {
        private StreamWriter _writer;
        public TextFileWriter(IFileData outputFilePath)
        {
            _writer = new StreamWriter(outputFilePath.FilePath);
        }

        public void InitializeWriting(string outputFilePath)
        {
            _writer = new StreamWriter(outputFilePath);
        }

        public void WriteLine(string line)
        {
            _writer.WriteLine(line);
        }
        public void Dispose()
        {
            _writer.Dispose();
        }

        public void WriteLine(int line)
        {
            _writer.WriteLine(line);
        }
    }
}
