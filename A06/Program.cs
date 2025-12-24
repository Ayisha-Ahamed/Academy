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
      WriteLine ("The twelve unique solutions of 8 queens are: ");
      _ = new SolveEightQueens ();
   }
}

class SolveEightQueens {
   #region Constants-------------------------------------------------------------------------------
   // Dimension of chess board (n x n)
   const int DIM = 8;
   #endregion--------------------------------------------------------------------------------------

   #region Public Methods--------------------------------------------------------------------------
   // Prints chess board on the console
   void PrintBoard (int[] board) {
      WriteLine ($"\nSolution no: {mSolved.Count}");
      WriteLine ("┏━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┓");
      for (int row = 0; row < DIM; row++) {
         Write ("┃ ");
         int posOfQueen = board[row]; // Stores the position of queen in current row
         for (int col = 0; col < DIM; col++) Write (col == posOfQueen ? "♛ ┃ " : "  ┃ ");
         if (row != 7) WriteLine ("\n┣━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━┫");
      }
      WriteLine ("\n┗━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┛");
   }
   #endregion--------------------------------------------------------------------------------------

   #region Constructors----------------------------------------------------------------------------
   public SolveEightQueens () {
      // Place queen in first row nth column and solve
      for (int n = 0; n < DIM; n++) SolveNthQueen (1, [n]);
   }

   #endregion -------------------------------------------------------------------------------------

   #region Helper Methods-------------------------------------------------------------------------
   // Returns all possible column positions for nth row
   List<int> GetValidColPos (int row, List<int> sol) {
      List<int> validColPos = [];
      bool isValid;
      // Eliminate previously occupied column positions
      foreach (var col in Enumerable.Range (0, DIM).Except (sol)) {
         isValid = true;
         for (int i = 0; i < sol.Count; i++) {
            // Check if current column is safe from other queens diagonally
            if (Abs (row - i) == Abs (col - sol[i])) {
               isValid = false; break;
            }
         }
         if (isValid) validColPos.Add (col);
      }
      return validColPos;
   }

   // Returns true if the solution is canonical
   bool IsValid (int[] sol) => !(IsDuplicate (HMirror (sol)) || IsDuplicate (VMirror (sol)) || IsDuplicate (sol));

   // Adds canonical solution to mSolved
   void AddIfValid (List<int> n) {
      int[] sol = [.. n];
      if (!IsValid (sol)) return;
      for (int i = 0; i < 3; i++) {
         sol = Rotate (sol); // Rotate solution by 90°
         if (!IsValid (sol)) return;
      }
      mSolved.Add (sol);
      PrintBoard (sol);
   }

   // Returns true if the solution is duplicate
   bool IsDuplicate (int[] sol) => mSolved.Any (arr => arr.SequenceEqual (sol));

   // Returns the vertical mirror of the solution
   int[] VMirror (int[] sol) => [.. sol.Reverse ()];

   // Returns the horizontal mirror of the solution
   int[] HMirror (int[] sol) {
      var arr = new int[DIM];
      for (int i = 0; i < DIM; i++) arr[i] = DIM - sol[i] - 1;
      return arr;
   }

   // Returns solution rotated by 90° in clockwise direction
   int[] Rotate (int[] sol) {
      int[] arr = new int[DIM];
      for (int i = 0; i < DIM; i++) arr[sol[i]] = DIM - i - 1;
      return arr;
   }

   // Adds valid column position for nth queen with respect to current solution
   void SolveNthQueen (int n, List<int> currentSol) {
      if (n == DIM) {
         AddIfValid (currentSol);
         return;
      }
      // If multiple solutions are found, the current solution is copied to the next iteration
      foreach (var pos in GetValidColPos (n, currentSol))
         SolveNthQueen (n + 1, [.. currentSol, pos]);
   }
   #endregion--------------------------------------------------------------------------------------

   #region Private Data----------------------------------------------------------------------------
   // Stores the solved results
   static List<int[]> mSolved = [];
   #endregion
}