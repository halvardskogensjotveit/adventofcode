var inputLines = File.ReadLines("C:\\dev\\repos\\adventofcode\\05\\input.txt").ToList();
var lengthOfStackInput = 8;
var numOfStacks = 9;
var stacks = new Stack<string>[numOfStacks];

for (int currentInputLine = lengthOfStackInput-1; currentInputLine > -1; currentInputLine--)
{
    int currentInputOffset = 0;
    for(int currentStack = 0; currentStack < stacks.Length; currentStack++)
    {
        if (stacks[currentStack] == null)
            stacks[currentStack] = new Stack<string>(); 

        var stackString = inputLines[currentInputLine].Substring(currentInputOffset, 3).Trim();
        if (!string.IsNullOrWhiteSpace(stackString))
            stacks[currentStack].Push(stackString[1].ToString());

        currentInputOffset += 4;
    }
}

foreach(var inputLine in inputLines.FindAll(l => l.StartsWith("move")))
{
    var commandStringArr = inputLine.Split(' ');
    var (amount, from, to) = (int.Parse(commandStringArr[1]), int.Parse(commandStringArr[3]) - 1, int.Parse(commandStringArr[5]) - 1);


    for (int i = amount; i > 0; i--)
    {
        stacks[to].Push(stacks[from].Pop());
    }
}

var topOfStackStr = "";
foreach(var stack in stacks)
{
    topOfStackStr += stack.Peek();
}

Console.WriteLine($"Top of stack string: {topOfStackStr}");