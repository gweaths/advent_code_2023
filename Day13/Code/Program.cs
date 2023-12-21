using System;
using System.IO;
using System.Linq;


static int FindMirror_Part1(string[] grid)
{
  for (int r = 1; r < grid.Length; r++)
  {
    string[] above = grid.Take(r).Reverse().ToArray();
    string[] below = grid.Skip(r).ToArray();

    above = above.Take(below.Length).ToArray();
    below = below.Take(above.Length).ToArray();

    if (above.SequenceEqual(below))
    {
      return r;
    }
  }
  return 0;
}

static int FindMirror_Part2(string[] grid)
{
  for (int currentRow = 1; currentRow < grid.Length; currentRow++)
  {
    // Split the grid into above and below the current row
    var aboveRows = grid.Take(currentRow).Reverse().ToArray();
    var belowRows = grid.Skip(currentRow).ToArray();

    // Check for symmetry by comparing corresponding elements in above and below
    int differencesCount = aboveRows
        .Zip(belowRows, CountDifferences)
        .Sum();

    if (differencesCount == 1)
    {
      return currentRow;
    }
  }

  return 0;
}

// Count the number of differences between two strings
static int CountDifferences(string str1, string str2)
{
  return str1.Zip(str2, (a, b) => a == b ? 0 : 1).Sum();
}

static void Part1()
{
  int total = 0;
  string[] lines = File.ReadAllText("./input.txt").Split("\n\n");
  foreach (string block in lines)
  {
    string[] grid = block.Split("\n");
    int row = FindMirror_Part1(grid);

    total += row * 100;


    /** create  rows from the columns so can be passed through FindMirror method
    - first select all the character and their indexes for each row
    - group by character index (i.e all index 0, 1, 2 to form columns)
    - create new strings (lines) of the characters */

    var columns = grid.SelectMany(s => s.Select((character, index) => new { character, index }))
              .GroupBy(sequence => sequence.index)
              .Select(group => new string(group.Select(x => x.character).ToArray()))
              .ToArray();

    int col = FindMirror_Part1(columns);

    total += col;
  }
  Console.WriteLine("Part1: " + total);
}


static void Part2()
{
  int total = 0;
  string[] blocks = File.ReadAllText("./input.txt").Split("\n\n");
  foreach (string block in blocks)
  {
    string[] grid = block.Split("\n");
    int row = FindMirror_Part2(grid);
    total += row * 100;

    var columns = grid.SelectMany(s => s.Select((char character, int index) => new { character, index }))
      .GroupBy(sequence => sequence.index)
      .Select(g => new string(g.Select(x => x.character).ToArray()))
      .ToArray();

    int col = FindMirror_Part2(columns);
    total += col;
  }
  Console.WriteLine("Part2: " + total);
}

Part1();
Part2();