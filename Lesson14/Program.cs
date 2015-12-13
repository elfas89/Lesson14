using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Lesson14
{
    class Program
    {
        static void Main(string[] args)
        {
            Assembly a = Assembly.LoadFrom("ReflectionTask.dll");
            Console.WriteLine(a.FullName);
            Type[] ta = a.GetTypes();
            foreach (Type t in ta)
            {
                Console.WriteLine(t.Name);
                foreach (MemberInfo m in t.GetMembers())
                {
                    Console.WriteLine(m.DeclaringType + " " + m.MemberType + " " + m.Name);
                }

                Console.WriteLine("Методы:");
                foreach (MethodInfo m in t.GetMethods())
                {
                    string mod = "";
                    if (m.IsStatic)
                    {
                        mod += "static ";
                    }
                    if (m.IsVirtual)
                    {
                        mod += "virtual ";
                    }
                    Console.Write(mod + m.ReturnType.Name + " " + m.Name + " (");
                    ParameterInfo[] p = m.GetParameters();
                    for (int i = 0; i < p.Length; i++)
                    {
                        Console.Write(p[i].ParameterType.Name + " " + p[i].Name);
                        if (i + 1 < p.Length) Console.Write(", ");
                    }
                    Console.WriteLine(")");


                }


                try
                {
                    object o = Activator.CreateInstance(t);
                    Console.WriteLine("Создан объект типа " + t.Name);
                    MethodInfo met = t.GetMethod("GetArea", new Type[] { typeof(System.Int32) });
                    if (!met.IsVirtual)
                    {
                        object res = met.Invoke(o, new object[] { 6 });
                        Console.WriteLine(res);
                    }
                    FieldInfo field = t.GetField("figeType");
                    Console.WriteLine(field.GetValue(o));

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }



        }
    }
}