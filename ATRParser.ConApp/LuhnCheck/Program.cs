using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuhnCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] creditCards = { "4735952620999868", "499273987170", "4556737586899855", "1234567812345670" };

            foreach (var ccNo in creditCards)
            {
                Console.WriteLine("{0} is {1} Valid.", ccNo, Luhn.CheckLuhn2(ccNo) ? "" : "Not");
            }

            string loop = "y";
            do
            {
                try
                {
                    Console.WriteLine("\nEnter your credit card no:");
                    string newPan = Console.ReadLine();
                    Console.WriteLine(newPan + " is{0} Valid.", Luhn.CheckLuhn2(newPan) ? "" : " Not");
                    Console.WriteLine("\nDo you wish to try again? (Y or N): ");
                    loop = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                    Console.WriteLine("Do you wish to try again? (Y or N): ");
                    loop = Console.ReadLine();
                }
            }
            while (loop != "n" && loop != "N");
        }
    }

    public static class Luhn
    {
        public static bool CheckLuhn(string panDigit)
        {
            char[] panArray = panDigit.ToCharArray();
            Array.Reverse(panArray);

            int LuhnSum = 0;

            for (int i = 1; i < panArray.Length; i++)
            {
                int sum = Convert.ToInt32(panArray[i].ToString());

                if (i % 2 == 1) //odd digits after removing check bit
                {
                    sum *= 2;
                    if (sum > 9) sum -= 9;
                }
                LuhnSum += sum;
            }

            int calculatedCheckDigit = 10 - LuhnSum % 10;

            return (calculatedCheckDigit.ToString() == panArray[0].ToString());
        }

        public static bool CheckLuhn2(string panDigit)
        {
            int LuhnSum = 0;

            for (int i = (panDigit.Length - 2); i > 0; i -= 2) // even digits from the end
            {
                int sum = Convert.ToInt32(panDigit[i].ToString()) * 2;
                if (sum > 9) sum -= 9;
                LuhnSum += sum;
                LuhnSum += Convert.ToInt32(panDigit[i - 1].ToString()); //.ToString()
            }

            return (10 - (LuhnSum % 10)).ToString() == panDigit[panDigit.Length - 1].ToString();
        }
    }
}