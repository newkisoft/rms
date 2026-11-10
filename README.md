# RMS Toy Robot Simulator

RMS Toy Robot Simulator is a C#/.NET 10 console application that simulates one robot moving on a fixed 5x5 tabletop. It was developed using the approved Spec-Driven Development workflow.

## Requirements

- .NET SDK 10.0 or later
- xUnit test support is restored through the test project

## Build

```text
dotnet restore RmsToyRobot.slnx
dotnet build RmsToyRobot.slnx
```

## Run interactively

Run the console application and enter one command per line. End input with EOF (`Ctrl+D` on Linux/macOS or `Ctrl+Z`, then Enter, on Windows).

```text
dotnet run --project ToyRobot.Console/ToyRobot.Console.csproj
```

Supported commands are `PLACE X,Y,F`, `MOVE`, `LEFT`, `RIGHT`, and `REPORT`. Reports use `X,Y,F`, for example `0,1,NORTH`.

## Run using the supplied examples

The original challenge examples are stored in `test-data/`:

```text
dotnet run --project ToyRobot.Console/ToyRobot.Console.csproj < test-data/example1.txt
dotnet run --project ToyRobot.Console/ToyRobot.Console.csproj < test-data/example2.txt
dotnet run --project ToyRobot.Console/ToyRobot.Console.csproj < test-data/example3.txt
```

Expected outputs are respectively:

```text
0,1,NORTH
```

```text
0,0,WEST
```

```text
3,3,NORTH
```

## Run tests

```text
dotnet test ToyRobot.Tests/ToyRobot.Tests.csproj
```

The xUnit suite covers table boundaries, robot state transitions, command parsing, command sequences, invalid input, blocked movement, replacement placement, reporting, and the three challenge examples.

## Verification results

Final verification executed in this repository:

- `dotnet build RmsToyRobot.slnx --no-restore -m:1` - succeeded with 0 warnings and 0 errors.
- `dotnet test ToyRobot.Tests/ToyRobot.Tests.csproj --no-build --no-restore` - 96 passed, 0 failed, 0 skipped.
- `test-data/example1.txt` - output `0,1,NORTH`.
- `test-data/example2.txt` - output `0,0,WEST`.
- `test-data/example3.txt` - output `3,3,NORTH`.

## Architecture

- `ToyRobot.Core` contains the `Direction` enum, `Coordinate`, fixed `Table`, `Robot`, simple command representation, parser, and command processor.
- `ToyRobot.Console` owns standard-input reading and standard-output writing. It reads until EOF and sends each line to the command processor.
- `ToyRobot.Tests` contains unit and command-sequence tests. Domain behavior is tested without console interaction.
- `specs` contains the approved requirements, architecture, implementation plan, and submission checklist.
- `test-data` contains the three original challenge input examples.

## Design decisions and assumptions

- The table is fixed at 5x5, with valid coordinates 0 through 4 on both axes.
- `Direction` is an enum; direction input is uppercase and case-sensitive.
- The parser handles syntax only. Coordinate range validation belongs to `Robot.Place`.
- Invalid commands, malformed input, and commands before a valid placement are ignored.
- Invalid replacement placement preserves the existing robot state.
- A blocked movement preserves position and facing, and later commands continue to execute.
- The command processor returns report text without performing I/O, keeping console concerns separate from domain logic.
- No dependency-injection framework, command hierarchy, persistence, obstacles, or extra features were added.

## Spec-Driven Development workflow

1. Extracted explicit requirements, assumptions, edge cases, acceptance criteria, and ambiguities from the challenge PDF.
2. Defined and approved the architecture and implementation plan before coding.
3. Created the .NET 10 solution, domain primitives, table rules, and xUnit foundation.
4. Implemented and tested robot state and transitions.
5. Implemented and tested independent command parsing.
6. Implemented command processing, standard-input handling, standard-output reporting, and sequence tests.
7. Added challenge test data, performed final build/test/output verification, and reviewed the submission checklist.

The detailed planning and test traceability remain in `specs/implementation-plan.md`.

## AI usage disclosure

Codex was used as an implementation assistant. The actual task prompts and the resulting human review checkpoints are recorded in [`docs/ai-usage.md`](docs/ai-usage.md). A human reviewed and approved the requirements, architecture, implementation phases, test naming, branch/PR workflow, and final scope. Tests and build commands were executed in the shared repository; results are reported only when observed.
