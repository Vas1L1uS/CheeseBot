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
    internal class Pickup : Command
    {
        public Pickup()
        {
            base._namesList = new List<string>() {"Самовывоз" };
            base.PreviousCommandBack = DataCommands.MyContinueCheckout;
        }

        Client _currentClient;
        Message _clientMessage;

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            client.TypeDelivery = Client.TypesDelivery.Pickup;
            _currentClient = client;
            _clientMessage = message;
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendLocationAsync(message.Chat.Id, 36.886779565421975f, 30.72666161501408f),

                botClient.SendTextMessageAsync(message.Chat.Id, $"Мы находимся по адресу ÇAYBAŞI MAH. 1361 SK. NO: 1A/A İÇ KAPI NO: 2 MURATPAŞA / ANTALYA" +
                $"\n-----------------------" +
                $"\r\nВведите ваше имя", replyMarkup: GetButtons())
            };
            client.LastCommand = this._namesList.First();
            return messages;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>();

            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(DataCommands.MyBackCommand.NamesList.First()) });
            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}
