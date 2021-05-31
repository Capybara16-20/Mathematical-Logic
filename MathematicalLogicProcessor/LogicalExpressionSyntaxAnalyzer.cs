using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LinqExtention;

namespace MathematicalLogicProcessor
{
    public class LogicalExpressionSyntaxAnalyzer
    {
        private const string tokenPattern = Token.TokenPattern;
        private const string variablePattern = Operand.VariablePattern;
        private const string constPattern = Operand.ConstPattern;
        private const string operationPattern = Operation.OperationPattern;
        private const string openBracePattern = Token.OpenBracePattern;
        private const string closeBracePattern = Token.CloseBracePattern;

        private readonly string expression;
        private readonly List<Token> tokens;
        private readonly List<Operand> variables;
        private readonly List<Operation> operations;
        private readonly List<Token> polishNotation;

        public string Expression { get { return expression; } }
        public List<Token> Tokens { get { return tokens; } }
        public List<Operand> Variables { get { return variables; } }
        public List<Operation> Operations { get { return operations; } }
        public List<Token> PolishNotation { get { return polishNotation; } }

        public LogicalExpressionSyntaxAnalyzer(string expression)
        {
            tokens = ParseExpression(ref expression);
            if (CheckExpression(expression))
            {
                this.expression = expression;
                variables = GetVariables(expression);
                operations = GetOperations(expression);
                polishNotation = GetPolishNotation(tokens);
            }
            else
            {
                throw new ArgumentException(nameof(expression));
            }
        }

        private static List<Token> ParseExpression(ref string expression)
        {
            List<char> openBracesTypes = new List<char> { '[', '{' };
            List<char> closeBracesTypes = new List<char> { ']', '}' };

            //удалить пробелы
            expression = Regex.Replace(expression, @"\s+", "");

            //заменить скобки на круглые
            List<char> symbols = expression.ToList();
            for (int i = 0; i < symbols.Count; i++)
            {
                if (openBracesTypes.Contains(symbols[i]))
                    symbols[i] = char.Parse(Token.OpenBrace);

                if (closeBracesTypes.Contains(symbols[i]))
                    symbols[i] = char.Parse(Token.CloseBrace);
            }
            expression = new string(symbols.ToArray());

            //удалить скобки, если внутри них нет бинарных операций и других скобок
            //
            //
            //

            //добавить произведение, если идут два операнда подряд (переменная/константа)
            List<Token> tokens = GetTokens(expression);
            List<int> indexesToAdd = new List<int>();
            int shift = 0;
            for (int i = 1; i < tokens.Count; i++)
            {
                Token previousToken = tokens[i - 1];
                Token followingToken = tokens[i];

                if ((previousToken.Type == TokenType.Variable || previousToken.Type == TokenType.Const)
                    && (followingToken.Type == TokenType.Variable || followingToken.Type == TokenType.Const))
                {
                    indexesToAdd.Add(i + shift);
                    shift++;
                }
            }

            foreach (int index in indexesToAdd)
                tokens.Insert(index, new Token(Operation.And, TokenType.Operation));

            StringBuilder sb = new StringBuilder();
            foreach (Token token in tokens)
                sb.Append(token.Identifier);

            expression = sb.ToString();
            return tokens;
        }

        private static bool CheckExpression(string expression)
        {
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            List<Token> tokens = GetTokens(expression);

            Token firstToken = tokens.First();
            if (firstToken.Type == TokenType.Operation
                    && operationOperandsCount.Any(n => n.Key == firstToken.Identifier && n.Value == 2)
                    || firstToken.Type == TokenType.CloseBrace)
                return false;

            Token lastToken = tokens.Last();
            if (lastToken.Type == TokenType.Operation || lastToken.Type == TokenType.OpenBrace)
                return false;

            for (int i = 1; i < tokens.Count; i++)
            {
                Token previousToken = tokens[i - 1];
                Token followingToken = tokens[i];
                if (previousToken.Type == TokenType.Operation
                    && followingToken.Type == TokenType.Operation
                    && operationOperandsCount.Any(n => n.Key == followingToken.Identifier && n.Value == 2))
                    return false;

                if (previousToken.Type == TokenType.OpenBrace
                    && followingToken.Type == TokenType.Operation
                    && operationOperandsCount.Any(n => n.Key == followingToken.Identifier && n.Value == 2)
                    || previousToken.Type == TokenType.CloseBrace && followingToken.Type == TokenType.Operation)
                    return false;
            }

            List<Token> braces = tokens.Where(n => n.Type == TokenType.OpenBrace
                                                        || n.Type == TokenType.CloseBrace).ToList();
            Stack<Token> braceStack = new Stack<Token>();
            for (int i = 0; i < braces.Count; i++)
            {
                if (braces[i].Type == TokenType.OpenBrace)
                {
                    braceStack.Push(braces[i]);
                }
                else
                {
                    if (braceStack.Count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        braceStack.Pop();
                    }
                }
            }
            if (braceStack.Count != 0)
                return false;

            //не должно начинаться с оператора (кроме отрицания), с закрывающей скобки +
            //не должно идти 2 операции подряд (кроме отрицания) +
            //после открывыющей скобки не должно быть бинарной операйции +
            //перед закрывающей скобкой не должно быть операции +
            //проверка парных скобок
            //отдельные числа отличные от 0 и 1 +

            return true;
        }

        public static List<Token> GetTokens(string expression)
        {
            Regex variableRegex = new Regex(variablePattern, RegexOptions.IgnoreCase);
            Regex operationRegex = new Regex(operationPattern, RegexOptions.IgnoreCase);
            Regex openBraceRegex = new Regex(openBracePattern, RegexOptions.IgnoreCase);
            Regex closeBraceRegex = new Regex(closeBracePattern, RegexOptions.IgnoreCase);
            Regex constRegex = new Regex(constPattern);

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

        public static List<Operand> GetVariables(string expression)
        {
            List<Token> tokens = GetTokens(expression);
            List<Token> variableTokens = tokens.Where(n => n.Type == TokenType.Variable)
                                  .DistinctBy(n => n.Identifier).OrderBy(n => n.Identifier).ToList();

            List<Operand> variables = new List<Operand>();
            foreach (Token token in variableTokens)
                variables.Add(new Operand(token.Identifier, token.Type));

            return variables;
        }

        public static List<Operation> GetOperations(string expression)
        {
            List<Token> tokens = GetTokens(expression);
            List<Token> operationTokens = tokens.Where(n => n.Type == TokenType.Operation)
                                  .DistinctBy(n => n.Identifier).ToList();

            List<Operation> operations = new List<Operation>();
            foreach (Token token in operationTokens)
                operations.Add(new Operation(token.Identifier, token.Type));

            return operations;
        }

        private static List<Token> GetPolishNotation(List<Token> tokens)
        {
            Dictionary<string, int> operationPriorities = Operation.Priorities;
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            List<Token> result = new List<Token>();
            Stack<Token> stack = new Stack<Token>();
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i].Type == TokenType.Variable || tokens[i].Type == TokenType.Const)
                {
                    result.Add(tokens[i]);
                    continue;
                }

                if (tokens[i].Type == TokenType.Operation
                    && operationOperandsCount.Any(n => n.Key == tokens[i].Identifier && n.Value == 1))
                {
                    stack.Push(tokens[i]);
                    continue;
                }

                if (tokens[i].Type == TokenType.OpenBrace)
                {
                    stack.Push(tokens[i]);
                    continue;
                }

                if (tokens[i].Type == TokenType.CloseBrace)
                {
                    while (stack.Peek().Type != TokenType.OpenBrace)
                    {
                        result.Add(stack.Pop());
                    }

                    stack.Pop();
                    continue;
                }

                if (tokens[i].Type == TokenType.Operation)
                {
                    while ((stack.Count != 0) && (stack.Peek().Type == TokenType.Operation)
                            && (operationPriorities[tokens[i].Identifier] <= operationPriorities[stack.Peek().Identifier]))
                    {
                        result.Add(stack.Pop());
                    }

                    stack.Push(tokens[i]);
                    continue;
                }
            }

            while (stack.Count != 0)
                result.Add(stack.Pop());

            return result;
        }
    }
}
