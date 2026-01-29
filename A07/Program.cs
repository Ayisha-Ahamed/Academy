// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A07: Program to parse string into double.
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A07;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      TestParse ();
      Write ("Press 'Y' to continue");
      while (ReadKey (true).Key == ConsoleKey.Y) {
         Clear ();
         Write ("Enter number: ");
         var input = (ReadLine () ?? "").Trim ();
         WriteLine (TryParse (input, out double num) ? num : "Not a double");
         Write ("Press 'Y' to continue");
      }
      // Function to test class Parse
      static void TestParse () {
         string[] testcase = ["+.23", "23.", "123.45", "+123.45", "++123.45", "123.45e.45", "-123.45e5", "123.45.45",
            "-12345", "12e4", "123.45e-4", "123.45e--4", "123e45e2", "123abc", "e34",".e34","e", ".32", "0.32",
            "abc23de", "1e", "++", "-e", "", "1-23", "-12-3.45e3", "123.-4e4", "123.45e3.6", "abc", "123.g", "123e",
            "123+", "123-", "+-98", "12E3", "-123.-98", ".e-", "-e+", "123e4e5", "0", "2e+", "4e-", "+", "NaN", "naN"];
         WriteLine ($"  {"Input",-10}  |  {"Expected",-10}  |  {"Actual",-10}  |  Status");
         WriteLine (new string ('-', 55));
         foreach (var test in testcase) {
            bool isDouble = double.TryParse (test, out double num), isParsed = TryParse (test, out double parsed);
            Write ($"  {test,-10}  |  {num,-10}  |  {parsed,-10}  |  ");
            PrintResult ((isDouble == isParsed && num == parsed) ||
                         (double.IsNaN (num) && double.IsNaN (parsed)) ? "Pass" : "Fail");
         }
      }
      // Displays test results to the console
      static void PrintResult (string result) {
         ForegroundColor = result == "Pass" ? ConsoleColor.Green : ConsoleColor.DarkRed;
         WriteLine (result);
         ResetColor ();
      }
      // Returns true if Parse implementation successfully converts input string to double
      static bool TryParse (string input, out double num) {
         num = 0;
         try { num = ParseDouble.Parse (input); } catch { return false; }
         return true;
      }
   }
   #endregion
}

#region class ParseDouble -------------------------------------------------------------------------
class ParseDouble {
   #region Methods --------------------------------------------------
   /// <summary>Returns double converted from input string</summary>
   public static double Parse (string input) {
      if (input.ToLower () == "nan") return double.NaN;
      double num = 0; // Get the whole part of the decimal number
      int sign = 1,   // Stores the sign of the number/exponent
         exp = 0,     // Stores the exponent part
         dCount = 1,  // Stores the count of decimal numbers
         i = 0;  // Index position of input string being read
      EParsed state = EParsed.A; // Tracks the current state of parsing
      Action todo = () => { }, none = () => { };
      while (i < input.Length) {
         char ch = input[i++];
         (todo, state) = (ch, state) switch {
            ('+' or '-', EParsed.A or EParsed.F) => (() => { sign = ch == '+' ? 1 : -1; }, ++state),
            ( >= '0' and <= '9', EParsed.A or EParsed.B or EParsed.C) =>
                                 (() => { num = (num * 10) + (ch - '0'); }, EParsed.C),
            ('.', EParsed.A or EParsed.B or EParsed.C) => (none, EParsed.D),
            ( >= '0' and <= '9', EParsed.D or EParsed.E) =>
                                 (() => { num += (ch - '0') * Math.Pow (0.1, dCount); dCount++; }, EParsed.E),
            ('e' or 'E', EParsed.C or EParsed.E) => (() => { num *= sign; sign = 1; }, EParsed.F),
            ( >= '0' and <= '9', EParsed.F or EParsed.G or EParsed.H) =>
                                 (() => { exp = (exp * 10) + (ch - '0'); }, EParsed.H),
            _ => throw new Exception ("Not a double")
         };
         todo ();
      }
      return state switch {
         EParsed.C or EParsed.D => num * sign, // Input has only whole part eg: "12", "28."
         EParsed.E => sign * Math.Round (num, dCount), // Input has decimal part (without exponent) eg: "12.4", "0.8"
         EParsed.H => Math.Round (num, dCount) * Math.Pow (10, exp * sign), // Input has both decimal and exponent part
         _ => throw new Exception ("Not a double")
      };
   }
   #endregion

   #region Nested types ---------------------------------------------
   // Defines the parsing states used when converting a numeric string into a double
   enum EParsed { A, B, C, D, E, F, G, H }
   #endregion
}
#endregion