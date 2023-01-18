using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace CheeseBot
{
    internal class SelectProduct : Command
    {
        private DataProducts _dataProducts;
        private Product _selectedProduct;

        public SelectProduct()
        {
            base._namesList = new List<string>();
            base._previousCommandList = new List<string>();

            _dataProducts = new DataProducts();
            foreach (Product product in _dataProducts.ProductsList)
            {
                base._namesList.Add(product.ButtonName);
            }
        }

        public Product GetSelectedProduct(string text)
        {
            foreach (var product in _dataProducts.ProductsList)
            {
                if (text == product.ButtonName)
                {
                    return product.Clone() as Product;
                }
            }
            return null;
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            Product selectedProduct = GetSelectedProduct(message.Text);
            client.SelectedProduct = selectedProduct;
            _selectedProduct = client.SelectedProduct;
            client.SetSelectedProductFromBusket();

            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Описание: {selectedProduct.Description}", replyMarkup: GetButtons()),
            };
            return messages;
        }

        public async Task SendHandleMessage(TelegramBotClient botClient, Message message, Client client)
        {
            if (client.LastHandleMessage == null)
            {
                client.LastHandleMessage = await botClient.SendTextMessageAsync(message.Chat.Id, $"Введите необходимое количество" +
                $"\n{GetStringByType(client)}", replyMarkup: GetInlineButtons());
            }
            else
            {
                await EditHandleMessage(botClient, message, client);
            }
            return;
        }

        public async Task EditHandleMessage(TelegramBotClient botClient, Message message, Client client)
        {
            if (client.LastHandleMessage != null)
            {
                client.LastHandleMessage = await botClient.EditMessageTextAsync(message.Chat.Id, client.LastHandleMessage.MessageId, $"Введите необходимое количество" +
                $"\n{GetStringByType(client)}", replyMarkup: (InlineKeyboardMarkup)GetInlineButtons());
            }
            return;
        }

        public async Task DeleteAndNewHandleMessage(TelegramBotClient botClient, Message message, Client client)
        {
            if (client.LastHandleMessage != null)
            {
                await botClient.DeleteMessageAsync(message.Chat.Id, client.LastHandleMessage.MessageId);

            }
            client.LastHandleMessage = await botClient.SendTextMessageAsync(message.Chat.Id, $"Введите необходимое количество" +
                $"\n{GetStringByType(client)}", replyMarkup: GetInlineButtons());
            return;
        }

        public async Task DeleteHandleMessage(TelegramBotClient botClient, Message message, Client client)
        {
            if (client.LastHandleMessage != null)
            {
                await botClient.DeleteMessageAsync(message.Chat.Id, client.LastHandleMessage.MessageId);
                client.LastHandleMessage = null;
            }
            return;
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{new KeyboardButton(DataCommands.MyAddToBasket.NamesList.First())},
                new List<KeyboardButton>{new KeyboardButton(DataCommands.MyViewListProducts.NamesList[2])}
            };

            return new ReplyKeyboardMarkup(Keyboard);
        }

        private string GetStringByType(Client client)
        {
            _selectedProduct = client.SelectedProduct;
            if (client.SelectedProduct.MyTypeProductAmount == Product.TypeProductAmount.Weight)
            {
                return $"\nСейчас выбрано {client.SelectedProduct.Weight} гр." +
                $"\nСтоимость {client.SelectedProduct.Weight * client.SelectedProduct.Price / 100}TL";
            }
            else if (_selectedProduct.MyTypeProductAmount == Product.TypeProductAmount.Float)
            {
                return $"\nСейчас выбрано {client.SelectedProduct.Weight} шт." +
                $"\nПримерная cтоимость {client.SelectedProduct.Weight * client.SelectedProduct.ApproximateWeight * client.SelectedProduct.Price / 100}TL";
            }
            else
            {
                if (_selectedProduct.ApproximateWeight == default(float))
                {
                    return $"\nСейчас выбрано {client.SelectedProduct.Weight} шт." +
                    $"\nСтоимость {client.SelectedProduct.Weight * client.SelectedProduct.Price}TL";
                }
                else
                {
                    return $"\nСейчас выбрано {client.SelectedProduct.Weight} шт." +
                    $"\nПримерная cтоимость {client.SelectedProduct.Weight * client.SelectedProduct.ApproximateWeight * client.SelectedProduct.Price / 100}TL";
                }
            }  
        }

        public override IReplyMarkup GetInlineButtons()
        {
            if (_selectedProduct.MyTypeProductAmount == Product.TypeProductAmount.Float)
            {
                var InlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("-1/2шт.", "WeightProduct_Subtract1/2"),
                    InlineKeyboardButton.WithCallbackData("+1/2шт.", "WeightProduct_Add1/2"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("-1шт.", "WeightProduct_Subtract1"),
                    InlineKeyboardButton.WithCallbackData("+1шт.", "WeightProduct_Add1"),
                },
            });
                return InlineKeyboard;
            }
            else if (_selectedProduct.MyTypeProductAmount == Product.TypeProductAmount.Int)
            {
                var InlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("-1шт.", "WeightProduct_Subtract1"),
                    InlineKeyboardButton.WithCallbackData("+1шт.", "WeightProduct_Add1"),
                },
            });
                return InlineKeyboard;
            }
            else
            {
                var InlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("-100гр.", "WeightProduct_Subtract100"),
                    InlineKeyboardButton.WithCallbackData("+100гр.", "WeightProduct_Add100"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("-500гр.", "WeightProduct_Subtract500"),
                    InlineKeyboardButton.WithCallbackData("+500гр.", "WeightProduct_Add500"),
                },
            });
                return InlineKeyboard;
            }
        }
    }
}
