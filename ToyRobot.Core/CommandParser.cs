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
            !Enum.TryParse<Direction>(match.Groups[3].Value, ignoreCase: true, out var facing))
        {
            return false;
        }

        command = new Command(CommandKind.Place, x, y, facing);
        return true;
    }
}
