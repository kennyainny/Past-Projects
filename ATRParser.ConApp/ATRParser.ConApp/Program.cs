using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATRParser.ConApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var atrValue = "3B3B96004025310000FFFF33239000";
            //var atrValue1 = "3B F6 18 00 FF 81 31 FE 45 4A 43 4F 50 33 31 0E".Trim(' ');
            
            Dictionary<string, object> _parsedATR = ATRParser.ParseATR(atrValue);

            foreach (var ATR_KeyValue in _parsedATR)
            {
                Console.WriteLine(ATR_KeyValue.Key + ATR_KeyValue.Value);
            }

            Console.ReadLine();
        }
    }
}