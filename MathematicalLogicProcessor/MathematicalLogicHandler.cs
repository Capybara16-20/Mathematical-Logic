using System;
using System.Collections.Generic;
using System.Linq;

namespace MathematicalLogicProcessor
{
    public class MathematicalLogicHandler
    {
        const string transformationsMessage = "Применяем эквивалентные преобразования.";
        const string deMorgansLawsMessage = "Применяем законы де Моргана.";
        const string removeTwiceNoMessage = "Удаляем двойные отрицания.";
        const string distributivityIdempotencyAbsorptionMessage = 
            "Применяем законы дистрибутивности, идемпотентности и поглощения.";

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

        public Dictionary<List<List<Token>>, string> GetDNF(List<Token> tokens)
        {
            Dictionary<List<List<Token>>, string> dnf = new Dictionary<List<List<Token>>, string>();

            //эквивалентные преобразования
            List<List<Token>> changes = new List<List<Token>> { tokens };
            bool isTransformationApplicable = true;
            while (isTransformationApplicable)
            {
                tokens = ApplyTransform(tokens, ref isTransformationApplicable);
                
                if (isTransformationApplicable)
                    changes.Add(tokens);
            }

            if (changes.Count > 1)
                dnf.Add(changes, transformationsMessage);

            //законы де Моргана
            changes = new List<List<Token>> { tokens };
            bool isDeMorganApplicable = true;
            while (isDeMorganApplicable)
            {
                tokens = ApplyDeMorgansLaws(tokens, ref isDeMorganApplicable);

                if (isDeMorganApplicable)
                    changes.Add(tokens);
            }

            if (changes.Count > 1)
                dnf.Add(changes, deMorgansLawsMessage);

            //двойное отрицание
            changes = new List<List<Token>> { tokens };
            bool isChanged;
            tokens = RemoveTwiceNo(tokens, out isChanged);
            
            if (isChanged)
            {
                changes.Add(tokens);
                dnf.Add(changes, removeTwiceNoMessage);
            }

            //дистрибутивность, идемпотентность, поглощение
            

            return dnf;
        }

        public Dictionary<List<List<Token>>, string> GetCNF(List<Token> tokens)
        {
            Dictionary<List<List<Token>>, string> cnf = new Dictionary<List<List<Token>>, string>();

            //эквивалентные преобразования
            List<List<Token>> changes = new List<List<Token>> { tokens };
            bool isTransformationApplicable = true;
            while (isTransformationApplicable)
            {
                tokens = ApplyTransform(tokens, ref isTransformationApplicable);

                if (isTransformationApplicable)
                    changes.Add(tokens);
            }

            if (changes.Count > 1)
                cnf.Add(changes, transformationsMessage);

            //законы де Моргана
            changes = new List<List<Token>> { tokens };
            bool isDeMorganApplicable = true;
            while (isDeMorganApplicable)
            {
                tokens = ApplyDeMorgansLaws(tokens, ref isDeMorganApplicable);

                if (isDeMorganApplicable)
                    changes.Add(tokens);
            }

            if (changes.Count > 1)
                cnf.Add(changes, deMorgansLawsMessage);

            //двойное отрицание
            changes = new List<List<Token>> { tokens };
            bool isChanged;
            tokens = RemoveTwiceNo(tokens, out isChanged);

            if (isChanged)
            {
                changes.Add(tokens);
                cnf.Add(changes, removeTwiceNoMessage);
            }

            //дистрибутивность, идемпотентность, поглощение


            return cnf;
        }

        public List<Token> ApplyTransform(List<Token> tokens, ref bool isChanged)
        {
            Dictionary<string, int> priorities = Operation.Priorities;
            int orPriority = priorities[Operation.Or];

            List<Token> result = tokens.GetRange(0, tokens.Count);
            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(result);
            if (!polishNotation.Any(n => n.Type == TokenType.Operation
                && priorities[n.Identifier] < orPriority))
            {
                isChanged = false;

                return result;
            }

            int index = polishNotation.Select((n, i) => new { Item = n, Index = i })
                                               .Where(n => n.Item.Type == TokenType.Operation
                                                    && priorities[n.Item.Identifier] < orPriority)
                                               .Select(n => n.Index).First();

            Token operation = polishNotation[index];
            index--;

            int temp = 0;
            List<Token> rightOperandPolish = GetOperandPolish(polishNotation, ref index, ref temp);
            List<Token> leftOperandPolish = GetOperandPolish(polishNotation, ref index, ref temp);
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
            result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

            bool isTransformNotConstantsApplicable = true;
            while (isTransformNotConstantsApplicable)
                result = TransformNotConstants(result, ref isTransformNotConstantsApplicable);

            isChanged = true;
            return result;
        }

        private static List<Token> ApplyDeMorgansLaws(List<Token> tokens, ref bool isChanged)
        {
            Token not = new Token(Operation.Not, TokenType.Operation);
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);

            List<Token> result = tokens.GetRange(0, tokens.Count);
            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(result);

            int index = polishNotation.Select((n, i) => new { Item = n, Index = i })
                                               .Where(n => n.Item.Equals(not)
                                                    && (polishNotation[n.Index - 1].Equals(and)
                                               || polishNotation[n.Index - 1].Equals(or)))
                                               .Select(n => n.Index).FirstOrDefault();
            if (index == 0)
            {
                isChanged = false;

                return result;
            }

            index--;
            Token operation = polishNotation[index];
            int tokensCountToGet = GetOperandsCountToGet(polishNotation, index, operation);
            index--;
            List<List<Token>> operandsPolish = new List<List<Token>>();
            int expressionLength = 2;

            for (int i = 0; i < tokensCountToGet; i++)
                operandsPolish.Add(GetOperandPolish(polishNotation, ref index, ref expressionLength));
            operandsPolish.Reverse();

            polishNotation.RemoveRange(++index, expressionLength);

            List<List<Token>> operands = new List<List<Token>>();
            foreach (List<Token> operand in operandsPolish)
                operands.Add((operand.Count > 1) ?
                    LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last()
                    : operand);

            List<Token> transformResult = DeMorgansLaws(operands, operation);
            List<Token> transformPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(transformResult);

            polishNotation.InsertRange(index, transformPolish);
            result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

            bool isTransformNotConstantsApplicable = true;
            while (isTransformNotConstantsApplicable)
                result = TransformNotConstants(result, ref isTransformNotConstantsApplicable);

            isChanged = true;
            return result;
        }

        private static List<Token> DeMorgansLaws(List<List<Token>> operands, Token operation)
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

        private static List<Token> RemoveTwiceNo(List<Token> tokens, out bool isChanged)
        {
            List<Token> result = tokens.GetRange(0, tokens.Count);
            isChanged = false;
            Token not = new Token(Operation.Not, TokenType.Operation);
            int indexToRemove = 0;
            bool isApplicable = true;
            while (isApplicable)
            {
                bool whetherToRemove = false;
                for (int i = 1; i < result.Count; i++)
                {
                    Token previousToken = result[i - 1];
                    Token followingToken = result[i];

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
                    result.RemoveAt(indexToRemove);
                    result.RemoveAt(indexToRemove);
                    isChanged = true;
                }
            }

            return result;
        }

        private static List<Token> TransformNotConstants(List<Token> tokens, ref bool isChanged)
        {
            Token not = new Token(Operation.Not, TokenType.Operation);
            Token one = new Token(Operand.One, TokenType.Const);
            Token zero = new Token(Operand.Zero, TokenType.Const);

            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(tokens);
            int index = polishNotation.Select((n, i) => new { Item = n, Index = i })
                                               .Where(n => n.Item.Equals(not)
                                                    && (polishNotation[n.Index - 1].Type == TokenType.Const))
                                               .Select(n => n.Index).FirstOrDefault();
            if (index == 0)
            {
                isChanged = false;

                return tokens;
            }

            index--;
            Token constant = polishNotation[index];
            polishNotation.RemoveAt(index);
            polishNotation.RemoveAt(index);

            polishNotation.Insert(index, constant.Identifier == "1" ? zero : one);

            List<Token> result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
            isChanged = true;
            return result;
        }

        private static List<Token> ApplyDistributivityIdempotencyAbsorptionLaws(List<Token> tokens, ref bool isChanged)
        {
            List<Token> result = tokens.GetRange(0, tokens.Count);



            return result;
        }

        private static List<Token> RemoveConstants(List<Token> tokens)
        {
            List<Token> result = new List<Token>();


            return result;
        }

        private static List<Token> GetOperandPolish(List<Token> polishNotation, ref int index,
            ref int expressionLength)
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
                    if (operandsCount.Any(n => n.Key == polishNotation[operandIndex].Identifier
                            && n.Value == 2))
                        tokensToGet++;
                }
                else
                {
                    tokensToGet--;
                }

                result.Add(polishNotation[operandIndex]);
                expressionLength++;
                operandIndex--;
            }
            result.Reverse();

            index = operandIndex;

            return result;
        }

        private static int GetOperandsCountToGet(List<Token> polishNotation, int index, Token operation)
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
                        if (polishNotation[index].Equals(operation))
                        {
                            count++;
                        }
                        else
                        {
                            int tokensToPass = 1;
                            while (tokensToPass != 0)
                            {
                                if (polishNotation[index].Type == TokenType.Operation)
                                {
                                    if (operandsCount.Any(n => n.Key == polishNotation[index].Identifier 
                                        && n.Value == 2))
                                    {
                                        tokensToPass++;
                                    }
                                }
                                else
                                {
                                    tokensToPass--;
                                }

                                index--;
                            }
                        }

                        tokensToGet--;
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
    }
}
