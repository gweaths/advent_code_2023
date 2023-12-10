using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Implementation;

public class Day9
{
  public static long Part1(string input)
  {
    var result = input
        .Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(ParseLine)
        .Select(Extrapolate)
        .Select(lastElement => lastElement[^1])
        .Sum();

    return result;
  }

  public static long Part2(string input)
  {
    var result = input
        .Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(ParseLine)
        .Select(Extrapolate)
        .Select(firstElement => firstElement[0])
        .Sum();

    return result;
  }

  public static List<long> ParseLine(string line) =>
            line.Split(new char[0], StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(long.Parse)
                .ToList();


  // this method will handle the backwards and forwards 
  // (we don't need the backwards for Part 1 but doesn't affect so all good)
  public static List<long> Extrapolate(List<long> sequence)
  {
    if (sequence.All(x => x == 0)) // Check for constant sequence
    {
      sequence.Insert(0, 0); // Insert 0 at the beginning
      sequence.Add(0); // Insert 0 at the end
      return sequence;
    }

    var nextSequence = sequence.Zip(sequence.Skip(1), (first, second) => second - first); // Calculate element differences

    // Convert nextSequence to a List<long> for indexing
    var nextSequenceList = nextSequence.ToList();

    Extrapolate(nextSequenceList); // Dreaded - Recurse to extrapolate the next sequence

    // Extract first and last differences from the extrapolated sequence
    // Remember these lines won't be hit until all Extrapolate methods have executed due to recursion
    var firstDifference = nextSequenceList[0];
    var lastDifference = nextSequenceList[^1];

    // Add first and last differences to the original sequence
    sequence.Insert(0, sequence[0] - firstDifference);
    sequence.Add(sequence[^1] + lastDifference);

    return sequence;
  }

}
