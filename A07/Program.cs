// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A07: Program to parse string into double.
// ------------------------------------------------------------------------------------------------

using System.Text.RegularExpressions;
using static System.Console;

namespace A07;

class Program {
   static Regex regex = new (@"^(\+|-)?([0-9]*)(\.[0-9]+)?((e|E)(\+|-)?[0-9]+)?$");

   static void Main () {
      do {
         Clear ();
         Write ("Enter number: ");
         var input = ReadLine () ?? "";
         if (regex.IsMatch (input)) WriteLine (Parse (input.ToLower ()));
         else WriteLine ("Entered value is not a double");
         Write ("Press 'Y' to continue");
      } while (ReadKey (true).Key == ConsoleKey.Y);
   }

   // Returns double converted from input string.
   static double Parse (string str) {
      double num = 0;
      // Length of number without exponent.
      int nLen = str.Contains ('e') ? str.IndexOf ('e') : str.Length;
      string dec = str[0] == '+' || str[0] == '-' ? str[1..nLen] : str[..nLen];
      // Calculate index position of decimal point.
      int point = dec.Contains ('.') ? dec.IndexOf ('.') : dec.Length, e = 0;
      // Conversion of significant digits to double.
      foreach (var ch in dec.Where (char.IsDigit)) num += (ch - '0') * Math.Pow (10, --point);
      // Conversion of exponent component to double.
      if (str.Length > nLen + 1) {
         foreach (var c in str[nLen..].Where (char.IsDigit)) e = (e * 10) + (c - '0');
         num = num * Math.Pow (10, str[nLen + 1] == '-' ? -e : +e);
      }
      return str[0] == '-' ? -num : num;
   }
}