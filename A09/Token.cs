// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Token.cs
// ------------------------------------------------------------------------------------------------
namespace A09;

#region class Token -------------------------------------------------------------------------------
// Base class for tokenizing expression components
class Token { }
#endregion

#region class TNumber -----------------------------------------------------------------------------
// Structure to store numeric data
class TNumber : Token {
   public virtual double Value { get; }
}
#endregion

#region class TLiteral ----------------------------------------------------------------------------
// Represents literal values (eg: 2, 3.45)
class TLiteral (double f) : TNumber {
   public override string ToString () => $"Literal: {Value}";
   public override double Value => mValue;
   readonly double mValue = f;
}
#endregion

#region class TVariable ---------------------------------------------------------------------------
// Represents user assigned variables
class TVariable (Evaluator eval, string name) : TNumber {
   public override string ToString () => $"Variable: {Name}";
   public override double Value => mEval.GetVariable (Name);
   public string Name => mName;
   readonly string mName = name;
   readonly Evaluator mEval = eval;
}
#endregion

#region class TOperator ---------------------------------------------------------------------------
// Structure represents operators to be applied on operands
abstract class TOperator (Evaluator eval) : Token {
   public abstract int Priority { get; }
   readonly public Evaluator mEval = eval;
}
#endregion

#region class TOpBinary ---------------------------------------------------------------------------
// Represents binary operators for arithmetic operations (requires two operands)
class TOpBinary (Evaluator eval, char op) : TOperator (eval) {
   public override string ToString () => $"Operator: {Op}";

   public override int Priority
      => Op switch {
         '+' or '-' => 1,
         '*' or '/' => 2,
         '^' => 3,
         _ => throw new NotImplementedException (),
      };

   public double Apply (double a, double b)
      => Op switch {
         '+' => a + b,
         '-' => a - b,
         '*' => a * b,
         '/' => b / a,
         '^' => Math.Pow (a, b),
         _ => throw new NotImplementedException (),
      };

   public char Op => mOp;
   readonly char mOp = op;
}
#endregion

#region class TOpFunction -------------------------------------------------------------------------
// Represents unary function in expression string
class TOpFunction (Evaluator eval, string func) : TOperator (eval) {
   public override string ToString () => $"Function: {Func}";

   public override int Priority => 4 + mEval.BasePriority;

   public double Apply (double a)
      => Func switch {
         "sin" => Math.Sin (D2R (a)),
         "cos" => Math.Cos (D2R (a)),
         "tan" => Math.Tan (D2R (a)),
         "sqrt" => Math.Sqrt (a),
         "log" => Math.Log (a),
         "exp" => Math.Exp (a),
         "asin" => R2D (Math.Asin (a)),
         "acos" => R2D (Math.Acos (a)),
         "atan" => R2D (Math.Atan (a)),
         _ => throw new NotImplementedException (),
      };
   // Converts degree to radians
   double D2R (double f) => f * Math.PI / 180;
   // Converts radians to degree
   double R2D (double f) => f * 180 / Math.PI;

   public string Func => mFunc;
   readonly string mFunc = func;
}
#endregion

#region class TOpUnary ----------------------------------------------------------------------------
// Represents unary operation (unary plus, unary minus)
class TOpUnary (Evaluator eval, char op) : TOperator (eval) {
   public override string ToString () => $"op:{Op}:{Priority}";
   public override int Priority => 5 + mEval.BasePriority;

   public double Apply (double a = 0) {
      return Op switch {
         '-' => -a,
         '+' => a,
         _ => throw new EvalException ($"Unknown unary operator: {Op}"),
      };
   }
   public char Op => mOp;
   readonly char mOp = op;
}
#endregion

#region class TPunctuation ------------------------------------------------------------------------
// Represents braces ['(' and ')'] in expression evaluation
class TPunctuation (char punct) : Token {
   public override string ToString () => $"Punctuation: {mPunct}";
   public char Punct => mPunct;
   readonly char mPunct = punct;
}
#endregion

#region class TEnd --------------------------------------------------------------------------------
// Represents the end of the expression
class TEnd : Token { }
#endregion

#region class TError ------------------------------------------------------------------------------
// Represents an error in parsing expression
class TError (string message) : Token {
   public override string ToString () => $"Error: {Message}";
   public string Message => mMessage;
   readonly string mMessage = message;
}
#endregion