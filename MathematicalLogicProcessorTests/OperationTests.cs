using NUnit.Framework;
using MathematicalLogicProcessor;
using System.Collections;

namespace MathematicalLogicProcessorTests
{
    public class OperationTests
    {
        [Test]
        [TestCaseSource(nameof(GetTestNOTCases))]
        public bool TestNOT(bool a)
        {
            return Operation.NOT(a);
        }

        private static IEnumerable GetTestNOTCases
        {
            get
            {
                yield return new TestCaseData(false).Returns(true);
                yield return new TestCaseData(true).Returns(false);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestANDCases))]
        public bool TestAND(bool a, bool b)
        {
            return Operation.AND(a, b);
        }

        private static IEnumerable GetTestANDCases
        {
            get
            {
                yield return new TestCaseData(false, false).Returns(false);
                yield return new TestCaseData(false, true).Returns(false);
                yield return new TestCaseData(true, false).Returns(false);
                yield return new TestCaseData(true, true).Returns(true);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestORCases))]
        public bool TestOR(bool a, bool b)
        {
            return Operation.OR(a, b);
        }

        private static IEnumerable GetTestORCases
        {
            get
            {
                yield return new TestCaseData(false, false).Returns(false);
                yield return new TestCaseData(false, true).Returns(true);
                yield return new TestCaseData(true, false).Returns(true);
                yield return new TestCaseData(true, true).Returns(true);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestXORCases))]
        public bool TestXOR(bool a, bool b)
        {
            return Operation.XOR(a, b);
        }

        private static IEnumerable GetTestXORCases
        {
            get
            {
                yield return new TestCaseData(false, false).Returns(false);
                yield return new TestCaseData(false, true).Returns(true);
                yield return new TestCaseData(true, false).Returns(true);
                yield return new TestCaseData(true, true).Returns(false);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestIMPLCases))]
        public bool TestIMPL(bool a, bool b)
        {
            return Operation.IMPL(a, b);
        }

        private static IEnumerable GetTestIMPLCases
        {
            get
            {
                yield return new TestCaseData(false, false).Returns(true);
                yield return new TestCaseData(false, true).Returns(true);
                yield return new TestCaseData(true, false).Returns(false);
                yield return new TestCaseData(true, true).Returns(true);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestReIMPLCases))]
        public bool TestXor(bool a, bool b)
        {
            return Operation.ReIMPL(a, b);
        }

        private static IEnumerable GetTestReIMPLCases
        {
            get
            {
                yield return new TestCaseData(false, false).Returns(true);
                yield return new TestCaseData(false, true).Returns(false);
                yield return new TestCaseData(true, false).Returns(true);
                yield return new TestCaseData(true, true).Returns(true);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestXNORCases))]
        public bool TestXNOR(bool a, bool b)
        {
            return Operation.XNOR(a, b);
        }

        private static IEnumerable GetTestXNORCases
        {
            get
            {
                yield return new TestCaseData(false, false).Returns(true);
                yield return new TestCaseData(false, true).Returns(false);
                yield return new TestCaseData(true, false).Returns(false);
                yield return new TestCaseData(true, true).Returns(true);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestNORCases))]
        public bool TestNOR(bool a, bool b)
        {
            return Operation.NOR(a, b);
        }

        private static IEnumerable GetTestNORCases
        {
            get
            {
                yield return new TestCaseData(false, false).Returns(true);
                yield return new TestCaseData(false, true).Returns(false);
                yield return new TestCaseData(true, false).Returns(false);
                yield return new TestCaseData(true, true).Returns(false);
            }
        }

        [Test]
        [TestCaseSource(nameof(GetTestNANDCases))]
        public bool TestNAND(bool a, bool b)
        {
            return Operation.NAND(a, b);
        }

        private static IEnumerable GetTestNANDCases
        {
            get
            {
                yield return new TestCaseData(false, false).Returns(true);
                yield return new TestCaseData(false, true).Returns(true);
                yield return new TestCaseData(true, false).Returns(true);
                yield return new TestCaseData(true, true).Returns(false);
            }
        }
    }
}
