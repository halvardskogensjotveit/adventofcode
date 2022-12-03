var lines = File.ReadAllLines("C:\\dev\\repos\\adventofcode\\03\\Rucksack\\input.txt");

int prioritiesSum = 0;
foreach(var line in lines)
{
    if (line.Length % 2 != 0) 
        throw new Exception($"Invalid input, odd length of string: {line}");

    var currStrings = new string[]
    {
        line.Substring(0, line.Length/2),
        line.Substring(line.Length/2)
    };

    var errorChar = currStrings[0].ToCharArray().First(c => currStrings[1].Contains(c));
    var isUpper = char.IsUpper(errorChar);
    var intChar = ((isUpper ? errorChar : char.ToUpperInvariant(errorChar)) - 'A') + 1;
    var priority = isUpper ? intChar + 26 : intChar;
    prioritiesSum += priority;
}

Console.WriteLine($"PrioritiesSum: {prioritiesSum}");
