using System.IO;
using System.Collections.Generic;
using System.Linq;
var file = File.ReadAllLines(@".\input.txt");
ulong totalOutput = 0;

foreach (var line in file)
{
    ulong value = 0;
    var io = line.Split('|');
    var input = io[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var output = io[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    Dictionary<string, int> decodedSequences = new();

    // sort our inputs
    var sortedInput = input.OrderByDescending(x => x.Length).ToList();
    char[] fourChars = new char[4];
    for (int i = 0; i < sortedInput.Count; i++)
    {
        sortedInput[i] = new string(sortedInput[i].OrderBy(c => c).ToArray());
        if (sortedInput[i].Length == 4)
        {
            decodedSequences.Add(sortedInput[i], 4);
            WriteLine(sortedInput[i] + " is our four");
            fourChars = sortedInput[i].ToCharArray();
        }
    }

    // The last two inputs are guaranteed to be 7 and 1, so we can get the top character
    string seven = sortedInput[^2];
    string one = sortedInput[^1];

    // Add one, seven, and eight since we already know these
    decodedSequences.Add(one, 1);
    decodedSequences.Add(seven, 7);
    decodedSequences.Add(sortedInput[0], 8);

    char topSegment = (from c in seven
                       where c != one[0] && c != one[1]
                       select c).First();

    char bottomSegment = 'h';

    //Figure out 9 by using 4 and our top segment
    foreach (var segment in sortedInput)
    {
        // 9 has six segments, so if this number doesn't have six digits, we don't care.
        if (segment.Length != 6) continue;


        bool isShitBroke = false;
        // 9 has all of the segments of 4, so let's make sure that they're all here
        foreach (char c in fourChars)
        {
            if (!segment.Contains(c)) isShitBroke = true;
        }
        // If our shit is broke then on to the next
        if (isShitBroke) continue;

        // At this point, we have 9
        decodedSequences.Add(segment, 9);

        // Let's get rid of the characters that we know
        string bottom = segment.Replace(topSegment.ToString(), string.Empty);
        foreach (char c in fourChars)
        {
            bottom = bottom.Replace(c.ToString(), string.Empty);
        }

        // Get our bottom segment for later :)
        bottomSegment = bottom[0];
        break;
    }

    // Let's find out 2, 3, 5
    // Supplementary segments of 4 include the ones that are in 4 but not 1
    // 2: does not contain all of 1 nor supplementary segments of 4
    // 3: contains all of 1 - instantly can be found
    // 5: does not contain all of 1, contains supplementary segments of 4
    var work = from code in sortedInput
               where code.Length == 5
               select code;

    var supplementarySegments = from c in fourChars
                                where one.IndexOf(c) == -1
                                select c;

    foreach (var code in work)
    {
        if (code.Contains(one.ToCharArray()[0]) && code.Contains(one.ToCharArray()[1]))
        {
            // We found three :)
            decodedSequences.Add(code, 3);
        }
        else
        {
            if (code.Contains(supplementarySegments.ToList()[1]) && code.Contains(supplementarySegments.ToList()[0]))
            {
                // We found five
                decodedSequences.Add(code, 5);
            }
            else
            {
                // Else, we found three
                decodedSequences.Add(code, 2);
            }
        }
    }

    // At this point, we are missing 6 and 0
    // Let's use one to get the last
    var remainingDigits = from code in sortedInput
                          where code.Length == 6 && !decodedSequences.ContainsKey(code)
                          select code;

    WriteLine(remainingDigits.Count());

    foreach (var code in remainingDigits)
    {
        bool wasSix = false;
        // Check against one lol
        foreach (var c in one)
        {
            if (!code.Contains(c))
            {
                // we found 6
                decodedSequences.Add(code, 6);
                wasSix = true;
                break;
            }
        }
        if (wasSix) continue;
        decodedSequences.Add(code, 0);
    }

    WriteLine(decodedSequences.Count);

    // Now let's go ahead and add this shit up
    for (int i = 0; i < output.Length; i++)
    {
        var sorted = new string(output[i].OrderBy(c => c).ToArray());
        var digitSigFig = 3 - i;
        var digitValue = decodedSequences[sorted];

        WriteLine($"Digit value = {digitValue} * 10^{digitSigFig}");

        var finalValue = digitValue;
        while (digitSigFig != 0)
        {
            finalValue *= 10;
            digitSigFig--;
        }

        value += (ulong)finalValue;
    }

    totalOutput += value;
}

WriteLine($"Output: {totalOutput}");