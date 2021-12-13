using System.IO;

private const int V = 1000;
var file = File.ReadAllLines(@".\input.txt");

struct Point
{
    public int x, y;

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public Point(string input)
    {
        var numbers = input.Split(',');
        x = int.Parse(numbers[0]);
        y = int.Parse(numbers[1]);
    }

    public static bool operator ==(Point a, Point b) => a.x == b.x && a.y == b.y;
    public static bool operator !=(Point a, Point b) => a.x != b.x || a.y != b.y;
}

struct Line
{
    public Point a;
    public Point b;

    public Line(string input)
    {
        var points = input.Split(" -> ");
        a = new(points[0]);
        b = new(points[1]);
    }
}

var lines = new List<Line>();
var coordPlane = new int[V, V];

foreach (var line in file)
{
    lines.Add(new(line));
}

for (int i = 0; i < V; i++)
    for (int j = 0; j < V; j++)
        coordPlane[i, j] = 0;

for (int i1 = 0; i1 < lines.Count; i1++)
{
    Line line = lines[i1];
    // figure out if the line is vertical or horizontal
    if (line.a.x == line.b.x)
    {
        // Vertical, loop through the line vertically
        if (line.a.y > line.b.y)
        {
            for (int i = line.a.y; i >= line.b.y; i--)
            {
                coordPlane[line.a.x, i]++;
            }
        }
        else
        {
            for (int i = line.a.y; i <= line.b.y; i++)
            {
                coordPlane[line.a.x, i]++;
            }
        }
    }
    else if (line.a.y == line.b.y)
    {
        // Horizontal, loop through the line horizontally
        if (line.a.x > line.b.x)
        {
            for (int i = line.a.x; i >= line.b.x; i--)
            {
                coordPlane[i, line.a.y]++;
            }
        }
        else
        {
            for (int i = line.a.x; i <= line.b.x; i++)
            {
                coordPlane[i, line.a.y]++;
            }
        }
    }
    else
    {
        // Diagonal time baby!
        if (line.a.x > line.b.x)
        {
            Point temp = line.a;
            line.a = line.b;
            line.b = temp;
        }

        bool goesNegative = line.a.y > line.b.y;
        for (int i = 0; i <= line.b.x - line.a.x; i++)
        {
            // We need to get the y coordinate
            var yCoord = goesNegative ? line.a.y - i : line.a.y + i;
            var xCoord = line.a.x + i;
            coordPlane[xCoord, yCoord]++;
        }
    }
}

var intersections = 0;

for (int i = 0; i < V; i++)
{
    for (int j = 0; j < V; j++)
    {
        if (coordPlane[i, j] > 1) intersections++;
    }
}

WriteLine($"Intersections: {intersections}");