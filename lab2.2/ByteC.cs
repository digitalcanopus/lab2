using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace TypesCreation
{
    public class ByteC : ICreate
    {
        public object Create(Type type)
        {
            Random rnd = new Random();
            return (byte)rnd.Next(byte.MaxValue + 1);
        }

        public bool Exist(Type type)
        {
            return type == typeof(byte);
        }
    }
}
