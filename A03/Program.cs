// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A03: Spelling Bee.
// Program prints Spelling bee solutions for seven letter input with first letter as the center.
// ------------------------------------------------------------------------------------------------
using static System.Console;

WriteLine ("Enter letters in pangram(uppercase): ");
var input = ReadLine ();
if (!string.IsNullOrWhiteSpace (input) && input.Distinct ().Count () == 7 && input.All (char.IsUpper)) {
   // Filter valid words and group into pangrams and non-pangrams.
   // Boolean expression is used as key for grouping(Key is true for pangrams).
   var words = File.ReadLines ("words.txt")
      .Where (w => w.Length > 3 && w.Contains (input[0]) && !w.Except (input).Any ())
      .Select (w => (Word: w, IsPangram: w.Distinct ().Count () == 7, w.Length))
      .Select (w => new { w.Word, w.IsPangram, Score = w.Length == 4 ? 1 : w.Length + (w.IsPangram ? 7 : 0) })
      .OrderByDescending (a => a.Score).GroupBy (a => a.IsPangram).ToList ();
   int total = 0;
   foreach (var group in words) {
      if (group.Key == true) ForegroundColor = ConsoleColor.Green;
      foreach (var w in group) {
         int score = w.Score;
         total += score;
         WriteLine ($"{score,3}. {w.Word}");
      }
      ResetColor ();
   }
   WriteLine ($"----\n{total} total");
} else WriteLine ("Invalid input. Please enter 7 distinct uppercase letters");