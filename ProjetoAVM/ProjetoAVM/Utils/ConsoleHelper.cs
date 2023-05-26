using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAVM.Utils
{
    static public class ConsoleHelper
    {
        static public string Question(string text)
        {
            Console.WriteLine(text);
            return Console.ReadLine();
        }

        static public string KeyInSelect(List<string> keys,string text)
        {
            Console.WriteLine(text);
            for(int i = 0; i <keys.Count; i++)
            {
                Console.WriteLine($"[{i+1}]" + keys[i]);
            }
            ConsoleKeyInfo keyInfo= Console.ReadKey();

            int selectedOption;

            if (int.TryParse(keyInfo.KeyChar.ToString(), out selectedOption))
            {
                for (int i = 0; i < keys.Count; i++)
                {
                    if (selectedOption - 1 == i)
                    {
                        return keys[i];
                    }
                }
            }
            else
            {
                return "";
            }

            return "";
        }
    }
}
