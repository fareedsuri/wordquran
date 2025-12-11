using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Xml.Linq;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Forms;

namespace Quran
{
    public partial class QuranRibbon
    {
        private XElement Q;
        private const string QURAN_FONT_NAME = "_PDMS_Saleem_QuranFont";

        private void QuranRibbon_Load(object sender, RibbonUIEventArgs e)
        {
            // Load Quran XML data
            try
            {
                Q = XElement.Load(@"C:\Data\SynologyPrefUnsync\Projects\VisualStudio\QuranCorpus\QuranCorpus\data\ProcessedXML\Grammar2.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Quran data: {ex.Message}", "Data Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Populate Surah dropdown (0-113)
            for (int i = 0; i < 114; i++)
            {
                Microsoft.Office.Tools.Ribbon.RibbonDropDownItem item = this.Factory.CreateRibbonDropDownItem();
                item.Label = i.ToString();
                dropdown_SurahSelector.Items.Add(item);
            }

            // Select first Surah by default
            if (dropdown_SurahSelector.Items.Count > 0)
            {
                dropdown_SurahSelector.SelectedItemIndex = 0;
            }
        }

        private void button_BuildQuranTable_Click(object sender, RibbonControlEventArgs e)
        {
            try
            {
                if (dropdown_SurahSelector.SelectedItem == null)
                {
                    MessageBox.Show("Please select a Surah number first.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int surahNumber = Int32.Parse(dropdown_SurahSelector.SelectedItem.Label);
                BuildQuranTable(surahNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error building table: {ex.Message}", "Build Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BuildQuranTable(int surahNumber)
        {
            Word._Application application = Globals.ThisAddIn.Application;
            Word.Document document = application.ActiveDocument;

            if (Q == null)
            {
                MessageBox.Show("Quran data not loaded!", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string bismillah = Q.Elements("chapter").ElementAt(0).
                Elements("verse").ElementAt(0).Attribute("text").Value;
            XElement chp = Q.Elements("chapter").ElementAt(surahNumber);

            // Set up page
            document.PageSetup.BottomMargin = 20;
            document.PageSetup.TopMargin = 20;
            document.PageSetup.LeftMargin = Convert.ToSingle(0.18 * 72);
            document.PageSetup.RightMargin = Convert.ToSingle(0.2 * 72);
            document.PageSetup.PageWidth = 72 * 4;
            document.PageSetup.PageHeight = 72 * 6;

            // Create table
            Word.Range tableLocation = document.Range(0, 0);
            document.Tables.Add(tableLocation, chp.Elements("verse").Count() + 1, 4);

            Word.Table newTable = document.Tables[1];

            // Set table properties
            newTable.LeftPadding = 1;
            newTable.RightPadding = 1;
            newTable.TopPadding = 0;
            newTable.BottomPadding = 0;
            newTable.Rows.HeightRule = Word.WdRowHeightRule.wdRowHeightAuto;

            // Set column widths
            newTable.Columns[1].SetWidth(Convert.ToSingle(0.2 * 72), Word.WdRulerStyle.wdAdjustNone);
            newTable.Columns[2].SetWidth(Convert.ToSingle(0.72 * 72), Word.WdRulerStyle.wdAdjustNone);
            newTable.Columns[3].SetWidth(Convert.ToSingle(2 * 72), Word.WdRulerStyle.wdAdjustNone);
            newTable.Columns[4].SetWidth(Convert.ToSingle(0.68 * 72), Word.WdRulerStyle.wdAdjustNone);

            // Apply font
            newTable.Range.Font.Name = QURAN_FONT_NAME;

            // Fill table
            FillQuranTable(newTable, chp, bismillah);

            MessageBox.Show($"Surah {surahNumber} table created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FillQuranTable(Word.Table newTable, XElement chp, string bismillah)
        {
            // Detect font
            string detectedFontName = DetectQuranFontName(Globals.ThisAddIn.Application.ActiveDocument);
            if (detectedFontName == null)
            {
                MessageBox.Show($"Warning: PDMS_Saleem_QuranFont not found!\n\nPlease verify the font is installed.",
                    "Font Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                detectedFontName = "Arial"; // Fallback
            }

            // Set bismillah
            newTable.Cell(1, 3).Range.Text = bismillah;
            newTable.Cell(1, 3).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            int verseCounter = 2;
            int verseNumber = 1;

            foreach (XElement verse in chp.Elements("verse"))
            {
                int tokensCount = verse.Elements("tokens").First().Elements("token").Count();
                IEnumerable<XElement> token = verse.Elements("tokens").First().Elements("token");

                // Set verse number
                SetVerseNumberCell(newTable, verseCounter, verseNumber, detectedFontName);

                // Build verse text
                string aya;
                switch (tokensCount)
                {
                    case 1:
                        newTable.Cell(verseCounter, 4).Range.Text = verse.Attribute("text").Value;
                        newTable.Cell(verseCounter, 4).Range.Font.Size = 9;
                        newTable.Cell(verseCounter, 4).Range.Font.Name = detectedFontName;
                        newTable.Cell(verseCounter, 4).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                        break;
                    case 2:
                        aya = BuildText(token, 0, 0);
                        EnterTextInCell(newTable, verseCounter, 4, aya, 9, detectedFontName);
                        aya = BuildText(token, 1, 1);
                        EnterTextInCell(newTable, verseCounter, 2, aya, 9, detectedFontName);
                        break;
                    case 3:
                        aya = BuildText(token, 0, 1);
                        EnterTextInCell(newTable, verseCounter, 4, aya, 9, detectedFontName);
                        aya = BuildText(token, 2, 2);
                        EnterTextInCell(newTable, verseCounter, 2, aya, 9, detectedFontName);
                        break;
                    case 4:
                        aya = BuildText(token, 0, 1);
                        EnterTextInCell(newTable, verseCounter, 4, aya, 9, detectedFontName);
                        aya = BuildText(token, 2, 3);
                        EnterTextInCell(newTable, verseCounter, 2, aya, 9, detectedFontName);
                        break;
                    default:
                        aya = BuildText(token, 0, 1);
                        EnterTextInCell(newTable, verseCounter, 4, aya, 9, detectedFontName);
                        aya = BuildText(token, 2, tokensCount - 3);
                        EnterTextInCell(newTable, verseCounter, 3, aya, 5, detectedFontName);
                        aya = BuildText(token, tokensCount - 2, tokensCount - 1);
                        EnterTextInCell(newTable, verseCounter, 2, aya, 9, detectedFontName);
                        break;
                }

                // Apply spacing to all cells in the row (including empty ones)
                SetRowSpacing(newTable, verseCounter);

                // Apply alternating row shading
                ApplyAlternateRowShading(newTable, verseCounter);

                verseCounter++;
                verseNumber++;
            }
        }

        private string BuildText(IEnumerable<XElement> token, int from, int to)
        {
            string ayaText = "";
            for (int cellNumber = from; cellNumber <= to; cellNumber++)
            {
                ayaText += token.ElementAt(cellNumber).Attribute("text").Value + " ";
            }
            return ayaText;
        }

        private void EnterTextInCell(Word.Table table, int row, int col, string text, int fontSize, string fontName)
        {
            table.Cell(row, col).Range.Text = text;
            table.Cell(row, col).Range.Font.Size = fontSize;
            table.Cell(row, col).Range.Font.Name = fontName;

            // Set alignment
            if (col == 2)
                table.Cell(row, col).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            else if (col == 3)
                table.Cell(row, col).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            else if (col == 4)
                table.Cell(row, col).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
        }

        private void SetRowSpacing(Word.Table table, int row)
        {
            // Set paragraph and line spacing for ALL cells in the row
            for (int col = 1; col <= 4; col++)
            {
                table.Cell(row, col).Range.ParagraphFormat.SpaceAfter = 0;
                table.Cell(row, col).Range.ParagraphFormat.SpaceBefore = 0;
                table.Cell(row, col).Range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
                
                // Set font size based on column (prevents empty cells from having size 12)
                if (col == 1)
                {
                    // Column 1 already set by SetVerseNumberCell (size 6)
                }
                else if (col == 3)
                {
                    // Column 3: Middle text - smaller font
                    table.Cell(row, col).Range.Font.Size = 5;
                }
                else
                {
                    // Columns 2 & 4: Standard text font
                    table.Cell(row, col).Range.Font.Size = 9;
                }
            }
        }

        private void SetParagraphSpacing(Word.Range range)
        {
            range.ParagraphFormat.SpaceAfter = 0;
            range.ParagraphFormat.SpaceBefore = 0;
            range.ParagraphFormat.LineSpacingRule = Word.WdLineSpacing.wdLineSpaceSingle;
        }

        private void SetVerseNumberCell(Word.Table table, int row, int verseNum, string fontName)
        {
            table.Cell(row, 1).Range.Text = verseNum.ToString();
            table.Cell(row, 1).Range.Font.Size = 6;
            table.Cell(row, 1).Range.Font.Name = fontName;
            table.Cell(row, 1).Range.Font.Bold = 1; // Make bold
            table.Cell(row, 1).Range.Font.Color = (Word.WdColor)0x808000; // Dark teal (RGB: 0, 128, 128)
            table.Cell(row, 1).Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            table.Cell(row, 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter; // Vertical center
            table.Cell(row, 1).Range.ParagraphFormat.SpaceAfter = 0;
            table.Cell(row, 1).Range.ParagraphFormat.SpaceBefore = 0;
        }

        private void ApplyAlternateRowShading(Word.Table table, int row)
        {
            if (row % 2 == 0)
            {
                for (int col = 1; col <= 4; col++)
                {
                    table.Cell(row, col).Shading.BackgroundPatternColor = Word.WdColor.wdColorGray15;
                }
            }
        }

        private string DetectQuranFontName(Word.Document document)
        {
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
                    return fontName;
                }
            }

            return null;
        }
    }
}
