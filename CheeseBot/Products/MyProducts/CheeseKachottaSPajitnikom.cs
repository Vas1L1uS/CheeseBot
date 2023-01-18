using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class CheeseKachottaSPajitnikom : Product
    {
        public CheeseKachottaSPajitnikom()
        {
            this._name = $"Сыр \"Качотта\" с пажитником";
            this._description = "Качотта с пажитником - итальянский полутвёрдый сыр, обладающий сливочно-молочным вкусом с ореховыми нотками, ореховым ароматом и пластичной структурой. Отлично подходит для бутербродов, для сырной тарелки и в качестве добавки в горячие блюда (хорошо плавится). Состав: фермерское молоко, бактериальная закваска, фермент, семена пажитника, соль.\r\nВес головок - от 350 до 500 граммов.";
            this._price = 45;
            this._buttonName = $"{_name} \nЦена за 100 гр. - {_price}TL";
            base.MyTypeProductAmount = TypeProductAmount.Float;
            base.ApproximateWeight = 400;
        }

        public override object Clone()
        {
            return new CheeseKachottaSPajitnikom();
        }
    }
}
