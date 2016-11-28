using System;
using System.Collections;
using System.Collections.Generic;

namespace TestEnigma
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string rotor1 = Utils.IniReadValue("ENIGMA", "ROTOR1");
            string rotor2 = Utils.IniReadValue("ENIGMA", "ROTOR2");
            string rotor3 = Utils.IniReadValue("ENIGMA", "ROTOR3");
            string[] pb_temp = Utils.IniReadValue("ENIGMA", "PATCHBOARDS").Split(',');
            Dictionary<char, char> patchBoards = new Dictionary<char, char>();
            foreach (string item in pb_temp)
            {
                patchBoards.Add(item[0], item[1]);
            }
            string result = string.Empty;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Create By CxzLabel");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please input mode  (encrypt or decrypt)");
            Console.ForegroundColor = ConsoleColor.Gray;
            string type = Console.ReadLine();
            if (type == "encrypt")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("enter ENCRYPT Mode");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Please enter the context");
                Console.ForegroundColor = ConsoleColor.Gray;
                string textInfo = Console.ReadLine();
                result = EnigmaConveter.ENCRYPT(textInfo, rotor1, rotor2, rotor3, patchBoards);
            }
            else if (type == "decrypt")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("enter DECRYPT Mode");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Please enter the cipherText");
                Console.ForegroundColor = ConsoleColor.Gray;
                string textInfo = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Gray;
                result = EnigmaConveter.DECRYPT(textInfo, rotor1, rotor2, rotor3, patchBoards);
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("result:");
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Read();
        }
    }
}