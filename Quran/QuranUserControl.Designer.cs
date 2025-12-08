namespace Quran
{
    partial class QuranUserControl
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
            this.button_FillTable = new System.Windows.Forms.Button();
            this.comboBox_SurahSelector = new System.Windows.Forms.ComboBox();
            this.button_MakeTable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_FillTable
            // 
            this.button_FillTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_FillTable.Location = new System.Drawing.Point(33, 173);
            this.button_FillTable.Name = "button_FillTable";
            this.button_FillTable.Size = new System.Drawing.Size(204, 55);
            this.button_FillTable.TabIndex = 0;
            this.button_FillTable.Text = "Fill Table";
            this.button_FillTable.UseVisualStyleBackColor = true;
            this.button_FillTable.Click += new System.EventHandler(this.button_RunCode_Click_1);
            // 
            // comboBox_SurahSelector
            // 
            this.comboBox_SurahSelector.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox_SurahSelector.FormattingEnabled = true;
            this.comboBox_SurahSelector.Location = new System.Drawing.Point(118, 36);
            this.comboBox_SurahSelector.Name = "comboBox_SurahSelector";
            this.comboBox_SurahSelector.Size = new System.Drawing.Size(119, 39);
            this.comboBox_SurahSelector.TabIndex = 1;
            // 
            // button_MakeTable
            // 
            this.button_MakeTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_MakeTable.Location = new System.Drawing.Point(33, 95);
            this.button_MakeTable.Name = "button_MakeTable";
            this.button_MakeTable.Size = new System.Drawing.Size(204, 55);
            this.button_MakeTable.TabIndex = 2;
            this.button_MakeTable.Text = "Make Table";
            this.button_MakeTable.UseVisualStyleBackColor = true;
            this.button_MakeTable.Click += new System.EventHandler(this.button_MakeTable_Click);
            // 
            // QuranUserControl
            // 
            this.Controls.Add(this.button_MakeTable);
            this.Controls.Add(this.comboBox_SurahSelector);
            this.Controls.Add(this.button_FillTable);
            this.Name = "QuranUserControl";
            this.Size = new System.Drawing.Size(290, 412);
            this.Load += new System.EventHandler(this.QuranUserControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_FillTable;
        private System.Windows.Forms.ComboBox comboBox_SurahSelector;
        private System.Windows.Forms.Button button_MakeTable;
    }
}
