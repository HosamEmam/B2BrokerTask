using B2Broker.TechnicalTask.Abstraction;
using B2Broker.TechnicalTask.Anagram;
using B2Broker.TechnicalTask.FileHandler;
using B2Broker.TechnicalTask.Numbers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace B2Broker.TechnicalTask.ConsoleApp
{

    class Program
    {
        const string inputFilePath = "input.txt";
        const string outputFilePath = "output";
       
        static void Main(string[] args)
        {
            ServiceProvider serviceProvider = SetupDI();

            IAnagramProcess _anagram = serviceProvider.GetService<IAnagramProcess>();
            INumbersProcess _numbers = serviceProvider.GetService<INumbersProcess>();
            IFileData _fileData = serviceProvider.GetService<IFileData>();

            Console.WriteLine("Process Started ----------------- ");

            try
            {
                _fileData.FilePath = inputFilePath;
                using (var inputFile = serviceProvider.GetService<IFileReader>())
                {
                    while (!inputFile.IsEndOfFile())
                    {
                        string[] words = inputFile.ReadLine().Split(' ');
                        foreach (var word in words)
                        {
                            int num = 0;
                            if (int.TryParse(word, out num))
                            {
                                _numbers.AddNumber(num);
                            }
                            else
                            {
                                _anagram.AddAnagram(word.ToLower());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("We can not continue, please try again after fixing the error");
                return;
            }

            try
            {
                _fileData.FilePath = $"{outputFilePath} anagram.txt";
                OutputTheAnagrams(serviceProvider, _anagram);

                _fileData.FilePath = $"{outputFilePath} numbers.txt";
                OutputTheNumbers(serviceProvider, _numbers);

                Console.WriteLine("The output file is ready, Check the 'output.txt'");
                Console.WriteLine("The followings are the numbers in the file'");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("We can not continue, please try again after fixing the error");
                return;
            }

            Console.WriteLine("Process Ended ----------------- ");

            Console.WriteLine("Enter any key to start and Enter 'Q' to quit");
            while (Console.ReadLine() != "Q")
            {
                CheckAnagramsForUser(_anagram);
                CheckFactorialsForUser(_numbers);
                Console.WriteLine("=============================");
                Console.WriteLine("Enter any key to start again and Enter 'Q' to quit");
            }

            Console.WriteLine("---------End ----------------- ");
            Console.ReadLine();
        }

        private static ServiceProvider SetupDI()
        {
            return new ServiceCollection()
                        .AddSingleton<IAnagramProcess, MemoryAnagramProcess>()
                        .AddSingleton<INumbersProcess, MemoryNumbersProcess>()
                        .AddScoped<IFileData,FileData>()
                        .AddTransient<IFileReader,TextFileReader>()
                        .AddTransient<IFileWriter,TextFileWriter>()
                        .BuildServiceProvider();
        }

        private static void CheckAnagramsForUser(IAnagramProcess _anagram)
        {
            Console.WriteLine("Enter a word to get you its anagrams in the file");
            var userWord = Console.ReadLine().ToLower();

            var anagramList = _anagram.GetAnagrams(new string(userWord.OrderBy(c=>c).ToArray()));

            Console.WriteLine($"The number of anagrams in the file is {anagramList.Count}");

            if (anagramList.Count > 0)
            {
                Console.WriteLine($"The anagrams are");
                foreach (var item in anagramList)
                {
                    Console.WriteLine($"{item.Key}:{item.Value}");
                }
            }
        }
        private static void CheckFactorialsForUser(INumbersProcess _numbers)
        {
            Console.WriteLine("Enter a number to check if there is its factorial in the file");
            
            int userNumber = 0;
            
            while (!int.TryParse(Console.ReadLine(),out userNumber))
            {
                Console.WriteLine("Enter a vaild number please");
            }

            int userFactorial = CalculateFactorial(userNumber);

            bool isExists = _numbers.IsNumberExists(userFactorial);

            Console.WriteLine($"Your number is {userNumber} and its factorial is {userFactorial} ");

            if (isExists)
            {
                Console.WriteLine($"And (YES) it EXISTS in the file");
            }
            else
            {
                Console.WriteLine($"And (NO) it DOES NOT EXISTS in the file");
            }
        }

        private static int CalculateFactorial(int userNumber)
        {
            if (userNumber < 1) return 0;
            if (userNumber == 1) return 1;
            if (userNumber == 2) return 2;
            return userNumber * CalculateFactorial(userNumber - 1);
        }

        private static void OutputTheNumbers(ServiceProvider serviceProvider, INumbersProcess _numbers)
        {
            var numbers = new string(string.Join(" , ", _numbers.GetAllNumbersKeys().OrderBy(n => n).ToList()));
            using (var outputFile = serviceProvider.GetService<IFileWriter>())
            {
                outputFile.WriteLine(numbers);
            }
            Console.WriteLine(numbers);
        }

        private static void OutputTheAnagrams(ServiceProvider serviceProvider, IAnagramProcess _anagram)
        {
            var anagramsKeys = _anagram.GetAllAnagramsKeys().ToList();
            
            using (var outputFile = serviceProvider.GetService<IFileWriter>())
            {
                foreach (var key in anagramsKeys)
                {
                    var anagramList = _anagram.GetAnagrams(key);
                   
                    var line = string.Join(" - ", anagramList.Select(kvp => new string ($"{kvp.Key}:{kvp.Value}")));
                        
                    outputFile.WriteLine(line);
                    
                }
            }
        }

    }
}
