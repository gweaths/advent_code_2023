using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
  static void Main()
  {
    SolvePuzzle("puzzle.txt");
  }

  static void SolvePuzzle(string filePath)
  {
    var lines = File.ReadAllLines(filePath);

    char[] instructions = lines[0].ToCharArray();
    var nodes = lines.Skip(2).Select(NextStep.Parse).ToList();

    Dictionary<string, NextStep> mappings = nodes.ToDictionary(node => node.Key, node => node.Step);

    string key = "AAA"; // defined starting point.
    long currentIndex = 0;
    long stepsTaken = 0;

    while (key != "ZZZ")
    {
      key = instructions[currentIndex] == 'L' ? mappings[key].Left : mappings[key].Right;
      stepsTaken++;
      currentIndex = (currentIndex + 1) % instructions.Length;
    }

    Console.WriteLine(stepsTaken);
  }

  public record NextStep(string Left, string Right)
  {
    private static readonly Regex regex = new Regex(@"(?<key>\w{3}) = \((?<left>\w{3}), (?<right>\w{3})\)");

    public static (string Key, NextStep Step) Parse(string line)
    {
      Match match = regex.Match(line);

      var key = match.Groups["key"].Value;
      var left = match.Groups["left"].Value;
      var right = match.Groups["right"].Value;

      return (key, new NextStep(left, right));
    }
  }
}
