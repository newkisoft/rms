namespace ToyRobot.Core;

public sealed class Table
{
    public const int Size = 5;

    public bool Contains(Coordinate coordinate) =>
        coordinate.X >= 0 && coordinate.X < Size &&
        coordinate.Y >= 0 && coordinate.Y < Size;
}
