// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A03: Spelling Bee.
// Program prints Spelling bee solutions for input with first input letter as must-use character.
// ------------------------------------------------------------------------------------------------
using static System.Console;

WriteLine ("Enter seven distinct letters: ");
// Remove trailing spaces.
// Null coalescent operator is used to make sure input is not null before Trim() operation.
var input = (ReadLine () ?? "").Trim ().ToUpper ();
// Filter valid words.
if (input.All (char.IsLetter) && input.Distinct ().Count () == 7) {
   var words = File.ReadLines ("words.txt")
   .Where (w => w.Length > 3 && w.Contains (input[0]) && !w.Except (input).Any ())
   .Select (w => {
      bool isPangram = w.Distinct ().Count () == 7;
      int len = w.Length, score = len == 4 ? 1 : len + (isPangram ? 7 : 0);
      return (Word: w, IsPangram: isPangram, Score: score);
   }).OrderByDescending (w => w.Score);
   foreach (var (Word, IsPangram, Score) in words) {
      if (IsPangram) ForegroundColor = ConsoleColor.Green;
      WriteLine ($"{Score,3}. {Word}");
      ResetColor ();
   }
   WriteLine ($"----\n{words.Sum (w => w.Score)} total");
} else WriteLine ("Invalid input. Please enter seven distinct alphabetic characters.");