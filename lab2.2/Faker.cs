using System.Diagnostics;
using System.Reflection;
using System.Reflection.Emit;
using Interfaces;

namespace lab2._2
{
    public class Faker : IFaker
    {
        private List<ICreate> creators;
        private HashSet<Type> createdTypes = new();

        public Faker() 
        { 
            creators = Creators();

            try
            {
                Assembly One = Assembly.LoadFrom("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\сэпэпэ\\lab2.2\\lab2.2\\obj\\Debug\\net6.0\\One.dll");
                Type[] types = One.GetTypes();
                foreach (Type type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(ICreate)))
                    {
                        var asmList = (One.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICreate))).Select(t => (ICreate)Activator.CreateInstance(t)).ToList());
                        creators.AddRange(asmList);
                    }
                }
            }
            catch { }

            try
            {
                Assembly Two = Assembly.LoadFrom("C:\\Users\\Lenovo\\OneDrive\\Рабочий стол\\сэпэпэ\\lab2.2\\lab2.2\\obj\\Debug\\net6.0\\Two.dll");
                Type[] types = Two.GetTypes();
                foreach (Type type in types)
                {
                    if (type.GetInterfaces().Contains(typeof(ICreate)))
                    {
                        var asmList = (Two.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICreate))).Select(t => (ICreate)Activator.CreateInstance(t)).ToList());
                        creators.AddRange(asmList);
                    }
                }
            }
            catch { }
        }

        private static List<ICreate> Creators()
        {
            var result = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ICreate))).Select(t => (ICreate)Activator.CreateInstance(t)).ToList();
            return result;
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