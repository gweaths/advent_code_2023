﻿using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
  static void Main()
  {
    string filePath = Path.Combine(".", "input.txt");
    string[] data = File.ReadAllText(filePath).Split("\n\n"); // Separate chunks

    // Seeds: 79 14 55 13
    var seeds = data[0].Split(": ")[1].Split(" ").Select(long.Parse).ToList();

    // Parse all the data sets (mappings)
    var dataMappingSets = data[1..].Select(DataMappingSet.Parse).ToList();

    Part1(seeds, dataMappingSets);
  }

  static void Part1(List<long> seeds, List<DataMappingSet> dataMappingSets)
  {
    var locations = seeds.Select(seed =>
    {
      long transformed = seed;
      foreach (var set in dataMappingSets)
      {
        transformed = set.Transform(transformed);
      }
      return transformed;
    }).ToList();

    long min = locations.Min();

    Console.WriteLine($"Part1: {min}");
  }
}

// Each Chunk of Data (e.g Seed -> Soil, Fertilizer -> Water, etc)
record DataMappingSet(string Label, List<DataMapping> Records)
{
  public static DataMappingSet Parse(string data)
  {
    var split = data.Split("\n");
    return new DataMappingSet(split[0], split[1..].Select(DataMapping.Parse).ToList());
  }

  public override string ToString() => $"DMS ({Label}, \n{string.Join("\n", Records)})";


  public long Transform(long source)
  {
    return Records.FirstOrDefault(mapping => mapping.IsInRange(source))?.Transform(source) ?? source;
  }
}

public record DataMapping(long DestinationStart, long SourceStart, long RangeLength)
{
  public bool IsInRange(long source) => source >= SourceStart && source < SourceStart + RangeLength;
  public long Transform(long source) => source + (DestinationStart - SourceStart);

  public static DataMapping Parse(string data)
  {
    // 50 98 2
    var parts = data.Split(" ").Select(long.Parse).ToArray();
    return new DataMapping(parts[0], parts[1], parts[2]);
  }
}
