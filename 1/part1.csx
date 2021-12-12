using System.IO;

var file = File.ReadAllLines(".\\input.txt");
var count = 0;

for (int i = 1; i < file.Length; i++)
{
    if(int.Parse(file[i]) > int.Parse(file[i - 1])) count++;
}

WriteLine(count);