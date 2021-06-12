using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathematicalLogicProcessor
{
    public class ZhegalkinPolynomial
    {
        private readonly List<List<string>> fFTMethodTable;
        private readonly List<List<string>> triangleMethodTable;
        private readonly List<List<string>> undefinedCoefficientMethodTable;
        private string polinomial;

        public List<List<string>> FFTMethodTable { get { return fFTMethodTable; } }
        public List<List<string>> TriangleMethodTable { get { return triangleMethodTable; } }
        public List<List<string>> UndefinedCoefficientMethodTable { get { return undefinedCoefficientMethodTable; } }
        public string Polinomial { get { return polinomial; } }

        public ZhegalkinPolynomial(List<Operand> variables, TruthTable truthTable)
        {
            fFTMethodTable = GetFFTMethodTable(variables, truthTable);
            triangleMethodTable = GetTriangleMethodTable(variables, truthTable);
            undefinedCoefficientMethodTable = GetUndefinedCoefficientMethodTable(variables, truthTable);
        }

        public List<List<string>> GetFFTMethodTable(List<Operand> variables, TruthTable truthTable)
        {
            const string arrow = "\u2192";

            int linesCount = truthTable.GetRowsCount();
            int variablesCount = variables.Count;

            List<List<string>> fFTMethodTable = new List<List<string>>();
            for (int i = 0; i < linesCount; i++)
            {
                fFTMethodTable.Add(new List<string>());
                for (int j = 0; j < variablesCount; j++)
                    fFTMethodTable[i].Add(truthTable[i, j] ? Operand.One : Operand.Zero);
            }

            bool[] functionVector = truthTable.FunctionVector;
            for (int i = 0; i < linesCount; i++)
                fFTMethodTable[i].Add(functionVector[i] ? Operand.One : Operand.Zero);

            bool[,] tempTable = new bool[linesCount, variablesCount];
            int xorShift = 1;
            bool isXor = false;
            int count = 0;
            int index = 0;
            while (xorShift != linesCount)
            {
                for (int i = 0; i < linesCount; i++)
                {
                    if (isXor)
                    {
                        string xor;
                        if (index == 0)
                        {
                            tempTable[i, index] = Operation.XOR(functionVector[i], functionVector[i - xorShift]);
                            xor = Operation.Xor + (functionVector[i - xorShift] ? Operand.One : Operand.Zero);
                        }
                        else
                        {
                            tempTable[i, index] = Operation.XOR(tempTable[i, index - 1], tempTable[i - xorShift, index - 1]);
                            xor = Operation.Xor + (tempTable[i - xorShift, index - 1] ? Operand.One : Operand.Zero);
                        }

                        fFTMethodTable[i].Add(xor);
                        fFTMethodTable[i].Add(tempTable[i, index] ? Operand.One : Operand.Zero);
                    }
                    else
                    {
                        tempTable[i, index] = index == 0 ? functionVector[i] : tempTable[i, index - 1];

                        fFTMethodTable[i].Add(arrow);
                        fFTMethodTable[i].Add(tempTable[i, index] ? Operand.One : Operand.Zero);
                    }
                    count++;

                    if (count == xorShift)
                    {
                        count = 0;
                        isXor = !isXor;
                    }
                }

                index++;
                xorShift *= 2;
            }

            bool[] polinomialVector = new bool[linesCount];
            for (int i = 0; i < linesCount; i++)
                polinomialVector[i] = fFTMethodTable[i].Last() == Operand.One;

            List<string> coefficients = GetCoefficients(truthTable);
            for (int i = 0; i < linesCount; i++)
                fFTMethodTable[i].Add(coefficients[i]);

            List<string> polinomialCoefs = GetPolinomialCoefficients(truthTable, polinomialVector);
            polinomial = GetPolinomial(polinomialCoefs);

            List<string> headers = GetTableHeaders(variables, truthTable);
            fFTMethodTable.Insert(0, headers);

            return fFTMethodTable;
        }

        private List<string> GetTableHeaders(List<Operand> variables, TruthTable truthTable)
        {
            List<string> headers = new List<string>();
            foreach (Operand operand in variables)
                headers.Add(operand.Identifier);

            StringBuilder sb = new StringBuilder();
            foreach (Token token in truthTable.Headers.Last())
                sb.Append(token.Identifier);

            headers.Add(sb.ToString());
            return headers;
        }

        public List<List<string>> GetTriangleMethodTable(List<Operand> variables, TruthTable truthTable)
        {
            int linesCount = truthTable.GetRowsCount();
            int variablesCount = variables.Count;

            List<List<string>> triangleMethodTable = new List<List<string>>();

            bool[,] tempTable = new bool[linesCount, linesCount];
            bool[] functionVector = truthTable.FunctionVector;
            for (int i = 0; i < linesCount; i++)
            {
                triangleMethodTable.Add(new List<string> { functionVector[i] ? Operand.One : Operand.Zero });
                tempTable[i, 0] = functionVector[i];
            }

            int index = 1;
            int shift = 1;
            while (index < linesCount)
            {
                for (int i = 0; i < linesCount - shift; i++)
                {
                    bool value = Operation.XOR(tempTable[i, index - 1], tempTable[i + 1, index - 1]);
                    tempTable[i, index] = value;

                    triangleMethodTable[i].Add(value ? Operand.One : Operand.Zero);
                }

                shift++;
                index++;
            }

            List<string> values = new List<string>();
            for (int i = 0; i < linesCount; i++)
            {
                StringBuilder value = new StringBuilder();
                for (int j = 0; j < variablesCount; j++)
                    value.Append(truthTable[i, j] ? Operand.One : Operand.Zero);

                values.Add(value.ToString());
            }
            triangleMethodTable.Insert(0, values);

            List<string> coefficients = GetCoefficients(truthTable);
            triangleMethodTable.Insert(0, coefficients);

            return triangleMethodTable;
        }

        public List<List<string>> GetUndefinedCoefficientMethodTable(List<Operand> variables, TruthTable truthTable)
        {
            const string operandSign = "f";
            const string arrow = "\u2192";
            const string equally = "=";

            int linesCount = truthTable.GetRowsCount();
            int variablesCount = variables.Count;
            bool[] functionVector = truthTable.FunctionVector;

            List<List<string>> undefinedCoefficientMethodTable = new List<List<string>>();
            for (int i = 0; i < linesCount; i++)
            {
                undefinedCoefficientMethodTable.Add(new List<string>());
                for (int j = 0; j < variablesCount; j++)
                    undefinedCoefficientMethodTable[i].Add(truthTable[i, j] ? Operand.One : Operand.Zero);
            }

            for (int i = 0; i < linesCount; i++)
                undefinedCoefficientMethodTable[i].Add(functionVector[i] ? Operand.One : Operand.Zero);

            List<Operand> operands = new List<Operand>();

            for (int i = 0; i < linesCount; i++)
            {
                undefinedCoefficientMethodTable[i].Add(equally);

                List<Operand> lineOperands = GetLineOperands(truthTable, i, operandSign);
                operands.Add(lineOperands.Last());

                if (i == 0)
                {
                    operands[i].Value = functionVector[i];
                }
                else
                {
                    int index = 0;
                    bool value = functionVector[i];
                    while (index < i)
                    {
                        value = Operation.XOR(value, operands[index].Value);
                        index++;
                    }

                    operands[i].Value = value;
                }

                for (int j = 0; j < lineOperands.Count; j++)
                {
                    undefinedCoefficientMethodTable[i].Add(lineOperands[j].Identifier);
                    undefinedCoefficientMethodTable[i].Add(Operation.Xor);
                }
                int lastIndex = undefinedCoefficientMethodTable[i].LastIndexOf(Operation.Xor);
                undefinedCoefficientMethodTable[i].RemoveAt(lastIndex);

                if (i != 0)
                {
                    undefinedCoefficientMethodTable[i].Add(equally);

                    for (int j = 0; j < lineOperands.Count; j++)
                    {
                        for (int k = 0; k < operands.Count - 1; k++)
                        {
                            if (lineOperands[j].Identifier == operands[k].Identifier)
                            {
                                undefinedCoefficientMethodTable[i].Add(operands[k].Value ? Operand.One : Operand.Zero);
                                undefinedCoefficientMethodTable[i].Add(Operation.Xor);
                            }
                        }
                    }

                    undefinedCoefficientMethodTable[i].Add(operands.Last().Identifier);
                }

                undefinedCoefficientMethodTable[i].Add(arrow);

                undefinedCoefficientMethodTable[i].Add(operands.Last().Identifier);
                undefinedCoefficientMethodTable[i].Add(equally);
                undefinedCoefficientMethodTable[i].Add(operands.Last().Value ? Operand.One : Operand.Zero);
            }

            List<string> headers = GetTableHeaders(variables, truthTable);
            undefinedCoefficientMethodTable.Insert(0, headers);

            return undefinedCoefficientMethodTable;
        }

        private List<string> GetCoefficients(TruthTable truthTable)
        {
            int linesCount = truthTable.GetRowsCount();
            List<Operand> variables = truthTable.Variables;
            int variablesCount = variables.Count();

            List<string> coefficients = new List<string>();
            for (int i = 0; i < linesCount; i++)
            {
                StringBuilder coefficient = new StringBuilder();
                for (int j = 0; j < variablesCount; j++)
                    if (truthTable[i, j])
                        coefficient.Append(variables[j].Identifier);

                coefficients.Add(coefficient.Length == 0 ? Operand.One : coefficient.ToString());
            }

            return coefficients;
        }

        private List<string> GetPolinomialCoefficients(TruthTable truthTable, bool[] polinomialVector)
        {
            List<List<Token>> headers = truthTable.Headers;
            int linesCount = polinomialVector.Length;
            int variablesCount = truthTable.Variables.Count;

            List<string> coefficients = new List<string>();
            for (int i = 0; i < linesCount; i++)
            {
                if (polinomialVector[i])
                {
                    StringBuilder coefficient = new StringBuilder();
                    for (int j = 0; j < variablesCount; j++)
                        if (truthTable[i, j])
                            foreach (Token token in headers[j])
                                coefficient.Append(token.Identifier);

                    coefficients.Add(coefficient.Length == 0 ? Operand.One : coefficient.ToString());
                }
            }

            return coefficients;
        }

        private string GetPolinomial(List<string> coefficients)
        {
            StringBuilder polinomial = new StringBuilder();
            foreach (string coefficient in coefficients)
            {
                polinomial.Append(coefficient);
                polinomial.Append(Operation.Xor);
            }
            polinomial.Remove(polinomial.Length - 1, 1);

            return polinomial.ToString();
        }

        private List<Operand> GetLineOperands(TruthTable truthTable, int lineIndex, string operandSign)
        {
            List<Operand> variables = truthTable.Variables;
            int linesCount = truthTable.GetRowsCount();
            int variablesCount = variables.Count;

            int lineMatches = 0;
            for (int i = 0; i < variablesCount; i++)
                if (truthTable[lineIndex, i])
                    lineMatches++;

            List<Operand> operands = new List<Operand>();
            int index = 0;
            while (index < lineIndex)
            {
                int matches = 0;
                for (int i = 0; i < variablesCount; i++)
                    if (truthTable[index, i])
                        matches++;

                if (matches < lineMatches)
                {
                    StringBuilder number = new StringBuilder();
                    for (int i = 0; i < variablesCount; i++)
                        number.Append(truthTable[index, i] ? Operand.One : Operand.Zero);
                    
                    operands.Add(new Operand(operandSign + number, TokenType.Variable));
                }

                index++;
            }

            StringBuilder operandNumber = new StringBuilder();
            for (int i = 0; i < variablesCount; i++)
                operandNumber.Append(truthTable[lineIndex, i] ? Operand.One : Operand.Zero);

            operands.Add(new Operand(operandSign + operandNumber, TokenType.Variable));

            return operands;
        }
    }
}
