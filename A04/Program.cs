// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A04: Extension of Spelling Bee assignment.
// Program to build a frequency table with words from words.txt
// Program displays the first seven frequent letters from the frequency table.
// ------------------------------------------------------------------------------------------------
using static System.Console;

var list = File.ReadAllLines ("words.txt");
Dictionary<char, int> table = [];
for (char c = 'A'; c <= 'Z'; c++) table[c] = 0; // Initialize dictionary with value zero.
foreach (var word in list) foreach (char c in word.Where (char.IsLetter)) table[c]++;
foreach (var w in table.OrderByDescending (a => a.Value).Take (7)) WriteLine ($"{w.Key}: {w.Value}");