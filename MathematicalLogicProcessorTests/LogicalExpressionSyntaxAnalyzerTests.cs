using NUnit.Framework;
using MathematicalLogicProcessor;
using System.Collections.Generic;

namespace MathematicalLogicProcessorTests
{
    public class LogicalExpressionSyntaxAnalyzerTests
    {
        [Test]
        public void TestCheckExpression_NormalExpression_Return()
        {
            Assert.Pass();
        }


        [Test]
        public void TestGetTokens_NoDuplicateTokens_Return()
        {
            const string expression = "A^B&C";
            List<Token> expected = new List<Token>()
            {
                new Token("A", TokenType.Variable),
                new Token("^", TokenType.Operation),
                new Token("B", TokenType.Variable),
                new Token("&", TokenType.Operation),
                new Token("C", TokenType.Variable)
            };

            List<Token> actual = LogicalExpressionSyntaxAnalyzer.GetTokens(expression);

            CollectionAssert.AreEqual(actual, expected);
        }

        [Test]
        public void TestGetTokens_DuplicateTokens_Return()
        {
            const string expression = "A^B&A";
            List<Token> expected = new List<Token>()
            {
                new Token("A", TokenType.Variable),
                new Token("^", TokenType.Operation),
                new Token("B", TokenType.Variable),
                new Token("&", TokenType.Operation),
                new Token("A", TokenType.Variable)
            };

            List<Token> actual = LogicalExpressionSyntaxAnalyzer.GetTokens(expression);

            CollectionAssert.AreEqual(actual, expected);
        }

        [Test]
        public void TestGetTokens_VariableWithNumber_Return()
        {
            const string expression = "a1^B";
            List<Token> expected = new List<Token>()
            {
                new Token("a1", TokenType.Variable),
                new Token("^", TokenType.Operation),
                new Token("B", TokenType.Variable)
            };

            List<Token> actual = LogicalExpressionSyntaxAnalyzer.GetTokens(expression);

            CollectionAssert.AreEqual(actual, expected);
        }

        [Test]
        public void TestGetTokens_ExpressionWithConst_Return()
        {
            const string expression = "a1^0";
            List<Token> expected = new List<Token>()
            {
                new Token("a1", TokenType.Variable),
                new Token("^", TokenType.Operation),
                new Token("0", TokenType.Const)
            };

            List<Token> actual = LogicalExpressionSyntaxAnalyzer.GetTokens(expression);

            CollectionAssert.AreEqual(actual, expected);
        }

        [Test]
        public void TestGetTokens_ExpressionWithBraces_Return()
        {
            const string expression = "((A^0)&C)↔D";
            List<Token> expected = new List<Token>()
            {
                new Token("(", TokenType.OpenBrace),
                new Token("(", TokenType.OpenBrace),
                new Token("A", TokenType.Variable),
                new Token("^", TokenType.Operation),
                new Token("0", TokenType.Const),
                new Token(")", TokenType.CloseBrace),
                new Token("&", TokenType.Operation),
                new Token("C", TokenType.Variable),
                new Token(")", TokenType.CloseBrace),
                new Token("↔", TokenType.Operation),
                new Token("D", TokenType.Variable)
            };

            List<Token> actual = LogicalExpressionSyntaxAnalyzer.GetTokens(expression);

            CollectionAssert.AreEqual(actual, expected);
        }
    }
}