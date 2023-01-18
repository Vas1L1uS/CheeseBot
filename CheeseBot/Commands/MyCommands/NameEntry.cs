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
    internal class NameEntry : Command
    {
        public NameEntry()
        {
            base.NamesList = new List<string>() { "Мое имя" };
            base.PreviousCommandBack = DataCommands.MyAdressEntry;
        }


        public async Task SendMessageToClientWhenName(TelegramBotClient botClient, Message message, Client client)
        {
            client.Name = message.Text;
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Имя принято!" +
                $"\nВведите ваш номер телефона.",
                replyMarkup: this.GetButtons());
        }

        public override Task<Message>[] SendMessageToClientWhenBackCommand(TelegramBotClient botClient, Message message, Client client)
        {
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Введите ваш номер телефона.",
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
