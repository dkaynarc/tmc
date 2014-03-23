using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop;
using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace RequirementsBuilder
{
    public partial class Form1 : Form
    {
        private Microsoft.Office.Interop.Excel.Application _excel;
        private Microsoft.Office.Interop.Word.Application _word;

        private OpenFileDialog _fileDialog;

        private object Missing = Type.Missing;
        private object EndOfDoc = "\\endofdoc";

        public Form1()
        {
            InitializeComponent();
            this._fileDialog = new OpenFileDialog();
        }

        private void PerformBuildRequirements()
        {
            _excel = new Microsoft.Office.Interop.Excel.Application();
            _word = new Microsoft.Office.Interop.Word.Application();
            _word.Visible = true;

            this.BeginInvoke(
                        new Action<Form1>(s =>
                        {
                            btnBuild.Enabled = false;
                            btnBuild.Text = "Working";
                        }), new object[] { this });

            var spreadsheetPath = tbSpreadsheetPath.Text;

            if (File.Exists(spreadsheetPath))
            {
                var workbook = _excel.Workbooks.Open(spreadsheetPath);
                Word.Document doc = BuildRequirementsDoc(workbook);
                try 
                {
                    // This call throws when the user elects to 'cancel' the save operation.
                    doc.Save();
                }
                catch (Exception)
                { }
               
            }
            else
            {
                MessageBox.Show("No spreadsheet selected.");
            }

            foreach (Excel.Workbook wb in _excel.Workbooks)
            {
                wb.Close();
            }
            _excel.Quit();
            _word.Quit(ref Missing, ref Missing, ref Missing);

            this.BeginInvoke(
                        new Action<Form1>(s =>
                        {
                            btnBuild.Enabled = true;
                            btnBuild.Text = "Build";
                        }), new object[] { this });
        }

        private Word.Document BuildRequirementsDoc(Excel.Workbook workbook)
        {
            Word.Document doc = CreateWordDocument();
            doc.Activate();
            Excel.Worksheet rqmntsWorksheet = workbook.Worksheets["SRS"];

            Excel.Range usedRange = rqmntsWorksheet.UsedRange;
            var requirements = new List<Requirement>();

            // The first row (i == 1) is reserved for headings, so skip it. 
            for (int i = 2; i < usedRange.Rows.Count; i++)
            {
                var row = usedRange.Rows[i];
                requirements.Add(ExcelRowToRequirement(row));
            }

            // Categorise requirements by their functional mappings (e.g. Sensing and Environment)
            var functions = requirements.Select(x => x.Function).Distinct();

            foreach (var function in functions)
            {
                // Categorise requirements by their Type (e.g. Functional, Behavioural)
                var requirementsByType =
                    from requirement in requirements
                    where requirement.Function == function
                    group requirement by requirement.Type into typesGroup
                    orderby typesGroup.Key
                    select typesGroup;

                Word.Range rng = doc.Bookmarks[EndOfDoc].Range;
                WriteParaText(function, rng, doc);

                foreach (var reqGroup in requirementsByType)
                {
                    rng = doc.Bookmarks[EndOfDoc].Range;
                    WriteParaText(reqGroup.Key, rng, doc);

                    foreach (var rqmnt in reqGroup)
                    {
                        try
                        {
                            Word.Table table = RequirementToWordTable(rqmnt, doc.Bookmarks[EndOfDoc].Range, doc);
                            rng = doc.Bookmarks[EndOfDoc].Range;
                            rng.InsertParagraphAfter();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    }
                }
            }

            return doc;
        }

        private void WriteParaText(string text, Word.Range location, Word.Document doc)
        {
            Word.Paragraph functionHeadingPara = doc.Content.Paragraphs.Add(location);
            functionHeadingPara.Range.Text = text;
            functionHeadingPara.Range.InsertParagraphAfter();
        }

        private Word.Table RequirementToWordTable(Requirement rqmnt, Word.Range location, Word.Document doc)
        {
            Word.Range tblRange = doc.Bookmarks[EndOfDoc].Range;
            Word.Table table = doc.Tables.Add(tblRange, 8, 2);

            table.Borders.Enable = 1;

            table.Cell(1, 1).Range.Text = "Identifier";
            table.Cell(1, 2).Range.Text = rqmnt.Id;
            table.Cell(2, 1).Range.Text = "Statement";
            table.Cell(2, 2).Range.Text = rqmnt.Description;
            table.Cell(3, 1).Range.Text = "Date Created";
            table.Cell(3, 2).Range.Text = rqmnt.DateCreated;
            table.Cell(4, 1).Range.Text = "Change History";
            table.Cell(4, 2).Range.Text = rqmnt.ChangeHistory;
            table.Cell(5, 1).Range.Text = "Dependencies";
            table.Cell(5, 2).Range.Text = rqmnt.Dependencies;
            table.Cell(6, 1).Range.Text = "Sources";
            table.Cell(6, 2).Range.Text = rqmnt.Traceability;
            table.Cell(7, 1).Range.Text = "Rationale";
            table.Cell(7, 2).Range.Text = rqmnt.Rationale;
            table.Cell(8, 1).Range.Text = "Classification";
            table.Cell(8, 2).Range.Text = rqmnt.Classification;

            table.PreferredWidthType = Word.WdPreferredWidthType.wdPreferredWidthPercent;
            table.PreferredWidth = 100;

            table.Columns[1].PreferredWidthType = Word.WdPreferredWidthType.wdPreferredWidthPercent;
            table.Columns[1].PreferredWidth = 15;

            for (int i = 1; i < 9; i++)
            {
                table.Cell(i, 1).Range.Font.Bold = 1;
            }

            return table;
        }

        private Requirement ExcelRowToRequirement(Excel.Range excelRow)
        {
            var r = new Requirement();
            
            r.Id = excelRow.Cells[Missing, "A"].Value as string;
            r.Description = excelRow.Cells[Missing, "B"].Value as string;
            r.Function = excelRow.Cells[Missing, "C"].Value as string;
            r.Type = excelRow.Cells[Missing, "D"].Value as string;
            r.Classification = excelRow.Cells[Missing, "E"].Value as string;
            r.Rationale = excelRow.Cells[Missing, "F"].Value as string;
            r.Dependencies = excelRow.Cells[Missing, "G"].Value as string;
            r.DateCreated = excelRow.Cells[Missing, "H"].Value as string;
            r.ChangeHistory = excelRow.Cells[Missing, "I"].Value as string;
            r.Traceability = excelRow.Cells[Missing, "J"].Value as string;

            return r;
        }

        private Word.Document CreateWordDocument()
        { 
            object isVisible = true;

            Word.Document newDoc = _word.Documents.Add(ref Missing, ref Missing, ref Missing, ref isVisible);
            return newDoc;
        }

        private Excel.Range FindInWorksheet(string searchText, Excel.Worksheet pssuWorksheet)
        {
            return pssuWorksheet.Cells.Find(
                searchText, Missing, Missing, Missing, Missing, Excel.XlSearchDirection.xlNext,
                Missing, Missing, Missing);
        }

        private string ShowSingleSelectBrowseDialog()
        {
            string filename = "";
            _fileDialog.Multiselect = false;
            var result = _fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                filename = _fileDialog.FileName;
            }
            return filename;
        }

        private string ShowSaveDialog(string title = "")
        {
            string filename = null;
            var dialog = new SaveFileDialog();

            dialog.Title = title;
            dialog.Filter = "Word files (*.docx)|*.docx|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                filename = dialog.FileName;
            }

            return filename;
        }

        #region eventhandlers

        private void btnBuild_Click(object sender, EventArgs e)
        {
            Task builder = new Task(PerformBuildRequirements);
            builder.Start();
        }

        private void btnSpreadsheetBrowse_Click(object sender, EventArgs e)
        {
            tbSpreadsheetPath.Text = ShowSingleSelectBrowseDialog();
        }
        #endregion
    }
}
