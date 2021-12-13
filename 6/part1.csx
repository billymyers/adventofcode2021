// Note: part 1 and 2 use the same code :)
using System.IO;
using System.Collections.Generic;
var file = File.ReadAllText(@".\input.txt").Split(',').ToList();

// Change # of days here
private const int NUM_DAYS = 256;

var lanternfish = new ulong[9];
lanternfish = Enumerable.Repeat((ulong)0, 9).ToArray();
foreach (var number in file)
{
    lanternfish[int.Parse(number)]++;
}

// Start timing lanternfish
for (int i = 1; i <= NUM_DAYS; i++)
{
    var deadTimers = lanternfish[0];

    // simulate a day, skip 0 since you shouldn't go lower
    for (int j = 1; j < 9; j++)
    {
        lanternfish[j - 1] = lanternfish[j];
    }

    lanternfish[6] += deadTimers;
    lanternfish[8] = deadTimers;
}

ulong total = 0;
for (int i = 0; i < 9; i++)
{
    total += lanternfish[i];
    WriteLine($"Total so far: {total} (+{lanternfish[i]})");
}
WriteLine($"Total lanternfish after {NUM_DAYS} days: {total}");

// Debug function to print total amount of lanternfish
void PrintLanternfish()
{
    for (int i = 0; i < 9; i++)
    {
        WriteLine($"{i}: {lanternfish[i]}");
    }
}