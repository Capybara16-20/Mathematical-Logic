using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("MathematicalLogicProcessorTests")]
namespace MathematicalLogicProcessor
{
    public class Operation : Token
    {
        public const string OperationPattern = @"¬|\&|\+|\^|→|←|↔|↓|\|";
        public const string Not = "¬";
        public const string And = "&";
        public const string Or = "+";
        public const string Xor = "^";
        public const string  Implication = "→";
        public const string ReverseImplication = "←";
        public const string Xnor = "↔";
        public const string Nor = "↓";
        public const string Nand = "|";

        public static Dictionary<string, Delegate> Operations = new Dictionary<string, Delegate>
        {
            {Not, new Func<bool, bool>(NOT)},
            {And, new Func<bool, bool, bool>(AND)},
            {Or, new Func<bool, bool, bool>(OR)},
            {Xor, new Func<bool, bool, bool>(XOR)},
            {Implication, new Func<bool, bool, bool>(IMPL)},
            {ReverseImplication, new Func<bool, bool, bool>(ReIMPL)},
            {Xnor, new Func<bool, bool, bool>(XNOR)},
            {Nor, new Func<bool, bool, bool>(NOR)},
            {Nand, new Func<bool, bool, bool>(NAND)}
        };

        public static Dictionary<string, Delegate> Transformations = new Dictionary<string, Delegate>
        {
            {Xor, new Func<List<Token>, List<Token>, List<Token>>(TransformXOR)},
            {Implication, new Func<List<Token>, List<Token>, List<Token>>(TransformIMPL)},
            {ReverseImplication, new Func<List<Token>, List<Token>, List<Token>>(TransformReIMPL)},
            {Xnor, new Func<List<Token>, List<Token>, List<Token>>(TransformXNOR)},
            {Nor, new Func<List<Token>, List<Token>, List<Token>>(TransformNOR)},
            {Nand, new Func<List<Token>, List<Token>, List<Token>>(TransformNAND)}
        };

        public static Dictionary<string, int> Priorities = new Dictionary<string, int>
        {
            {Not, 7},
            {And, 6},
            {Or, 5},
            {Xor, 4},
            {Implication, 3},
            {ReverseImplication, 3},
            {Xnor, 2},
            {Nor, 1},
            {Nand, 1}
        };

        public static Dictionary<string, int> OperandsCount = new Dictionary<string, int>
        {
            {Not, 1},
            {And, 2},
            {Or, 2},
            {Xor, 2},
            {Implication, 2},
            {ReverseImplication, 2},
            {Xnor, 2},
            {Nor, 2},
            {Nand, 2}
        };

        public Operation(string identifier, TokenType type) : base(identifier, type)
        {
            if (type != TokenType.Operation)
                throw new ArgumentException(nameof(type));
        }

        internal static bool NOT(bool a)
        {
            return !a;
        }

        internal static bool AND(bool a, bool b)
        {
            return a & b;
        }

        internal static bool OR(bool a, bool b)
        {
            return a | b;
        }

        internal static bool XOR(bool a, bool b)
        {
            return a & !b | !a & b;
        }

        internal static List<Token> TransformXOR(List<Token> a, List<Token> b)
        {
            Token and = new Token(And, TokenType.Operation);
            Token or = new Token(Or, TokenType.Operation);
            Token not = new Token(Not, TokenType.Operation);

            bool isASeveralTokens = a.Count > 1;
            bool isBSeveralTokens = b.Count > 1;
            bool isANeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(a, and);
            bool isBNeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(b, and);

            List<Token> result = new List<Token>();
            result.AddRange(isANeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(a) : a);
            result.Add(and);
            result.Add(not);
            result.AddRange(isBSeveralTokens ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(b) : b);
            result.Add(or);
            result.Add(not);
            result.AddRange(isASeveralTokens ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(a) : a);
            result.Add(and);
            result.AddRange(isBNeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(b) : b);

            return result;
        }

        internal static bool IMPL(bool a, bool b)
        {
            return !a | b;
        }

        internal static List<Token> TransformIMPL(List<Token> a, List<Token> b)
        {
            Token or = new Token(Or, TokenType.Operation);
            Token not = new Token(Not, TokenType.Operation);

            bool isASeveralTokens = a.Count > 1;
            bool isBNeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(b, or);


            List<Token> result = new List<Token>();
            result.Add(not);
            result.AddRange(isASeveralTokens ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(a) : a);
            result.Add(or);
            result.AddRange(isBNeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(b) : b);

            return result;
        }

        internal static bool ReIMPL(bool a, bool b)
        {
            return a | !b;
        }

        internal static List<Token> TransformReIMPL(List<Token> a, List<Token> b)
        {
            Token or = new Token(Or, TokenType.Operation);
            Token not = new Token(Not, TokenType.Operation);

            bool isBSeveralTokens = b.Count > 1;
            bool isANeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(a, or);


            List<Token> result = new List<Token>();
            result.AddRange(isANeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(a) : a);
            result.Add(or);
            result.Add(not);
            result.AddRange(isBSeveralTokens ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(b) : b);

            return result;
        }

        internal static bool XNOR(bool a, bool b)
        {
            return a & b | !a & !b;
        }

        internal static List<Token> TransformXNOR(List<Token> a, List<Token> b)
        {
            Token and = new Token(And, TokenType.Operation);
            Token or = new Token(Or, TokenType.Operation);
            Token not = new Token(Not, TokenType.Operation);

            bool isASeveralTokens = a.Count > 1;
            bool isBSeveralTokens = b.Count > 1;
            bool isANeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(a, and);
            bool isBNeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(b, and);

            List<Token> result = new List<Token>();
            result.AddRange(isANeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(a) : a);
            result.Add(and);
            result.AddRange(isBNeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(b) : b);
            result.Add(or);
            result.Add(not);
            result.AddRange(isASeveralTokens ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(a) : a);
            result.Add(and);
            result.Add(not);
            result.AddRange(isBSeveralTokens ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(b) : b);

            return result;
        }

        internal static bool NOR(bool a, bool b)
        {
            return !(a | b);
        }

        internal static List<Token> TransformNOR(List<Token> a, List<Token> b)
        {
            Token or = new Token(Or, TokenType.Operation);
            Token not = new Token(Not, TokenType.Operation);
            Token openBrace = new Token(OpenBrace, TokenType.OpenBrace);
            Token closeBrace = new Token(CloseBrace, TokenType.CloseBrace);

            bool isANeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(a, or);
            bool isBNeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(b, or);

            List<Token> result = new List<Token>();
            result.Add(not);
            result.Add(openBrace);
            result.AddRange(isANeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(a) : a);
            result.Add(or);
            result.AddRange(isBNeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(b) : b);
            result.Add(closeBrace);

            return result;
        }

        internal static bool NAND(bool a, bool b)
        {
            return !(a & b);
        }

        internal static List<Token> TransformNAND(List<Token> a, List<Token> b)
        {
            Token and = new Token(And, TokenType.Operation);
            Token not = new Token(Not, TokenType.Operation);
            Token openBrace = new Token(OpenBrace, TokenType.OpenBrace);
            Token closeBrace = new Token(CloseBrace, TokenType.CloseBrace);

            bool isANeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(a, and);
            bool isBNeedBraces = LogicalExpressionSyntaxAnalyzer.IsNeedBraces(b, and);

            List<Token> result = new List<Token>();
            result.Add(not);
            result.Add(openBrace);
            result.AddRange(isANeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(a) : a);
            result.Add(and);
            result.AddRange(isBNeedBraces ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(b) : b);
            result.Add(closeBrace);

            return result;
        }

        public override bool Equals(object obj)
        {
            if (obj is Operation operation)
            {
                return operation.Identifier == identifier
                    && operation.Type == type;
            }
            else if (obj is Token token)
            {
                return token.Identifier == identifier 
                    && token.Type == type;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return 31 * identifier.GetHashCode() + type.ToString().GetHashCode();
        }
    }
}
