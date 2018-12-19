using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caojin.Common;
using Microsoft.Office.Interop.Excel;
using System.Data;
using DataTable = System.Data.DataTable;


namespace FirstFloor.ModernUI.App.Runner
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
            get
            {
                if (_app == null)
                {
                    _app = excelHelper.app();
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
            get
            {
                if (_workbook == null) { _workbook = excelHelper.GetWorkbook(_filePath, app); return _workbook; }
                else { return _workbook; }
            }
        }

        private Worksheet _worksheet;
        private Worksheet worksheet
        {
            get
            {
                if (_worksheet == null) { _worksheet = excelHelper.GetWorksheet(workbook, 1); }
                return _worksheet;
            }
        }

  
        public void Save()
        { excelHelper.Save(workbook, _filePath); }

        public void Quit()
        {
            excelHelper.QuitExcel(app, workbook);
        }

   
    

        public DataTable GetContentFromExcel()
        {
            DataTable dt = new DataTable();
            int iRowCount = worksheet.UsedRange.Rows.Count;
            int iColCount = worksheet.UsedRange.Columns.Count;
            for (int i = 0; i < iColCount; i++)
            { dt.Columns.Add(); }
            for (int iRow = 1; iRow <= iRowCount; iRow++)
            {
                DataRow dr = dt.NewRow();
                for (int iCol = 1; iCol <= iColCount; iCol++)
                {
                    Range range = worksheet.Cells[iRow, iCol];
                    dr[iCol - 1] = (range.Value2 == null) ? "" : range.Text.ToString();
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

    }
}
