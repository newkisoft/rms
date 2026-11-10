# AI Usage and Prompt Record

## Disclosure

Codex assisted with requirements extraction, architecture and implementation planning, C#/.NET implementation, xUnit test creation, documentation, verification, and GitHub branch/pull-request operations for this repository.

ChatGPT was used to help structure the Spec-Driven Development workflow, prepare implementation prompts, and review code and test coverage at each checkpoint. Codex CLI was used to implement the approved specifications, generate tests, and assist with documentation. The developer reviewed the generated artefacts, manually tested the application, and verified the automated test results.

The user reviewed and approved the requirements, architecture, implementation phases, test naming improvement, and pull-request workflow at the checkpoints recorded in the conversation. The user also supplied the challenge PDF in the repository. Codex did not independently invent external requirements; the approved `specs/` documents and PDF were used as the source of truth.

## Actual implementation prompts

The following are the substantive user prompts used to direct implementation work. Administrative prompts such as requests to push commits or create/delete branches are omitted from this code-generation record but remain in the conversation history.

### Requirements specification

> We are completing the RMS Toy Robot Simulator coding challenge using Spec-Driven Development (SDD).
>
> Read "Toy Robot Challenge - 2026.pdf" carefully.
>
> We will implement the solution using C# and .NET 10.
>
> For this first stage, DO NOT write application code.
>
> Create `specs/requirements.md` containing functional requirements, supported commands and expected behaviour, movement and rotation rules, table boundaries and validation rules, edge cases, Given/When/Then acceptance criteria, and ambiguities. Preserve the original requirements, distinguish explicit requirements from assumptions, avoid unnecessary features, and do not proceed to architecture or coding.

### Architecture and implementation plan

> We have completed Stage 1 of Spec-Driven Development for the RMS Toy Robot Challenge.
>
> Before proceeding, update `specs/requirements.md` to include an acceptance criterion confirming that an invalid PLACE command after a successful placement preserves the previous robot state.
>
> Also create `specs/submission-checklist.md` covering all deliverables required by the original PDF, including AI prompts and artefacts.
>
> Next, read the approved requirements and create `specs/architecture.md` and `specs/implementation-plan.md` for C#/.NET 10, a console application, xUnit, separated parsing/domain/I/O, a simple design, and mapped tests. Do not generate application code or test code yet.

### Phases 1 and 2

> Proceed with Phase 1 and Phase 2 of `specs/implementation-plan.md` only. Create the .NET solution and projects, implement the Direction enum and table boundary rules, create xUnit tests for valid and invalid coordinates, build the solution, and run the tests. Do not implement Robot movement, command parsing, or console command processing yet.

### Phase 3

> Phase 1 and Phase 2 are complete and approved. Implement Phase 3 only: Robot state and transitions. Implement initially unplaced state, valid PLACE, invalid placement rejection and state preservation, MOVE in all directions, boundary protection, LEFT/RIGHT, ignored unplaced commands, read-only state, and descriptive xUnit tests. Do not implement command parsing or console I/O.

### Phase 4

> Phase 3 has been reviewed and approved. Before starting, add a Robot unit test verifying that a valid movement succeeds after a previously blocked movement, and tests for four consecutive LEFT and RIGHT rotations returning to the original direction. Then implement Phase 4 (Command Parsing): recognise PLACE, MOVE, LEFT, RIGHT, REPORT; parse integer coordinates and valid directions; reject unknown/malformed input; follow case and whitespace assumptions; remain independent of Robot, Table, and console I/O; use a simple command representation; and add xUnit coverage.

### Phase 5

> Phase 4 has been reviewed and approved. Before starting, add parser tests for null input and integer overflow, and use case-sensitive direction parsing consistently. Then implement Phase 5: create a simple command processor connecting CommandParser with Robot, ignore commands before valid PLACE, process all commands, allow valid replacement PLACE, preserve state after invalid placements and blocked moves, format reports as uppercase X,Y,F, read standard input until EOF, write reports to standard output, and keep console I/O separate from domain logic. Add command-sequence tests including all three examples and required edge cases.

### Final verification and documentation

> Phases 1–5 have been reviewed and approved. Read AGENTS.md, the approved specifications, and the original challenge requirements. Complete the remaining verification and documentation phases: add command processor tests for valid replacement PLACE and invalid initial PLACE followed by ignored commands; create exact example input files; verify expected console output; run build and tests recording actual results; create a professional README; document the SDD workflow; include an honest AI usage disclosure describing Codex assistance, prompts, human review, and testing; review the submission checklist and identify anything missing. Do not introduce new frameworks or features.

## Artefacts and review

- Source artefacts generated or changed by these prompts are the files under `ToyRobot.Core/`, `ToyRobot.Console/`, `ToyRobot.Tests/`, `README.md`, `specs/`, `test-data/`, and this disclosure file.
- The original challenge PDF is retained in the repository as `Toy Robot Challenge - 2026.pdf`.
- Human review occurred through explicit approvals before Phases 1–5 and through requested test/documentation improvements.
- Verification evidence is the executed `dotnet build`, `dotnet test`, and direct console runs recorded in the implementation session and final task report.
