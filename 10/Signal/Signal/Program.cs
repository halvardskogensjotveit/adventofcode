using System.Reflection;

var lines = File.ReadAllLines("C:\\dev\\repos\\adventofcode\\10\\input.txt");

var signal = 0;
var x = 1;
var accumulatedSignalStrength = 0;

foreach (var line in lines)
{
    if (line.StartsWith("noop"))
    {
        signal++;
        CheckSignal();
    }
    else
    {
        signal++;
        CheckSignal();
        signal++;
        CheckSignal();
        x += int.Parse(line.Split(' ')[1]); ;
    }
}

Console.WriteLine($"Accumulated signal strength: {accumulatedSignalStrength}");

void CheckSignal()
{
    if (signal == 20 || signal % 40 == 20)
    {
        accumulatedSignalStrength += x * signal;
    }
}

var signalDraw = 0;
var xDraw = 1;
var currString = "";


foreach (var line in lines)
{
    if (line.StartsWith("noop"))
    {
        signalDraw++;
        CheckSignalAndDraw();
    }
    else
    {
        signalDraw++;
        CheckSignalAndDraw();
        signalDraw++;
        CheckSignalAndDraw();
        xDraw += int.Parse(line.Split(' ')[1]); ;
    }
}


void CheckSignalAndDraw()
{
    currString += IsSprite();
    if (signalDraw % 40 == 0)
    {
        Console.WriteLine(currString);
        currString = "";
    }
}

string IsSprite()
{
    var index = (signalDraw % 40)-1;
    return Math.Abs(xDraw - index) <= 1 ? "#" : ".";
}