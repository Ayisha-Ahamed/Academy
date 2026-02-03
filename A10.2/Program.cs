// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A10.1: Implementation of Double ended circular queue.
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A10._1;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      var queue1 = new TQueue<int> ();
      var queue2 = new TQueue<int> ();
      Random r = new ();
      for (int i = 1; i <= 17; i++)
         queue1.EnqueueHead (i);
      Write ("Queue 1: ");
      while (!queue1.IsEmpty) {
         int head = queue1.DequeueTail ();
         Write ($"{head} ");
         queue2.EnqueueTail (head);
      }
      Write ("\nQueue 2: ");
      while (!queue2.IsEmpty) Write ($"{queue2.DequeueHead ()} ");
      WriteLine ($"\nCapacity of the queue 1: {queue1.Capacity}");
      WriteLine ($"\nCapacity of the queue 2: {queue2.Capacity}");
   }
}
#endregion

#region class TQueue ------------------------------------------------------------------------------
// Implementation of double ended queue
class TQueue<T> {
   #region Constructors ---------------------------------------------
   /// <summary>Initialize an empty queue with default capacity to hold four objects</summary>
   public TQueue () {
      mData = new T[4];
      mHead = 0;
      mTail = 0;
      mCount = 0;
   }
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the maximum number of objects the queue can store without resizing</summary>
   public int Capacity => mCapacity;


   /// <summary>Returns if the queue is empty</summary>
   public bool IsEmpty => mCount == 0;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Removes and returns object at the head of the queue</summary>
   public T DequeueHead () {
      if (IsEmpty) throw new Exception ("Queue empty");
      int first = mHead;
      mHead = (mHead + 1) % mCapacity; // Update pointer to point to the next element
      mCount--;
      return mData[first];
   }

   /// <summary>Removes and returns object at the end of the queue</summary>
   public T DequeueTail () {
      if (IsEmpty) throw new Exception ("Queue empty");
      int last = (mTail - 1 + mCapacity) % mCapacity;
      mTail = last;
      mCount--;
      return mData[last];
   }

   /// <summary>Adds object at the head of the queue</summary>
   public void EnqueueHead (T a) {
      Resize ();
      // Decrement head pointer to update the head of the queue
      mHead = (mCapacity + mHead - 1) % mCapacity;
      mData[mHead] = a;
      mCount++;
   }

   /// <summary>Adds object at the end of the queue</summary>
   public void EnqueueTail (T a) {
      Resize ();
      mData[mTail] = a;
      // Increment tail pointer to point to the next empty space available at the end of the queue
      mTail = (mTail + 1) % mCapacity;
      mCount++;
   }
   #endregion

   #region Implementation -------------------------------------------
   // Resize if the array is full
   void Resize () {
      // Check whether the array needs to be resized
      if (mCount < mData.Length) return;
      int index = 0, len = mCapacity, count = mCount;
      var temp = new T[len * 2];
      while (count > 0) {
         temp[index++] = mData[mHead];
         mHead = (mHead + 1) % len;
         count--;
      }
      mData = temp;
      mTail = index;
      mHead = 0;
   }
   #endregion

   #region Private Data ---------------------------------------------
   int mCapacity => mData.Length;
   int mCount; // Counts the number of elements in the queue
   T[] mData;  // Array structure to store objects in queue
   int mHead;  // Points to the first element of the queue
   int mTail;  // Points to the next empty location at the rear end of the queue
   #endregion
}
#endregion