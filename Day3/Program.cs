using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;


string filePath = "./test.txt"; // Replace this with the path to your file
string[] inputLines = File.ReadAllLines(filePath).Select(x => string.IsNullOrWhiteSpace(x) ? x : x.Trim()).ToArray();

Dictionary<Coordinates, int> numbers = BuildCoordinates(c => char.IsDigit(c) && c != '.');
Dictionary<Coordinates, char> specialCharacters = BuildCoordinates(c => !char.IsDigit(c) && c != '.');

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

Dictionary<Coordinates, T> BuildCoordinates<T>(Func<char, bool> condition)
{
  Dictionary<Coordinates, T> result = new();
  for (int row = 0; row < inputLines.Length; row++)
  {
    var line = inputLines[row];
    for (int col = 0; col < line.Length; col++)
    {
      char ch = line[col];
      if (condition(ch))
      {
        Coordinates coordinates = new(row, col);
        if (typeof(T) == typeof(int))
        {
          StringBuilder b = new();
          while (col < line.Length && char.IsDigit(line[col]))
          {
            var value = line[col];
            b.Append(value);
            col++;
          }
          result[coordinates] = int.Parse(b.ToString());
        }
        else if (typeof(T) == typeof(char))
        {
          result[coordinates] = ch;
        }
      }
    }
  }
  return result;
}

IEnumerable<Coordinates> GetNumberCoordinates(Coordinates coordinate)
{
  if (!numbers.TryGetValue(coordinate, out _))
  {
    return Enumerable.Empty<Coordinates>();
  }

  int numberOfDigits = numbers[coordinate].ToString().Length;
  return Enumerable.Range(coordinate.Col, numberOfDigits).Select(col => new Coordinates(coordinate.Row, col));
}

bool NeigbourIsSpecial(Coordinates coordinate)
{
  Coordinates[] directions = {
            new Coordinates(coordinate.Row, coordinate.Col - 1), //left
            new Coordinates(coordinate.Row, coordinate.Col + 1), // right
            new Coordinates(coordinate.Row - 1, coordinate.Col), // up
            new Coordinates(coordinate.Row + 1, coordinate.Col), // down

            new Coordinates(coordinate.Row - 1, coordinate.Col - 1), // diagonal-up-left
            new Coordinates(coordinate.Row - 1, coordinate.Col + 1), // diagonal-up-right
            new Coordinates(coordinate.Row + 1, coordinate.Col - 1), // diagonal-down-left
            new Coordinates(coordinate.Row + 1, coordinate.Col + 1), // diagonal-right-down
        };

  return directions.Any(c => specialCharacters.TryGetValue(c, out _));
}

record struct Coordinates(int Row, int Col);
