using System.IO;
using System.Collections.Generic;
using System.Linq;
var file = File.ReadAllLines(@".\sample.txt");
var totalOutput = 0;

foreach (var line in file)
{
    var io = line.Split('|');
    var input = io[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);

    SegmentDecoder decoder = new();
    string decodedSegments = string.Empty;

    // sort our inputs
    var sortedInput = input.OrderByDescending(x => x.Length).ToList();
    for (int i = 0; i < sortedInput.Count; i++)
    {
        sortedInput[i] = new string(sortedInput[i].OrderBy(c => c).ToArray());
        WriteLine(sortedInput[i]);
    }

    // The last two inputs are guaranteed to be 7 and 1, so we can get the top character
    string seven = sortedInput[^2];
    string one = sortedInput[^1];

    var topResult = from c in seven
                    where c != one[0] && c != one[1]
                    select c;

    decoder.Top = topResult.First();
    WriteLine($"Top segment is {decoder.Top}");

    decodedSegments += decoder.Top;

    // Next, since we have the top, let's look for the middle segment (should be in every number except 0 and 1)
    string charsInRunning = "abcdefg";
    charsInRunning.Replace(decoder.Top.ToString(), string.Empty);
    foreach(var data in sortedInput)
    {
        
    }

    break;
}

WriteLine($"Output: {totalOutput}");

struct SegmentDecoder
{
    public char Top, TopLeft, TopRight, Middle, Bottom, BottomLeft, BottomRight;

    public int GetValue(char[] segments)
    {
        switch(segments.Length)
        {
            case 2:
                return 1;
            case 3:
                return 7;
            case 4:
                return 4;
            case 5:
                if(IsTwo(segments)) return 2;
                else if (IsThree(segments)) return 3;
                else return 5;
            case 6:
                if(IsSix(segments)) return 6;
                else if (IsNine(segments)) return 9;
                else return 0;
            default:
                return 8;
        }
    }
    private bool IsTwo(char[] segments)
    {
        if(segments.Length != 5) return false;
        bool t = false;
        bool tr = false;
        bool m = false;
        bool bl = false;
        bool b = false;
        foreach(var c in segments)
        {
            if(Top == c && !t)
            {
                t = true;
                continue;
            }

            if(TopRight == c && !tr)
            {
                tr = true;
                continue;
            }

            if(Middle == c && !m)
            {
                m = true;
                continue;
            }

            if(BottomLeft == c && !bl)
            {
                bl = true;
                continue;
            }

            if(Bottom == c && !b)
            {
                b = true;
                continue;
            }

            return false;
        }

        return true;
    }
    private bool IsThree(char[] segments)
    {
        if(segments.Length != 5) return false;
        bool t = false;
        bool tr = false;
        bool m = false;
        bool br = false;
        bool b = false;
        foreach(var c in segments)
        {
            if(Top == c && !t)
            {
                t = true;
                continue;
            }

            if(TopRight == c && !tr)
            {
                tr = true;
                continue;
            }

            if(Middle == c && !m)
            {
                m = true;
                continue;
            }

            if(BottomRight == c && !br)
            {
                br = true;
                continue;
            }

            if(Bottom == c && !b)
            {
                b = true;
                continue;
            }

            return false;
        }

        return true;
    }
    private bool IsSix(char[] segments)
    {
        if(segments.Length != 6) return false;
        bool t = false;
        bool tl = false;
        bool m = false;
        bool bl = false;
        bool br = false;
        bool b = false;
        foreach(var c in segments)
        {
            if(Top == c && !t)
            {
                t = true;
                continue;
            }

            if(TopLeft == c && !tl)
            {
                tl = true;
                continue;
            }

            if(Middle == c && !m)
            {
                m = true;
                continue;
            }

            if(BottomLeft == c && !bl)
            {
                bl = true;
                continue;
            }

            if(BottomRight == c && !br)
            {
                br = true;
                continue;
            }

            if(Bottom == c && !b)
            {
                b = true;
                continue;
            }

            return false;
        }

        return true;
    }
    private bool IsNine(char[] segments)
    {
        if(segments.Length != 6) return false;
        bool t = false;
        bool tl = false;
        bool m = false;
        bool tr = false;
        bool br = false;
        bool b = false;
        foreach(var c in segments)
        {
            if(Top == c && !t)
            {
                t = true;
                continue;
            }

            if(TopLeft == c && !tl)
            {
                tl = true;
                continue;
            }

            if(Middle == c && !m)
            {
                m = true;
                continue;
            }

            if(TopRight == c && !tr)
            {
                tr = true;
                continue;
            }

            if(BottomRight == c && !br)
            {
                br = true;
                continue;
            }

            if(Bottom == c && !b)
            {
                b = true;
                continue;
            }

            return false;
        }

        return true;
    }
}