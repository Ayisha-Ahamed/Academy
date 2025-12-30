// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A03: Spelling Bee.
// Program prints spelling bee solutions for given input with first letter as must-use character.
// ------------------------------------------------------------------------------------------------
using static System.Console;

const int NLETTERS = 7; // Number of letters in spelling bee puzzle

Write ("Enter seven distinct letters: ");
string input = (ReadLine () ?? "").Trim ().ToUpper ();
int total = 0; // Stores the total score of the words in the solution
if (input.All (char.IsLetter) && IsPangram (input)) {
   IEnumerable<WordResult> result = File.ReadLines ("words.txt").Where (w => IsValid (w, input))
                                        .Select (EvaluateWord).OrderByDescending (w => w.Score);
   foreach (var entry in result) {
      ForegroundColor = entry.Color;
      // Right align score with a width of 3
      WriteLine ($"{entry.Score, 3}. {entry.Word}");
      total += entry.Score;
   }
   ResetColor ();
   WriteLine ($"----\n{total} total");
} else WriteLine ("Invalid input. Please enter seven distinct alphabetic characters.");

// Returns a structure with properties of the word such as console color, word and score
static WordResult EvaluateWord (string word) {
   (ConsoleColor color, int score) = IsPangram (word) ? (ConsoleColor.Green, NLETTERS) : (ConsoleColor.Gray, 0);
   int len = word.Length;
   return new WordResult { Color = color, Word = word, Score = score + (len == 4 ? 1 : len) };
}

// Returns true if the word is made up of seven distinct characters
static bool IsPangram (string word) => word.Distinct ().Count () == NLETTERS;

// Returns true if the word is a valid spelling bee solution for the given input
static bool IsValid (string word, string input) => word.Length > 3 && word.Contains (input[0])
                                                   && !word.Except (input).Any ();

// Represents the result after analyzing the word
struct WordResult {
   /// <summary>Represents the console color of the word to be displayed</summary>
   public ConsoleColor Color;

   /// <summary>Represents the word to be displayed</summary>
   public string Word;

   /// <summary>Represents the score of the word</summary>
   public int Score;
}