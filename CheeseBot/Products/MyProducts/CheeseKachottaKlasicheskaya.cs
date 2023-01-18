using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class CheeseKachottaKlasicheskaya : Product
    {
        public CheeseKachottaKlasicheskaya()
        {
            this._name = $"Сыр \"Качотта\" класcическая";
            this._description = "Качотта классическая - итальянский полутвёрдый сыр, обладающий сливочно-молочным вкусом и пластичной структурой. Отлично подходит для бутербродов, для сырной тарелки и в качестве добавки в горячие блюда (хорошо плавится). Состав: фермерское молоко, бактериальная закваска, фермент, соль.\r\nВес головок - от 350 до 500 граммов.";
            this._price = 45;
            this._buttonName = $"{_name} \nЦена за 100 гр. - {_price}TL";
            base.MyTypeProductAmount = TypeProductAmount.Float;
            base.ApproximateWeight = 400;
        }

        public override object Clone()
        {
            return new CheeseKachottaKlasicheskaya();
        }
    }
}
