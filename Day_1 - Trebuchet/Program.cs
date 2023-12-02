using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;

const string FILE_PATH = "./final_input.txt";

const string DIGITS_PATTERN = @"\d";
const string DIGIT_REPLACEMENT_PATTERN = @"(?<=\d)";
StringBuilder builder = new StringBuilder();

string OnlyNumerics(string strInput)
{
  builder.Clear();
  foreach (char ch in strInput)
  {
    if (char.IsDigit(ch))
    {
      builder.Append(ch);
    }
  }
  return builder.ToString();
}

Dictionary<string, string> translations = new(){
      {"one", "one1one"},
      {"two", "two2two"},
      {"three", "three3three"},
      {"four", "four4four"},
      {"five", "five5five"},
      {"six", "six6six"},
      {"seven", "seven7seven"},
      {"eight", "eight8eight"},
      {"nine", "nine9nine"}
    };

string OnlyNumerics(string strInput)
{
  builder.Clear();
  foreach (char ch in strInput)
  {
    if (char.IsDigit(ch))
    {
      builder.Append(ch);
    }
  }
  return builder.ToString();
}

string ReplaceNumbers(string original)
{
  return Regex.Replace(original, @"(?<=\d)", m => translations[m.Value]);
}

int GetFirstAndLast(string str)
{
  return int.Parse(str[0].ToString()) * 10 + int.Parse(str[^1].ToString());
}

int Challenge_1()
{
  return File.ReadAllLines(FILE_PATH)
      .Select(OnlyNumerics)
      .Select(GetFirstAndLast)
      .Sum();
}

int Challenge_2()
{
  return File.ReadAllLines(FILE_PATH)
      .Select(ReplaceNumbers)
      .Select(OnlyNumerics)
      .Select(GetFirstAndLast)
      .Sum();
}

Console.WriteLine("Part 1:" + Challenge_1().ToString());
Console.WriteLine("Part 2:" + Challenge_2().ToString());


