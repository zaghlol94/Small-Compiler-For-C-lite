namespace Scanner
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ScanText = new System.Windows.Forms.TextBox();
            this.ScanButton = new System.Windows.Forms.Button();
            this.TokenList = new System.Windows.Forms.ListBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.TypeList = new System.Windows.Forms.ListBox();
            this.ParseListBox = new System.Windows.Forms.ListBox();
            this.ParseButton = new System.Windows.Forms.Button();
            this.SemanticButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ScanText
            // 
            this.ScanText.Location = new System.Drawing.Point(55, 26);
            this.ScanText.Multiline = true;
            this.ScanText.Name = "ScanText";
            this.ScanText.Size = new System.Drawing.Size(780, 240);
            this.ScanText.TabIndex = 0;
            // 
            // ScanButton
            // 
            this.ScanButton.Location = new System.Drawing.Point(728, 294);
            this.ScanButton.Name = "ScanButton";
            this.ScanButton.Size = new System.Drawing.Size(107, 36);
            this.ScanButton.TabIndex = 1;
            this.ScanButton.Text = "Scan";
            this.ScanButton.UseVisualStyleBackColor = true;
            this.ScanButton.Click += new System.EventHandler(this.ScanButton_Click);
            // 
            // TokenList
            // 
            this.TokenList.FormattingEnabled = true;
            this.TokenList.ItemHeight = 16;
            this.TokenList.Location = new System.Drawing.Point(287, 294);
            this.TokenList.Name = "TokenList";
            this.TokenList.Size = new System.Drawing.Size(120, 84);
            this.TokenList.TabIndex = 2;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(891, 530);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(100, 24);
            this.maskedTextBox1.TabIndex = 3;
            // 
            // TypeList
            // 
            this.TypeList.FormattingEnabled = true;
            this.TypeList.ItemHeight = 16;
            this.TypeList.Location = new System.Drawing.Point(459, 294);
            this.TypeList.Name = "TypeList";
            this.TypeList.Size = new System.Drawing.Size(120, 84);
            this.TypeList.TabIndex = 4;
            // 
            // ParseListBox
            // 
            this.ParseListBox.FormattingEnabled = true;
            this.ParseListBox.ItemHeight = 16;
            this.ParseListBox.Location = new System.Drawing.Point(55, 410);
            this.ParseListBox.Name = "ParseListBox";
            this.ParseListBox.Size = new System.Drawing.Size(434, 84);
            this.ParseListBox.TabIndex = 5;
            // 
            // ParseButton
            // 
            this.ParseButton.Location = new System.Drawing.Point(728, 352);
            this.ParseButton.Name = "ParseButton";
            this.ParseButton.Size = new System.Drawing.Size(107, 36);
            this.ParseButton.TabIndex = 6;
            this.ParseButton.Text = "Parse";
            this.ParseButton.UseVisualStyleBackColor = true;
            this.ParseButton.Click += new System.EventHandler(this.ParseButton_Click);
            // 
            // SemanticButton
            // 
            this.SemanticButton.Location = new System.Drawing.Point(728, 410);
            this.SemanticButton.Name = "SemanticButton";
            this.SemanticButton.Size = new System.Drawing.Size(107, 36);
            this.SemanticButton.TabIndex = 7;
            this.SemanticButton.Text = "Semantic";
            this.SemanticButton.UseVisualStyleBackColor = true;
            this.SemanticButton.Click += new System.EventHandler(this.SemanticButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 630);
            this.Controls.Add(this.SemanticButton);
            this.Controls.Add(this.ParseButton);
            this.Controls.Add(this.ParseListBox);
            this.Controls.Add(this.TypeList);
            this.Controls.Add(this.maskedTextBox1);
            this.Controls.Add(this.TokenList);
            this.Controls.Add(this.ScanButton);
            this.Controls.Add(this.ScanText);
            this.Name = "Form1";
            this.Text = "Scanner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ScanText;
        private System.Windows.Forms.Button ScanButton;
        private System.Windows.Forms.ListBox TokenList;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.ListBox TypeList;
        private System.Windows.Forms.ListBox ParseListBox;
        private System.Windows.Forms.Button ParseButton;
        private System.Windows.Forms.Button SemanticButton;
    }
}

