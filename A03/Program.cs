// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A03: Program that prints valid Spelling bee solutions for input string(7 alphabetic characters).
// Program takes first letter of the input string as must use character.
// ------------------------------------------------------------------------------------------------
using static System.Console;

Write ("Enter puzzle string: ");
var input = ReadLine () ?? "";
if (!string.IsNullOrEmpty (input) && input.Distinct ().Count () == 7 && input.All (char.IsLetter)) {
   var str = input.ToUpper ();
   // Filter valid words and group into pangrams and non-pangrams.
   // Boolean expression is used as key for grouping(Key is true for pangrams).
   var words = File.ReadAllLines ("words.txt")
      .Where (w => w.Length > 3 && w.Contains (str[0]) && !w.Except (str).Any ())
      .Select (w => (Word: w, IsPangram: w.Distinct ().Count () == 7))
      .GroupBy (w => w.IsPangram).Reverse ().ToList ();
   int total = 0;
   foreach (var group in words) {
      // Sort the elements in nth group in descending order of word length.
      if (group.Key == true) ForegroundColor = ConsoleColor.Green;
      foreach (var (Word, IsPangram) in group.OrderByDescending (a => a.Word.Length).ToList ()) {
         int len = Word.Length, score = len == 4 ? 1 : len + (IsPangram ? 7 : 0);
         total += score;
         WriteLine ($"{score,3}. {Word}");
      }
      ResetColor ();
   }
   WriteLine ($"----\n{total} total");
} else WriteLine ("Invalid input. Please enter 7 distinct alphabetic characters");