using System.IO;
using System.Collections.Generic;
var file = File.ReadAllLines(@".\input.txt");

var width = file[0].Length;
var height = file.Length;
// Dictionary<int, int> locationsOfRiskPoints = new();
List<Point> riskPoints = new();

// Let's construct our heightmap in code
int[,] heightmap = new int[height, width];

for (int i1 = 0; i1 < file.Length; i1++)
{
    string line = file[i1];
    var numbers = line.ToCharArray();
    for (int i = 0; i < numbers.Length; i++)
    {
        heightmap[i1, i] = numbers[i] - '0';
    }
}

for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        Write(heightmap[i, j]);
    }
    Write('\n');
}

// Scan the entire heightmap for risk areas
for (int i = 0; i < height; i++)
{
    for (int j = 0; j < width; j++)
    {
        // scan top - skip if i = 0
        if (i != 0)
            if (heightmap[i - 1, j] <= heightmap[i, j])
                continue;
        // scan bottom - skip if i = height - 1
        if (i != height - 1)
            if (heightmap[i + 1, j] <= heightmap[i, j])
                continue;
        // scan left - skip if j = 0
        if (j != 0)
            if (heightmap[i, j - 1] <= heightmap[i, j])
                continue;
        // scan right - skip if j = height - 1
        if (j != width - 1)
            if (heightmap[i, j + 1] <= heightmap[i, j])
                continue;

        // if the code made it here, we are a risk point
        riskPoints.Add(new(i, j));
    }
}

List<Basin> basins = new();

foreach (Point point in riskPoints)
{
    // Create a basin and increase it in size as much as we can
    Basin basin = new Basin(point);

    
}

struct Point
{
    public int x, y;
    public Point(int x, int y) => (this.x, this.y) = (x, y);
}

struct Basin
{
    public Point origin;
    public int size;
    public Basin(Point origin) => (this.origin, this.size) = (origin, 1);
}

