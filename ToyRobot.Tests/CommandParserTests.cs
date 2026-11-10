using ToyRobot.Core;

namespace ToyRobot.Tests;

public sealed class CommandParserTests
{
    private readonly CommandParser parser = new();

    [Theory]
    [InlineData("MOVE", CommandKind.Move)]
    [InlineData("LEFT", CommandKind.Left)]
    [InlineData("RIGHT", CommandKind.Right)]
    [InlineData("REPORT", CommandKind.Report)]
    public void TryParse_ReturnsCommandForSimpleCommands(string input, CommandKind expectedKind)
    {
        var parsed = parser.TryParse(input, out var command);

        Assert.True(parsed);
        Assert.Equal(expectedKind, command.Kind);
    }

    [Theory]
    [InlineData("PLACE 0,0,NORTH", 0, 0, Direction.North)]
    [InlineData("PLACE 4,3,EAST", 4, 3, Direction.East)]
    [InlineData("PLACE -1,2,SOUTH", -1, 2, Direction.South)]
    [InlineData("PLACE 5,6,WEST", 5, 6, Direction.West)]
    public void TryParse_ReturnsPlaceCommandForIntegerCoordinates(
        string input,
        int expectedX,
        int expectedY,
        Direction expectedFacing)
    {
        var parsed = parser.TryParse(input, out var command);

        Assert.True(parsed);
        Assert.Equal(CommandKind.Place, command.Kind);
        Assert.Equal(expectedX, command.X);
        Assert.Equal(expectedY, command.Y);
        Assert.Equal(expectedFacing, command.Facing);
    }

    [Fact]
    public void TryParse_IgnoresSurroundingWhitespace()
    {
        var parsed = parser.TryParse("  PLACE 1,2,EAST  ", out var command);

        Assert.True(parsed);
        Assert.Equal(new Command(CommandKind.Place, 1, 2, Direction.East), command);
    }

    [Theory]
    [InlineData("PLACE one,2,NORTH")]
    [InlineData("PLACE 1,two,NORTH")]
    [InlineData("PLACE 1,2.5,NORTH")]
    [InlineData("PLACE ,2,NORTH")]
    public void TryParse_ReturnsFalseForMalformedCoordinates(string input)
    {
        Assert.False(parser.TryParse(input, out _));
    }

    [Theory]
    [InlineData("PLACE 1,2,UP")]
    [InlineData("PLACE 1,2,")]
    [InlineData("PLACE 1,2,north")]
    public void TryParse_ReturnsFalseForInvalidDirections(string input)
    {
        Assert.False(parser.TryParse(input, out _));
    }

    [Theory]
    [InlineData("PLACE 1,2,NORTH,EXTRA")]
    [InlineData("PLACE 1,2,NORTH,")]
    [InlineData("PLACE 1,,NORTH")]
    [InlineData("PLACE 1,2,,NORTH")]
    public void TryParse_ReturnsFalseForExtraDelimiters(string input)
    {
        Assert.False(parser.TryParse(input, out _));
    }

    [Theory]
    [InlineData("PLACE")]
    [InlineData("PLACE 1,2")]
    [InlineData("PLACE 1,NORTH")]
    public void TryParse_ReturnsFalseForMissingArguments(string input)
    {
        Assert.False(parser.TryParse(input, out _));
    }

    [Theory]
    [InlineData("UNKNOWN")]
    [InlineData("MOVE FORWARD")]
    [InlineData("place 1,2,NORTH")]
    public void TryParse_ReturnsFalseForUnknownOrWrongCaseCommands(string input)
    {
        Assert.False(parser.TryParse(input, out _));
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void TryParse_ReturnsFalseForBlankInput(string input)
    {
        Assert.False(parser.TryParse(input, out _));
    }
}
