using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace CheeseBot
{
    internal class StartCommand : Command
    {
        public StartCommand()
        {
            base._namesList = new List<string>() { "/start", "Старт", "Перезапуск", "Перезагрузка" };
        }


        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            client.ProductsBasket.Clear();
            client.SelectedProduct = null;
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Здравствуйте, {message.From.FirstName}! " +
                $"\nВсе команды следует вводить через кнопки, которые находятся снизу. К командам, которые можно вводить вручную относятся;" +
                $"\n/start - Перезапуск бота" +
                $"\n/help - Вызов команды помощи" +
                $"\n\nБот находится в тестовой версии, так что некоторые команды могут быть недоступны.",
                replyMarkup: this.GetButtons()),
            };
            return messages;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{new KeyboardButton(DataCommands.MyViewListProducts.NamesList.First())},
                new List<KeyboardButton>{new KeyboardButton(DataCommands.MyFAQ.NamesList.First())}
            };

            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}
