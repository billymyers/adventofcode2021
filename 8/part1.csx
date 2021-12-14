using System.IO;
var file = File.ReadAllLines(@".\input.txt");
var uniqueNumberInOutputs = 0;

foreach(var line in file)
{
    var output = line.Split('|')[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
    output.ForEach(x => x = x.Trim());

    foreach(var segment in output)
    {
        switch(segment.Length)
        {
            case 7:
                // 8
            case 4:
                // 4
            case 3:
                // 7
            case 2:
                // 1
                uniqueNumberInOutputs++;
                break;
            default:
                // nothing
                break;
        }
    }
}

WriteLine($"Number of 1s, 4s, 7s, and 8s: {uniqueNumberInOutputs}");