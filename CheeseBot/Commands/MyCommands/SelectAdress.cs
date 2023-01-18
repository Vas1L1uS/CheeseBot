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
    internal class SelectAdress : Command
    {
        public SelectAdress()
        {
            base._namesList = new List<string>() { "Коньялты", "Кепез", "Муратпаша", "Лара" };
            base.PreviousCommandBack = DataCommands.MyDelivery;
        }


        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            client.Area = message.Text;
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Отправьте геопозицию для указания адреса доставки. Для этого нажмите на скрепку рядом с полем ввода и снизу выберите пункт \"Геопозиция\" и нажмите \"Отправить свою геопозицию\", или вы можете выбрать вручную точку на карте и нажать \"Отправить выбранную геопозицию\"" +
                $"\nЕсли нет такой возможности, то можете вручную ввести адрес доставки.",
                replyMarkup: this.GetButtons()),
            };
            return messages;
        }

        public override Task<Message>[] SendMessageToClientWhenBackCommand(TelegramBotClient botClient, Message message, Client client)
        {
            if (client.TypeDelivery == Client.TypesDelivery.Pickup)
            {
                return DataCommands.MyPickup.SendMessageToClient(botClient, message, client);
            }

            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Отправьте геопозицию для указания адреса доставки. Для этого нажмите на скрепку рядом с полем ввода и снизу выберите пункт \"Геопозиция\" и нажмите \"Отправить свою геопозицию\", или вы можете выбрать вручную точку на карте и нажать \"Отправить выбранную геопозицию\"" +
                $"\nЕсли нет такой возможности, то можете вручную ввести адрес доставки.",
                replyMarkup: this.GetButtons()),
            };
            client.LastCommand = this._namesList.First();
            return messages;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{ new KeyboardButton(DataCommands.MyBackCommand.NamesList.First())}
            };

            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}
