# SudokuSolver
**C# console app for solving sudoku puzzles**

Sudoku is my go-to game while on airplanes.  Typically, the puzzles in the magazines on airplanes are partially filled in by the time I get to them.  This frustrates me a bit.  If you're going to mess it up by writing in it, at least fill it in all the way.  I'd rather see it filled in brokenly than have wild and obvious gaps showing.  Strange, I know, but that's me.

In any case, these puzzles are not worth doing.  However, I think it would be fun to not waste any time on them but at least fill them in correctly.  In order to do that, I need to either:

1. Get _lots_ better at solving them in my head, because the page is already messed up, or...
2. Feed the initial numbers into a solver and have it spit out the answers for me so I can use my fine-tipped Sharpie to fill in all the right numbers.

So, here's my Sudoku Solver.  I'll keep a log in the commits on how it evolves.

## Classes

As it turns out, Sudoku has a set of terms for all of the concepts involved.  I was looking to find names for the classes dancing around in my head and, after a quick search, found them all listed on Wikipedia.  I'll be using the terms found here: http://en.wikipedia.org/wiki/Glossary_of_Sudoku#Grid_layout_and_puzzle_terms

I'm going to start with things like:

* Grid
* Row
* Column
* Block
* Cell

I'm not using "square" for the reasons stated in the article, and I'm not using "box" since I'm doing C# here and "box" might be reserved in the Framework.

## Starting simple

I suspect there are some pretty standard algorithmic implementations for Sudoku.  However, I'd like to evolve the solution using tests and the approach I use to solve them by hand.  As a result, I'll be writing tests first (primarily), and starting with puzzles that I can easily solve at a glance.

