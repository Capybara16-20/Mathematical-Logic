
namespace MathematicalLogicProcessorUI.PL
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tlpMainField = new System.Windows.Forms.TableLayoutPanel();
            this.tlpOptions = new System.Windows.Forms.TableLayoutPanel();
            this.gbKeyboard = new System.Windows.Forms.GroupBox();
            this.pOperands = new System.Windows.Forms.Panel();
            this.bX3 = new System.Windows.Forms.Button();
            this.bBOperand = new System.Windows.Forms.Button();
            this.bX2 = new System.Windows.Forms.Button();
            this.bAOperand = new System.Windows.Forms.Button();
            this.bX1 = new System.Windows.Forms.Button();
            this.bOne = new System.Windows.Forms.Button();
            this.bZero = new System.Windows.Forms.Button();
            this.bCOperand = new System.Windows.Forms.Button();
            this.bX5 = new System.Windows.Forms.Button();
            this.bX4 = new System.Windows.Forms.Button();
            this.bCloseBrace = new System.Windows.Forms.Button();
            this.bOpenBrace = new System.Windows.Forms.Button();
            this.bNAND = new System.Windows.Forms.Button();
            this.bNOR = new System.Windows.Forms.Button();
            this.bXNOR = new System.Windows.Forms.Button();
            this.bREIMPL = new System.Windows.Forms.Button();
            this.bIMPL = new System.Windows.Forms.Button();
            this.bXOR = new System.Windows.Forms.Button();
            this.bNOT = new System.Windows.Forms.Button();
            this.bAND = new System.Windows.Forms.Button();
            this.bOR = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.gbOptions = new System.Windows.Forms.GroupBox();
            this.pDNF = new System.Windows.Forms.Panel();
            this.cbDNF = new System.Windows.Forms.CheckBox();
            this.pClassification = new System.Windows.Forms.Panel();
            this.cbClassification = new System.Windows.Forms.CheckBox();
            this.pPolinomial = new System.Windows.Forms.Panel();
            this.cbPolinomial = new System.Windows.Forms.CheckBox();
            this.pCNF = new System.Windows.Forms.Panel();
            this.cbCNF = new System.Windows.Forms.CheckBox();
            this.pPDNF = new System.Windows.Forms.Panel();
            this.cbPDNF = new System.Windows.Forms.CheckBox();
            this.pPCNF = new System.Windows.Forms.Panel();
            this.cbPCNF = new System.Windows.Forms.CheckBox();
            this.pTruthTable = new System.Windows.Forms.Panel();
            this.cbTruthTable = new System.Windows.Forms.CheckBox();
            this.tlpOutput = new System.Windows.Forms.TableLayoutPanel();
            this.pInputField = new System.Windows.Forms.Panel();
            this.bCalculate = new System.Windows.Forms.Button();
            this.lbInstruction = new System.Windows.Forms.Label();
            this.tbInputField = new System.Windows.Forms.TextBox();
            this.pOutput = new System.Windows.Forms.Panel();
            this.tlpMainField.SuspendLayout();
            this.tlpOptions.SuspendLayout();
            this.gbKeyboard.SuspendLayout();
            this.pOperands.SuspendLayout();
            this.gbOptions.SuspendLayout();
            this.pDNF.SuspendLayout();
            this.pClassification.SuspendLayout();
            this.pPolinomial.SuspendLayout();
            this.pCNF.SuspendLayout();
            this.pPDNF.SuspendLayout();
            this.pPCNF.SuspendLayout();
            this.pTruthTable.SuspendLayout();
            this.tlpOutput.SuspendLayout();
            this.pInputField.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMainField
            // 
            this.tlpMainField.ColumnCount = 2;
            this.tlpMainField.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.tlpMainField.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainField.Controls.Add(this.tlpOptions, 0, 0);
            this.tlpMainField.Controls.Add(this.tlpOutput, 1, 0);
            this.tlpMainField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainField.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tlpMainField.Location = new System.Drawing.Point(0, 0);
            this.tlpMainField.Name = "tlpMainField";
            this.tlpMainField.RowCount = 1;
            this.tlpMainField.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMainField.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 561F));
            this.tlpMainField.Size = new System.Drawing.Size(984, 561);
            this.tlpMainField.TabIndex = 0;
            // 
            // tlpOptions
            // 
            this.tlpOptions.ColumnCount = 1;
            this.tlpOptions.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOptions.Controls.Add(this.gbKeyboard, 0, 0);
            this.tlpOptions.Controls.Add(this.gbOptions, 0, 1);
            this.tlpOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOptions.Location = new System.Drawing.Point(3, 3);
            this.tlpOptions.Name = "tlpOptions";
            this.tlpOptions.RowCount = 3;
            this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.tlpOptions.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 263F));
            this.tlpOptions.Size = new System.Drawing.Size(304, 555);
            this.tlpOptions.TabIndex = 1;
            // 
            // gbKeyboard
            // 
            this.gbKeyboard.Controls.Add(this.pOperands);
            this.gbKeyboard.Controls.Add(this.bCloseBrace);
            this.gbKeyboard.Controls.Add(this.bOpenBrace);
            this.gbKeyboard.Controls.Add(this.bNAND);
            this.gbKeyboard.Controls.Add(this.bNOR);
            this.gbKeyboard.Controls.Add(this.bXNOR);
            this.gbKeyboard.Controls.Add(this.bREIMPL);
            this.gbKeyboard.Controls.Add(this.bIMPL);
            this.gbKeyboard.Controls.Add(this.bXOR);
            this.gbKeyboard.Controls.Add(this.bNOT);
            this.gbKeyboard.Controls.Add(this.bAND);
            this.gbKeyboard.Controls.Add(this.bOR);
            this.gbKeyboard.Controls.Add(this.panel1);
            this.gbKeyboard.Controls.Add(this.panel2);
            this.gbKeyboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbKeyboard.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gbKeyboard.Location = new System.Drawing.Point(3, 3);
            this.gbKeyboard.Name = "gbKeyboard";
            this.gbKeyboard.Size = new System.Drawing.Size(298, 144);
            this.gbKeyboard.TabIndex = 0;
            this.gbKeyboard.TabStop = false;
            this.gbKeyboard.Text = "Keyboard";
            // 
            // pOperands
            // 
            this.pOperands.BackColor = System.Drawing.SystemColors.Control;
            this.pOperands.Controls.Add(this.bX3);
            this.pOperands.Controls.Add(this.bBOperand);
            this.pOperands.Controls.Add(this.bX2);
            this.pOperands.Controls.Add(this.bAOperand);
            this.pOperands.Controls.Add(this.bX1);
            this.pOperands.Controls.Add(this.bOne);
            this.pOperands.Controls.Add(this.bZero);
            this.pOperands.Controls.Add(this.bCOperand);
            this.pOperands.Controls.Add(this.bX5);
            this.pOperands.Controls.Add(this.bX4);
            this.pOperands.Location = new System.Drawing.Point(88, 58);
            this.pOperands.Name = "pOperands";
            this.pOperands.Size = new System.Drawing.Size(199, 68);
            this.pOperands.TabIndex = 17;
            // 
            // bX3
            // 
            this.bX3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bX3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bX3.Location = new System.Drawing.Point(82, 37);
            this.bX3.Name = "bX3";
            this.bX3.Size = new System.Drawing.Size(35, 30);
            this.bX3.TabIndex = 18;
            this.bX3.Text = "X3";
            this.bX3.UseVisualStyleBackColor = true;
            this.bX3.Click += new System.EventHandler(this.bX3_Click);
            // 
            // bBOperand
            // 
            this.bBOperand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bBOperand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bBOperand.Location = new System.Drawing.Point(122, 2);
            this.bBOperand.Name = "bBOperand";
            this.bBOperand.Size = new System.Drawing.Size(35, 30);
            this.bBOperand.TabIndex = 14;
            this.bBOperand.Text = "B";
            this.bBOperand.UseVisualStyleBackColor = true;
            this.bBOperand.Click += new System.EventHandler(this.bBOperand_Click);
            // 
            // bX2
            // 
            this.bX2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bX2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bX2.Location = new System.Drawing.Point(42, 37);
            this.bX2.Name = "bX2";
            this.bX2.Size = new System.Drawing.Size(35, 30);
            this.bX2.TabIndex = 17;
            this.bX2.Text = "X2";
            this.bX2.UseVisualStyleBackColor = true;
            this.bX2.Click += new System.EventHandler(this.bX2_Click);
            // 
            // bAOperand
            // 
            this.bAOperand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAOperand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bAOperand.Location = new System.Drawing.Point(82, 2);
            this.bAOperand.Name = "bAOperand";
            this.bAOperand.Size = new System.Drawing.Size(35, 30);
            this.bAOperand.TabIndex = 13;
            this.bAOperand.Text = "A";
            this.bAOperand.UseVisualStyleBackColor = true;
            this.bAOperand.Click += new System.EventHandler(this.bAOperand_Click);
            // 
            // bX1
            // 
            this.bX1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bX1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bX1.Location = new System.Drawing.Point(2, 37);
            this.bX1.Name = "bX1";
            this.bX1.Size = new System.Drawing.Size(35, 30);
            this.bX1.TabIndex = 16;
            this.bX1.Text = "X1";
            this.bX1.UseVisualStyleBackColor = true;
            this.bX1.Click += new System.EventHandler(this.bX1_Click);
            // 
            // bOne
            // 
            this.bOne.BackColor = System.Drawing.SystemColors.Control;
            this.bOne.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOne.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bOne.Location = new System.Drawing.Point(42, 2);
            this.bOne.Name = "bOne";
            this.bOne.Size = new System.Drawing.Size(35, 30);
            this.bOne.TabIndex = 12;
            this.bOne.Text = "1";
            this.bOne.UseVisualStyleBackColor = false;
            this.bOne.Click += new System.EventHandler(this.bOne_Click);
            // 
            // bZero
            // 
            this.bZero.BackColor = System.Drawing.SystemColors.Control;
            this.bZero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bZero.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bZero.ForeColor = System.Drawing.SystemColors.ControlText;
            this.bZero.Location = new System.Drawing.Point(2, 2);
            this.bZero.Name = "bZero";
            this.bZero.Size = new System.Drawing.Size(35, 30);
            this.bZero.TabIndex = 11;
            this.bZero.Text = "0";
            this.bZero.UseVisualStyleBackColor = false;
            this.bZero.Click += new System.EventHandler(this.bZero_Click);
            // 
            // bCOperand
            // 
            this.bCOperand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCOperand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bCOperand.Location = new System.Drawing.Point(162, 2);
            this.bCOperand.Name = "bCOperand";
            this.bCOperand.Size = new System.Drawing.Size(35, 30);
            this.bCOperand.TabIndex = 15;
            this.bCOperand.Text = "C";
            this.bCOperand.UseVisualStyleBackColor = true;
            this.bCOperand.Click += new System.EventHandler(this.bCOperand_Click);
            // 
            // bX5
            // 
            this.bX5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bX5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bX5.Location = new System.Drawing.Point(162, 37);
            this.bX5.Name = "bX5";
            this.bX5.Size = new System.Drawing.Size(35, 30);
            this.bX5.TabIndex = 20;
            this.bX5.Text = "X5";
            this.bX5.UseVisualStyleBackColor = true;
            this.bX5.Click += new System.EventHandler(this.bX5_Click);
            // 
            // bX4
            // 
            this.bX4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bX4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bX4.Location = new System.Drawing.Point(122, 37);
            this.bX4.Name = "bX4";
            this.bX4.Size = new System.Drawing.Size(35, 30);
            this.bX4.TabIndex = 19;
            this.bX4.Text = "X4";
            this.bX4.UseVisualStyleBackColor = true;
            this.bX4.Click += new System.EventHandler(this.bX4_Click);
            // 
            // bCloseBrace
            // 
            this.bCloseBrace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCloseBrace.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bCloseBrace.Location = new System.Drawing.Point(50, 95);
            this.bCloseBrace.Name = "bCloseBrace";
            this.bCloseBrace.Size = new System.Drawing.Size(35, 30);
            this.bCloseBrace.TabIndex = 10;
            this.bCloseBrace.Text = ")";
            this.bCloseBrace.UseVisualStyleBackColor = true;
            this.bCloseBrace.Click += new System.EventHandler(this.bCloseBrace_Click);
            // 
            // bOpenBrace
            // 
            this.bOpenBrace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOpenBrace.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bOpenBrace.Location = new System.Drawing.Point(10, 95);
            this.bOpenBrace.Name = "bOpenBrace";
            this.bOpenBrace.Size = new System.Drawing.Size(35, 30);
            this.bOpenBrace.TabIndex = 9;
            this.bOpenBrace.Text = "(";
            this.bOpenBrace.UseVisualStyleBackColor = true;
            this.bOpenBrace.Click += new System.EventHandler(this.bOpenBrace_Click);
            // 
            // bNAND
            // 
            this.bNAND.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNAND.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bNAND.Location = new System.Drawing.Point(50, 60);
            this.bNAND.Name = "bNAND";
            this.bNAND.Size = new System.Drawing.Size(35, 30);
            this.bNAND.TabIndex = 8;
            this.bNAND.Text = "|";
            this.bNAND.UseVisualStyleBackColor = true;
            this.bNAND.Click += new System.EventHandler(this.bNAND_Click);
            // 
            // bNOR
            // 
            this.bNOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNOR.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bNOR.Location = new System.Drawing.Point(10, 60);
            this.bNOR.Name = "bNOR";
            this.bNOR.Size = new System.Drawing.Size(35, 30);
            this.bNOR.TabIndex = 7;
            this.bNOR.Text = "↓";
            this.bNOR.UseVisualStyleBackColor = true;
            this.bNOR.Click += new System.EventHandler(this.bNOR_Click);
            // 
            // bXNOR
            // 
            this.bXNOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bXNOR.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bXNOR.Location = new System.Drawing.Point(250, 25);
            this.bXNOR.Name = "bXNOR";
            this.bXNOR.Size = new System.Drawing.Size(35, 30);
            this.bXNOR.TabIndex = 6;
            this.bXNOR.Text = "↔";
            this.bXNOR.UseVisualStyleBackColor = true;
            this.bXNOR.Click += new System.EventHandler(this.bXNOR_Click);
            // 
            // bREIMPL
            // 
            this.bREIMPL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bREIMPL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bREIMPL.Location = new System.Drawing.Point(210, 25);
            this.bREIMPL.Name = "bREIMPL";
            this.bREIMPL.Size = new System.Drawing.Size(35, 30);
            this.bREIMPL.TabIndex = 5;
            this.bREIMPL.Text = "←";
            this.bREIMPL.UseVisualStyleBackColor = true;
            this.bREIMPL.Click += new System.EventHandler(this.bREIMPL_Click);
            // 
            // bIMPL
            // 
            this.bIMPL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bIMPL.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bIMPL.Location = new System.Drawing.Point(170, 25);
            this.bIMPL.Name = "bIMPL";
            this.bIMPL.Size = new System.Drawing.Size(35, 30);
            this.bIMPL.TabIndex = 4;
            this.bIMPL.Text = "→";
            this.bIMPL.UseVisualStyleBackColor = true;
            this.bIMPL.Click += new System.EventHandler(this.bIMPL_Click);
            // 
            // bXOR
            // 
            this.bXOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bXOR.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bXOR.Location = new System.Drawing.Point(130, 25);
            this.bXOR.Name = "bXOR";
            this.bXOR.Size = new System.Drawing.Size(35, 30);
            this.bXOR.TabIndex = 3;
            this.bXOR.Text = "⊕";
            this.bXOR.UseVisualStyleBackColor = true;
            this.bXOR.Click += new System.EventHandler(this.bXOR_Click);
            // 
            // bNOT
            // 
            this.bNOT.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bNOT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bNOT.Location = new System.Drawing.Point(10, 25);
            this.bNOT.Name = "bNOT";
            this.bNOT.Size = new System.Drawing.Size(35, 30);
            this.bNOT.TabIndex = 0;
            this.bNOT.Text = "¬";
            this.bNOT.UseVisualStyleBackColor = true;
            this.bNOT.Click += new System.EventHandler(this.bNOT_Click);
            // 
            // bAND
            // 
            this.bAND.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAND.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bAND.Location = new System.Drawing.Point(90, 25);
            this.bAND.Name = "bAND";
            this.bAND.Size = new System.Drawing.Size(35, 30);
            this.bAND.TabIndex = 2;
            this.bAND.Text = "∧";
            this.bAND.UseVisualStyleBackColor = true;
            this.bAND.Click += new System.EventHandler(this.bAND_Click);
            // 
            // bOR
            // 
            this.bOR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bOR.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bOR.Location = new System.Drawing.Point(50, 25);
            this.bOR.Name = "bOR";
            this.bOR.Size = new System.Drawing.Size(35, 30);
            this.bOR.TabIndex = 1;
            this.bOR.Text = "∨";
            this.bOR.UseVisualStyleBackColor = true;
            this.bOR.Click += new System.EventHandler(this.bOR_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Location = new System.Drawing.Point(8, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(79, 104);
            this.panel1.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Location = new System.Drawing.Point(87, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 35);
            this.panel2.TabIndex = 22;
            // 
            // gbOptions
            // 
            this.gbOptions.Controls.Add(this.pDNF);
            this.gbOptions.Controls.Add(this.pClassification);
            this.gbOptions.Controls.Add(this.pPolinomial);
            this.gbOptions.Controls.Add(this.pCNF);
            this.gbOptions.Controls.Add(this.pPDNF);
            this.gbOptions.Controls.Add(this.pPCNF);
            this.gbOptions.Controls.Add(this.pTruthTable);
            this.gbOptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbOptions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.gbOptions.Location = new System.Drawing.Point(3, 153);
            this.gbOptions.Name = "gbOptions";
            this.gbOptions.Size = new System.Drawing.Size(298, 264);
            this.gbOptions.TabIndex = 1;
            this.gbOptions.TabStop = false;
            this.gbOptions.Text = "Options";
            // 
            // pDNF
            // 
            this.pDNF.Controls.Add(this.cbDNF);
            this.pDNF.Location = new System.Drawing.Point(149, 111);
            this.pDNF.Name = "pDNF";
            this.pDNF.Size = new System.Drawing.Size(146, 45);
            this.pDNF.TabIndex = 6;
            // 
            // cbDNF
            // 
            this.cbDNF.AutoSize = true;
            this.cbDNF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbDNF.Location = new System.Drawing.Point(10, 10);
            this.cbDNF.Name = "cbDNF";
            this.cbDNF.Size = new System.Drawing.Size(63, 25);
            this.cbDNF.TabIndex = 4;
            this.cbDNF.Text = "ДНФ";
            this.cbDNF.UseVisualStyleBackColor = true;
            // 
            // pClassification
            // 
            this.pClassification.Controls.Add(this.cbClassification);
            this.pClassification.Location = new System.Drawing.Point(3, 201);
            this.pClassification.Name = "pClassification";
            this.pClassification.Size = new System.Drawing.Size(292, 45);
            this.pClassification.TabIndex = 5;
            // 
            // cbClassification
            // 
            this.cbClassification.AutoSize = true;
            this.cbClassification.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbClassification.Location = new System.Drawing.Point(10, 10);
            this.cbClassification.Name = "cbClassification";
            this.cbClassification.Size = new System.Drawing.Size(186, 25);
            this.cbClassification.TabIndex = 6;
            this.cbClassification.Text = "Классификация Поста";
            this.cbClassification.UseVisualStyleBackColor = true;
            // 
            // pPolinomial
            // 
            this.pPolinomial.Controls.Add(this.cbPolinomial);
            this.pPolinomial.Location = new System.Drawing.Point(3, 156);
            this.pPolinomial.Name = "pPolinomial";
            this.pPolinomial.Size = new System.Drawing.Size(292, 45);
            this.pPolinomial.TabIndex = 4;
            // 
            // cbPolinomial
            // 
            this.cbPolinomial.AutoSize = true;
            this.cbPolinomial.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbPolinomial.Location = new System.Drawing.Point(10, 10);
            this.cbPolinomial.Name = "cbPolinomial";
            this.cbPolinomial.Size = new System.Drawing.Size(177, 25);
            this.cbPolinomial.TabIndex = 5;
            this.cbPolinomial.Text = "Полином Жегалкина";
            this.cbPolinomial.UseVisualStyleBackColor = true;
            // 
            // pCNF
            // 
            this.pCNF.Controls.Add(this.cbCNF);
            this.pCNF.Location = new System.Drawing.Point(3, 111);
            this.pCNF.Name = "pCNF";
            this.pCNF.Size = new System.Drawing.Size(146, 45);
            this.pCNF.TabIndex = 3;
            // 
            // cbCNF
            // 
            this.cbCNF.AutoSize = true;
            this.cbCNF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbCNF.Location = new System.Drawing.Point(10, 10);
            this.cbCNF.Name = "cbCNF";
            this.cbCNF.Size = new System.Drawing.Size(61, 25);
            this.cbCNF.TabIndex = 3;
            this.cbCNF.Text = "КНФ";
            this.cbCNF.UseVisualStyleBackColor = true;
            // 
            // pPDNF
            // 
            this.pPDNF.Controls.Add(this.cbPDNF);
            this.pPDNF.Location = new System.Drawing.Point(149, 66);
            this.pPDNF.Name = "pPDNF";
            this.pPDNF.Size = new System.Drawing.Size(146, 45);
            this.pPDNF.TabIndex = 2;
            // 
            // cbPDNF
            // 
            this.cbPDNF.AutoSize = true;
            this.cbPDNF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbPDNF.Location = new System.Drawing.Point(10, 10);
            this.cbPDNF.Name = "cbPDNF";
            this.cbPDNF.Size = new System.Drawing.Size(73, 25);
            this.cbPDNF.TabIndex = 2;
            this.cbPDNF.Text = "СДНФ";
            this.cbPDNF.UseVisualStyleBackColor = true;
            // 
            // pPCNF
            // 
            this.pPCNF.Controls.Add(this.cbPCNF);
            this.pPCNF.Location = new System.Drawing.Point(3, 66);
            this.pPCNF.Name = "pPCNF";
            this.pPCNF.Size = new System.Drawing.Size(146, 45);
            this.pPCNF.TabIndex = 1;
            // 
            // cbPCNF
            // 
            this.cbPCNF.AutoSize = true;
            this.cbPCNF.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbPCNF.Location = new System.Drawing.Point(10, 10);
            this.cbPCNF.Name = "cbPCNF";
            this.cbPCNF.Size = new System.Drawing.Size(71, 25);
            this.cbPCNF.TabIndex = 1;
            this.cbPCNF.Text = "СКНФ";
            this.cbPCNF.UseVisualStyleBackColor = true;
            // 
            // pTruthTable
            // 
            this.pTruthTable.Controls.Add(this.cbTruthTable);
            this.pTruthTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.pTruthTable.Location = new System.Drawing.Point(3, 21);
            this.pTruthTable.Name = "pTruthTable";
            this.pTruthTable.Size = new System.Drawing.Size(292, 45);
            this.pTruthTable.TabIndex = 0;
            // 
            // cbTruthTable
            // 
            this.cbTruthTable.AutoSize = true;
            this.cbTruthTable.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cbTruthTable.Location = new System.Drawing.Point(10, 10);
            this.cbTruthTable.Name = "cbTruthTable";
            this.cbTruthTable.Size = new System.Drawing.Size(175, 25);
            this.cbTruthTable.TabIndex = 0;
            this.cbTruthTable.Text = "Таблица истинности";
            this.cbTruthTable.UseVisualStyleBackColor = true;
            // 
            // tlpOutput
            // 
            this.tlpOutput.AutoSize = true;
            this.tlpOutput.ColumnCount = 1;
            this.tlpOutput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOutput.Controls.Add(this.pInputField, 0, 0);
            this.tlpOutput.Controls.Add(this.pOutput, 0, 1);
            this.tlpOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpOutput.Location = new System.Drawing.Point(313, 3);
            this.tlpOutput.Name = "tlpOutput";
            this.tlpOutput.RowCount = 2;
            this.tlpOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tlpOutput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpOutput.Size = new System.Drawing.Size(668, 555);
            this.tlpOutput.TabIndex = 2;
            // 
            // pInputField
            // 
            this.pInputField.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pInputField.BackColor = System.Drawing.Color.Gray;
            this.pInputField.Controls.Add(this.bCalculate);
            this.pInputField.Controls.Add(this.lbInstruction);
            this.pInputField.Controls.Add(this.tbInputField);
            this.pInputField.Location = new System.Drawing.Point(3, 3);
            this.pInputField.Name = "pInputField";
            this.pInputField.Size = new System.Drawing.Size(662, 144);
            this.pInputField.TabIndex = 3;
            // 
            // bCalculate
            // 
            this.bCalculate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.bCalculate.Location = new System.Drawing.Point(9, 100);
            this.bCalculate.Name = "bCalculate";
            this.bCalculate.Size = new System.Drawing.Size(175, 30);
            this.bCalculate.TabIndex = 2;
            this.bCalculate.Text = "Рассчитать";
            this.bCalculate.UseVisualStyleBackColor = true;
            this.bCalculate.Click += new System.EventHandler(this.bCalculate_Click);
            // 
            // lbInstruction
            // 
            this.lbInstruction.AutoSize = true;
            this.lbInstruction.Location = new System.Drawing.Point(9, 28);
            this.lbInstruction.Name = "lbInstruction";
            this.lbInstruction.Size = new System.Drawing.Size(320, 17);
            this.lbInstruction.TabIndex = 1;
            this.lbInstruction.Text = "Введите логическую функцию, её вектор или номер";
            // 
            // tbInputField
            // 
            this.tbInputField.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbInputField.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tbInputField.Location = new System.Drawing.Point(10, 60);
            this.tbInputField.Name = "tbInputField";
            this.tbInputField.Size = new System.Drawing.Size(642, 33);
            this.tbInputField.TabIndex = 0;
            // 
            // pOutput
            // 
            this.pOutput.AutoScroll = true;
            this.pOutput.BackColor = System.Drawing.SystemColors.Control;
            this.pOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pOutput.Location = new System.Drawing.Point(3, 153);
            this.pOutput.Name = "pOutput";
            this.pOutput.Size = new System.Drawing.Size(662, 399);
            this.pOutput.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tlpMainField);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mathematical Logic";
            this.tlpMainField.ResumeLayout(false);
            this.tlpMainField.PerformLayout();
            this.tlpOptions.ResumeLayout(false);
            this.gbKeyboard.ResumeLayout(false);
            this.pOperands.ResumeLayout(false);
            this.gbOptions.ResumeLayout(false);
            this.pDNF.ResumeLayout(false);
            this.pDNF.PerformLayout();
            this.pClassification.ResumeLayout(false);
            this.pClassification.PerformLayout();
            this.pPolinomial.ResumeLayout(false);
            this.pPolinomial.PerformLayout();
            this.pCNF.ResumeLayout(false);
            this.pCNF.PerformLayout();
            this.pPDNF.ResumeLayout(false);
            this.pPDNF.PerformLayout();
            this.pPCNF.ResumeLayout(false);
            this.pPCNF.PerformLayout();
            this.pTruthTable.ResumeLayout(false);
            this.pTruthTable.PerformLayout();
            this.tlpOutput.ResumeLayout(false);
            this.pInputField.ResumeLayout(false);
            this.pInputField.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMainField;
        private System.Windows.Forms.TableLayoutPanel tlpOptions;
        private System.Windows.Forms.GroupBox gbKeyboard;
        private System.Windows.Forms.GroupBox gbOptions;
        private System.Windows.Forms.Button bOR;
        private System.Windows.Forms.Button bREIMPL;
        private System.Windows.Forms.Button bIMPL;
        private System.Windows.Forms.Button bXOR;
        private System.Windows.Forms.Button bNOT;
        private System.Windows.Forms.Button bAND;
        private System.Windows.Forms.Button bNAND;
        private System.Windows.Forms.Button bNOR;
        private System.Windows.Forms.Button bXNOR;
        private System.Windows.Forms.Button bCloseBrace;
        private System.Windows.Forms.Button bOpenBrace;
        private System.Windows.Forms.Panel pTruthTable;
        private System.Windows.Forms.Panel pPCNF;
        private System.Windows.Forms.Panel pPDNF;
        private System.Windows.Forms.Panel pCNF;
        private System.Windows.Forms.Panel pDNF;
        private System.Windows.Forms.Panel pPolinomial;
        private System.Windows.Forms.Panel pClassification;
        private System.Windows.Forms.Panel pOperands;
        private System.Windows.Forms.Button bX3;
        private System.Windows.Forms.Button bBOperand;
        private System.Windows.Forms.Button bX2;
        private System.Windows.Forms.Button bAOperand;
        private System.Windows.Forms.Button bX1;
        private System.Windows.Forms.Button bOne;
        private System.Windows.Forms.Button bZero;
        private System.Windows.Forms.Button bCOperand;
        private System.Windows.Forms.Button bX5;
        private System.Windows.Forms.Button bX4;
        private System.Windows.Forms.CheckBox cbTruthTable;
        private System.Windows.Forms.CheckBox cbPCNF;
        private System.Windows.Forms.CheckBox cbPDNF;
        private System.Windows.Forms.CheckBox cbCNF;
        private System.Windows.Forms.CheckBox cbDNF;
        private System.Windows.Forms.CheckBox cbClassification;
        private System.Windows.Forms.CheckBox cbPolinomial;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tlpOutput;
        private System.Windows.Forms.Panel pInputField;
        private System.Windows.Forms.Button bCalculate;
        private System.Windows.Forms.Label lbInstruction;
        private System.Windows.Forms.TextBox tbInputField;
        private System.Windows.Forms.Panel pOutput;
    }
}

