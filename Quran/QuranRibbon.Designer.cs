namespace Quran
{
    partial class QuranRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public QuranRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

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
            this.tab1 = this.Factory.CreateRibbonTab();
            this.Quran = this.Factory.CreateRibbonGroup();
            this.button_OpenQuranTaskPane = this.Factory.CreateRibbonButton();
            this.tab1.SuspendLayout();
            this.Quran.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab1
            // 
            this.tab1.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.tab1.Groups.Add(this.Quran);
            this.tab1.Label = "TabAddIns";
            this.tab1.Name = "tab1";
            // 
            // Quran
            // 
            this.Quran.Items.Add(this.button_OpenQuranTaskPane);
            this.Quran.Label = "Quran App";
            this.Quran.Name = "Quran";
            // 
            // button_OpenQuranTaskPane
            // 
            this.button_OpenQuranTaskPane.Label = "Start Quran App";
            this.button_OpenQuranTaskPane.Name = "button_OpenQuranTaskPane";
            this.button_OpenQuranTaskPane.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button_OpenQuranTaskPane_Click);
            // 
            // QuranRibbon
            // 
            this.Name = "QuranRibbon";
            this.RibbonType = "Microsoft.Word.Document";
            this.Tabs.Add(this.tab1);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.QuranRibbon_Load);
            this.tab1.ResumeLayout(false);
            this.tab1.PerformLayout();
            this.Quran.ResumeLayout(false);
            this.Quran.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab tab1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Quran;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button_OpenQuranTaskPane;
    }

    partial class ThisRibbonCollection
    {
        internal QuranRibbon QuranRibbon
        {
            get { return this.GetRibbon<QuranRibbon>(); }
        }
    }
}
