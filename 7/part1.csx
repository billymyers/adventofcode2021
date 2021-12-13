using System.IO;
using System.Linq;
var file = File.ReadAllText(@".\input.txt").Split(',');

var numbers = new List<int>();
file.ToList().ForEach(x => numbers.Add(int.Parse(x)));

numbers = numbers.OrderBy(x => x)
                 .ToList();

var fuelCost = new List<int>();

for (int i = 0; i < numbers.Count; i++)
{
    int number = numbers[i];
    int mode = number;
    int fuel = 0;
    foreach(var numero in numbers)
    {
        if(numero >= mode)
        {
            fuel += numero - mode;
        }
        else
        {
            fuel += mode - numero;
        }
    }
    fuelCost.Add(fuel);
}

int lowest = fuelCost.OrderBy(x => x)
                     .First();

WriteLine(lowest);