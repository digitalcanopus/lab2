using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace TypesCreation
{
    public class ShortC : ICreate
    {
        public object Create(Type type)
        {
            Random rnd = new Random();
            return rnd.Next(short.MinValue, short.MaxValue + 1);
        }

        public bool Exist(Type type)
        {
            return type == typeof(short);
        }
    }
}