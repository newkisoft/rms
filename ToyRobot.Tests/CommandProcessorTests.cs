using ToyRobot.Core;

namespace ToyRobot.Tests;

public sealed class CommandProcessorTests
{
    [Fact]
    public void Process_ProducesExpectedReportForChallengeExampleOne()
    {
        var reports = ProcessCommands(
            "PLACE 0,0,NORTH",
            "MOVE",
            "REPORT");

        Assert.Equal(["0,1,NORTH"], reports);
    }

    [Fact]
    public void Process_ProducesExpectedReportForChallengeExampleTwo()
    {
        var reports = ProcessCommands(
            "PLACE 0,0,NORTH",
            "LEFT",
            "REPORT");

        Assert.Equal(["0,0,WEST"], reports);
    }

    [Fact]
    public void Process_ProducesExpectedReportForChallengeExampleThree()
    {
        var reports = ProcessCommands(
            "PLACE 1,2,EAST",
            "MOVE",
            "MOVE",
            "LEFT",
            "MOVE",
            "REPORT");

        Assert.Equal(["3,3,NORTH"], reports);
    }

    [Fact]
    public void Process_IgnoresCommandsBeforeFirstValidPlacement()
    {
        var reports = ProcessCommands(
            "MOVE",
            "LEFT",
            "RIGHT",
            "REPORT",
            "PLACE 1,1,NORTH",
            "REPORT");

        Assert.Equal(["1,1,NORTH"], reports);
    }

    [Fact]
    public void Process_ContinuesAfterInvalidCommand()
    {
        var reports = ProcessCommands(
            "PLACE 0,0,NORTH",
            "UNKNOWN",
            "PLACE invalid,1,NORTH",
            "MOVE",
            "REPORT");

        Assert.Equal(["0,1,NORTH"], reports);
    }

    [Fact]
    public void Process_PreservesStateAfterInvalidReplacementPlacement()
    {
        var reports = ProcessCommands(
            "PLACE 2,2,EAST",
            "PLACE 5,2,NORTH",
            "REPORT");

        Assert.Equal(["2,2,EAST"], reports);
    }

    [Fact]
    public void Process_ContinuesAfterBlockedMovement()
    {
        var reports = ProcessCommands(
            "PLACE 0,0,WEST",
            "MOVE",
            "RIGHT",
            "RIGHT",
            "MOVE",
            "REPORT");

        Assert.Equal(["1,0,EAST"], reports);
    }

    [Fact]
    public void Process_ProducesEachReportWithoutChangingState()
    {
        var reports = ProcessCommands(
            "PLACE 1,2,SOUTH",
            "REPORT",
            "REPORT",
            "MOVE",
            "REPORT");

        Assert.Equal(["1,2,SOUTH", "1,2,SOUTH", "1,1,SOUTH"], reports);
    }

    private static List<string> ProcessCommands(params string[] commands)
    {
        var processor = new CommandProcessor(new Robot());
        var reports = new List<string>();

        foreach (var command in commands)
        {
            var report = processor.Process(command);
            if (report is not null)
            {
                reports.Add(report);
            }
        }

        return reports;
    }
}
