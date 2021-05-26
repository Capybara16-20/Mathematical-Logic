using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MathematicalLogicProcessor
{
    public class LogicalExpressionSyntaxAnalyzer
    {
        private const string tokenPattern = @"\(|\)|[A-Z][1-9]*[0-9]*|¬|\&|\+|\^|→|←|↔|↓|(?<=\W)1|(?<=\W)0";
        private const string variablePattern = @"[A-Z][1-9]*[0-9]*";
        private const string operationPattern = @"¬|\&|\+|\^|→|←|↔|↓";
        private const string openBracePattern = @"\(";
        private const string closeBracePattern = @"\)";
        private const string constPattern = @"1|0";

        private Dictionary<string, int> OperationPriorities = new Dictionary<string, int>
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

        private string expression;
        private List<Token> tokens;
        private List<Token> variables;
        private List<Token> operations;
        private string polishNotation;

        public string Expression { get { return expression; } }
        public List<Token> Tokens { get { return tokens; } }
        public List<Token> Variables { get { return variables; } }
        public List<Token> Operations { get { return operations; } }

        public LogicalExpressionSyntaxAnalyzer(string expression)
        {
            expression = Regex.Replace(expression, @"\s+", "");
            tokens = GetTokens(expression);

            if (CheckExpression(ref tokens))
            {
                this.expression = expression;
                
                variables = tokens.Where(n => n.Type == TokenType.Variable).ToList();
                operations = tokens.Where(n => n.Type == TokenType.Operation).ToList();

            }
            else
            {
                throw new ArgumentException(nameof(expression));
            }
        }

        private bool CheckExpression(ref List<Token> tokens)
        {
            //не должно начинаться с оператора, с закрывающей скобки
            //не должно идти 2 операции подряд
            //добавить произведение, если идут два операнда подряд (переменная/константа)
            //отдельные числа отличные от 0 и 1
            throw new NotImplementedException();
        }

        public static List<Token> GetTokens(string expression)
        {
            Regex variableRegex = new Regex(variablePattern, RegexOptions.IgnoreCase);
            Regex operationRegex = new Regex(operationPattern, RegexOptions.IgnoreCase);
            Regex openBraceRegex = new Regex(openBracePattern, RegexOptions.IgnoreCase);
            Regex closeBraceRegex = new Regex(closeBracePattern, RegexOptions.IgnoreCase);
            Regex constRegex = new Regex(constPattern, RegexOptions.IgnoreCase);

            List<Token> tokens = new List<Token>();
            MatchCollection tokenMatches = Regex.Matches(expression, tokenPattern, RegexOptions.IgnoreCase);
            foreach (Match match in tokenMatches)
            {
                Match temp = variableRegex.Match(match.Value);
                if (temp.Success)
                {
                    tokens.Add(new Token(temp.Value, TokenType.Variable));
                    continue;
                }

                temp = operationRegex.Match(match.Value);
                if (temp.Success)
                {
                    tokens.Add(new Token(temp.Value, TokenType.Operation));
                    continue;
                }

                temp = openBraceRegex.Match(match.Value);
                if (temp.Success)
                {
                    tokens.Add(new Token(temp.Value, TokenType.OpenBrace));
                    continue;
                }

                temp = closeBraceRegex.Match(match.Value);
                if (temp.Success)
                {
                    tokens.Add(new Token(temp.Value, TokenType.CloseBrace));
                    continue;
                }

                temp = constRegex.Match(match.Value);
                if (temp.Success)
                {
                    tokens.Add(new Token(temp.Value, TokenType.Const));
                    continue;
                }
            }

            return tokens;
        }

        private static List<Token> GetPolishNotation(List<Token> tokens, List<Token> variables)
        {
            List<Token> result = new List<Token>();
            Stack<Token> stack = new Stack<Token>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (variables.Any(n => n.Equals(tokens[i])))
                {

                }
            }


            return result;
        }
    }
}
