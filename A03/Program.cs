// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A03: Spelling Bee.
// Program prints spelling bee solutions for the given input with first input letter
// as must-use character.
// ------------------------------------------------------------------------------------------------
using static System.Console;

Write ("Enter seven distinct letters: ");
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
      string str = $"{Score,3}. {Word}";
      if (IsPangram) PrintPangram (str);
      else WriteLine (str);
   }
   WriteLine ($"----\n{words.Sum (w => w.Score)} total");
} else WriteLine ("Invalid input. Please enter seven distinct alphabetic characters.");

static void PrintPangram (string str) {
   ForegroundColor = ConsoleColor.Green;
   WriteLine (str);
   ResetColor ();
}