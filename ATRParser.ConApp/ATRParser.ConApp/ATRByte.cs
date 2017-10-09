using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATRParser.ConApp
{
    [Flags]
    public enum FlaggableBits : int
    {
        TDFlag = 0x80, // 1000 0000
        TCFlag = 0x40, // 0100 0000
        TBFlag = 0x20, // 0010 0000
        TAFlag = 0x10  // 0001 0000
    }

    public class ATRByte
    {
        string _byteName;
        byte[] _interfaceBytes;
        int _atrByteInt, _noOfInterfaceXter;
        FlaggableBits _atrByteEnum;
        static bool TA = false, TB = false, TC = false, TD = false;

        Dictionary<int, Tuple<string, string>> ClockRateConversionFactor
            = new Dictionary<int, Tuple<string, string>>() //<F(byte), Fi(string), f(string)>
        {
            {0000, new Tuple<string, string>("372", "4")}, {0001, new Tuple<string, string>("372", "5")},
            {0010, new Tuple<string, string>("558", "6")}, {0011, new Tuple<string, string>("744", "8")},
            {0100, new Tuple<string, string>("1116", "12")}, {0101, new Tuple<string, string>("1488", "16")},
            {0110, new Tuple<string, string>("1860", "20")}, {0111, new Tuple<string, string>("RFU", "-")},
            {1000, new Tuple<string, string>("RFU", "-")}, {1001, new Tuple<string, string>("512", "5")},
            {1010, new Tuple<string, string>("768", "7.5")}, {1011, new Tuple<string, string>("1024", "10")},
            {1100, new Tuple<string, string>("1536", "15")}, {1101, new Tuple<string, string>("2048", "20")},
            {1110, new Tuple<string, string>("RFU", "-")}, {1111, new Tuple<string, string>("RFU", "-")}
        };

        Dictionary<int, string> BaudRateAdjustmentFactor = new Dictionary<int, string>()
        {
            {0000, "RFU"},{0001, "1"},{0010, "2"},{0011, "4"},{0100, "8"},{0101, "16"},
            {0110, "32"},{0111, "RFU"},{1000, "12"},{1001, "20"},{1010, "RFU"},{1011, "RFU"},
            {1100, "RFU"},{1101, "RFU"},{1110, "RFU"},{1111, "RFU"}
        };

        public ATRByte(int xterByte, string byteName)
        {
            _atrByteInt = xterByte;
            _atrByteEnum = (FlaggableBits)xterByte;
            _byteName = byteName;
        }

        public int GetNoOfInterfaceCharacter()
        {
            _noOfInterfaceXter = 0;

            if (_atrByteEnum.HasFlag(FlaggableBits.TAFlag))
            {
                TA = true;
                _noOfInterfaceXter++;
            }
            if (_atrByteEnum.HasFlag(FlaggableBits.TBFlag))
            {
                TB = true;
                _noOfInterfaceXter++;
            }
            if (_atrByteEnum.HasFlag(FlaggableBits.TCFlag))
            {
                TC = true;
                _noOfInterfaceXter++;
            }
            if (_atrByteEnum.HasFlag(FlaggableBits.TDFlag))
            {
                TD = true;
               // _noOfInterfaceXter++;
            }

            return _noOfInterfaceXter;
        }

        public void SetInterfaceBytes(byte[] ifBytes)
        {
            _interfaceBytes = new byte[ifBytes.Length];
            Array.Copy(ifBytes, _interfaceBytes, ifBytes.Length);
        }

        public bool HasProtocolByte()
        {
            return TD;
        }

        public Dictionary<string, object> ParseInterfaceBytes()
        {
            Dictionary<string, object> parsedByteResult = new Dictionary<string, object>();

            switch (_byteName)
            {
                #region Format Character - T0

                case "T0":
                    {
                        parsedByteResult.Add("\n" + _byteName + ": ", _atrByteInt.ToString("X2") + " (Format Character)");

                        if (!TD) parsedByteResult.Add("\tT=0 only", " (Protocol type)");

                        if (TA)
                        {
                            int TAByte = _interfaceBytes[0];
                            parsedByteResult.Add("\nTA1: ", TAByte.ToString("X2") + " (Interface Character)");

                            //calculate Fi - Clock rate convertion factor, b8 - b5
                            int maskToBeUsed = 0x0F; //000001111 = ~11110000
                            int msNibble = (TAByte | maskToBeUsed) & ~maskToBeUsed;
                            msNibble = msNibble >> 4;
                            parsedByteResult.Add("\tFi: ", msNibble + " (Clock rate convertion factor)");

                            //calculate Di - Bit rate adjustment factor, b4 - b1
                            maskToBeUsed = 0xF0; //11110000 = ~00001111
                            int lsNibble = (TAByte | maskToBeUsed) & ~maskToBeUsed;
                            parsedByteResult.Add("\tDi: ", lsNibble + " (Bit rate adjustment factor)");

                        }

                        if (TB)
                        {
                            int TBByte;

                            if (TA) //
                                TBByte = _interfaceBytes[1];
                            else TBByte = _interfaceBytes[0];

                            parsedByteResult.Add("\nTB1: ", TBByte.ToString("X2") + " (Interface Character - Vpp & Ipp)");

                            //calculate II1 - Maximum Programming Current b7 & b61, e.g. 0 10 11101 
                            int maskToBeUsed = 0x9F; //10011111 = ~01100000
                            int msNibble = (TBByte | maskToBeUsed) & ~maskToBeUsed;
                            msNibble = msNibble >> 5;
                            parsedByteResult.Add("\tII1: ", msNibble + " (Max Programming Current factor)");

                            //calculate PI1 - Programming Voltage b5 - b1, e.g. 011 10011
                            maskToBeUsed = 0xE0; //11100000 = ~00011111
                            int lsNibble = (TBByte | maskToBeUsed) & ~maskToBeUsed;
                            parsedByteResult.Add("\tPI1: ", lsNibble + " (Programming Voltage factor)");

                            if (TBByte.Equals(0))
                                parsedByteResult.Add("\n\tTB1 = 00", " (Vpp is not connected in ICC)");
                        }

                        if (TC)
                        {
                            int TCByte;

                            if (TA && TB) //
                                TCByte = _interfaceBytes[2];
                            else if (TA && !TB) //
                                TCByte = _interfaceBytes[1];
                            else if (!TA && TB) //
                                TCByte = _interfaceBytes[1];
                            else TCByte = _interfaceBytes[0];

                            parsedByteResult.Add("\nTC1: ", TCByte.ToString("X2") + " (Interface Character -Extra Guard Time (N))");

                            if (TCByte != 255)
                                parsedByteResult.Add("\tN = ", TCByte + " etus of extra guardtime)"); // shall be added to minimum character duration
                            else
                            {
                                if (TD) //T=1 Protocol, N = 11 etus
                                    parsedByteResult.Add("\tN = ", "11 etus of extra guardtime)"); // shall be added to minimum character duration
                                else //T=0 Protocol, N = 12 etus
                                    parsedByteResult.Add("\tN = ", "12 etus of extra guardtime)");  // shall be added to minimum character duration
                            }
                        }
                    }
                    break;
                #endregion Format Character - T0 End

                #region Interface Character - T1/TD1

                case "T1":
                    {
                        parsedByteResult.Add("\nTD1: ", _atrByteInt.ToString("X2") + " (Interface Character)");
                        int T1_Protocol = 0x81;

                        if (_atrByteInt == T1_Protocol) parsedByteResult.Add("\tT=1 only", " (Protocol type)");

                        if (TA)
                        {
                            int TAByte = _interfaceBytes[0];
                            parsedByteResult.Add("\nTA1: ", TAByte.ToString("X2") + " (Interface Character)");

                            //calculate Fi - Clock rate convertion factor, b8 - b5
                            int maskToBeUsed = 0x0F; //000001111 = ~11110000
                            int msNibble = (TAByte | maskToBeUsed) & ~maskToBeUsed;
                            msNibble = msNibble >> 4;
                            parsedByteResult.Add("\tFi: ", msNibble + " (Clock rate convertion factor)");

                            //calculate Di - Bit rate adjustment factor, b4 - b1
                            maskToBeUsed = 0xF0; //11110000 = ~00001111
                            int lsNibble = (TAByte | maskToBeUsed) & ~maskToBeUsed;
                            parsedByteResult.Add("\tDi: ", lsNibble + " (Bit rate adjustment factor)");

                        }

                        if (TB)
                        {
                            int TBByte;

                            if (TA) //
                                TBByte = _interfaceBytes[1];
                            else TBByte = _interfaceBytes[0];

                            parsedByteResult.Add("\nTB1: ", TBByte.ToString("X2") + " (Interface Character - Vpp & Ipp)");

                            //calculate II1 - Maximum Programming Current b7 & b61, e.g. 0 10 11101 
                            int maskToBeUsed = 0x9F; //10011111 = ~01100000
                            int msNibble = (TBByte | maskToBeUsed) & ~maskToBeUsed;
                            msNibble = msNibble >> 5;
                            parsedByteResult.Add("\tII1: ", msNibble + " (Max Programming Current factor)");

                            //calculate PI1 - Programming Voltage b5 - b1, e.g. 011 10011
                            maskToBeUsed = 0xE0; //11100000 = ~00011111
                            int lsNibble = (TBByte | maskToBeUsed) & ~maskToBeUsed;
                            parsedByteResult.Add("\tPI1: ", lsNibble + " (Programming Voltage factor)");

                            if (TBByte.Equals(0))
                                parsedByteResult.Add("\n\tTB1 = 00", " (Vpp is not connected in ICC)");
                        }

                        if (TC)
                        {
                            int TCByte;

                            if (TA && TB) //
                                TCByte = _interfaceBytes[2];
                            else if (TA && !TB) //
                                TCByte = _interfaceBytes[1];
                            else if (!TA && TB) //
                                TCByte = _interfaceBytes[1];
                            else TCByte = _interfaceBytes[0];

                            parsedByteResult.Add("\nTC1: ", TCByte.ToString("X2") + " (Interface Character -Extra Guard Time (N))");

                            if (TCByte != 255)
                                parsedByteResult.Add("\tN = ", TCByte + " etus of extra guardtime)"); // shall be added to minimum character duration
                            else
                            {
                                if (TD) //T=1 Protocol, N = 11 etus
                                    parsedByteResult.Add("\tN = ", "11 etus of extra guardtime)"); // shall be added to minimum character duration
                                else //T=0 Protocol, N = 12 etus
                                    parsedByteResult.Add("\tN = ", "12 etus of extra guardtime)");  // shall be added to minimum character duration
                            }
                        }
                    }
                    break;
                #endregion Interface Character - T1 End

                default:
                    break;
            }

            return parsedByteResult;
        }


        /// Example 3: Masking
        //    /// Step 1: 1101 0010 Or 1111 0000 | Mark all places to be removed with "1" and places to be left with "0"
        //    /// Step 2: 1111 0010 And 0000 1111 | And with the Mask's inverse.
        //    int toBeMaskedBits = 0xD2; //
        //    int maskToBeUsed = 0xf0;
        //    int maskedData = (toBeMaskedBits | maskToBeUsed) & ~maskToBeUsed;

        //    int historicalDataLength = (T0Bits | maskToBeUsed) & ~maskToBeUsed;

        //public override string ToString()
        //{
        //    if (_characterName == "T0")
        //    {
        //        return "";
        //    }
        //    else
        //    {

        //        return "";
        //    }
        //}

    }
}
