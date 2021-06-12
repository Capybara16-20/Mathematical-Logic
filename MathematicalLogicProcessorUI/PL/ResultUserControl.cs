using System.Collections.Generic;
using System.Windows.Forms;

namespace MathematicalLogicProcessorUI
{
    public partial class ResultUserControl : UserControl
    {
        private DecisionUserControl ucDecision;
        Dictionary<List<string>, string> decision;
        private bool isShowed;

        public ResultUserControl(string resultName, string result)
        {
            InitializeComponent();

            lResultName.Text = resultName;
            tbResult.Text = result;


        }
        public ResultUserControl(string resultName, Dictionary<List<string>, string> decision, string result)
        {
            InitializeComponent();

            lResultName.Text = resultName;
            tbResult.Text = result;

            this.decision = decision;
        }

        private void llDecision_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!isShowed)
            {
                ucDecision = new DecisionUserControl(decision);
                ucDecision.Dock = DockStyle.Top;
                pDecision.Controls.Add(ucDecision);
                pDecision.AutoSize = true;
                pDecision.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                isShowed = true;
            }
            else
            {
                ucDecision.Dispose();
                isShowed = false;
                pDecision.Height = 0;
            }
        }
    }
}
