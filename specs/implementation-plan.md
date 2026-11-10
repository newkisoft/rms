# Toy Robot Simulator - Implementation Plan

This plan follows the approved requirements and proposed architecture. It defines small implementation and verification tasks but does not contain application code or test code.

## 1. Development sequence

### Phase 1: Project and documentation foundation

1. Create the .NET 10 console application project.
2. Create the xUnit test project targeting .NET 10.
3. Add the project reference from tests to the application/domain project as appropriate.
4. Add a README with build, test, run, input, and output instructions.
5. Record the chosen input source and any other approved assumptions.

Verification:

- The solution restores and builds with no application behaviour added beyond the project skeleton.
- The test project is discovered by the .NET test runner.

### Phase 2: Domain primitives and table rules

1. Add the `Direction` enum with the four supported directions.
2. Add the coordinate/state representation needed by the robot.
3. Add the 5-by-5 table boundary rule, with optional internal width/height values only if it remains simple.
4. Verify coordinates 0 through 4 are accepted and coordinates outside that range are rejected.

Verification:

- Unit tests cover corners, edges, centre positions, negative coordinates, and coordinates above 4.

### Phase 3: Robot state and transitions

1. Implement valid placement.
2. Preserve the unplaced state after invalid initial placement.
3. Preserve the previous placed state after invalid replacement placement.
4. Implement forward movement for all four directions.
5. Ignore movements that would leave the table.
6. Implement `LEFT` and `RIGHT` rotations without changing position.
7. Ignore operational commands while unplaced.
8. Expose state in a read-only form for reporting and tests.

Verification:

- Unit tests cover each direction's movement delta, all rotation transitions, full rotation cycles, valid placements, invalid placements, state preservation, and boundary movement.

### Phase 4: Command parsing

1. Parse the five supported command forms.
2. Parse `PLACE X,Y,F` coordinates and direction.
3. Reject malformed placement syntax, invalid integers, invalid directions, unknown commands, and blank lines according to the approved parsing assumptions.
4. Keep parser tests independent of the robot and console.

Verification:

- Parser tests distinguish valid command syntax from malformed input.
- Parsing does not mutate robot state.

### Phase 5: Command processing and reporting

1. Add the command processor that applies parsed commands to the robot.
2. Ensure commands before the first valid `PLACE` are ignored.
3. Ensure ignored or blocked commands do not stop subsequent commands.
4. Add canonical report formatting using `X,Y,F`.
5. Add standard-input reading and standard-output reporting at the console boundary.

Verification:

- Command-sequence tests cover placement, movement, rotation, invalid commands, invalid replacement placement, and continued processing.
- Output tests verify the report contains the expected coordinates and direction.

### Phase 6: Challenge examples and integration verification

Run the three examples from the original challenge as end-to-end command sequences.

#### Example 1

```text
PLACE 0,0,NORTH
MOVE
REPORT
```

Expected report: `0,1,NORTH`.

#### Example 2

```text
PLACE 0,0,NORTH
LEFT
REPORT
```

Expected report: `0,0,WEST`.

#### Example 3

```text
PLACE 1,2,EAST
MOVE
MOVE
LEFT
MOVE
REPORT
```

Expected report: `3,3,NORTH`.

Verification:

- The examples pass through the intended input and output path.
- The results match the PDF exactly.

### Phase 7: Documentation and submission readiness

1. Finalise README build, test, and run instructions.
2. Add reusable test data for the three examples and edge cases, if the selected test-data format needs separate files.
3. Document the development process, tools, resources, decisions, and trade-offs.
4. Preserve AI prompts and relevant artefacts if AI was used to write any ToyRobot code.
5. Complete `specs/submission-checklist.md` and verify every required deliverable.
6. Run formatting, build, automated tests, and a clean-checkout documentation verification.

## 2. Acceptance-criteria test map

| Acceptance criterion | Planned verification |
|---|---|
| AC-01 Valid initial placement | Robot/domain unit test |
| AC-02 Commands before placement are ignored | Command processor test |
| AC-03 Move north and report | Command sequence/output test |
| AC-04 Rotate left without moving | Robot unit test plus report test |
| AC-05 PDF command sequence | End-to-end sequence test |
| AC-06 Prevent falling at a boundary | Robot boundary unit test |
| AC-07 Continue after ignored movement | Command sequence test |
| AC-08 Reject invalid initial placement | Placement/domain test and parser test |
| AC-09 Preserve state after invalid replacement placement | Robot/domain state-preservation test |
| AC-10 Accept valid replacement placement | Robot/domain test |
| AC-11 Reject invalid movement destination | Boundary and continued-processing test |

## 3. Additional test coverage

### Placement and validation

- All four valid directions.
- All four corners and representative edge positions.
- Negative X and Y values.
- X and Y values greater than 4.
- Invalid direction names.
- Malformed coordinate separators and missing fields.
- Invalid replacement placement preserves both position and facing.

### Movement

- North, south, east, and west movement from interior positions.
- Attempted movement off each of the four table edges.
- Multiple blocked moves followed by a valid move.
- Position and facing are unchanged by a blocked move.

### Rotation

- Left and right from every direction.
- Four left rotations return to the initial direction.
- Four right rotations return to the initial direction.
- Rotations do not change coordinates.

### Command sequences and I/O

- Commands before placement.
- Unknown and malformed commands between valid commands.
- Multiple valid placements.
- Reports after movement, rotation, blocked movement, and replacement placement.
- Empty input and end-of-input handling according to the selected console behaviour.

## 4. Build and verification commands to document

The final README should document the exact commands selected for the repository, including equivalents of:

```text
dotnet restore
dotnet build
dotnet test
dotnet run
```

The exact project or solution paths should be filled in after the project structure is created. No implementation or test code is created by this planning stage.

## 5. Approval gates

Before implementation begins, confirm:

- The architecture's standard-input choice is acceptable.
- The canonical report format `X,Y,F` is acceptable.
- The parsing assumptions in the requirements are acceptable, especially uppercase-only commands, surrounding-whitespace tolerance, and ignoring malformed input.
- Optional internal table configurability should either be retained or removed based on implementation simplicity.
