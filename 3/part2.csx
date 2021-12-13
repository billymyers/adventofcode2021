// Read input file and convert to numbers
using System.IO;
using System.Collections.Generic;
using System.Linq;
var file = File.ReadAllLines(".\\input.txt").ToList();
var binaryLength = file[0].Length;
var values = new List<ushort>();
file.ForEach((str) =>
{
    values.Add(Convert.ToUInt16(str, 2));
});

// O2 sorting
var oxygenSet = new List<ushort>(values);
for (var i = binaryLength - 1; i >= 0; i--)
{
    // Check how many items are in the list and bail out if there is 1 item left
    if(oxygenSet.Count <= 1) break;
    var bitHash = 1 << i;
    int setBitCount = 0, unsetBitCount = 0;
    foreach(var v in oxygenSet)
    {
        if((v & bitHash) == bitHash) setBitCount++;
        else unsetBitCount++;
    }

    if(setBitCount >= unsetBitCount)
    {
        oxygenSet.RemoveAll(x => (x & bitHash) != bitHash);
    }
    else
    {
        oxygenSet.RemoveAll(x => (x & bitHash) == bitHash);
    }
}
WriteLine($"Oxygen generator rating: {oxygenSet[0]}");

var co2Set = new List<ushort>(values);
for (var i = binaryLength - 1; i >= 0; i--)
{
    // Check how many items are in the list and bail out if there is 1 item left
    if(co2Set.Count <= 1) break;
    var bitHash = 1 << i;
    int setBitCount = 0, unsetBitCount = 0;
    foreach(var v in co2Set)
    {
        if((v & bitHash) == bitHash) setBitCount++;
        else unsetBitCount++;
    }

    if(setBitCount < unsetBitCount)
    {
        co2Set.RemoveAll(x => (x & bitHash) != bitHash);
    }
    else
    {
        co2Set.RemoveAll(x => (x & bitHash) == bitHash);
    }
}
WriteLine($"CO2 scrubber rating: {co2Set[0]}");

WriteLine($"Life support rating: {co2Set[0] * oxygenSet[0]}");