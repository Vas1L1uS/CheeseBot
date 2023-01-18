using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CheeseBot
{
    public abstract class Product : ICloneable
    {
        protected string _name;
        public string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        protected string _buttonName;
        public string ButtonName
        {
            get { return _buttonName; }
            protected set { _buttonName = value; }
        }

        protected string _description;
        public string Description
        {
            get { return _description; }
            protected set { _description = value; }
        }

        /// <summary>
        /// Цена за 100 грамм
        /// </summary>
        protected double _price;

        /// <summary>
        /// Цена за 100 грамм
        /// </summary>
        public double Price
        {
            get { return _price; }
            protected set { _price = value; }
        }

        protected float _weight;

        public float Weight
        {
            get { return _weight; }
            set
            {
                if (value >= 0)
                {
                    _weight = value;
                }
            }
        }

        public float ApproximateWeight { get; set; }

        public TypeProductAmount MyTypeProductAmount;

        public enum TypeProductAmount
        {
            Weight,
            Float,
            Int
        }

        public float MinWeight { get; protected set; }

        public string CheckAndGetTypeCommand(string text)
        {
            if (text == Name)
            {
                return GetType().Name;
            }
            return null;
        }

        public virtual object Clone()
        {
            return null;
        }
    }
}
