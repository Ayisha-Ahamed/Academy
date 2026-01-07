// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A09.2: Implementation of circular queue
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A09._2;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      var queue = new TQueue<int> ();
      Random r = new ();
      int count = 0;
      for (int i = 1; i <= 100; i++) {
         if (r.NextDouble () < 0.5 && !queue.IsEmpty) Write ($"{queue.Dequeue ()} ");
         else queue.Enqueue (++count);
      }
      Write ($"\nCapacity of queue is: {queue.Capacity}");
   }
}
#endregion

#region class TQueue ------------------------------------------------------------------------------
class TQueue<T> {

   #region Constructors ---------------------------------------------
   /// <summary>Initialize array structure with capacity to hold two objects</summary>
   public TQueue () {
      mData = new T[2];
      mStart = 0;
      mNext = 0;
      mCount = 0;
   }
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Returns if the queue is empty</summary>
   public bool IsEmpty => mCount == 0;

   /// <summary>Gets the number of elements the structure can store without resizing</summary>
   public int Capacity => mCapacity;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Removes and returns first element from queue</summary>
   public T Dequeue () {
      // Check whether the list is empty
      if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
      T first = mData[mStart];
      mStart = (mStart + 1) % mCapacity; // Update start pointer
      mCount--;
      return first;
   }

   /// <summary>Adds element to the beginning of the queue</summary>
   public void Enqueue (T a) {
      // Resize if the circular buffer is full
      if (mCount == mCapacity) Resize ();
      // Add new element to the next empty space in the array
      mData[mNext] = a;
      mNext = (mNext + 1) % mCapacity;
      mCount++;
   }

   /// <summary>Returns the element at the beginning of the queue</summary>
   public T Peek () {
      if (IsEmpty) throw new InvalidOperationException ("Queue is empty");
      return mData[mStart];
   }
   #endregion

   #region Implementation -------------------------------------------
   // Resize array to add more objects to the queue
   void Resize () {
      int index = 0, len = mCapacity, count = mCount;
      var temp = new T[len * 2];
      // Copy existing elements to the new array
      while (count > 0) {
         temp[index++] = mData[mStart];
         mStart = (mStart + 1) % mCapacity;
         count--;
      }
      // Update data to represent newly allocated array
      mData = temp;
      mStart = 0;
      mNext = index;
   }
   #endregion

   #region Private Data ---------------------------------------------
   int mCapacity => mData.Length; // Returns the capacity of the array
   int mCount; // Number of objects stored in the queue
   T[] mData;  // Array structure to store queue elements
   int mNext;  // Points to next empty space to be used
   int mStart; // Points to the first element of the queue
   #endregion
}
#endregion