using FI.Library.Encoding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATRParser.ConApp
{
    public static class ATRParser
    {
        //string _ATR, _initialXter;

        //Hex _atrHex;
        //byte[] atrBytes;

        //LinkedList<ATRByte> _ATRBytes;
        //public ATRParser(string atrStr)
        //{
        //    //_ATR = atrStr;

        //    //_atrHex = new Hex(atrStr);
        //    //atrBytes = _atrHex.ToBytes();

        //}

        public static Dictionary<string, object> ParseATR(string atrStr)
        {
            Dictionary<string, object> atrParsedResult = new Dictionary<string, object>();

            if (atrStr.Length > 1)
            {
                List<ATRByte> _ATRBytes = new List<ATRByte>();

                byte[] atrByteArray = BinaryUtility.ConvertHexStringToBytes(atrStr);
                MemoryStream msByte = new MemoryStream(atrByteArray);
                int readByte = msByte.ReadByte();

                atrParsedResult.Add("ATR Value: ", atrStr);
                atrParsedResult.Add("No of ATR Bytes: ", atrByteArray.Length);

                atrParsedResult.Add("\nInitial Character - TS: ", readByte.ToString("X2")); //X2
                atrParsedResult.Add("\t", BitComparison.GetConvention(readByte));

                ATRByte atrByte;
                int i = 0;

                //read each byte from stream
                do
                {
                    readByte = msByte.ReadByte();
                    atrByte = new ATRByte(readByte, "T" + i);
                    
                    //check if interface bytes are in the stream
                    int noOfIfByte = atrByte.GetNoOfInterfaceCharacter();
                    if(noOfIfByte>0)
                    {
                        byte[] ifBytes = new byte[noOfIfByte];
                        int bytesRead = msByte.Read(ifBytes, 0, noOfIfByte);

                        atrByte.SetInterfaceBytes(ifBytes);                        
                    }
                    else
                    {
                        //no Interface byte, get historical data
                    }

                    _ATRBytes.Add(atrByte);
                    //atrParsedResult.Add("Added byte - T" + i + ": ", readByte.ToString("X")); //X2
                    i++;
                }
                while (atrByte.HasProtocolByte());

                //ptrint the analysis values
                foreach (var atrByteVal in _ATRBytes)
                {
                    var artDictList = atrByteVal.ParseInterfaceBytes();
                    foreach (var dictItem in artDictList)
                    {
                        atrParsedResult.Add(dictItem.Key, dictItem.Value);
                    }
                }

                //get no of historical byte
                int streamLength = (int)msByte.Length;
                int streamPosition = (int)msByte.Position;
                int noOfHistoricalByte = streamLength - streamPosition;
                byte[] byteArray = new byte[noOfHistoricalByte];
                int val = msByte.Read(byteArray, 0, noOfHistoricalByte);

                //print historical byte
                Hex historicalByte = new Hex(byteArray);
                atrParsedResult.Add("\nNo of bytes read: ", val);
                atrParsedResult.Add("Historical Byte: ", historicalByte.ToString());
                //atrParsedResult.Add("Stream to Byte Array: ", msByte.ToArray().Length);

            }
            else
            {
                atrParsedResult.Add("Error - ", "No ATR detected!!!");

            }

            return atrParsedResult;
        }
    }
}



//int i = 1;
//foreach(var val in Conversion.HexArray(_ATR))
//{
//    atrParsedResult.Add(i + ": ", val);
//    i++;
//}

//atrParsedResult.Add("No of Bytes: ", atrBytes.Length.ToString());
//atrParsedResult.Add("TS - : ", atrBytes[1].ToString());
//atrParsedResult.Add("No of Hex: ", _atrHex.Count.ToString());
//atrParsedResult.Add("ATR Hex: ", _atrHex.ToString());

//protocol type T=0 only is supported (character-oriented asynchronous transmission protocol),
//atrBytes = _atrHex.ToBytes();

//string hexStr = BinaryUtility.ConvertStringToHexString(_ATR, Encoding.ASCII);
//atrParsedResult.Add("String: ", _ATR);
//atrParsedResult.Add("ASCII: ", BinaryUtility.ConvertStringToHexString(_ATR, Encoding.ASCII));
//atrParsedResult.Add("Unicode: ", BinaryUtility.ConvertStringToHexString(_ATR, Encoding.Unicode));
//atrParsedResult.Add("UTF8: ", BinaryUtility.ConvertStringToHexString(_ATR, Encoding.UTF8));
//var val = Conversion.HexArray(_ATR);

