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

// Returns the random number calculated from user response.
static int FindNumber () {
   int max = 256, min = 0, num = 0, mid, bitlen = 7;
   for (int i = 0; i <= bitlen; i++) {
      mid = (max + min) / 2;
      Write ($"\nIs the number less than {mid}? (y/n) ");
      bool choice = ReadKey (true).Key == ConsoleKey.Y;
      Write (choice ? "Yes" : "No");
      if (!choice) {
         num += (int)Math.Pow (2, bitlen - i); // Converts binary to decimal.
         min = mid;
      } else max = mid;
   }
   return num;
}