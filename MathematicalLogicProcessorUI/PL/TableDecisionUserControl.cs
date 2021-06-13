using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MathematicalLogicProcessorUI.PL
{
    public partial class TableDecisionUserControl : UserControl, IDecision
    {
        public TableDecisionUserControl(List<List<string>> decision, string dicisionName)
        {
            InitializeComponent();

            lDecisionName.Text = dicisionName;

            dgvDecision.RowCount = decision.Count;
            dgvDecision.ColumnCount = decision.Max(n => n.Count);
            for (int i = 0; i < decision.Count; i++)
            {
                for (int j = 0; j < decision[i].Count; j++)
                {
                    dgvDecision.Columns[j].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvDecision.Rows[i].Cells[j].Value = decision[i][j];
                }
            }

            dgvDecision.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvDecision.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDecision.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvDecision.AutoSize = true;
            dgvDecision.AllowUserToAddRows = false;
        }
    }
}
