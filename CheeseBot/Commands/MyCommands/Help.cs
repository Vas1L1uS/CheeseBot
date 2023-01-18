using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;

namespace CheeseBot
{
    internal class Help : Command
    {
        public Help()
        {
            base._namesList = new List<string>() { "/help", "Помощь" , "помощь", "Help" , "help"};       
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Если у вас возникла проблема, то перезапустите бота введя /start, но учтите, что ваша корзина продуктов будет сброшена." +
                $"\nВсе команды следует вводить через кнопки, которые находятся снизу. К командам, которые можно вводить вручную относятся;" +
                $"\n/start - Перезапуск бота" +
                $"\n/help - Вызов команды помощи" +
                $"\n\nБот находится в тестовой версии, так что некоторые команды могут быть недоступны.", replyMarkup: null)
            };
            return messages;
        }
    }
}
