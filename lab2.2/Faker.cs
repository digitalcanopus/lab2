using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
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

            var plPaths = Directory.GetFiles("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\сэпэпэ\\lab2.2\\lab2.2\\obj\\Debug\\net6.0", "*.dll");

            foreach (var plPath in plPaths)
            {
                var asm = Assembly.LoadFrom(plPath);
                var types = asm.GetTypes().Where(t => t.GetInterfaces().Any(i => i.FullName == typeof(ICreate).FullName));
                foreach (var type in types)
                {
                    try
                    {
                        creators = creators.Append((ICreate)Activator.CreateInstance(type));
                    }
                    catch { }
                }
            }
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