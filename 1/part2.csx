using System.IO;
using System.Collections.Generic;
var file = File.ReadAllLines(".\\input.txt");
var count = 0;
var rollingTotals = new List<int>();

for (int i = 0; i < file.Length - 2; i++)
{
    rollingTotals.Add(int.Parse(file[i]) + int.Parse(file[i + 1]) + int.Parse(file[i + 2]));
}

for (int i = 1; i < rollingTotals.Count(); i++)
{
    if(rollingTotals[i] > rollingTotals[i - 1]) count++;
}

WriteLine(count);