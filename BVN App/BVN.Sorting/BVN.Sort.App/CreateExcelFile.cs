using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Microsoft.Office.Interop.Excel;
//using OfficeOpenXml;
using System.Runtime.InteropServices;



namespace BVN.Sort.App
{
    class CreateExcelFile
    {
        //Global Private Variables
        Form1 _form1Instance; //in order to access methods in form1       
        Application _newExcelApp = null;
        Workbook _newWorkbook = null;
        Worksheet _newWorksheet = null;
        Range _excelCellRange = null;

        string _fullPath, _fileName;

        //Initialise variables with constructor
        public CreateExcelFile(string fullPath, string fileName, Form1 form1Instance)
        {
            _fullPath = fullPath;
            _fileName = fileName;
            _form1Instance = form1Instance; //in order to access methods in form1
        }

        //Creates an excel file
        public void CreateExcel(string branchName, string pageTitle, string[,] excelData)
        {
            try
            {
                _newExcelApp = new Application();
                //Disable functions that can slow down processing
                _newExcelApp.Visible = false;
                _newExcelApp.DisplayAlerts = false;
                _newExcelApp.UserControl = false;
                _newExcelApp.ScreenUpdating = false;

                _newWorkbook = _newExcelApp.Workbooks.Add(Type.Missing);
                _newWorksheet = (Worksheet)_newWorkbook.ActiveSheet;

                //maximum character length for excel sheet
                if (branchName.Length > 31)
                    _newWorksheet.Name = branchName.Substring(0, 30);
                else
                    _newWorksheet.Name = branchName;

                //Build a string for footer in the required format
                string footerSubString = pageTitle.Substring(0, (pageTitle.Length - branchName.Length - 7));

                _newWorksheet.PageSetup.LeftFooter = footerSubString;
                _newWorksheet.PageSetup.RightFooter = "Page &P of &N";
                
                //Set the sheet header and centralise it
                _newWorksheet.Cells[1, 1] = pageTitle;
                _excelCellRange = _newWorksheet.get_Range("A1", "E1");
                _excelCellRange.Font.Bold = true;
                _excelCellRange.HorizontalAlignment = XlHAlign.xlHAlignCenterAcrossSelection;
                _excelCellRange.Merge(4);

                //Add table headers going cell by cell.
                _newWorksheet.Cells[3, 2] = "S/N";
                _newWorksheet.Cells[3, 3] = "BRANCH S/N";
                _newWorksheet.Cells[3, 4] = "BVN";
                _newWorksheet.Cells[3, 5] = "NAME ON CARD";

                _excelCellRange = _newWorksheet.get_Range("B3", "E3");
                _excelCellRange.Font.Bold = true;
                _excelCellRange.EntireColumn.AutoFit();
                _excelCellRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
                _excelCellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                _excelCellRange.Interior.ColorIndex = 41; //Background color for header = light blue
                _excelCellRange.Font.ColorIndex = 2; //Font color for header = white

                //Get the length of the first dimension of array and add offset for header
                int lastCol = excelData.GetLength(0) + 3;
                _excelCellRange = _newWorksheet.get_Range("B4", "E" + lastCol);
                _excelCellRange.Value2 = excelData; //write array data to excel

                //convert excel rows to table and apply the required style
                _excelCellRange = _newWorksheet.get_Range("B3", "E" + lastCol);
                _newWorksheet.ListObjects.Add(XlListObjectSourceType.xlSrcRange, _excelCellRange,
                    Type.Missing, XlYesNoGuess.xlYes, Type.Missing).Name = branchName;
                _excelCellRange.Select();
                _newWorksheet.ListObjects[branchName].TableStyle = "TableStyleLight9";
                _newWorksheet.ListObjects[branchName].ShowHeaders = true;
                _excelCellRange.Font.Size = 9;

                //set individual column width and alignement as requirement demands
                _excelCellRange = _newWorksheet.get_Range("B4", "B" + lastCol);
                _excelCellRange.ColumnWidth = 10;
                _excelCellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                _excelCellRange = _newWorksheet.get_Range("C4", "C" + lastCol);
                _excelCellRange.ColumnWidth = 14;
                _excelCellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                _excelCellRange = _newWorksheet.get_Range("D4", "D" + lastCol);
                _excelCellRange.ColumnWidth = 17;
                _excelCellRange.HorizontalAlignment = XlHAlign.xlHAlignCenter;

                _excelCellRange = _newWorksheet.get_Range("E4", "E" + lastCol);
                _excelCellRange.ColumnWidth = 31;
                _excelCellRange.InsertIndent(2);
                
                //Repeat table row on each page and scale by 80%
                _newWorksheet.PageSetup.PrintTitleRows = "$3:$3";
                //worksheet.UsedRange.PrintTitleRows = "$3:$3"; 
                //excelCellrange.Resize

                          
                //Check if file name already exists before saving excel sheet and close application
                if (File.Exists(_fullPath + _fileName))                    
                    throw new IOException("ERROR: file '" + _fileName + "' already exsit in directory");

                _newWorkbook.SaveAs(_fullPath + _fileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, false,
                    false, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                _newWorkbook.Close();

            }
            catch (Exception ex)
            {
                if(_form1Instance != null)
                _form1Instance.UpdateRichBox(ex.Message.ToString());
            }
            finally
            {
                // Quit Excel application
                _newExcelApp.Quit();

                // Release COM objects
                if (_newWorksheet != null) Marshal.ReleaseComObject(_newWorksheet);
                if (_newWorkbook != null) Marshal.ReleaseComObject(_newWorkbook);
                if (_newExcelApp != null) Marshal.ReleaseComObject(_newExcelApp);

                // Empty variables
                _newExcelApp = null;
                _newWorkbook = null;
                _newWorksheet = null;

                // Force garbage collector cleaning
                GC.Collect();
            }
        }
    }
}



/*

create directory to match each bank name*

check if filename already exist before creating the excel file*

shrink excel doc by 80%

Write to control from a different class

check if the excel object read is not null*

kill all threads when exception is raised and initialise all variables

calculate estimated time
*/