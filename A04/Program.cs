// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A04: Extension of Spelling Bee assignment.
// Program displays the seven most frequent letters used in words.txt.
// ------------------------------------------------------------------------------------------------
using static System.Console;

var list = string.Join ("", File.ReadLines ("words.txt"));
Dictionary<char, int> table = [];
foreach (var letter in list) {
   if (!table.TryGetValue (letter, out int num)) table[letter] = 0;
   table[letter]++;
}
WriteLine ("The seven most frequently used letters are:");
foreach (var w in table.OrderByDescending (a => a.Value).Take (7)) WriteLine ($"{w.Key}: {w.Value}");