# Brainfuck Primer

## Introduction

Brainfuck is a minimalistic imperative programming language created by Urban Müller around 1993.
This primer provides a complete specification of the Brainfuck language for this workshop.

Based on the specification from [brainfuck.org](http://brainfuck.org/brainfuck.html)

## Execution

A Brainfuck program consists of a string of ASCII characters.
The program manipulates an array of cells, each initially set to zero.
A movable pointer starts at the leftmost cell (index 0).

Execution begins at the first character and proceeds sequentially until every character is evaluated.

## Operators

Brainfuck has eight operators:

- `+`: Increment the value of the cell at the pointer.
- `-`: Decrement the value of the cell at the pointer.
- `>`: Move the pointer to the next cell to the right.
- `<`: Move the pointer to the next cell to the left.
- `[`: If the cell at the pointer is zero, jump to the command after the matching `]`.
- `]`: If the cell at the pointer is nonzero, jump to the command after the matching `[`.
- `.`: Output the value of the cell at the pointer.
- `,`: Input a byte and store it in the cell at the pointer.

## Simplified Input/Output Model

For this workshop, we use a simplified I/O model to prevent keyboard interactions or console logging.

- Input and output are `bytes`, typically represented as a `char` or `string`
- Input comes from an input string
- Output is sent to an output string

## Interpreter Assumptions

Brainfuck's specification forces the programmer to make some assumptions as not all behaviour is defined.
The following assumptions should make the interpreter fault-tolerant to most valid (syntax-wise) programs,
though it might not result in the correct output if the code was written for different assumptions.

- Cells are 8 bits / a byte long.
- Cells can under/over-flow (0 - 1 becomes 255, 255 + 1 becomes 0).
- The pointer wraps around to the beginning/end of the memory if it goes out of bounds.

This interpreter does not _need_ to handle:

- Unmatched `[` or `]` in the program.
- Dynamic memory length.
- Reading from input after all characters have been read already.
  - This will throw an error in the tests

## Annotated Example

```brainfuck
 1 +++++ +++               Set Cell #0 to 8
 2 [
 3     >++++               Add 4 to Cell #1; this will always set Cell #1 to 4
 4     [                   as the cell will be cleared by the loop
 5         >++             Add 4*2 to Cell #2
 6         >+++            Add 4*3 to Cell #3
 7         >+++            Add 4*3 to Cell #4
 8         >+              Add 4 to Cell #5
 9         <<<<-           Decrement the loop counter in Cell #1
10     ]                   Loop till Cell #1 is zero
11     >+                  Add 1 to Cell #2
12     >+                  Add 1 to Cell #3
13     >-                  Subtract 1 from Cell #4
14     >>+                 Add 1 to Cell #6
15     [<]                 Move back to the first zero cell you find; this will
16                         be Cell #1 which was cleared by the previous loop
17     <-                  Decrement the loop Counter in Cell #0
18 ]                       Loop till Cell #0 is zero
19 
20 The result of this is:
21 Cell No :   0   1   2   3   4   5   6
22 Contents:   0   0  72 104  88  32   8
23 Pointer :   ^
24 
25 >>.                     Cell #2 has value 72 which is 'H'
26 >---.                   Subtract 3 from Cell #3 to get 101 which is 'e'
27 +++++ ++..+++.          Likewise for 'llo' from Cell #3
28 >>.                     Cell #5 is 32 for the space
29 <-.                     Subtract 1 from Cell #4 for 87 to give a 'W'
30 <.                      Cell #3 was set to 'o' from the end of 'Hello'
31 +++.----- -.----- ---.  Cell #3 for 'rl' and 'd'
32 >>+.                    Add 1 to Cell #5 gives us an exclamation point
33 >++.                    And finally a newline from Cell #6
```