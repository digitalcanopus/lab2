using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace TypesCreation
{
    public class DoubleC : ICreate
    {
        public object Create(Type type)
        {
            Random rnd = new Random();
            return (rnd.NextDouble() * double.MaxValue);
        }
    }
}
