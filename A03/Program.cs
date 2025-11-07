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
if (!string.IsNullOrEmpty (input) && input.Distinct ().Count () == 7
   && input.All (char.IsLetter)) {
   var str = input.ToUpper ();
   // Store the list of valid words paired with the number of letters used in the word.
   var list = File.ReadAllLines ("words.txt")
      .Where (word => word.Length > 3 && word.Contains (str[0]) && !word.Except (str).Any ())
      .Select (a => (Word: a, LCount: a.Distinct ().Count ())).GroupBy (a => a.LCount >= 7).Reverse ().ToList ();
   // Group list into pangrams and non-pangrams.
   // Boolean expression is used as Key for grouping(Key is true for pangrams). 
   int score = 0;
   foreach (var group in list) {
      // Sort the elements in nth group in descending order of word length.
      var order = group.OrderByDescending (a => a.Word.Length).ToList ();
      if (group.Key == true) ForegroundColor = ConsoleColor.Green;
      foreach (var (Word, LCount) in order) {
         int len = Word.Length;
         int length = len == 4 ? 1 : len + (group.Key == true ? 7 : 0);
         score += length;
         WriteLine ($"{length,3}. {Word}");
      }
      ResetColor ();
   }
   WriteLine ($"----\n{score} total");
} else WriteLine ("Invalid input. Please enter 7 distinct alphabetic characters");