using System;
using System.Collections.Generic;

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
            {"¬", new Func<bool, bool>(NOT)},
            {"&", new Func<bool, bool, bool>(AND)},
            {"+", new Func<bool, bool, bool>(OR)},
            {"^", new Func<bool, bool, bool>(XOR)},
            {"→", new Func<bool, bool, bool>(IMPL)},
            {"←", new Func<bool, bool, bool>(ReIMPL)},
            {"↔", new Func<bool, bool, bool>(XNOR)},
            {"↓", new Func<bool, bool, bool>(NOR)},
            {"|", new Func<bool, bool, bool>(NAND)}
        };

        public static Dictionary<string, int> Priorities = new Dictionary<string, int>
        {
            {"¬", 4},
            {"&", 3},
            {"+", 2},
            {"^", 2},
            {"→", 1},
            {"←", 1},
            {"↔", 1},
            {"↓", 1},
            {"|", 1}
        };

        public static Dictionary<string, int> OperandsCount = new Dictionary<string, int>
        {
            {"¬", 1},
            {"&", 2},
            {"+", 2},
            {"^", 2},
            {"→", 2},
            {"←", 2},
            {"↔", 2},
            {"↓", 2},
            {"|", 2}
        };

        public Operation(string identifier, TokenType type) : base(identifier, type)
        {
            if (type != TokenType.Operation)
                throw new ArgumentException(nameof(type));
        }

        private static bool NOT(bool a)
        {
            return !a;
        }

        private static bool AND(bool a, bool b)
        {
            return a & b;
        }

        private static bool OR(bool a, bool b)
        {
            return a | b;
        }

        private static bool XOR(bool a, bool b)
        {
            return a & !b | !a & b;
        }

        private static bool IMPL(bool a, bool b)
        {
            return !a | b;
        }

        private static bool ReIMPL(bool a, bool b)
        {
            return a | !b;
        }

        private static bool XNOR(bool a, bool b)
        {
            return a & b | !a & !b;
        }

        private static bool NOR(bool a, bool b)
        {
            return !(a | b);
        }

        private static bool NAND(bool a, bool b)
        {
            return !(a & b);
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
