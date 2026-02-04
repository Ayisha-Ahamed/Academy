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
         CursorLeft = CenterAlign + (mWordle.LCount == 0 ? 0 : GridWidth);
         if (mWordle.LCount != 5) Write (Mouse);
         else CursorLeft++;
      }

      /// <summary>Print entered letter to the console</summary>
      public void Print (char ch) {
         CursorLeft -= 1;
         Write (mWordle.LCount == 4 ? $"{ch}" : $"{ch}  {Mouse}");
      }

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
      public void PrintResult (int msgId) {
         MoveCursorToTop (row: mWordle.MaxTries + 5);
         MoveCursorToCenter (moveLeft: 15);
         (string msg, ForegroundColor) = msgId switch {
            -1 => ($"{mWordle.Input,18} is not a word", ConsoleColor.Yellow),
            0 => ($"{"",8}You found the word in {mWordle.Tries} tries", ConsoleColor.Green),
            1 => (new string (' ', 50), ConsoleColor.Gray), // Clean-up console print message section
            2 => ($"{"",10}Sorry! The word was {mWord}", ConsoleColor.Yellow), // Clean-up console print message section
            _ => throw new Exception ("Result: Unknown error code")
         };
         WriteLine ($"{msg,15}");
         ResetColor ();
      }

      /// <summary>Update cursor position following backspace</summary>
      public void UpdateRow () {
         CursorLeft -= 1;
         if (mWordle.LCount == 5) Write (Mouse);
         else {
            CursorLeft -= 3;
            Write ($"{Mouse}  {Dot}");
            CursorLeft -= 3;
         }
      }
      #endregion

      #region Implementation ----------------------------------------
      // Align cursor to center position
      void MoveCursorToCenter (int moveLeft = 0) {
         WriteLine ("\n");
         CursorLeft = CenterAlign - moveLeft;
      }

      // Move cursor to current input row in grid
      void MoveCursorToTop (int row = 0) => SetCursorPosition (CenterAlign, (row < 0 ? 0 : row) * 2);

      // Print alphabets to console
      void PrintAlphabets () {
         int maxLeft = CenterAlign + 20;
         MoveCursorToCenter (moveLeft: 8);
         for (char ch = 'A'; ch <= 'Z'; ch++) {
            if (ColorTable != null && ColorTable.TryGetValue (ch, out ConsoleColor color)) ForegroundColor = color;
            Write ($"{ch}  ");
            if (CursorLeft > maxLeft) MoveCursorToCenter (8);
            ResetColor ();
         }
      }

      // Prints valid word to console
      void PrintWord () {
         string input = mWordle.Input ?? "";
         for (int j = 0; j < input.Length; j++) {
            char ch = input[j];
            ConsoleColor col = mWord.Contains (ch) ? (mWord[j] == ch ? ConsoleColor.Green : ConsoleColor.Blue)
                                                   : ConsoleColor.DarkGray;
            // Update color table for printing alphabets
            if (ColorTable.TryGetValue (ch, out ConsoleColor color)) {
               // Update color if the letter is not previously found (Letters once found will always be printed green)
               if (color != ConsoleColor.Green || mWord.Where (l => l == ch).Count () > 1) ColorTable[ch] = color;
            } else ColorTable.Add (ch, col);
            ForegroundColor = col;
            Write ($"{ch}  ");
            ResetColor ();
         }
         MoveCursorToCenter ();
      }
      #endregion

      #region Private data ------------------------------------------
      int CenterAlign = (int)(WindowWidth * 0.5) - 8;
      Dictionary<char, ConsoleColor> ColorTable = new (5);
      char Dot = '\u00b7', Mouse = '\u25cc';
      string mWord = word;
      Wordle mWordle = wordle;
      int GridWidth = 12; // Width of the wordle grid
      #endregion
   }
   #endregion
}
