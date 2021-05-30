using System.Collections.Generic;
using System.Linq;

namespace MathematicalLogicProcessor
{
    public class TruthTable
    {
        private readonly List<Token> polishNotation;
        private readonly List<Operand> variables;
        private readonly List<List<Token>> headers;

        public List<Token> PolishNotation { get { return polishNotation; } }
        public List<Operand> Variables { get { return variables; } }
        public List<List<Token>> Headers { get { return headers; } }

        public TruthTable(List<Token> polishNotation, List<Operand> variables)
        {
            this.polishNotation = polishNotation;
            this.variables = variables;
            headers = GetHeaders(polishNotation, variables);
        }

        private List<List<Token>> GetHeaders(List<Token> polishNotation, List<Operand> variables)
        {
            Dictionary<string, int> operationPriorities = Operation.Priorities;
            Dictionary<string, int> operationOperandsCount = Operation.OperandsCount;

            List<List<Token>> headers = new List<List<Token>>();
            foreach (Operand variable in variables)
                headers.Add(new List<Token> { variable });

            List<List<Token>> temp = new List<List<Token>>();
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
                        if (operand.Count > 1)
                            operand = EncloseInBraces(operand);

                        List<Token> newOperand = new List<Token> { polishNotation[i] };
                        newOperand.AddRange(operand);

                        temp.Add(newOperand);
                        stack.Push(newOperand);
                    }
                    else
                    {
                        List<Token> operand2 = stack.Pop();
                        List<Token> operand1 = stack.Pop();

                        if (operand1.Any(n => n.Type == TokenType.Operation 
                            && operationPriorities[n.Identifier] 
                            < operationPriorities[polishNotation[i].Identifier]))
                            operand1 = EncloseInBraces(operand1);

                        if (operand2.Any(n => n.Type == TokenType.Operation
                            && operationPriorities[n.Identifier]
                            < operationPriorities[polishNotation[i].Identifier]))
                            operand2 = EncloseInBraces(operand2);

                        List<Token> newOperand = new List<Token>();
                        newOperand.AddRange(operand1);
                        newOperand.Add(polishNotation[i]);
                        newOperand.AddRange(operand2);

                        temp.Add(newOperand);
                        stack.Push(newOperand);
                    }
                }
            }

            headers.AddRange(temp);
            return headers;
        }

        private List<Token> EncloseInBraces(List<Token> operand)
        {
            List<Token> newOperand = new List<Token>();
            newOperand.Add(new Token("(", TokenType.OpenBrace));
            newOperand.AddRange(operand);
            newOperand.Add(new Token(")", TokenType.CloseBrace));

            return newOperand;
        }
    }
}
