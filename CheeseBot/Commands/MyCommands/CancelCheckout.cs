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
    internal class CancelCheckout : Command
    {
        public CancelCheckout()
        {
            base._namesList = new List<string>() { "Отменить оформление❌" };
        }


        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            client.SelectedProduct = null;
            client.ProductsBasket.Clear();
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Оформление заказа отменено! \nВнизу рядом с вводом есть панель команд. Нажмите на нужную вам команду.", replyMarkup: this.GetButtons()),
            };
            return messages;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{new KeyboardButton("Создать заказ")},
                new List<KeyboardButton>{new KeyboardButton("Частые вопросы")}
            };

            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}
