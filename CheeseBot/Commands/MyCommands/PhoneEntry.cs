using Microsoft.Extensions.Logging;
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
    internal class PhoneEntry : Command
    {
        public PhoneEntry()
        {
            base.NamesList = new List<string>() { "Мой телефон" };
            base.PreviousCommandBack = DataCommands.MyNameEntry;
        }


        public async Task SendMessageToClientWhenPhone(TelegramBotClient botClient, Message message, Client client)
        {
            client.NumberPhone = message.Text;
            await botClient.SendTextMessageAsync(message.Chat.Id, $"Номер телефона принят!" +
                $"\nВаш заказ:" +
                $"\n{MyBasket(client)}" +
                $"----------------------" +
                $"\nСпособ получения: {GetTypeDelivery(client)}" +
                $"\nИтоговая стоимость(Примерная): {TotalPrice(client)}TL" +
                $"\n{GetAdress(client)}" +
                $"\nВаше имя: {client.Name}" +
                $"\nВаш номер телефона: {client.NumberPhone}" +
                $"\nУбедитесь в корректности введенной вами информации!" +
                $"\nЕсли все в порядке, то можете оформить заказ." +
                $"\nЕсли вам нужно отредактировать, то нажмите внизу изменить.",
                replyMarkup: this.GetButtons());
        }

        public string MyBasket(Client client)
        {
            string basket = "";

            foreach (Product product in client.ProductsBasket)
            {
                if (product.MyTypeProductAmount == Product.TypeProductAmount.Weight)
                {
                    basket += $"-{product.Name} {product.Weight} гр. {product.Price}TL за 100 гр.\n";
                }
                else if (product.MyTypeProductAmount == Product.TypeProductAmount.Float)
                {
                    basket += $"-{product.Name} {product.Weight} шт. {product.Price}TL за 100 гр.\n";
                }
                else
                {
                    if (product.ApproximateWeight == default(float))
                    {
                        basket += $"-{product.Name} {product.Weight} шт. {product.Price}TL за штуку.\n";
                    }
                    else
                    {
                        basket += $"-{product.Name} {product.Weight} шт. {product.Price}TL за 100гр.\n";
                    }
                }
            }
            return basket;
        }

        public int TotalPrice(Client client)
        {
            int totalPrice = GetAllProductPrice(client);

            if (client.TypeDelivery == Client.TypesDelivery.Delivery)
            {
                if (totalPrice < 300)
                {
                    totalPrice += 30;
                }
            }
            return totalPrice;
        }

        public string GetAdress(Client client)
        {
            if (client.TypeDelivery == Client.TypesDelivery.Delivery)
            {
                if (client.Adress != null)
                {
                    return $"Адрес доставки: {client.Adress}";
                }
                else
                {
                    return $"Местоположение доставки: {client.Location.Latitude} {client.Location.Longitude}";
                }
            }
            else
            {
                return "";
            }
        }

        public string GetTypeDelivery(Client client)
        {
            if (client.TypeDelivery == Client.TypesDelivery.Delivery)
            {
                if (GetAllProductPrice(client) < 300)
                {
                    return "Доставка + 30TL";
                }
                else
                {
                    return "Доставка бесплатная";
                }
            }
            else
            {
                return "Самовывоз";
            }
        }

        public override IReplyMarkup GetButtons()
        {
            var Keyboard = new List<List<KeyboardButton>>
            {
                new List<KeyboardButton>{ new KeyboardButton(DataCommands.MyFinishOrder.NamesList.First())},
                new List<KeyboardButton>{ new KeyboardButton(DataCommands.MyContinueCheckout.NamesList[1])},
                new List<KeyboardButton>{ new KeyboardButton(DataCommands.MyBackCommand.NamesList.First())}
            };

            return new ReplyKeyboardMarkup(Keyboard);
        }

        private int GetAllProductPrice(Client client)
        {
            int totalPrice = 0;

            foreach (Product product in client.ProductsBasket)
            {
                if (product.MyTypeProductAmount == Product.TypeProductAmount.Weight)
                {
                    totalPrice += (int)(product.Weight * product.Price / 100);
                }
                else if (product.MyTypeProductAmount == Product.TypeProductAmount.Float)
                {
                    totalPrice += (int)(product.Weight * product.ApproximateWeight * product.Price / 100);
                }
                else
                {
                    if (product.ApproximateWeight == default(float))
                    {
                        totalPrice += (int)(product.Weight * product.Price);
                    }
                    else
                    {
                        totalPrice += (int)(product.Weight * product.ApproximateWeight * product.Price / 100);
                    }
                }
            }

            return totalPrice;
        }
    }
}
