var input = File.ReadLines("C:\\dev\\repos\\adventofcode\\07\\input.txt");

var head = new DirectoryNode() { DirName = "/"};
var currentDirectory = head;

foreach (var line in input)
{
    if(line.StartsWith("$ cd"))
    {
        var dir = line.Split(' ')[2];
        if (dir.Equals("/"))
            continue;
        if (dir.Equals(".."))
            currentDirectory = currentDirectory.Parent;
        else
            currentDirectory = currentDirectory.FindChild(dir);
    }  
    else if (line.StartsWith("$ ls"))
    {
        continue;
    }
    else if(int.TryParse(line.Split(' ').First(), out int result))
    {
        currentDirectory.Files.Add(result);
    }
    else if (line.StartsWith("dir"))
    {
        currentDirectory.ChildDirectories.Add(new DirectoryNode() {
            DirName = line.Split(' ')[1],
            Parent = currentDirectory
        });       
    }
}


int fileSizeSum = 0;
var head2 = head;
var spaceUsed = TreeTraverseSum(head);
Console.WriteLine($"Filesize sum: {fileSizeSum}, sum of head: {spaceUsed}");

int spaceTotal = 70000000;
int spaceRequired = 30000000;
int spaceToFree = spaceRequired - (spaceTotal - spaceUsed);

int currentDeleteSum = spaceTotal;
int dircount = 0;
TreeTraverseDelete(head2);
Console.WriteLine($"Delete directory size: {currentDeleteSum} dircount {dircount}");

int TreeTraverseDelete(DirectoryNode currentNode)
{
    int currentSum = currentNode.Files.Sum();
    dircount++;
    foreach (var childNode in currentNode.ChildDirectories)
    {
        currentSum += TreeTraverseDelete(childNode);
    }

    if (currentSum >= spaceToFree) {
        if (currentSum < currentDeleteSum)
            currentDeleteSum = currentSum;
    }
    return currentSum;
}


int TreeTraverseSum(DirectoryNode currentNode)
{
    int currentSum = currentNode.Files.Sum();
    foreach(var childNode in currentNode.ChildDirectories)
    {
        currentSum += TreeTraverseSum(childNode);
    }
    if (currentSum < 100000 + 1)
        fileSizeSum += currentSum;
    return currentSum;
}

class DirectoryNode
{
    public string DirName { get; init; }
    public List<int> Files = new List<int>();
    public List<DirectoryNode> ChildDirectories = new List<DirectoryNode>();
    public DirectoryNode? Parent { get; set; }

    public DirectoryNode FindChild(string dirName)
    {
        return ChildDirectories.Find(d => d.DirName.Equals(dirName));
    }
}


