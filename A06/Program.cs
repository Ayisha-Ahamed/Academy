// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A06: Program to print solutions for 8 queens problem.
// ------------------------------------------------------------------------------------------------
namespace A06;

using System.Text;
using static System.Console;
using static Math;

class Program {
   static void Main () {
      OutputEncoding = new UnicodeEncoding ();
      List<List<Pos>> solutions = [];
      for (int col = 1; col < 3; col++) {
         List<Pos> arr = [new Pos (1, col)]; // Position of queen in row 1.
         SolveNthQueen (2, arr, solutions); // Calculate positions for subsequent queens.
      }
      int count = 1;
      foreach (var list in solutions) {
         Clear ();
         WriteLine ($"Solution no: {count++}");
         PrintBoard (list);
         Write ("Press 'n' to move next ");
         while (ReadKey (true).Key != ConsoleKey.N) ;
      }
   }

   // Returns all posible positions to place next queen in nth row.
   static List<Pos> GetNthRowPos (int row, List<Pos> sol) {
      List<Pos> valid = [];
      for (int col = 1; col <= 8; col++) {
         Pos q = new (row, col); // Queen position to check.
         bool isSafe = true; // Assume the current position is safe.
         foreach (var pos in sol) // Check if q is safe with respect to previous solution.
            if (!pos.IsSafe (q)) {
               isSafe = false; break;
            }
         if (isSafe) valid.Add (q);
      }
      return valid;
   }

   // Adds list of 8 position coordinates to solutions if valid.
   // Each iteration searches for position(s) to place nth queen in nth row(Rows range from 1 to 8).
   static void SolveNthQueen (int n, List<Pos> sol, List<List<Pos>> solutions) {
      if (n >= 9) {
         solutions.Add (sol);
         return;
      }
      // If multiple solutions are found, the current solution is copied to the next iteration
      // for each position and nth queen is solved respectively
      foreach (var pos in GetNthRowPos (n, sol)) {
         var newSol = new List<Pos> (sol) { pos };
         SolveNthQueen (n + 1, newSol, solutions);
      }
   }

   static void PrintBoard (List<Pos> b) {
      List<int> pos = [.. b.Select (a => a.ToInt)];
      WriteLine ("┏━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┓");
      for (int i = 1; i < 9; i++) {
         Write ("┃ ");
         for (int j = 1; j < 9; j++) {
            if (pos.Contains (i * 10 + j)) Write ($"♛ ┃ ");
            else Write ("  ┃ ");
         }
         if (i != 8) WriteLine ("\n┣━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━┫");
      }
      WriteLine ("\n┗━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┛");

   }

   // Structure to store the positions of queens in the board.
   class Pos (int row, int col) {
      #region Properties---------------------------------------------------------------------------
      public int Row = row;
      public int Col = col;
      public int ToInt => Row * 10 + Col; // Position coordinates stored as int.
      #endregion-----------------------------------------------------------------------------------
      #region Methods------------------------------------------------------------------------------
      // Returns if 'next' position is safe with respect to current position.
      public bool IsSafe (Pos next) =>
         Row != next.Row && Col != next.Col && Abs (Row - next.Row) != Abs (Col - next.Col);
      #endregion-----------------------------------------------------------------------------------
   }
}