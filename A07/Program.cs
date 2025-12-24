// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A07: Program to parse string into double.
// ------------------------------------------------------------------------------------------------

using static System.Console;

namespace A07;

class Program {

   static void Main () {
      TestParse ();
      Write ("Press 'Y' to continue");
      while (ReadKey (true).Key == ConsoleKey.Y) {
         Clear ();
         Write ("Enter number: ");
         var input = (ReadLine () ?? "").Trim ().ToLower ();
         if (TryParse (input, out double num)) WriteLine (num);
         else WriteLine ("Not a double");
         Write ("Press 'Y' to continue");
      }
   }

   // Returns true if the input string is a valid double
   static bool TryParse (string input, out double num) {
      num = double.NaN;
      try {
         num = new Parse (input).Value;
         return true;
      } catch {
         return false;
      }
   }

   // Method to test Parse implementation
   static void TestParse () {
      ResetColor ();
      string[] testcase = ["123.45","+123.45" ,"123.45e.45", "-123.45e5", "123.45.45", "123.45e-4",
      "123e45e2", "123abc", "e34", ".32"];
      foreach (var test in testcase) {
         bool isDouble = double.TryParse (test, out double parsed);
         if (TryParse (test, out double num) && num == parsed || !isDouble)
            WriteLine ($"{test,-10}  |  Pass");
         else {
            ForegroundColor = ConsoleColor.DarkRed;
            WriteLine ($"{test,-10}  |  Fail");
            ResetColor ();
         }
      }
   }

   // Extracting double from string takes place by dividing double into parts such as whole(before decimal point),
   // fraction(after decimal point) and exponent. Each part is made up of integers with whole and exponent parts having
   // possible signed values
   class Parse {
      #region Enums -------------------------------------------------------------------------------
      enum State {
         Whole,
         Fraction,
         Exponent
      }
      #endregion

      #region Properties---------------------------------------------------------------------------
      // Returns double value converted from input string
      public double Value => mValue;
      #endregion

      #region Constructor -------------------------------------------------------------------------
      public Parse (string text) {
         mText = text;
         mN = 0;
         mValue = GetDouble ();
      }
      #endregion

      #region Private Methods ---------------------------------------------------------------------
      // Returns the number converted from the string
      int GetLiteral () {
         int num = 0;
         while (mN < mText.Length && char.IsDigit (mText[mN]))
            num = (num * 10) + (mText[mN++] - '0');
         return num;
      }

      // Returns signed number converted from string
      int GetSignedLiteral () {
         int sign = mText[mN] is '-' ? -1 : 1;
         if (mText[mN] is '+' or '-') mN++;
         return GetLiteral () * sign;
      }

      // Returns double converted from input string
      double GetDouble () {
         double num = GetSignedLiteral ();
         // Tracks the current part of the double (whole, fraction, exponent) processed by the methods
         State prevState = State.Whole;
         while (mN < mText.Length) {
            switch (mText[mN++]) {
               case '.': {
                     if (prevState is State.Whole) {
                        int start = mN, len;
                        double fraction = GetLiteral () * Math.Pow (0.1, len = mN - start);
                        num += num < 0 ? -fraction : fraction;
                        // Round number up to converted decimal places
                        num = Math.Round (num, len);
                        prevState = State.Fraction; break;
                     } else throw new Exception ();
                  }
               case 'e': {
                     if (prevState is State.Whole or State.Fraction) {
                        num *= Math.Pow (10, GetSignedLiteral ());
                        prevState = State.Exponent; break;
                     } else throw new Exception ();
                  }
               default: throw new Exception ();
            }
         }
         return num;
      }
      #endregion

      #region Private Data ---------------------------------------------------------------------
      readonly string mText; // Stores input string
      int mN;          // Points to the next index position to be read
      double mValue;   // Stores double converted from string
      #endregion ----------------------------------------------------------------------------------
   }
}