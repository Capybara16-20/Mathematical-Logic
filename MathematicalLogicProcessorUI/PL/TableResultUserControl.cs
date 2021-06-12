using System.Collections.Generic;
using System.Windows.Forms;

namespace MathematicalLogicProcessorUI.PL
{
    public partial class TableResultUserControl : UserControl
    {
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

            dgvResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dgvResult.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvResult.AutoSize = true;
            dgvResult.AllowUserToAddRows = false;
        }
    }
}
