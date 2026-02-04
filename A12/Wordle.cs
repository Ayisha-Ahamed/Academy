// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A12: Wordle.
// Implementation of Wordle Crossword Puzzle.
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A12 {
   #region class Wordle ------------------------------------------------------------------------------
   class Wordle {
      #region Constructors ---------------------------------------------
      public Wordle () {
         string[] Puzzle = [.. File.ReadLines ("puzzle-5.txt")];
         Word = Puzzle[new Random ().Next (0, Puzzle.Length)];
         mDisplay = new Display (this, Word);
         Buffer = new char[5];
         Dictionary = [.. File.ReadAllLines ("dictionary-5.txt")];
         mDisplay.PrintWindow ();
      }
      #endregion

      #region Properties -----------------------------------------------
      public int MaxTries = 6;
      public int Tries = 0, LCount = 0;
      #endregion

      #region Public Methods -------------------------------------------
      public void Run () {
         while (Tries < 6) {
            Input = GetUserInput ();
            EState msgId = IsFound (Input) ? EState.IsFound : (IsAWord (Input) ? EState.IsAWord : EState.IsInvalid);
            if (msgId is not EState.IsInvalid) {
               Tries++; LCount = 0;
               mDisplay.PrintWindow ();
            }
            mDisplay.PrintResult ((int)msgId);
            if (msgId == EState.IsFound) return;
         }
         mDisplay.PrintResult ((int)EState.IsNotFound);
      }
      #endregion

      #region Implementation -------------------------------------------
      string GetUserInput () {
         ConsoleKeyInfo key;
         mDisplay.AlignCursorForInput ();
         while (true) {
            switch ((key = ReadKey (true)).Key) {
               case >= ConsoleKey.A and <= ConsoleKey.Z:
                  if (LCount < 5) {
                     Buffer[LCount] = char.ToUpper (key.KeyChar);
                     mDisplay.Print (Buffer[LCount]);
                     LCount++;
                  }
                  break;
               case ConsoleKey.Backspace:
                  if (LCount > 0) {
                     mDisplay.UpdateRow ();
                     LCount--;
                  }
                  break;
               case ConsoleKey.Enter:
                  if (LCount == 5) return new string (Buffer); break;
            }
         }
      }

      bool IsAWord (string str) => Dictionary.Any (a => a == str);

      bool IsFound (string word) => string.Equals (Word, word, StringComparison.OrdinalIgnoreCase);
      #endregion

      #region Nested Types ---------------------------------------------
      // Represents the state of the game following user input
      enum EState { IsInvalid = -1, IsFound, IsAWord, IsNotFound }
      #endregion

      #region Private Members ------------------------------------------
      string Word;
      char[] Buffer;
      public string? Input;
      SortedSet<string> Dictionary;
      Display mDisplay;
      #endregion
   }
   #endregion
}