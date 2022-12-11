using System.Runtime.CompilerServices;

var inputLines = File.ReadAllLines("C:\\dev\\repos\\adventofcode\\11\\test.txt");
var monkeys = new List<Monkey>();
for (int i = 0;i < inputLines.Length;i+=7)
{
    var currMonkey = new Monkey();
    GetStartingItems(currMonkey, inputLines[i + 1]);
    GetOperation(currMonkey, inputLines[i+2]);
    GetTest(currMonkey, inputLines[i+3], inputLines[i+4], inputLines[i+5]);
    monkeys.Add(currMonkey);

}

for (int i = 0;i < 20; i++)
{
    foreach(var monkey in monkeys)
    {
        foreach (var item in monkey.Items)
        {
            var currentvalue = item;
            currentvalue = monkey.Operation(item);
            currentvalue = currentvalue / 3;
            var monkeyToThrowTo = monkey.Test(currentvalue);
            monkeys[monkeyToThrowTo].Items.Add(currentvalue);
            monkey.Inspected++;
        }
        monkey.Items = new List<long>();
    }
}

var topInspected = monkeys.OrderByDescending(m => m.Inspected).Take(2);

Console.WriteLine(topInspected.First().Inspected * topInspected.Last().Inspected);


var monkeyBogBoyRounds = new List<Monkey>();
for (int i = 0; i < inputLines.Length; i += 7)
{
    var currMonkey = new Monkey();
    GetStartingItems(currMonkey, inputLines[i + 1]);
    GetOperation(currMonkey, inputLines[i + 2]);
    GetTest(currMonkey, inputLines[i + 3], inputLines[i + 4], inputLines[i + 5]);
    monkeyBogBoyRounds.Add(currMonkey);

}

for (int i = 1; i < 10001; i++)
{
    foreach (var monkey in monkeyBogBoyRounds)
    {
        foreach (var item in monkey.Items)
        {
            var currentvalue = item;
            currentvalue = monkey.Operation(currentvalue);
            var monkeyToThrowTo = monkey.Test(currentvalue);
            monkeyBogBoyRounds[monkeyToThrowTo].Items.Add(currentvalue);
            monkey.Inspected++;
        }
        monkey.Items = new List<long>();
        if (i == 19 || i % 1000 == 0)
        {
            Console.WriteLine(monkey.Inspected);
        }
    }
}

var topInspectedBigBoy = monkeyBogBoyRounds.OrderByDescending(m => m.Inspected).Take(2);
Console.WriteLine(topInspectedBigBoy.First().Inspected);
Console.WriteLine(topInspectedBigBoy.Last().Inspected);

Console.WriteLine(topInspectedBigBoy.First().Inspected * topInspectedBigBoy.Last().Inspected);

void GetStartingItems(Monkey monkey, string line)
{
    var items = line.Split(':')[1].Split(',');
    foreach (var item in items)
    {
        item.Trim();
        monkey.Items.Add(long.Parse(item));
    }
}

void GetOperation(Monkey monkey, string line)
{
    line = line.Trim();
    var splitLines = line.Split(" ");
    var operation = splitLines[4];
    if (operation.Equals("+"))
    {
        monkey.Operation = (i) => i + long.Parse(splitLines[5]);
    }
    else if (splitLines.Where(x => x.Equals("old")).Count() > 1) 
    {
        monkey.Operation = (i) => i * i;
    }
    else
    {
        monkey.Operation = (i) => i * long.Parse(splitLines[5]);
    }
}

void GetTest(Monkey monkey, string testLine, string trueLine, string falseLine)
{
    testLine = testLine.Trim();
    trueLine = trueLine.Trim();
    falseLine = falseLine.Trim();
    var divisibleNumber = long.Parse(testLine.Split(' ')[3]);
    var monkey1 = long.Parse(trueLine.Split(' ')[5]);
    var monkey2 = long.Parse(falseLine.Split(' ')[5]);
    monkey.Test = (item) => (int)(item % divisibleNumber == 0 ? monkey1 : monkey2);
}



public class Monkey
{
    public List<long> Items = new List<long>();
    public Func<long, long> Operation { get; set; }
    public Func<long, int> Test {get;set;}
    public long Inspected { get; set; }
}
