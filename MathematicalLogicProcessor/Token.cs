namespace MathematicalLogicProcessor
{
    public class Token
    {
        public const string TokenPattern = @"\(|\)|[A-Z][1-9]*[0-9]*|¬|\&|\+|\^|→|←|↔|↓|(?<=\W)1|(?<=\W)0";

        protected string identifier;
        protected TokenType type;

        public string Identifier { get { return identifier; } }
        public TokenType Type { get { return type; } }

        public Token(string value, TokenType type)
        {
            this.identifier = value;
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj is Token token)
            {
                return token.Identifier == identifier && token.Type == type;
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
