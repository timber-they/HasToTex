# HasToTex
A Haskell to LaTeX converter in .NET Core.

## How does it work?
This works as follows:
1. Abstract Haskell code to Haskell POJOs
2. Parse Haskell POJOs to LaTeX POJOs
3. De-abstract LaTeX POJOs to LaTeX code

## What is this not?
This does not validate the Haskell code for validity.

## What does this _currently_ (at the time of writing) do?
* Strip down Haskell code to its keywords (not the actual POJOs)
