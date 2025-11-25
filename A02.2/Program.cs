// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A02.2: Guessing game (LSB to MSB).
// Program guesses the number between 0 - 255 by finding one bit at a time from LSB to MSB.
// ------------------------------------------------------------------------------------------------
using static System.Console;

WriteLine ("Think of a number between 0 - 255");
Write ("Is the number odd? (y/n) ");
WriteLine ($"\nYour number is {FindNumber (IsYPerssed ())}");

// Gets user response(Y/N) and returns if Y is pressed
static bool IsYPerssed () {
   ConsoleKey key = ReadKey (true).Key;
   while (!(key is ConsoleKey.Y or ConsoleKey.N)) key = ReadKey (true).Key;
   bool isYPressed = key == ConsoleKey.Y;
   Write ($"{(isYPressed ? "Yes" : "No")}\n");
   return isYPressed;
}

static int FindNumber (bool isOdd) {
   int num = isOdd ? 1 : 0;
   for (int i = 1; i < 8; i++) {
      int rem = (1 << i) + num, pow = 1 << (i + 1);
      Write ($"Does your number leave a remainder of {rem} when divided by {pow}? (y/n) ");
      num = IsYPerssed () ? rem : num;
   }
   return num;
}