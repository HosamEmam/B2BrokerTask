using B2Broker.TechnicalTask.Abstraction;
using System.Collections.Generic;
using System.Linq; 

namespace B2Broker.TechnicalTask.Anagram
{
    public class MemoryAnagramProcess : IAnagramProcess
    {
        private readonly Dictionary<string, Dictionary<string,int>> _anagrams;
        public MemoryAnagramProcess()
        {
            _anagrams = new Dictionary<string, Dictionary<string, int>>();
        }
        public void AddAnagram(string word)
        {
            string sortedWord = new string(word.OrderBy(c => c).ToArray());
            if (_anagrams.ContainsKey(sortedWord))
            {
                if (_anagrams[sortedWord].ContainsKey(word))
                {
                    _anagrams[sortedWord][word]++;
                }
                else
                {
                    _anagrams[sortedWord][word] = 1;
                }
            }
            else
            {
                _anagrams[sortedWord] = new Dictionary<string, int>() { { word, 1 } };
            }
        }

        public IEnumerable<string> GetAllAnagramsKeys()
        {
            return _anagrams.Keys;
        }

        public Dictionary<string,int> GetAnagrams(string word)
        {
            if (_anagrams.ContainsKey(word))
            {
                return _anagrams[word];
            }
            return new Dictionary<string, int>();
        }
    }
}
