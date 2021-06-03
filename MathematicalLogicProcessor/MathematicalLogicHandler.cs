using System;
using System.Collections.Generic;
using System.Linq;

namespace MathematicalLogicProcessor
{
    public class MathematicalLogicHandler
    {
        private TruthTable truthTable;

        public TruthTable TruthTable { get { return truthTable; } }

        public MathematicalLogicHandler(string expression)
        {
            LogicalExpressionSyntaxAnalyzer analyzer = null;
            try
            {
                analyzer = new LogicalExpressionSyntaxAnalyzer(expression);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException(nameof(expression));
            }

            List<Operand> variables = analyzer.Variables;
            List<Token> polishNotation = analyzer.PolishNotation;
            truthTable = new TruthTable(variables, polishNotation);
        }

        public MathematicalLogicHandler(TruthTable truthTable)
        {
            this.truthTable = truthTable;
        }

        public List<Token> GetPDNF()
        {
            List<Token> pdnf = new List<Token>();
            bool[] vector = truthTable.FunctionVector;
            List<Operand> variables = truthTable.Variables;
            int variablesCount = truthTable.Variables.Count;

            List<List<Token>> temp = new List<List<Token>>();
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i])
                {
                    List<Token> disjunction = new List<Token>();
                    for (int j = 0; j < variablesCount; j++)
                    {
                        if (truthTable[i, j])
                        {
                            disjunction.Add(variables[j]);
                        }
                        else
                        {
                            disjunction.Add(new Token(Operation.Not, TokenType.Operation));
                            disjunction.Add(variables[j]);
                        }
                    }

                    LogicalExpressionSyntaxAnalyzer.AddSeparator(disjunction, new Token(Operation.And, TokenType.Operation));
                    temp.Add(disjunction);
                }
            }

            if (temp.Count > 1)
                for (int i = 0; i < temp.Count; i++)
                    temp[i] = LogicalExpressionSyntaxAnalyzer.EncloseInBraces(temp[i]);

            foreach (List<Token> tokens in temp)
                pdnf.AddRange(tokens);

            LogicalExpressionSyntaxAnalyzer.AddSeparator(pdnf, new Token(Operation.Or, TokenType.Operation));

            return pdnf;
        }

        public List<Token> GetPCNF()
        {
            List<Token> pcnf = new List<Token>();
            bool[] vector = truthTable.FunctionVector;
            List<Operand> variables = truthTable.Variables;
            int variablesCount = truthTable.Variables.Count;

            List<List<Token>> temp = new List<List<Token>>();
            for (int i = 0; i < vector.Length; i++)
            {
                if (!vector[i])
                {
                    List<Token> disjunction = new List<Token>();
                    for (int j = 0; j < variablesCount; j++)
                    {
                        if (!truthTable[i, j])
                        {
                            disjunction.Add(variables[j]);
                        }
                        else
                        {
                            disjunction.Add(new Token(Operation.Not, TokenType.Operation));
                            disjunction.Add(variables[j]);
                        }
                    }

                    LogicalExpressionSyntaxAnalyzer.AddSeparator(disjunction, new Token(Operation.Or, TokenType.Operation));
                    temp.Add(disjunction);
                }
            }

            if (temp.Count > 1)
                for (int i = 0; i < temp.Count; i++)
                    temp[i] = LogicalExpressionSyntaxAnalyzer.EncloseInBraces(temp[i]);

            foreach (List<Token> tokens in temp)
                pcnf.AddRange(tokens);

            LogicalExpressionSyntaxAnalyzer.AddSeparator(pcnf, new Token(Operation.And, TokenType.Operation));

            return pcnf;
        }

        public List<Token> GetDNF(List<Token> tokens)
        {
            List<Token> dnf = new List<Token>();

            Console.WriteLine("эквивалентные преобразования");
            //эквивалентные преобразования
            bool isTransformationApplicable = true;
            while (isTransformationApplicable)
                tokens = ApplyTransform(tokens, ref isTransformationApplicable);

            Console.WriteLine("законы де Моргана");
            //законы де Моргана
            bool isDeMorganApplicable = true;
            while (isDeMorganApplicable)
                tokens = ApplyDeMorgansLaw(tokens, ref isDeMorganApplicable);

            Console.WriteLine("двойное отрицание");
            //двойное отрицание
            RemoveTwiceNo(tokens);


            foreach (Token token in tokens)
            {
                Console.Write(token.Identifier);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("==========");
            Console.WriteLine();
            
            //дистрибутивность, идемпотентность, поглощение 

            return tokens;
        }

        public List<Token> GetCNF(List<Token> tokens)
        {
            List<Token> cnf = new List<Token>();

            //эквивалентные преобразования

            //де Морган
            //hundredInts.RemoveRange(20, 31).InsertRange(20, thirtyOneInts);

            //двойное отрицание

            //дистрибутивность, идемпотентность, поглощение

            return cnf;
        }

        public List<Token> ApplyTransform(List<Token> tokens, ref bool isChanged)
        {
            Dictionary<string, int> priorities = Operation.Priorities;
            int orPriority = priorities[Operation.Or];
            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(tokens);
            if (!polishNotation.Any(n => n.Type == TokenType.Operation
                && priorities[n.Identifier] < orPriority))
            {
                isChanged = false;

                return tokens;
            }

            int index = polishNotation.Select((n, i) => new { Item = n, Index = i })
                                               .Where(n => n.Item.Type == TokenType.Operation
                                                    && priorities[n.Item.Identifier] < orPriority)
                                               .Select(n => n.Index).First();

            Token operation = polishNotation[index];
            index--;

            int temp = 0;
            List<Token> rightOperandPolish = GetOperand(polishNotation, ref index, ref temp);
            List<Token> leftOperandPolish = GetOperand(polishNotation, ref index, ref temp);
            int expressionLength = rightOperandPolish.Count + leftOperandPolish.Count + 1;
            polishNotation.RemoveRange(++index, expressionLength);

            Dictionary<string, Delegate> transformations = Operation.Transformations;
            Delegate Transform = transformations[operation.Identifier];

            List<Token> leftOperand = (leftOperandPolish.Count > 1) ?
                LogicalExpressionSyntaxAnalyzer.GetAllExpressions(leftOperandPolish).Last()
                : leftOperandPolish;
            List<Token> rightOperand = (rightOperandPolish.Count > 1) ?
                LogicalExpressionSyntaxAnalyzer.GetAllExpressions(rightOperandPolish).Last()
                : rightOperandPolish;
            List<Token> transformResult = (List<Token>)Transform.DynamicInvoke(leftOperand, rightOperand);

            List<Token> transformPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(transformResult);

            polishNotation.InsertRange(index, transformPolish);
            List<Token> result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

            foreach (Token token in result)
            {
                Console.Write(token.Identifier);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("==========");
            Console.WriteLine();

            isChanged = true;
            return result;
        }

        private static List<Token> ApplyDeMorgansLaw(List<Token> tokens, ref bool isChanged)
        {
            Token not = new Token(Operation.Not, TokenType.Operation);
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);

            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(tokens);

            int index = polishNotation.Select((n, i) => new { Item = n, Index = i })
                                               .Where(n => n.Item.Equals(not)
                                                    && (polishNotation[n.Index - 1].Equals(and)
                                               || polishNotation[n.Index - 1].Equals(or)))
                                               .Select(n => n.Index).FirstOrDefault();
            
            if (index == 0)
            {
                isChanged = false;

                return tokens;
            }

            index--;
            Token operation = polishNotation[index];
            int tokensCountToGet = GetTokensCountToGet(polishNotation, index, operation);
            index--;
            List<List<Token>> operandsPolish = new List<List<Token>>();
            int expressionLength = 2;

            for (int i = 0; i < tokensCountToGet; i++)
                operandsPolish.Add(GetOperand(polishNotation, ref index, ref expressionLength, operation));
            operandsPolish.Reverse();

            polishNotation.RemoveRange(++index, expressionLength);

            List<List<Token>> operands = new List<List<Token>>();
            foreach (List<Token> operand in operandsPolish)
                operands.Add((operand.Count > 1) ?
                    LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last()
                    : operand);

            List<Token> transformResult = DeMorgansLaw(operands, operation);
            List<Token> transformPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(transformResult);

            polishNotation.InsertRange(index, transformPolish);
            List<Token> result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

            foreach (Token token in result)
            {
                Console.Write(token.Identifier);
            }
            Console.WriteLine();
            foreach (Token token in polishNotation)
            {
                Console.Write(token.Identifier);
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("==========");
            Console.WriteLine();

            isChanged = true;
            return result;
        }

        private static List<Token> GetOperand(List<Token> polishNotation, ref int index, 
            ref int expressionLength, Token operation = null)
        {
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);

            Dictionary<string, int> operandsCount = Operation.OperandsCount;

            int operandIndex = index;
            List<Token> result = new List<Token>();
            int tokensToGet = 1;
            while (tokensToGet != 0)
            {
                if (polishNotation[operandIndex].Type == TokenType.Operation)
                {
                    if (!polishNotation[operandIndex].Equals(operation))
                        result.Add(polishNotation[operandIndex]);

                    if (operandsCount.Any(n => n.Key == polishNotation[operandIndex].Identifier
                        && n.Value == 2))
                        tokensToGet++;

                    expressionLength++;
                }
                else
                {
                    result.Add(polishNotation[operandIndex]);
                    expressionLength++;

                    tokensToGet--;
                }

                operandIndex--;
            }
            result.Reverse();

            index = operandIndex;

            return result;
        }

        private static int GetTokensCountToGet(List<Token> polishNotation, int index, Token operation)
        {
            Dictionary<string, int> operandsCount = Operation.OperandsCount;

            int count = 1;
            int tokensToGet = 1;
            while (tokensToGet != 0)
            {
                if (polishNotation[index].Type == TokenType.Operation)
                {
                    if (operandsCount.Any(n => n.Key == polishNotation[index].Identifier
                        && n.Value == 2))
                    {
                        if (polishNotation[index].Identifier == operation.Identifier)
                            count++;

                        tokensToGet++;
                    }
                }
                else
                {
                    tokensToGet--;
                }
                index--;
            }

            return count;
        }

        private static List<Token> DeMorgansLaw(List<List<Token>> operands, Token operation)
        {
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);
            Token not = new Token(Operation.Not, TokenType.Operation);

            bool isAnd = operation.Equals(and);

            List<Token> result = new List<Token>();
            foreach (List<Token> operand in operands)
            {
                result.Add(not);
                result.AddRange((operand.Count > 1) ? 
                    LogicalExpressionSyntaxAnalyzer.EncloseInBraces(operand) 
                    : operand);
                result.Add(isAnd ? or : and);
            }
            result.RemoveAt(result.Count - 1);

            return result;
        }

        private static List<Token> RemoveTwiceNo(List<Token> tokens)
        {
            Token not = new Token(Operation.Not, TokenType.Operation);
            int indexToRemove = 0;
            bool isApplicable = true;
            while (isApplicable)
            {
                bool whetherToRemove = false;
                for (int i = 1; i < tokens.Count; i++)
                {
                    Token previousToken = tokens[i - 1];
                    Token followingToken = tokens[i];

                    if (previousToken.Equals(not) && followingToken.Equals(not))
                    {
                        indexToRemove = i - 1;

                        whetherToRemove = true;
                        isApplicable = true;

                        break;
                    }

                    isApplicable = false;
                }

                if (whetherToRemove)
                {
                    tokens.RemoveAt(indexToRemove);
                    tokens.RemoveAt(indexToRemove);
                }
            }

            return tokens;
        }
    }
}
