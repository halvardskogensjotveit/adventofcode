var inputLines = File.ReadLines("C:\\dev\\repos\\adventofcode\\05\\input.txt").ToList();

var stacks9000 = CreateStacks(inputLines);
foreach (var inputLine in inputLines.FindAll(l => l.StartsWith("move")))
{
    var (amount, from, to) = GetCommands(inputLine);
    for (int i = amount; i > 0; i--)
    {
        stacks9000[to].Push(stacks9000[from].Pop());
    }
}

var topOfStackStr9000 = "";
foreach(var stack in stacks9000)
{
    topOfStackStr9000 += stack.Peek();
}

Console.WriteLine($"Top of stack string 9000: {topOfStackStr9000}");


var stacks9001 = CreateStacks(inputLines);
foreach (var inputLine in inputLines.FindAll(l => l.StartsWith("move")))
{
    var (amount, from, to) = GetCommands(inputLine);
    var tempStack = new List<string>();
    for (int i = amount; i > 0; i--)
    {
        tempStack.Add(stacks9001[from].Pop());
    }
    tempStack.Reverse();
    tempStack.ForEach(e => stacks9001[to].Push(e));
    
}

var topOfStackStr9001 = "";
foreach (var stack in stacks9001)
{
    topOfStackStr9001 += stack.Peek();
}

Console.WriteLine($"Top of stack string 9001: {topOfStackStr9001}");


(int, int, int) GetCommands(string inputLine)
{
    var commandStringArr = inputLine.Split(' ');
    return (int.Parse(commandStringArr[1]), int.Parse(commandStringArr[3]) - 1, int.Parse(commandStringArr[5]) - 1);
}

Stack<string>[] CreateStacks(List<string> inputLines)
{
    var lengthOfStackInput = 8;
    var numOfStacks = 9;

    var stacks = new Stack<string>[numOfStacks];
    for (int currentInputLine = lengthOfStackInput - 1; currentInputLine > -1; currentInputLine--)
    {
        int currentInputOffset = 0;
        for (int currentStack = 0; currentStack < stacks.Length; currentStack++)
        {
            if (stacks[currentStack] == null)
                stacks[currentStack] = new Stack<string>();

            var stackString = inputLines[currentInputLine].Substring(currentInputOffset, 3).Trim();
            if (!string.IsNullOrWhiteSpace(stackString))
                stacks[currentStack].Push(stackString[1].ToString());

            currentInputOffset += 4;
        }
    }
    return stacks;
}