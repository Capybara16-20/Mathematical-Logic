
namespace MathematicalLogicProcessorUI.PL
{
    partial class TableResultUserControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.pResultName = new System.Windows.Forms.Panel();
            this.lResultName = new System.Windows.Forms.Label();
            this.llDecision = new System.Windows.Forms.LinkLabel();
            this.pDecisionBlock = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pDecision = new System.Windows.Forms.Panel();
            this.pResult = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.pResultName.SuspendLayout();
            this.pDecisionBlock.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pResult.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToResizeColumns = false;
            this.dgvResult.AllowUserToResizeRows = false;
            this.dgvResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.dgvResult.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvResult.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResult.Location = new System.Drawing.Point(0, 0);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvResult.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvResult.RowHeadersVisible = false;
            this.dgvResult.RowTemplate.Height = 25;
            this.dgvResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.dgvResult.Size = new System.Drawing.Size(0, 0);
            this.dgvResult.TabIndex = 0;
            // 
            // pResultName
            // 
            this.pResultName.Controls.Add(this.lResultName);
            this.pResultName.Dock = System.Windows.Forms.DockStyle.Top;
            this.pResultName.Location = new System.Drawing.Point(0, 0);
            this.pResultName.Name = "pResultName";
            this.pResultName.Size = new System.Drawing.Size(0, 40);
            this.pResultName.TabIndex = 1;
            // 
            // lResultName
            // 
            this.lResultName.AutoSize = true;
            this.lResultName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lResultName.Location = new System.Drawing.Point(10, 10);
            this.lResultName.Name = "lResultName";
            this.lResultName.Size = new System.Drawing.Size(102, 21);
            this.lResultName.TabIndex = 1;
            this.lResultName.Text = "lResultName";
            // 
            // llDecision
            // 
            this.llDecision.ActiveLinkColor = System.Drawing.Color.Tomato;
            this.llDecision.AutoSize = true;
            this.llDecision.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.llDecision.LinkColor = System.Drawing.Color.Black;
            this.llDecision.Location = new System.Drawing.Point(10, 10);
            this.llDecision.Name = "llDecision";
            this.llDecision.Size = new System.Drawing.Size(74, 21);
            this.llDecision.TabIndex = 2;
            this.llDecision.TabStop = true;
            this.llDecision.Text = "Решение";
            this.llDecision.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDecision_LinkClicked);
            // 
            // pDecisionBlock
            // 
            this.pDecisionBlock.AutoSize = true;
            this.pDecisionBlock.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pDecisionBlock.BackColor = System.Drawing.SystemColors.Control;
            this.pDecisionBlock.Controls.Add(this.panel2);
            this.pDecisionBlock.Controls.Add(this.pDecision);
            this.pDecisionBlock.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pDecisionBlock.Location = new System.Drawing.Point(0, 40);
            this.pDecisionBlock.Name = "pDecisionBlock";
            this.pDecisionBlock.Size = new System.Drawing.Size(0, 40);
            this.pDecisionBlock.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.llDecision);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(0, 40);
            this.panel2.TabIndex = 4;
            // 
            // pDecision
            // 
            this.pDecision.AutoSize = true;
            this.pDecision.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pDecision.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pDecision.Location = new System.Drawing.Point(0, 40);
            this.pDecision.Name = "pDecision";
            this.pDecision.Size = new System.Drawing.Size(0, 0);
            this.pDecision.TabIndex = 3;
            // 
            // pResult
            // 
            this.pResult.AutoSize = true;
            this.pResult.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pResult.BackColor = System.Drawing.SystemColors.Control;
            this.pResult.Controls.Add(this.dgvResult);
            this.pResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pResult.Location = new System.Drawing.Point(0, 40);
            this.pResult.Name = "pResult";
            this.pResult.Size = new System.Drawing.Size(0, 0);
            this.pResult.TabIndex = 4;
            // 
            // TableResultUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pResult);
            this.Controls.Add(this.pResultName);
            this.Controls.Add(this.pDecisionBlock);
            this.Name = "TableResultUserControl";
            this.Size = new System.Drawing.Size(0, 80);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.pResultName.ResumeLayout(false);
            this.pResultName.PerformLayout();
            this.pDecisionBlock.ResumeLayout(false);
            this.pDecisionBlock.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pResult.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Panel pResultName;
        private System.Windows.Forms.Label lResultName;
        private System.Windows.Forms.LinkLabel llDecision;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pDecisionBlock;
        private System.Windows.Forms.Panel pResult;
        private System.Windows.Forms.Panel pDecision;
        private System.Windows.Forms.Panel panel2;
    }
}
