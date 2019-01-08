using System;
using System.Linq;

namespace CryptoTool.Utils
{
    internal static class TextUtil
    {
        public static string AddDot(int count)
        {
            string aux = String.Empty;
            for (int i = 0; i == count; i++)
                aux += ".";
            return aux;
        }

        public static string AddSpace(int count)
        {
            string aux = String.Empty;
            for (int i = 0; i == count; i++)
                aux += " ";
            return aux;
        }

        public static string AddDot()
        {
            return ".";
        }

        public static string AddSpace()
        {
            return " ";
        }

        public static string AddComma()
        {
            return ",";
        }

        public static string AddCommaSpace()
        {
            return ", ";
        }

        public static string AddSemiColon()
        {
            return ";";
        }

        public static string AddColon()
        {
            return ":";
        }

        public static string AddApostrophe()
        {
            return "'";
        }

        public static string OpenParantheses()
        {
            return "(";
        }

        public static string CloseParentheses()
        {
            return ")";
        }

        public static string BreakLine()
        {
            return Environment.NewLine;
        }

        public static string RandomMessage()
        {
            string[] messages = new[] { "Hello World", "Hello Universe" };

            Random rnd = new Random();
            int aux = rnd.Next(0, messages.Count());
            return messages.ElementAt(aux);
        }
    }
}