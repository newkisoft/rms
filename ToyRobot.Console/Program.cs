using ToyRobot.Core;

var processor = new CommandProcessor(new Robot());

string? input;
while ((input = Console.ReadLine()) is not null)
{
    var report = processor.Process(input);
    if (report is not null)
    {
        Console.WriteLine(report);
    }
}
