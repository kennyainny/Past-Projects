using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BVN.Sort.App
{
    public partial class Form1 : Form
    {
        static StreamWriter _textWriter;
        string _existingFileName;
        string _saveFilePath;
        string fileNamePrefix1, fileNamePrefix2;
        bool _isCanceled; //keeps the state of application when cancel button is pressed
        int _countStopped;
        
        public Form1()
        {
            InitializeComponent();

            _isCanceled = false;
            _countStopped = 0;

            // To report progress from the background worker we need to set this property
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;

            // This event will be raised on the worker thread when the worker starts
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //fileNamePrefix1 = "NIBSS_BVN_09102015_1000000_";
            fileNamePrefix1 = "NIBSS_BVN_08012016_1000000_200000_";
            textBox1.Text = fileNamePrefix1;
            label4.Text = "Status: Application Started";
            fileNamePrefix2 = "_ALL BANK 1_";
            textBox2.Text = fileNamePrefix2;
            //fileNamePrefix2 = "_ALL BANK 11_";
            //fileNamePrefix2 = "_ALL BANK 12_";
            //fileNamePrefix2 = "_ALL BANK 13_";
            //fileNamePrefix2 = "_ALL BANK 14_";
            //fileNamePrefix2 = "_ALL BANK 15_";
        }
        
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            //checks if background task has been canceled by user
            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            #region Excel Operations Code

            try
            {
                if (_isCanceled) //initialize variables if stop button was pressed
                    backgroundWorker1.ReportProgress(0, "");

                backgroundWorker1.ReportProgress(-2, "Status: Reading selected file..."); // update progress bar II
                backgroundWorker1.ReportProgress(-1, "Reading records in the selected file..."); //update progress bar I
                //Read records in the specified excel file
                ReadExcelFile _excelReader = new ReadExcelFile(this);
                object[,] _readData = _excelReader.OpenAndReadSpreadsheet(_existingFileName);

                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                //Convert array to list for easy grouping
                BVNModel _unsortedRecord = new BVNModel();
                List<BVNModel> _unsortedList = _unsortedRecord.ArrayToList(_readData);

                int count = 0; //record counts
                int totalRecords = _unsortedList.Count;

                string _statusMessage = "Total records discovered: " + totalRecords;

                backgroundWorker1.ReportProgress(-2, "Records discovered: " + totalRecords.ToString());
                backgroundWorker1.ReportProgress(-1, _statusMessage); // update status   

                //Sort by bank name
                var _selectBanks = from _customerRecord in _unsortedList
                                   group _customerRecord by _customerRecord.BankName;

                #region Sorting By Bank Name

                foreach (var _bankGroup in _selectBanks)
                {
                    //sort by branches in a given bank
                    var _selectBranch = from _recordInBranch in _bankGroup
                                        group _recordInBranch by _recordInBranch.PickUpBranch;

                    foreach (var _branchGroup in _selectBranch)
                    {
                        //sort by enrolment branch if pickup branch is empty
                        if (_branchGroup.Key == "")
                        {
                            throw new Exception("Unexpected behaviour Pick up branch not specified!!!");


                            #region Sorting By Enrolment Branch

                            //List<BVNModel> branchLessCustomers = _branchGroup.ToList();
                            //var enrolementBranchCusts = from custInEnrolmentBranch in branchLessCustomers //sort by Enrolment branch
                            //                            group custInEnrolmentBranch
                            //                            by custInEnrolmentBranch.BranchOfEnrollment;

                            //foreach (var branchInnerGroup in enrolementBranchCusts)
                            //{
                            //    if (branchInnerGroup.Count() > 500)//If record greater that 500
                            //    {
                            //        int branchSeriaNo = 0;
                            //        string branchName = RemoveSpecialCharacters(branchInnerGroup.Key);

                            //        //split group into 500
                            //        var groupInto500 = branchInnerGroup.Select((p, index) => new { p, index })
                            //                                   .GroupBy(a => a.index / 500).ToList();

                            //        //Format the each group and write to excel
                            //        foreach (var index_model in groupInto500)
                            //        {
                            //            var filteredModel = index_model.Select(grp => grp.p).ToList();

                            //            //New naming convention
                            //            string newFileName = fileNamePrefix1 + _bankGroup.Count() + fileNamePrefix2
                            //                + _bankGroup.Key + "_" + branchName + "_BOX " + (index_model.Key + 1).ToString();

                            //            string[,] customerBranchArray = new string[filteredModel.Count, 4];
                            //            int arrayIndex = 0;

                            //            foreach (BVNModel customer in filteredModel)
                            //            {
                            //                customerBranchArray[arrayIndex, 0] = customer.SerialNo;
                            //                customerBranchArray[arrayIndex, 1] = (branchSeriaNo + 1).ToString();
                            //                customerBranchArray[arrayIndex, 2] = customer.BVN;
                            //                customerBranchArray[arrayIndex, 3] = customer.NameOnCard;

                            //                branchSeriaNo++;
                            //                arrayIndex++;

                            //                int _recordCount = branchSeriaNo + count;
                            //                backgroundWorker1.ReportProgress(-2, "Status: " + _recordCount + " | " + _bankGroup.Key + " | " + branchInnerGroup.Key);
                            //            }

                            //            // create a separate directory for each bank if it doesnot exist already
                            //            string _saveDirectory = _saveFilePath + _bankGroup.Key + @"\";
                            //            if (!Directory.Exists(_saveDirectory))
                            //            {
                            //                var newDirectory = Directory.CreateDirectory(_saveDirectory);
                            //                _saveDirectory = newDirectory.FullName;
                            //            }

                            //            //parse the array content to excel
                            //            CreateExcelFile excell_app = new CreateExcelFile(_saveDirectory, newFileName + ".xlsx", this);
                            //            excell_app.CreateExcel(branchName, newFileName, customerBranchArray);

                            //StreamWriter _branchNameWriter = new StreamWriter(_saveDirectory + _bankGroup.Key + "_Branches.txt", true);
                            //_branchNameWriter.WriteLine(newFileName + ".xlsx");
                            //_branchNameWriter.Flush();
                            //_branchNameWriter.Close();

                            //            count += arrayIndex;
                            //            int _progress1 = count * 100 / totalRecords; //append variables with number to avoid ambiguity
                            //            string _statusMessage1 = "Bank: " + _bankGroup.Key + " | Branch: " + branchName + " | Count: " + branchSeriaNo + " | Branch of Enrolment.";

                            //            backgroundWorker1.ReportProgress(_progress1, _statusMessage1);

                            //            if (worker.CancellationPending)
                            //            {
                            //                //Save state before canceling
                            //                _countStopped = count;
                            //                _isCanceled = true;
                            //                e.Cancel = true;
                            //                return;
                            //            }
                            //        }
                            //    }
                            //    else //If record is less than 500
                            //    {
                            //        int custCount = branchInnerGroup.Count();
                            //        string branchName = RemoveSpecialCharacters(branchInnerGroup.Key);

                            //        //New naming convention
                            //        string newFileName = fileNamePrefix1 + _bankGroup.Count() + fileNamePrefix2
                            //            + _bankGroup.Key + "_" + branchName + "_BOX 1";

                            //        //string newFileName = "NIBSS_BVN_23092015_" + custCount.ToString()
                            //        //    + "_" + _bankGroup.Key + "_" + branchName + "_BOX1";

                            //        string[,] customerBranchArray = new string[custCount, 4];
                            //        int branchSN = 0;

                            //        foreach (BVNModel customer in branchInnerGroup)
                            //        {
                            //            customerBranchArray[branchSN, 0] = customer.SerialNo;
                            //            customerBranchArray[branchSN, 1] = (1 + branchSN).ToString();
                            //            customerBranchArray[branchSN, 2] = customer.BVN;
                            //            customerBranchArray[branchSN, 3] = customer.NameOnCard;

                            //            branchSN++;

                            //            int _recordCount = branchSN + count;
                            //            backgroundWorker1.ReportProgress(-2, "Status: " + _recordCount + " | "
                            //                + _bankGroup.Key + " | " + branchInnerGroup.Key); // update progress bar II
                            //        }

                            //        // create a separate directory for each bank if it doesnot exist already
                            //        string _saveDirectory = _saveFilePath + _bankGroup.Key + @"\";
                            //        if (!Directory.Exists(_saveDirectory))
                            //        {
                            //            var newDirectory = Directory.CreateDirectory(_saveDirectory);
                            //            _saveDirectory = newDirectory.FullName;
                            //        }

                            //        CreateExcelFile excell_app = new CreateExcelFile(_saveDirectory, newFileName + ".xlsx", this);
                            //        excell_app.CreateExcel(branchName, newFileName, customerBranchArray);

                            //StreamWriter _branchNameWriter = new StreamWriter(_saveDirectory + _bankGroup.Key + "_Branches.txt", true);
                            //_branchNameWriter.WriteLine(newFileName + ".xlsx");
                            //_branchNameWriter.Flush();
                            //_branchNameWriter.Close();

                            //        count += custCount;
                            //        int _progress3 = count * 100 / totalRecords;
                            //        string _statusMessage3 = "Bank: " + _bankGroup.Key + " | Branch: " + branchName + " | Count: " + custCount + " | Branch of Enrolment.";

                            //        backgroundWorker1.ReportProgress(_progress3, _statusMessage3);

                            //        if (worker.CancellationPending)
                            //        {
                            //            //Save state before canceling
                            //            _countStopped = count;
                            //            _isCanceled = true;
                            //            e.Cancel = true;
                            //            return;
                            //        }
                            //    }
                            //}
                            ////continue;

                            #endregion Sorting By Enrolment Branch END
                        }
                        else
                        {
                            #region Sorting By Pick up Branch

                            if (_branchGroup.Count() > 500) //If record greater that 500
                            {
                                int branchSeriaNo = 0;
                                string branchName = RemoveSpecialCharacters(_branchGroup.Key);

                                //split group into 500
                                var groupInto500 = _branchGroup.Select((p, index) => new { p, index })
                                                           .GroupBy(a => a.index / 500).ToList();

                                //Format the each group and write to excel
                                foreach (var index_model in groupInto500)
                                {
                                    var filteredModel = index_model.Select(grp => grp.p).ToList();

                                    //New naming convention
                                    string newFileName = fileNamePrefix1 + _bankGroup.Count() + fileNamePrefix2
                                        + _bankGroup.Key + "_" + branchName + "_BOX " + (index_model.Key + 1).ToString();

                                    string[,] customerBranchArray = new string[filteredModel.Count, 4];
                                    int arrayIndex = 0;

                                    foreach (BVNModel customer in filteredModel)
                                    {
                                        customerBranchArray[arrayIndex, 0] = customer.SerialNo;
                                        customerBranchArray[arrayIndex, 1] = (branchSeriaNo + 1).ToString();
                                        customerBranchArray[arrayIndex, 2] = customer.BVN;
                                        customerBranchArray[arrayIndex, 3] = customer.NameOnCard;

                                        branchSeriaNo++;
                                        arrayIndex++;

                                        int _recordCount = branchSeriaNo + count;
                                        backgroundWorker1.ReportProgress(-2, "Status: " + _recordCount + " | " + _bankGroup.Key + " | " + _branchGroup.Key);
                                    }

                                    // create a separate directory for each bank if it doesnot exist already
                                    string _saveDirectory = _saveFilePath + _bankGroup.Key + @"\";
                                    if (!Directory.Exists(_saveDirectory))
                                    {
                                        var newDirectory = Directory.CreateDirectory(_saveDirectory);
                                        _saveDirectory = newDirectory.FullName;
                                    }

                                    CreateExcelFile excell_app = new CreateExcelFile(_saveDirectory, newFileName + ".xlsx", this);
                                    excell_app.CreateExcel(branchName, newFileName, customerBranchArray);

                                    StreamWriter _branchNameWriter = new StreamWriter(_saveDirectory + _bankGroup.Key + "_Branches.txt", true);
                                    _branchNameWriter.WriteLine(newFileName + ".xlsx");
                                    _branchNameWriter.Flush();
                                    _branchNameWriter.Close();

                                    count += arrayIndex;
                                    int _progress4 = count * 100 / totalRecords;
                                    string _statusMessage4 = "Bank: " + _bankGroup.Key + " | Branch: " + branchName + " | Count: " + branchSeriaNo + " | Pick up Branch.";

                                    backgroundWorker1.ReportProgress(_progress4, _statusMessage4);

                                    if (worker.CancellationPending)
                                    {
                                        //Save state before canceling
                                        _countStopped = count;
                                        _isCanceled = true;
                                        e.Cancel = true;
                                        return;
                                    }
                                }
                            }
                            else //if record is less than 500
                            {
                                int custCount_br = _branchGroup.Count();
                                string branchName_br = RemoveSpecialCharacters(_branchGroup.Key);

                                //New naming convention
                                string newFileName_br = fileNamePrefix1 + _bankGroup.Count() + fileNamePrefix2
                                    + _bankGroup.Key + "_" + branchName_br + "_BOX 1";

                                string[,] customerBranchArray_br = new string[custCount_br, 4];
                                int branchSN_br = 0;

                                foreach (BVNModel customer in _branchGroup)
                                {
                                    customerBranchArray_br[branchSN_br, 0] = customer.SerialNo;
                                    customerBranchArray_br[branchSN_br, 1] = (1 + branchSN_br).ToString();
                                    customerBranchArray_br[branchSN_br, 2] = customer.BVN;
                                    customerBranchArray_br[branchSN_br, 3] = customer.NameOnCard;

                                    branchSN_br++;

                                    int _recordCount = branchSN_br + count;
                                    backgroundWorker1.ReportProgress(-2, "Status: " + _recordCount + " | " + _bankGroup.Key + " | " + _branchGroup.Key);
                                }

                                //Create a separate directory for each bank if it doesnot exist already
                                string _saveDirectory = _saveFilePath + _bankGroup.Key + @"\";
                                if (!Directory.Exists(_saveDirectory))
                                {
                                    var newDirectory = Directory.CreateDirectory(_saveDirectory);
                                    _saveDirectory = newDirectory.FullName;
                                }

                                CreateExcelFile excell_app_br = new CreateExcelFile(_saveDirectory, newFileName_br + ".xlsx", this);
                                excell_app_br.CreateExcel(branchName_br, newFileName_br, customerBranchArray_br);

                                StreamWriter _branchNameWriter = new StreamWriter(_saveDirectory + _bankGroup.Key + "_Branches.txt", true);
                                _branchNameWriter.WriteLine(newFileName_br + ".xlsx");
                                _branchNameWriter.Flush();
                                _branchNameWriter.Close();

                                count = count + custCount_br;
                                int _progress6 = count * 100 / totalRecords;
                                string _statusMessage6 = "Bank: " + _bankGroup.Key + " | Branch: " + branchName_br + " | Count: " + custCount_br + " | Pick up Branch.";

                                backgroundWorker1.ReportProgress(_progress6, _statusMessage6);

                                if (worker.CancellationPending)
                                {
                                    //Save state before canceling
                                    _countStopped = count;
                                    _isCanceled = true;
                                    e.Cancel = true;
                                    return;
                                }
                            }

                            #endregion Sorting By Pick up Branch END
                        }

                        if (worker.CancellationPending)
                        {
                            //Save state before canceling
                            _countStopped = count;
                            _isCanceled = true;
                            e.Cancel = true;
                            return;
                        }
                        // backgroundWorker1.ReportProgress(-1, "Total records in" + _bankGroup.Key + " | " + _branchGroup.Key + " : " + _bankGroup.Count());
                    }
                    // backgroundWorker1.ReportProgress(-1, "Total Branches in " + _bankGroup.Key + ": " + _selectBranch.Count());

                    if (worker.CancellationPending)
                    {
                        //Save state before canceling
                        _countStopped = count;
                        _isCanceled = true;
                        e.Cancel = true;
                        return;
                    }
                }

                backgroundWorker1.ReportProgress(100, "Total Bank sorted: " + _selectBanks.Count());
                backgroundWorker1.ReportProgress(100, "Total records processed: " + count + ".");
                backgroundWorker1.ReportProgress(-5, count.ToString());
                _countStopped = count; //save the total records processed
                _isCanceled = true;

                #endregion Sorting By Bank Name END
            }
            catch (Exception ex)
            {
                //Update label4
                backgroundWorker1.ReportProgress(0, "ERROR: \n" + ex.Message.ToString());
                e.Result = "ERROR";
                //UpdateView("Error has occured: " + ex.Message.ToString());
            }
            finally
            {
                //_textWriter.Flush();
                //_textWriter.Close();

                //GC.Collect();
            }

            #endregion Excel Operations Code END

        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

            string _logWriter;

            //dont update progressbar if value is -1, but update text box
            if (e.ProgressPercentage == -1) //case I update
            {
                _logWriter = e.UserState as string;
                richTextBox1.AppendText("[" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() + "] -> " + _logWriter + "\n");
                richTextBox1.Select(richTextBox1.Text.Length - 1, 0);
                richTextBox1.ScrollToCaret();

            }
            else if (e.ProgressPercentage == -2)//case II update
            {
                _logWriter = e.UserState as string;
                label4.Text = _logWriter;
                progressBar2.Style = ProgressBarStyle.Marquee;//.Update();
                progressBar2.MarqueeAnimationSpeed = 10;
            }
            else if (e.ProgressPercentage == -3)//case III update
            {
                _logWriter = e.UserState as string;

                label4.Text = "Status: An error has occurred ";
                richTextBox1.AppendText("[ERROR] -> " + _logWriter + "\n");
                progressBar2.Style = ProgressBarStyle.Blocks;
            }
            else if (e.ProgressPercentage == -4)//case IV update
            {
                _logWriter = e.UserState as string;

                label4.Text = "Status: " + _logWriter;
                richTextBox1.AppendText("[Stopped] -> " + _logWriter + "\n");
                progressBar2.Style = ProgressBarStyle.Blocks;
            }
            else if (e.ProgressPercentage == -5)//case V update
            {
                _logWriter = e.UserState as string;

                _logWriter = "[Completed] -> " + _logWriter + " records processed";
                //"Status: Completed" + _logWriter;
                richTextBox1.AppendText(_logWriter + "\n");
                progressBar2.Style = ProgressBarStyle.Blocks;
                progressBar2.Value = 100;
            }
            else //Case VI update
            {
                _logWriter = e.UserState as string;

                int _barValue = e.ProgressPercentage > 100 ? 100 : e.ProgressPercentage;

                progressBar1.Value = _barValue;
                label1.Text = e.ProgressPercentage.ToString() + " %";

                richTextBox1.AppendText("[" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() + "] -> " + _logWriter + "\n");
                richTextBox1.Select(richTextBox1.Text.Length - 1, 0);
                richTextBox1.ScrollToCaret();

            }

            _textWriter.WriteLine("[" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() + "] -> " + _logWriter);
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                label4.Text = "Status: Sorting canceled.";// +_countStopped + " records processed";
            }
            else if (e.Result != null) //"ERROR"
            {
                label4.Text = "Status: An error has occurred ";
            }
            else
            {
                label4.Text = "Status: Completed - " + _countStopped + " records processed!";
            }

            _textWriter.Flush();
            _textWriter.Close();

            GC.Collect();
        }

        //button selects source file
        private void button1_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Please wait system is busy!");
                return;
            }

            // Show the dialog to select a file
            openFileDialog1.CheckFileExists = true;
            DialogResult _dialogResult = openFileDialog1.ShowDialog();

            //check the file extension
            if (_dialogResult == DialogResult.OK && openFileDialog1.SafeFileName.Contains(".xlsx"))
            {
                _existingFileName = openFileDialog1.FileName;
                richTextBox1.AppendText("[Source file selected] -> " + _existingFileName + "\n");
            }
            else
            {
                _existingFileName = "";
                MessageBox.Show("Error selecting file...");
            }
        }

        //button starts BVN sorting
        private void button2_Click(object sender, EventArgs e)
        {
            //check if a background process is already running
            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Please wait system is busy!");
                return;
            }

            //check if the directories selected exist
            if (_existingFileName == null || _existingFileName == ""
                || _saveFilePath == null || _saveFilePath == "")
            {
                MessageBox.Show("Please select a correct file and destination folder!");
                return;
            }

            textBox1.Text = fileNamePrefix1;

            //check if text values are set to name prefixes or assume default values
            if (textBox1.Text.Length > 1)
                fileNamePrefix1 = textBox1.Text;
            else
                fileNamePrefix1 = "NIBSS_BVN_09102015_1000000_";

            if (textBox2.Text.Length > 1)
                fileNamePrefix2 = textBox2.Text;
            else
                fileNamePrefix2 = "_ALL BANK 1_";

            if (_textWriter.BaseStream == null) //initialize variables if stop button was pressed
            {
                _textWriter = new StreamWriter(_saveFilePath + "log.txt", true);
            }

            backgroundWorker1.RunWorkerAsync();
        }

        //Button cancel background worker
        private void button3_Click(object sender, EventArgs e)
        {
            if (!backgroundWorker1.IsBusy)
            {
                return;
            }

            if (backgroundWorker1.WorkerSupportsCancellation)
            {
                backgroundWorker1.ReportProgress(-4, "Sorting canceled by user");

                backgroundWorker1.CancelAsync();
            }
        }

        //button selects destination directory
        private void button4_Click(object sender, EventArgs e)
        {

            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Please wait system is busy!");
                return;
            }

            // Show the dialog to save a file
            DialogResult _dialogResult = folderBrowserDialog1.ShowDialog();

            if (_dialogResult == DialogResult.OK) // Test result.
            {
                _saveFilePath = folderBrowserDialog1.SelectedPath + @"\";
                //initalize the text writer
                _textWriter = new StreamWriter(_saveFilePath + "log.txt", true);
                richTextBox1.AppendText("[Destination folder selected] -> " + _saveFilePath + "\n");
            }
            else
            {
                _saveFilePath = "";
                MessageBox.Show("Error selecting destination directory...");
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        //Update from other class
        public void UpdateRichBox(string _text)
        {
            backgroundWorker1.ReportProgress(-3, _text); // update progress bar III

            if (backgroundWorker1.WorkerSupportsCancellation)
            {
                backgroundWorker1.CancelAsync();
            }
        }
        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        #region Unused Code

        //if (isProcessRunning)
        //{
        //    MessageBox.Show("A process is already running.");
        //    return;
        //}

        //Thread backgroundThread = new Thread(
        //    new ThreadStart(() =>
        //    {
        //        isProcessRunning = true;

        //        for (int n = 0; n < 100; n++)
        //        {
        //            Thread.Sleep(50);
        //            progressBar1.BeginInvoke(
        //                new Action(() =>
        //                {
        //                    progressBar1.Value = n;
        //                }
        //            ));
        //        }

        //        MessageBox.Show("Thread completed!");
        //        progressBar1.BeginInvoke(
        //                new Action(() =>
        //                {
        //                    progressBar1.Value = 0;
        //                }
        //        ));

        //        isProcessRunning = false;
        //    }
        //));
        //backgroundThread.Start();


        //if (isProcessRunning)
        //{
        //    MessageBox.Show("A process is already running.");
        //    return;
        //}

        //ProgressDialog progressDialog = new ProgressDialog();

        //Thread backgroundThread = new Thread(
        //    new ThreadStart(() =>
        //    {
        //        isProcessRunning = true;

        //        for (int n = 0; n < 100; n++)
        //        {
        //            Thread.Sleep(50);
        //            progressDialog.UpdateProgress(n); // Update progress in progressDialog
        //        }

        //        MessageBox.Show("Thread completed!");
        //        // No need to reset the progress since we are closing the dialog
        //        progressDialog.BeginInvoke(new Action(() => progressDialog.Close()));

        //        isProcessRunning = false;
        //    }
        //));
        //backgroundThread.Start();

        //progressDialog.ShowDialog();

        //method to update progress bar
        //public void UpdateProgressBar(int progress)
        //{
        //    if (progressBar1.InvokeRequired)
        //    {
        //        progressBar1.BeginInvoke(
        //            new Action(() =>
        //            {
        //                progressBar1.Value = progress; // *100;
        //                label1.Text = progress.ToString() + " %"; // * 100000).ToString() + " %";
        //            }
        //        ));
        //    }
        //    else
        //    {
        //        progressBar1.Value = progress; // *100;
        //        label1.Text = progress.ToString()+" %";// * 100000).ToString() + " %";
        //    }
        //}

        //method to update richtextbox and streamwriter
        //public void UpdateView(string msg)
        //{
        //    if (richTextBox1.InvokeRequired)
        //    {
        //        richTextBox1.BeginInvoke(
        //            new Action(() =>
        //            {
        //                richTextBox1.AppendText("[" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() + "] -> " + msg + "\n");
        //                //richTextBox1.SelectionStart = richTextBox1.Text.Length;
        //                //richTextBox1.Focus();
        //                richTextBox1.Select(richTextBox1.Text.Length - 1, 0);
        //                richTextBox1.ScrollToCaret();
        //            }
        //        ));
        //    }
        //    else
        //    {
        //        richTextBox1.AppendText("[" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() + "] -> " + msg + "\n");
        //        //richTextBox1.SelectionStart = richTextBox1.Text.Length;
        //        //richTextBox1.Focus();
        //        richTextBox1.Select(richTextBox1.Text.Length - 1, 0);
        //        richTextBox1.ScrollToCaret();
        //    }

        //    _textWriter.WriteLine("[" + DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToLongTimeString() + "] -> " + msg);
        //}

        //Remove unwanted characters

        //backgroundWorker1.IsBusy();
        //backgroundWorker1_DoWork(sender, DoWorkEventArgs eh);

        //creates a new thread for excel operations to prevent GUI from freezing
        //     backgroundThread = new Thread(
        //    new ThreadStart(() =>
        //    {
        //        isProcessRunning = true;

        //        //for (int n = 0; n < 100; n++)
        //        //{
        //        //    Thread.Sleep(50);
        //        //    progressBar1.BeginInvoke(
        //        //        new Action(() =>
        //        //        {
        //        //            progressBar1.Value = n;
        //        //        }
        //        //    ));
        //        //}

        //        //MessageBox.Show("Thread completed!");
        //        //progressBar1.BeginInvoke(
        //        //        new Action(() =>
        //        //        {
        //        //            progressBar1.Value = 0;
        //        //        }
        //        //));

        //        //UpdateView("Analyzing selected file, please wait... ");
        //        //richTextBox1.AppendText("Analyzing selected file, please wait... "+"\n");


        //        isProcessRunning = false;
        //    }
        //));
        //     backgroundThread.Start();


        #endregion

    }
}