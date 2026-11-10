namespace ToyRobot.Core;

public sealed class Robot
{
    private readonly Table table = new();

    public bool IsPlaced { get; private set; }

    public Coordinate Position { get; private set; }

    public Direction Facing { get; private set; } = Direction.North;

    public bool Place(Coordinate position, Direction facing)
    {
        if (!table.Contains(position) || !Enum.IsDefined(facing))
        {
            return false;
        }

        Position = position;
        Facing = facing;
        IsPlaced = true;
        return true;
    }

    public bool Move()
    {
        if (!IsPlaced)
        {
            return false;
        }

        var nextPosition = Facing switch
        {
            Direction.North => Position with { Y = Position.Y + 1 },
            Direction.East => Position with { X = Position.X + 1 },
            Direction.South => Position with { Y = Position.Y - 1 },
            Direction.West => Position with { X = Position.X - 1 },
            _ => Position
        };

        if (!table.Contains(nextPosition))
        {
            return false;
        }

        Position = nextPosition;
        return true;
    }

    public void RotateLeft()
    {
        if (!IsPlaced)
        {
            return;
        }

        Facing = Facing switch
        {
            Direction.North => Direction.West,
            Direction.West => Direction.South,
            Direction.South => Direction.East,
            Direction.East => Direction.North,
            _ => Facing
        };
    }

    public void RotateRight()
    {
        if (!IsPlaced)
        {
            return;
        }

        Facing = Facing switch
        {
            Direction.North => Direction.East,
            Direction.East => Direction.South,
            Direction.South => Direction.West,
            Direction.West => Direction.North,
            _ => Facing
        };
    }
}
