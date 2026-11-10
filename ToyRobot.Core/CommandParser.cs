using System.Globalization;
using System.Text.RegularExpressions;

namespace ToyRobot.Core;

public sealed class CommandParser
{
    private static readonly Regex PlaceCommandPattern = new(
        "^PLACE (-?\\d+),(-?\\d+),(NORTH|SOUTH|EAST|WEST)$",
        RegexOptions.CultureInvariant | RegexOptions.Compiled);

    public bool TryParse(string? input, out Command command)
    {
        command = default;

        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        var line = input.Trim();

        switch (line)
        {
            case "MOVE":
                command = new Command(CommandKind.Move);
                return true;
            case "LEFT":
                command = new Command(CommandKind.Left);
                return true;
            case "RIGHT":
                command = new Command(CommandKind.Right);
                return true;
            case "REPORT":
                command = new Command(CommandKind.Report);
                return true;
        }

        var match = PlaceCommandPattern.Match(line);
        if (!match.Success ||
            !int.TryParse(match.Groups[1].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var x) ||
            !int.TryParse(match.Groups[2].Value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var y) ||
            !TryParseDirection(match.Groups[3].Value, out var facing))
        {
            return false;
        }

        command = new Command(CommandKind.Place, x, y, facing);
        return true;
    }

    private static bool TryParseDirection(string value, out Direction direction)
    {
        switch (value)
        {
            case "NORTH":
                direction = Direction.North;
                return true;
            case "EAST":
                direction = Direction.East;
                return true;
            case "SOUTH":
                direction = Direction.South;
                return true;
            case "WEST":
                direction = Direction.West;
                return true;
            default:
                direction = default;
                return false;
        }
    }
}
