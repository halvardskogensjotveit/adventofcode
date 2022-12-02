// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using System.Net;

Console.WriteLine("Hello, World!");
var file = File.ReadAllLines("C:\\Users\\HalvardSjøtveit\\source\\repos\\ConsoleApp2\\ConsoleApp2\\input.txt");
int accumulated = 0;
var list = new List<int>();
foreach (var line in file)
{

    int currentNum = 0;
    if(int.TryParse(line, out currentNum))
    {
        accumulated += currentNum;
    }
    else
    {
        list.Add(accumulated);
        accumulated = 0;
    }
}
list.Sort();
list.Reverse();
var biggest = list.Take(3).Sum();

Console.WriteLine($"Biggest: {biggest}");