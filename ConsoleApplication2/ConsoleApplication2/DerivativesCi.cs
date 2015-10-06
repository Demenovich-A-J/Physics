using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class DerivativesCi
    {
        public static Dictionary<Type, int> N = new Dictionary<Type,int>()
        {
           { SimpleCi.m, 1},
           { SimpleCi.kg, 1},
           { SimpleCi.s, -2}
        };
    }
}
