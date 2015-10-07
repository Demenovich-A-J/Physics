using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Calculator
    {
        public static Value Add(Value obj1, Value obj2)
        {
            if(obj1.Citype != obj2.Citype)
            {
                throw new NotSupportedException();
            }

            return new Value(obj1.Num + obj2.Num, obj1.Citype);
        }
        public static Value Minus(Value obj1, Value obj2)
        {
            if (obj1.Citype != obj2.Citype)
            {
                throw new NotSupportedException();
            }

            return new Value(obj1.Num - obj2.Num, obj1.Citype);
        }

        public static Value Multiply(Value obj1, Value obj2)
        {
            if (obj1.IsSimple || obj2.IsSimple)
            {
                if (obj1.Citype == obj2.Citype)
                {
                    return new Value(obj1.Num * obj2.Num, new Type(obj1.Citype.Name, obj1.Citype.Power + obj2.Citype.Power));
                }
                else if (obj1.Citype != obj2.Citype)
                {
                    Dictionary<Type, int> newType = new Dictionary<Type, int>();
                    if (obj1.IsSimple)
                    {
                        foreach (var a in obj2.Derivatives)
                        {
                            if (obj1.Citype == a.Key)
                            {
                                newType.Add(a.Key, a.Value + obj1.Citype.Power);
                            }
                            else
                            {
                                newType.Add(a.Key, a.Value);
                            }

                        }
                        if (!newType.ContainsKey(obj1.Citype))
                        {
                            newType.Add(obj1.Citype, obj1.Citype.Power);

                        }

                        return new Value(obj1.Num * obj2.Num,newType);
                    }else if (obj2.IsSimple)
                    {
                        foreach (var a in obj1.Derivatives)
                        {
                            if (obj2.Citype == a.Key)
                            {
                                newType.Add(a.Key, a.Value + obj2.Citype.Power);
                            }
                            else
                            {
                                newType.Add(a.Key, a.Value);
                            }

                        }
                        if (!newType.ContainsKey(obj2.Citype))
                        {
                            newType.Add(obj2.Citype, obj2.Citype.Power);
                        }

                        return new Value(obj1.Num * obj2.Num, newType);
                    }
                }
            }
            else
            {
                Dictionary<Type, int> newType = new Dictionary<Type, int>();
                
                if(obj1.Derivatives.Count > obj2.Derivatives.Count)
                {
                   foreach(var a in obj1.Derivatives)
                   {
                       if(obj2.Derivatives.ContainsKey(a.Key))
                       {
                           newType.Add(a.Key,obj2.Derivatives[a.Key] + a.Value);
                       }
                       else
                       {
                           newType.Add(a.Key, a.Value);
                       }
                   }

                   return new Value(obj1.Num * obj2.Num, newType);
                }
                else
                {
                    foreach (var a in obj2.Derivatives)
                    {
                        if (obj1.Derivatives.ContainsKey(a.Key))
                        {
                            newType.Add(a.Key, obj1.Derivatives[a.Key] + a.Value);
                        }
                        else
                        {
                            newType.Add(a.Key, a.Value);
                        }
                    }

                    return new Value(obj1.Num * obj2.Num, newType);
                }
            }

            return null;
        }
        public static Value Divide(Value obj1, Value obj2)
        {
            if (obj1.IsSimple || obj2.IsSimple)
            {
                if (obj1.Citype == obj2.Citype)
                {
                    return new Value(obj1.Num / obj2.Num, new Type(obj1.Citype.Name, obj1.Citype.Power - obj2.Citype.Power));
                }
                else if (obj1.Citype != obj2.Citype)
                {
                    Dictionary<Type, int> newType = new Dictionary<Type, int>();
                    if (obj1.IsSimple)
                    {
                        foreach (var a in obj2.Derivatives)
                        {
                            if (obj1.Citype == a.Key)
                            {
                                newType.Add(a.Key, a.Value - obj1.Citype.Power);
                            }
                            else
                            {
                                newType.Add(a.Key, a.Value);
                            }

                        }
                        if (!newType.ContainsKey(obj1.Citype))
                        {
                            newType.Add(obj1.Citype, -1);

                        }

                        return new Value(obj1.Num / obj2.Num, newType);
                    }
                    else if (obj2.IsSimple)
                    {
                        foreach (var a in obj1.Derivatives)
                        {
                            if (obj2.Citype == a.Key)
                            {
                                newType.Add(a.Key, a.Value - obj2.Citype.Power);
                            }
                            else
                            {
                                newType.Add(a.Key, a.Value);
                            }

                        }
                        if (!newType.ContainsKey(obj2.Citype))
                        {
                            newType.Add(obj2.Citype, -1);
                        }

                        return new Value(obj1.Num / obj2.Num, newType);
                    }
                }
            }
            else
            {
                Dictionary<Type, int> newType = new Dictionary<Type, int>();

                if (obj1.Derivatives.Count > obj2.Derivatives.Count)
                {
                    foreach (var a in obj1.Derivatives)
                    {
                        if (obj2.Derivatives.ContainsKey(a.Key))
                        {
                            newType.Add(a.Key, obj2.Derivatives[a.Key] - a.Value);
                        }
                        else
                        {
                            newType.Add(a.Key, -a.Value);
                        }
                    }

                    return new Value(obj1.Num / obj2.Num, newType);
                }
                else
                {
                    foreach (var a in obj2.Derivatives)
                    {
                        if (obj1.Derivatives.ContainsKey(a.Key))
                        {
                            newType.Add(a.Key, obj1.Derivatives[a.Key] - a.Value);
                        }
                        else
                        {
                            newType.Add(a.Key, -a.Value);
                        }
                    }

                    return new Value(obj1.Num / obj2.Num, newType);
                }
            }

            return null;
        }

        public static Value Sqrt(Value obj1, int power)
        {
            Dictionary<Type, int> newType = new Dictionary<Type, int>();

            if (obj1.IsSimple)
            {
                if (obj1.Citype.Power == power)
                {
                    return new Value(Math.Pow(obj1.Num,power),new Type(obj1.Citype.Name,obj1.Citype.Power / power));
                }
            }
            else
            {
                foreach (var a in obj1.Derivatives)
                {
                    if (a.Value == power)
                    {
                        newType.Add(a.Key, a.Value/power);
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }

                return new Value(Math.Pow(obj1.Num,power),newType);
            }

            return null;
        }
    }
}
