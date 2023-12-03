using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day_2;

record GameSet(int green, int red, int blue)
{

  public static List<GameSet> ParseSets(string setInfo)
  {
    string[] sets = setInfo.Split(';', StringSplitOptions.TrimEntries);
    return sets.Select(ParseSet).ToList();
  }

  public static GameSet ParseSet(string line)
  {
    var parts = line.Split(',', StringSplitOptions.TrimEntries);
    var regEx = new Regex(@"(?<id>\d+) (?<color>red|green|blue)", RegexOptions.IgnoreCase);
    Dictionary<string, int> colors = new(){
     {"red",0},
     {"blue",0},
     {"green",0}
    };

    foreach (var part in parts)
    {
      Match game = regEx.Match(part);
      Group id = game.Groups["id"];
      Group color = game.Groups["color"];

      colors[color.Value] = int.Parse(id.Value);
    }
    return new GameSet(colors["green"], colors["red"], colors["blue"]);
  }
}

record Game(int gameId, List<GameSet> sets)
{

  public static Game Parse(string line)
  {
    var parts = line.Split(':', StringSplitOptions.TrimEntries);
    string pattern = @"\d+";
    var id = Regex.Match(parts[0], pattern).Value;

    return new Game(int.Parse(id), GameSet.ParseSets(parts[1]));
  }

}
