namespace ToyRobot.Core;

public readonly record struct Command(
    CommandKind Kind,
    int X = 0,
    int Y = 0,
    Direction? Facing = null);
