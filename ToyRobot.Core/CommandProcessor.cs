namespace ToyRobot.Core;

public sealed class CommandProcessor
{
    private readonly CommandParser parser = new();
    private readonly Robot robot;

    public CommandProcessor(Robot robot)
    {
        this.robot = robot;
    }

    public string? Process(string? input)
    {
        if (!parser.TryParse(input, out var command))
        {
            return null;
        }

        switch (command.Kind)
        {
            case CommandKind.Place:
                robot.Place(new Coordinate(command.X, command.Y), command.Facing!.Value);
                return null;
            case CommandKind.Move:
                robot.Move();
                return null;
            case CommandKind.Left:
                robot.RotateLeft();
                return null;
            case CommandKind.Right:
                robot.RotateRight();
                return null;
            case CommandKind.Report:
                return robot.IsPlaced
                    ? $"{robot.Position.X},{robot.Position.Y},{robot.Facing.ToString().ToUpperInvariant()}"
                    : null;
            default:
                return null;
        }
    }
}
