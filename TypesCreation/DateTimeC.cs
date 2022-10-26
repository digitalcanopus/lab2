using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace TypesCreation
{
    public class DateTimeC : ICreate
    {
        public DateTime MinDT { get; set; } = new(1500, 2, 3, 0, 0, 0);
        public DateTime MaxDT { get; set; } = new(2022, 2, 3, 0, 0, 0);

        public object Create(Type type)
        {
            Random rnd = new Random();
            return MinDT.AddSeconds(rnd.Next() * (MaxDT - MinDT).TotalSeconds);
        }

        public bool Exist(Type type)
        {
            return type == typeof(DateTime);
        }
    }
}
