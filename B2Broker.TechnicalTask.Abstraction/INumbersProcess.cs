using System.Collections.Generic;

namespace B2Broker.TechnicalTask.Abstraction
{
    public interface INumbersProcess 
    {
        bool IsNumberExists(int number);
        void AddNumber(int number);
        IEnumerable<int> GetAllNumbersKeys();
    }
}
