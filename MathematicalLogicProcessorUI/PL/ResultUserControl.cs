using System.Collections.Generic;
using System.Windows.Forms;
using MathematicalLogicProcessorUI.PL;

namespace MathematicalLogicProcessorUI
{
    public partial class ResultUserControl : UserControl
    {
        private List<IDecision> decisions;
        private List<string> linesDecision;
        private Dictionary<List<string>, string> dictionaryDecision;
        private List<List<List<string>>> tableDecisions;
        private List<string> dicisionNames;
        private bool isShowed;

        public ResultUserControl(string resultName, string result)
        {
            InitializeComponent();

            lResultName.Text = resultName;
            tbResult.Text = result;
        }

        public ResultUserControl(string resultName, string result, List<string> decision)
        {
            InitializeComponent();

            lResultName.Text = resultName;
            tbResult.Text = result;

            linesDecision = decision;
        }

        public ResultUserControl(string resultName, string result, Dictionary<List<string>, string> decision)
        {
            InitializeComponent();

            lResultName.Text = resultName;
            tbResult.Text = result;

            dictionaryDecision = decision;
        }

        public ResultUserControl(string resultName, string result, List<List<string>> undefinedCoefficient,
            string undefinedCoefficientName, List<List<string>> triangle, string triangleName, 
            List<List<string>> fft, string fftName)
        {
            InitializeComponent();

            lResultName.Text = resultName;
            tbResult.Text = result;

            tableDecisions = new List<List<List<string>>> { undefinedCoefficient, triangle, fft };
            dicisionNames = new List<string> { undefinedCoefficientName, triangleName, fftName};
        }

        private void llDecision_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!isShowed)
            {
                decisions = new List<IDecision>();

                if (linesDecision != null)
                    decisions.Add(new DecisionUserControl(linesDecision));
                else if (dictionaryDecision != null)
                    decisions.Add(new DecisionUserControl(dictionaryDecision));
                else
                    for (int i = 0; i < tableDecisions.Count; i++)
                        decisions.Add(new TableDecisionUserControl(tableDecisions[i], dicisionNames[i]));

                foreach (IDecision decision in decisions)
                    AddDecision(decision, pDecision);

                pDecision.AutoSize = true;
                pDecision.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                isShowed = true;
            }
            else
            {
                foreach (IDecision decision in decisions)
                    HideDecision(decision, pDecision);

                isShowed = false;
                pDecision.Height = 0;
            }
        }

        private void AddDecision(IDecision ucDecision, Panel pDecision)
        {
            if (ucDecision is DecisionUserControl textDecision)
            {
                textDecision.Dock = DockStyle.Top;
                pDecision.Controls.Add(textDecision);
            }
            else if (ucDecision is TableDecisionUserControl tableDecision)
            {
                tableDecision.Dock = DockStyle.Top;
                pDecision.Controls.Add(tableDecision);
            }
        }

        private void HideDecision(IDecision ucDecision, Panel pDecision)
        {
            if (ucDecision is DecisionUserControl textDecision)
                textDecision.Dispose();
            else if (ucDecision is TableDecisionUserControl tableDecision)
                tableDecision.Dispose();
        }
    }
}
