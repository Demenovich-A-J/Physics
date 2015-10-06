using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Value
    {
        public bool IsSimple;
        public double num { get; set; }

        public Type citype { get; set; }

        public Dictionary<Type,int> derivatives { get; set; }

        public Value(){}
        public Value(double value, Type citype)
        {
            this.num = value;
            this.citype = citype;
            this.IsSimple = true;
        }
        public Value(double value, Dictionary<Type, int> derivatives)
        {
            this.num = value;
            this.derivatives = derivatives;
            this.IsSimple = false;
        }

        public static Value operator +(Value obj1, Value obj2)
        {
            return Calculator.Add(obj1,obj2);
        }
        public static Value operator -(Value obj1, Value obj2)
        {
            return Calculator.Minus(obj1,obj2);
        }

        public static Value operator* (Value obj1, double num)
        {
            obj1.num = obj1.num * num;

            return obj1;
        }

        public static Value operator /(Value obj1, double num)
        {
            obj1.num = obj1.num / num;

            return obj1;
        }


        public static Value operator* (Value obj1, Value obj2)
        {
            return Calculator.Multiply(obj1,obj2);
        }

    }
}