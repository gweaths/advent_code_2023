var samplePath = "./sample.txt";
var sample2Path = "./sample2.txt";
var inputPath = "./input.txt";

var lines = File.ReadAllLines(sample2Path);
Part1();
Part2();

void Part1()
{
  Card[] cards = lines.Select(ParseLine).ToArray();
  int accumulativeTotal = cards.Sum(card =>
  {
    int count = card.WinningNumbers.Intersect(card.MyNumbers).Count();
    if (count > 0)
    {
      // double the 3rd numbers onwards
      int points = Enumerable.Range(0, count).Sum(i => i < 2 ? 1 : 2 << (i - 2));
      return points;
    }
    return 0;
  });

  Console.WriteLine($"Part1: {accumulativeTotal.ToString()}");
}

void Part2()
{
  var input = File.ReadAllLines(inputPath);
  // initialize the array - each card has at least one.
  int[] cardCount = Enumerable.Repeat(1, input.Length).ToArray();

  // loop over each card
  for (int cardId = 0; cardId < input.Length; cardId++)
  {
    string? line = input[cardId];
    var card = ParseLine(line);

    // collect number of winning numbers as before.
    var matchCount = card.WinningNumbers.Intersect(card.MyNumbers).Count();

    // for the number of wins, update any cards with extras.
    for (int i = 0; i < matchCount; i++)
    {
      cardCount[cardId + 1 + i] += cardCount[cardId];
    }
  }

  Console.WriteLine(cardCount.Sum());
}


Card ParseLine(string line)
{
  var parts = line.Split(':');
  var numbers = parts[1].Split('|');
  var winningNumbers = ExtractNumbers(numbers[0]);
  var myNumbers = ExtractNumbers(numbers[1]);

  return new Card(winningNumbers, myNumbers);
}

int[] ExtractNumbers(string input)
{
  return input.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
       .Select(int.Parse)
       .ToArray();
}
record Card(int[] WinningNumbers, int[] MyNumbers);


