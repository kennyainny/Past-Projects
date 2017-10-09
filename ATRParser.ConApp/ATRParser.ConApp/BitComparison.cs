using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATRParser.ConApp
{
    public static class BitComparison
    {
        [Flags]
        public enum ConventionBits : int
        {
            DirectConvention = 0x3B, // 1000
            InverseConvention = 0x3F, // 0100        
        }

        public static string GetConvention(int atrByte)
        {
            ConventionBits _atrByte = (ConventionBits)atrByte;

            if ((_atrByte & ConventionBits.DirectConvention) == ConventionBits.DirectConvention)
                return "Direct Convention is supported";
            if ((_atrByte & ConventionBits.InverseConvention) == ConventionBits.InverseConvention)
                return "Inverse Convention is supported";

            return "Convention type not recognised";
        }

        public static bool IsFlagSet(FlaggableBits bitToCheck, FlaggableBits bitFlag)
        {
            return bitToCheck.HasFlag(bitFlag);
        }

        //public BitComparison()
        //{
        //    //FlaggableBits bitsToCheck = FlaggableBits.TDFlag | FlaggableBits.TCFlag;

        //    /// Example 1: 1100 & 0010 = 0000 != 0010
        //    /// Flaggable bits.
        //    if ((bitsToCheck & FlaggableBits.TDFlag) == FlaggableBits.TDFlag) Console.WriteLine("Option 1 checked.");

        //    /// Example 2: Bitwise shift LEFT or RIGHT
        //    /// T0 bits: [1110] TDFlag-TCFlag-TBFlag-TAFlag [1111] Historical Bytes Length
        //    int integerValue = 3; // 00 00 00 00 11
        //    integerValue = integerValue << 1; // 00 00 00 01 10
        //    integerValue = integerValue >> 2; // 00 00 00 00 01
        //    integerValue = integerValue << 1; // 00 00 00 00 10

        //    int T0Bits = 0xEF;
        //    FlaggableBits Ta2dFlags = (FlaggableBits)(T0Bits >> 4); // 0000 1110
        //    if ((Ta2dFlags & FlaggableBits.TAFlag) == FlaggableBits.TAFlag) Console.Write("TA Present");

        //    /// Example 3: Masking
        //    /// Step 1: 1101 0010 Or 1111 0000 | Mark all places to be removed with "1" and places to be left with "0"
        //    /// Step 2: 1111 0010 And 0000 1111 | And with the Mask's inverse.
        //    int toBeMaskedBits = 0xD2;
        //    int maskToBeUsed = 0xf0;
        //    int maskedData = (toBeMaskedBits | maskToBeUsed) & ~maskToBeUsed;

        //    int historicalDataLength = (T0Bits | maskToBeUsed) & ~maskToBeUsed;
        //}

    }
}
