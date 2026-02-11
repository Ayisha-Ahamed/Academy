// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A14: Anagrams
// Program displays anagrams in words.txt
// ------------------------------------------------------------------------------------------------

namespace A14;

using static System.Console;

class Program {
   static void Main () {
      var query = File.ReadLines ("words.txt")
                      .GroupBy (a => new string ([.. a.Order ()]))
                      .Where (a => a.Count () > 1)
                      .OrderByDescending (a => a.Count ());
      foreach (var anagram in query) WriteLine ($"{string.Join (' ', anagram)}");
   }
}
