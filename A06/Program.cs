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
      var solver = new SolveEightQueens ();
      solver.PrintBoard ();
   }
}

class SolveEightQueens {
   #region Constants-------------------------------------------------------------------------------
   // Dimension of chess board (n x n).
   const int DIM = 8;
   #endregion--------------------------------------------------------------------------------------

   #region Public Methods--------------------------------------------------------------------------
   // Prints chess board on the console
   public void PrintBoard () {
      for (int n = 0; n < mSolved.Count; n++) {
         Clear ();
         List<int> board = mSolved[n]; // nth solution
         WriteLine ($"Solution no: {n + 1}");
         WriteLine ("\n┏━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┓");
         for (int row = 0; row < DIM; row++) {
            Write ("┃ ");
            int posOfQueen = board[row]; // Stores the position of queen in current row
            for (int col = 0; col < DIM; col++) Write (col == posOfQueen ? "♛ ┃ " : "  ┃ ");
            if (row != 7) WriteLine ("\n┣━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━┫");
         }
         WriteLine ("\n┗━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┛");
         Write ("Press 'N' to move next");
         while (ReadKey (true).Key != ConsoleKey.N) ;
      }
   }
   #endregion--------------------------------------------------------------------------------------

   #region Constructors----------------------------------------------------------------------------
   public SolveEightQueens () {
      if (mSolved.Count == 0) {
         // Place queen in first row ith column and calculate
         for (int i = 0; i < DIM; i++) SolveNthQueen (1, [i]);
      }
   }

   #endregion -------------------------------------------------------------------------------------

   #region Private Methods-------------------------------------------------------------------------
   // Returns all possible column positions for nth row
   static List<int> GetValidColPos (int row, List<int> sol) {
      List<int> validColPos = [];
      foreach (var col in Enumerable.Range (0, DIM).Except (sol)) {
         if (sol.Contains (col)) continue;
         bool isValid = true;
         for (int i = 0; i < sol.Count; i++) {
            // Verify column position is not affected by queens placed in diagonal position.
            if (Abs (row - i) == Abs (col - sol[i])) {
               isValid = false; break;
            }
         }
         if (isValid) validColPos.Add (col);
      }
      return validColPos;
   }

   // Adds list n to solution if n is canonical.
   static void AddIfValid (List<int> n) {
      int[] nRotated = [.. n];
      for (int i = 0; i < 4; i++) {
         nRotated = Rotate (nRotated);
         if (IsDuplicate (HMirror (nRotated)) || IsDuplicate (VMirror (nRotated)) || IsDuplicate (nRotated)) return;
      }
      mSolved.Add (n);
   }

   // Returns if the solution is duplicate.
   static bool IsDuplicate (int[] sol) => mSolved.Any (arr => arr.SequenceEqual (sol));

   // Returns the vertical mirror of array n.
   static int[] VMirror (int[] n) => [.. n.Reverse ()];

   // Returns the horizontal mirror of array n.
   static int[] HMirror (int[] n) {
      var arr = new int[DIM];
      for (int i = 0; i < DIM; i++) arr[i] = DIM - n[i] - 1;
      return arr;
   }

   // Returns n rotated by 90 degrees in clockwise direction.
   static int[] Rotate (int[] n) {
      int[] arr = new int[DIM];
      for (int i = 0; i < DIM; i++) arr[n[i]] = DIM - i - 1;
      return arr;
   }

   // Search for position(s) to place nth queen in nth row.
   static void SolveNthQueen (int n, List<int> currentSol) {
      if (n == DIM) {
         AddIfValid (currentSol);
         return;
      }
      // If multiple solutions are found, the current solution is copied to the next iteration.
      foreach (var pos in GetValidColPos (n, currentSol))
         SolveNthQueen (n + 1, [.. currentSol, pos]);
   }
   #endregion--------------------------------------------------------------------------------------

   #region Private Data----------------------------------------------------------------------------
   // Stores the solved results.
   static List<List<int>> mSolved = [];
   #endregion
}