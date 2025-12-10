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
if (input.All (char.IsLetter) && input.Distinct ().Count () == NLETTERS) {
   var words = File.ReadLines ("words.txt")
   .Where (w => IsValid (w, input))
   .Select (GetScore) // Filter valid words into a tuple
   .OrderByDescending (w => w.Score)
   .ToList ();
   foreach (var word in words) {
      if (word.IsPangram) {
         ForegroundColor = ConsoleColor.Green;
         PrintScore (word);
         ResetColor ();
      } else PrintScore (word);
   }
   WriteLine ($"----\n{words.Sum (w => w.Score)} total");
} else WriteLine ("Invalid input. Please enter seven distinct alphabetic characters.");

// Returns a tuple consisting of input, whether the input is a pangram and its score
static (string Word, bool IsPangram, int Score) GetScore (string word) {
   bool isPangram = word.Distinct ().Count () == NLETTERS;
   int len = word.Length, score = len == 4 ? 1 : len + (isPangram ? NLETTERS : 0);
   return (word, isPangram, score);
}

// Returns if the word is a valid spelling bee solution for the given input
static bool IsValid (string w, string input) => w.Length > 3 && w.Contains (input[0]) && !w.Except (input).Any ();

// Prints the score to console
static void PrintScore ((string Word, bool IsPangram, int Score) tp) => WriteLine ($"{tp.Score,3}. {tp.Word}");