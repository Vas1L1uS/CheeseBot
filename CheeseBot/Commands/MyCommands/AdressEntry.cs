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
    internal class AdressEntry : Command
    {
        public AdressEntry()
        {
            base.NamesList = new List<string>() { "Мой адрес" };
            base.PreviousCommandBack = DataCommands.MySelectAdress;
        }


        public async Task SendMessageToClientWhenLocation(TelegramBotClient botClient, Message message, Client client)
        {
            client.Location = message.Location;
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Местоположение принято!" +
                $"\nВведите ваше Имя.",
                replyMarkup: this.GetButtons());
        }

        public async Task SendMessageToClientWhenAdress(TelegramBotClient botClient, Message message, Client client)
        {
            client.Adress = message.Text;
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Адрес принят!" +
                $"\nВведите ваше Имя.",
                replyMarkup: this.GetButtons());
        }

        public override Task<Message>[] SendMessageToClientWhenBackCommand(TelegramBotClient botClient, Message message, Client client)
        {
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Введите ваше Имя.",
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