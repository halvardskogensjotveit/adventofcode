var inputLines = File.ReadAllLines("C:\\dev\\repos\\adventofcode\\09\\input.txt");

var tupleArray = new List<Tuple<int, int>>();
int startI = 0;
int startJ = 4;

var head = new MutableTuple { Item1 = startI, Item2 = startJ };
var tail = new MutableTuple { Item1 = startI, Item2 = startJ };

foreach (var line in inputLines)
{
    var lineArr = line.Split(' ');
    var command = lineArr[0];
    var steps = int.Parse(lineArr[1]);
    while (steps > 0)
    {
        var isUpOrDown = MoveAndGetUpOrDown(head, command);
        if (!IsAdjacent(head, tail))
        {
            MoveAndGetUpOrDown(tail, command);
            if (!IsColumnOrRow(head, tail))
            {
                if (isUpOrDown)
                {
                    MoveRightOrLeft(head, tail);                    
                }
                else
                {
                    ModeUpOrDown(head, tail);
                }
            }
        }
        tupleArray.Add(new Tuple<int, int>(tail.Item1, tail.Item2));
        steps--;
    }
}

Console.WriteLine(tupleArray.Distinct().Count());

bool MoveAndGetUpOrDown(MutableTuple tupleToMove, string command)
{
    switch (command)
    {
        case "U":
            tupleToMove.Item2--;
            return true;
        case "D":
            tupleToMove.Item2++;
            return true;
        case "R":
            tupleToMove.Item1++;                
            return false;
        case "L":
            tupleToMove.Item1--;                
            return false;
        default:
            return false;
    }
}

void MoveRightOrLeft(MutableTuple head, MutableTuple tail)
{
    if (tail.Item1 < head.Item1)
    {
        tail.Item1++;
        return;
    }        
    tail.Item1--;
}

void ModeUpOrDown(MutableTuple head, MutableTuple tail)
{
    if (tail.Item2 > head.Item2)
    {
        tail.Item2--;
        return;
    }
    tail.Item2++;
}

bool IsColumnOrRow(MutableTuple head, MutableTuple tail)
{
    if (head.Item1 == tail.Item1 || head.Item2 == tail.Item2)
        return true;
    return false;
}

bool IsAdjacent(MutableTuple head, MutableTuple tail, int diff = 1)
{
    if (Math.Abs(head.Item1 - tail.Item1) <= diff && Math.Abs(head.Item2 - tail.Item2) <= diff)
        return true;
    return false;
}

public class MutableTuple
{
    public int Item1 { get; set; }
    public int Item2 { get; set; }
    public bool Tail { get; set; }
    public bool Head { get; set; }
}

