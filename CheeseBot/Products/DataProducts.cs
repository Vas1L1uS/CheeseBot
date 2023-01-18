using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace CheeseBot
{
    internal class DataProducts
    {
        public CheeseKachottaKlasicheskaya  MyCheeseKachottaKlasicheskaya;
        public CheeseKachottaSPajitnikom    MyCheeseKachottaSPajitnikom;
        public BelperKnollePeper            MyBelperKnollePeper;
        public BelperKnollePaprika          MyBelperKnollePaprika;
        public CheeseKosichkaChechil        MyCheeseKosichkaChechil;
        //public Suluguni                     MySuluguni;
        //public Smetana                    MySmetana;
        public Tvorog                       MyTvorog;

        public List<Product> ProductsList { get; private set; }

        public DataProducts()
        {
            MyCheeseKachottaKlasicheskaya = new CheeseKachottaKlasicheskaya();
            MyCheeseKachottaSPajitnikom = new CheeseKachottaSPajitnikom();
            MyBelperKnollePeper = new BelperKnollePeper();
            MyCheeseKosichkaChechil = new CheeseKosichkaChechil();
            MyBelperKnollePaprika = new BelperKnollePaprika();
            //MySuluguni = new Suluguni();
            //MySmetana = new Smetana();
            MyTvorog = new Tvorog();

            ProductsList = new List<Product>() { MyTvorog, MyCheeseKachottaKlasicheskaya, MyCheeseKachottaSPajitnikom, MyBelperKnollePeper, MyBelperKnollePaprika, MyCheeseKosichkaChechil /*MySmetana,*/ /*MySuluguni,*/ };
        }

        public string FindAndGetProduct(string text)
        {

            foreach (Product product in ProductsList)
            {
                if (product.CheckAndGetTypeCommand(text) != null)
                {
                    return product.GetType().Name;
                }
            }
            return null;
        }
    }
}
