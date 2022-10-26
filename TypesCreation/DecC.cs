using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace TypesCreation
{
    public class DecC : ICreate
    {
        public object Create(Type type)
        {
            Random rnd = new Random();
            return (decimal)rnd.NextDouble() * decimal.MaxValue;
        }

        public bool Exist(Type type)
        {
            return type == typeof(decimal);
        }
    }
}
