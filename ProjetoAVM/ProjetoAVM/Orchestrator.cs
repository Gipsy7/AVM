using ProjetoAVM.Entities;
using ProjetoAVM.Robots;
using ProjetoAVM.User;
using ProjetoAVM.Utils;

public class Orchestrator
{
    static public async Task Start()
    {
        Content content = new Content();

        new UserInput(content);
        await new TxRobot(content).StartTxRobot();
    }
}
