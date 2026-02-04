// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A12: Wordle
// Program implements Wordle Crossword puzzle.
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;

namespace A12;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      CursorVisible = false;
      OutputEncoding = Encoding.UTF8;
      new Wordle ().Run ();
   }
}
#endregion