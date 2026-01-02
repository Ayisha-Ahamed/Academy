// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Tokenizer.cs
// Program splits expression into tokens for evaluation.
// ------------------------------------------------------------------------------------------------
namespace A09;

#region class Tokenizer ---------------------------------------------------------------------------
class Tokenizer (Evaluator eval, string input) {
   #region Methods --------------------------------------------------
   /// <summary>Gets token converted from input string</summary>
   public Token GetNext () {
      while (mN < mText.Length) {
         char ch = mText[mN++];
         switch (ch) {
            case '*' or '^' or '/' or '=': return new TOpBinary (mEval, ch);
            case ' ': break;
            case >= '0' and <= '9': return GetLiteral ();
            case '(' or ')': return new TPunctuation (ch);
            case '+' or '-': return mEval.GetPrevToken is not (TLiteral or TVariable) ? new TOpUnary(mEval,ch) 
                                                                                    : new TOpBinary (mEval,ch);
            case >= 'a' and <= 'z': return GetIdentifier ();
            default: return new TError ($"Unexpected character {ch}");
         }
      }
      return new TEnd ();
   }
   #endregion

   #region Implementation -------------------------------------------
   TLiteral GetLiteral () {
      int start = mN - 1;
      while (mN < mText.Length && mText[mN++] is >= '0' and <= '9');
      string number = mText[start..mN];
      double f = double.Parse (number);
      return new TLiteral (f);
   }

   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN++];
         if (ch is >= '0' and <= '9' or >= 'a' and <= 'z')
            continue;
         mN--; 
         break;
      }
      string identifier = mText[start..mN];
      // If the method is defined in mFunc return token TOpFunction
      if (mFunc.Contains (identifier)) return new TOpFunction (mEval,identifier);
      return new TVariable (mEval, identifier);
   }
   readonly string[] mFunc = ["sin", "cos", "tan", "asin", "acos", "atan", "log", "exp", "sqrt"];
   #endregion

   #region Private Data ---------------------------------------------
   readonly string mText = input;
   int mN = 0;
   readonly Evaluator mEval = eval;
   #endregion
}
#endregion