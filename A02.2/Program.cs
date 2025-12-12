// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A02.2: Guessing game (LSB to MSB).
// Program to find the number (0 - 255) based on the user response.
// ------------------------------------------------------------------------------------------------
using static System.Console;

const int NBIT = 1; // Nth bit value. Bits range from 0 - 7 for finding numbers below 256

do {
   Clear ();
   Write ("Think of a number between 0 and 255\nIs your number odd? (y/n) ");
   Write ($"\nYour number is {FindNumber ()}\nPress 'Y' to play again");
} while (ReadKey (true).Key == ConsoleKey.Y);

// Returns the user guessed number
static int FindNumber () {
   // Initialize LSB of the number
   int num = IsYes () ? NBIT : 0, rem;
   // Prompts user input to find nth MSB
   for (int n = 1; n <= 7; n++) {
      rem = (NBIT << n) + num;
      Write ($"Does your number leave a remainder of {rem} when divided by {NBIT << (n + 1)}? (y/n) ");
      if (IsYes ()) num = rem;
   }
   return num;
}

// Returns user response (Y/N)
static bool IsYes () {
   bool isYPressed = ReadKey (true).Key == ConsoleKey.Y;
   WriteLine (isYPressed ? "Yes" : "No");
   return isYPressed;
}