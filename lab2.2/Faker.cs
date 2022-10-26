using Interfaces;

namespace lab2._2
{
    public class Faker : IFaker
    {
        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

    }
}