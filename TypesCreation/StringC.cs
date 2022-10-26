using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Interfaces;

namespace TypesCreation
{
    public class StringC : ICreate
    {
        public string Letters { get; set; } = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        public int MinLen { get; set; } = 5;
        public int MaxLen { get; set; } = 20;

        public object Create(Type type)
        {
            Random rnd = new Random();
            int length = rnd.Next(MinLen, MaxLen + 1);
            var str = new StringBuilder(length);
            for (int i = 0; i < length; i++)
                str.Append(Letters[rnd.Next(Letters.Length)]);
            return str.ToString();
        }
    }
}
