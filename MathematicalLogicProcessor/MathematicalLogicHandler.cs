using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicalLogicProcessor
{
    public class MathematicalLogicHandler
    {
        private const string findSetsFormat = "Найдём наборы, на которых функция принимает {0} значение: ";
        private const string alignFormat = "В соответствие найденным наборам поставим элементарные " +
            "{0} по всем переменным,";
        private const string negationConditionMessage = "если переменная в наборе принимает значение {0}, " +
            "то она будет записана с отрицанием:";
        private const string conformityFormat = "{0} : {1}";
        private const string associationFormat = "Объединим {0} с помощью операции {1}:";

        private const string transformationsMessage = "Применяем эквивалентные преобразования.";
        private const string deMorgansLawsMessage = "Применяем законы де Моргана.";
        private const string removeTwiceNoMessage = "Удаляем двойные отрицания.";
        private const string distributivityLawMessage = "Применяем закон дистрибутивности.";
        private const string idempotencyLawMessage = "Применяем закон идемпотентности.";
        private const string removeConstantsMessage = "Избавляемся от констант.";
        private const string absorptionLawMessage = "Применяем закон поглощения.";
        private const string dnfYetMessage = "Выражение уже находится в ДНФ.";
        private const string cnfYetMessage = "Выражение уже находится в КНФ.";

        private TruthTable truthTable;
        private ZhegalkinPolynomial zhegalkinPolynomial;
        private PostClassification classification;

        public TruthTable TruthTable { get { return truthTable; } }
        public ZhegalkinPolynomial ZhegalkinPolynomial { get { return zhegalkinPolynomial; } }
        public PostClassification PostClassification { get { return classification; } }

        public MathematicalLogicHandler(string expression)
        {
            LogicalExpressionSyntaxAnalyzer analyzer;
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
            zhegalkinPolynomial = new ZhegalkinPolynomial(variables, truthTable);
            classification = new PostClassification(TruthTable, zhegalkinPolynomial);
        }

        public MathematicalLogicHandler(int functionNumber)
        {
            truthTable = new TruthTable(functionNumber);
            List<Operand> variables = truthTable.Variables;
            zhegalkinPolynomial = new ZhegalkinPolynomial(variables, truthTable);
        }

        public MathematicalLogicHandler(bool[] functionVector)
        {
            truthTable = new TruthTable(functionVector);
            List<Operand> variables = truthTable.Variables;
            zhegalkinPolynomial = new ZhegalkinPolynomial(variables, truthTable);
        }

        public MathematicalLogicHandler(TruthTable truthTable)
        {
            this.truthTable = truthTable;
        }

        public List<Token> GetPDNF(out List<string> decision)
        {
            const string truth = "истинное";
            const string setFormat = "{{{0}}}";
            const string separator = ",";
            const string conjunctions = "конъюнкции";
            const string zero = "0";
            const string or = "ИЛИ";

            decision = new List<string>();
            string findSets = string.Format(findSetsFormat, truth);
            decision.Add(findSets);

            List<string> setsStr = new List<string>();
            StringBuilder sbSets = new StringBuilder();
            List<Token> pdnf = new List<Token>();
            bool[] vector = truthTable.FunctionVector;
            List<Operand> variables = truthTable.Variables;
            int variablesCount = truthTable.Variables.Count;
            List<List<Token>> temp = new List<List<Token>>();
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i])
                {
                    StringBuilder sbSet = new StringBuilder();
                    List<Token> conjunction = new List<Token>();
                    for (int j = 0; j < variablesCount; j++)
                    {
                        if (truthTable[i, j])
                        {
                            sbSet.Append("1");
                            conjunction.Add(variables[j]);
                        }
                        else
                        {
                            sbSet.Append("0");
                            conjunction.Add(new Token(Operation.Not, TokenType.Operation));
                            conjunction.Add(variables[j]);
                        }
                    }

                    string set = string.Format(setFormat, sbSet.ToString());
                    sbSets.Append(set);
                    sbSets.Append(separator);

                    setsStr.Add(set);

                    LogicalExpressionSyntaxAnalyzer.AddSeparator(conjunction, new Token(Operation.And, TokenType.Operation));
                    temp.Add(conjunction);
                }
            }

            sbSets.Remove(sbSets.Length - 1, 1);
            string sets = sbSets.ToString();
            decision.Add(sets);

            string align = string.Format(alignFormat, conjunctions);
            decision.Add(align);

            string negationCondition = string.Format(negationConditionMessage, zero);
            decision.Add(negationCondition);

            for (int i = 0; i < temp.Count; i++)
            {
                pdnf.AddRange(temp[i]);

                StringBuilder sbTokens = new StringBuilder();
                foreach (Token token in temp[i])
                    sbTokens.Append(token.Identifier);
                string tokens = sbTokens.ToString();

                string conformity = string.Format(conformityFormat, setsStr[i], tokens);
                decision.Add(conformity);
            }

            LogicalExpressionSyntaxAnalyzer.AddSeparator(pdnf, new Token(Operation.Or, TokenType.Operation));

            string association = string.Format(associationFormat, conjunctions, or);
            decision.Add(association);

            StringBuilder sbPdnf = new StringBuilder();
            foreach (Token token in pdnf)
                sbPdnf.Append(token.Identifier);
            decision.Add(sbPdnf.ToString());

            return pdnf;
        }

        public List<Token> GetPCNF(out List<string> decision)
        {
            const string lie = "ложное";
            const string setFormat = "{{{0}}}";
            const string separator = ",";
            const string disjunctions = "дизъюнкции";
            const string one = "1";
            const string and = "И";

            decision = new List<string>();
            string findSets = string.Format(findSetsFormat, lie);
            decision.Add(findSets);

            List<string> setsStr = new List<string>();
            StringBuilder sbSets = new StringBuilder();
            List<Token> pcnf = new List<Token>();
            bool[] vector = truthTable.FunctionVector;
            List<Operand> variables = truthTable.Variables;
            int variablesCount = truthTable.Variables.Count;

            List<List<Token>> temp = new List<List<Token>>();
            for (int i = 0; i < vector.Length; i++)
            {
                if (!vector[i])
                {
                    StringBuilder sbSet = new StringBuilder();
                    List<Token> disjunction = new List<Token>();
                    for (int j = 0; j < variablesCount; j++)
                    {
                        if (!truthTable[i, j])
                        {
                            sbSet.Append("0");
                            disjunction.Add(variables[j]);
                        }
                        else
                        {
                            sbSet.Append("1");
                            disjunction.Add(new Token(Operation.Not, TokenType.Operation));
                            disjunction.Add(variables[j]);
                        }
                    }

                    string set = string.Format(setFormat, sbSet.ToString());
                    sbSets.Append(set);
                    sbSets.Append(separator);

                    setsStr.Add(set);

                    LogicalExpressionSyntaxAnalyzer.AddSeparator(disjunction, new Token(Operation.Or, TokenType.Operation));
                    temp.Add(disjunction);
                }
            }

            sbSets.Remove(sbSets.Length - 1, 1);
            string sets = sbSets.ToString();
            decision.Add(sets);

            string align = string.Format(alignFormat, disjunctions);
            decision.Add(align);

            string negationCondition = string.Format(negationConditionMessage, one);
            decision.Add(negationCondition);

            if (temp.Count > 1)
                for (int i = 0; i < temp.Count; i++)
                    temp[i] = LogicalExpressionSyntaxAnalyzer.EncloseInBraces(temp[i]);

            for (int i = 0; i < temp.Count; i++)
            {
                pcnf.AddRange(temp[i]);

                StringBuilder sbTokens = new StringBuilder();
                foreach (Token token in temp[i])
                    sbTokens.Append(token.Identifier);
                string tokens = sbTokens.ToString();

                string conformity = string.Format(conformityFormat, setsStr[i], tokens);
                decision.Add(conformity);
            }

            LogicalExpressionSyntaxAnalyzer.AddSeparator(pcnf, new Token(Operation.And, TokenType.Operation));

            string association = string.Format(associationFormat, disjunctions, and);
            decision.Add(association);

            StringBuilder sbPcnf = new StringBuilder();
            foreach (Token token in pcnf)
                sbPcnf.Append(token.Identifier);
            decision.Add(sbPcnf.ToString());

            return pcnf;
        }

        public Dictionary<List<List<Token>>, string> GetDNF(List<Token> tokens)
        {
            Dictionary<List<List<Token>>, string> dnf = new Dictionary<List<List<Token>>, string>();

            bool isApplicable = true;
            while (isApplicable)
            {
                int changesCount = 0;

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
                {
                    dnf.Add(changes, transformationsMessage);
                    changesCount++;
                }

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
                {
                    dnf.Add(changes, deMorgansLawsMessage);
                    changesCount++;
                }

                //двойное отрицание
                changes = new List<List<Token>> { tokens };
                bool isChanged;
                tokens = RemoveTwiceNo(tokens, out isChanged);

                if (isChanged)
                {
                    changes.Add(tokens);
                    dnf.Add(changes, removeTwiceNoMessage);
                    changesCount++;
                }

                //дистрибутивность
                changes = new List<List<Token>> { tokens };
                bool isDNFDistributivityLawApplicable = true;
                while (isDNFDistributivityLawApplicable)
                {
                    tokens = ApplyDNFDistributivityLaw(tokens, ref isDNFDistributivityLawApplicable);

                    if (isDNFDistributivityLawApplicable)
                        changes.Add(tokens);
                }

                if (changes.Count > 1)
                {
                    dnf.Add(changes, distributivityLawMessage);
                    changesCount++;
                }

                //идемпотентность
                changes = new List<List<Token>> { tokens };
                bool isIdempotencyLawApplicable = true;
                while (isIdempotencyLawApplicable)
                {
                    tokens = ApplyIdempotencyLaw(tokens, ref isIdempotencyLawApplicable);

                    if (isIdempotencyLawApplicable)
                        changes.Add(tokens);
                }

                if (changes.Count > 1)
                {
                    dnf.Add(changes, idempotencyLawMessage);
                    changesCount++;
                }

                //удаление констант
                changes = new List<List<Token>> { tokens };
                bool isRemoveConstantApplicable = true;
                while (isRemoveConstantApplicable)
                {
                    tokens = RemoveConstants(tokens, ref isRemoveConstantApplicable);

                    if (isRemoveConstantApplicable)
                        changes.Add(tokens);
                }

                if (changes.Count > 1)
                {
                    dnf.Add(changes, removeConstantsMessage);
                    changesCount++;
                }

                //поглощение
                changes = new List<List<Token>> { tokens };
                bool isAbsorptionLawApplicable = true;
                while (isAbsorptionLawApplicable)
                {
                    tokens = ApplyAbsorptionLaw(tokens, ref isAbsorptionLawApplicable);

                    if (isAbsorptionLawApplicable)
                        changes.Add(tokens);
                }

                if (changes.Count > 1)
                {
                    dnf.Add(changes, absorptionLawMessage);
                    changesCount++;
                }
                
                if (changesCount == 0)
                    isApplicable = false;
            }

            if (dnf.Count == 0)
            {
                List<List<Token>> decision = new List<List<Token>> { tokens };
                dnf.Add(decision, dnfYetMessage);
            }

            return dnf;
        }

        public Dictionary<List<List<Token>>, string> GetCNF(List<Token> tokens)
        {
            Dictionary<List<List<Token>>, string> cnf = new Dictionary<List<List<Token>>, string>();

            bool isApplicable = true;
            while (isApplicable)
            {
                int changesCount = 0;

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
                {
                    cnf.Add(changes, transformationsMessage);
                    changesCount++;
                }

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
                {
                    cnf.Add(changes, deMorgansLawsMessage);
                    changesCount++;
                }

                //двойное отрицание
                changes = new List<List<Token>> { tokens };
                bool isChanged;
                tokens = RemoveTwiceNo(tokens, out isChanged);

                if (isChanged)
                {
                    changes.Add(tokens);
                    cnf.Add(changes, removeTwiceNoMessage);
                    changesCount++;
                }

                //дистрибутивность
                changes = new List<List<Token>> { tokens };
                bool isCNFDistributivityLawApplicable = true;
                while (isCNFDistributivityLawApplicable)
                {
                    tokens = ApplyCNFDistributivity(tokens, ref isCNFDistributivityLawApplicable);

                    if (isCNFDistributivityLawApplicable)
                        changes.Add(tokens);
                }

                if (changes.Count > 1)
                {
                    cnf.Add(changes, distributivityLawMessage);
                    changesCount++;
                }

                //идемпотентность
                changes = new List<List<Token>> { tokens };
                bool isIdempotencyLawApplicable = true;
                while (isIdempotencyLawApplicable)
                {
                    tokens = ApplyIdempotencyLaw(tokens, ref isIdempotencyLawApplicable);

                    if (isIdempotencyLawApplicable)
                        changes.Add(tokens);
                }

                if (changes.Count > 1)
                {
                    cnf.Add(changes, idempotencyLawMessage);
                    changesCount++;
                }

                //удаление констант
                changes = new List<List<Token>> { tokens };
                bool isRemoveConstantApplicable = true;
                while (isRemoveConstantApplicable)
                {
                    tokens = RemoveConstants(tokens, ref isRemoveConstantApplicable);

                    if (isRemoveConstantApplicable)
                        changes.Add(tokens);
                }

                if (changes.Count > 1)
                {
                    cnf.Add(changes, removeConstantsMessage);
                    changesCount++;
                }

                //поглощение
                changes = new List<List<Token>> { tokens };
                bool isAbsorptionLawApplicable = true;
                while (isAbsorptionLawApplicable)
                {
                    tokens = ApplyAbsorptionLaw(tokens, ref isAbsorptionLawApplicable);

                    if (isAbsorptionLawApplicable)
                        changes.Add(tokens);
                }

                if (changes.Count > 1)
                {
                    cnf.Add(changes, absorptionLawMessage);
                    changesCount++;
                }

                if (changesCount == 0)
                    isApplicable = false;
            }

            if (cnf.Count == 0)
            {
                List<List<Token>> decision = new List<List<Token>> { tokens };
                cnf.Add(decision, cnfYetMessage);
            }

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

            List<Token> rightOperandPolish = GetOperandPolish(polishNotation, ref index);
            List<Token> leftOperandPolish = GetOperandPolish(polishNotation, ref index);
            int expressionLength = rightOperandPolish.Count + leftOperandPolish.Count + 1;

            index++;
            polishNotation.RemoveRange(index, expressionLength);

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

            List<List<Token>> operandsPolish = GetOperandsPolish(polishNotation, ref index, operation);

            int expressionLength = operandsPolish.Count;
            foreach (List<Token> operand in operandsPolish)
                expressionLength += operand.Count;

            index++;
            polishNotation.RemoveRange(index, expressionLength);

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

        private static List<Token> ApplyDNFDistributivityLaw(List<Token> tokens, ref bool isChanged)
        {
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);

            List<Token> result = tokens.GetRange(0, tokens.Count);
            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(result);
            for (int i = 0; i < polishNotation.Count; i++)
            {
                if (polishNotation[i].Equals(and))
                {
                    int index = i - 1;
                    List<Token> rightOperandPolish = GetOperandPolish(polishNotation, ref index);
                    List<Token> leftOperandPolish = GetOperandPolish(polishNotation, ref index);
                    index++;

                    int operandIndex = leftOperandPolish.Count - 1;
                    List<List<Token>> leftOperandOperandsPolish = GetOperandsPolish(leftOperandPolish, ref operandIndex, or);

                    operandIndex = rightOperandPolish.Count - 1;
                    List<List<Token>> rightOperandOperandsPolish = GetOperandsPolish(rightOperandPolish, ref operandIndex, or);

                    if (leftOperandOperandsPolish.Count > 1 
                        || rightOperandOperandsPolish.Count > 1)
                    {
                        List<List<Token>> leftOperandOperands = new List<List<Token>>();
                        foreach (List<Token> operand in leftOperandOperandsPolish)
                            leftOperandOperands.Add((operand.Count > 1) ?
                                LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last()
                                : operand);

                        List<List<Token>> rightOperandOperands = new List<List<Token>>();
                        foreach (List<Token> operand in rightOperandOperandsPolish)
                            rightOperandOperands.Add((operand.Count > 1) ?
                                LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last()
                                : operand);

                        List<Token> newTokens = DistributivityLaw(leftOperandOperands, rightOperandOperands, and);
                        List<Token> newPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(newTokens);

                        int expressionLength = rightOperandPolish.Count + leftOperandPolish.Count + 1;
                        polishNotation.RemoveRange(index, expressionLength);
                        polishNotation.InsertRange(index, newPolish);
                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

                        isChanged = true;
                        return result;
                    }
                }
            }

            isChanged = false;
            return result;
        }

        private static List<Token> ApplyCNFDistributivity(List<Token> tokens, ref bool isChanged)
        {
            isChanged = false;

            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);

            List<Token> result = tokens.GetRange(0, tokens.Count);
            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(result);

            for (int i = 0; i < polishNotation.Count; i++)
            {
                if (polishNotation[i].Equals(or))
                {
                    int index = i - 1;
                    List<Token> rightOperandPolish = GetOperandPolish(polishNotation, ref index);
                    List<Token> leftOperandPolish = GetOperandPolish(polishNotation, ref index);
                    index++;

                    int operandIndex = leftOperandPolish.Count - 1;
                    List<List<Token>> leftOperandOperandsPolish = GetOperandsPolish(leftOperandPolish, ref operandIndex, and);

                    operandIndex = rightOperandPolish.Count - 1;
                    List<List<Token>> rightOperandOperandsPolish = GetOperandsPolish(rightOperandPolish, ref operandIndex, and);

                    if (leftOperandOperandsPolish.Count > 1
                        || rightOperandOperandsPolish.Count > 1)
                    {
                        List<List<Token>> leftOperandOperands = new List<List<Token>>();
                        foreach (List<Token> operand in leftOperandOperandsPolish)
                            leftOperandOperands.Add((operand.Count > 1) ?
                                LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last()
                                : operand);

                        List<List<Token>> rightOperandOperands = new List<List<Token>>();
                        foreach (List<Token> operand in rightOperandOperandsPolish)
                            rightOperandOperands.Add((operand.Count > 1) ?
                                LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last()
                                : operand);

                        List<Token> newTokens = DistributivityLaw(leftOperandOperands, rightOperandOperands, or);
                        List<Token> newPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(newTokens);

                        int expressionLength = rightOperandPolish.Count + leftOperandPolish.Count + 1;
                        polishNotation.RemoveRange(index, expressionLength);
                        polishNotation.InsertRange(index, newPolish);
                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

                        isChanged = true;
                        break;
                    }
                }
            }

            return result;
        }

        public static List<Token> DistributivityLaw(List<List<Token>> leftOperands,
            List<List<Token>> rightOperands, Token operation)
        {
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);

            bool isAnd = operation.Equals(and);

            List<Token> result = new List<Token>();
            foreach (List<Token> leftOperand in leftOperands)
            {
                foreach (List<Token> rightOperand in rightOperands)
                {
                    List<Token> newOperand = new List<Token>();
                    newOperand.AddRange(leftOperand);
                    newOperand.Add(isAnd ? and : or);
                    newOperand.AddRange(rightOperand);

                    result.AddRange(LogicalExpressionSyntaxAnalyzer.IsNeedBraces(newOperand, isAnd ? or : and)
                    ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(newOperand) : newOperand);
                    result.Add(isAnd ? or : and);
                }
            }
            result.RemoveAt(result.Count - 1);

            return result;
        }

        public static List<Token> ApplyIdempotencyLaw(List<Token> tokens, ref bool isChanged)
        {
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);
            Token not = new Token(Operation.Not, TokenType.Operation);
            Token zero = new Token(Operand.Zero, TokenType.Const);
            Token one = new Token(Operand.One, TokenType.Const);

            List<Token> result = tokens.GetRange(0, tokens.Count);
            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(result);
            for (int i = 0; i < polishNotation.Count; i++)
            {
                bool isAnd;
                if ((isAnd = polishNotation[i].Equals(and)) || polishNotation[i].Equals(or))
                {
                    int index = i;
                    List<List<Token>> operandsPolish = GetOperandsPolish(polishNotation, ref index, isAnd ? and : or);
                    index++;

                    int expressionLength = operandsPolish.Count - 1;
                    foreach (List<Token> operand in operandsPolish)
                        expressionLength += operand.Count;

                    for (int j = 0; j < operandsPolish.Count; j++)
                    {
                        for (int k = 0; k < j; k++)
                        {
                            List<Token> leftOperand = operandsPolish[k].GetRange(0, operandsPolish[k].Count);
                            List<Token> rightOperand = operandsPolish[j].GetRange(0, operandsPolish[j].Count);

                            if (leftOperand.Count == 1 && leftOperand[0].Type == TokenType.Const
                                && rightOperand.Count == 1 && rightOperand[0].Type == TokenType.Const)
                                break;

                            if (AreOperandsEqual(leftOperand, rightOperand))
                            {
                                polishNotation.RemoveRange(index, expressionLength);
                                operandsPolish.RemoveAt(j);
                                List<List<Token>> operands = new List<List<Token>>();
                                foreach (List<Token> operand in operandsPolish)
                                    operands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());
                                List<Token> newTokens = AddSeparators(operands, isAnd ? and : or);
                                List <Token> newPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(newTokens);
                                polishNotation.InsertRange(index, newPolish);
                                result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

                                isChanged = true;
                                return result;
                            }
                            else
                            {
                                int leftLastIndex = leftOperand.Count - 1;
                                int rightLastIndex = rightOperand.Count - 1;
                                if (leftLastIndex > 0 && leftOperand[leftLastIndex].Equals(not))
                                {
                                    leftOperand.RemoveAt(leftLastIndex);
                                    if (AreOperandsEqual(leftOperand, rightOperand))
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        polishNotation.Insert(index, isAnd ? zero : one);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

                                        isChanged = true;
                                        return result;
                                    }
                                }
                                else if (rightLastIndex > 0 && rightOperand[rightLastIndex].Equals(not))
                                {
                                    rightOperand.RemoveAt(rightLastIndex);
                                    if (AreOperandsEqual(leftOperand, rightOperand))
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        polishNotation.Insert(index, isAnd ? zero : one);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

                                        isChanged = true;
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            isChanged = false;
            return result;
        }

        public static List<Token> ApplyAbsorptionLaw(List<Token> tokens, ref bool isChanged)
        {
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);

            List<Token> result = tokens.GetRange(0, tokens.Count);
            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(result);
            for (int i = 0; i < polishNotation.Count; i++)
            {
                bool isAnd;
                if ((isAnd = polishNotation[i].Equals(and)) || polishNotation[i].Equals(or))
                {
                    int index = i - 1;

                    List<Token> rightOperandPolish = GetOperandPolish(polishNotation, ref index);
                    List<Token> leftOperandPolish = GetOperandPolish(polishNotation, ref index);
                    index++;

                    int leftIndex = leftOperandPolish.Count - 1;
                    List<List<Token>> leftOperandOperandsPolish = GetOperandsPolish(leftOperandPolish, ref leftIndex,
                        isAnd ? and : or);

                    int rightIndex = rightOperandPolish.Count - 1;
                    List<List<Token>> rightOperandOperandsPolish = GetOperandsPolish(rightOperandPolish, ref rightIndex,
                        isAnd ? and : or);

                    for (int j = 0; j < leftOperandOperandsPolish.Count; j++)
                    {
                        for (int k = 0; k < rightOperandOperandsPolish.Count; k++)
                        {
                            if (IsAbsorptionLawApplicable(leftOperandOperandsPolish[j], 
                                rightOperandOperandsPolish[k], isAnd ? or : and))
                            {
                                int expressionLength = rightOperandPolish.Count + leftOperandPolish.Count + 1;
                                polishNotation.RemoveRange(index, expressionLength);

                                List<List<Token>> newOperands = AbsorptionLaw(leftOperandOperandsPolish[j], 
                                    rightOperandOperandsPolish[k], isAnd ? or : and);

                                List<Token> leftOperand = newOperands.First();
                                List<Token> rightOperand = newOperands.Last();

                                newOperands = new List<List<Token>>();
                                if (j != 0)
                                {
                                    List<List<Token>> operandsBeforeLeftPolish = leftOperandOperandsPolish
                                        .GetRange(0, j);

                                    List<List<Token>> operandsBeforeLeft = new List<List<Token>>();
                                    foreach (List<Token> operand in operandsBeforeLeftPolish)
                                        operandsBeforeLeft.Add(LogicalExpressionSyntaxAnalyzer
                                            .GetAllExpressions(operand).Last());

                                    newOperands.Add(AddSeparators(operandsBeforeLeft, isAnd ? and : or));
                                }

                                if (leftOperand.Count > 0)
                                    newOperands.Add(leftOperand);

                                int leftCount = leftOperandOperandsPolish.Count;
                                if (j != leftCount - 1)
                                {
                                    List<List<Token>> operandsAfterLeftPolish = leftOperandOperandsPolish
                                        .GetRange(j + 1, leftCount - j - 1);

                                    List<List<Token>> operandsAfterLeft = new List<List<Token>>();
                                    foreach (List<Token> operand in operandsAfterLeftPolish)
                                        operandsAfterLeft.Add(LogicalExpressionSyntaxAnalyzer
                                            .GetAllExpressions(operand).Last());

                                    newOperands.Add(AddSeparators(operandsAfterLeft, isAnd ? and : or));
                                }

                                if (rightOperand.Count > 0)
                                    newOperands.Add(rightOperand);

                                List<Token> newTokens = AddSeparators(newOperands, isAnd ? and : or);
                                List<Token> newPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(newTokens);
                                polishNotation.InsertRange(index, newPolish);
                                result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();

                                isChanged = true;
                                return result;
                            }
                        }
                    }

                    
                }
            }

            isChanged = false;
            return result;
        }

        private static bool IsAbsorptionLawApplicable(List<Token> leftOperand, List<Token> rightOperand, Token operation)
        {
            Token not = new Token(Operation.Not, TokenType.Operation);

            int leftOperandIndex = leftOperand.Count - 1;
            List<List<Token>> leftOperands = GetOperandsPolish(leftOperand, ref leftOperandIndex, operation);

            int rightOperandIndex = rightOperand.Count - 1;
            List<List<Token>> rightOperands = GetOperandsPolish(rightOperand, ref rightOperandIndex, operation);

            int leftOperandsCount = leftOperands.Count;
            int rightOperandsCount = rightOperands.Count;
            if (leftOperandsCount == rightOperandsCount)
                return false;

            int notMatchesCount = 0;
            int matchesCount = 0;
            foreach (List<Token> leftTokens in leftOperands)
            {
                foreach (List<Token> rightTokens in rightOperands)
                {
                    int notIndex = 1;
                    if (leftTokens.SequenceEqual(rightTokens))
                    {
                        matchesCount++;
                    }
                    else if (leftTokens.Count > notIndex && leftTokens[notIndex].Equals(not))
                    {
                        List<Token> tokens = leftTokens.GetRange(0, leftTokens.Count);
                        tokens.RemoveAt(notIndex);

                        if (tokens.SequenceEqual(rightTokens))
                            notMatchesCount++;
                    }
                    else if (rightTokens.Count > notIndex && rightTokens[notIndex].Equals(not))
                    {
                        List<Token> tokens = rightTokens.GetRange(0, rightTokens.Count);
                        tokens.RemoveAt(notIndex);

                        if (leftTokens.SequenceEqual(tokens))
                            notMatchesCount++;
                    }
                }
            }

            if (notMatchesCount > 1)
                return false;

            if (leftOperandsCount > rightOperandsCount)
            {
                if (matchesCount + notMatchesCount == leftOperandsCount - 1)
                {
                    return true;
                }
            }
            else if (matchesCount + notMatchesCount == rightOperandsCount - 1)
            {
                return true;
            }

            return false;
        }

        private static List<List<Token>> AbsorptionLaw(List<Token> leftOperand, List<Token> rightOperand, Token operation)
        {
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);
            Token not = new Token(Operation.Not, TokenType.Operation);

            bool isAnd = operation.Equals(and);

            int operandIndex = leftOperand.Count - 1;
            List<List<Token>> leftOperandsPolish = GetOperandsPolish(leftOperand, ref operandIndex, operation);

            operandIndex = rightOperand.Count - 1;
            List<List<Token>> rightOperandsPolish = GetOperandsPolish(rightOperand, ref operandIndex, operation);

            int leftCount = leftOperandsPolish.Count;
            int rightCount = rightOperandsPolish.Count;

            List<List<Token>> result = new List<List<Token>>();
            if (leftCount > rightCount)
            {
                int notIndex = 1;
                int matchesCount = 0;
                List<List<Token>> operandsToRemove = new List<List<Token>>();
                foreach (List<Token> operand in leftOperandsPolish)
                {
                    if (rightOperandsPolish.Any(n => n.SequenceEqual(operand)))
                    {
                        matchesCount++;
                    }
                    else if (operand.Count > notIndex && operand[notIndex].Equals(not))
                    {
                        List<Token> temp = operand.GetRange(0, operand.Count);
                        temp.RemoveAt(notIndex);

                        if (rightOperandsPolish.Any(n => n.SequenceEqual(temp)))
                            operandsToRemove.Add(operand);
                    }
                    else
                    {
                        List<Token> temp = operand.GetRange(0, operand.Count);
                        temp.Add(not);

                        if (rightOperandsPolish.Any(n => n.SequenceEqual(temp)))
                            operandsToRemove.Add(operand);
                    }
                }

                foreach (List<Token> operand in operandsToRemove)
                    leftOperandsPolish.Remove(operand);

                if (matchesCount != 0 && matchesCount == leftCount - 1)
                {
                    List<List<Token>> rightOperands = new List<List<Token>>();
                    foreach (List<Token> operand in rightOperandsPolish)
                        rightOperands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());

                    result.Add(new List<Token>());
                    result.Add(AddSeparators(rightOperands, isAnd ? and : or));
                }
                else
                {
                    List<List<Token>> leftOperands = new List<List<Token>>();
                    foreach (List<Token> operand in leftOperandsPolish)
                        leftOperands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());
                    List<List<Token>> rightOperands = new List<List<Token>>();
                    foreach (List<Token> operand in rightOperandsPolish)
                        rightOperands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());

                    result.Add(AddSeparators(leftOperands, isAnd ? and : or));
                    result.Add(AddSeparators(rightOperands, isAnd ? and : or));
                }
            }
            else
            {
                int notIndex = 1;
                int matchesCount = 0;
                List<List<Token>> operandsToRemove = new List<List<Token>>();
                foreach (List<Token> operand in rightOperandsPolish)
                {
                    if (leftOperandsPolish.Any(n => n.SequenceEqual(operand)))
                    {
                        matchesCount++;
                    }
                    else if (operand.Count > notIndex && operand[notIndex].Equals(not))
                    {
                        List<Token> temp = operand.GetRange(0, operand.Count);
                        temp.RemoveAt(notIndex);

                        if (leftOperandsPolish.Any(n => n.SequenceEqual(temp)))
                            operandsToRemove.Add(operand);
                    }
                    else if (operand.Count == notIndex)
                    {
                        List<Token> temp = operand.GetRange(0, operand.Count);
                        temp.Add(not);

                        if (leftOperandsPolish.Any(n => n.SequenceEqual(temp)))
                            operandsToRemove.Add(operand);
                    }
                }

                foreach (List<Token> operand in operandsToRemove)
                    rightOperandsPolish.Remove(operand);

                if (matchesCount != 0 && matchesCount == rightCount - 1)
                {
                    List<List<Token>> leftOperands = new List<List<Token>>();
                    foreach (List<Token> operand in leftOperandsPolish)
                        leftOperands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());

                    result.Add(AddSeparators(leftOperands, isAnd ? and : or));
                    result.Add(new List<Token>());
                }
                else
                {
                    List<List<Token>> leftOperands = new List<List<Token>>();
                    foreach (List<Token> operand in leftOperandsPolish)
                        leftOperands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());
                    List<List<Token>> rightOperands = new List<List<Token>>();
                    foreach (List<Token> operand in rightOperandsPolish)
                        rightOperands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());

                    result.Add(AddSeparators(leftOperands, isAnd ? and : or));
                    result.Add(AddSeparators(rightOperands, isAnd ? and : or));
                }
            }

            return result;
        }

        private static List<Token> AddSeparators(List<List<Token>> operands, Token separator)
        {
            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);

            bool isAnd = separator.Equals(and);

            List<Token> result = new List<Token>();
            foreach (List<Token> operand in operands)
            {
                result.AddRange(LogicalExpressionSyntaxAnalyzer.IsNeedBraces(operand, separator) 
                    ? LogicalExpressionSyntaxAnalyzer.EncloseInBraces(operand) : operand);
                result.Add(separator);
            }
            result.RemoveAt(result.Count - 1);

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

        private static List<Token> RemoveConstants(List<Token> tokens, ref bool isChanged)
        {
            isChanged = false;

            Token and = new Token(Operation.And, TokenType.Operation);
            Token or = new Token(Operation.Or, TokenType.Operation);
            Token zero = new Token(Operand.Zero, TokenType.Const);
            Token one = new Token(Operand.One, TokenType.Const);

            List<Token> result = tokens.GetRange(0, tokens.Count);
            List<Token> polishNotation = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(result);
            for (int i = 0; i < polishNotation.Count; i++)
            {
                bool isAnd;
                if ((isAnd = polishNotation[i].Equals(and)) || polishNotation[i].Equals(or))
                {
                    int index = i;
                    List<List<Token>> operandsPolish = GetOperandsPolish(polishNotation, ref index, isAnd ? and : or);

                    int expressionLength = operandsPolish.Count - 1;
                    foreach (List<Token> operand in operandsPolish)
                        expressionLength += operand.Count;

                    index++;
                    for (int j = 0; j < operandsPolish.Count; j++)
                    {
                        for (int k = 0; k < j; k++)
                        {
                            List<Token> leftOperand = operandsPolish[k].GetRange(0, operandsPolish[k].Count);
                            List<Token> rightOperand = operandsPolish[j].GetRange(0, operandsPolish[j].Count);

                            int constIndex = 0;
                            if (leftOperand.Count == 1 & leftOperand[constIndex].Type == TokenType.Const)
                            {
                                if (isAnd)
                                {
                                    if (leftOperand[constIndex].Equals(zero))
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        polishNotation.Insert(index, zero);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
                                    }
                                    else
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        operandsPolish.RemoveAt(k);
                                        List<List<Token>> operands = new List<List<Token>>();
                                        foreach (List<Token> operand in operandsPolish)
                                            operands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());
                                        List<Token> newTokens = AddSeparators(operands, isAnd ? and : or);
                                        List<Token> newPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(newTokens);
                                        polishNotation.InsertRange(index, newPolish);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
                                    }
                                }
                                else
                                {
                                    if (leftOperand[constIndex].Equals(zero))
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        operandsPolish.RemoveAt(k);
                                        List<List<Token>> operands = new List<List<Token>>();
                                        foreach (List<Token> operand in operandsPolish)
                                            operands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());
                                        List<Token> newTokens = AddSeparators(operands, isAnd ? and : or);
                                        List<Token> newPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(newTokens);
                                        polishNotation.InsertRange(index, newPolish);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
                                    }
                                    else
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        polishNotation.Insert(index, one);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
                                    }
                                }

                                isChanged = true;
                                return result;
                            }
                            else if (rightOperand.Count == 1 & rightOperand[constIndex].Type == TokenType.Const)
                            {
                                if (isAnd)
                                {
                                    if (rightOperand[constIndex].Equals(zero))
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        polishNotation.Insert(index, zero);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
                                    }
                                    else
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        operandsPolish.RemoveAt(j);
                                        List<List<Token>> operands = new List<List<Token>>();
                                        foreach (List<Token> operand in operandsPolish)
                                            operands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());
                                        List<Token> newTokens = AddSeparators(operands, isAnd ? and : or);
                                        List<Token> newPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(newTokens);
                                        polishNotation.InsertRange(index, newPolish);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
                                    }
                                }
                                else
                                {
                                    if (rightOperand[constIndex].Equals(zero))
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        operandsPolish.RemoveAt(j);
                                        List<List<Token>> operands = new List<List<Token>>();
                                        foreach (List<Token> operand in operandsPolish)
                                            operands.Add(LogicalExpressionSyntaxAnalyzer.GetAllExpressions(operand).Last());
                                        List<Token> newTokens = AddSeparators(operands, isAnd ? and : or);
                                        List<Token> newPolish = LogicalExpressionSyntaxAnalyzer.GetPolishNotation(newTokens);
                                        polishNotation.InsertRange(index, newPolish);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
                                    }
                                    else
                                    {
                                        polishNotation.RemoveRange(index, expressionLength);
                                        polishNotation.Insert(index, one);
                                        result = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation).Last();
                                    }
                                }

                                isChanged = true;
                                return result;
                            }
                        }
                    }
                }
            }
            
            return result;
        }

        private static List<List<Token>> GetOperandsPolish(List<Token> polishNotation, ref int index, Token operation)
        {
            List<List<Token>> operandsPolish = new List<List<Token>>();
            if (polishNotation.Count <= 2)
            {
                operandsPolish.Add(polishNotation);

                return operandsPolish;
            }

            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            int operandsCount = GetOperandsCountToGet(polishNotation, index, operation);

            int operandIndex = index;
            for (int i = 0; i < operandsCount; i++)
            {
                if (polishNotation[operandIndex].Equals(operation))
                    operandIndex--;

                List<Token> operandPolish = GetOperandPolish(polishNotation, ref operandIndex);
                operandsPolish.Add(operandPolish);
            }

            index = operandIndex;
            operandsPolish.Reverse();
            return operandsPolish;
        }

        private static List<Token> GetOperandPolish(List<Token> polishNotation, ref int index)
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

            while (tokensToGet != 0 && index >= 0)
            {
                if (polishNotation[index].Type == TokenType.Operation)
                {
                    if (operandsCount.Any(n => n.Key == polishNotation[index].Identifier
                        && n.Value == 2))
                    {
                        if (polishNotation[index].Equals(operation))
                        {
                            count++;
                            tokensToGet++;

                            index--;
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

                            tokensToGet--;
                        }
                    }
                    else if (operandsCount.Any(n => n.Key == polishNotation[index].Identifier
                        && n.Value == 1))
                    {
                        index--;
                    }
                }
                else
                {
                    tokensToGet--;

                    index--;
                }
            }

            return count;
        }

        private static bool AreOperandsEqual(List<Token> leftOperandPolish, List<Token> rightOperandPolish)
        {
            int length = leftOperandPolish.Count;
            if (length != rightOperandPolish.Count)
                return false;

            for (int i = 0; i < length; i++)
                if (!leftOperandPolish[i].Equals(rightOperandPolish[i]))
                    return false;

            return true;
        }
    }
}
