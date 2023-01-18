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
    internal class ContinueСheckout : Command
    {
        public ContinueСheckout()
        {
            base._namesList = new List<string>() { "Продолжить оформление", "Изменить" };
            base.PreviousCommandBack = DataCommands.MyAddToBasket;
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            if (client.ProductsBasket.Count > 0)
            {
                Task<Message>[] messages = new Task<Message>[]
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Выберите тип получения." +
                    $"\nСамовывоз - бесплатный." +
                    $"\nДоставка - 30TL, если сумма заказа больше 300TL, то доставка бесплатная", replyMarkup: GetButtons())
                };
                return messages;
            }
            else
            {
                Task<Message>[] messages = new Task<Message>[]
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, "Вы не можете оформить заказ с пустой корзиной!")
                };
                return messages;
            }
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>();

            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton("Самовывоз") });
            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton("Доставка") });
            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(DataCommands.MyBackCommand.NamesList.First()) });
            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}
