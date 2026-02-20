// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A15: Priority Queue
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A15;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      int arrLength = 80;
      int[] arr = new int[arrLength];
      PriorityQueue<int> priorityQueue = new ();
      Random random = new ();
      bool result = true;
      for (int i = 0; i < arrLength; i++) {
         arr[i] = random.Next (500, 10000);
         priorityQueue.Enqueue (arr[i]);
      }
      Array.Sort (arr);
      for (int i = 0; i < arrLength; i++) {
         int priority = priorityQueue.Dequeue ();
         if (arr[i] != priority) {
            PrintResult (arr[i], priority); result = false;
            break;
         }
      }
      if (result) {
         ForegroundColor = ConsoleColor.Green;
         WriteLine ($"Test passed");
         ResetColor ();
      }

      static void PrintResult<T> (T expected, T actual) {
         ForegroundColor = ConsoleColor.Red;
         WriteLine ($"Expected: {expected}   Actual: {actual}");
         ResetColor ();
      }
   }
}
#endregion