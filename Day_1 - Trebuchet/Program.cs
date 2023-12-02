using System.Runtime.ExceptionServices;
using System.Text;
using System.Text.RegularExpressions;

string FILE_PATH = "./final_input.txt";

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
  StringBuilder builder = new();
  foreach (char ch in strInput)
  {
    if (char.IsDigit(ch)) builder.Append(ch);
  }
  return builder.ToString();
}

string ReplaceNumbers(string original)
{
  foreach (var key in translations.Keys)
  {
    original = original.Replace(key, translations[key].ToString());
  }
  return original;
}

int GetFirstAndLast(string str)
{
  int first = int.Parse(str[0].ToString());
  int last = int.Parse(str[^1].ToString());

  return first * 10 + last; // this converts first digit to a 10th digit e.g 1 = 10, 2 = 20
}

int Challenge_1()
{
  string[] lines = File.ReadAllLines(FILE_PATH);
  return lines.Select(OnlyNumerics).Select(GetFirstAndLast).Sum();
}

int Challenge_2()
{
  string[] lines = File.ReadAllLines(FILE_PATH);
  string[] replaced = lines.Select(ReplaceNumbers).ToArray();
  return replaced.Select(OnlyNumerics).Select(GetFirstAndLast).Sum();
}

Console.WriteLine("Part 1:" + Challenge_1().ToString());
Console.WriteLine("Part 2:" + Challenge_2().ToString());
