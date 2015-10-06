using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {

            Value v1 = new Value(5,DerivativesCi.N);
            Value v2 = new Value(2, SimpleCi.mol);

            var v3 = v1 * v2;

        }
    }
}
