// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A10.1: File name parser
// Implementation of file name parser with state machine.
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A10._1;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      TestFileParser ();
      Write ("All tests are completed. Press 'Y' to continue");
      while (ReadKey (true).Key == ConsoleKey.Y) {
         Clear ();
         Write ("Enter path: ");
         try {
            var file = FileParser (ReadLine () ?? "");
            WriteLine ($"Drive: {file.Drive}\nName: {file.Name}\nPath: {file.Path}\nExtension: {file.Ext}");
         } catch (Exception ex) {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine (ex.Message);
            ResetColor ();
         }
         Write ("Press 'Y' to continue");
      }

      // Method to test file parser
      static void TestFileParser () {
         (string fullpath, char drive, string path, string name, string ext, bool isfilepath)[] test = {
         ("C:/Work", ' ', "", "", "", false),   // Incomplete path
         ("5:/test.c", ' ', "", "", "", false), // Drive name is not an alphabet
         ("c:/Work/123Sample/test.c", ' ', "", "", "", false), // Drive is not a capital letter
         ("c:/Work/123Sample/test.", ' ', "", "", "", false),  // Incomplete file extension
         ("c:/Work/123Sample/.", ' ', "", "", "", false),
         ("c:/Work///sample.c", ' ', "", "", "", false), // Incomplete folder path
         ("C:/Work/W/sample.c", 'C', "Work/W", "sample", "c", true),
         ("C:/Work/test.c", 'C', "Work", "test", "c", true),
         ("C:/Work/Sample/test.c", 'C', "Work/Sample", "test", "c", true),
         ("C:/Work/123Sample/test.c", 'C', "Work/123Sample", "test", "c", true),
         ("C:/23Work/123Sample/test.c", 'C', "23Work/123Sample", "test", "c", true),
         ("C:/test.c", 'C', "", "test", "c", true)};
         foreach (var (fullpath, drive, path, name, ext, isfilepath) in test) {
            try {
               var fp = FileParser (fullpath);
               if (!(isfilepath && fp.Drive == drive && fp.Path == path && fp.Name == name && fp.Ext == ext)) {
                  WriteLine ($"    Path: {fullpath}");
                  WriteLine ($"Expected: \"{drive}\" \"{path}\" \"{name}\" \"{ext}\"");
                  WriteLine ($"  Actual: \"{fp.Drive}\" \"{fp.Path}\" \"{fp.Name}\" \"{fp.Ext}\"\n  Status: Fail\n\n");
               }
            } catch { if (isfilepath) WriteLine ($"{fullpath} \"{drive}\" \"{path}\" \"{name}\" \"{ext}\" - Fail(Crash)"); }
         }
      }
   }

   #region Implementation -------------------------------------------
   // Returns drive name, path, file name and file extension of the file path entered
   // Drive name must be a capital letter. File extensions should consist of lower case letters
   static (char Drive, string Path, string Name, string Ext) FileParser (string input) {
      Action todo, none = () => { };
      int name = 0, path = 0, ext = 0;
      EState state = EState.A;
      for (int i = 0; i < input.Length; i++) {
         (state, todo) = (state, input[i]) switch {
            (EState.A, >= 'A' and <= 'Z') => (EState.B, none),
            (EState.B, ':') => (EState.C, none),
            (EState.C, '/' or '\\') => (EState.D, () => { path = i + 1; }),
            (EState.D, (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or (>= '0' and <= '9')) => (EState.E, () => { name = i; }),
            (EState.E, (>= 'A' and <= 'Z') or (>= 'a' and <= 'z') or (>= '0' and <= '9')) => (EState.E, none),
            (EState.E, '/' or '\\') => (EState.D, none),
            (EState.E, '.') => (EState.F, () => { ext = i + 1; }),
            (EState.F, >= 'a' and <= 'z') => (EState.G, none),
            (EState.G, >= 'a' and <= 'z') => (EState.G, none),
            _ => throw new Exception ("Not a file path")
         };
         todo ();
      }
      if (state != EState.G) throw new Exception ("File path is incomplete");
      return (input[0], path == name ? "" : input[path..(name - 1)], input[name..(ext - 1)], input[ext..]);
   }
   #endregion

   #region enum EState ----------------------------------------------
   ///<summary>Represents the states in file parser</summary>
   public enum EState { A, B, C, D, E, F, G };
   #endregion
}
#endregion