using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class Tvorog : Product
    {
        public Tvorog()
        {
            base._name = "Творог";
            base._description = "Творог - домашний классический творог из фермерского молока на итальянской закваске. Срок хранения творога в холодильнике - 5 дней. Состав: фермерское молоко, бактериальная закваска";
            base._price = 19;
            base._buttonName = $"{_name} \nЦена за 100 гр. - {_price}TL";
            base.MinWeight = 500;
            base.MyTypeProductAmount = TypeProductAmount.Weight;
        }

        public override object Clone()
        {
            return new Tvorog();
        }
    }
}
