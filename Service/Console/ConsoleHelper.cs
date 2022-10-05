using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Primark.MpeTestingSuite.MpeMetricsCollectionApp
{
    public class ConsoleHelper
    {
        private ConsoleColor DefaultColor = ConsoleColor.White;
        public void WriteRegular(string text)
        {
            WriteWithColor(text, DefaultColor);
        }

        public void WriteWithColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = DefaultColor;
        }
        public void WriteRed(string text)
        {
            Console.ForegroundColor =  ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = DefaultColor;
        }
        public void WriteYellow(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = DefaultColor;
        }

        public void WriteCyan(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(text);
            Console.ForegroundColor = DefaultColor;
        }
        public void WriteGreen(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ForegroundColor = DefaultColor;
        }
        public void Enter()
        {
            Console.WriteLine("");
        }

    }
}
