// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// T01: CONSOLE TEXT EDITOR WITH UNDO REDO.
// ------------------------------------------------------------------------------------------------
using System.Text;

namespace T1;

class Program {
   static void Main (string[] args) {
      new StringBuilder ("hello").Remove (2, 3);
      Console.Write ("> ");
      var input = Console.ReadLine () ?? "";
      var editor = new TextEditor ();
      while (input != "EXIT") {
         var split = input.Split (" ");
         switch (split[0].ToUpper()) {
            case "ADD":
               editor.Add (split[1]); break;
            case "SHOW":
               editor.Display ();
               break;
            case "DELETE":
               if (int.TryParse (split[1], out int chars)) editor.Delete (chars);
               else Console.WriteLine ("Invalid input! Please enter number of characters to delete!"); break;
            case "UNDO": editor.Undo (); break;
            case "REDO": editor.Redo (); break;
            default: Console.WriteLine ("Invalid command"); break;
         }
         Console.Write ("> ");
         input = Console.ReadLine () ?? "";

      }
   }
}

class TextEditor {
   List<String> mEditor = new ();
   List<List<String>> mCopies = new (); // Store last save point

   int mCount => mEditor.Count;

   int mCurrentCopy;

   public TextEditor () => mCurrentCopy = 0;

   public void Add (string text) {
      mEditor.Add (text);
      mCopies.Add ([.. mEditor]);
   }

   public void Delete (int chars) {
      if (chars <= 0) {
         mCopies.Add ([.. mEditor]);
         return;
      }
      if (mCount == 0) {
         Console.WriteLine ("Editor is empty!");
         return;
      }
      var lastWord = mEditor[mCount - 1];
      int lastWordLen = lastWord.Length;
      if (chars < lastWordLen)
         mEditor[mEditor.Count - 1] = lastWord[..(lastWordLen - chars)];
      else if (chars >= lastWordLen) {
         if (mCount - 2 < 0) {
            Console.WriteLine ("Nothing to delete!");
            return;
         }
         mEditor.RemoveRange (mCount - 2, mCount - 1);
         //mCopies.Add ([.. mEditor]);
      }
      Delete (chars - lastWordLen);
   }

   public void Display () {
      foreach (var text in mEditor) {
         Console.Write (text);
      }
      Console.WriteLine ();
   }

   public void Undo () {
      int count = mCopies.Count;
      if (count > 1) {
         mCurrentCopy = count - 2;
         mEditor = mCopies[mCurrentCopy];
      } else {
         Console.WriteLine ("Nothing to undo!");
      }
   }

   public void Redo () {
      int count = mCopies.Count;
      if (mCurrentCopy + 1 < count) {
         mEditor = mCopies[++mCurrentCopy];
      } else {
         Console.WriteLine ("Nothing to redo!");
      }
   }
}
