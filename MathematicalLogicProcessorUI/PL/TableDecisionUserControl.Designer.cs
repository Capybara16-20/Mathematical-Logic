
namespace MathematicalLogicProcessorUI.PL
{
    partial class TableDecisionUserControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvDecision = new System.Windows.Forms.DataGridView();
            this.pDecisionName = new System.Windows.Forms.Panel();
            this.lDecisionName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDecision)).BeginInit();
            this.pDecisionName.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvDecision
            // 
            this.dgvDecision.AllowUserToAddRows = false;
            this.dgvDecision.AllowUserToDeleteRows = false;
            this.dgvDecision.AllowUserToResizeColumns = false;
            this.dgvDecision.AllowUserToResizeRows = false;
            this.dgvDecision.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvDecision.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvDecision.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvDecision.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDecision.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDecision.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDecision.ColumnHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDecision.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDecision.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDecision.Location = new System.Drawing.Point(0, 39);
            this.dgvDecision.Name = "dgvDecision";
            this.dgvDecision.ReadOnly = true;
            this.dgvDecision.RowHeadersVisible = false;
            this.dgvDecision.RowTemplate.Height = 25;
            this.dgvDecision.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgvDecision.Size = new System.Drawing.Size(0, 0);
            this.dgvDecision.TabIndex = 0;
            // 
            // pDecisionName
            // 
            this.pDecisionName.Controls.Add(this.lDecisionName);
            this.pDecisionName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pDecisionName.Location = new System.Drawing.Point(0, 0);
            this.pDecisionName.Name = "pDecisionName";
            this.pDecisionName.Size = new System.Drawing.Size(0, 39);
            this.pDecisionName.TabIndex = 1;
            // 
            // lDecisionName
            // 
            this.lDecisionName.AutoSize = true;
            this.lDecisionName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lDecisionName.Location = new System.Drawing.Point(10, 10);
            this.lDecisionName.Name = "lDecisionName";
            this.lDecisionName.Size = new System.Drawing.Size(102, 21);
            this.lDecisionName.TabIndex = 2;
            this.lDecisionName.Text = "lResultName";
            // 
            // TableDecisionUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.dgvDecision);
            this.Controls.Add(this.pDecisionName);
            this.Name = "TableDecisionUserControl";
            this.Size = new System.Drawing.Size(0, 39);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDecision)).EndInit();
            this.pDecisionName.ResumeLayout(false);
            this.pDecisionName.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvDecision;
        private System.Windows.Forms.Panel pDecisionName;
        private System.Windows.Forms.Label lDecisionName;
    }
}
