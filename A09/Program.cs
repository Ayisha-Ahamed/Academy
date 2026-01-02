// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program implements expression evaluator.
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A09;

#region class Program -----------------------------------------------------------------------------
class Program {
   public static void Main () {
      ResetColor();
      var eval = new Evaluator ();
      for (; ; ) {
         Write ("> ");
         string input = (ReadLine () ?? "").Trim ().ToLower ();
         if (input == "exit") break;
         try {
            double result = eval.Evaluate (input);
            ForegroundColor = ConsoleColor.Green;
            WriteLine (result);
            ResetColor ();
         } catch (Exception e) {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine (e.Message);
            ResetColor ();
         }
      }
   }
}
#endregion