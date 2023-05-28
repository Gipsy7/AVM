using ProjetoAVM.Entities;
using ProjetoAVM.Robots;
using ProjetoAVM.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoAVM.User
{
    public class UserInput
    {
        public UserInput() { }
        public UserInput(Content content) 
        {
            content.SerachTerm = AskAndReturnSearchTerm();
            Console.Clear();
            content.Prefix = AskAndReturnPrefix();
            Console.Clear();

            string AskAndReturnSearchTerm()
            {
                string readline = ConsoleHelper.Question("Type a search term: ");
                return readline;
            }

            string AskAndReturnPrefix()
            {
                List<string> prefixes = new List<string> { "Who is", "What is", "The history of" };
                string selectedPrefixIndex = ConsoleHelper.KeyInSelect(prefixes, "Choose one option:");

                return selectedPrefixIndex;
            }
        }
    }
}
