# RMS Toy Robot Simulator

RMS Toy Robot Simulator is a C#/.NET 10 console application developed using Spec-Driven Development.

## Current implementation stage

Phases 1 and 2 are complete:

- .NET 10 solution and projects are created.
- `ToyRobot.Core` contains the `Direction` enum, coordinate value, and fixed 5x5 table boundary rules.
- `ToyRobot.Tests` contains xUnit coverage for valid and invalid coordinates.

Robot movement, command parsing, and console command processing are intentionally not implemented yet.

## Prerequisites

- .NET SDK 10.0 or later

## Build

```text
dotnet restore RmsToyRobot.slnx
dotnet build RmsToyRobot.slnx
```

## Test

```text
dotnet test ToyRobot.Tests/ToyRobot.Tests.csproj
```

The test project uses xUnit and currently verifies the fixed 5x5 table boundaries.

## Run

The console project is present but command processing will be added in a later implementation phase.

```text
dotnet run --project ToyRobot.Console/ToyRobot.Console.csproj
```

## Project layout

- `ToyRobot.Core` - domain primitives and table rules.
- `ToyRobot.Console` - console application host; command processing is not implemented yet.
- `ToyRobot.Tests` - xUnit automated tests for the domain rules.
- `specs` - approved requirements, architecture, implementation plan, and submission checklist.
