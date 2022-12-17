
using System.Globalization;
using System.Security.Cryptography;

var inputCurrentIndex = 0;
var inputLines = File.ReadAllText("C:\\dev\\repos\\adventofcode\\17\\test.txt");
var tetrisPositions = new string[4,7];
Fill(tetrisPositions);

int rocks = 0;
var indexOfTopOfRock = -1;
int indexRockPosition = 0;
int heightOfRock = 2;
//while (rocks < 2023)
//{
//    //PlaceFlatRock()
//}

tetrisPositions = PlaceSquareRock(tetrisPositions);

Print(tetrisPositions);

Console.WriteLine();

MoveRight(tetrisPositions, heightOfRock);
Print(tetrisPositions);
Console.WriteLine();

MoveLeft(tetrisPositions, heightOfRock);
Print(tetrisPositions);
Console.WriteLine();
MoveRight(tetrisPositions, heightOfRock);
Print(tetrisPositions);
Console.WriteLine();

    MoveLeft(tetrisPositions, heightOfRock);
Print(tetrisPositions);
Console.WriteLine();

MoveLeft(tetrisPositions, heightOfRock);
Print(tetrisPositions);
Console.WriteLine();

if (CanMoveRockDown(tetrisPositions))
{
    Console.WriteLine("CAn move");
    MoveRockDown(tetrisPositions);
}
else
{
    Console.WriteLine("CAnt move!");
}
    
Print(tetrisPositions);

if (CanMoveRockDown(tetrisPositions))
{
    Console.WriteLine("CAn move");
    MoveRockDown(tetrisPositions);
}
else
{
    Console.WriteLine("CAnt move!");
}

Print(tetrisPositions);

if (CanMoveRockDown(tetrisPositions))
{
    Console.WriteLine("CAn move");
    MoveRockDown(tetrisPositions);
}
else
{
    Console.WriteLine("CAnt move!");
}

Print(tetrisPositions);

if (CanMoveRockDown(tetrisPositions))
{
    Console.WriteLine("CAn move");
    MoveRockDown(tetrisPositions);
}
else
{
    Console.WriteLine("CAnt move!");
}

Print(tetrisPositions);
Console.WriteLine();

PlaceRock(tetrisPositions);

Print(tetrisPositions);

void PlaceRock(string[,] curr)
{
    for (int i = indexRockPosition;i > indexRockPosition -4; i--)
    {
        for (int j = 0; j < curr.GetLength(1); j++)
        {
            if (curr[i, j].Equals("@"))
            {
                curr[i, j] = "#";
            }
        }
    }
}

void MoveLeft(string[,] curr, int heightOfRock)
{
    for (int i = indexOfTopOfRock -3-heightOfRock;i <= indexOfTopOfRock - 3; i++)
    {
        for (int j = 0;j < curr.GetLength(1); j++)
        {
            if (curr[i, j].Equals("@"))
            {
                if (j-1 < 0 || curr[i, j - 1].Equals("#"))
                {
                    return;
                }
            }
        }
    }

    for (int i = indexOfTopOfRock - 3 - heightOfRock; i <= indexOfTopOfRock - 3; i++)
    {
        for (int j = 0; j < curr.GetLength(1); j++)
        {
            if (curr[i, j].Equals("@"))
            {
                curr[i, j] = ".";
                curr[i, j - 1] = "@";
            }
        }
    }
}

void MoveRight(string[,] curr, int heightOfRock)
{
    for (int i = indexRockPosition - heightOfRock; i <= indexRockPosition; i++)
    {
        for (int j = 0; j < curr.GetLength(1); j++)
        {
            if (curr[i, j].Equals("@"))
            {
                if (j + 1 == curr.GetLength(1) || curr[i, j + 1].Equals("#"))
                {
                    return;
                }
            }
        }
    }

    for (int i = indexRockPosition - heightOfRock; i <= indexRockPosition; i++)
    {
        for (int j = curr.GetLength(1)-1; j >-1; j--)
        {
            if (curr[i, j].Equals("@"))
            {
                curr[i, j] = ".";
                curr[i, j + 1] = "@";
            }
        }
    }
}

bool CanMoveRockDown(string[,] curr)
{
    for (int i = indexRockPosition; i >= 0; i--)
    {
        for (int j = 0; j < curr.GetLength(1); j++)
        {
            if (curr[i, j].Equals("@"))
            {
                if (i + 1 == curr.GetLength(0) || curr[i+1, j].Equals("#"))
                {
                    return false;
                }
            }
        }
    }
    return true;
}

void MoveRockDown(string[,] curr)
{
    for (int i = indexRockPosition; i >= 0; i--)
    {
        for (int j = 0; j < curr.GetLength(1); j++)
        {
            if (curr[i, j].Equals("@"))
            {
                curr[i, j] = ".";
                curr[i+1, j ] = "@";
            }
        }
    }

    indexRockPosition += 1;
}

void LandRock()
{
    // 4 indexes replace @ with #
}

string[,] PlaceSquareRock(string[,] old)
{
    var curr = ResizeArray(old, 2);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var squareRock = new string[2, 2] { { "@", "@"}, { "@", "@"} };
    for (int j = 0; j < squareRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 3, 2 + j] = squareRock[1, j];
    }

    if (indexOfTopOfRock - 4 < 0)
        return curr;

    for (int j = 0; j < squareRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 4, 2 + j] = squareRock[0, j];
    }

    indexRockPosition = indexOfTopOfRock - 3;

    return curr;
}

string[,] PlaceTallRock(string[,] old)
{ 
    var curr = ResizeArray(old, 4);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var tallRock = new string[4] { "@", "@", "@", "@" };
    for (int i = 0; i < tallRock.Length; i++)
    {
        curr[indexOfTopOfRock - (3 + i), 2] = tallRock[i];
    }

    indexRockPosition = indexOfTopOfRock - (3);
    return curr;
}

string[,] PlaceLRock(string[,] old)
{
    var curr = ResizeArray(old, 3);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var lRock = new string[3, 3] { { ".", ".", "@" }, { ".", ".", "@" }, { "@", "@", "@" } };
    for (int j = 0; j < lRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 3, 2 + j] = lRock[2, j];
    }

    for (int j = 0; j < lRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 4, 2 + j] = lRock[1, j];
    }

    for (int j = 0; j < lRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 5, 2 + j] = lRock[0, j];
    }

    indexRockPosition = indexOfTopOfRock - 3;
    return curr;
}

string[,] PlaceCrossRock(string[,] old)
{
    var curr = ResizeArray(old, 3);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var crossRock = new string[3, 3] { { ".", "@", "." }, { "@", "@", "@" }, { ".", "@", "." } };
    for (int j  = 0;j < crossRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 3, 2 + j] = crossRock[2, j];
    }

    for (int j = 0;j < crossRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 4, 2 + j] = crossRock[1, j];
    }

    for (int j = 0; j < crossRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 5, 2 + j] = crossRock[0, j];
    }

    indexRockPosition = indexOfTopOfRock - 3;
    return curr;
}

string[,] PlaceFlatRock(string[,] old)
{
    var curr = ResizeArray(old, 1);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var flatRock = new string[4] {"@", "@", "@", "@" };
    for (int i = 0;i < flatRock.Length; i++)
    {
        curr[indexOfTopOfRock - 3, 2 + i] = flatRock[i];
    }

    indexRockPosition = indexOfTopOfRock - 3;

    return curr;
}


string[,] ResizeArray(string[,] original, int height)
{
    var newArray = new string[original.GetLength(0)+ height, original.GetLength(1)];
    Fill(newArray);
    for (int i = 0; i < newArray.GetLength(0)-height; i++)
        for (int j = 0; j < newArray.GetLength(1); j++)
            newArray[i, j] = original[i, j];
    return newArray;
}

int FindTopOfRocks(string[,] curr)
{
    for (int i = 0; i < curr.GetLength(0); i++)
    {
        for (int j = 0; j < curr.GetLength(1); j++)
        {
            if (curr[i, j].Equals("#"))
            {
                return i;
            }
        }
    }
    return curr.GetLength(0)-1;
}

void Print(string[,] curr)
{
    for (int i = 0; i < curr.GetLength(0); i++)
    {
        var currString = "";
        for (int j = 0; j < curr.GetLength(1); j++)
        {
            currString += curr[i, j];
        }
        Console.WriteLine(currString);
    }
}

void Fill(string[,] curr)
{
    for (int i = 0; i < curr.GetLength(0); i++)
    {
        for (int j = 0; j < curr.GetLength(1); j++)
        {
            curr[i, j] = ".";
        }
    }
}
