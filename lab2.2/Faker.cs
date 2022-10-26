using System.Reflection;
using Interfaces;

namespace lab2._2
{
    public class Faker : IFaker
    {
        private readonly IEnumerable<ICreate> creators;
        private readonly HashSet<Type> createdTypes = new();

        public Faker() 
        { 
            creators = Creators();
            //Assembly.LoadWithPartialName(".dll");
            try
            {
                Assembly one = Assembly.LoadFrom("../../../../One.dll");
                Type intCreator = one.GetType("One.IntC2");
                creators = creators.Append((ICreate)Activator.CreateInstance(intCreator));
            }
            catch { }

            try
            {
                Assembly two = Assembly.LoadFrom("../../../../Two.dll");
                Type stringCreator = two.GetType("Two.StringC2");
                creators = creators.Append((ICreate)Activator.CreateInstance(stringCreator));
            }
            catch { }

        }

        private static IEnumerable<ICreate> Creators()
        {
            return Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICreate))).Select(t => (ICreate)Activator.CreateInstance(t)).ToList();
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object Create(Type type)
        {
            foreach (var creator in creators)
            {
                if (creator.Exist(type))
                {
                    return creator.Create(type);
                }
            }

            if (createdTypes.Contains(type))
                return DefaultVal(type);

            createdTypes.Add(type);
            var obj = CreateNested(type);

            if (obj == null)
                return null;

            Fields(obj);
            Properties(obj);
            createdTypes.Remove(type);
            return obj;
        }

        private static object? DefaultVal(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        private object? CreateNested(Type type)
        {
            var constructors = type.GetConstructors().ToList().OrderByDescending(constr => constr.GetParameters().Length).ToList();

            foreach (var constructor in constructors)
            {
                try
                {
                    return constructor.Invoke(constructor.GetParameters().Select(info => Create(info.ParameterType)).ToArray());
                }
                catch (Exception)
                {
                    //no exceptions on my shift
                }
            }
            return DefaultVal(type);
        }

        private void Fields(object obj)
        {
            var fields = obj.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                if (field.IsInitOnly)
                {
                    continue;
                }
                field.SetValue(obj, Create(field.FieldType));
            }
        }

        private void Properties(object obj)
        {
            var properties = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            foreach (var property in properties)
            {
                if (property.CanWrite)
                {
                    property.SetValue(obj, Create(property.PropertyType));
                }
            }
        }
    }
}