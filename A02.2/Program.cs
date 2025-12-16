// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A02.2: Guessing game (LSB to MSB).
// Program to find the number between 0 and 255 based on the user response.
// ------------------------------------------------------------------------------------------------
using static System.Console;

const int LSB = 1; // LSB of the number.

do {
   Clear ();
   Write ("Think of a number between 0 and 255\nIs your number odd? (y/n) ");
   Write ($"\nYour number is {FindNumber ()}\nPress 'Y' to play again");
} while (ReadKey (true).Key == ConsoleKey.Y);

// Returns the user guessed number
static int FindNumber () {
   // Initialize LSB of the number
   int num = IsYPressed () ? LSB : 0, rem;
   // Prompts the user input to find (8 - n)th MSB where n ranges from 1 to 7
   for (int n = 1; n <= 7; n++) {
      rem = (LSB << n) + num;
      Write ($"Does your number leave a remainder of {rem} when divided by {LSB << (n + 1)}? (y/n) ");
      if (IsYPressed ()) num = rem;
   }
   return num;
}

// Returns if the user pressed 'Y'.
static bool IsYPressed () {
   bool isYPressed = ReadKey (true).Key == ConsoleKey.Y;
   WriteLine (isYPressed ? "Yes" : "No");
   return isYPressed;
}