using System.Text;



string filePath = "./test.txt"; // Replace this with the path to your file
string[] inputLines = File.ReadAllLines(filePath).Select(x => x.Trim()).ToArray();

Dictionary<Coordinates, int> numbers = BuildNumbers();
Dictionary<Coordinates, char> specialCharacters = BuildSpecialCharacters();

Part1();

Dictionary<Coordinates, int> BuildNumbers()
{
  Dictionary<Coordinates, int> numbers = new();
  for (int row = 0; row < inputLines.Length; row++)
  {
    var line = inputLines[row];
    for (int col = 0; col < line.Length; col++)
    {
      // find the numbers and store against their starting coordinates
      if (char.IsDigit(line[col]) && line[col] is not '.')
      {
        Coordinates coordinates = new(row, col);
        StringBuilder b = new();
        while (col < line.Length && char.IsDigit(line[col]))
        {
          var value = line[col];
          b.Append(value);
          col++;
        }

        numbers[coordinates] = int.Parse(b.ToString());
      }
    }
  }
  return numbers;
}

Dictionary<Coordinates, char> BuildSpecialCharacters()
{
  Dictionary<Coordinates, char> spCharacters = new();
  for (int row = 0; row < inputLines.Length; row++)
  {
    var line = inputLines[row];
    for (int col = 0; col < line.Length; col++)
    {
      char ch = line[col];
      if (char.IsDigit(ch) || ch == '.')
      {
        continue;
      }

      Coordinates coordinates = new(row, col);
      spCharacters[coordinates] = ch;
    }
  }
  return spCharacters;
}

IEnumerable<Coordinates> GetNumberCoordinates(Coordinates coordinate)
{
  if (!numbers.ContainsKey(coordinate))
  { return new Coordinates[0]; }

  int numberOfDigits = numbers[coordinate].ToString().Length;
  return Enumerable.Range(coordinate.Col, numberOfDigits).Select(col => new Coordinates(coordinate.Row, col));
}



bool NeigbourIsSpecial(Coordinates coordinate)
{
  Coordinates[] directions = {
    new Coordinates(coordinate.Row, coordinate.Col - 1), //left
    new Coordinates(coordinate.Row, coordinate.Col +1), // right
    new Coordinates(coordinate.Row - 1, coordinate.Col), // up
    new Coordinates(coordinate.Row + 1, coordinate.Col), // down

    new Coordinates(coordinate.Row -1,coordinate.Col - 1), // diagonal-up-left
    new Coordinates(coordinate.Row -1, coordinate.Col +1), // diagonal-up-right
    new Coordinates(coordinate.Row +1,coordinate.Col -1), // diagonal-down-left
    new Coordinates(coordinate.Row +1,coordinate.Col + 1), // diagonal-right-down
  };

  foreach (Coordinates c in directions)
  {
    if (specialCharacters.ContainsKey(c)) { return true; }
  }
  return false;
}

void Part1()
{
  int accumulativeValue = 0;
  foreach ((Coordinates c, int number) in numbers)
  {
    IEnumerable<Coordinates> numberCoordinates = GetNumberCoordinates(c);
    if (numberCoordinates.Any(NeigbourIsSpecial))
    {
      accumulativeValue += number;
    }
  }
  Console.WriteLine("Accumulative value: " + accumulativeValue);
}

record struct Coordinates(int Row, int Col);