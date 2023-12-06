using System.Text.RegularExpressions;

string[] data = File.ReadAllText("./puzzle.txt").Split("\n");
List<Boat> boats = Boat.ParseMultipleBoats(data);
Boat boat = Boat.ParseBoat(data);

long Part1() => boats
  .Select(boat => boat.FindPossibilities())
  .Aggregate((x, y) => x * y);

long Part2() => boat.FindPossibilities();


Console.WriteLine($"Part 1: {Part1()}");
Console.WriteLine($"Part 2: {Part2()}");


record Boat(long Time, long Distance)
{
  bool CanBeat(long boost)
  {
    var timeRemaining = Time - boost;
    return (timeRemaining * boost) > Distance;
  }

  public long FindPossibilities() =>
    Enumerable.Range(1, (int)Distance)
      .Where(x => CanBeat(x) == true)
      .Count();

  public static Boat ParseBoat(string[] lines)
  {
    string time = "";
    string distance = "";
    if (lines.Length >= 2)
    {

      string times = lines[0].Trim();
      string distances = lines[1].Trim();

      time = string.Join("", Regex
             .Matches(times.Trim(), @"\d+")
             .Select(match => match.Value)
           );

      distance = string.Join("", Regex
              .Matches(distances.Trim(), @"\d+")
              .Select(match => match.Value)
            );
    }
    return new Boat(long.Parse(time), long.Parse(distance));
  }

  public static List<Boat> ParseMultipleBoats(string[] lines)
  {
    List<Boat> boats = new();
    if (lines.Length >= 2)
    {
      string[] times = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
      string[] distance = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

      for (int i = 1; i < times.Length; i++)
      {
        boats.Add(new Boat(long.Parse(times[i]), long.Parse(distance[i])));
      }
    }
    return boats;
  }
}



