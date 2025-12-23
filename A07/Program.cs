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
      do {
         Clear ();
         Write ("Enter number: ");
         var input = ReadLine () ?? "";
         if (TryParse (input, out double num)) WriteLine (num);
         else WriteLine ("Not a double");
         Write ("Press 'Y' to continue");
      } while (ReadKey (true).Key == ConsoleKey.Y);
   }

   // Returns true if the input string is a valid double
   static bool TryParse (string input, out double num) {
      num = Double.NaN;
      try {
         num = new Parse (input).Value;
         return true;
      } catch {
         return false;
      }
   }

   // Extracting double from string takes place by dividing double into parts such as whole, fraction and exponent
   // Each part is made up of integers with whole and exponent parts having  possible signed values
   class Parse {
      #region Enums -------------------------------------------------------------------------------
      // Tracks the current part of the double (whole, fraction, exponent) processed by the methods
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
         int sign = 1;
         if (mText[mN] is '+' or '-')
            if (mText[mN++] is '-') sign = -1;
         return GetLiteral () * sign;
      }

      // Returns double converted from input string
      double GetDouble () {
         double parsed = GetSignedLiteral ();
         State prevState = State.Whole;
         while (mN < mText.Length) {
            char c = mText[mN++];
            switch (c) {
               case '.': {
                     if (prevState is State.Whole) {
                        int currentIndex = mN;
                        double fraction = GetLiteral () * Math.Pow (0.1, mN - currentIndex);
                        parsed += parsed < 0 ? -fraction : fraction;
                        prevState = State.Fraction; break;
                     } else throw new Exception ();
                  }
               case 'e': {
                     if (prevState is State.Whole or State.Fraction) {
                        parsed *= Math.Pow (10, GetSignedLiteral ());
                        prevState = State.Exponent; break;
                     } else throw new Exception ();
                  }
               default: throw new Exception ();
            }
         }
         return parsed;
      }
      #endregion

      #region Private Data ---------------------------------------------------------------------
      readonly string mText; // Stores input string
      int mN;          // Points to the next index position to be read
      double mValue;   // Stores double converted from string
      #endregion ----------------------------------------------------------------------------------
   }
}