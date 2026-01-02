// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Evaluator.cs
// Program evaluates tokens from tokenizer and returns the final result.
// ------------------------------------------------------------------------------------------------
namespace A09;

#region Eval Expression ---------------------------------------------------------------------------
class EvalException (string message) : Exception (message) {
}
#endregion

#region class Evaluator ---------------------------------------------------------------------------
class Evaluator {
   #region Properties -----------------------------------------------------------------------------
   /// <summary>Gets previous token evaluated in Evaluator</summary>
   public Token? GetPrevToken { get; private set; }

   public int BasePriority = 0; // Stores the base priority of operand under evaluation

   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the result obtained after evaluation of expression</summary>
   public double Evaluate (string input) {
      var tokenizer = new Tokenizer (this, input);
      mOperators.Clear ();
      mOperands.Clear ();
      BasePriority = 0;
      List<Token> tokens = [];
      for (; ; ) {
         var token = tokenizer.GetNext ();
         GetPrevToken = token;
         if (token is TEnd) break;
         if (token is TError err) throw new EvalException (err.Message);
         tokens.Add (token);
      }
      // Evaluate variable assignments
      TVariable? tVar = null;
      if (tokens.Count > 1 && tokens[0] is TVariable tv && tokens[1] is TOpBinary bin && bin.Op == '=') {
         tVar = tv;
         tokens.RemoveRange (0, 2);
      }
      foreach (var token in tokens) Process (token);
      while (mOperators.Count > 0) ApplyOperator ();
      if (BasePriority != 0) throw new Exception ("Mismatched parenthesis");
      if (mOperators.Count > 0) throw new EvalException ("Too many operators");
      if (mOperands.Count > 1) throw new EvalException ("Too many operands");
      double f = mOperands.Pop ();
      if (tVar != null)
         mVariables[tVar.Name] = f; // Update dictionary to store user assigned variable
      return f;
   }

   /// <summary>Returns value associated with user assigned variable</summary>
   public double GetVariable (string name) {
      if (mVariables.TryGetValue (name, out var f)) return f;
      throw new EvalException ($"Unknown variable '{name}'");
   }
   #endregion

   #region Implementation -------------------------------------------
   // Applies operators in operator stack to the values stores in operand stack
   // Updates operands stack with evaluated result
   void ApplyOperator () {
      var op = mOperators.Pop ();
      if (mOperands.Count < 1) throw new EvalException ("Operation requires atleast one operand");
      double f = mOperands.Pop ();
      if (op is TOpBinary bin) {
         if (mOperands.Count < 1) throw new EvalException ("Operation requires two operands");
         double f2 = mOperands.Pop ();
         mOperands.Push (bin.Apply (f2, f));
      } else if (op is TOpFunction func) mOperands.Push (func.Apply (f));
      else if (op is TOpUnary unary) mOperands.Push (unary.Apply (f));
      else throw new NotImplementedException ();
   }

   // Updates operators and operands in the expression to the stack
   void Process (Token token) {
      switch (token) {
         case TLiteral lit:
            mOperands.Push (lit.Value); return;
         case TOperator op:
            // Apply operator if the previous operand has a higher priority
            while (mOperators.Count > 0 && mOperators.Peek ().Priority > op.Priority)
               ApplyOperator ();
            mOperators.Push (op); return;
         case TPunctuation p:
            // Increase priority for expressions within braces
            BasePriority += p.Punct == '(' ? 10 : -10; return;
         default: throw new NotImplementedException ();
      }
   }
   #endregion

   #region Private Data ---------------------------------------------
   Stack<double> mOperands = new ();
   Stack<TOperator> mOperators = new ();
   Dictionary<string, double> mVariables = []; // Stores the user assigned variables
   #endregion
}
#endregion