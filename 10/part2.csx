using System.IO;
using System.Collections.Generic;
var file = File.ReadAllLines(@".\input.txt");

Dictionary<char, ulong> scores = new()
{
    { ')', 1 },
    { ']', 2 },
    { '}', 3 },
    { '>', 4 },
};

const string OpeningCharacters = "([{<";
const string ClosingCharacters = ")]}>";

List<ulong> autocompleteLineScores = new();

foreach(var line in file)
{
    ulong score = 0;
    Stack<char> syntaxStack = new();
    var weFuckedUpSkipThisLine = false;
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
                // Ayo we fucked up, fuck this line
                weFuckedUpSkipThisLine = true;
            }
        }
    }

    if(weFuckedUpSkipThisLine) continue;

    if(syntaxStack.Count != 0)
    {
        // Line incomplete, let's fix it
        string fixThisLine = string.Empty;
        while(syntaxStack.Count != 0)
        {
            char next = syntaxStack.Pop();
            char closingChar = ClosingCharacters[OpeningCharacters.IndexOf(next)];
            score *= 5;
            score += scores[closingChar];
            fixThisLine += closingChar;
        }

        WriteLine($"Line {line} fixed with {fixThisLine} - for a score of {score} points");

        autocompleteLineScores.Add(score);
    }
}

autocompleteLineScores = autocompleteLineScores.OrderBy(x => x).ToList();
WriteLine($"Winrar: {autocompleteLineScores[autocompleteLineScores.Count / 2]}");