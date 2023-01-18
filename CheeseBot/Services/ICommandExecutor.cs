using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CheeseBot.Services
{
    public interface ICommandExecutor
    {
        Task Execute(Update update);
    }
}