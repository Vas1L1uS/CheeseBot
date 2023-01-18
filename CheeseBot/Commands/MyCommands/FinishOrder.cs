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
    internal class FinishOrder : Command
    {
        private Client _currentClient;

        public FinishOrder()
        {
            base._namesList = new List<string>() { "Оформить заказ" };
        }


        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(message.Chat.Id, $"Заказ оформлен!" +
                $"\nВ ближайшее время с вами свяжутся для подтверждения заказа.",
                replyMarkup: DataCommands.MyStartCommand.GetButtons()),
            };
            _currentClient = client;
            return messages;
        }

        public override Task<Message>[] SendMessageToAdmin(TelegramBotClient botClient, long chatID)
        {
            Task<Message>[] messages = new Task<Message>[]
            {
                botClient.SendTextMessageAsync(chatID, $"Оформлен новый заказ!" +
                $"\n{DataCommands.MyPhoneEntry.MyBasket(_currentClient)}" +
                $"----------------------" +
                $"\nСпособ получения: {DataCommands.MyPhoneEntry.GetTypeDelivery(_currentClient)}" +
                $"\nИтоговая стоимость(Примерная): {DataCommands.MyPhoneEntry.TotalPrice(_currentClient)}TL" +
                $"\n{GetAdress(_currentClient)}" +
                $"\n{GetUsername(_currentClient)}" +
                $"\nИмя: {_currentClient.Name}" +
                $"\nНомер телефона: {_currentClient.NumberPhone}")
            };
            return messages;
        }

        public string GetAdress(Client client)
        {
            if (_currentClient.TypeDelivery == Client.TypesDelivery.Delivery)
            {
                if (client.Adress != null)
                {
                    return $"Адрес доставки: {client.Adress}";
                }
                else
                {
                    return $"Координаты доставки: {client.Location.Latitude} {client.Location.Longitude}";
                }
            }
            else
            {
                return "";
            }
        }

        public string GetUsername(Client client)
        {
            if (client.UserName != null)
            {
                return $"Ник пользователя @{client.UserName}";
            }
            else
            {
                return "Ник пользователя скрыт.";
            }
        }


    }
}