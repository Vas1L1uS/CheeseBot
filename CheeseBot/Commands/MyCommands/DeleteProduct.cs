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
    internal class DeleteProduct : Command
    {
        private DataProducts _dataProducts;

        public DeleteProduct()
        {
            _dataProducts = new DataProducts();
            base.NamesList = new List<string>() { "Удалить" };

            base.PreviousCommandBack = DataCommands.MyAddToBasket;
        }

        public override Task<Message>[] SendMessageToClient(TelegramBotClient botClient, Message message, Client client)
        {
            for (int i = 0; i < client.ProductsBasket.Count; i++)
            {
                if (client.SelectedProduct.Name == client.ProductsBasket[i].Name)
                {
                    client.ProductsBasket.Remove(client.ProductsBasket[i]);
                    break;
                }
            }
            return DataCommands.MyViewMyBasket.SendMessageToClient(botClient, message, client);
        }
    }
}
