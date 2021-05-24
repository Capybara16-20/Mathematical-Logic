using System;

namespace MathematicalLogicProcessor
{
    public class LogicalExpression
    {
        private string expression;
        private TruthTable truthTable;

        public LogicalExpression(string expression)
        {
            this.expression = expression;
        }

        public TruthTable TruthTable { get { return truthTable; } }
    }
}
