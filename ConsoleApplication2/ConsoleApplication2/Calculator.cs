using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Calculator
    {
        public static Value Add(Value obj1, Value obj2)
        {
            if(obj1.citype != obj2.citype)
            {
                throw new NotSupportedException();
            }

            return new Value(obj1.num + obj2.num, obj1.citype);
        }
        public static Value Minus(Value obj1, Value obj2)
        {
            if (obj1.citype != obj2.citype)
            {
                throw new NotSupportedException();
            }

            return new Value(obj1.num - obj2.num, obj1.citype);
        }

        public static Value Multiply(Value obj1, Value obj2)
        {
            if (obj1.IsSimple || obj2.IsSimple)
            {
                if (obj1.citype == obj2.citype)
                {
                    return new Value(obj1.num * obj2.num, new Type(obj1.citype.name, obj1.citype.power + obj2.citype.power));
                }
                else if (obj1.citype != obj2.citype)
                {
                    Dictionary<Type, int> newType = new Dictionary<Type, int>();
                    if (obj1.IsSimple)
                    {
                        foreach (var a in obj2.derivatives)
                        {
                            if (obj1.citype == a.Key)
                            {
                                newType.Add(a.Key, a.Value + obj1.citype.power);
                            }
                            else
                            {
                                newType.Add(a.Key, a.Value);
                            }

                        }
                        if (!newType.ContainsKey(obj1.citype))
                        {
                            newType.Add(obj1.citype, obj1.citype.power);

                        }

                        return new Value(obj1.num * obj2.num,newType);
                    }else if (obj2.IsSimple)
                    {
                        foreach (var a in obj1.derivatives)
                        {
                            if (obj2.citype == a.Key)
                            {
                                newType.Add(a.Key, a.Value + obj2.citype.power);
                            }
                            else
                            {
                                newType.Add(a.Key, a.Value);
                            }

                        }
                        if (!newType.ContainsKey(obj2.citype))
                        {
                            newType.Add(obj2.citype, obj2.citype.power);
                        }

                        return new Value(obj1.num * obj2.num, newType);
                    }
                }
            }
            else
            {
                Dictionary<Type, int> newType = new Dictionary<Type, int>();
                
                if(obj1.derivatives.Count > obj2.derivatives.Count)
                {
                   foreach(var a in obj1.derivatives)
                   {
                       if(obj2.derivatives.ContainsKey(a.Key))
                       {
                           newType.Add(a.Key,obj2.derivatives[a.Key] + a.Value);
                       }
                       else
                       {
                           newType.Add(a.Key, a.Value);
                       }
                   }

                   return new Value(obj1.num * obj2.num, newType);
                }
                else
                {
                    foreach (var a in obj2.derivatives)
                    {
                        if (obj1.derivatives.ContainsKey(a.Key))
                        {
                            newType.Add(a.Key, obj1.derivatives[a.Key] + a.Value);
                        }
                        else
                        {
                            newType.Add(a.Key, a.Value);
                        }
                    }

                    return new Value(obj1.num * obj2.num, newType);
                }
            }

            return null;
        }
        public static Value Divide(Value obj1, Value obj2)
        {
            if (obj1.IsSimple || obj2.IsSimple)
            {
                if (obj1.citype == obj2.citype)
                {
                    return new Value(obj1.num / obj2.num, new Type(obj1.citype.name, obj1.citype.power - obj2.citype.power));
                }
                else if (obj1.citype != obj2.citype)
                {
                    Dictionary<Type, int> newType = new Dictionary<Type, int>();
                    if (obj1.IsSimple)
                    {
                        foreach (var a in obj2.derivatives)
                        {
                            if (obj1.citype == a.Key)
                            {
                                newType.Add(a.Key, a.Value - obj1.citype.power);
                            }
                            else
                            {
                                newType.Add(a.Key, a.Value);
                            }

                        }
                        if (!newType.ContainsKey(obj1.citype))
                        {
                            newType.Add(obj1.citype, obj1.citype.power);

                        }

                        return new Value(obj1.num / obj2.num, newType);
                    }
                    else if (obj2.IsSimple)
                    {
                        foreach (var a in obj1.derivatives)
                        {
                            if (obj2.citype == a.Key)
                            {
                                newType.Add(a.Key, a.Value - obj2.citype.power);
                            }
                            else
                            {
                                newType.Add(a.Key, a.Value);
                            }

                        }
                        if (!newType.ContainsKey(obj2.citype))
                        {
                            newType.Add(obj2.citype, obj2.citype.power);
                        }

                        return new Value(obj1.num / obj2.num, newType);
                    }
                }
            }
            else
            {
                Dictionary<Type, int> newType = new Dictionary<Type, int>();

                if (obj1.derivatives.Count > obj2.derivatives.Count)
                {
                    foreach (var a in obj1.derivatives)
                    {
                        if (obj2.derivatives.ContainsKey(a.Key))
                        {
                            newType.Add(a.Key, obj2.derivatives[a.Key] - a.Value);
                        }
                        else
                        {
                            newType.Add(a.Key, a.Value);
                        }
                    }

                    return new Value(obj1.num / obj2.num, newType);
                }
                else
                {
                    foreach (var a in obj2.derivatives)
                    {
                        if (obj1.derivatives.ContainsKey(a.Key))
                        {
                            newType.Add(a.Key, obj1.derivatives[a.Key] - a.Value);
                        }
                        else
                        {
                            newType.Add(a.Key, a.Value);
                        }
                    }

                    return new Value(obj1.num / obj2.num, newType);
                }
            }

            return null;
        }
    }
}
