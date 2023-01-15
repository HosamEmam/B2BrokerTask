using B2Broker.TechnicalTask.Abstraction;
using System;
using System.Collections.Generic;

namespace B2Broker.TechnicalTask.Numbers
{
    public class MemoryNumbersProcess : INumbersProcess
    {
        private readonly HashSet<int> _numbers;
        public MemoryNumbersProcess()
        {
            _numbers = new HashSet<int>();
        }
        public void AddNumber(int number)
        {
            if (!_numbers.Contains(number))
            {
                _numbers.Add(number);
            }
        }

        public IEnumerable<int> GetAllNumbersKeys()
        {
            return _numbers;
        }

        public bool IsNumberExists(int number)
        {
            return _numbers.Contains(number);
        }
    }
}
