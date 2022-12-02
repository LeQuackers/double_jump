# double_jump

A test/example of retroactively adding behaviors and networked variables to a pawn.

This is a proof of concept and a work in progress.

## Known Issues:

- Casting `Client.Pawn` breaks stuff (Switching weapons, spawning things, pressing the use key on things, etc.)
- Most of the `Pawn`/`Player` properties aren't virtual, which causes lots of problems
