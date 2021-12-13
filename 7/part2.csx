using System.IO;
using System.Linq;
var file = File.ReadAllText(@".\input.txt").Split(',');

var numbers = new List<int>();
file.ToList().ForEach(x => numbers.Add(int.Parse(x)));

numbers = numbers.OrderBy(x => x)
                 .ToList();

var range = Enumerable.Range(numbers[0], numbers[numbers.Count - 1]).ToArray();

var fuelCost = new List<int>();

for (int i = 0; i < range.Length; i++)
{
    int number = range[i];
    int mode = number;
    int fuel = 0;
    foreach(var numero in numbers)
    {
        var total = 0;
        if(numero >= mode)
            total = numero - mode;
        else
            total = mode - numero;

        for (int j = 0; j < total; j++)
            fuel += j + 1;
    }
    fuelCost.Add(fuel);
}

int lowest = fuelCost.OrderBy(x => x)
                     .First();

WriteLine(lowest);