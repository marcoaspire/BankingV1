using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingV1._7.Menu;
namespace BankingV1._7
{
    class Person : ICloneable
    {
        string name;
        int age;
        public Person(string name, int age)
        {
            this.age = age;
            this.name = name;
        }
        internal int getAge()
        {
           return  this.age ;
        }

        internal void Age(int v)
        {
            this.age=v;
        }

        public object Clone()
        {
            return (Person)MemberwiseClone();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BankMenu bank = new BankMenu();
            bank.DataLoad();
            bank.DisplayMenu();
            /*
            List<Person> list = new List<Person>();
            Person p1 = new Person("MArco", 25);
            Person p2 = new Person("Lucia", 30);

            list.Add((Person)p1.Clone());
            list.Add(p2);
            p1.Age(26);
            list.Add(p1);

            foreach (var item in list)
            {
                Console.WriteLine(item.getAge());
            }
            */
            Console.ReadKey();
        }
    }
}
