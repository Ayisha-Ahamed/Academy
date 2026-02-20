// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A15: Priority Queue
// Program tests PriorityQueue<int>
// ------------------------------------------------------------------------------------------------
using A15;

namespace TestQueue;

[TestClass]
public sealed class PriorityQueueTests {
   [TestMethod]
   public void TestDequeue () {
      List<int> test = [];
      // Dequeue empty list
      Assert.Throws<InvalidOperationException> (() => { new PriorityQueue<int> ().Dequeue (); });
      PriorityQueue<int> queue = new ();
      int len = new Random ().Next (10, 50); // Select an array of random length
      // Add reverse sorted elements to queue
      for (int i = len; i > 0; i--) queue.Enqueue (i);
      for (int i = len; i > 0; i--) test.Add (queue.Dequeue ());
      Assert.IsTrue (queue.IsEmpty);
      Assert.Throws<InvalidOperationException> (() => { queue.Dequeue (); });
      Assert.AreEqual (test.Count, len);
      int count = 0;
      // Check if array output is sorted in ascending order
      for (int i = 1; i <= len; i++) Assert.AreEqual (i, test[count++]);
   }

   [TestMethod]

   public void TestEnqueue () {
      PriorityQueue<int> queue = new ();
      List<int> test = [];
      Random random = new ();
      int len = random.Next (1, 50); // Select an array of random length
      for (int i = 0; i < len; i++) {
         if (random.NextDouble () > 0.5) {
            int value = random.Next (1, 10000);
            queue.Enqueue (value); test.Add (value);
         } else {
            if (!queue.IsEmpty) {
               int[] arr = [.. test]; int value = queue.Dequeue ();
               Array.Sort (arr);
               Assert.AreEqual (arr[0], value);
               test.Remove (value);
            } else Assert.IsEmpty (test);
         }
      }
   }
}