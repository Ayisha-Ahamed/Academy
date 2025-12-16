// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A03: Spelling Bee.
// Program prints spelling bee solutions for given input with first letter as must-use character.
// ------------------------------------------------------------------------------------------------
using static System.Console;

const int NLETTERS = 7; // Number of letters in spelling bee puzzle

Write ("Enter seven distinct letters: ");
string input = (ReadLine () ?? "").Trim ().ToUpper ();
if (input.All (char.IsLetter) && IsPangram (input)) {
   List<(ConsoleColor Color, string Word, int Score)> words = [.. File.ReadLines ("words.txt").Where (w => IsValid (w, input))
                                                              .Select (GetScore).OrderBy (w => w.Color)
                                                              .ThenByDescending (w => w.Score)];
   foreach (var (Color, Word, Score) in words) {
      ForegroundColor = Color;
      WriteLine ($"{Score,3}. {Word}");
   }
   WriteLine ($"----\n{words.Sum (w => w.Score)} total");
} else WriteLine ("Invalid input. Please enter seven distinct alphabetic characters.");

// Returns a tuple consisting of input, foreground color and score
static (ConsoleColor Color, string Word, int Score) GetScore (string word) {
   (ConsoleColor color, int score) = IsPangram (word) ? (ConsoleColor.Green, NLETTERS) : (ConsoleColor.White, 0);
   int len = word.Length;
   return (color, word, score + (len == 4 ? 1 : len));
}

// Returns if the word is made up of seven distinct characters
static bool IsPangram (string word) => word.Distinct ().Count () == NLETTERS;

// Returns if the word is a valid spelling bee solution for the given input
static bool IsValid (string w, string input) => w.Length > 3 && w.Contains (input[0]) && !w.Except (input).Any ();