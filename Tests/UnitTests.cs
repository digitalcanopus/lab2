using System.Reflection;
using System.Linq.Expressions;

using Interfaces;
using lab2._2;
using TypesCreation;
using NuGet.Frameworks;

namespace Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void ByteCreation()
        {
            Faker faker = new Faker();
            byte b = faker.Create<byte>();
            Assert.IsNotNull(b);
            Assert.IsTrue(b != default(byte));
        }

        [TestMethod]
        public void CharCreation()
        {
            Faker faker = new Faker();
            char b = faker.Create<char>();
            Assert.IsNotNull(b);
            Assert.IsTrue(b != default(char));
        }

        [TestMethod]
        public void IntCreation()
        {
            Faker faker = new Faker();
            int n = faker.Create<int>();
            Assert.IsNotNull(n);
            Assert.IsTrue(n != default(int));
        }

        [TestMethod]
        public void StringCreation()
        {
            Faker faker = new Faker();
            string s = faker.Create<string>();
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void DecimalCreation()
        {
            Faker faker = new Faker();
            decimal d = faker.Create<decimal>();
            Assert.IsNotNull(d);
            Assert.IsTrue(d != default(decimal));
        }

        [TestMethod]
        public void ListCreation()
        {
            Faker faker = new Faker();
            List<byte> list = faker.Create<List<byte>>();
            Assert.IsNotNull(list);
        }

        [TestMethod]
        public void ListOfListCreation()
        {
            Faker faker = new Faker();
            List<List<float>> list = faker.Create<List<List<float>>>();
            Assert.IsNotNull(list);
            Assert.IsNotNull(list[0]);
            Assert.IsTrue(list[0][0].GetType() == typeof(float));
        }

        [TestMethod]
        public void ULongCreation()
        {
            Faker faker = new Faker();
            ulong ul = faker.Create<ulong>();
            Assert.IsNotNull(ul);
            Assert.IsTrue(ul != default(ulong));
        }

        class Song
        {
            public string Title { get; set; }
            public string artist;
            public int TimeSec { get; set; }

            public Song(string title, string artist)
            {
                Title = title;
                this.artist = artist;
            }
        }

        [TestMethod]
        public void ObjCreation()
        {
            Faker faker = new Faker();
            Song song = faker.Create<Song>();
            Assert.IsNotNull(song.Title);
            Assert.IsNotNull(song.artist);
            Assert.IsNotNull(song.TimeSec);
            Assert.IsTrue(song.Title != default(string));
            Assert.IsTrue(song.artist != default(string));
            Assert.IsTrue(song.TimeSec != default(int));
        }

        class Class1
        {
            public DateTime Num { get; set; }
            public Class2 class2Instance { get; set; }
        }

        class Class2
        {
            public string song;
            public Class3 class3Instance { get; set; }
        }

        class Class3
        {
            public readonly bool flag = true;
            public Class1 class1Instance { get; set; }
        }

        [TestMethod]
        public void CycleDep()
        {
            Faker faker = new Faker();
            Class1 class1Inst = faker.Create<Class1>();

            Assert.IsNotNull(class1Inst);
            Assert.IsNotNull(class1Inst.Num);
            Assert.AreNotEqual(class1Inst.Num, 0);
            Assert.IsNotNull(class1Inst.class2Instance);
            Assert.IsNotNull(class1Inst.class2Instance.song);
            Assert.IsNotNull(class1Inst.class2Instance.class3Instance);
            Assert.IsNotNull(class1Inst.class2Instance.class3Instance.flag);
            Assert.AreNotEqual(class1Inst.class2Instance.class3Instance.flag, false);
            Assert.IsNull(class1Inst.class2Instance.class3Instance.class1Instance);
        }
    }
}