using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MathematicalLogicProcessor;
using MathematicalLogicProcessorUI.BLL;

namespace MathematicalLogicProcessorUI.PL
{
    public partial class MainForm : Form
    {
        HandlerBL handler = null;

        const string truthTableString = "Таблица истинности";
        const string pcnfString = "СКНФ";
        const string pdnfString = "СДНФ";
        const string cnfString = "КНФ";
        const string dnfString = "ДНФ";
        const string polinomialString = "Полином Жегалкина";
        const string classificationString = "Классификация Поста";

        const string emptyExpressionMessage = "Введите логическую функцию, её вектор или номер.";
        const string incorrectFormatMessage = "Некорректный формат выражения.";
        const string notChoosenOptionsMessage = "Не выбраны условия для расчёта.";
        const string notExists = "Не существует";

        const string undefinedCoefficientName = "Метод неопределённых коэффициентов";
        const string triangleName = "Метод треугольника";
        const string fftName = "Метод БПФ";

        public MainForm()
        {
            InitializeComponent();
            InicializeCheckBoxes();
        }

        private void bNOT_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.Not);

        private void bOR_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.Or);

        private void bAND_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.And);

        private void bXOR_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.Xor);

        private void bIMPL_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.Implication);

        private void bREIMPL_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.ReverseImplication);

        private void bXNOR_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.Xnor);

        private void bNOR_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.Nor);

        private void bNAND_Click(object sender, EventArgs e) => InputToken(tbInputField, Operation.Nand);

        private void bOpenBrace_Click(object sender, EventArgs e) => InputToken(tbInputField, Token.OpenBrace);

        private void bCloseBrace_Click(object sender, EventArgs e) => InputToken(tbInputField, Token.CloseBrace);

        private void bZero_Click(object sender, EventArgs e) => InputToken(tbInputField, Operand.Zero);

        private void bOne_Click(object sender, EventArgs e) => InputToken(tbInputField, Operand.One);

        private void bAOperand_Click(object sender, EventArgs e) => InputToken(tbInputField, bAOperand.Text);

        private void bBOperand_Click(object sender, EventArgs e) => InputToken(tbInputField, bBOperand.Text);

        private void bCOperand_Click(object sender, EventArgs e) => InputToken(tbInputField, bCOperand.Text);

        private void bX1_Click(object sender, EventArgs e) => InputToken(tbInputField, bX1.Text);

        private void bX2_Click(object sender, EventArgs e) => InputToken(tbInputField, bX2.Text);

        private void bX3_Click(object sender, EventArgs e) => InputToken(tbInputField, bX3.Text);

        private void bX4_Click(object sender, EventArgs e) => InputToken(tbInputField, bX4.Text);

        private void bX5_Click(object sender, EventArgs e) => InputToken(tbInputField, bX5.Text);

        private void bCalculate_Click(object sender, EventArgs e) => Calculate();

        private void InicializeCheckBoxes()
        {
            cbTruthTable.Text = truthTableString;
            cbPCNF.Text = pcnfString;
            cbPDNF.Text = pdnfString;
            cbCNF.Text = cnfString;
            cbDNF.Text = dnfString;
            cbPolinomial.Text = polinomialString;
            cbClassification.Text = classificationString;
        }

        private void InputToken(TextBox textBox, string token)
        {
            int start = textBox.SelectionStart;
            textBox.Text = textBox.Text.Insert(start, token);
            textBox.Focus();
            textBox.Select(start + token.Length, 0);
        }

        private void Calculate()
        {
            ClearOutput();

            string expression = tbInputField.Text;
            if (!IsValidated(expression))
                return;

            try
            {
                handler = new HandlerBL(expression);
            }
            catch
            {
                MessageBox.Show(incorrectFormatMessage);
                return;
            }

            List<CheckBox> checkBoxes = GetCheckedCheckBoxes(gbOptions);

            ClearOutput();
            AddResults(checkBoxes);
        }

        private bool IsValidated(string expression)
        {
            if (!ValidateExpression(expression))
            {
                MessageBox.Show(emptyExpressionMessage);
                return false;
            }

            if (!IsChoosenSmth())
            {
                MessageBox.Show(notChoosenOptionsMessage);
                return false;
            }

            return true;
        }

        private bool ValidateExpression(string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return false;

            return true;
        }

        private bool IsChoosenSmth()
        {
            bool isChoosen = false;
            foreach (Panel panel in gbOptions.Controls.OfType<Panel>())
                foreach (CheckBox checkBox in panel.Controls.OfType<CheckBox>())
                    if (checkBox.Checked)
                        isChoosen = true;

            return isChoosen;
        }

        private void ClearOutput()
        {
            RemoveControls(pOutput, typeof(ResultUserControl));
            RemoveControls(pOutput, typeof(TableResultUserControl));
            RemoveControls(pOutput, typeof(SeparatorUserControl));
        }

        private void RemoveControls(Control control, Type type)
        {
            List<Control> controls = new List<Control>();
            Stack<Control> stack = new Stack<Control>();
            stack.Push(control);
            while (stack.Count > 0)
                foreach (Control child in stack.Pop().Controls)
                    if (child.GetType() == type)
                        controls.Add(child);
                    else
                        if (true == child.HasChildren)
                        stack.Push(child);

            foreach (Control ctrl in controls)
            {
                control.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }

        private List<CheckBox> GetCheckedCheckBoxes(GroupBox groupBox)
        {
            List<CheckBox> checkBoxes = new List<CheckBox>();
            foreach (Panel panel in groupBox.Controls.OfType<Panel>())
                foreach (CheckBox checkBox in panel.Controls.OfType<CheckBox>())
                    if (checkBox.Checked)
                        checkBoxes.Add(checkBox);

            return checkBoxes;
        }

        private void AddResults(List<CheckBox> checkBoxes)
        {
            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Text == classificationString)
                    AddResult(classificationString, handler.PostClassificationHeaders, 
                        handler.PostClassification, handler.PostClassificationDecision);

            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Text == polinomialString)
                    AddResult(polinomialString, handler.Polynomial, handler.UndefinedCoefficientMethodTable, 
                        handler.TriangleMethodTable, handler.FFTMethodTable);

            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Text == dnfString)
                    AddResult(dnfString, handler.DNF, handler.DNFDecision);

            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Text == cnfString)
                    AddResult(cnfString, handler.CNF, handler.CNFDecision);

            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Text == pdnfString)
                    AddResult(pdnfString, handler.PDNF, handler.PDNFDecision);

            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Text == pcnfString)
                    AddResult(pcnfString, handler.PCNF, handler.PCNFDecision);

            foreach (CheckBox checkBox in checkBoxes)
                if (checkBox.Text == truthTableString)
                    AddResult(truthTableString, handler.TruthTableHeaders, handler.TruthTable);
        }

        private void AddResult(string resultName, string result)
        {
            ResultUserControl ucResult = new ResultUserControl(resultName, result);
            ucResult.Dock = DockStyle.Top;

            if (pOutput.Controls.Count > 0)
            {
                SeparatorUserControl separator = new SeparatorUserControl();
                separator.Dock = DockStyle.Top;
                pOutput.Controls.Add(separator);
            }

            pOutput.Controls.Add(ucResult);
        }

        private void AddResult(string resultName, string result, List<string> decision)
        {
            ResultUserControl ucResult = new ResultUserControl(resultName, result, decision);
            ucResult.Dock = DockStyle.Top;

            if (pOutput.Controls.Count > 0)
            {
                SeparatorUserControl separator = new SeparatorUserControl();
                separator.Dock = DockStyle.Top;
                pOutput.Controls.Add(separator);
            }

            pOutput.Controls.Add(ucResult);
        }

        private void AddResult(string resultName, string result, Dictionary<List<string>, string> decision)
        {
            ResultUserControl ucResult = new ResultUserControl(resultName, result, decision);
            ucResult.Dock = DockStyle.Top;

            if (pOutput.Controls.Count > 0)
            {
                SeparatorUserControl separator = new SeparatorUserControl();
                separator.Dock = DockStyle.Top;
                pOutput.Controls.Add(separator);
            }

            pOutput.Controls.Add(ucResult);
        }

        private void AddResult(string resultName, string result, List<List<string>> undefinedCoefficient,
            List<List<string>> triangle, List<List<string>> fft)
        {
            ResultUserControl ucResult = new ResultUserControl(resultName, result, undefinedCoefficient,
                undefinedCoefficientName, triangle, triangleName, fft, fftName);
            ucResult.Dock = DockStyle.Top;

            if (pOutput.Controls.Count > 0)
            {
                SeparatorUserControl separator = new SeparatorUserControl();
                separator.Dock = DockStyle.Top;
                pOutput.Controls.Add(separator);
            }

            pOutput.Controls.Add(ucResult);
        }

        private void AddResult(string resultName, List<string> headers, List<List<string>> table)
        {
            TableResultUserControl ucResult = new TableResultUserControl(resultName, headers, 
                table);
            ucResult.Dock = DockStyle.Top;

            if (pOutput.Controls.Count > 0)
            {
                SeparatorUserControl separator = new SeparatorUserControl();
                separator.Dock = DockStyle.Top;
                pOutput.Controls.Add(separator);
            }

            pOutput.Controls.Add(ucResult);
        }

        private void AddResult(string resultName, List<string> headers, List<string> table,
            List<List<string>> decision)
        {
            TableResultUserControl ucResult = new TableResultUserControl(resultName, headers, table, decision);
            ucResult.Dock = DockStyle.Top;

            if (pOutput.Controls.Count > 0)
            {
                SeparatorUserControl separator = new SeparatorUserControl();
                separator.Dock = DockStyle.Top;
                pOutput.Controls.Add(separator);
            }

            pOutput.Controls.Add(ucResult);
        }
    }
}
