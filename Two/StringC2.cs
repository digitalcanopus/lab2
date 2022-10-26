using Interfaces;

using System.Text;

namespace Two
{
    public class StringC2 : ICreate
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

        public bool Exist(Type type)
        {
            return type == typeof(string);
        }
    }
}