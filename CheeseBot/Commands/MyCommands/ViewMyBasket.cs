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
    internal class ViewMyBasket : Command
    {
        private Client _currentClient;
        private DataProducts _dataProducts;

        public ViewMyBasket()
        {
            _dataProducts = new DataProducts();

            base.PreviousCommandBack = DataCommands.MyAddToBasket;
            base._namesList = new List<string>() { "Посмотреть или редактировать мою корзину" };
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            _currentClient = client;
            int totalPrice = 0;
            totalPrice = DataCommands.MyPhoneEntry.TotalPrice(client);

            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Стоимость вашей корзины {totalPrice}TL" +
                $"\nНажмите на продукт из предствленного ниже списка, чтобы редактировать его.", replyMarkup: GetButtons())
            };
            return messages;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>();

            foreach (var product in _currentClient.ProductsBasket)
            {
                Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(product.Name) });
            }
            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(DataCommands.MyBackCommand.NamesList.First()) });
            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(DataCommands.MyCancelCheckout.NamesList.First()) });
            return new ReplyKeyboardMarkup(Keyboard);
        }

        public override Task<Message>[] SendMessageToClientWhenBackCommand(TelegramBotClient botClient, Message message, Client client)
        {
            if (client.SelectedProduct.Weight > 0)
            {
                _currentClient = client;
                int totalPrice = 0;
                totalPrice = DataCommands.MyPhoneEntry.TotalPrice(client);

                Task<Message>[] messages = new Task<Message>[]
                {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Стоимость вашей корзины {totalPrice}TL" +
                $"\nНажмите на продукт из представленного ниже списка, чтобы редактировать его.", replyMarkup: GetButtons())
                };

                client.LastCommand = this._namesList.First();
                return messages;
            }
            else
            {
                Task<Message>[] messages = new Task<Message>[]
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Количество не может равняться 0!")
                };
                DataCommands.MySelectProduct.DeleteAndNewHandleMessage(botClient, message, client);
                return messages;
            }
        }
    }
}
