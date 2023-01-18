using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Collections.ObjectModel;

namespace CheeseBot
{
    internal class AddToBasket : Command
    {
        private DataProducts _dataProducts;

        public AddToBasket()
        {
            base._namesList = new List<string>() { "Добавить в корзину", "Вернуться к списку продуктов" };
            base._previousCommandList = new List<string>() { base._namesList.First() };

            _dataProducts = new DataProducts();
            foreach (var product in _dataProducts.ProductsList)
            {
                base._previousCommandList.Add(product.ButtonName);
            }
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            if (message.Text == base._namesList[1])
            {
                Task<Message>[] messages = new Task<Message>[]
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Выберите нужную вам команду", replyMarkup: GetButtons())
                };
                return messages;
            }

            if (client.SelectedProduct.Weight > 0)
            {
                if (client.SetSelectedProductFromBusket())
                {
                    foreach (var product in client.ProductsBasket)
                    {
                        if (client.SelectedProduct == product)
                        {
                            Task<Message>[] messages = new Task<Message>[]
                            {
                                botClient.SendTextMessageAsync(message.Chat.Id, $"Продукт уже добавлен в вашу корзину!", replyMarkup: GetButtons())
                            };
                            DataCommands.MySelectProduct.DeleteHandleMessage(botClient, message, client);
                            return messages;
                        }
                    }
                }
                else
                {
                    client.ProductsBasket.Add(client.SelectedProduct);
                    Task<Message>[] messages = new Task<Message>[]
                    {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Продукт успешно добавлен в вашу корзину!", replyMarkup: GetButtons())
                    };
                    DataCommands.MySelectProduct.DeleteHandleMessage(botClient, message, client);
                    return messages;
                }
            }
            else
            {
                Task<Message>[] messages = new Task<Message>[]
                {
                    botClient.SendTextMessageAsync(message.Chat.Id, $"Необходимо указать количество прежде чем добавлять в корзину!")
                };
                DataCommands.MySelectProduct.DeleteAndNewHandleMessage(botClient, message, client);
                return messages;
            }
            return null;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>();

            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(DataCommands.MyViewMyBasket.NamesList.First()) });
            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton("Добавить еще продукт из списка") });
            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton("Продолжить оформление") });
            Keyboard.Add(new List<KeyboardButton> { new KeyboardButton(DataCommands.MyCancelCheckout.NamesList.First()) });
            return new ReplyKeyboardMarkup(Keyboard);
        }
    }
}
