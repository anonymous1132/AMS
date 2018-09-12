using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCADataTranslator.Helper;
using Microsoft.Office.Interop.Excel;
using System.Data;
using DataTable = System.Data.DataTable;

namespace MCADataTranslator.Bll
{
    public class ExcelOper
    {
        public ExcelOper(string filepath)
        {
            _filePath = filepath;
        }

        ~ExcelOper()
        {
            try
            { Quit(); }
            catch (Exception)
            { }
        }

        private Application _app;
        public Application app
        {
            get {
                if (_app == null)
                {
                    _app= excelHelper.app();
                    return _app;
                }
                else { return _app; }
            }
        }

        private ExcelHelper excelHelper = new ExcelHelper();
        private string _filePath;

        private Workbook _workbook;
        private Workbook workbook
        {
            get {
                if (_workbook == null) { _workbook= excelHelper.GetWorkbook(_filePath, app); return _workbook; }
                else { return _workbook; }
            }
        }

        private Worksheet _worksheet;
        private Worksheet worksheet
        {
            get {
                if (_worksheet == null) { _worksheet= excelHelper.GetWorksheet(workbook, 1); }
                return _worksheet;
            }
        }

        public DataSet dataset
        {
            get { return excelHelper.GetContent(_filePath); }
        }
        public void Save()
        { excelHelper.Save(workbook,_filePath); }

        public void Quit()
        {
            excelHelper.QuitExcel(app,workbook);
        }

        int currentRow = 21;
        //针对本业务，画一个表格模块
        public void PrintOneReportBlcok(ReportModelBlock report)
        {
            int startrow = currentRow;
            currentRow++;
            worksheet.Cells[currentRow,"B"] = report.EQP;
            worksheet.Cells[currentRow,"A"] = "SAMPLE:";
            currentRow++;
            worksheet.Cells[currentRow,"B"] = report.MEMO1;
            worksheet.Cells[currentRow,"A"] = "MEMO1 :";
            currentRow++;
            List<string> strlist =new List<string> { "No.", "S", "Cl", "K",   "Ca",  "Ti",  "V",   "Cr",  "Mn",  "Fe",  "Co",  "Ni",  "Cu",  "Zn",  "Sb",  "Te",  "Na",  "Mg",  "Al",  "Ge",  "As"};
            List<string> unitlist = new List<string> {"", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2", "*E10ats/cm2"};
            int colConut = strlist.Count;
            for (int i = 1; i <= colConut; i++)
            {
                worksheet.Cells[currentRow,i] = strlist[i-1];
                worksheet.Cells[currentRow + 1,i] = unitlist[i - 1];
            }

            currentRow += 2;
            List<string> speclist = new List<string> {report.SpecUnit.S,report.SpecUnit.Cl, report.SpecUnit.K, report.SpecUnit.Ca, report.SpecUnit.Ti, report.SpecUnit.V, report.SpecUnit.Cr, report.SpecUnit.Mn, report.SpecUnit.Fe, report.SpecUnit.Co, report.SpecUnit.Ni, report.SpecUnit.Cu, report.SpecUnit.Zn, report.SpecUnit.Sb, report.SpecUnit.Te, report.SpecUnit.Na, report.SpecUnit.Mg, report.SpecUnit.Al, report.SpecUnit.Ge, report.SpecUnit.As };
            foreach (ReportModelUnit unit in report.repotunits)
            {
                List<string> elementlist = new List<string> { unit.IndexNo, unit.S, unit.Cl, unit.K, unit.Ca, unit.Ti, unit.V, unit.Cr, unit.Mn, unit.Fe, unit.Co, unit.Ni, unit.Cu, unit.Zn, unit.Sb, unit.Te, unit.Na, unit.Mg, unit.Al, unit.Ge, unit.As };
                for (int i = 1; i <= colConut; i++)
                {
                    worksheet.Cells[currentRow, i] = elementlist[i - 1];
                    try
                    {
                        if (i > 1)
                        {
                            double num = speclist[i - 1] == "" ? 0 : Convert.ToDouble(speclist[i - 1]);
                            double elevalue = elementlist[i - 1] == "" ? 0 : Convert.ToDouble(elementlist[i - 1]);
                            if (elevalue > num)
                            {
                                excelHelper.SetRangeBackground((Range)worksheet.Cells[currentRow, i]);
                            }
                        }
                    }
                    catch (Exception)
                    { }
                }
                currentRow++;
            }

            //设置格式
            Range rng = worksheet.Range["A"+(startrow+1).ToString(),"U"+(startrow+3).ToString()];
            excelHelper.SetFontSize(rng,12);
            excelHelper.SetFontStyle(rng, "Times New Roman");
             rng = worksheet.Range["B" + (startrow + 1).ToString(), "U" + (startrow + 3).ToString()];
            rng.Cells.ColumnWidth = 10;
            rng = worksheet.Range["A" + (startrow + 1).ToString(), "A" + (startrow + 3).ToString()];
            rng.Cells.ColumnWidth = 15;
            rng = (Range)worksheet.Cells[startrow+3,"A"];
            excelHelper.SetFontHVLeft(rng);
            rng= worksheet.Range["B" + (startrow + 3).ToString(), "U" + (startrow + 3).ToString()];
            excelHelper.SetFontHVCenter(rng);
            rng= worksheet.Range["A" + (startrow + 4).ToString(), "U" + (startrow + 4).ToString()];
            excelHelper.SetFontHVLeft(rng);
            rng= worksheet.Range["A" + (startrow + 4).ToString(), "U" + (currentRow-1).ToString()];
            excelHelper.SetFontSize(rng,10);
            excelHelper.SetFontStyle(rng, "新細明體");
            rng= worksheet.Range["A" + (startrow + 3).ToString(), "U" + (currentRow - 1).ToString()];
            excelHelper.SetRangeBodersStyle(rng,1);
            excelHelper.SetRangeBodersThickness(rng,XlBorderWeight.xlThin);
            rng= worksheet.Range["B" + (startrow + 5).ToString(), "U" + (currentRow - 1).ToString()];
            excelHelper.SetRangeValueStyleNumber(rng);
        }

        public void PrintOneReportBlcok(ReportModelBlockType2 report)
        {
            int startrow = 21;
            int currentrow = startrow;
            foreach (ReportModelUnitType2 unit in report.repotunits)
            {
                List<string> elementlist = new List<string> { unit.EQP, unit.WaferID, unit.Na, unit.Al, unit.Ca, unit.Cr, unit.Fe, unit.Ni, unit.Cu, unit.Zn , "E10ats/cm2" };
                for (int i = 1; i <=elementlist.Count ; i++)
                {
                    worksheet.Cells[currentrow, i] = elementlist[i - 1];
                }
                currentrow++;
            }
            //设置格式
            Range rng = worksheet.Range["A" + startrow.ToString(), "K" + (currentrow-1).ToString()];
            excelHelper.SetFontSize(rng, 12);
            excelHelper.SetFontStyle(rng, "Times New Roman");
            excelHelper.SetRangeBodersStyle(rng, 1);
            excelHelper.SetRangeBodersThickness(rng, XlBorderWeight.xlThin);
            excelHelper.SetFontHVCenter(rng);
        }

        public DataTable GetContentFromExcel()
        {
            DataTable dt = new DataTable();
            int iRowCount = worksheet.UsedRange.Rows.Count;
            int iColCount = worksheet.UsedRange.Columns.Count;
            for (int i=0;i<iColCount;i++)
            { dt.Columns.Add(); }
            for (int iRow = 1; iRow <= iRowCount; iRow++)
            {
                DataRow dr = dt.NewRow();
                for (int iCol = 1; iCol <= iColCount; iCol++)
                {
                  Range  range = worksheet.Cells[iRow, iCol];
                    dr[iCol - 1] = (range.Value2 == null) ? "" : range.Text.ToString();
                }
                dt.Rows.Add(dr);
            }
            return dt;   
        }

    }
}
