# Toy Robot Simulator - Requirements Specification

## 1. Purpose and scope

This specification captures the requirements in **Toy Robot Challenge - 2026.pdf** for a console application that simulates one toy robot moving on a square tabletop.

This document is limited to requirements and acceptance criteria. It does not define the application architecture, implementation, project structure, or test code.

## 2. Source requirements

The following are explicit requirements from the challenge PDF:

1. The application is a console application.
2. The application reads a sequence of robot commands.
3. The table is square and has dimensions 5 units by 5 units.
4. The table has no other obstructions.
5. The robot may move over the table surface but must not fall from the table.
6. Any movement that would cause the robot to fall must be prevented, while later valid movement commands remain allowed.
7. The first command to the robot is required to be a `PLACE` command.
8. Commands received before a valid `PLACE` command must be discarded.
9. After a valid placement, commands may be issued in any order, including another `PLACE` command.
10. A robot that is not on the table may ignore `MOVE`, `LEFT`, `RIGHT`, and `REPORT`.
11. Input may come from a file or standard input; the developer may choose which input source to support.
12. The initial placement is also subject to the no-falling constraint.
13. A move that would cause the robot to fall must be ignored.
14. `REPORT` must announce the robot's X coordinate, Y coordinate, and facing direction. The format may be any form; standard output is sufficient.

The PDF's submission guidelines also request complete source code, test data, a short README with build/run instructions, a description of the development process and tools, and any AI prompts or artefacts used for code generation. Those are delivery requirements, not simulator runtime behaviour, and are outside this requirements document's implementation scope.

## 3. Table and coordinate system

### 3.1 Explicit rules

- The table contains a 5-by-5 grid of valid robot positions.
- The origin `(0,0)` is the south-west corner.
- A valid position is therefore any coordinate satisfying:

  - `0 <= X <= 4`
  - `0 <= Y <= 4`

- There are no obstacles or blocked cells.

### 3.2 Assumptions for this specification

- Coordinates identify grid positions, not continuous points.
- Each `MOVE` changes exactly one coordinate by one unit.
- Coordinates outside the inclusive range 0 through 4 are invalid for `PLACE` and would place the robot outside the table.

## 4. Supported commands and expected behaviour

Commands are shown using the command names and syntax defined in the PDF.

### 4.1 `PLACE X,Y,F`

Where:

- `X` is the robot's horizontal coordinate.
- `Y` is the robot's vertical coordinate.
- `F` is one of `NORTH`, `SOUTH`, `EAST`, or `WEST`.

Expected behaviour:

- If the coordinates are on the table and the facing value is valid, place the robot at `(X,Y)` facing `F`.
- A valid `PLACE` establishes that the robot is on the table and enables subsequent commands.
- A later valid `PLACE` replaces the robot's current position and facing direction.
- If the placement is invalid, the robot must not be placed outside the table. The exact treatment of an invalid placement when a robot is already on the table is not specified by the PDF; see the ambiguities section.

### 4.2 `MOVE`

- Move the robot one unit forward in its current facing direction.
- If the resulting position is on the table, apply the movement.
- If the resulting position would be outside the table, ignore the command and leave both position and facing unchanged.
- A blocked movement must not prevent later valid commands from being processed.
- If no valid placement has yet occurred, the command may be ignored.

### 4.3 `LEFT`

- Rotate the robot 90 degrees to the left (counter-clockwise relative to the coordinate system).
- Do not change the robot's position.
- If no valid placement has yet occurred, the command may be ignored.

### 4.4 `RIGHT`

- Rotate the robot 90 degrees to the right (clockwise relative to the coordinate system).
- Do not change the robot's position.
- If no valid placement has yet occurred, the command may be ignored.

### 4.5 `REPORT`

- Announce the robot's current X coordinate, Y coordinate, and facing direction.
- Standard output is sufficient.
- If no valid placement has yet occurred, the command may be ignored.
- The PDF does not mandate an exact report formatting beyond including X, Y, and F; the examples use `X,Y,F`.

## 5. Movement and rotation rules

### 5.1 Movement deltas

For a valid `MOVE`:

| Facing | X change | Y change |
|---|---:|---:|
| `NORTH` | 0 | +1 |
| `EAST` | +1 | 0 |
| `SOUTH` | 0 | -1 |
| `WEST` | -1 | 0 |

### 5.2 Rotation order

The facing direction cycles as follows:

| Current facing | After `LEFT` | After `RIGHT` |
|---|---|---|
| `NORTH` | `WEST` | `EAST` |
| `EAST` | `NORTH` | `SOUTH` |
| `SOUTH` | `EAST` | `WEST` |
| `WEST` | `SOUTH` | `NORTH` |

Rotation does not affect coordinates and is not subject to table-boundary checks.

## 6. Boundaries and validation

### 6.1 Placement validation

A `PLACE` command is valid only when:

- `X` is a valid table coordinate from 0 through 4.
- `Y` is a valid table coordinate from 0 through 4.
- `F` is exactly one of `NORTH`, `SOUTH`, `EAST`, or `WEST`, subject to the parsing assumptions below.

An invalid `PLACE` must not result in a robot position outside the table.

### 6.2 Movement validation

- Evaluate the proposed destination before changing the robot state.
- Reject a destination when its X or Y coordinate is outside 0 through 4.
- Rejected movement leaves the robot in its prior state.

### 6.3 Command-sequence validation

- Before the first valid `PLACE`, ignore commands that cannot operate on a robot on the table.
- A valid `PLACE` is required before a meaningful `MOVE`, `LEFT`, `RIGHT`, or `REPORT` operation.
- After a valid `PLACE`, continue processing commands independently; an invalid or blocked command must not terminate the sequence unless the chosen input mechanism itself ends.

## 7. Edge cases

The following cases are required by the explicit rules or are necessary to make those rules testable:

1. `MOVE`, `LEFT`, `RIGHT`, or `REPORT` before any valid `PLACE`: ignore the command and do not produce a robot state or report.
2. Invalid placement coordinates below 0 or above 4: reject the placement.
3. Invalid placement direction: reject the placement.
4. A placement exactly on any edge or corner, such as `(0,0)` or `(4,4)`: accept it when the direction is valid.
5. A `MOVE` from an edge towards the outside: ignore it without changing state.
6. Repeated boundary moves: continue ignoring each invalid move; do not terminate processing.
7. A valid move after an ignored boundary move: apply the valid move normally.
8. Four consecutive `LEFT` commands or four consecutive `RIGHT` commands: return to the original direction while keeping the same position.
9. A `PLACE` after the robot is already placed: a valid new placement becomes the current state.
10. Invalid commands or malformed command text: the PDF does not define required behaviour. The selected handling must not create an invalid robot state; see assumptions and ambiguities below.
11. Blank input lines: the PDF does not define required behaviour. They should not create an action or an invalid robot state under the parsing assumption below.
12. A `REPORT` after an ignored `MOVE`: report the unchanged position and facing.

## 8. Parsing assumptions

These are assumptions for the implementation and are not stated explicitly in the PDF. They must remain easy to change if the challenge owner clarifies them:

- Commands are expected to be supplied one per input line.
- Command names and directions are case-sensitive and use the uppercase spelling shown in the PDF.
- The canonical accepted `PLACE` shape is `PLACE X,Y,F`, with two comma-separated integer coordinates and one direction token.
- Leading or trailing whitespace may be ignored when identifying a command line.
- Extra whitespace within a command, extra delimiters, missing arguments, non-integer coordinates, or unknown direction values are treated as malformed and ignored.
- Unknown commands are ignored and do not alter the robot state.
- Blank lines are ignored.
- A malformed `PLACE` is not a valid placement and therefore does not satisfy the requirement for the first valid command.
- Reports use the example format `X,Y,F` on standard output, although the PDF allows any output form that announces all three values.

## 9. Acceptance criteria

### AC-01: Valid initial placement

**Given** the robot has not been placed

**When** the command `PLACE 0,0,NORTH` is processed

**Then** the robot is on the table at `(0,0)` facing `NORTH`

### AC-02: Commands before placement are ignored

**Given** no valid `PLACE` command has been processed

**When** `MOVE`, `LEFT`, `RIGHT`, or `REPORT` is processed

**Then** the command has no effect and no invalid robot state is created

### AC-03: Move north and report

**Given** the robot is at `(0,0)` facing `NORTH`

**When** `MOVE` followed by `REPORT` is processed

**Then** the report announces `0,1,NORTH`

### AC-04: Rotate left without moving

**Given** the robot is at `(0,0)` facing `NORTH`

**When** `LEFT` followed by `REPORT` is processed

**Then** the report announces `0,0,WEST`

### AC-05: Execute a sequence of movement and rotation commands

**Given** the robot has not been placed

**When** the following commands are processed in order:

```text
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
```

**Then** the report announces `3,3,NORTH`

### AC-06: Prevent falling at a boundary

**Given** the robot is at `(0,0)` facing `WEST`

**When** `MOVE` is processed

**Then** the robot remains at `(0,0)` facing `WEST`

### AC-07: Continue after an ignored movement

**Given** the robot is at `(0,0)` facing `WEST`

**When** `MOVE`, then `RIGHT`, then `RIGHT`, then `MOVE`, then `REPORT` is processed

**Then** the first `MOVE` is ignored, the two rotations result in `EAST`, the second `MOVE` succeeds, and the report announces `1,0,EAST`

### AC-08: Reject invalid initial placement

**Given** no valid placement has been processed

**When** an out-of-range or malformed `PLACE` is processed

**Then** the robot remains unplaced and later operational commands continue to be ignored until a valid `PLACE` is processed

### AC-09: Preserve state after an invalid replacement placement

**Given** the robot is placed at `(2,2)` facing `EAST`

**When** an invalid `PLACE`, such as `PLACE 5,2,NORTH`, is processed

**Then** the invalid placement is ignored and the robot remains at `(2,2)` facing `EAST`

### AC-10: Accept a valid replacement placement

**Given** the robot is already placed

**When** another valid `PLACE` is processed

**Then** the current position and facing are replaced by the new placement

### AC-11: Reject an invalid movement destination

**Given** the robot is on the table at any edge position

**When** a `MOVE` would produce a coordinate outside 0 through 4

**Then** the move is ignored, the robot remains on the table, and subsequent valid commands can still be processed

## 10. Ambiguities in the original requirements

The PDF leaves the following points open:

1. **Exact report format:** It says any form is acceptable, while examples use `X,Y,F`. This specification adopts the example form for consistency.
2. **Input source choice:** File input and standard input are both allowed, but the PDF does not require both. The implementation may choose one, as explicitly permitted.
3. **Invalid command handling:** The PDF only says commands before a valid placement may be ignored; it does not state how unknown or malformed commands after placement should be handled. This specification assumes they are ignored without changing state.
4. **Invalid replacement placement:** The PDF says initial placement must be valid but does not explicitly state whether an invalid later `PLACE` preserves the prior valid state or clears it. This specification assumes the prior valid state is preserved.
5. **Case and whitespace rules:** The PDF does not define case-insensitivity or whitespace tolerance. This specification assumes uppercase command and direction tokens, with surrounding whitespace tolerated.
6. **End-of-input behaviour:** The PDF does not specify whether the application should wait for more input, terminate at end-of-file, or provide an interactive prompt. This is an input-interface concern and is not fixed here.
7. **Error feedback:** The PDF does not require error messages for ignored, malformed, or invalid commands. No error output is required by this specification.
8. **The phrase 'first command':** The PDF says the first command must be `PLACE`, then separately says the application should discard commands until a valid `PLACE` has been executed. This specification interprets the latter as authoritative operational behaviour: pre-placement commands, including invalid commands, are ignored until a valid placement occurs.
9. **Meaning of 'dimensions 5 units x 5 units':** The PDF does not explicitly state whether the maximum coordinate is 4 or 5. Its standard Toy Robot examples and the interpretation of a 5-position grid imply coordinates 0 through 4; this specification adopts that interpretation.

## 11. Out of scope for this stage

- Application architecture and project structure.
- C#/.NET implementation details.
- Automated test implementation.
- Logging, persistence, networking, user interfaces beyond the required console interaction, and multiple robots.
- Obstacles, collision handling, commands other than `PLACE`, `MOVE`, `LEFT`, `RIGHT`, and `REPORT`.
