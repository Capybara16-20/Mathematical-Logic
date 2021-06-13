using System.Collections.Generic;
using System.Windows.Forms;

namespace MathematicalLogicProcessorUI.PL
{
    public partial class TableResultUserControl : UserControl
    {
        private List<DecisionUserControl> decisions;
        private bool isShowed;

        public TableResultUserControl(string resultName, List<string> headers, List<List<string>> table)
        {
            InitializeComponent();

            lResultName.Text = resultName;

            dgvResult.RowCount = table.Count;
            dgvResult.ColumnCount = headers.Count;
            for (int i = 0; i < table.Count; i++)
            {
                for (int j = 0; j < table[i].Count; j++)
                {
                    dgvResult.Columns[j].HeaderText = headers[j];
                    dgvResult.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvResult.Rows[i].Cells[j].Value = table[i][j];
                }
            }

            dgvResult.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvResult.AutoSize = true;
            dgvResult.AllowUserToAddRows = false;

            pDecisionBlock.AutoSize = false;
            pDecisionBlock.Height = 0;
        }

        public TableResultUserControl(string resultName, List<string> headers, List<string> table, 
            List<List<string>> decisions)
        {
            InitializeComponent();

            this.decisions = new List<DecisionUserControl>();
            foreach (List<string> decision in decisions)
                this.decisions.Add(new DecisionUserControl(decision));

            lResultName.Text = resultName;

            int rowIndex = 0;
            int rowsCount = 1;
            dgvResult.RowCount = rowsCount;
            dgvResult.ColumnCount = table.Count;
            for (int i = 0; i < table.Count; i++)
            {
                dgvResult.Columns[i].HeaderText = headers[i];
                dgvResult.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvResult.Rows[rowIndex].Cells[i].Value = table[i];
            }

            dgvResult.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvResult.AutoSize = true;
            dgvResult.AllowUserToAddRows = false;

            pDecisionBlock.Dock = DockStyle.Bottom;
        }

        private void llDecision_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!isShowed)
            {
                foreach (DecisionUserControl decision in decisions)
                    AddDecision(decision);
                pDecisionBlock.AutoSize = true;
                pDecisionBlock.AutoSizeMode = AutoSizeMode.GrowAndShrink;
                isShowed = true;
            }
            else
            {
                foreach (DecisionUserControl decision in decisions)
                    HideDecision(decision);

                isShowed = false;
                pDecisionBlock.Height = 0;
            }
        }

        private void AddDecision(DecisionUserControl ucDecision)
        {
            ucDecision.Dock = DockStyle.Bottom;
            pDecisionBlock.Controls.Add(ucDecision);
        }

        private void HideDecision(DecisionUserControl ucDecision)
        {
            ucDecision.Dispose();
        }
    }
}
