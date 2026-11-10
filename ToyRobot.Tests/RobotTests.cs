using ToyRobot.Core;

namespace ToyRobot.Tests;

public sealed class RobotTests
{
    [Fact]
    public void NewRobot_IsUnplaced()
    {
        var robot = new Robot();

        Assert.False(robot.IsPlaced);
    }

    [Theory]
    [InlineData(Direction.North, 2, 3)]
    [InlineData(Direction.East, 3, 2)]
    [InlineData(Direction.South, 2, 1)]
    [InlineData(Direction.West, 1, 2)]
    public void Move_AdvancesOneUnitInTheFacingDirection(Direction facing, int expectedX, int expectedY)
    {
        var robot = new Robot();
        robot.Place(new Coordinate(2, 2), facing);

        var moved = robot.Move();

        Assert.True(moved);
        Assert.Equal(new Coordinate(expectedX, expectedY), robot.Position);
        Assert.Equal(facing, robot.Facing);
    }

    [Theory]
    [InlineData(Direction.North, Direction.West)]
    [InlineData(Direction.East, Direction.North)]
    [InlineData(Direction.South, Direction.East)]
    [InlineData(Direction.West, Direction.South)]
    public void RotateLeft_ChangesFacingWithoutChangingPosition(Direction initial, Direction expected)
    {
        var robot = new Robot();
        var position = new Coordinate(2, 2);
        robot.Place(position, initial);

        robot.RotateLeft();

        Assert.Equal(position, robot.Position);
        Assert.Equal(expected, robot.Facing);
    }

    [Theory]
    [InlineData(Direction.North, Direction.East)]
    [InlineData(Direction.East, Direction.South)]
    [InlineData(Direction.South, Direction.West)]
    [InlineData(Direction.West, Direction.North)]
    public void RotateRight_ChangesFacingWithoutChangingPosition(Direction initial, Direction expected)
    {
        var robot = new Robot();
        var position = new Coordinate(2, 2);
        robot.Place(position, initial);

        robot.RotateRight();

        Assert.Equal(position, robot.Position);
        Assert.Equal(expected, robot.Facing);
    }

    [Theory]
    [InlineData(0, 0, Direction.West)]
    [InlineData(4, 0, Direction.East)]
    [InlineData(0, 4, Direction.West)]
    [InlineData(4, 4, Direction.East)]
    [InlineData(0, 0, Direction.South)]
    [InlineData(0, 4, Direction.North)]
    [InlineData(4, 0, Direction.South)]
    [InlineData(4, 4, Direction.North)]
    public void Move_ReturnsFalseAndPreservesStateAtTableBoundary(
        int x,
        int y,
        Direction facing)
    {
        var robot = new Robot();
        var position = new Coordinate(x, y);
        robot.Place(position, facing);

        var moved = robot.Move();

        Assert.False(moved);
        Assert.Equal(position, robot.Position);
        Assert.Equal(facing, robot.Facing);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, -1)]
    [InlineData(5, 0)]
    [InlineData(0, 5)]
    public void Place_ReturnsFalseAndLeavesRobotUnplacedForInvalidCoordinates(int x, int y)
    {
        var robot = new Robot();

        var placed = robot.Place(new Coordinate(x, y), Direction.North);

        Assert.False(placed);
        Assert.False(robot.IsPlaced);
    }

    [Fact]
    public void Place_ReturnsFalseAndLeavesRobotUnplacedForInvalidDirection()
    {
        var robot = new Robot();

        var placed = robot.Place(new Coordinate(2, 2), (Direction)999);

        Assert.False(placed);
        Assert.False(robot.IsPlaced);
    }

    [Fact]
    public void Place_PreservesExistingStateWhenReplacementPlacementIsInvalid()
    {
        var robot = new Robot();
        var originalPosition = new Coordinate(2, 2);
        robot.Place(originalPosition, Direction.East);

        var placed = robot.Place(new Coordinate(5, 2), Direction.North);

        Assert.False(placed);
        Assert.True(robot.IsPlaced);
        Assert.Equal(originalPosition, robot.Position);
        Assert.Equal(Direction.East, robot.Facing);
    }

    [Fact]
    public void Place_ReplacesExistingStateWhenReplacementPlacementIsValid()
    {
        var robot = new Robot();
        robot.Place(new Coordinate(2, 2), Direction.East);

        var placed = robot.Place(new Coordinate(1, 3), Direction.North);

        Assert.True(placed);
        Assert.Equal(new Coordinate(1, 3), robot.Position);
        Assert.Equal(Direction.North, robot.Facing);
    }

    [Fact]
    public void Move_DoesNothingWhenRobotIsUnplaced()
    {
        var robot = new Robot();

        var moved = robot.Move();

        Assert.False(moved);
        Assert.False(robot.IsPlaced);
    }

    [Fact]
    public void Move_SucceedsAfterPreviouslyBlockedMovement()
    {
        var robot = new Robot();
        robot.Place(new Coordinate(0, 0), Direction.West);

        Assert.False(robot.Move());

        robot.RotateRight();
        robot.RotateRight();

        Assert.True(robot.Move());
        Assert.Equal(new Coordinate(1, 0), robot.Position);
        Assert.Equal(Direction.East, robot.Facing);
    }

    [Theory]
    [InlineData(Direction.North)]
    [InlineData(Direction.East)]
    [InlineData(Direction.South)]
    [InlineData(Direction.West)]
    public void RotateLeft_FourConsecutiveRotationsReturnToOriginalDirection(Direction initial)
    {
        var robot = new Robot();
        robot.Place(new Coordinate(2, 2), initial);

        robot.RotateLeft();
        robot.RotateLeft();
        robot.RotateLeft();
        robot.RotateLeft();

        Assert.Equal(initial, robot.Facing);
    }

    [Theory]
    [InlineData(Direction.North)]
    [InlineData(Direction.East)]
    [InlineData(Direction.South)]
    [InlineData(Direction.West)]
    public void RotateRight_FourConsecutiveRotationsReturnToOriginalDirection(Direction initial)
    {
        var robot = new Robot();
        robot.Place(new Coordinate(2, 2), initial);

        robot.RotateRight();
        robot.RotateRight();
        robot.RotateRight();
        robot.RotateRight();

        Assert.Equal(initial, robot.Facing);
    }

    [Fact]
    public void RotateLeft_DoesNothingWhenRobotIsUnplaced()
    {
        var robot = new Robot();

        robot.RotateLeft();

        Assert.False(robot.IsPlaced);
        Assert.Equal(Direction.North, robot.Facing);
    }

    [Fact]
    public void RotateRight_DoesNothingWhenRobotIsUnplaced()
    {
        var robot = new Robot();

        robot.RotateRight();

        Assert.False(robot.IsPlaced);
        Assert.Equal(Direction.North, robot.Facing);
    }
}
