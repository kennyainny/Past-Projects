using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FI.Library.Encoding
{
    public static class BinaryUtility
    {
        /// <summary>
        /// Converts String Array To Bytes
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] ConvertStringToBytes(string str, System.Text.Encoding encoding)
        {
            return encoding.GetBytes(str);
        }
        /// <summary>
        /// Converts Bytes Array To String
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ConvertBytesToString(byte[] bytes, System.Text.Encoding encoding)
        {
            return encoding.GetString(bytes);
        }
        /// <summary>
        /// Converts a string to Hexadecimal String
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ConvertStringToHexString(string str, System.Text.Encoding encoding)
        {
            return ConvertBytesToHexString(ConvertStringToBytes(str, encoding));
        }
        /// <summary>
        /// Converts a Hexadecimal String to String
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static string ConvertHexStringToString(string str, System.Text.Encoding encoding)
        {
            return ConvertBytesToString(ConvertHexStringToBytes(str), encoding);
        }
        /// <summary>
        /// Converts hexadecimal string to bytes
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] ConvertHexStringToBytes(string str)
        {
            str = str.Replace(" ", "");
            byte[] buffer = new byte[str.Length / 2];
            for (int i = 0; i < str.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(str.Substring(i, 2), 16);
            return buffer;
        }
        /// <summary>
        /// Converts a byte array to Hexadecimal String
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertBytesToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 3);
            foreach (byte b in bytes)
            {
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0'));
            }
            return sb.ToString().ToUpper();
        }
        /// <summary>
        /// Converts Integer To Bytes
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static byte[] ConvertIntegerToBytes(int integer)
        {
            string integerString = Convert.ToString(integer, 16);
            if (integerString.Length % 2 != 0)
                integerString = integerString.PadLeft(integerString.Length + 1, '0');
            return ConvertHexStringToBytes(integerString);
            // return BitConverter.GetBytes(integer);
        }
        /// <summary>
        /// Converts byte to integer
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static int ConvertBytesToInteger(byte[] bytes)
        {
            string integerString = ConvertBytesToHexString(bytes);
            int i = Convert.ToInt32(integerString, 16);
            return i;
            //if (BitConverter.IsLittleEndian)
            //    Array.Reverse(bytes);
            //int i = BitConverter.ToInt32(bytes, 0);
            //return i;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static string ConvertIntegerToHexString(int integer)
        {
            string integerString = Convert.ToString(integer, 16);
            if (integerString.Length % 2 != 0) integerString = "0" + integerString;
            return integerString;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static int ConvertHexStringToInteger(string hexString)
        {
            return Convert.ToInt32(hexString, 16);
        }
        /// <summary>
        /// Combines one or more keys using XORing.
        /// </summary>
        /// <param name="args">List of keys to Xor.</param>
        /// <returns>byte array.</returns>
        public static byte[] Xor(params object[] args)
        {
            try
            {
                if (args.Length == 1)
                    return (byte[])args[0];
                else if (args.Length > 1)
                {
                    int expectedLength = ((byte[])args[0]).Length;
                    for (int i = 0; i < args.Length; i++)
                    {
                        byte[] arg = (byte[])args[i];
                        if (arg.Length != expectedLength)
                            throw new Exception();
                    }
                    byte[] args1 = (byte[])args[0];
                    byte[] args2 = (byte[])args[1];
                    byte[] args3 = new byte[args1.Length];
                    for (int i = 0; i < args1.Length; i++)
                    {
                        int charValue = Convert.ToInt32(args1[i]); //get the ASCII value of the character
                        int charValue2 = Convert.ToInt32(args2[i]);
                        charValue ^= charValue2; //xor the value
                        args3[i] = Convert.ToByte(charValue);
                    }
                    object[] arguments = new object[args.Length - 1];
                    arguments[0] = args3;
                    Array.Copy(args, 2, arguments, 1, arguments.Length - 1);
                    return Xor(arguments);
                }
                else
                    throw new Exception();
            }
            catch { throw new Exception(); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="initialObject"></param>
        /// <returns></returns>
        public static byte[] ConvertObjectToBytes(object initialObject)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter encoder = new BinaryFormatter();
            encoder.Serialize(ms, initialObject);
            return ms.ToArray();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encodedObjectBytes"></param>
        /// <returns></returns>
        public static object ConvertBytesToObject(byte[] encodedObjectBytes)
        {
            MemoryStream ms = new MemoryStream(encodedObjectBytes);
            BinaryFormatter encoder = new BinaryFormatter();
            return (object)encoder.Deserialize(ms);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="matchBytes1"></param>
        /// <param name="matchBytes2"></param>
        /// <returns></returns>
        public static bool IsMatch(byte[] matchBytes1, byte[] matchBytes2)
        {
            if (matchBytes1.Length != matchBytes2.Length)
                return false;
            for (int i = 0; i < matchBytes1.Length; i++)
            {
                if (matchBytes1[i] != matchBytes2[i])
                    return false;
            }
            return true;
        }
    }
}
