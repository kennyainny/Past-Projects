using System;
using System.Collections.Generic;
using System.Text;
using FI.Library.Encoding;

namespace FI.Library.Smartcard
{
    //public class CardSimulator
    //{
    //    byte[] responseData;

    //    byte[] CardKeyDiversificationData;
    //    byte[] CardKeyInformationData;
    //    byte[] CardChallenge;

    //    public string ATR = "3B6800000073C84011009000";

    //    public void ReadCard(byte cla, byte ins, byte p1, byte p2, byte length, out byte[] data, out byte sw1, out byte sw2)
    //    {
    //        string cmd = Convert.ToString(cla, 16) + Convert.ToString(ins, 16) + Convert.ToString(p1, 16) + Convert.ToString(p2, 16);
    //        data = new byte[] { };

    //        switch (cmd)
    //        {
    //            case "00C00000":
    //                if (length != responseData.Length) { sw1 = 0x67; sw2 = 0; }
    //                else { data = responseData; sw1 = 0x90; sw2 = 0x90; }
    //                break;
    //            case "80CA9F7D":
    //                if (length != 0x13) { sw1 = 0x6c; sw2 = 0x2a; }
    //                else data = BinaryUtility.ConvertHexStringToBytes("9F7D1032303032303220565344432032343020"); sw1 = 0x90; sw2 = 0;
    //                break;
    //            case "80CA9F7F":
    //                if (length != 0x2a) { sw1 = 0x6c; sw2 = 0x13; }
    //                else data = BinaryUtility.ConvertHexStringToBytes("9F7F2A323030323032205653444320323430200000000000000000000000000000000000000000000000000000"); sw1 = 0x90; sw2 = 0;
    //                break;
    //            default:
    //                sw1 = 0x6d; sw2 = 0;
    //                break;
    //        }
    //        responseData = new byte[] { };
    //    }
    //    public void WriteCard(byte cla, byte ins, byte p1, byte p2, byte[] data, out byte sw1, out byte sw2)
    //    {
    //        string cmd = Convert.ToString(cla, 16) + Convert.ToString(ins, 16) + Convert.ToString(p1, 16) + Convert.ToString(p2, 16);

    //        switch (cmd)
    //        {
    //            case "80500000":
    //                InitializeUpdate(data, out sw1, out sw2);
    //                break;
    //            case "84820000":
    //                ExternalAuthenticate(data, out sw1, out sw2);
    //                break;
    //            case "00A40400":
    //                responseData = BinaryUtility.ConvertHexStringToBytes("0102030405060708091001020304050607080910");
    //                sw1 = 0x61;
    //                sw2 = BinaryUtility.ConvertIntegerToBytes(responseData.Length)[0];
    //                break;
    //            case "80F28000":
    //                responseData = BinaryUtility.ConvertHexStringToBytes("08A000000003000000079E");
    //                sw1 = 0x61;
    //                sw2 = BinaryUtility.ConvertIntegerToBytes(responseData.Length)[0];
    //                break;
    //            case "80F24000":
    //                responseData = BinaryUtility.ConvertHexStringToBytes("07A0000000032010071007FF0000A00600020704");
    //                sw1 = 0x61;
    //                sw2 = BinaryUtility.ConvertIntegerToBytes(responseData.Length)[0];
    //                break;
    //            case "80F22000":
    //                responseData = BinaryUtility.ConvertHexStringToBytes("07A0000000032010071007FF0000A00600020704");
    //                sw1 = 0x61;
    //                sw2 = BinaryUtility.ConvertIntegerToBytes(responseData.Length)[0];
    //                break;
    //            case "80400000":
    //                sw1 = 0x90;
    //                sw2 = 0;
    //                break;
    //            default:
    //                sw1 = 0x90; sw2 = 0;
    //                break;
    //        }
            
    //    }

    //    void InitializeUpdate(byte[] data, out byte sw1, out byte sw2)
    //    {
    //        sw1 = 0x64;
    //        sw2 = 0;

    //        CardKeyDiversificationData = BinaryUtility.ConvertHexStringToBytes("00001217002212956803");
    //        CardKeyInformationData = BinaryUtility.ConvertHexStringToBytes("FF02"); // "0101";
    //        CardChallenge = BinaryUtility.ConvertHexStringToBytes("61CAB33D1D9414C9");
    //        responseData = BinaryUtility.ConvertHexStringToBytes("00001217002212956803FF02001561CAB33D1D9414C9F205A1F57CFB");
    //        sw1 = 0x61; sw2 = 0x1c;
    //    }
    //    void ExternalAuthenticate(byte[] data, out byte sw1, out byte sw2)
    //    {
    //        sw1 = 0x64;
    //        sw2 = 0;

    //        string HostCryptogram = BinaryUtility.ConvertBytesToHexString(data).Substring(0, 16);
    //        string Mac = BinaryUtility.ConvertBytesToHexString(data).Substring(16, 16);

    //        if (HostCryptogram == "0F1EB0B389E5A237" && Mac == "1FF42DD8122EDA6A")
    //        {
    //            sw1 = 0x90; sw2 = 0;
    //        }
    //        else { sw1 = 0x69; sw2 = 0x85; }
    //    }
    //}
}
