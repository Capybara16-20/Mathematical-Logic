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
            if (CheckExpression(expression))
            {
                tokens = ParseExpression(ref expression);
                this.expression = expression;
                variables = GetVariables(expression);
                operations = GetOperations(expression);
                RemoveExtraBraces(tokens);
                polishNotation = GetPolishNotation(tokens);
            }
            else
            {
                throw new ArgumentException(nameof(expression));
            }
        }

        public static List<Token> ParseExpression(ref string expression)
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

            //добавить произведение, если
            //# идут два операнда подряд
            //# операнд после закрывающей скобки
            //# операнд перед открывающей скобкой
            //# унарная операция после операнда
            //# унарная операция после закрывающей скобки
            //# закрывающая скобка после открывающей
            List<Token> tokens = GetTokens(expression);
            tokens = AddSeparator(tokens, new Token(Operation.And, TokenType.Operation));

            StringBuilder sb = new StringBuilder();
            foreach (Token token in tokens)
                sb.Append(token.Identifier);

            expression = sb.ToString();
            return tokens;
        }

        public static List<Token> AddSeparator(List<Token> tokens, Token separator)
        {
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            List<int> indexesToAdd = new List<int>();
            int shift = 0;
            for (int i = 1; i < tokens.Count; i++)
            {
                Token previousToken = tokens[i - 1];
                Token followingToken = tokens[i];

                bool isTwoOperandsInRow = (previousToken.Type == TokenType.Variable
                                            || previousToken.Type == TokenType.Const)
                                            && (followingToken.Type == TokenType.Variable
                                            || followingToken.Type == TokenType.Const);

                bool isOperandAfterCloseBrace = (previousToken.Type == TokenType.CloseBrace)
                                            && (followingToken.Type == TokenType.Variable
                                            || followingToken.Type == TokenType.Const);

                bool isOperandBeforeOpenBrace = (previousToken.Type == TokenType.Variable
                                            || previousToken.Type == TokenType.Const)
                                            && (followingToken.Type == TokenType.OpenBrace);

                bool isUnaryOperationAfterOperand = (previousToken.Type == TokenType.Variable
                                            || previousToken.Type == TokenType.Const)
                                            && followingToken.Type == TokenType.Operation
                                            && operationOperandsCount.Any(n => n.Key == followingToken.Identifier
                                                && n.Value == 1);

                bool isUnaryOperationAfterCloseBrace = (previousToken.Type == TokenType.CloseBrace)
                                            && followingToken.Type == TokenType.Operation
                                            && operationOperandsCount.Any(n => n.Key == followingToken.Identifier
                                                && n.Value == 1);

                bool isOpenBraceAfterCloseBrace = (previousToken.Type == TokenType.CloseBrace)
                                            && (followingToken.Type == TokenType.OpenBrace);

                if (isTwoOperandsInRow || isOperandAfterCloseBrace || isOperandBeforeOpenBrace
                    || isUnaryOperationAfterOperand || isUnaryOperationAfterCloseBrace || isOpenBraceAfterCloseBrace)
                {
                    indexesToAdd.Add(i + shift);
                    shift++;
                }
            }

            foreach (int index in indexesToAdd)
                tokens.Insert(index, separator);

            return tokens;
        }

        public static List<Token> RemoveExtraBraces(List<Token> tokens)
        {
            List<Token> polishNotation = GetPolishNotation(tokens);
            List<List<Token>> expressions = GetAllExpressions(polishNotation);

            return expressions.Last();
        }

        public static List<List<Token>> GetAllExpressions(List<Token> polishNotation)
        {
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;
            Dictionary<string, int> operationPriorities = Operation.Priorities;

            List<List<Token>> expressions = new List<List<Token>>();
            Stack<List<Token>> stack = new Stack<List<Token>>();
            for (int i = 0; i < polishNotation.Count; i++)
            {
                if (polishNotation[i].Type == TokenType.Variable
                    || polishNotation[i].Type == TokenType.Const)
                {
                    List<Token> newOperand = new List<Token> { polishNotation[i] };
                    stack.Push(newOperand);
                }
                else
                {
                    if (operationOperandsCount.Any(n => n.Key == polishNotation[i].Identifier
                        && n.Value == 1))
                    {
                        List<Token> operand = stack.Pop();
                        if (IsNeedBraces(operand, polishNotation[i]))
                            operand = EncloseInBraces(operand);

                        List<Token> newOperand = new List<Token> { polishNotation[i] };
                        newOperand.AddRange(operand);

                        expressions.Add(newOperand);
                        stack.Push(newOperand);
                    }
                    else
                    {
                        List<Token> operand2 = stack.Pop();
                        List<Token> operand1 = stack.Pop();

                        if (IsNeedBraces(operand1, polishNotation[i]))
                            operand1 = EncloseInBraces(operand1);
                        if (IsNeedBraces(operand2, polishNotation[i]))
                            operand2 = EncloseInBraces(operand2);

                        List<Token> newOperand = new List<Token>();
                        newOperand.AddRange(operand1);
                        newOperand.Add(polishNotation[i]);
                        newOperand.AddRange(operand2);

                        expressions.Add(newOperand);
                        stack.Push(newOperand);
                    }
                }
            }

            return expressions;
        }

        public static bool IsNeedBraces(List<Token> tokens, Token operation)
        {
            Dictionary<string, int> operationPriorities = Operation.Priorities;

            if (tokens.Count == 1)
                return false;

            if (!tokens.Any(n => n.Type == TokenType.Operation
                && operationPriorities[n.Identifier]
                < operationPriorities[operation.Identifier]))
            {
                return false;
            }
            else
            {
                Stack<Token> stack = new Stack<Token>();
                for (int i = 0; i < tokens.Count; i++)
                {
                    if (tokens[i].Type == TokenType.OpenBrace)
                        stack.Push(tokens[i]);

                    if (tokens[i].Type == TokenType.CloseBrace)
                        stack.Pop();

                    if (tokens[i].Type == TokenType.Operation)
                    {
                        if (stack.Count == 0 && tokens[i].Type == TokenType.Operation
                            && operationPriorities[tokens[i].Identifier]
                            < operationPriorities[operation.Identifier])
                        {
                            return true;
                        }
                    }

                }
            }

            return false;
        }

        public static List<Token> EncloseInBraces(List<Token> tokens)
        {
            Token openBrace = new Token(Token.OpenBrace, TokenType.OpenBrace);
            Token closeBrace = new Token(Token.CloseBrace, TokenType.CloseBrace);

            List<Token> newOperand = new List<Token>();
            newOperand.Add(openBrace);
            newOperand.AddRange(tokens);
            newOperand.Add(closeBrace);

            return newOperand;
        }

        public static bool CheckExpression(string expression)
        {
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            List<Token> tokens = GetTokens(expression);

            //первый токен не может быть бинарной операцией или закрывающей скобкой
            Token firstToken = tokens.First();
            if (firstToken.Type == TokenType.Operation
                    && operationOperandsCount.Any(n => n.Key == firstToken.Identifier && n.Value == 2)
                    || firstToken.Type == TokenType.CloseBrace)
                return false;

            //последний токен не может быть операцией или открывающей скобкой
            Token lastToken = tokens.Last();
            if (lastToken.Type == TokenType.Operation || lastToken.Type == TokenType.OpenBrace)
                return false;

            //за операцией не может идти бинарной операции
            //за открывающей скобкой не может идти банарная операция
            //перед закрывающей скобкой не может идти банарная операция
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
                    && operationOperandsCount.Any(n => n.Key == followingToken.Identifier && n.Value == 2))
                    return false;

                if (previousToken.Type == TokenType.Operation
                    && followingToken.Type == TokenType.CloseBrace)
                    return false;
            }

            //проверка парных скобок
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

            return true;
        }

        public static List<Token> GetTokens(string expression)
        {
            Regex variableRegex = new Regex(variablePattern, RegexOptions.IgnoreCase);
            Regex constRegex = new Regex(constPattern);
            Regex operationRegex = new Regex(operationPattern, RegexOptions.IgnoreCase);
            Regex openBraceRegex = new Regex(openBracePattern, RegexOptions.IgnoreCase);
            Regex closeBraceRegex = new Regex(closeBracePattern, RegexOptions.IgnoreCase);

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

                temp = constRegex.Match(match.Value);
                if (temp.Success)
                {
                    tokens.Add(new Token(temp.Value, TokenType.Const));
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

        public static List<Token> GetPolishNotation(List<Token> tokens)
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
