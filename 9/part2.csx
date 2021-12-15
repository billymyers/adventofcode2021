using System.IO;
using System.Collections.Generic;
var file = File.ReadAllLines(@".\input.txt");

var width = file[0].Length;
var height = file.Length;
List<Point> riskPoints = new();
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
        // scan right - skip if j = width - 1
        if (j != width - 1)
            if (heightmap[i, j + 1] <= heightmap[i, j])
                continue;

        // if the code made it here, we are a risk point
        riskPoints.Add(new(j, i));
    }
}

List<Basin> basins = new();
List<Point> pointsAlreadyChecked = new();

foreach (Point point in riskPoints)
{
    // Create a basin and increase it in size as much as we can
    Basin basin = new(point);
    CalculateBasin(point, ref basin);
    basin.additionalPoints = basin.additionalPoints.Distinct().ToList();
    basins.Add(basin);
}
var topThreeBasins = basins.OrderByDescending(x => x.Size).Take(3).ToList();
var result = 1;
topThreeBasins.ForEach(x => result *= x.Size);
WriteLine($"Top 3 basins (of {basins.Count}) multiplied: {result}");

void CalculateBasin(Point point, ref Basin basin)
{
    pointsAlreadyChecked.Add(point);
    // Base case: if this point is greater than or equal to 8
    if(heightmap[point.y,point.x] >= 8)
        return;

    // Our desired value should be one above the point we are looking at
    var desiredValue = heightmap[point.y, point.x] + 1;

    // Check left first
    if(point.x != 0)
        if (heightmap[point.y, point.x - 1] >= desiredValue && heightmap[point.y, point.x - 1] != 9)
        {
            // Add this point to the basin, and check it too!
            Point next = new(point.x - 1, point.y);
            if(!pointsAlreadyChecked.Contains(next))
            {
                basin.additionalPoints.Add(next);
                CalculateBasin(next, ref basin);
            }
            
        }
    // Then right
    if(point.x != width - 1)
        if (heightmap[point.y, point.x + 1] >= desiredValue && heightmap[point.y, point.x + 1] != 9)
        {
            // Add this point to the basin, and check it too!
            Point next = new(point.x + 1, point.y);
            if(!pointsAlreadyChecked.Contains(next))
            {
                basin.additionalPoints.Add(next);
                CalculateBasin(next, ref basin);
            }
        }
    // Then up
    if(point.y != 0)
        if (heightmap[point.y - 1, point.x] >= desiredValue && heightmap[point.y - 1, point.x] != 9)
        {
            // Add this point to the basin, and check it too!
            Point next = new(point.x, point.y - 1);
            if(!pointsAlreadyChecked.Contains(next))
            {
                basin.additionalPoints.Add(next);
                CalculateBasin(next, ref basin);
            }
        }
    // Then down
    if(point.y != height - 1)
        if (heightmap[point.y + 1, point.x] >= desiredValue && heightmap[point.y + 1, point.x] != 9)
        {
            // Add this point to the basin, and check it too!
            Point next = new(point.x, point.y + 1);
            if(!pointsAlreadyChecked.Contains(next))
            {
                basin.additionalPoints.Add(next);
                CalculateBasin(next, ref basin);
            }
        }
}

struct Point
{
    public int x, y;
    public Point(int x, int y) => (this.x, this.y) = (x, y);

    public override string ToString()
    {
        return $"({x},{y})";
    }
}

struct Basin
{
    public Point origin;
    public List<Point> additionalPoints = new();
    public int Size => additionalPoints.Count + 1;
    public Basin(Point origin) => this.origin = origin;
}

