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

int Challenge_Part1(Game[] games)
{
  return games.Where(isValidGame).Select(x => x.gameId).Sum();
}

string[] lines = File.ReadAllLines("./test.txt");
Game[] games = lines.Select(Game.Parse).ToArray();
Console.WriteLine(Challenge_Part1(games));











