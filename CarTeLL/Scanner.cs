using System;
using System.Globalization;

namespace CarTeLL
{
    internal class Scanner
    {

        internal static int ReadInteger(string caption)
        {
        l1:
            Console.Write(caption);

            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            goto l1;
        }
        internal static double ReadDouble(string caption)
        {
        l1:
            Console.Write(caption);

            if (double.TryParse(Console.ReadLine(), out double value))
            {
                return value;
            }
            goto l1;
        }
        internal static string ReadString(string caption)
        {
        l1:
            Console.Write(caption);
            string value = Console.ReadLine();
            if (string.IsNullOrEmpty(value))
                goto l1;

            return value;
        }
        internal static bool ReadBoolean(string caption)
        {
        l1:
            Console.Write(caption);
            bool boolean=true;
            string value=Console.ReadLine();
            if (value=="yes")
            {
                boolean = true;
            }
            else if (value == "no")
            {
                boolean= false;
            }
            else
            {
                goto l1;
            }

            return boolean;
        }
        internal static DateTime ReadDateTime(string caption)
        {
        l1:
            Console.Write($"{caption} [yyyy.MM.dd] ");

            if (DateTime.TryParseExact(Console.ReadLine(), "yyyy.MM.dd", null, DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            goto l1;
        }
    }
}
