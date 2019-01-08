using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptoTool.Utils
{
    public static class EncodingOperations
    {
        public static class Base64Operations
        {
            public static string Encode(string plainText)
            {
                var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                return Convert.ToBase64String(plainTextBytes);
            }

            public static string Decode(string base64EncodedData)
            {
                if (isBase64(base64EncodedData))
                {
                    var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
                    string sx = Encoding.UTF8.GetString(base64EncodedBytes);

                    if (!sx.Contains("�"))
                        return sx;
                }
                return "Invalid Text.";
            }
        }

        public static class ASCIIOperations
        {
            public static string Encode(string plainText)
            {
                string finalResult = string.Empty;
                byte[] encodedBytes = Encoding.ASCII.GetBytes(plainText);
                foreach (byte _byte in encodedBytes)
                {
                    finalResult += _byte + " ";
                }
                return finalResult;
            }

            public static string Decode(string ASCIIData)
            {
                if (ASCIIData != "" && ASCIIData.Last().Equals(' '))
                    ASCIIData = ASCIIData.Remove(ASCIIData.Length - 1);

                if (ASCIIData.Contains(' ') && !Regex.IsMatch(ASCIIData, @"[a-zA-Z]"))
                    try
                    {
                        byte[] byteArray = ASCIIData.Split(' ').Select(byte.Parse).ToArray();
                        return Encoding.ASCII.GetString(byteArray);
                    }
                    catch { }
                return "Invalid Text.";
            }
        }

        public static class BinaryOperations
        {
            public static string Encode(string TextData)
            {
                return string.Join(" ", TextData.Select(byt => Convert.ToString(byt, 2).PadLeft(8, '0')));
            }

            public static string Decode(string BinaryData)
            {
                if (BinaryData != "" && BinaryData.Contains(' '))
                    BinaryData = BinaryData.Replace(" ", string.Empty);

                try
                {
                    List<Byte> byteList = new List<Byte>();

                    for (int i = 0; i < BinaryData.Length; i += 8)
                    {
                        byteList.Add(Convert.ToByte(BinaryData.Substring(i, 8), 2));
                    }
                    return Encoding.ASCII.GetString(byteList.ToArray());
                }
                catch { }

                return "Invalid Text.";
            }
        }

        public static bool isBase64(this string data)
        {
            data = data.Trim();
            return (data.Length % 4 == 0) && Regex.IsMatch(data, @"^([A-Za-z0-9+/]{4})*([A-Za-z0-9+/]{4}|[A-Za-z0-9+/]{3}=|[A-Za-z0-9+/]{2}==)$", RegexOptions.None);
        }

        public static byte[] ConvertToByteArray(string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }
    }
}