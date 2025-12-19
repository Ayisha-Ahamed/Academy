// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A02.2: Guessing game (LSB to MSB).
// Program to find the number between 0 and 255 based on the user response.
// ------------------------------------------------------------------------------------------------
using static System.Console;

do {
   Clear ();
   Write ("Think of a number between 0 and 255\n");
   Write ($"\nYour number is {FindNumber ()}\nPress 'Y' to play again");
} while (ReadKey (true).Key == ConsoleKey.Y);

// Returns the user guessed number
static int FindNumber () {
   Write ("Is your number odd? (y/n) ");
   // If the number is odd, set LSB to 1
   int num = IsYPressed () ? 1 : 0, rem;
   // Prompts the user input to find (8 - n)th MSB where n ranges from 1 to 7
   for (int n = 1; n <= 7; n++) {
      rem = ShiftOneByN (n) + num;
      Write ($"Does your number leave a remainder of {rem} when divided by {ShiftOneByN (n + 1)}? (y/n) ");
      if (IsYPressed ()) num = rem;
   }
   return num;
}

// Returns if the user pressed 'Y'
static bool IsYPressed () {
   bool isYPressed = ReadKey (true).Key == ConsoleKey.Y;
   WriteLine (isYPressed ? "Yes" : "No");
   return isYPressed;
}

// Left shifts bit 1 by n positions
static int ShiftOneByN (int n) => 1 << n;