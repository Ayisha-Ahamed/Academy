// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A07: Program to parse string into double.
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A07;

#region class Program ------------------------------------------------------------------------------
class Program {
   static void Main () {
      string[] testcase = ["23.","123.45","+123.45","++123.45" ,"123.45e.45", "-123.45e5", "123.45.45",
         "123.45e-4", "123.45e--4", "123e45e2", "123abc", "e34", ".32", "0.32", "abc23de", "1e", "++","-e"];
      foreach (var test in testcase) {
         bool isDouble = double.TryParse (test, out double parsed);
         Write ($"{test, -10}  |  ");
         PrintResult (TryParse (test, out double num) && num == parsed || !isDouble ? "Pass" : "Fail");
      }
      Write ("Press 'Y' to continue");
      while (ReadKey (true).Key == ConsoleKey.Y) {
         Clear ();
         Write ("Enter number: ");
         var input = (ReadLine () ?? "").Trim ().ToLower ();
         if (TryParse (input, out double num)) WriteLine (num);
         else WriteLine ("Not a double");
         Write ("Press 'Y' to continue");
      }
      // Method to print test results
      static void PrintResult (string result) {
         ForegroundColor = result == "Pass" ? ConsoleColor.Green : ConsoleColor.DarkRed;
         WriteLine (result);
         ResetColor ();
      }
      // Returns true if Parse implementation successfully converts input string to double
      static bool TryParse (string input, out double num) {
         num = 0;
         try {
            num = new Parse (input).Double;
         } catch {
            return false;
         }
         return true;
      }
   }
}
#endregion

#region class Parse --------------------------------------------------------------------------------
class Parse {
   #region Constructor --------------------------------------------
   /// <summary>
   /// Initialize class with input string. 
   /// Set the index to be processed to starting position. 
   /// Convert input string to double and store value in field variable.
   /// </summary>
   public Parse (string input) {
      mInput = input;
      mIdx = 0;
      mDouble = GetDouble ();
   }
   #endregion

   #region Properties ---------------------------------------------
   // Returns double value converted from input string
   public double Double => mDouble;
   #endregion

   #region Implementation -----------------------------------------
   // Returns the number converted from the string
   int GetLiteral () {
      int num = 0;
      while (mIdx < mInput.Length && char.IsDigit (mInput[mIdx]))
         num = (num * 10) + (mInput[mIdx++] - '0');
      return num;
   }

   // Returns signed number converted from string
   int GetSignedLiteral () {
      if (mIdx >= mInput.Length) throw new Exception ();
      int sign = mInput[mIdx] is '-' ? -1 : 1;
      if (mInput[mIdx] is '+' or '-') mIdx++;
      return GetLiteral () * sign;
   }

   // Returns double converted from input string
   double GetDouble () {
      double num = GetSignedLiteral (); // Get the whole part of the decimal number
      // Tracks the current state of the double processed
      EParsed prevState = EParsed.Whole;
      while (mIdx < mInput.Length) {
         switch (mInput[mIdx++]) {
            case '.': {
                  if (prevState is EParsed.Whole) {
                     int start = mIdx, len;
                     double fraction = GetLiteral () * Math.Pow (0.1, len = mIdx - start);
                     num += num < 0 ? -fraction : fraction;
                     // Round number up to converted decimal places
                     num = Math.Round (num, len);
                     prevState = EParsed.Fraction; break;
                  } else throw new Exception ();
               }
            case 'e': {
                  if (prevState is EParsed.Whole or EParsed.Fraction) {
                     num *= Math.Pow (10, GetSignedLiteral ());
                     prevState = EParsed.Exponent; break;
                  } else throw new Exception ();
               }
            default: throw new Exception ();
         }
      }
      return num;
   }
   #endregion

   #region Private Data -------------------------------------------
   readonly string mInput; // Copy of input string
   int mIdx;          // Stores the index position of input string to be processed
   double mDouble;    // Stores the number converted from input string
   #endregion
}
#endregion

#region enum EParsed -------------------------------------------------------------------------------
/// <summary>Defines the parsing states used when converting a numeric string into a double</summary>
public enum EParsed {
   /// <summary>Indicates that digits before decimal point (whole part) have been parsed</summary>
   Whole,
   /// <summary>Indicates that digits after decimal point (fractional part) have been parsed</summary>
   Fraction,
   /// <summary>Indicates that digits following 'e' or 'E' (exponent part) have been parsed</summary>
   Exponent
}
#endregion