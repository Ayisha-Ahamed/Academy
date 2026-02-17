// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A12: Wordle.
// Implementation of display elements for Wordle.
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A12 {
   #region class Display --------------------------------------------------------------------------
   // Class prints wordle input/output to console
   internal class Display (Wordle wordle, string word) {
      #region Methods -----------------------------------------------
      /// <summary>Align cursor for next input</summary>
      public void AlignCursorForInput () {
         MoveCursorToTop (mWordle.Tries);
         CursorLeft = CenterAlign + (mWordle.Length == 0 ? 0 : GridWidth);
         if (mWordle.Length != 5) Write (Mouse);
         else CursorLeft++;
      }

      /// <summary>Print entered letter to the console</summary>
      public void Print (char ch) { CursorLeft -= 1; Write (mWordle.Length == 4 ? $"{ch}" : $"{ch}  {Mouse}"); }

      /// <summary>Print input grid followed by alphabets</summary>
      public void PrintWindow () {
         int rowNo = mWordle.Tries - 1, rowsToPrint = mWordle.MaxTries - mWordle.Tries;
         MoveCursorToTop (rowNo);
         CursorLeft = CenterAlign;
         // Check if a valid word has been entered
         if (rowNo >= 0) PrintWord ();
         for (int i = 0; i < rowsToPrint; i++) {
            for (int j = 0; j < 5; j++) Write ($"{Dot}  ");
            MoveCursorToCenter ();
         }
         MoveCursorToCenter (8); // Align line with respect to width of the grid(13 * 0.5))
         WriteLine ($"{new string ('-', 30)}");
         PrintAlphabets ();
      }

      /// <summary>Print result corresponding to the error state</summary>
      public void PrintResult (EState state) {
         MoveCursorToTop (row: mWordle.MaxTries + 5);
         MoveCursorToCenter (moveLeft: 15);
         (string msg, ForegroundColor) = state switch {
            EState.IsInvalid => ($"{mWordle.Input,18} is not a word", ConsoleColor.Yellow),
            EState.IsFound => ($"{"",8}You found the word in {mWordle.Tries} tries", ConsoleColor.Green),
            EState.IsAWord => (new string (' ', 50), ConsoleColor.Gray), // Clean-up console print message section
            EState.IsNotFound => ($"{"",10}Sorry! The word was {mWord}", ConsoleColor.Yellow),
            _ => throw new Exception ("Result: Unknown error code")
         };
         WriteLine ($"{msg,15}");
         ResetColor ();
      }

      /// <summary>Update cursor position following backspace</summary>
      public void UpdateRow () {
         CursorLeft -= 1;
         if (mWordle.Length == 5) Write (Mouse);
         else { CursorLeft -= 3; Write ($"{Mouse}  {Dot}"); CursorLeft -= 3; }
      }
      #endregion

      #region Implementation ----------------------------------------
      // Align cursor to center position
      void MoveCursorToCenter (int moveLeft = 0) { Write ("\n\n"); CursorLeft = CenterAlign - moveLeft; }

      // Move cursor to current input row in grid
      void MoveCursorToTop (int row = 0) => SetCursorPosition (CenterAlign, (row < 0 ? 0 : row) * 2);

      // Prints alphabets to console
      void PrintAlphabets () {
         int maxLeft = CenterAlign + 20;
         MoveCursorToCenter (moveLeft: 8);
         for (char ch = 'A'; ch <= 'Z'; ch++) {
            if (ColorTable != null && ColorTable.TryGetValue (ch, out ConsoleColor color)) PrintLetter (ch, color);
            else PrintLetter (ch, ConsoleColor.Gray);
            if (CursorLeft > maxLeft) MoveCursorToCenter (8);
         }
      }

      // Prints a letter to console
      void PrintLetter (char ch, ConsoleColor color) {
         ForegroundColor = color;
         Write ($"{ch}  ");
         ResetColor ();
      }

      // Prints input word to console
      void PrintWord () {
         string input = mWordle.Input ?? "";
         Dictionary<char, Queue<ConsoleColor>> dict = [];
         for (int j = 0; j < input.Length; j++) {
            char ch = input[j];
            mFreq.TryGetValue (ch, out int count);
            ConsoleColor color = mWord[j] == ch ? ConsoleColor.Green
                                                : mWord.Contains (ch) ? ConsoleColor.Blue : ConsoleColor.DarkGray;
            if (!dict.TryGetValue (ch, out Queue<ConsoleColor>? queue)) dict.Add (ch, new ());
            else if (queue.Count >= count) color = ConsoleColor.DarkGray;
            dict[ch].Enqueue (color);
         }
         foreach (var ch in input) {
            ConsoleColor next = dict[ch].Dequeue ();
            if (ColorTable.TryGetValue (ch, out ConsoleColor prev)) {
               if (prev != next && next != ConsoleColor.DarkGray) ColorTable[ch] = next;
            } else ColorTable.Add (ch, next);
            PrintLetter (ch, next);
         }
         MoveCursorToCenter ();
      }
      #endregion

      #region Private data ------------------------------------------
      int CenterAlign = (int)(WindowWidth * 0.5) - 8;
      Dictionary<char, ConsoleColor> ColorTable = new (5);
      char Dot = '\u00b7', Mouse = '\u25cc';
      Dictionary<char, int> mFreq = word.Where (char.IsLetter).GroupBy (c => c).ToDictionary (c => c.Key, c => c.Count ());
      int GridWidth = 12; // Width of the wordle grid
      string mWord = word;
      Wordle mWordle = wordle;
      #endregion
   }
   #endregion
}