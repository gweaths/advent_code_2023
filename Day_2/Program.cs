using System.Text;
using Day_2;



Dictionary<string, int> maxValues = new()
{
    { "red", 12 },
    { "green", 13 },
    { "blue", 14 }
};

bool isValidGame(Game game)
{
  return game.sets.All(isValidSet);
}

bool isValidSet(GameSet set)
{
  return set.red <= maxValues["red"] && set.green <= maxValues["green"] && set.blue <= maxValues["blue"];

}

GameSet FindMaxSets(List<GameSet> sets)
{
  int red = 0;
  int green = 0;
  int blue = 0;
  foreach (var set in sets)
  {
    red = Math.Max(set.red, red);
    green = Math.Max(set.green, green);
    blue = Math.Max(set.blue, blue);
  }

  return new GameSet(red: red, green: green, blue: blue);
}

int Challenge_Part1(Game[] games)
{
  return games.Where(isValidGame).Select(x => x.gameId).Sum();
}

int Challenge_Part2(Game[] games)
{
  return games.Select(game => FindMaxSets(game.sets)).Select(x => x.red * x.green * x.blue).Sum();
}

string[] lines = File.ReadAllLines("./test.txt");
Game[] games = lines.Select(Game.Parse).ToArray();


Console.WriteLine(Challenge_Part1(games));
Console.WriteLine(Challenge_Part2(games));











