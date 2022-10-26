using Interfaces;

namespace TypesCreation
{
    public class IntC : ICreate
    {
        public object Create(Type type)
        {
            Random rnd = new Random();
            return rnd.Next(int.MinValue, int.MaxValue);
        }
        public bool Exist(Type type)
        {
            return type == typeof(int);
        }
    }
}