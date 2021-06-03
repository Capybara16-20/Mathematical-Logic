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
            Dictionary<string, Delegate> transformations = Operation.Transformations;
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            List<Token> dnf = new List<Token>();

            //эквивалентные преобразования
            bool isTransformationApplicable = true;
            while (isTransformationApplicable)
                tokens = ApplyTransform(tokens, ref isTransformationApplicable);

            //законы де Моргана
            /*bool isDeMorganApplicable = true;
            while (isDeMorganApplicable)
            {


            }*/

            //двойное отрицание
            //RemoveTwiceNo(dnf);

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

            index--;
            List<Token> rightOperandPolish = GetOperand(polishNotation, ref index);
            List<Token> leftOperandPolish = GetOperand(polishNotation, ref index);

            int expressionLength = rightOperandPolish.Count + leftOperandPolish.Count + 1;
            List<Token> polishToTransform = polishNotation.GetRange(++index, expressionLength);
            polishNotation.RemoveRange(index, expressionLength);

            Token operation = polishToTransform.Last();
            List<Token> expressionToTransform = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishToTransform).Last();

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
            result = LogicalExpressionSyntaxAnalyzer.RemoveExtraBraces(result);
            return result;
        }

        private List<Token> GetOperand(List<Token> tokens, ref int index)
        {
            Dictionary<string, int> operandsCount = Operation.OperandsCount;

            int operandIndex = index;
            List<Token> result = new List<Token>();
            int tokensCount = 1;
            while (tokensCount != 0)
            {
                if (tokens[operandIndex].Type == TokenType.Operation)
                {
                    result.Add(tokens[operandIndex]);
                    if (operandsCount.Any(n => n.Key == tokens[operandIndex].Identifier
                        && n.Value == 2))
                        tokensCount++;
                }
                else
                {
                    result.Add(tokens[operandIndex]);
                    tokensCount--;
                }
                operandIndex--;
            }
            result.Reverse();

            index = operandIndex;
            return result;
        }

        private static List<Token> ApplyDeMorgansLaw(List<Token> tokens, Token operation)
        {
            List<Token> result = new List<Token>();

            List<List<Token>> operands = new List<List<Token>>();
            List<Token> operand = new List<Token>();
            bool isNextOperand = false;
            foreach (Token token in tokens)
            {
                if (token.Type == TokenType.Operation && token.Equals(operation))
                {
                    isNextOperand = true;

                    continue;
                }

                if (isNextOperand)
                {
                    operands.Add(operand);
                    operand = new List<Token> { token };

                    isNextOperand = false;
                }
                else
                {
                    operand.Add(token);
                }
            }
            operands.Add(operand);

            for (int i = 0; i < operands.Count; i++)
            {
                if (operands[i].Count > 1)
                    operands[i] = LogicalExpressionSyntaxAnalyzer.EncloseInBraces(operands[i]);

                operands[i].Insert(0, new Token(Operation.Not, TokenType.Operation));

                result.AddRange(operands[i]);
                result = LogicalExpressionSyntaxAnalyzer.AddSeparator(result, new Token(Operation.And, TokenType.Operation));
            }

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
