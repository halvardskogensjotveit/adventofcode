var dictionary = new Dictionary<string, int>
{
    {"A X", 3+1},
    {"A Y", 6+2},
    {"A Z", 0+3},

    {"B X", 0+1},
    {"B Y", 3+2},
    {"B Z", 6+3},

    {"C X", 6+1},
    {"C Y", 0+2},
    {"C Z", 3+3}
};


var file = File.ReadAllLines("C:\\dev\\repos\\adventofcode\\02\\input.txt");

int accumulatedScore = 0;
foreach (var line in file)
{
    accumulatedScore += dictionary[line];
}

Console.WriteLine($"Total: {accumulatedScore}");

var dictionary2 = new Dictionary<string, int>
{
    {"A X", 0+3},
    {"A Y", 3+1},
    {"A Z", 6+2},

    {"B X", 0+1},
    {"B Y", 3+2},
    {"B Z", 6+3},

    {"C X", 0+2},
    {"C Y", 3+3},
    {"C Z", 6+1}
};

int accumulatedScore2 = 0;
foreach (var line in file)
{
    accumulatedScore2 += dictionary2[line];
}

Console.WriteLine($"Total2: {accumulatedScore2}");
