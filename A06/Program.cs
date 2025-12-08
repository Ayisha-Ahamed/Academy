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
      OutputEncoding = new UnicodeEncoding ();
      List<List<int>> solved = [];
      for (int i = 0; i < 8; i++) {
         List<int> arr = [i]; // Place queen in first row ith column.
         SolveNthQueen (1, arr, solved); // Calculate positions for subsequent queens.
      }
      for (int n = 0; n < solved.Count; n++) {
         Clear ();
         WriteLine ($"Solution no: {n + 1}");
         PrintBoard (solved[n]);
         Write ("Press 'n' to move next");
         while (ReadKey (true).Key != ConsoleKey.N) ;
      }
   }

   // Returns all posible column positions for nth row
   static List<int> GetNthRowPos (int n, List<int> sol) {
      List<int> valid = [];
      for (int col = 0; col < 8; col++) {
         if (sol.Contains (col)) continue;
         bool isValid = true;
         for (int i = 0; i < sol.Count; i++)
            // Verify column position is not affected by queens placed in diagonal position.
            if (Abs (n - i) == Abs (col - sol[i])) {
               isValid = false; break;
            }
         if (isValid) valid.Add (col);
      }
      return valid;
   }

   // Adds list n to solution if n is canonical.
   static void AddIfValid (List<int> n, List<List<int>> solved) {
      int[] nref = n.ToArray ();
      for (int i = 0; i < 4; i++) {
         nref = Rotate (nref);
         if (IsDuplicate (HMirror (nref), solved) || IsDuplicate (VMirror (nref), solved) ||
               IsDuplicate (nref, solved)) return;
      }
      solved.Add (n);
   }

   // Returns if the solution is duplicate.
   static bool IsDuplicate (int[] n, List<List<int>> solved) {
      foreach (var sol in solved) if (sol.SequenceEqual (n)) return true;
      return false;
   }

   // Returns the vertical mirror of array n.
   static int[] VMirror (int[] n) => n.Reverse ().ToArray ();

   // Returns the horizontal mirror of array n.
   static int[] HMirror (int[] n) {
      var arr = new int[8];
      for (int i = 0; i < 8; i++) arr[i] = 7 - n[i];
      return arr;
   }

   // Returns n rotated by 90 degrees in clockwise direction.
   static int[] Rotate (int[] n) {
      int[] arr = new int[8];
      for (int i = 0; i < 8; i++) arr[n[i]] = 7 - i;
      return arr;
   }

   // Search for position(s) to place nth queen in nth row.
   static void SolveNthQueen (int n, List<int> currentSol, List<List<int>> solved) {
      if (n >= 8) {
         AddIfValid (currentSol, solved);
         return;
      }
      // If multiple solutions are found, the current solution is copied to the next iteration.
      foreach (var pos in GetNthRowPos (n, currentSol)) {
         List<int> newSol = [.. currentSol];
         newSol.Add (pos);
         SolveNthQueen (n + 1, newSol, solved);
      }
   }

   static void PrintBoard (List<int> b) {
      WriteLine ("┏━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┳━━━┓");
      for (int i = 0; i < 8; i++) {
         Write ("┃ ");
         for (int j = 0; j < 8; j++) Write (j == b[i] ? "♛ ┃ " : "  ┃ ");
         if (i != 7) WriteLine ("\n┣━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━╋━━━┫");
      }
      WriteLine ("\n┗━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┻━━━┛");
   }
}