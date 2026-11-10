# Toy Robot Simulator - Architecture

## 1. Architectural goals

The solution will be a small C#/.NET 10 console application with clear separation between:

1. Command parsing
2. Robot and table business rules
3. Console input and output

The design should make the robot rules testable without using the console, while avoiding abstractions that do not provide value for this single-robot challenge.

## 2. Proposed structure

```text
Console input
    |
    v
Command parser  --->  Parsed command values
    |
    v
Command processor / application flow
    |
    +--> Robot state and movement rules
    |        |
    |        +--> Table boundary validation
    |
    +--> Report formatter ---> Console output
```

The console layer supplies input lines and writes reports. The parser converts valid text into command values. The command processor applies commands to the robot. The robot and table contain the domain rules and do not depend on console APIs.

## 3. Responsibilities

### 3.1 Console entry point / application flow

- Select the supported input source, initially standard input unless a different choice is approved.
- Read input lines until end of input.
- Pass each line to the parser.
- Pass successfully parsed commands to the command processor.
- Write report results to standard output.
- Avoid implementing movement, rotation, or placement rules directly.

### 3.2 Command parser

- Recognise `PLACE X,Y,F`, `MOVE`, `LEFT`, `RIGHT`, and `REPORT`.
- Parse integer coordinates and direction text for `PLACE`.
- Return a representation of valid commands and their arguments.
- Treat malformed or unknown input as ignored according to the requirements assumptions.
- Have no dependency on the console, robot state, or output.

The parser is responsible for syntax. It does not decide whether coordinates are on the table; that is a domain validation concern.

### 3.3 Command representation

- Use a small command discriminator, such as an enum, for the five supported command kinds.
- Carry `X`, `Y`, and direction only for `PLACE`.
- Represent direction with a `Direction` enum containing `North`, `East`, `South`, and `West`.

The command representation should be simple data rather than a command-object hierarchy. Five command types do not justify separate command classes or a command pattern.

### 3.4 Robot

- Hold whether the robot has been placed.
- Hold the current X coordinate, Y coordinate, and `Direction` when placed.
- Apply a valid placement.
- Ignore operational commands while unplaced.
- Move one unit in the current direction only when the destination is valid.
- Rotate left or right by 90 degrees without changing position.
- Expose a read-only state suitable for reporting and tests.
- Preserve the existing state when an invalid placement is attempted after a successful placement.

The robot owns state transitions so movement and rotation can be tested directly without console interaction.

### 3.5 Table / bounds value object

- Determine whether a coordinate is within the table.
- Represent the default 5-by-5 table.
- Optionally accept width and height internally if that adds little complexity and does not leak unnecessary configuration into the console interface.

The default valid coordinate range is 0 through 4 for both axes. The table has no obstacle or collision responsibilities.

### 3.6 Report formatting and output

- Convert the placed robot state to the canonical example format `X,Y,F`.
- Keep formatting separate from state mutation.
- Write formatted reports through the console boundary only.
- Produce no required report for an unplaced robot, consistent with the requirements assumptions.

## 4. Command processing flow

For each input line:

1. The console input layer reads the line.
2. The parser trims permitted surrounding whitespace and identifies the command.
3. Malformed or unknown input is ignored.
4. A valid command is passed to the command processor.
5. The processor delegates placement, movement, or rotation to the robot.
6. For `REPORT`, the current placed state is formatted and written to standard output.
7. The next input line is processed even when a command is ignored or a move is blocked.

Invalid `PLACE` is validated before changing the robot state. Therefore, after the robot has been successfully placed, an invalid later `PLACE` leaves the previous state intact.

## 5. Key design decisions and trade-offs

### Simple domain model over extensive patterns

Use a small set of focused types rather than factories, dependency-injection containers, strategy objects, or a command class hierarchy. This keeps the implementation proportional to five commands and makes the state rules easy to follow.

### Direction enum

Directions are represented by an enum so invalid directions cannot enter the domain model after parsing. Rotation can use explicit clockwise/counter-clockwise transitions, which is clearer than relying on enum numeric values.

### Domain-first testability

The robot and table accept values and return state/results without reading from or writing to the console. Console integration is tested separately with controlled input/output boundaries where needed.

### Default fixed table with low-cost configurability

The challenge requires a 5-by-5 table. A small internal table abstraction may accept dimensions if it does not complicate the public flow. No configuration file, command-line option, or user-facing table-size feature is required.

### Standard input as the initial input source

The PDF allows either file input or standard input. The initial design uses standard input because it is the smallest console-oriented surface and supports redirected test data. File-specific behaviour is not added unless later required.

## 6. Non-goals

- Multiple robots
- Obstacles or collisions
- Persistence or networking
- Interactive menus or a graphical interface
- Runtime table-size configuration
- General-purpose plugin or command frameworks
- Application code or test code in this architecture stage
