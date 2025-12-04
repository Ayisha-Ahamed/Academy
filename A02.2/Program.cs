// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A02.2: Guessing game (LSB to MSB).
// Program prints the random number (0 - 255) calculated from LSB to MSB through user response.
// ------------------------------------------------------------------------------------------------
using static System.Console;

do {
   Clear ();
   Write ("Think of a number between 0 - 255\nIs the number odd? (y/n) ");
   Write ($"\nYour number is {FindNumber ()}\nPress 'Y' to continue");
} while (ReadKey (true).Key == ConsoleKey.Y);

// Returns user response (Y/N)
static ConsoleKey GetKey () {
   ConsoleKey key = ReadKey (true).Key;
   while (!(key is ConsoleKey.Y or ConsoleKey.N)) key = ReadKey (true).Key;
   WriteLine (key == ConsoleKey.Y ? "Yes" : "No");
   return key;
}

// Returns the number calculated from user response.
static int FindNumber () {
   int num = GetKey () == ConsoleKey.Y ? 1 : 0, rem, div; // LSB of the number.
   // Prompts user input to find ith MSB.
   for (int i = 1; i < 8; i++) {
      rem = (1 << i) + num; div = 1 << (i + 1);
      Write ($"Does your number leave a remainder of {rem} when divided by {div}? (y/n) ");
      if (GetKey () == ConsoleKey.Y) num = rem;
   }
   return num;
}