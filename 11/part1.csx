using System.IO;
using System.Collections.Generic;

var file = File.ReadAllLines(@".\input.txt");
int width = file[0].Length, height = file.Length;
const int ITERATIONS = 100;
int[,] octopi = new int[height, width];
HashSet<Point> pointsFlashed = new();
int flashes = 0;

for (int i = 0; i < height; i++)
{
    var octopiRaw = file[i].ToCharArray();
    for (int j = 0; j < width; j++)
    {
        octopi[i, j] = octopiRaw[j] - '0';
    }
}

for (int i = 1; i <= ITERATIONS; i++)
{
    RunIteration();
    WriteLine($"Iteration {i}: {flashes} flashes");
}

void RunIteration()
{
    // Clear our list of points flashed (only one flash per iteration)
    pointsFlashed.Clear();
    List<Point> pointsToFlash = new();

    // First, increment all octopi by one
    for (int i = 0; i < height; i++)
        for (int j = 0; j < width; j++)
        {
            if (++octopi[i, j] > 9)
            {
                pointsToFlash.Add(new(j, i));
            }
        }

    while (pointsToFlash.Count != 0)
    {
        WriteLine($"Points to flash: {pointsToFlash.Count}");
        var newPointsToFlash = new List<Point>();
        foreach (var point in pointsToFlash)
        {
            // Top left
            try
            {
                octopi[point.y - 1, point.x - 1]++;
                if (octopi[point.y - 1, point.x - 1] > 9)
                    newPointsToFlash.Add(new(point.x - 1, point.y - 1));
            }
            catch (IndexOutOfRangeException) { }

            // Top
            try
            {
                octopi[point.y - 1, point.x]++;
                if (octopi[point.y - 1, point.x] > 9)
                    newPointsToFlash.Add(new(point.x, point.y - 1));
            }
            catch (IndexOutOfRangeException) { }

            // Top right
            try
            {
                octopi[point.y - 1, point.x + 1]++;
                if (octopi[point.y - 1, point.x + 1] > 9)
                    newPointsToFlash.Add(new(point.x + 1, point.y - 1));
            }
            catch (IndexOutOfRangeException) { }

            // Left
            try
            {
                octopi[point.y, point.x - 1]++;
                if (octopi[point.y, point.x - 1] > 9)
                    newPointsToFlash.Add(new(point.x - 1, point.y));
            }
            catch (IndexOutOfRangeException) { }

            // Right
            try
            {
                octopi[point.y, point.x + 1]++;
                if (octopi[point.y, point.x + 1] > 9)
                    newPointsToFlash.Add(new(point.x + 1, point.y));
            }
            catch (IndexOutOfRangeException) { }

            // Bottom left
            try
            {
                octopi[point.y + 1, point.x - 1]++;
                if (octopi[point.y + 1, point.x - 1] > 9)
                    newPointsToFlash.Add(new(point.x - 1, point.y + 1));
            }
            catch (IndexOutOfRangeException) { }

            // Bottom
            try
            {
                octopi[point.y + 1, point.x]++;
                if (octopi[point.y + 1, point.x] > 9)
                    newPointsToFlash.Add(new(point.x, point.y + 1));
            }
            catch (IndexOutOfRangeException) { }

            // Bottom right
            try
            {
                octopi[point.y + 1, point.x + 1]++;
                if (octopi[point.y + 1, point.x + 1] > 9)
                    newPointsToFlash.Add(new(point.x + 1, point.y + 1));
            }
            catch (IndexOutOfRangeException) { }
        }
        pointsToFlash.Distinct().ToList().ForEach(x => pointsFlashed.Add(x));
        pointsToFlash.Clear();
        newPointsToFlash.Distinct().ToList().ForEach(x =>
        {
            if (!pointsFlashed.Contains(x))
                pointsToFlash.Add(x);
        });
    }

    foreach (var point in pointsFlashed)
    {
        octopi[point.y, point.x] = 0;
    }

    flashes += pointsFlashed.Count;
}

struct Point
{
    public int x, y;
    public Point(int x, int y) => (this.x, this.y) = (x, y);
    public override string ToString() => $"({x}, {y})";
}