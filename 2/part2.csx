using System.IO;

var file = File.ReadAllLines(".\\input.txt");
int depth = 0, hpos = 0, aim = 0;

foreach(var line in file)
{
    var command = line.Split(' ').First();
    var distance = int.Parse(line.Split(' ').Last());
    switch(command)
    {
        case "forward":
            hpos += distance;
            depth += aim * distance;
            break;
        case "up":
            aim -= distance;
            break;
        case "down":
            aim += distance;
            break;
        default:
            throw new Exception($"Invalid command: {command}");
    }
}

WriteLine($"[DEPTH - {depth} | AIM - {aim} | H. POSITION - {hpos} | DISTANCE - {hpos * depth}");