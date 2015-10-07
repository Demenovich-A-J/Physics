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
        public double Num { get; set; }

        public Type Citype { get; set; }

        public Dictionary<Type,int> Derivatives { get; set; }

        public Value(){}
        public Value(double value, Type citype)
        {
            this.Num = value;
            this.Citype = citype;
            this.IsSimple = true;
        }
        public Value(double value, Dictionary<Type, int> derivatives)
        {
            this.Num = value;
            this.Derivatives = derivatives;
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
            obj1.Num = obj1.Num * num;

            return obj1;
        }

        public static Value operator /(Value obj1, double num)
        {
            obj1.Num = obj1.Num / num;

            return obj1;
        }

        public static Value operator /(Value obj1, Value obj2)
        {
            return Calculator.Divide(obj1, obj2);
        }



        public static Value operator* (Value obj1, Value obj2)
        {
            return Calculator.Multiply(obj1,obj2);
        }

    }
}