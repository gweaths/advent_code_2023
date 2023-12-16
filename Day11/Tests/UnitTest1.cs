using Code;
using Shouldly;

namespace Tests;

public class GalaxyTests
{
    [Fact]
    public void test_part1_sample()
    {
        var input = """
...#......
.......#..
#.........
..........
......#...
.#........
.........#
..........
.......#..
#...#.....
""".Trim().ReplaceLineEndings();

        var result = Day11.Solve(input, 2);
        result.ShouldBe(374);

    }
}