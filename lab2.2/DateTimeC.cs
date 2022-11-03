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
        public DateTime MinDT { get; set; } = new(2000, 2, 3, 7, 5, 38);
        public DateTime MaxDT { get; set; } = new(2022, 2, 3, 16, 10, 14);

        public object Create(Type type)
        {
            Random rnd = new Random();
            return MinDT.AddSeconds(rnd.Next() * (MaxDT - MinDT).Seconds);
        }

        public bool Exist(Type type)
        {
            return type == typeof(DateTime);
        }
    }
}
