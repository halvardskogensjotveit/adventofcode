
using System.Globalization;
using System.Security.Cryptography;

var inputCurrentIndex = 0;
var inputLines = File.ReadAllText("C:\\dev\\repos\\adventofcode\\17\\test.txt");
var tetrisPositions = new string[4,7];
Fill(tetrisPositions);

int rocks = 0;
int rock = 0;
var indexOfTopOfRock = -1;
int indexRockPosition = 0;
int heightOfRock = 2;

for (int i = 0; i < 2023; i++)
{
    var height = AddCorrectRock(tetrisPositions);
    //Console.WriteLine("Start");
    //Print(tetrisPositions);
    bool canMove = true;
    while (canMove)
    {
        //Print(tetrisPositions);
        if (inputCurrentIndex == inputLines.Length)
            inputCurrentIndex = 0;
        if (inputLines[inputCurrentIndex] == '<')
        {
            MoveLeft(tetrisPositions, height);
        }
        else
        {
            MoveRight(tetrisPositions, height);
        }
        inputCurrentIndex++;

        if (CanMoveRockDown(tetrisPositions))
        {
            MoveRockDown(tetrisPositions);
        }
        else
        {
            //Print(tetrisPositions);
            canMove = false;
            PlaceRock(tetrisPositions);
        }
        //Print(tetrisPositions);
    }

    //Print(tetrisPositions);
}

//Print(tetrisPositions);
//indexOfTopOfRock = FindTopOfRocks(tetrisPositions);
Console.WriteLine($"Answer? {tetrisPositions.GetLength(0) - indexOfTopOfRock}");

int AddCorrectRock(string[,] curr)
{
    if (rock == 0)
    {
        tetrisPositions = PlaceFlatRock(curr);
        rock++;
        return 1;
    }
    if (rock == 1)
    {
        tetrisPositions = PlaceCrossRock(curr);
        rock++;
        return 3;
    }
    if (rock == 2)
    {
        tetrisPositions = PlaceLRock(curr);
        rock++;
        return 3;
    }
    if (rock == 3)
    {
        tetrisPositions = PlaceTallRock(curr);
        rock++;
        return 4;
    }
    if (rock == 4)
    {
        tetrisPositions = PlaceSquareRock(curr);
        rock = 0;
        return 2;
    }
    return 89;
}

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
    for (int i = indexRockPosition-heightOfRock;i <= indexRockPosition; i++)
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

    for (int i = indexRockPosition - heightOfRock; i <= indexRockPosition; i++)
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
        curr[indexOfTopOfRock - 4, 2 + j] = squareRock[1, j];
    }

    if (indexOfTopOfRock - 5 < 0)
        return curr;

    for (int j = 0; j < squareRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 5, 2 + j] = squareRock[0, j];
    }

    indexRockPosition = indexOfTopOfRock - 4;

    return curr;
}

string[,] PlaceTallRock(string[,] old)
{ 
    var curr = ResizeArray(old, 4);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var tallRock = new string[4] { "@", "@", "@", "@" };
    for (int i = 0; i < tallRock.Length; i++)
    {
        curr[indexOfTopOfRock - (4 + i), 2] = tallRock[i];
    }

    indexRockPosition = indexOfTopOfRock - (4);
    return curr;
}

string[,] PlaceLRock(string[,] old)
{
    var curr = ResizeArray(old, 3);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var lRock = new string[3, 3] { { ".", ".", "@" }, { ".", ".", "@" }, { "@", "@", "@" } };
    for (int j = 0; j < lRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 4, 2 + j] = lRock[2, j];
    }

    for (int j = 0; j < lRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 5, 2 + j] = lRock[1, j];
    }

    for (int j = 0; j < lRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 6, 2 + j] = lRock[0, j];
    }

    indexRockPosition = indexOfTopOfRock - 4;
    return curr;
}

string[,] PlaceCrossRock(string[,] old)
{
    var curr = ResizeArray(old, 3);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var crossRock = new string[3, 3] { { ".", "@", "." }, { "@", "@", "@" }, { ".", "@", "." } };
    for (int j  = 0;j < crossRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 4, 2 + j] = crossRock[2, j];
    }

    for (int j = 0;j < crossRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 5, 2 + j] = crossRock[1, j];
    }

    for (int j = 0; j < crossRock.GetLength(0); j++)
    {
        curr[indexOfTopOfRock - 6, 2 + j] = crossRock[0, j];
    }

    indexRockPosition = indexOfTopOfRock - 4;
    return curr;
}

string[,] PlaceFlatRock(string[,] old)
{
    var curr = ResizeArray(old, 1);
    indexOfTopOfRock = FindTopOfRocks(curr);
    var flatRock = new string[4] {"@", "@", "@", "@" };
    for (int i = 0;i < flatRock.Length; i++)
    {
        curr[indexOfTopOfRock - 4, 2 + i] = flatRock[i];
    }

    indexRockPosition = indexOfTopOfRock - 4;

    return curr;
}


string[,] ResizeArray(string[,] original, int height)
{
    var newArray = new string[original.GetLength(0)+ height, original.GetLength(1)];
    Fill(newArray);
    for (int i = height; i < newArray.GetLength(0); i++)
        for (int j = 0; j < newArray.GetLength(1); j++)
            newArray[i, j] = original[i-height, j];
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
    return curr.GetLength(0);
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
    Console.WriteLine();
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
