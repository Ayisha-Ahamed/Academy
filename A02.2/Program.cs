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
   int num = 0, rem, pow;
   // Prompts the user input to find nth bit where n ranges from 0 to 7
   for (int n = 0; n <= 7; n++) {
      pow = PowerOfTwo (n); // Stores value of 2^n
      rem = pow + num;
      Write ($"Does your number leave a remainder of {rem} when divided by {2 * pow}? (y/n) ");
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

// Calculates nth power of two using left shift operator
static int PowerOfTwo (int n) => 1 << n;