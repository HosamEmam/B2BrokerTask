using System.Collections.Generic;

namespace B2Broker.TechnicalTask.Abstraction
{
    public interface IAnagramProcess
    {
        Dictionary<string, int> GetAnagrams(string word);
        IEnumerable<string> GetAllAnagramsKeys();
        void AddAnagram(string word);
    }
}
