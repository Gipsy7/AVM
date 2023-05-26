using ProjetoAVM.Utils;

public class Orchestrator
{
    public async Task Start()
    {

        Dictionary<string, object> content = new Dictionary<string, object>();

        content["searchTerm"] = AskAndReturnSearchTerm();
        Console.Clear();
        content["prefix"] = AskAndReturnPrefix();

        string AskAndReturnSearchTerm()
        {
            string readline = ConsoleHelper.Question("Type a search term: ");
            return readline;
        }

        string AskAndReturnPrefix()
        {
            List<string> prefixes = new List<string> {"Who is", "What is", "The history of"};
            string selectedPrefixIndex = ConsoleHelper.KeyInSelect(prefixes, "Choose one option:");

            return selectedPrefixIndex;
        }

        Console.WriteLine(content["searchTerm"]);
        Console.WriteLine(content["prefix"]);
    }
}
