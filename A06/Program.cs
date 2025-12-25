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

# region Class SolveEightQueens--------------------------------------------------------------------
class SolveEightQueen {

   #region Constructors----------------------------------------------
   public SolveEightQueen () => SolveNthQueen (0, []);
   #endregion

   #region Helper Methods--------------------------------------------
   // Adds canonical solution to mSolution
   void AddValidSol (List<int> n) {
      int[] sol = [.. n];
      for (int i = 0; i < 4; i++) {
         if (IsDuplicate (sol) || IsVerticalMirror (sol) || IsHorizontalMirror (sol)) return;
         if (i != 3) sol = Rotate (sol); // Rotate solution by 90°
      }
      mSolutions.Add (sol);
      PrintBoard (sol);
   }

   // Returns all possible column positions for nth row
   List<int> GetValidColPos (int row, List<int> sol) {
      List<int> validColPos = [];
      bool isValid;
      // Eliminate previously occupied column positions
      foreach (var col in Enumerable.Range (0, NQUEENS).Except (sol)) {
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
      WriteLine ("The twelve unique solutions of 8 queens are: ");
      WriteLine ($"\nSolution no: {mSolutions.Count}");
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

   // Finds safe position for nth queen with respect to current solution
   public void SolveNthQueen (int n, List<int> currentSol) {
      if (n == NQUEENS) {
         AddValidSol (currentSol);
         return;
      }
      // Recursively call method for all possible positions of next queen 
      foreach (var pos in GetValidColPos (n, currentSol))
         SolveNthQueen (n + 1, [.. currentSol, pos]);
   }
   #endregion

   #region Constants-------------------------------------------------
   // Number of queens in the chess board
   const int NQUEENS = 8;
   #endregion

   #region Private Data----------------------------------------------
   // Stores the solved results
   List<int[]> mSolutions = [];
   #endregion
}
#endregion