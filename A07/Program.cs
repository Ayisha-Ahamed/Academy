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
         string[] testcase = ["23.","123.45","+123.45","++123.45" ,"123.45e.45", "-123.45e5", "123.45.45",
         "123.45e-4", "123.45e--4", "123e45e2", "123abc", "e34",".e34","e", ".32", "0.32", "abc23de", "1e", "++","-e","",
         "1-23","-12-3.45e3","123.-4e4","123.45e3.6","abc","123.g","123e","123+","123-","+-98","12E3","-123.-98",".e-","-e+",
         "123e4e5","0","2e+","4e-","+", "NaN"];
         foreach (var test in testcase) {
            bool isDouble = double.TryParse (test, out double num), isParsed = TryParse (test, out double parsed);
            Write ($"{test,-10}  |  {num,-10}  |  {parsed,-10}  |  ");
            PrintResult (isDouble == isParsed && num == parsed ? "Pass" : "Fail");
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
         try { num = new Parse (input).Double; } catch { return false; }
         return true;
      }
   }
}
#endregion

#region class Parse -------------------------------------------------------------------------------
class Parse {
   #region Constructor ----------------------------------------------
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

   #region Properties -----------------------------------------------
   /// <summary>Returns double value converted from input string</summary>
   public double Double => mDouble;
   #endregion

   #region Implementation -------------------------------------------
   // Returns the number converted from the string
   int GetNum () {
      int num = 0;
      while (mIdx < mInput.Length && char.IsDigit (mInput[mIdx]))
         num = (num * 10) + (mInput[mIdx++] - '0');
      return num;
   }

   // Returns signed number converted from string
   int GetSignedNum () {
      int sign = mInput[mIdx] is '+' or '-' && mInput[mIdx++] is '-' ? -1 : 1, start = mIdx, num = GetNum();
      if (mIdx - start == 0) throw new Exception ("Sign operator should be followed by an integer");
      return sign * num;
   }

   // Returns double converted from input string
   double GetDouble () {
      double num = 0; // Get the whole part of the decimal number
      // Tracks the current state of the double processed
      EParsed state = EParsed.None;
      while (mIdx < mInput.Length) {
         switch (mInput[mIdx], state) {
            case ('+' or '-' or (>= '0' and <= '9'), EParsed.None):
               num = GetSignedNum ();
               state = EParsed.Whole; break;
            case ('.', EParsed.None or EParsed.Whole):
               int start = ++mIdx, len;
               double f = GetNum () * Math.Pow (0.1, len = mIdx - start);
               if (len == 0 && start != mInput.Length)
                  throw new Exception ("A decimal point should follow atleast one number");
               num += num < 0 ? -f : f;
               // Round number up to converted decimal places
               num = Math.Round (num, len);
               state = EParsed.Fraction; break;
            case ('e' or 'E', EParsed.Whole or EParsed.Fraction):
               mIdx++;
               num *= Math.Pow (10, GetSignedNum ());
               state = EParsed.Exponent; break;
            default: throw new Exception ("Not a double");
         }
      }
      if (state == EParsed.None) throw new Exception ("Input is empty");
      return num;
   }
   #endregion

   #region Private Data ---------------------------------------------
   readonly string mInput; // Copy of input string
   int mIdx;          // Stores the index position of input string to be processed
   double mDouble;    // Stores the number converted from input string
   #endregion
}
#endregion

#region enum EParsed ------------------------------------------------------------------------------
/// <summary>Defines the parsing states used when converting a numeric string into a double</summary>
public enum EParsed {
   None,
   // Indicates that digits before decimal point (whole part) have been parsed</summary>
   Whole,
   // Indicates that digits after decimal point (fractional part) have been parsed</summary>
   Fraction,
   // Indicates that digits following 'e' or 'E' (exponent part) have been parsed</summary>
   Exponent
}
#endregion