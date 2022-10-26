using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace TypesCreation
{
    public class CharC : ICreate
    {
        public object Create(Type type)
        {
            Random rnd = new Random();
            return (char)rnd.Next(char.MinValue, char.MaxValue + 1);
        }

        public bool Exist(Type type)
        {
            return type == typeof(char);
        }
    }
}
