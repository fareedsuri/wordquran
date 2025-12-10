using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;
using System.Data;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;

namespace Quran
{
	public partial class QuranUserControl : UserControl
	{
		private XElement Q;
		// PDMS Saleem QuranFont Regular
		private const string QURAN_FONT_NAME = "_PDMS_Saleem_QuranFont";
		
		public QuranUserControl()
		{
			InitializeComponent();
		}
		private void button_MakeTable_Click(object sender, EventArgs e)
		{
			Word._Application application = Globals.ThisAddIn.Application;
			Word.Document document = Globals.ThisAddIn.Application.ActiveDocument;
			int surahNumber = Int32.Parse(this.comboBox_SurahSelector.Text);
			string bismillah = Q.Elements("chapter").ElementAt(0).
				Elements("verse").ElementAt(0).Attribute("text").Value;
			XElement chp = Q.Elements("chapter").ElementAt(surahNumber);
			document.PageSetup.BottomMargin = 20; // 72 points = 1 inch
			document.PageSetup.TopMargin = 20; // 72 points = 1 inch
			document.PageSetup.LeftMargin = 20; // 72 points = 1 inch
			document.PageSetup.RightMargin = 20; // 72 points = 1 inch
			document.PageSetup.PageWidth = 72 * 4;
			document.PageSetup.PageHeight = 72 * 6;
			
			//Set the range to the top of the document.
			Word.Range tableLocation = document.Range(0, 0);
			document.Tables.Add(tableLocation, chp.Elements("verse").Count() + 1, 3); // rows, columns

			Word.Table newTable = document.Tables[1];
			document.Tables[1].Columns[1].SetWidth(Convert.ToSingle(0.75 * 72), Word.WdRulerStyle.wdAdjustNone);
			document.Tables[1].Columns[3].SetWidth(Convert.ToSingle(0.75 * 72), Word.WdRulerStyle.wdAdjustNone);
			document.Tables[1].Columns[2].SetWidth(Convert.ToSingle( 2 * 72), Word.WdRulerStyle.wdAdjustNone);
			
			// Apply PDMS Saleem QuranFont to entire table
			newTable.Range.Font.Name = QURAN_FONT_NAME;
		}

		private string DetectQuranFontName(Word.Document document)
		{
			// Try different possible font names
			string[] possibleNames = {
				"_PDMS_Saleem_QuranFont",
				"PDMS_Saleem_QuranFont",
				"PDMS Saleem QuranFont",
				"_PDMS_Saleem_QuranFont Regular"
			};

			Word.Range testRange = document.Range(0, 0);
			
			foreach (string fontName in possibleNames)
			{
				testRange.Font.Name = fontName;
				if (testRange.Font.Name == fontName)
				{
					System.Diagnostics.Debug.WriteLine($"Found font: {fontName}");
					return fontName;
				}
			}
			
			return null; // Font not found
		}

		private void button_RunCode_Click_1(object sender, EventArgs e)
		{
			Word._Application application = Globals.ThisAddIn.Application;
			Word.Document document = Globals.ThisAddIn.Application.ActiveDocument;
			int surahNumber = Int32.Parse(this.comboBox_SurahSelector.Text);
			string bismillah = Q.Elements("chapter").ElementAt(0).
				Elements("verse").ElementAt(0).Attribute("text").Value;
			XElement chp = Q.Elements("chapter").ElementAt(surahNumber);

			Word.Table newTable = document.Tables[1];
			newTable.Cell(1, 2).Range.Text = bismillah;
			newTable.Cell(1, 2).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
			int verseCounter = 2;
			string aya;
			int verseNumber = 1; // Track the verse number

			// Test font availability and detect correct font name
			string detectedFontName = DetectQuranFontName(document);
			if (detectedFontName == null)
			{
				MessageBox.Show($"Warning: PDMS Saleem QuranFont not found!\n\nPlease verify:\n" +
					$"1. Font file '_PDMS_Saleem_QuranFont Regular.ttf' is installed\n" +
					$"2. Word has been restarted after installation\n" +
					$"3. Font appears in Word's font list\n\n" +
					$"Check Windows Settings → Fonts to confirm installation.",
					"Font Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return; // Exit early
			}

			// Local functions defined after detectedFontName
			string buildText(IEnumerable<XElement> token, int from, int to)
			{
				string ayaText = "";
				for (int cellNumber = from; cellNumber <= to; cellNumber++)
				{
					ayaText += token.ElementAt(cellNumber).Attribute("text").Value + " ";
				}
				return ayaText;
			}

			void enterTextInCell(string text, int columnNumber, int fontSize) 
			{
				newTable.Cell(verseCounter, columnNumber).Range.Text = text;
				newTable.Cell(verseCounter, columnNumber).Range.Font.Size = fontSize;
				newTable.Cell(verseCounter, columnNumber).Range.Font.Name = detectedFontName;
			}

			foreach (XElement verse in chp.Elements("verse"))
			{
				int tokensCount = verse.Elements("tokens").First().Elements("token").Count();
				IEnumerable<XElement> token = verse.Elements("tokens").First().Elements("token");
				
				switch (tokensCount)
				{
					case 1:
						newTable.Cell(verseCounter, 3).Range.Text = verse.Attribute("text").Value;
						newTable.Cell(verseCounter, 3).Range.Font.Size = 9;
						newTable.Cell(verseCounter, 3).Range.Font.Name = detectedFontName;
						break;
					case 2:
						// first column -- right 
						aya = buildText(token, 0, 0);
						enterTextInCell(aya, 3, 9);

						// third column -- left
						aya = buildText(token, 1, 1);
						enterTextInCell(aya, 1, 9);
						break;
					case 3:
						// first column -- right 
						aya = buildText(token, 0,1);
						enterTextInCell(aya, 3, 9);

						// third column -- left
						aya = buildText(token, 2, 2);
						enterTextInCell(aya, 1, 9);
						break;
					case 4:
						// first column -- right 
						aya = buildText(token, 0, 1);
						enterTextInCell(aya, 3, 9);

						// third column -- left
						aya = buildText(token, 2, 3);
						enterTextInCell(aya, 1, 9);
						break;
					default:
						// first column -- right 
						aya = buildText(token, 0, 1);
						enterTextInCell(aya, 3, 9);

						// second column -- middle
						aya = buildText(token, 2, tokensCount - 3);
						enterTextInCell(aya, 2, 5);

						// third column -- left
						aya = buildText(token, tokensCount - 2, tokensCount - 1);
						enterTextInCell(aya, 1, 9);
						break;
				}
				verseCounter += 1;
				verseNumber += 1;
			}
		}

		private void QuranUserControl_Load(object sender, EventArgs e)
		{
			 Q = XElement.Load(@"C:\Data\SynologyPrefUnsync\Projects\VisualStudio\QuranCorpus\QuranCorpus\data\ProcessedXML\Grammar2.xml");
			for (int i = 0; i < 114; i++)
			{
				this.comboBox_SurahSelector.Items.Add(i.ToString());
			}
		}


	}
}