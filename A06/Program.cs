// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A06: Program to print canonical solutions for 8 queens problem.
// ------------------------------------------------------------------------------------------------
namespace A06;

using System.Text;
using static System.Console;
using static Math;

class Program {
   static void Main () {
      OutputEncoding = Encoding.UTF8;
      new SolveEightQueen ();
   }
}

# region class SolveEightQueens --------------------------------------------------------------------
class SolveEightQueen {

   #region Constructors ----------------------------------------------
   public SolveEightQueen () => Solve (0, []);
   #endregion

   #region Implementation --------------------------------------------
   // Adds canonical solution to mSolution
   void AddValidSol (int[] sol) {
      for (int i = 0; i < 4; i++) {
         if (IsDuplicate (sol) || IsVerticalMirror (sol) || IsHorizontalMirror (sol)) return;
         if (i != 3) sol = Rotate (sol); // Rotate solution by 90°
      }
      mSolutions.Add (sol);
      PrintBoard (sol);
   }

   // Returns all column positions where a queen can be placed in the given row
   List<int> GetValidColPos (int row, int[] sol) {
      List<int> validColPos = [];
      bool isValid;
      // Eliminate previously occupied column positions
      foreach (var col in Enumerable.Range (0, NQUEENS).Except (sol)) {
         isValid = true;
         for (int i = 0; i < sol.Length; i++) {
            // Check if diagonal axis of current column position is occupied
            if (Abs (row - i) == Abs (col - sol[i])) {
               isValid = false; break;
            }
         }
         if (isValid) validColPos.Add (col);
      }
      return validColPos;
   }

   // Returns true if the solution is duplicate
   bool IsDuplicate (int[] sol) => mSolutions.Any (arr => arr.SequenceEqual (sol));

   // Returns true if the horizontal mirror of current solution is a duplicate solution
   bool IsHorizontalMirror (int[] sol) {
      var arr = new int[NQUEENS];
      for (int i = 0; i < NQUEENS; i++) arr[i] = NQUEENS - sol[i] - 1;
      return IsDuplicate (arr);
   }

   // Returns true if the vertical mirror of current solution is a duplicate solution
   bool IsVerticalMirror (int[] sol) => IsDuplicate ([.. sol.Reverse ()]);

   // Displays solution to the console
   void PrintBoard (int[] board) {
      Clear ();
      WriteLine ($"The twelve unique solutions of 8 queens are:\n\nSolution no: {mSolutions.Count}");
      WriteLine ("┏━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┓");
      for (int row = 0; row < NQUEENS; row++) {
         Write ("┃ ");
         int posOfQueen = board[row]; // Stores the position of queen in current row
         for (int col = 0; col < NQUEENS; col++) Write (col == posOfQueen ? "♛ ┃ " : "  ┃ ");
         if (row != 7) WriteLine ("\n┣━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━┫");
      }
      WriteLine ("\n┗━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┛");
      Write ("Press 'N' to move next");
      while (ReadKey (true).Key != ConsoleKey.N) ;
   }

   // Returns solution rotated by 90° (clockwise direction)
   int[] Rotate (int[] sol) {
      int[] arr = new int[NQUEENS];
      for (int i = 0; i < NQUEENS; i++) arr[sol[i]] = NQUEENS - i - 1;
      return arr;
   }

   // Calculates possible positions to place eight queens in the chess board
   // Variable n represents the row (ranges from 0 to 7) where the queen is to be placed
   public void Solve (int n, int[] sol) {
      // Return if all eight queens are placed
      if (n == NQUEENS) {
         // Add only canonical solution to the list
         AddValidSol (sol);
         return;
      }
      // Recursively call method for every valid column position
      foreach (var validCol in GetValidColPos (n, sol))
         // Update solution and solve for the next row
         Solve (n + 1, [.. sol, validCol]);
   }
   #endregion

   #region Constants -------------------------------------------------
   // Number of queens in the chess board
   const int NQUEENS = 8;
   #endregion

   #region Private Data ----------------------------------------------
   // List of canonical solutions
   List<int[]> mSolutions = [];
   #endregion
}
#endregion