// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// A15: Priority Queue
// Implementation of priority queue using binary min heap.
// ------------------------------------------------------------------------------------------------
namespace A15;

#region class PriorityQueue -----------------------------------------------------------------------
/// <summary>Implements priority queue using binary(min) heap</summary>
public class PriorityQueue<T> where T : IComparable<T> {
   #region Constructors ---------------------------------------------
   public PriorityQueue () => mQueue = [];
   #endregion

   #region Public methods -------------------------------------------
   /// <summary>Returns the smallest object in the queue</summary>
   public T Dequeue () {
      if (IsEmpty) throw new InvalidOperationException ("Empty list");
      T value = mQueue[0];
      (mQueue[0], mQueue[mCount - 1]) = (mQueue[mCount - 1], mQueue[0]);
      mQueue.RemoveAt (mQueue.Count - 1);
      SiftDown (0);
      return value;
   }

   /// <summary>Adds object to queue</summary>
   public void Enqueue (T value) {
      mQueue.Add (value);
      SiftUp (mCount - 1);
   }

   /// <summary>Returns if the queue is empty</summary>
   public bool IsEmpty => mCount == 0;
   #endregion

   #region Implementation -------------------------------------------
   // Swap parent node with child node if the child node is smaller than the parent node
   void SiftDown (int index) {
      int leftNode = 2 * index + 1, rightNode = 2 * index + 2;
      if (leftNode >= mCount) return;
      int smallerNode = rightNode < mCount &&
                        mQueue[leftNode].CompareTo (mQueue[rightNode]) > 0 ? rightNode : leftNode;
      if (mQueue[smallerNode].CompareTo (mQueue[index]) < 0) {
         (mQueue[index], mQueue[smallerNode]) = (mQueue[smallerNode], mQueue[index]);
         SiftDown (smallerNode);
      }
   }

   // Swap child node with parent node if the child node is smaller than the parent node
   void SiftUp (int index) {
      int parentNode = (index - 1) / 2;
      if (mQueue[parentNode].CompareTo (mQueue[index]) > 0) {
         (mQueue[parentNode], mQueue[index]) = (mQueue[index], mQueue[parentNode]);
         SiftUp (parentNode);
      }
   }
   #endregion

   #region Private data ---------------------------------------------
   int mCount => mQueue.Count;
   List<T> mQueue;
   #endregion
}
#endregion