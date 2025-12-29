// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// T02: DIAL ROTATION PUZZLE.
// ------------------------------------------------------------------------------------------------
namespace T2;

class Program {
   static void Main () {
      var passwordFinder = new FindPassword ("dial.txt");
      Console.WriteLine (passwordFinder.Password);
   }
}

class FindPassword {
   public int Password => mPassword;
   int mPassword;
   int mDial;

   /// <summary>Reads dial instructions from filepath and returns the password</summary>
   public FindPassword (string filePath) {
      mPassword = 0; mDial = 50;
      foreach (var position in File.ReadLines (filePath)) {
         if (!int.TryParse (position[1..], out int num))
            Console.WriteLine ("Not a valid position");
         else {
            switch (position[0]) {
               case 'L': MoveLeft (num); break;
               case 'R': MoveRight (num); break;
               default : Console.WriteLine ("Invalid format"); break;
            }
            if (mDial == 0) mPassword++;
         }
      }
   }

   // Moves dial to the left
   void MoveLeft (int pos) {
      int temp = mDial - pos;
      if (temp < 0) temp = 100 + temp;
      mDial = temp;
   }

   // Moves dial to the right
   void MoveRight (int pos) {
      int temp = mDial + pos;
      if (temp > 99) temp = temp - 100;
      mDial = temp;
   }
}