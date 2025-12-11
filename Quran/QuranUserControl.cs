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
			
			// Adjust margins
			document.PageSetup.BottomMargin = 20; // 72 points = 1 inch
			document.PageSetup.TopMargin = 20; // 72 points = 1 inch
			document.PageSetup.LeftMargin = Convert.ToSingle(0.18 * 72); // 0.18 inches
			document.PageSetup.RightMargin = Convert.ToSingle(0.2 * 72); // 0.2 inches
			document.PageSetup.PageWidth = 72 * 4;
			document.PageSetup.PageHeight = 72 * 6;
			
			//Set the range to the top of the document.
			Word.Range tableLocation = document.Range(0, 0);
			// Now 4 columns: verse number + 3 text columns
			document.Tables.Add(tableLocation, chp.Elements("verse").Count() + 1, 4);

			Word.Table newTable = document.Tables[1];
			
			// Set table cell padding to minimal
			newTable.LeftPadding = 1;
			newTable.RightPadding = 1;
			newTable.TopPadding = 0;
			newTable.BottomPadding = 0;
			
			// Remove row height restrictions - allow rows to auto-fit content
			newTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightAuto;
			
			// Column 1: Verse number (0.2 inches = 14.4 points, minimum safe width)
			newTable.Columns[1].SetWidth(Convert.ToSingle(0.2 * 72), Word.WdRulerStyle.wdAdjustNone);
			
			// Column 2: Left text (0.72 inches)
			newTable.Columns[2].SetWidth(Convert.ToSingle(0.72 * 72), Word.WdRulerStyle.wdAdjustNone);
			
			// Column 3: Middle text (2.0 inches)
			newTable.Columns[3].SetWidth(Convert.ToSingle(2 * 72), Word.WdRulerStyle.wdAdjustNone);
			
			// Column 4: Right text (0.68 inches)
			newTable.Columns[4].SetWidth(Convert.ToSingle(0.68 * 72), Word.WdRulerStyle.wdAdjustNone);
			
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
			
			// Set bismillah in the middle column (now column 3)
			newTable.Cell(1, 3).Range.Text = bismillah;
			newTable.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
			
			int verseCounter = 2;
			string aya;
			int verseNumber = 1; // Track the verse number

			// Test font availability and detect correct font name
			string detectedFontName = DetectQuranFontName(document);
			if (detectedFontName == null)
			{
				MessageBox.Show($"Warning: PDMS_Saleem_QuranFont not found!\n\nPlease verify:\n" +
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
				
				// Set paragraph spacing to 0
				newTable.Cell(verseCounter, columnNumber).Range.ParagraphFormat.SpaceAfter = 0;
				newTable.Cell(verseCounter, columnNumber).Range.ParagraphFormat.SpaceBefore = 0;
				
				// Set line spacing to single (1.0)
				newTable.Cell(verseCounter, columnNumber).Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
				
				// Set alignment based on column
				if (columnNumber == 2)
				{
					// Column 2: Left align
					newTable.Cell(verseCounter, columnNumber).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
				}
				else if (columnNumber == 3)
				{
					// Column 3: Center align
					newTable.Cell(verseCounter, columnNumber).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
				}
				else if (columnNumber == 4)
				{
					// Column 4: Right align
					newTable.Cell(verseCounter, columnNumber).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
				}
			}

			void setVerseNumberCell(int verseNum)
			{
				// Column 1: Verse number
				newTable.Cell(verseCounter, 1).Range.Text = verseNum.ToString();
				newTable.Cell(verseCounter, 1).Range.Font.Size = 6;
				newTable.Cell(verseCounter, 1).Range.Font.Name = detectedFontName;
				newTable.Cell(verseCounter, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
				// Remove paragraph spacing
				newTable.Cell(verseCounter, 1).Range.ParagraphFormat.SpaceAfter = 0;
				newTable.Cell(verseCounter, 1).Range.ParagraphFormat.SpaceBefore = 0;
			}

			void applyAlternateRowShading()
			{
				// Apply light grey shading to alternate rows (even row numbers)
				if (verseCounter % 2 == 0)
				{
					// Light grey shading for even rows (RGB: 242, 242, 242)
					for (int col = 1; col <= 4; col++)
					{
						newTable.Cell(verseCounter, col).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
					}
				}
			}

			foreach (XElement verse in chp.Elements("verse"))
			{
				int tokensCount = verse.Elements("tokens").First().Elements("token").Count();
				IEnumerable<XElement> token = verse.Elements("tokens").First().Elements("token");
				
				// Set verse number in column 1
				setVerseNumberCell(verseNumber);
				
				switch (tokensCount)
				{
					case 1:
						// Column 4: Right text (full verse)
						newTable.Cell(verseCounter, 4).Range.Text = verse.Attribute("text").Value;
						newTable.Cell(verseCounter, 4).Range.Font.Size = 9;
						newTable.Cell(verseCounter, 4).Range.Font.Name = detectedFontName;
						newTable.Cell(verseCounter, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
						// Set paragraph and line spacing
						newTable.Cell(verseCounter, 4).Range.ParagraphFormat.SpaceAfter = 0;
						newTable.Cell(verseCounter, 4).Range.ParagraphFormat.SpaceBefore = 0;
						newTable.Cell(verseCounter, 4).Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
						break;
					case 2:
						// Column 4: Right text
						aya = buildText(token, 0, 0);
						enterTextInCell(aya, 4, 9);

						// Column 2: Left text
						aya = buildText(token, 1, 1);
						enterTextInCell(aya, 2, 9);
						break;
					case 3:
						// Column 4: Right text
						aya = buildText(token, 0, 1);
						enterTextInCell(aya, 4, 9);

						// Column 2: Left text
						aya = buildText(token, 2, 2);
						enterTextInCell(aya, 2, 9);
						break;
					case 4:
						// Column 4: Right text
						aya = buildText(token, 0, 1);
						enterTextInCell(aya, 4, 9);

						// Column 2: Left text
						aya = buildText(token, 2, 3);
						enterTextInCell(aya, 2, 9);
						break;
					default:
						// Column 4: Right text
						aya = buildText(token, 0, 1);
						enterTextInCell(aya, 4, 9);

						// Column 3: Middle text
						aya = buildText(token, 2, tokensCount - 3);
						enterTextInCell(aya, 3, 5);

						// Column 2: Left text
						aya = buildText(token, tokensCount - 2, tokensCount - 1);
						enterTextInCell(aya, 2, 9);
						break;
				}
				
				// Apply alternating row shading
				applyAlternateRowShading();
				
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