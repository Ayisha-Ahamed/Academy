// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A02.1: Guessing game (MSB to LSB)
// Program guesses a random number (0-255) using binary search from MSB to LSB.
// ------------------------------------------------------------------------------------------------
using static System.Console;

do {
   Clear ();
   Write ($"Think of a number between 0 - 255");
   Write ($"\nYour number is {FindNumber ()}\nPress 'Y' to play again");
} while (ReadKey (true).Key == ConsoleKey.Y);

// Returns the user guessed number
static int FindNumber () {
   int max = 256, min = 0, num = 0, mid, bitLen = 7;
   bool isLess;
   for (int i = 0; i <= bitLen; i++) {
      mid = (max + min) / 2;
      Write ($"\nIs the number less than {mid}? (y/n) ");
      Write ((isLess = ReadKey (true).Key == ConsoleKey.Y) ? "Yes" : "No");
      if (isLess) max = mid;
      else {
         num += (int)Math.Pow (2, bitLen - i); // Converts binary to decimal
         min = mid;
      }
   }
   return num;
}