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

                    AddSeparatorsBetweenTokens(disjunction, new Token(Operation.And, TokenType.Operation));
                    temp.Add(disjunction);
                }
            }

            if (temp.Count > 1)
                for (int i = 0; i < temp.Count; i++)
                    EncloseInBraces(temp[i]);

            foreach (List<Token> tokens in temp)
                pdnf.AddRange(tokens);

            AddSeparatorsBetweenBraces(pdnf, new Token(Operation.Or, TokenType.Operation));

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

                    AddSeparatorsBetweenTokens(disjunction, new Token(Operation.Or, TokenType.Operation));
                    temp.Add(disjunction);
                }
            }

            if (temp.Count > 1)
                for (int i = 0; i < temp.Count; i++)
                    EncloseInBraces(temp[i]);

            foreach (List<Token> tokens in temp)
                pcnf.AddRange(tokens);

            AddSeparatorsBetweenBraces(pcnf, new Token(Operation.And, TokenType.Operation));

            return pcnf;
        }

        private void AddSeparatorsBetweenTokens(List<Token> tokens, Token separator)
        {
            for (int i = 1; i < tokens.Count; i++)
                if (tokens[i - 1].Type == TokenType.Variable)
                    tokens.Insert(i, separator);
        }

        private void AddSeparatorsBetweenBraces(List<Token> tokens, Token separator)
        {
            for (int i = 1; i < tokens.Count; i++)
                if (tokens[i - 1].Type == TokenType.CloseBrace
                    && tokens[i].Type == TokenType.OpenBrace)
                    tokens.Insert(i, separator);
        }

        private void EncloseInBraces(List<Token> tokens)
        {
            tokens.Insert(0, new Token("(", TokenType.OpenBrace));
            tokens.Add(new Token(")", TokenType.CloseBrace));
        }
    }
}
