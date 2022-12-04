using System.Runtime.CompilerServices;


var lines = File.ReadAllLines("C:\\dev\\repos\\adventofcode\\04\\Sections\\input.txt");
int count = 0;

foreach (var line in lines)
{
    var stringTuples = line.Split(',');
    var stringTuple1 = stringTuples[0].Split('-');
    var stringTuple2 = stringTuples[1].Split('-');

    Tuple<int, int> tuple1 = Tuple.Create(int.Parse(stringTuple1[0]), int.Parse(stringTuple1[1]));
    Tuple<int, int> tuple2 = Tuple.Create(int.Parse(stringTuple2[0]), int.Parse((stringTuple2[1])));

    if (tuple1.InRange(tuple2) || tuple2.InRange(tuple1))
        count++;
}

Console.WriteLine($"Count: {count}");

public static class TupleExtensions
{
    public static bool InRange(this Tuple<int, int> range, Tuple<int, int> input)
    {
        return input.Item1 >= range.Item1 && input.Item2 <= range.Item2;
    }
}