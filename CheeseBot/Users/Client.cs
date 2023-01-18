using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CheeseBot
{
    public class Client : User
    {
        public List<Product> ProductsBasket { get; private set; }
        public Product SelectedProduct { get; set; }
        public Location Location { get; set; }
        public string Adress { get; set; }
        public string Area { get; set; }
        public string Name { get; set; }
        public string NumberPhone { get; set; }
        public TypesDelivery TypeDelivery { get; set; }

        public enum TypesDelivery
        {
            Pickup,
            Delivery
        }

        public Client(Message message) : base(message)
        {
        }

        public Client()
        {
            ProductsBasket = new List<Product>();
        }

        public bool SetSelectedProductFromBusket()
        {
            foreach (var item in this.ProductsBasket)
            {
                if (item.Name == this.SelectedProduct.Name)
                {
                    this.SelectedProduct = item;
                    return true;
                }
            }
            return false;
        }

        public void ClearInformation()
        {
            this.ProductsBasket.Clear();
            this.SelectedProduct = null;
            this.Area = null;
            this.Location = null;
            this.Adress = null;
            this.Name = null;
            this.NumberPhone = null;
            base.LastCommand = null;
            base.LastHandleMessage = null;
        }
    }
}
