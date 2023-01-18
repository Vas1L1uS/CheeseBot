using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace CheeseBot
{
    internal class GoToNextStep : Command
    {
        public GoToNextStep()
        {
            base._namesList = new List<string>() { "Перейти на следующий шаг➡" };
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            if (client.ProductsBasket.Count == 0)
            {
                Task<Message>[] messages1 = new Task<Message>[]
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Вам необходимо добавить хотя-бы один товар для перехода на следующий шаг оформелния!")
                };
                return messages1;
            }
            else
            {
                client.SelectedProduct = null;
                client.LastCommand = DataCommands.MyAddToBasket.NamesList.First();
                Task<Message>[] messages1 = new Task<Message>[]
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Выберите команду из предложенного ниже списка", replyMarkup: DataCommands.MyAddToBasket.GetButtons())
                };
                return messages1;
            }
        }
    }
}
