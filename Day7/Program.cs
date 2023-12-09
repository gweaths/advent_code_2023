class Program
{
  static void Main()
  {
    string input = File.ReadAllText("./puzzle.txt");
    string[] lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    Console.WriteLine(Part1(lines));
    Console.WriteLine(Part2(lines));
  }

  static long Part1(string[] lines)
  {
    var hands = lines.Select(Hand.Parse).ToArray();
    return TotalWinnings(hands);
  }

  static long Part2(string[] lines)
  {
    Hand[] hands = [.. lines.Select(s => s.Replace('J', '*')).Select(Hand.Parse)];
    return TotalWinnings(hands);
  }

  public static long TotalWinnings(Hand[] hands)
  {
    var sorted = hands.OrderBy(hand => hand.WinningHandRank)
                      .ThenBy(hand => hand.Cards, new CardArrayComparer());

    return sorted.Select((hand, index) => hand.Bid * (index + 1)).Sum();
  }

  //----------------------------------------------------------------

  #region Records
  public record Hand(Card[] Cards, long Bid)
  {

    public WinningHandRank WinningHandRank => GetWinningHandRank(Cards);

    public static Hand Parse(string input)
    {
      string[] parts = input.Split(new char[0], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
      Card[] cards = [.. parts[0].Select(Card.Parse)];
      long bid = long.Parse(parts[1]);
      return new Hand(cards, bid);
    }

    public static WinningHandRank GetWinningHandRank(Card[] cards)
    {
      var nonWildCards = cards.Where(c => c.Rank > 1); // gets all Non-Joker cards
      int count = nonWildCards.Count();

      // no substitutions needed
      if (count == 5) { return GetTypeOfHand(cards); }
      int wildCards = 5 - count;

      // gets all the card values -> returns this card for however many wildcards need to be substituted
      // e.g 2 wiildcards -> will be 2 x A, 2 x K, 2 x Q and so on.
      // combine the existing + wildcard subs 
      // check the best by doing Max. 

      IEnumerable<Card[]> substitutions = Card.All.Select(c => Enumerable.Repeat(c, wildCards).ToArray());

      WinningHandRank best = WinningHandRank.HighCard;
      foreach (Card[] substitution in substitutions)
      {
        Card[] p = [.. nonWildCards, .. substitution];
        WinningHandRank current = GetTypeOfHand(p);
        best = (WinningHandRank)Math.Max((int)best, (int)current);
      }
      return best;
    }

    private static WinningHandRank GetTypeOfHand(Card[] cards)
    {
      int[] counts = [..
            cards
                .GroupBy(c => c)
                .Select(g => g.Count())
                .OrderDescending()
      ];
      return counts switch
      {
      [5] => WinningHandRank.FiveOfAKind,
      [4, 1] => WinningHandRank.FourOfAKind,
      [3, 2] => WinningHandRank.FullHouse,
      [3, 1, 1] => WinningHandRank.ThreeOfAKind,
      [2, 2, 1] => WinningHandRank.TwoPair,
      [2, 1, 1, 1] => WinningHandRank.OnePair,
        _ => WinningHandRank.HighCard,
      };
    }
  }

  public record Card(long Rank, char mappedCharacter)
  {
    public static Card[] All { get; } =
    [
        new Card(2, '2'),
        new Card(3, '3'),
        new Card(4, '4'),
        new Card(5, '5'),
        new Card(6, '6'),
        new Card(7, '7'),
        new Card(8, '8'),
        new Card(9, '9'),
        new Card(10, 'T'),
        new Card(12, 'Q'),
        new Card(13, 'K'),
        new Card(14, 'A'),
    ];

    public static Card Parse(char ch)
    {
      int rank = ch switch
      {
        '*' => 1,
        '2' => 2,
        '3' => 3,
        '4' => 4,
        '5' => 5,
        '6' => 6,
        '7' => 7,
        '8' => 8,
        '9' => 9,
        'T' => 10,
        'J' => 11,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => throw new InvalidOperationException($"Invalid card rank: '{ch}'")
      };
      return new Card(rank, ch);
    }
  }

  public enum WinningHandRank
  {
    HighCard = 0,
    OnePair = 1,
    TwoPair = 2,
    ThreeOfAKind = 3,
    FullHouse = 4,
    FourOfAKind = 5,
    FiveOfAKind = 6,
  }

  public class CardArrayComparer : IComparer<Card[]>
  {
    public int Compare(Card[] x, Card[] y)
    {
      for (int i = 0; i < x.Length; i++)
      {
        int compareResult = x[i].Rank.CompareTo(y[i].Rank);
        if (compareResult != 0)
        {
          return compareResult;
        }
      }

      return 0; // Arrays are equal
    }
  }

}

#endregion

