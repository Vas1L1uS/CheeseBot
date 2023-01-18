using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class BelperKnollePeper : Product
    {
        public BelperKnollePeper()
        {
            this._name = $"Белпер Кнолле с черным перцем";
            this._description = "Белпер Кнолле - пикантный швейцарский сыр со сроком вызревания от 10 дней. Во вкусе преобладают чеснок и специи, из которых состоит обсыпка.В зависимости от срока вызревания, структура меняется от полутвёрдой до твёрдой, крошащейся. Сыр можно натирать, нарезать стружкой или разламывать на кусочки, как пармезан. Вид обсыпки: черный перец.\r\nСостав: фермерское молоко, бактериальная закваска, фермент, свежий чеснок, гималайская соль, специи. Вес головки - от 50 до 60 граммов";
            this._price = 40;
            this._buttonName = $"{_name} \nЦена за штуку - {_price}TL";
            base.MyTypeProductAmount = TypeProductAmount.Int;
        }

        public override object Clone()
        {
            return new BelperKnollePeper();
        }
    }
}
