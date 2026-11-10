# Submission Checklist

This checklist is based on the **Submission Guidelines** in `Toy Robot Challenge - 2026.pdf`.

## Required deliverables

- [ ] Complete source code for the Toy Robot Simulator.
  - [ ] C# source targeting .NET 10.
  - [ ] Console application entry point and runtime behaviour.
  - [ ] All required commands implemented: `PLACE`, `MOVE`, `LEFT`, `RIGHT`, and `REPORT`.
- [ ] Test data to exercise the application.
  - [ ] The three examples from the challenge PDF.
  - [ ] Valid placements and replacement placements.
  - [ ] Invalid placements, including an invalid placement after a successful placement.
  - [ ] Boundary movements and ignored commands before placement.
  - [ ] Rotations and command sequences.
- [ ] Short README with instructions for building and running the solution.
  - [ ] Prerequisites, including the required .NET SDK.
  - [ ] Build command.
  - [ ] Test command.
  - [ ] Run command.
  - [ ] Input source and expected input format.
  - [ ] Example invocation and output.
- [ ] Brief description of the development process.
  - [ ] Describe the Spec-Driven Development stages used.
  - [ ] Identify tools used and how they were used.
  - [ ] Identify resources consulted, including the challenge PDF.
  - [ ] Record notable decisions and trade-offs.
- [ ] AI prompts and artefacts, if AI was used to write any part of the ToyRobot.
  - [ ] Preserve the prompts used for code generation or material code assistance.
  - [ ] Preserve relevant input artefacts supplied to the AI.
  - [ ] Identify which generated or AI-assisted outputs were used, reviewed, or changed.

## Final verification before submission

- [ ] The application builds successfully with .NET 10.
- [ ] Automated tests pass.
- [ ] Test data can be run or followed independently of the implementation details.
- [ ] The README instructions work from a clean checkout.
- [ ] No required deliverable contains secrets, credentials, or unrelated local files.
- [ ] The final submission contains the source, tests/test data, README, development-process description, and AI prompts/artefacts where applicable.
