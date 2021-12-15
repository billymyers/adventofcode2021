using System.IO;
using System.Collections.Generic;
var file = File.ReadAllLines(@".\input.txt");

Dictionary<char, int> scores = new()
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 },
};

const string OpeningCharacters = "([{<";
const string ClosingCharacters = ")]}>";
int score = 0;

foreach(var line in file)
{
    Stack<char> syntaxStack = new();
    foreach(var c in line)
    {
        if(OpeningCharacters.Contains(c))
        {
            // Opening character pushes to the stack
            syntaxStack.Push(c);
        }
        else
        {
            // Closing character checks against the stack
            if(OpeningCharacters.IndexOf(syntaxStack.Peek()) == ClosingCharacters.IndexOf(c))
            {
                syntaxStack.Pop();
            }
            else
            {
                // we made a fucky wucky
                score += scores[c];
                break;
            }
        }
    }

    if(syntaxStack.Count != 0)
    {
        WriteLine("Line incomplete");
    }
}

WriteLine($"Score: {score}");