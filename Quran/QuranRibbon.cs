using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;

namespace Quran
{
    public partial class QuranRibbon
    {
        private void QuranRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button_OpenQuranTaskPane_Click(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TpMainValue.Visible = true;
        }
    }
}
