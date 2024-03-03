using BenchmarkDotNet;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Module13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 13.6.1
            ListsToTextReader myReader = new(path: "C:\\Users\\Eddy\\Desktop\\Text1.txt");

            if (myReader.IsPathOK)
            {
                long listSumElapsedTime = 0;
                long linkedListSumElapsedTime = 0;
                
                Console.WriteLine($"Сначала измеряем время вставки для обычного List<>\n");

                
                var timer = new Stopwatch();
                timer.Start();
                
                for (int i = 0; i < 100; i++)
                {
                    timer.Restart();
                    myReader.TextToListReader();
                    timer.Stop();
                    listSumElapsedTime += timer.ElapsedMilliseconds;
                }
                Console.WriteLine($"Суммарное время за 100 проходов для List<>: {listSumElapsedTime}\n");
                
                Console.WriteLine($"Теперь для LinkedList<>\n");

                for (int i = 0; i < 100; i++)
                {
                    timer.Restart();
                    myReader.TextToLinkedListReader();
                    timer.Stop();
                    linkedListSumElapsedTime += timer.ElapsedMilliseconds;
                }
                Console.WriteLine($"Суммарное время за 100 проходов для LinkedList<>: {linkedListSumElapsedTime}\n");

                // 13.6.2

                Console.WriteLine("Теперь посчитаем топ-10 слов в этом вашем Обломове\n");

                string originalText = string.Empty;
                StringBuilder stringBuilder = new();

                // создаём единую строку из списка считанных строк
                foreach (var str in myReader.TestList)
                {
                    stringBuilder.AppendLine(str);
                }
                originalText = stringBuilder.ToString();

                // убираем пунктуацию и разбиваем на массив слов
                var noPunctuationText = new string(originalText.Where(c => !char.IsPunctuation(c)).ToArray());
                string[] words = noPunctuationText.Split(" ");


                Dictionary<string, int> wordsDict = []; // словарь "слово-количество"
                SortedDictionary<int, string> sortedWordsDict = new(Comparer<int>.Create((x, y) => y.CompareTo(x))); //сортированный по убыванию словарь количество-слово
                List<string> topTenWords = []; // список для топ-10 слов


                foreach (var w in words)
                { 
                    var word = w.ToLower().Trim(); // к единому регистру и убираем пустые символы в начале и конце
                    if(word.Length > 3) // чтоб убрать местоимения, союзы, тд. в продакшне нужно естественно проверять не кол-вом символов, но мы не в проде
                    {
                        wordsDict.TryGetValue(word, out var currentCount);
                        wordsDict[word] = currentCount + 1;
                    }                   
                }

                // добавляем только ключи, которых еще нет, равные значения опускаем (в условиях не сказано, нужны они или нет)
                foreach (KeyValuePair<string, int> p in wordsDict)
                {
                    if (!sortedWordsDict.ContainsKey(p.Value)) 
                    {
                        sortedWordsDict.Add(p.Value, p.Key);
                    }
                }

                // сохраняем строки с инфой в список заданной длинны
                foreach (KeyValuePair<int, string> p in sortedWordsDict)
                {
                    if (topTenWords.Count == 10)
                        break;
                    topTenWords.Add($"{p.Value}: {p.Key} раз");
                }

                foreach(var str in topTenWords)
                {
                    Console.WriteLine(str);
                }


                    
                    


            }
        }

        


    }
}
