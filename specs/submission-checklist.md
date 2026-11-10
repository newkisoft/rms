# Submission Checklist

This checklist is based on the **Submission Guidelines** in `Toy Robot Challenge - 2026.pdf`.

## Required deliverables

- [x] Complete source code for the Toy Robot Simulator.
  - [x] C# source targeting .NET 10.
  - [x] Console application entry point and runtime behaviour.
  - [x] All required commands implemented: `PLACE`, `MOVE`, `LEFT`, `RIGHT`, and `REPORT`.
- [x] Test data to exercise the application.
  - [x] The three examples from the challenge PDF in `test-data/`.
  - [x] Valid placements and replacement placements covered by automated tests.
  - [x] Invalid placements, including an invalid placement after a successful placement, covered by automated tests.
  - [x] Boundary movements and ignored commands before placement covered by automated tests.
  - [x] Rotations and command sequences covered by automated tests.
- [x] Short README with instructions for building and running the solution.
  - [x] Prerequisites, including the required .NET SDK.
  - [x] Build command.
  - [x] Test command.
  - [x] Run command.
  - [x] Input source and expected input format.
  - [x] Example invocation and output.
- [x] Brief description of the development process.
  - [x] Spec-Driven Development stages are documented in `README.md`.
  - [x] Tools and their use are documented in `README.md` and `docs/ai-usage.md`.
  - [x] The challenge PDF and approved specifications are identified as resources.
  - [x] Decisions and trade-offs are documented in `README.md` and `specs/architecture.md`.
- [x] AI prompts and artefacts, because AI was used.
  - [x] Substantive implementation prompts are preserved in `docs/ai-usage.md`.
  - [x] The challenge PDF and generated source/document artefacts are retained.
  - [x] Human review, approvals, and testing are disclosed.

## Final verification before submission

- [x] The application builds successfully with .NET 10.
- [x] Automated tests pass.
- [x] Test data can be run independently of the implementation details.
- [x] The README instructions were verified from a fresh clean checkout.
- [x] Final human review confirmed that no required deliverable contains secrets, credentials, or unrelated local files.
- [x] The final submission currently contains the source, tests/test data, README, development-process description, and AI prompts/artefacts.
