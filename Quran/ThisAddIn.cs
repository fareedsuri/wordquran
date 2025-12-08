using Microsoft.Office.Tools;

namespace Quran
{
    public partial class ThisAddIn
    {
        private QuranUserControl _tpMain;

        public CustomTaskPane TpMainValue { get; set; }

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            _tpMain = new QuranUserControl();
            TpMainValue = this.CustomTaskPanes.Add(_tpMain, "Quran App");
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
