using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalLogicProcessor
{
    public class PostClassification
    {
        private const string classT0 = "T0";
        private const string classT1 = "T1";
        private const string classL = "L";
        private const string classM = "M";
        private const string classS = "S";

        private const string affiliation = "пренадлежит";
        private const string nonAffiliation = "не пренадлежит";

        private const string affiliationFormat = "Функция {0} классу {1}";

        private readonly List<string> classification;
        private readonly List<List<string>> decision;
        private readonly List<string> headers = new List<string> { classT0, classT1, classL, classM, classS };

        public List<string> Classification { get { return classification; } }
        public List<string> Headers { get { return headers; } }
        public List<List<string>> Decision { get { return decision; } }

        public PostClassification(TruthTable truthTable, ZhegalkinPolynomial polynomial)
        {
            List<List<string>> decision;
            classification = GetClassification(truthTable, polynomial, out decision);
            this.decision = decision;
        }

        private List<string> GetClassification(TruthTable truthTable, ZhegalkinPolynomial polynomial, 
            out List<List<string>> decision)
        {
            const string plus = "+";
            const string minus = "-";

            decision = new List<List<string>>();
            List<string> classification = new List<string>();

            List<string> decisionT0;
            classification.Add(isBelongsToClassT0(truthTable, out decisionT0) ? plus : minus);
            decision.Add(decisionT0);

            List<string> decisionT1;
            classification.Add(isBelongsToClassT1(truthTable, out decisionT1) ? plus : minus);
            decision.Add(decisionT1);

            List<string> decisionL;
            classification.Add(isBelongsToClassL(polynomial, out decisionL) ? plus : minus);
            decision.Add(decisionL);

            List<string> decisionM;
            classification.Add(isBelongsToClassM(truthTable, out decisionM) ? plus : minus);
            decision.Add(decisionM);

            List<string> decisionS;
            classification.Add(isBelongsToClassS(truthTable, out decisionS) ? plus : minus);
            decision.Add(decisionS);

            return classification;
        }

        private bool isBelongsToClassT0(TruthTable truthTable, out List<string> decisionT0)
        {
            const string requirementFormat = ", если на нулевом наборе она принимает значение 0.";
            const string conditionFormat = "На нулевом наборе значение функции равно {0}.";

            string requirementPart = string.Format(affiliationFormat, affiliation, classT0 + "{0}");
            string requirement = string.Format(requirementPart, requirementFormat);

            decisionT0 = new List<string>();
            decisionT0.Add(requirement);

            int columnIndex = truthTable.GetColumnsCount() - 1;
            int rowIndex = 0;
            bool value = truthTable[rowIndex, columnIndex];

            string condition = string.Format(conditionFormat, value ? "1" : "0");
            decisionT0.Add(condition);

            string conclusion = string.Format(affiliationFormat, !value ? affiliation : nonAffiliation, classT0 + ".");
            decisionT0.Add(conclusion);

            return !value;
        }

        private bool isBelongsToClassT1(TruthTable truthTable, out List<string> decisionT1)
        {
            const string requirementMessage = ", если на единичном наборе она принимает значение 1.";
            const string conditionFormat = "На единичном наборе значение функции равно {0}.";

            string requirementPart = string.Format(affiliationFormat, affiliation, classT1 + "{0}");
            string requirement = string.Format(requirementPart, requirementMessage);

            decisionT1 = new List<string>();
            decisionT1.Add(requirement);

            int columnIndex = truthTable.GetColumnsCount() - 1;
            int rowIndex = truthTable.GetRowsCount() - 1;
            bool value = truthTable[rowIndex, columnIndex];

            string condition = string.Format(conditionFormat, value ? "1" : "0");
            decisionT1.Add(condition);

            string conclusion = string.Format(affiliationFormat, value ? affiliation : nonAffiliation, classT1 + ".");
            decisionT1.Add(conclusion);

            return value;
        }

        private bool isBelongsToClassL(ZhegalkinPolynomial polynomial, out List<string> decisionL)
        {
            const string requirementMessage = ", если её полином Жегалкина не содержит произведений.";
            const string contains = "содержит произведения";
            const string notContains = "не содержит произведений";
            const string conditionFormat = "Полином Жегалкина {0} {1}.";

            string requirementPart = string.Format(affiliationFormat, affiliation, classL + "{0}");
            string requirement = string.Format(requirementPart, requirementMessage);

            decisionL = new List<string>();
            decisionL.Add(requirement);

            string polinomialString = polynomial.Polinomial;
            bool result = polinomialString.Contains(Operation.Xor);

            string condition = string.Format(conditionFormat, polinomialString, result ? contains : notContains);
            decisionL.Add(condition);

            string conclusion = string.Format(affiliationFormat, result ? nonAffiliation : affiliation, classL + ".");
            decisionL.Add(conclusion);

            return !result;
        }

        private bool isBelongsToClassM(TruthTable truthTable, out List<string> decisionM)
        {
            const string requirementMessage = ", если для любой пары наборов α и β таких, что α ≤ β, " +
                "выполняется условие f(α) ≤ f(β).";
            const string comparisonFormat = "Сравниваем соседние наборы по {0}-й переменной:";
            const string valuesComparisonFormat = "Сравним значения {0} и {1} - условие монотонности {2}.";
            const string done = "выполнено";
            const string violated = "нарушено";

            string requirementPart = string.Format(affiliationFormat, affiliation, classM + "{0}");
            string requirement = string.Format(requirementPart, requirementMessage);

            decisionM = new List<string>();
            decisionM.Add(requirement);

            string conclusion;
            int rowsCount = truthTable.GetRowsCount();
            bool[] functionVector = truthTable.FunctionVector;
            const int binaryBase = 2;
            double pow = 1;
            double valuesCount = 1;
            while (valuesCount <= rowsCount / 2)
            {
                string comparison = string.Format(comparisonFormat, pow);
                decisionM.Add(comparison);

                int index = 0;
                while (index < rowsCount)
                {
                    StringBuilder sb1 = new StringBuilder();
                    StringBuilder sb2 = new StringBuilder();

                    int count = 0;
                    while (count < valuesCount)
                    {
                        sb1.Append(functionVector[index] ? "1" : "0");

                        count++;
                        index++;
                    }
                    count = 0;
                    while (count < valuesCount)
                    {
                        sb2.Append(functionVector[index] ? "1" : "0");

                        count++;
                        index++;
                    }

                    string firstValue = sb1.ToString();
                    string secondValue = sb2.ToString();
                    int compare = string.Compare(firstValue, secondValue);

                    bool isDone = compare != 1;
                    string valuesComparison = string.Format(valuesComparisonFormat, firstValue, secondValue,
                        isDone ? done : violated);

                    decisionM.Add(valuesComparison);

                    if (!isDone)
                    {
                        conclusion = string.Format(affiliationFormat, nonAffiliation, classM + ".");
                        decisionM.Add(conclusion);

                        return false;
                    }
                }

                valuesCount = Math.Pow(binaryBase, pow);
                pow++;
            }

            conclusion = string.Format(affiliationFormat, affiliation, classM + ".");
            decisionM.Add(conclusion);
            return true;
        }

        private bool isBelongsToClassS(TruthTable truthTable, out List<string> decisionS)
        {
            const string requirementMessage = ", если на противоположных наборах она принимает противоположные значения.";
            const string valuesComparisonFormat = "Сравним значения на наборах {0} и {1}: {2} и {3} - {4}.";
            const string match = "совпадают";
            const string notMatch = "противоположны";

            string requirementPart = string.Format(affiliationFormat, affiliation, classS + "{0}");
            string requirement = string.Format(requirementPart, requirementMessage);

            decisionS = new List<string>();
            decisionS.Add(requirement);

            string conclusion;
            bool[] functionVector = truthTable.FunctionVector;
            int variablesCount = truthTable.Variables.Count;
            int downIndex = 0;
            int upIndex = functionVector.Length - 1;
            while (downIndex < upIndex)
            {
                StringBuilder downSb = new StringBuilder();
                for (int i = 0; i < variablesCount; i++)
                    downSb.Append(truthTable[downIndex, i] ? "1" : "0");
                string downSet = downSb.ToString();
                string downValue = functionVector[downIndex] ? "1" : "0";

                StringBuilder upSb = new StringBuilder();
                for (int i = 0; i < variablesCount; i++)
                    upSb.Append(truthTable[upIndex, i] ? "1" : "0");
                string upSet = upSb.ToString();
                string upValue = functionVector[upIndex] ? "1" : "0";

                int compare = string.Compare(downValue, upValue);
                bool isMatch = compare == 0;

                string valuesComparison = string.Format(valuesComparisonFormat, downSet, upSet, 
                    downValue, upValue, isMatch ? match : notMatch);
                decisionS.Add(valuesComparison);

                if (isMatch)
                {
                    conclusion = string.Format(affiliationFormat, nonAffiliation, classS + ".");
                    decisionS.Add(conclusion);

                    return false;
                }

                downIndex++;
                upIndex--;
            }

            conclusion = string.Format(affiliationFormat, affiliation, classS + ".");
            decisionS.Add(conclusion);
            return true;
        }
    }
}
