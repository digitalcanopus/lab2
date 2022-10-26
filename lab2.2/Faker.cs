using Interfaces;

namespace lab2._2
{
    public class Faker : IFaker
    {
        private readonly List<ICreate> creators;

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object Create(Type type)
        {
            foreach (var creator in creators)
            {
                if (creator.CanGenerate(type))
                {
                    return generator.Generate(type, _context);
                }
            }
        }

    }
}