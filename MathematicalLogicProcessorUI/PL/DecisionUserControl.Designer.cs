
namespace MathematicalLogicProcessorUI
{
    partial class DecisionUserControl
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
            this.lbDecision = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbDecision
            // 
            this.lbDecision.BackColor = System.Drawing.Color.Bisque;
            this.lbDecision.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbDecision.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbDecision.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbDecision.FormattingEnabled = true;
            this.lbDecision.ItemHeight = 21;
            this.lbDecision.Location = new System.Drawing.Point(0, 0);
            this.lbDecision.Name = "lbDecision";
            this.lbDecision.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lbDecision.Size = new System.Drawing.Size(0, 21);
            this.lbDecision.TabIndex = 0;
            // 
            // DecisionUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.OrangeRed;
            this.Controls.Add(this.lbDecision);
            this.Name = "DecisionUserControl";
            this.Size = new System.Drawing.Size(0, 21);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbDecision;
    }
}
