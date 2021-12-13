using System.IO;
using System.Collections.Generic;
using System.Linq;

// Load file, numbers to call
var file = File.ReadAllLines(@".\input.txt");
var numbersToCall = new List<int>();
file[0].Split(',').ToList().ForEach(x => numbersToCall.Add(int.Parse(x)));
// Load boards
var boards = new List<BingoBoard>();
var boardHasWon = false;
var boardsTemp = file.Skip(1);
var boardStrings = from line in boardsTemp
             where !string.IsNullOrEmpty(line)
             select line;

for (int i = 0; i < boardStrings.Count() / 5; i++)
{
    var board = boardStrings.Skip(i * 5).Take(5).ToArray();
    boards.Add(new(board));
}

for (int i = 0; i < numbersToCall.Count; i++)
{
    var hasWon = false;
    var numbersToTest = numbersToCall.Take(i + 1).ToList();
    foreach (BingoBoard board in boards)
    {
        if (board.HasWon(numbersToTest))
        {
            WriteLine(board.CalculateScore(numbersToTest) * numbersToTest.Last());
            hasWon = true;
            break;
        }
    }
    if(hasWon) break;
}

// Struct for our bingo board
struct BingoBoard
{
    readonly int[,] board = new int[5, 5];
    public BingoBoard(string[] input)
    {
        board = new int[5, 5];
        // Each line in the array contains five numbers
        for (var i = 0; i < 5; i++)
        {
            var numbers = input[i].Split(' ').ToList();
            numbers.RemoveAll(x => string.IsNullOrEmpty(x));
            for (var j = 0; j < 5; j++)
            {
                board[j, i] = int.Parse(numbers[j].Trim());
            }
        }
    }

    public bool HasWon(List<int> calledNumbers)
    {
        if(calledNumbers.Count < 5) return false;
        for (int i = 0; i < 5; i++)
        {
            var matchRow = true;
            var matchColumn = true;
            
            // Check rows
            for (int j = 0; j < 5; j++)
            {
                if(!calledNumbers.Contains(board[i, j]))
                {
                    matchRow = false;
                    break;
                }
            }

            // Check columns
            for (int j = 0; j < 5; j++)
            {
                if(!calledNumbers.Contains(board[j, i]))
                {
                    matchColumn = false;
                    break;
                }
            }
            if(matchRow || matchColumn) return true;
        }
        return false;
    }

    public int CalculateScore(List<int> calledNumbers)
    {
        int result = 0;
        foreach(var number in board)
        {
            if(calledNumbers.Contains(number)) continue;
            result += number;
        }

        return result;
    }
}