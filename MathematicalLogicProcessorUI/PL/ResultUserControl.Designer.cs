
namespace MathematicalLogicProcessorUI
{
    partial class ResultUserControl
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
            this.lResultName = new System.Windows.Forms.Label();
            this.tbResult = new System.Windows.Forms.TextBox();
            this.llDecision = new System.Windows.Forms.LinkLabel();
            this.pDecision = new System.Windows.Forms.Panel();
            this.pInput = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // lResultName
            // 
            this.lResultName.AutoSize = true;
            this.lResultName.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lResultName.Location = new System.Drawing.Point(10, 10);
            this.lResultName.Name = "lResultName";
            this.lResultName.Size = new System.Drawing.Size(102, 21);
            this.lResultName.TabIndex = 0;
            this.lResultName.Text = "lResultName";
            // 
            // tbResult
            // 
            this.tbResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbResult.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbResult.Location = new System.Drawing.Point(10, 34);
            this.tbResult.Name = "tbResult";
            this.tbResult.ReadOnly = true;
            this.tbResult.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbResult.Size = new System.Drawing.Size(533, 33);
            this.tbResult.TabIndex = 1;
            this.tbResult.WordWrap = false;
            // 
            // llDecision
            // 
            this.llDecision.ActiveLinkColor = System.Drawing.Color.Tomato;
            this.llDecision.AutoSize = true;
            this.llDecision.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.llDecision.LinkColor = System.Drawing.Color.Black;
            this.llDecision.Location = new System.Drawing.Point(10, 70);
            this.llDecision.Name = "llDecision";
            this.llDecision.Size = new System.Drawing.Size(74, 21);
            this.llDecision.TabIndex = 2;
            this.llDecision.TabStop = true;
            this.llDecision.Text = "Решение";
            this.llDecision.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llDecision_LinkClicked);
            // 
            // pDecision
            // 
            this.pDecision.AutoSize = true;
            this.pDecision.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pDecision.BackColor = System.Drawing.SystemColors.Control;
            this.pDecision.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pDecision.Location = new System.Drawing.Point(0, 100);
            this.pDecision.Name = "pDecision";
            this.pDecision.Size = new System.Drawing.Size(553, 0);
            this.pDecision.TabIndex = 5;
            // 
            // pInput
            // 
            this.pInput.AutoScroll = true;
            this.pInput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pInput.BackColor = System.Drawing.SystemColors.Control;
            this.pInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pInput.Location = new System.Drawing.Point(0, 0);
            this.pInput.Name = "pInput";
            this.pInput.Size = new System.Drawing.Size(553, 100);
            this.pInput.TabIndex = 6;
            // 
            // ResultUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.llDecision);
            this.Controls.Add(this.tbResult);
            this.Controls.Add(this.lResultName);
            this.Controls.Add(this.pInput);
            this.Controls.Add(this.pDecision);
            this.Name = "ResultUserControl";
            this.Size = new System.Drawing.Size(553, 100);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lResultName;
        private System.Windows.Forms.TextBox tbResult;
        private System.Windows.Forms.LinkLabel llDecision;
        private System.Windows.Forms.Panel pDecision;
        private System.Windows.Forms.Panel pInput;
    }
}
