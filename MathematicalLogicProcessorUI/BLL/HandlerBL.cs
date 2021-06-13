using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MathematicalLogicProcessor;

namespace MathematicalLogicProcessorUI.BLL
{
    public class HandlerBL
    {
        const string functionVectorPattern = "^[01]+$";

        private TruthTable truthTable;
        private List<Token> pcnf;
        private List<string> pcnfDecision;
        private List<Token> pdnf;
        private List<string> pdnfDecision;
        private Dictionary<List<List<Token>>, string> cnf;
        private Dictionary<List<List<Token>>, string> dnf;
        private ZhegalkinPolynomial polynomial;
        private PostClassification classification;

        public List<string> TruthTableHeaders { get { return GetListOfString(truthTable.Headers); } }
        public List<List<string>> TruthTable { get { return GetTable(truthTable); } }
        public string PCNF { get { return GetExpressionString(pcnf); } }
        public List<string> PCNFDecision { get { return pcnfDecision; } }
        public string PDNF { get { return GetExpressionString(pdnf); } }
        public List<string> PDNFDecision { get { return pdnfDecision; } }
        public string CNF { get { return GetExpressionString(cnf); } }
        public Dictionary<List<string>, string> CNFDecision { get { return GetDecision(cnf); } }
        public string DNF { get { return GetExpressionString(dnf); } }
        public Dictionary<List<string>, string> DNFDecision { get { return GetDecision(dnf); } }
        public string Polynomial { get { return polynomial.Polinomial; } }
        public List<List<string>> UndefinedCoefficientMethodTable { get { return polynomial.UndefinedCoefficientMethodTable; } }
        public List<List<string>> TriangleMethodTable { get { return polynomial.TriangleMethodTable; } }
        public List<List<string>> FFTMethodTable { get { return polynomial.FFTMethodTable; } }
        public List<string> PostClassificationHeaders { get { return classification.Headers; } }
        public List<string> PostClassification { get { return classification.Classification; } }
        public List<List<string>> PostClassificationDecision { get { return classification.Decision; } }

        public HandlerBL(string expression)
        {
            MathematicalLogicHandler handler;
            bool isExpression;

            if (!IsExpression(expression))
            {
                if (IsFunctionVector(expression))
                {
                    bool[] functionVector = new bool[expression.Length];
                    for (int i = 0; i < expression.Length; i++)
                        functionVector[i] = expression[i].ToString() == Operand.One;

                    handler = new MathematicalLogicHandler(functionVector);
                    isExpression = false;
                }
                else
                {
                    int functionNumber = int.Parse(expression);

                    handler = new MathematicalLogicHandler(functionNumber);
                    isExpression = false;
                }
            }
            else
            {
                try
                {
                    handler = new MathematicalLogicHandler(expression);
                    isExpression = true;
                }
                catch
                {
                    throw new ArgumentException(nameof(expression));
                }
            }

            truthTable = handler.TruthTable;

            pcnf = handler.GetPCNF(out pcnfDecision);
            pdnf = handler.GetPDNF(out pdnfDecision);

            List<Token> tokens; 
            if (isExpression)
                tokens = LogicalExpressionSyntaxAnalyzer.GetTokens(expression);
            else
                tokens = pdnf;

            cnf = handler.GetCNF(tokens);
            dnf = handler.GetDNF(tokens);

            polynomial = handler.ZhegalkinPolynomial;
            classification = handler.PostClassification;
        }

        private string GetExpressionString(List<Token> tokens)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Token token in tokens)
                sb.Append(token.Identifier);

            return sb.ToString();
        }

        private string GetExpressionString(Dictionary<List<List<Token>>, string> dictionary)
        {
            if (dictionary.Count == 0)
                return string.Empty;

            List<List<Token>> decision = dictionary.Keys.Last();
            List<Token> tokens = decision.Last();

            return (GetExpressionString(tokens));
        }

        private List<string> GetListOfString(List<List<Token>> headers)
        {
            List<string> headersString = new List<string>();
            foreach (List<Token> tokens in headers)
                headersString.Add(GetExpressionString(tokens));

            return headersString;
        }
        private List<List<string>> GetTable(TruthTable truthTable)
        {
            int linesCount = truthTable.GetRowsCount();
            int columnsCount = truthTable.GetColumnsCount();
            List<List<string>> newTruthTable = new List<List<string>>();
            for (int i = 0; i < linesCount; i++)
            {
                newTruthTable.Add(new List<string>());
                for (int j = 0; j < columnsCount; j++)
                    newTruthTable[i].Add(truthTable[i, j] ? Operand.One : Operand.Zero);
            }

            return newTruthTable;
        }

        private Dictionary<List<string>, string> GetDecision(Dictionary<List<List<Token>>, string> normalForm)
        {
            Dictionary<List<string>, string> decision = new Dictionary<List<string>, string>();
            foreach (List<List<Token>> expressions in normalForm.Keys)
            {
                List<string> newExpressions = GetListOfString(expressions);
                string message = normalForm[expressions];

                decision.Add(newExpressions, message);
            }

            return decision;
        }

        private bool IsExpression(string expression)
        {
            return !int.TryParse(expression, out _);
        }

        private bool IsFunctionVector(string expression)
        {
            if (!Regex.IsMatch(expression, functionVectorPattern))
                return false;

            int length = expression.Length;
            return IsDevided(length);
        }

        private static bool IsDevided(int number)
        {
            if (number == 2) 
                return true;
            else if (number % 2 == 0) 
                return IsDevided(number / 2);
            else 
                return false;
        }
    }
}
