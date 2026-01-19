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
      TestEvaluator ();
      var eval = new Evaluator ();
      for (; ; ) {
         Write ("> ");
         string input = (ReadLine () ?? "").Trim ().ToLower ();
         if (input == "exit") break;
         try {
            double result = eval.Evaluate (input);
            ForegroundColor = ConsoleColor.Blue;
            WriteLine (result);
            ResetColor ();
         } catch (Exception e) {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine (e.Message);
            ResetColor ();
         }
      }
   }

   public static void TestEvaluator () {
      Dictionary<string, double> test = new Dictionary<string, double> {
         { "1", 1 },
         { "a = 51", 51 },
         { "(5)", 5 },
         { "a / 10", 5.1 },
         { "8 + 5 * 30", 158 },
         { "(8 + 5) * 30", 390 },
         { "8 * (5 + 30)", 280 },
         { "(8* (5 - 30)) / -200", 1 },
         { "((5 * 9) + 6) * 98", 4998 },
         { "b = ((9 + 5) * 56)", 784 },
         { "b = b / 56", 14 },
         { "c = -b", -14 },
      };
      var eval = new Evaluator ();
      foreach (var str in test) {
         double result = eval.Evaluate (str.Key);
         if (result != str.Value) {
            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine ($"{str.Key} Expected: {str.Value} Actual: {result}");
            ResetColor ();
         }
      }
   }
}
#endregion