using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  static void Main()
  {

    var input = File.ReadAllText("./input.txt");
    var splits = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    var endResult = splits.Select(ParseString).Sum();
    Console.WriteLine(endResult.ToString());
  }


  static int ParseString(string str)
  {
    var currentValue = 0;

    foreach (var c in str)
    {
      currentValue = IncreaseNumber(currentValue, c);
    }

    return currentValue;
  }

  static int IncreaseNumber(int currentValue, char character)
  {
    var asciiValue = (int)character;

    currentValue += asciiValue;
    currentValue *= 17;
    currentValue = currentValue % 256;

    return currentValue;
  }
}
