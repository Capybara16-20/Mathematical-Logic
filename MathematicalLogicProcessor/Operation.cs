using System;
using System.Collections.Generic;

namespace MathematicalLogicProcessor
{
    public class Operation : Token
    {
        public const string OperationPattern = @"¬|\&|\+|\^|→|←|↔|↓";

        public static Dictionary<string, string> Operations = new Dictionary<string, string>
        {
            {"NOT", "¬"},
            {"AND", "&"},
            {"OR", "+"},
            {"XOR", "^"},
            {"x", "→"},
            {"y", "←"},
            {"XNOR", "↔"},
            {"z", "↓"}
        };

        public static Dictionary<string, int> Priorities = new Dictionary<string, int>
        {
            {"¬", 6},
            {"&", 5},
            {"+", 4},
            {"^", 4},
            {"→", 3},
            {"←", 3},
            {"↔", 2},
            {"↓", 1}
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
            {"↓", 2}
        };

        public Operation(string identifier, TokenType type) : base(identifier, type)
        {
            if (type != TokenType.Operation)
                throw new ArgumentException(nameof(type));
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
