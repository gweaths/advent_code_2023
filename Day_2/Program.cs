using System.Text.RegularExpressions;

Dictionary<string, int> maxValues = new()
{
    { "red", 12 },
    { "green", 13 },
    { "blue", 14 }
};

bool isValidGame(Game game) => game.sets.All(isValidSet);

bool isValidSet(GameSet set) => set.red <= maxValues["red"] && set.green <= maxValues["green"] && set.blue <= maxValues["blue"];

GameSet FindMaxSets(List<GameSet> sets) =>
        new GameSet(
            red: sets.Max(s => s.red),
            green: sets.Max(s => s.green),
            blue: sets.Max(s => s.blue)
        );

int ChallengePart1(Game[] games)
{
  return games.Where(isValidGame).Select(x => x.gameId).Sum();
}

int ChallengePart2(Game[] games) =>
       games.Select(game => FindMaxSets(game.sets))
            .Select(x => x.red * x.green * x.blue)
            .Sum();

string[] lines = File.ReadAllLines("./test.txt");
Game[] games = lines.Select(Game.Parse).ToArray();


Console.WriteLine(ChallengePart1(games));
Console.WriteLine(ChallengePart2(games));


record GameSet(int green, int red, int blue)
{

  public static List<GameSet> ParseSets(string setInfo) => setInfo
    .Split(';', StringSplitOptions.TrimEntries)
    .Select(ParseSet)
    .ToList();

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









