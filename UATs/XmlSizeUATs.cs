using System.Collections.Generic;
using NUnit.Framework;


namespace NXmlSerializer.UATs
{
    [TestFixture]
    public class XmlSizeUATs : SerializeContext
    {
        [Test]
        public void XmlTextSizeForGenericDictionary()
        {
            string xmlText = Serialize(new ClassA(true));
            Assert.Less(xmlText.Length, 3600);
        }

        [Test]
        public void XmlTextSizeForGenericList()
        {
            string xmlText = Serialize(new ClassB(true));
            Assert.Less(xmlText.Length, 1000);
        }

#pragma warning disable 168

        private class ClassA : Dictionary<string, int>
        {
            public ClassA(bool dummayParameter)
            {
                Add("one", 1);
                Add("two", 2);
            }
        }

        private class ClassB : List<int>
        {
            public ClassB(bool dummayParameter)
            {
                Add(1);
                Add(2);
                Add(3);
            }
        }

#pragma warning restore 168

    }
}
