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
    internal class EditProductInMyBasket : Command
    {
        private DataProducts _dataProducts;

        public EditProductInMyBasket()
        {
            _dataProducts = new DataProducts();
            foreach (Product product in _dataProducts.ProductsList)
            {
                base._namesList.Add(product.Name);
            }
            base.PreviousCommandBack = DataCommands.MyViewMyBasket;
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            for (int i = 0; i < client.ProductsBasket.Count; i++)
            {
                if (message.Text == client.ProductsBasket[i].Name)
                {
                    client.SelectedProduct = client.ProductsBasket[i];

                    Task<Message>[] messages = new Task<Message>[]
                    {
                        botClient.SendTextMessageAsync(message.Chat.Id, $"Описание: {client.SelectedProduct.Description}", replyMarkup: GetButtons()),
                    };
                    DataCommands.MySelectProduct.SendHandleMessage(botClient, message, client);
                    return messages;
                }
            }
            return null;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{new KeyboardButton(DataCommands.MyDeleteProduct.NamesList.First())},
                new List<KeyboardButton>{new KeyboardButton(DataCommands.MyBackCommand.NamesList.First())}
            };

            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}
