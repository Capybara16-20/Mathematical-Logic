using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwoDimensionalArray;

namespace MathematicalLogicProcessor
{
    public class TruthTable
    {
        private const string functionIdentifier = "Function";
        private const int binaryBase = 2;
        private readonly List<Operand> variables;
        private readonly List<List<Token>> headers;
        private readonly bool[] functionVector;
        private readonly int functionNumber;
        private readonly bool[,] table;

        public List<Operand> Variables { get { return variables; } }
        public List<List<Token>> Headers { get { return headers; } }
        public bool[] FunctionVector { get { return functionVector; } }
        public int FunctionNumber { get { return functionNumber; } }

        public TruthTable(List<Operand> variables, List<Token> polishNotation)
        {
            this.variables = variables;
            headers = GetHeaders(polishNotation, variables);
            table = GetTruthTable(variables, polishNotation);
            functionVector = GetFunctionVector(table);
            functionNumber = GetFunctionNumber(functionVector);
        }

        public TruthTable(bool[] functionVector)
        {
            if (CheckVectorLength(functionVector.Length))
            {
                this.functionVector = functionVector;
                functionNumber = GetFunctionNumber(functionVector);
                int variablesCount = GetVariablesCount(functionVector.Length);
                variables = GetVariables(variablesCount);
                headers = GetHeaders(variables);
                table = GetTruthTable(variables, functionVector);
            }
            else
            {
                throw new ArgumentException(nameof(functionVector));
            }
        }

        public TruthTable(int functionNumber)
        {
            this.functionNumber = functionNumber;
            functionVector = GetFunctionVector(functionNumber);
            int variablesCount = GetVariablesCount(functionVector.Length);
            variables = GetVariables(variablesCount);
            headers = GetHeaders(variables);
            table = GetTruthTable(variables, functionVector);
        }

        private bool CheckVectorLength(int length)
        {
            if (length == 2)
                return true;
            else if (length % 2 == 0)
                return CheckVectorLength(length / 2);
            else
                return false;
        }

        private bool[] GetFunctionVector(int functionNumber)
        {
            double maxNumber;
            int variablesCount = 0;
            double length;
            do
            {
                length = Math.Pow(binaryBase, variablesCount);
                maxNumber = Math.Pow(binaryBase, length);
                variablesCount++;
            }
            while (length % 1 != 0 || maxNumber <= functionNumber);
            int vectorLength = (int)length;

            string vector = Convert.ToString(functionNumber, binaryBase);
            vector = vector.PadLeft(vectorLength, '0');
            bool[] functionVector = new bool[vectorLength];
            for (int i = 0; i < vectorLength; i++)
                functionVector[i] = vector[i] == '1';

            Array.Reverse(functionVector);

            return functionVector;
        }

        private bool[] GetFunctionVector(bool[,] table)
        {
            int lastColumnIndex = table.GetColumnsCount() - 1;
            int rowsCount = table.GetRowsCount();
            bool[] functionVector = new bool[rowsCount];
            for (int i = 0; i < rowsCount; i++)
                functionVector[i] = table[i, lastColumnIndex];

            return functionVector;
        }

        private int GetFunctionNumber(bool[] functionVector)
        {
            int length = functionVector.Length;
            bool[] reversedVector = new bool[length];
            Array.Copy(functionVector, reversedVector, length);
            Array.Reverse(reversedVector);

            StringBuilder sb = new StringBuilder();
            foreach (bool value in reversedVector)
            {
                sb.Append(value ? 1.ToString() : 0.ToString());
            }

            return Convert.ToInt32(sb.ToString(), binaryBase);
        }

        private int GetVariablesCount(int vectorLength)
        {
            return (int)Math.Log(vectorLength, binaryBase);
        }

        private List<Operand> GetVariables(int variablesCount)
        {
            const int alphabetStartCode = 65;

            List<Operand> variables = new List<Operand>();
            int code = alphabetStartCode;
            for (int i = 0; i < variablesCount; i++)
            {
                variables.Add(new Operand(((char)code).ToString(), TokenType.Variable));
                code++;
            }

            return variables;
        }

        private List<List<Token>> GetHeaders(List<Operand> variables)
        {
            List<List<Token>> headers = new List<List<Token>>();
            foreach (Operand variable in variables)
                headers.Add(new List<Token> { variable });

            headers.Add(new List<Token> { new Token(functionIdentifier, TokenType.Variable) });
            return headers;
        }

        private List<List<Token>> GetHeaders(List<Token> polishNotation, List<Operand> variables)
        {
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            List<List<Token>> headers = new List<List<Token>>();
            foreach (Operand variable in variables)
                headers.Add(new List<Token> { variable });

            List<List<Token>> expressions;
            if (polishNotation.Count > 1)
            {
                expressions = LogicalExpressionSyntaxAnalyzer.GetAllExpressions(polishNotation);
                headers.AddRange(expressions);
            }

            return headers;
        }

        private bool[,] GetTruthTable(List<Operand> variables, bool[] functionVector)
        {
            int linesCount = functionVector.Length;
            bool[,] table = new bool[linesCount, variables.Count + 1];
            FillTruthTable(table, linesCount);

            int lastColumnIndex = variables.Count;
            for (int i = 0; i < linesCount; i++)
                table[i, lastColumnIndex] = functionVector[i];

            return table;
        }

        private bool[,] GetTruthTable(List<Operand> variables, List<Token> polishNotation)
        {
            Dictionary<string, Delegate> operations = Operation.Operations;
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            int linesCount = GetLinesCount(variables.Count);
            bool[,] table = new bool[linesCount, headers.Count];
            FillTruthTable(table, linesCount);

            for (int i = 0; i < linesCount; i++)
            {
                int index = variables.Count;
                Stack<Operand> stack = new Stack<Operand>();
                for (int j = 0; j < polishNotation.Count; j++)
                {
                    if (polishNotation[j].Type == TokenType.Variable)
                    {
                        Operand operand = new Operand(polishNotation[j]);
                        int operandIndex = variables.IndexOf(operand);
                        operand.Value = table[i, operandIndex];
                        stack.Push(operand);
                    }
                    else if (polishNotation[j].Type == TokenType.Const)
                    {
                        Operand operand = new Operand(polishNotation[j]);
                        stack.Push(operand);
                    }
                    else
                    {
                        if (operationOperandsCount.Any(n => n.Key == polishNotation[j].Identifier
                            && n.Value == 1))
                        {
                            Operand operand = stack.Pop();
                            Delegate Calculate = operations[polishNotation[j].Identifier];
                            bool result = (bool)Calculate.DynamicInvoke(operand.Value);

                            table[i, index] = result;

                            Operand newOperand = new Operand("temp", TokenType.Variable, result);
                            stack.Push(newOperand);

                            index++;
                        }
                        else
                        {
                            Operand operand2 = stack.Pop();
                            Operand operand1 = stack.Pop();
                            Delegate Calculate = operations[polishNotation[j].Identifier];
                            bool result = (bool)Calculate.DynamicInvoke(operand1.Value, operand2.Value);

                            table[i, index] = result;

                            Operand newOperand = new Operand("temp", TokenType.Variable, result);
                            stack.Push(newOperand);

                            index++;
                        }
                    }
                }
            }

            return table;
        }

        private void FillTruthTable(bool[,] table, int linesCount)
        {
            for (int i = 0; i < linesCount; i++)
            {
                string line = Convert.ToString(i, binaryBase).PadLeft(variables.Count, '0');
                for (int j = 0; j < variables.Count; j++)
                {
                    table[i, j] = line[j] == '1';
                }
            }
        }

        private int GetLinesCount(int variablesCount)
        {
            return (int)Math.Pow(binaryBase, variablesCount);
        }

        public bool this[int i, int j]
        {
            get 
            {
                if (i >= table.GetRowsCount())
                    throw new IndexOutOfRangeException(nameof(i));

                if (j >= table.GetColumnsCount())
                    throw new IndexOutOfRangeException(nameof(j) + " " + j);

                return table[i, j]; 
            }
        }

        public int GetRowsCount()
        {
            return table.GetRowsCount();
        }

        public int GetColumnsCount()
        {
            return table.GetColumnsCount();
        }
    }
}
