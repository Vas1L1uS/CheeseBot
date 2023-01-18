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
    internal class ViewListProducts : Command
    {
        private Message _myMessage;
        private DataProducts _dataProducts;

        public ViewListProducts()
        {
            base._namesList = new List<string>() { "Создать заказ", "Добавить еще продукт из списка", "Вернуться к списку продуктов" };
            _dataProducts = new DataProducts();
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            _myMessage = message;
            client.SelectedProduct = null;
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Выберите продукт из представленного ниже списка для добавления его в корзину и просмотра описания.", replyMarkup: GetButtons())
            };
            return messages;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>();

            foreach (var product in _dataProducts.ProductsList)
            {
                Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(product.ButtonName) });
            }

            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(DataCommands.MyGoToNextStep.NamesList.First()) });
            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(DataCommands.MyCancelCheckout.NamesList.First()) });

            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}
