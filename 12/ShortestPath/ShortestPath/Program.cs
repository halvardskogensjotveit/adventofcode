var inputLines = File.ReadAllLines("C:\\dev\\repos\\adventofcode\\12\\test.txt");
var xLength = inputLines.Length;
var yLength = inputLines[0].Length;
char[,] map = new char[xLength ,yLength];
int shortestPath = 1000000;
int xStart = -1;
int yStart = -1;
int endX = -1;
int endY = -1;

var list = new List<Tuple<int, int>>();

for (int x = 0; x < xLength; x++)
{
    for (int y = 0;y < yLength; y++)
    {
        map[x,y] = inputLines[x][y];
        if (map[x, y].Equals('S'))
        {
            map[x, y] = 'a';
            xStart = x;
            yStart = y;
        }
        if (map[x, y].Equals('E'))
        {
            map[x, y] = 'z';
            endX= x;
            endY= y;
        }
    }
}

FindNextMove(xStart, yStart, 0, list);

Console.WriteLine(shortestPath);

void FindNextMove(int x, int y, int currCount, List<Tuple<int,int>> moves)
{
    moves.Add(new Tuple<int, int>(x,y));

    if (currCount != 0 && x == xStart && y == yStart)
    {
        return;
    }

    if (x == 2 && y == 7)
    {
        Console.WriteLine("test");
    }
    if (x == endX && y == endY)
    {
        if (shortestPath > currCount)
            shortestPath = currCount;
        moves.Remove(new Tuple<int, int>(x, y));
        return;
    }

    //UP
    if (y-1 >= 0 && CanTraverse(x, y, x, y-1, moves))
    {
        FindNextMove(x, y-1, currCount+1, moves);        
    }

    // DOWN
    if (y+1 < yLength && CanTraverse(x, y, x, y+1, moves))
    {
        FindNextMove(x, y + 1, currCount + 1, moves);
    }

    // RIGHT
    if (x+1 < xLength && CanTraverse(x, y, x+1, y, moves))
    {
        FindNextMove(x+1 ,y, currCount+1, moves);
    }

    // LEFT
    if (x-1 >= 0 && CanTraverse(x,y,x-1, y, moves))
    {
        FindNextMove(x - 1, y, currCount + 1, moves);
    }
    moves.Remove(new Tuple<int, int>(x, y));
}

bool CanTraverse(int x, int y, int nextX, int nextY, List<Tuple<int, int>> moves)
{
    if (moves.Contains(new Tuple<int, int>(nextX, nextY)))
        return false;
    if (((int)map[nextX, nextY] - (int)map[x, y]) < 2)
        return true;
    return false;
}
