using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;
using lab2._2;

namespace TypesCreation
{
    public  class ListC : ICreate
    {
        public int MinLen { get; set; } = 2;
        public int MaxLen { get; set; } = 8;

        public object Create(Type type)
        {
            var listType = typeof(List<>).MakeGenericType(type.GenericTypeArguments[0]);
            var list = (IList)Activator.CreateInstance(listType)!;

            Random rnd = new Random();
            int size = rnd.Next(MinLen, MaxLen + 1);
            var elType = type.GetGenericArguments()[0];
            for (int i = 0; i < size; i++) 
                //list.Add(Faker.Create(elType));

            return list;
        }

        public bool Exist(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
        }
    }
}
