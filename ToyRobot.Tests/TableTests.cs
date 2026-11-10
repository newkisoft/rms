using ToyRobot.Core;

namespace ToyRobot.Tests;

public sealed class TableTests
{
    private readonly Table table = new();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(4, 0)]
    [InlineData(0, 4)]
    [InlineData(4, 4)]
    [InlineData(2, 2)]
    [InlineData(1, 3)]
    public void AC01_and_AC10_coordinates_from_zero_through_four_are_on_the_table(int x, int y)
    {
        Assert.True(table.Contains(new Coordinate(x, y)));
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(-1, -1)]
    [InlineData(5, 0)]
    [InlineData(0, 5)]
    [InlineData(5, 5)]
    [InlineData(6, 2)]
    [InlineData(2, 6)]
    public void AC08_and_AC11_coordinates_outside_zero_through_four_are_not_on_the_table(int x, int y)
    {
        Assert.False(table.Contains(new Coordinate(x, y)));
    }

    [Fact]
    public void Table_has_the_required_fixed_five_by_five_size()
    {
        Assert.Equal(5, Table.Size);
    }
}
