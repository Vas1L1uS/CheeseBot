using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Collections;

namespace CheeseBot
{
    internal class Order
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string NumberPhone { get; set; }
        public string TypeDelivery { get; set; }
        public string Adress { get; set; }
        public string Location { get; set; }
        public string ProductsBasket { get; set; }
        public string TotalPrice { get; set; }
        public string DateCreate { get; set; }

        public Order(Client client)
        {
            this.UserName = $"@{client.UserName}";
            this.Name = client.Name;
            this.NumberPhone = $"{client.NumberPhone}";
            this.Adress = GetAdress(client);
            this.Location = GetLocationInString(client.Location);
            this.ProductsBasket = GetProductsInString(client.ProductsBasket);
            this.TypeDelivery = GetTypeDeliveryInString(client);
            this.TotalPrice = $"{GetTotalPrice(client)}TL";
            this.DateCreate = GetDateNowInString();
        }

        public Order()
        {

        }

        private string GetAdress(Client client)
        {
            return $"{client.Area}: {client.Adress}";
        }

        private int GetAllPriceProducts(List<Product> productsList)
        {
            int totalPrice = 0;
            foreach (var product in productsList)
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

        private int GetTotalPrice(Client client)
        {
            int totalPrice = GetAllPriceProducts(client.ProductsBasket);
            if (client.TypeDelivery == Client.TypesDelivery.Delivery)
            {
                if (totalPrice < 300)
                {
                    totalPrice += 30;
                }
            }

            return totalPrice;
        }

        private string GetProductsInString(List<Product> productsList)
        {
            string str = "";
            foreach (var product in productsList)
            {
                str += $"*{product.Name}* кол-во {product.Weight}, ";
            }
            return str;
        }

        private string GetTypeDeliveryInString(Client client)
        {
            if (client.TypeDelivery == Client.TypesDelivery.Delivery)
            {
                if (GetAllPriceProducts(client.ProductsBasket) < 300)
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

        private string GetDateNowInString()
        {
            DateTime dateTime = DateTime.Now;
            return $"{dateTime.ToString()}";
        }

        private string GetLocationInString(Location location)
        {
            if (location == null)
            {
                return "Геопозиция не задана";
            }
            else
            {
                return $"{location.Latitude} {location.Longitude}";
            }
        }
    }
}
