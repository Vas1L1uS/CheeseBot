using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace CheeseBot
{
    internal class BackCommand : Command
    {
        private DataCommands _dataCommands;

        public BackCommand()
        {
            base._namesList = new List<string>() { "◀Назад" };
            _dataCommands = new DataCommands();
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            return _dataCommands.FindAndGetCommand(client.LastCommand)?.PreviousCommandBack.SendMessageToClientWhenBackCommand(botClient, message, client);
        }
    }
}
