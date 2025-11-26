// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A02.1: Guessing game (MSB to LSB)
// Program prints the number the user thinks of (0 - 255) using binary search from MSB to LSB.
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;

do {
   Clear ();
   WriteLine ("Think of a number between 0 - 255");
   WriteLine ($"\nYour number is {FindNumber ()}");
   Write ("Press 'Y' to play again");
} while (ReadKey (true).Key == ConsoleKey.Y);

// Returns the number the user thinks of.
// Gets user response at each step.
static int FindNumber () {
   int max = 256, min = 0;
   StringBuilder num = new ();
   for (int i = 0; i < 8; i++) {
      int mid = (max + min) / 2;
      Write ($"Is the number less than {mid}? (y/n) ");
      switch (GetKey ()) {
         case ConsoleKey.Y: max = mid; num.Append ('0'); break;
         case ConsoleKey.N: min = mid; num.Append ('1'); break;
      }
   }
   return BinaryToInt (num.ToString ());
}

// Returns integer value of binary string.
static int BinaryToInt (string binStr) {
   int num = 0, len = binStr.Length, pow = len - 1;
   for (int i = 0; i < len; i++) {
      num += binStr[i] == '1' ? (int)Math.Pow (2, pow) : 0;
      pow--;
   }
   return num;
}

// Gets user response(Y/N) and returns the key pressed.
static ConsoleKey GetKey () {
   ConsoleKey key = ReadKey (true).Key;
   while (!(key is ConsoleKey.Y or ConsoleKey.N)) key = ReadKey (true).Key;
   Write ($"{(key == ConsoleKey.Y ? "Yes" : "No")}\n");
   return key;
}