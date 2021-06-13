using System.Collections.Generic;
using System.Windows.Forms;
using MathematicalLogicProcessorUI.PL;

namespace MathematicalLogicProcessorUI
{
    public partial class DecisionUserControl : UserControl, IDecision
    {
        public DecisionUserControl(Dictionary<List<string>, string> decision)
        {
            const string equally = "=";

            InitializeComponent();

            lbDecision.Items.Clear();
            foreach (List<string> expressions in decision.Keys)
            {
                lbDecision.Items.Add(decision[expressions]);

                int lastIndex = expressions.Count - 1;
                for (int i = 0; i < lastIndex; i++)
                    lbDecision.Items.Add(string.Format("{0} {1}", expressions[i], equally));
                lbDecision.Items.Add(expressions[lastIndex]);

                lbDecision.Items.Add(string.Empty);
            }

            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
            lbDecision.AutoSize = true;
        }

        public DecisionUserControl(List<string> decision)
        {
            InitializeComponent();

            lbDecision.Items.Clear();
            foreach (string line in decision)
                lbDecision.Items.Add(line);
            lbDecision.Items.Add(string.Empty);

            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AutoSize = true;
            lbDecision.AutoSize = true;
        }
    }
}
