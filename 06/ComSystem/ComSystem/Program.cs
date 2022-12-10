var input = File.ReadAllText("C:\\dev\\repos\\adventofcode\\06\\input.txt");
for (int i = 0;i+4< input.Length; i++)
{
    if (!CheckDuplicates(input.Substring(i, 4)))
    {
        Console.WriteLine($"No duplicate chars in packet {input.Substring(i, 4)} at index: {i+4}");
        break;
    }        
}

for (int i = 0; i + 14 < input.Length; i++)
{
    if (!CheckDuplicates(input.Substring(i, 14)))
    {
        Console.WriteLine($"No duplicate chars in message {input.Substring(i, 14)} at index: {i + 14}");
        break;
    }
}

bool CheckDuplicates(string text)
{
    return text.GroupBy(n => n).Any(n => n.Count() >= 2);
}