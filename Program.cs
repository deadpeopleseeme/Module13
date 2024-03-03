using BenchmarkDotNet;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

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

                timer.Start();

                for (int i = 0; i < 100; i++)
                {
                    timer.Restart();
                    myReader.TextToLinkedListReader();
                    timer.Stop();
                    linkedListSumElapsedTime += timer.ElapsedMilliseconds;
                }
                Console.WriteLine($"Суммарное время за 100 проходов для LinkedList<>: {linkedListSumElapsedTime}\n");
            }
        }
    }
}
