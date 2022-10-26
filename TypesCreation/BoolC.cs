using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace TypesCreation
{
    public class BoolC : ICreate
    {
        public object Create(Type type)
        {
            Random rnd = new Random();
            return rnd.Next(0, 2);
        }
    }
}
