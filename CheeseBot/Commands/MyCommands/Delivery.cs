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
    internal class Delivery : Command
    {
        public Delivery()
        {
            base._namesList = new List<string>() { "Доставка" };
            base.PreviousCommandBack = DataCommands.MyContinueCheckout;
        }


        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            client.TypeDelivery = Client.TypesDelivery.Delivery;
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Доставка осуществляется только в районы, которые представлены снизу" +
                $"\nВыберите ваш район",
                replyMarkup: this.GetButtons()),
            };
            return messages;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{new KeyboardButton("Коньялты"), new KeyboardButton("Кепез")},
                new List<KeyboardButton>{new KeyboardButton("Муратпаша"), new KeyboardButton("Лара")},
                new List<KeyboardButton>{ new KeyboardButton(DataCommands.MyBackCommand.NamesList.First())}
            };

            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}