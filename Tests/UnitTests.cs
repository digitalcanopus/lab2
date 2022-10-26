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
            ByteC byteC = new ByteC();
            Assert.IsTrue(byteC.Exist(typeof(byte)));
        }

        [TestMethod]
        public void IntCreation()
        {
            Faker faker = new Faker();
            var n = faker.Create<One.IntC2>();
            Assert.IsNotNull(n);
        }

        [TestMethod]
        public void StringCreation()
        {
            Faker faker = new Faker();
            var s = faker.Create<Two.StringC2>();
            Assert.IsNotNull(s);
        }

        [TestMethod]
        public void DecimalCreation()
        {
            DecC decC = new DecC();
            Assert.IsTrue(decC.Exist(typeof(decimal)));
        }

        [TestMethod]
        public void ListCreation()
        {
            ListC listC = new ListC();
            //Assert.IsNotNull(listC);
            Assert.IsTrue(listC.Exist(typeof(List<>)));
        }

        class Song
        {
            public string Title { get; set; }
            public string Artist { get; set; }
            private int TimeSec { get; set; }
            private readonly int Rating = 10;
        }

        class Class1
        {
            public DateTime Num { get; set; }
            public Class2 class2Instance { get; set; }
        }

        class Class2
        {
            private readonly string song = "Hourglass";
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
            Assert.IsNotNull(class1Inst.class2Instance.class3Instance);
            Assert.IsNotNull(class1Inst.class2Instance.class3Instance.flag);
            Assert.AreNotEqual(class1Inst.class2Instance.class3Instance.flag, false);
            Assert.IsNull(class1Inst.class2Instance.class3Instance.class1Instance);
        }
    }
}