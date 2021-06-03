namespace MathematicalLogicProcessor
{
    public class Token
    {
        public const string TokenPattern = @"\(|\)|[A-Z][1-9]*[0-9]*|¬|\&|\+|\^|→|←|↔|↓|\||(?<=\W)1|(?<=\W)0";
        public const string OpenBracePattern = @"\(";
        public const string CloseBracePattern = @"\)";
        public const string OpenBrace = "(";
        public const string CloseBrace = ")";

        protected string identifier;
        protected TokenType type;

        public string Identifier { get { return identifier; } }
        public TokenType Type { get { return type; } }

        public Token(string identifier, TokenType type)
        {
            this.identifier = identifier;
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is Token token)
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
            return 31 * identifier.GetHashCode() + type.ToString().GetHashCode();
        }
    }
}
