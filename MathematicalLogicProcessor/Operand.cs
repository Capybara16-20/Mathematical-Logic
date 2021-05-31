using System;

namespace MathematicalLogicProcessor
{
    public class Operand : Token
    {
        public const string VariablePattern = @"[A-Z][1-9]*[0-9]*";
        public const string ConstPattern = @"1|0";

        public bool Value { get; set; }

        public Operand(Token token) : base(token.Identifier, token.Type) 
        {
            if (token.Type != TokenType.Variable && token.Type != TokenType.Const)
                throw new ArgumentException(nameof(type));

            if (token.Type == TokenType.Const)
                Value = identifier == "1";
        }

        public Operand(string identifier, TokenType type, bool value) : base(identifier, type)
        {
            if (type != TokenType.Variable)
                throw new ArgumentException(nameof(type));

            Value = value;
        }

        public Operand(string identifier, TokenType type) : base(identifier, type)
        {
            if (type != TokenType.Variable && type != TokenType.Const)
                throw new ArgumentException(nameof(type));

            if (type == TokenType.Const)
                Value = identifier == "1";
        }

        public override bool Equals(object obj)
        {
            if (obj is Operand operand)
            {
                return operand.Identifier == identifier
                    && operand.Type == type
                    && Value == operand.Value;
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
