
namespace MathematicalLogicProcessorUI.PL
{
    partial class SeparatorUserControl
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
            this.pSeparator = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // pSeparator
            // 
            this.pSeparator.BackColor = System.Drawing.Color.Gray;
            this.pSeparator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pSeparator.Location = new System.Drawing.Point(0, 0);
            this.pSeparator.Name = "pSeparator";
            this.pSeparator.Size = new System.Drawing.Size(400, 2);
            this.pSeparator.TabIndex = 0;
            // 
            // SeparatorUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pSeparator);
            this.Name = "SeparatorUserControl";
            this.Size = new System.Drawing.Size(400, 2);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pSeparator;
    }
}
