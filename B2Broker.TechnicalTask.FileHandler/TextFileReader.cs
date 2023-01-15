using B2Broker.TechnicalTask.Abstraction;
using System;
using System.IO;

namespace B2Broker.TechnicalTask.FileHandler
{
    public class TextFileReader : IFileReader
    {
        private StreamReader _reader;
        
        public TextFileReader(IFileData inputFile)
        {
            _reader = new StreamReader(inputFile.FilePath);
        }

        public bool IsEndOfFile()
        {
            return _reader.EndOfStream;
        }

        public string ReadLine()
        {
            return _reader.ReadLine();
        }

        public void InitializeReading(string inputFilePath)
        {
            _reader = new StreamReader(inputFilePath);
        }
        public void Dispose()
        {
            _reader?.Dispose();
        }
    }
}
