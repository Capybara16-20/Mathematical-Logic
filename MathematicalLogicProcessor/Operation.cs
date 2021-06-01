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

        public static Dictionary<string, int> Priorities = new Dictionary<string, int>
        {
            {Not, 4},
            {And, 3},
            {Or, 2},
            {Xor, 2},
            {Implication, 1},
            {ReverseImplication, 1},
            {Xnor, 1},
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

        internal static bool IMPL(bool a, bool b)
        {
            return !a | b;
        }

        internal static bool ReIMPL(bool a, bool b)
        {
            return a | !b;
        }

        internal static bool XNOR(bool a, bool b)
        {
            return a & b | !a & !b;
        }

        internal static bool NOR(bool a, bool b)
        {
            return !(a | b);
        }

        internal static bool NAND(bool a, bool b)
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
