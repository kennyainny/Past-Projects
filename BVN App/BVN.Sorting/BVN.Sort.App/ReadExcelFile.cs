using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BVN.Sort.App
{
    class ReadExcelFile
    {
        Form1 _form1Instance; //in order to access methods in form1       
        Application _excelApp;

        public ReadExcelFile(Form1 form1Instance)
        {
            _excelApp = new Application();
            _form1Instance = form1Instance; //in order to access methods in form1
        }

        //Read all the contents of an excel spreadsheet and return a 2D object array
        public object[,] OpenAndReadSpreadsheet(string existingFile)
        {
            Workbook _workBook;
            Worksheet _sheet;

            try
            {
                _workBook = _excelApp.Workbooks.Open(existingFile,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing);

                _sheet = _workBook.Worksheets[1];

                Range excelRange = _sheet.UsedRange;

                object[,] valueArray = (object[,])excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);

                _workBook.Close(false, existingFile, null);
                _excelApp.Quit();

                //Marshal.ReleaseComObject(workBook);
                if (_sheet != null) Marshal.ReleaseComObject(_sheet);
                if (_workBook != null) Marshal.ReleaseComObject(_workBook);
                if (_excelApp != null) Marshal.ReleaseComObject(_excelApp);

                return valueArray;
            }
            catch (Exception ex)
            {
                if (_form1Instance != null)
                    _form1Instance.UpdateRichBox(ex.Message.ToString()); 
                return null;
            }
            finally
            {
                //free resources
                _excelApp = null;
                _workBook = null;
                _sheet = null;

                GC.Collect();
            }
        }
    }
}
