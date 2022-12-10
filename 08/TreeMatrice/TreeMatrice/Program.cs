var inputLines = File.ReadAllLines("C:\\dev\\repos\\adventofcode\\08\\input.txt");
var length = inputLines[0].Length;
var depth = inputLines.Length;
int[,] lengthLists = new int[length, depth];
int[,] depthLists = new int[depth, length];

for (int i = 0; i < length; i++)
{
    for (int j = 0; j < depth; j++)
    {
        var currentInt = int.Parse(inputLines[i][j].ToString());
        lengthLists[i, j] = currentInt;
        depthLists[j, i] = currentInt;
    }
}

var visibleTreeCount = length * 2 + (depth-2) * 2;

for (int i = 1; i < length - 1; i++)
{
    for (int j = 1; j < depth - 1; j++)
    {
            if (VisibleFromLeft(i, j, lengthLists, lengthLists[i, j])
                || VisibleFromRight(i, j, lengthLists, lengthLists[i, j])
                || VisibleFromTop(j, i, depthLists, depthLists[j, i])
                || VisibleFromBottom(j, i, depthLists, depthLists[j, i]))
            visibleTreeCount++; 

    }
}

Console.WriteLine($"Visible trees: {visibleTreeCount}");

var highestViewDistance = 0;

for (int i = 1; i < length - 1; i++)
{
    for (int j = 1; j < depth - 1; j++)
    {
        var right = RightViewvingDistance(i, j, lengthLists, lengthLists[i, j]);
        var left = LeftViewingDistance(i, j, lengthLists, lengthLists[i, j]);
        var top = TopViewingDistance(j, i, depthLists, depthLists[j, i]);
        var bottom = BottomViewingDistance(j, i, depthLists, depthLists[j, i]);
        //Console.WriteLine($"num:{lengthLists[i, j]}    {right} {left} {top} {bottom}");

        var currentViewingDistance = right * left * top * bottom;
        if (currentViewingDistance > highestViewDistance)
            highestViewDistance = currentViewingDistance;
    }
}

Console.WriteLine($"Highest viewing distance: {highestViewDistance}");

int RightViewvingDistance(int i, int j, int[,] list, int value)
{
    int viewingDistance = 0;
    for (int right = j + 1; right < length; right++)
    {
        viewingDistance++;
        if (list[i, right] >= value)
        {
            return viewingDistance;
        }
    }
    return viewingDistance;
}

int LeftViewingDistance(int i, int j, int[,] list, int value)
{
    int viewingDistance = 0;
    for (int left = j-1; left >= 0; left--)
    {
        viewingDistance++;
        if (list[i, left] >= value)
        {
            return viewingDistance;
        }
    }
    return viewingDistance;
}

int TopViewingDistance(int i, int j, int[,] list, int value)
{
    int viewingDistance = 0;
    for (int top = j-1; top >= 0; top--)
    {
        viewingDistance++;
        if (list[i, top] >= value)
        {
            return viewingDistance;
        }
    }
    return viewingDistance;
}

int BottomViewingDistance(int i, int j, int[,] list, int value)
{
    int viewingDistance = 0;
    for (int bottom = j + 1; bottom < depth; bottom++)
    {
        viewingDistance++;
        if (list[i, bottom] >= value)
        {
            return viewingDistance;
        }
    }
    return viewingDistance;
}


bool VisibleFromRight(int i, int j, int[,] list, int value)
{
    for (int right = j + 1; right < length; right++)
    {
        if (list[i, right] >= value)
        {
            return false;
        }
    }
    return true;
}

bool VisibleFromLeft(int i, int j, int[,] list, int value)
{
    for (int left = 0; left < j; left++)
    {
        if (list[i, left] >= value)
        {
            return false;
        }
    }
    return true;
}

bool VisibleFromTop(int i, int j, int[,] list, int value)
{
    for (int top = 0; top < j; top++)
    {
        if (list[i, top] >= value)
        {
            return false;
        }
    }
    return true;
}

bool VisibleFromBottom(int i, int j, int[,] list, int value)
{
    for (int bottom = j + 1; bottom < depth; bottom++)
    {
        if (list[i, bottom] >= value)
        {
            return false;
        }
    }
    return true;
}



