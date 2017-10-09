using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BVN.Sort.App
{
    class BVNModel
    {
        public BVNModel() { }

        //Method converts 2D array to list for easier sorting
        public List<BVNModel> ArrayToList(object[,] excelObjectArray)
        {
            List<BVNModel> _excelObjectList = new List<BVNModel>();

            for (int i = 2; i <= excelObjectArray.GetLength(0); i++) // Include header offset
            {
                BVNModel excelRow = new BVNModel();
                excelRow.SerialNo = excelObjectArray[i, 1].ToString();
                excelRow.BVN = (string)excelObjectArray[i, 2].ToString();

                excelRow.BankName = (string)excelObjectArray[i, 3].ToString();
                excelRow.NameOnCard = (string)excelObjectArray[i, 4].ToString(); //index swaped with bank name

                //excelRow.BranchOfEnrollment = (string)excelObjectArray[i, 4];
                //excelRow.NameOnCard = (string)excelObjectArray[i, 8];
                excelRow.PickUpBranch = (string)excelObjectArray[i, 5].ToString();

                /*
                 * Verion I
                 * 
                 * BVNModel excelRow = new BVNModel();
                excelRow.SerialNo = excelObjectArray[i, 1].ToString();
                excelRow.BVN = (string)excelObjectArray[i, 2];
                excelRow.BankName = (string)excelObjectArray[i, 6];
                excelRow.BranchOfEnrollment = (string)excelObjectArray[i, 7];
                excelRow.NameOnCard = (string)excelObjectArray[i, 8];
                excelRow.PickUpBranch = (string)excelObjectArray[i, 10];
                 * */

                _excelObjectList.Add(excelRow);
            }

            return _excelObjectList;
        }
        public string SerialNo { get; set; }
        public string BVN { get; set; }
        public string BankName { get; set; }
        //public string BranchOfEnrollment { get; set; } // -version 1
        public string NameOnCard { get; set; }
        public string PickUpBranch { get; set; }
     
    }   
}


