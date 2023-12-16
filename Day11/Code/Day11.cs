using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Code;

public static class Day11
{

  public static long Solve(string input, int scale)
  {

    string[] universe = input.Split(Environment.NewLine);
    HashSet<int> emptyRows = [..universe.Select((string row, int index)=>(row, index))
      .Where(entry => entry.row.All(c => c == '.'))
      .Select(entry => entry.index)];

    HashSet<int> emptyColumns = [];

    for (int col = 0; col < universe[0].Length; col++)
    {
      bool empty = true;
      for (int row = 0; row < universe.Length; row++)
      {
        if (universe[row][col] != '.')
        {
          empty = false;
          break;
        }
      }
      if (empty) emptyColumns.Add(col);
    }

    List<Position> galaxyLocations = ExpandFindGalaxy(universe, scale, emptyRows, emptyColumns).ToList();
    List<(Position first, Position second)> pairs = new();

    for (int x = 0; x < galaxyLocations.Count; x++)
    {
      for (int y = x + 1; y < galaxyLocations.Count; y++)
      {
        pairs.Add((galaxyLocations[x], galaxyLocations[y]));
      }
    }

    return pairs
      .Select(x => ManhattanDistance(x.first.row, x.second.row, x.first.col, x.second.col))
      .Sum();
  }

  public static long ManhattanDistance(int x1, int x2, int y1, int y2)
  {
    return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
  }

  public static IEnumerable<Position> ExpandFindGalaxy(string[] universe, int scale, HashSet<int> emptyRows, HashSet<int> emptyColumns)
  {
    List<Position> positions = new List<Position>();

    for (int row = 0; row < universe.Length; row++)
    {
      if (emptyRows.Contains(row))
      {
        continue;
      }

      for (int col = 0; col < universe[row].Length; col++)
      {
        if (emptyColumns.Contains(col))
        {
          continue;
        }

        if (universe[row][col] == '#')
        {
          int adjustedRow = row + (scale - 1) * emptyRows.TakeWhile(r => r < row).Count();
          int adjustedCol = col + (scale - 1) * emptyColumns.TakeWhile(c => c < col).Count();
          positions.Add(new Position(adjustedRow, adjustedCol));
        }
      }
    }

    return positions;
  }
}

public record Position(int row, int col);