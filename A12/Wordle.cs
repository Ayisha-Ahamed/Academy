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
   #region enum EState ----------------------------------------------------------------------------
   // Represents the state of the game following user input
   enum EState { IsInvalid = -1, IsFound, IsAWord, IsNotFound }
   #endregion

   #region class Wordle ---------------------------------------------------------------------------
   class Wordle {
      #region Constructors ------------------------------------------
      public Wordle () {
         string[] Puzzle = [.. File.ReadLines ("Data/puzzle-5.txt")];
         Word = Puzzle[new Random ().Next (0, Puzzle.Length)];
         mDisplay = new Display (this, Word);
         Buffer = new char[5];
         Dictionary = [.. File.ReadAllLines ("Data/dictionary-5.txt")];
         mDisplay.PrintWindow ();
      }
      #endregion

      #region Properties --------------------------------------------
      public int MaxTries = 6;
      public int Tries = 0, Length = 0;
      #endregion

      #region Public Methods ----------------------------------------
      public void Run () {
         while (Tries < 6) {
            Input = GetUserInput ();
            EState state = IsFound (Input) ? EState.IsFound : (IsAWord (Input) ? EState.IsAWord : EState.IsInvalid);
            if (state is not EState.IsInvalid) {
               Tries++; Length = 0;
               mDisplay.PrintWindow ();
            }
            mDisplay.PrintResult (state);
            if (state == EState.IsFound) return;
         }
         mDisplay.PrintResult (EState.IsNotFound);
      }
      #endregion

      #region Implementation ----------------------------------------
      string GetUserInput () {
         ConsoleKeyInfo key;
         mDisplay.AlignCursorForInput ();
         while (true) {
            switch ((key = ReadKey (true)).Key) {
               case >= ConsoleKey.A and <= ConsoleKey.Z:
                  if (Length < 5) {
                     Buffer[Length] = char.ToUpper (key.KeyChar);
                     mDisplay.Print (Buffer[Length]);
                     Length++;
                  }
                  break;
               case ConsoleKey.Backspace:
                  if (Length > 0) {
                     mDisplay.UpdateRow ();
                     Length--;
                  }
                  break;
               case ConsoleKey.Enter:
                  if (Length == 5) return new string (Buffer); break;
            }
         }
      }

      bool IsAWord (string str) => Dictionary.Any (a => a == str);

      bool IsFound (string word) => string.Equals (Word, word, StringComparison.OrdinalIgnoreCase);
      #endregion

      #region Private Members ---------------------------------------
      string Word;
      char[] Buffer;
      public string? Input;
      SortedSet<string> Dictionary;
      Display mDisplay;
      #endregion
   }
   #endregion
}