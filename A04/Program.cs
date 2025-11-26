// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A04: Extension of Spelling Bee assignment.
// Program displays the seven most frequent letters used in words.txt.
// ------------------------------------------------------------------------------------------------
using static System.Console;

Dictionary<char, int> table = [];
foreach (var ch in File.ReadLines ("words.txt").SelectMany (a => a))
   table[ch] = 1 + (table.TryGetValue (ch, out int num) ? num : 0);
WriteLine ("The seven most frequently used letters are:");
foreach (var w in table.OrderByDescending (a => a.Value).Take (7)) WriteLine ($"{w.Key}: {w.Value}");