using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    internal class CheeseKosichkaChechil : Product
    {
        public CheeseKosichkaChechil()
        {
            this._name = $"Сыр-косичка \"Чечил\" классическая";
            this._description = "Чечил-косичка - рассольный вытяжной сыр с выраженным молочным вкусом и умеренной солёностью. Идеален в качестве закуски. Состав: фермерское молоко, бактериальная закваска, фермент, соль. Вес косички - 130-180 граммов.";
            this._price = 45;
            this._buttonName = $"{_name} \nЦена за 100 гр. - {_price}TL";
            base.MyTypeProductAmount = TypeProductAmount.Int;
            base.ApproximateWeight = 160;
        }

        public override object Clone()
        {
            return new CheeseKosichkaChechil();
        }
    }
}
