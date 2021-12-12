// Load input file
using System.IO;
var file = File.ReadAllLines(".\\input.txt");
var totalInput = file.Length;
// Prepare gamma and epsilon vars
var length = file[0].Length;
ushort gamma, epsilon = 0;
// Count bits in each array
int[] counts = new int[length];

foreach(var line in file)
{
    ushort value = Convert.ToUInt16(line, 2);
    var i = 11;

    while (i >= 0)
    {
        var test = value & 0x1;
        if(test != 0)
        {
            counts[i]++;
        }
        i--;
        value >>= 1;
    }
}

for (int i = 0; i < length; i++)
{
    if (counts[i] > totalInput / 2)
    {
        gamma |= 1;
        epsilon |= 0;
    }
    else
    {
        gamma |= 0;
        epsilon |= 1;
    }
    if(i == length - 1) break;
    gamma <<= 1;
    epsilon <<= 1;
}

WriteLine($"Gamma: {gamma}, Epsilon: {epsilon}\nPower consumption: {gamma * epsilon}")