namespace MathematicalLogicProcessor
{
    public class Token
    {
        private string value;
        private TokenType type;

        public string Value { get { return value; } }
        public TokenType Type { get { return type; } }

        public Token(string value, TokenType type)
        {
            this.value = value;
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj is Token token)
            {
                return token.Value == value && token.Type == type;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            throw new System.NotImplementedException();
        }
    }
}
