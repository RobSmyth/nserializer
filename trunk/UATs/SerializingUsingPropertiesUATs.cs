#region Copyright

// The contents of this file are subject to the Mozilla Public License
//  Version 1.1 (the "License"); you may not use this file except in compliance
//  with the License. You may obtain a copy of the License at
//  
//  http://www.mozilla.org/MPL/
//  
//  Software distributed under the License is distributed on an "AS IS"
//  basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
//  License for the specific language governing rights and limitations under 
//  the License.
//  
//  The Initial Developer of the Original Code is Robert Smyth.
//  Portions created by Robert Smyth are Copyright (C) 2008.
//  
//  All Rights Reserved.

#endregion

using System.Collections.Generic;
using NSerializer.TestAssembly1;
using NSerializer.Tests.XML.Targets;
using NUnit.Framework;


namespace NSerializer.UATs
{
    [TestFixture]
    public class SerializingUsingPropertiesUATs : SerializeContext
    {
        [Test]
        public void CanSerializeArrayProperty()
        {
            var xmlText = SerializeAsXml(new SerializableClassWithArrayProperty(true));
            var readObject =
                (SerializableClassWithArrayProperty) ReadXmlText<object>(xmlText);

            var expected = new SerializableClassWithArrayProperty(true);
            AssertArraysAreEqual(expected.PropertyA, readObject.PropertyA);
        }

        [Test]
        public void CanSerializeClassWithoutAnyProperties()
        {
            var xmlText = SerializeAsXml(new SerializableClassWithoutProperties());
            var readObject = ReadXmlText<object>(xmlText);

            Assert.IsInstanceOfType(typeof (SerializableClassWithoutProperties), readObject);
        }

        [Test]
        public void CanSerializeClassWithProperties()
        {
            var xmlText = SerializeAsXml(new SerializableClassWithProperties(true));
            var readObject = (SerializableClassWithProperties) ReadXmlText<object>(xmlText);

            Assert.IsInstanceOfType(typeof (SerializableClassWithProperties), readObject);
            var expected = new SerializableClassWithProperties(true);
            Assert.AreEqual(expected.PropertyA, readObject.PropertyA);
            Assert.AreEqual(expected.PropertyB, readObject.PropertyB);
        }

        [Test]
        public void CanSerializePropertyThatIsAGenericListType()
        {
            var xmlText = SerializeAsXml(new SerializableClassWithGenericListProperty(true));
            var readObject =
                (SerializableClassWithGenericListProperty) ReadXmlText<object>(xmlText);

            var expected = new SerializableClassWithGenericListProperty(true);
            AssertArraysAreEqual(expected.PropertyA.ToArray(), readObject.PropertyA.ToArray());
        }

        [Test]
        public void CanSerializePropertyThatIsAnArrayOfSerializableObjects()
        {
            var xmlText = SerializeAsXml(new SerializableClassWithNestedArrayProperty(true));
            var readObject =
                (SerializableClassWithNestedArrayProperty) ReadXmlText<object>(xmlText);

            var expected = new SerializableClassWithNestedArrayProperty(true);

            AssertArraysAreEqual(expected.PropertyOne, readObject.PropertyOne);
        }

        [Test]
        public void CanSerializePropertyThatIsAnInterfaceType()
        {
            var xmlText = SerializeAsXml(new SerializableClassWithPropertyOfAnInterfaceType(true));
            var readObject =
                (SerializableClassWithPropertyOfAnInterfaceType) ReadXmlText<object>(xmlText);

            var expected =
                new SerializableClassWithPropertyOfAnInterfaceType(true);
            Assert.AreEqual(expected.PropertyOne, readObject.PropertyOne);
        }

        [Test]
        public void CanSerializePropertyWithNullValue()
        {
            var xmlText = SerializeAsXml(new SerializableClassWithPropertyOfAnInterfaceType(null));
            var readObject =
                (SerializableClassWithPropertyOfAnInterfaceType) ReadXmlText<object>(xmlText);

            Assert.IsInstanceOfType(typeof (SerializableClassWithPropertyOfAnInterfaceType), readObject);
            Assert.IsNull(readObject.PropertyOne);
        }

#pragma warning disable UnusedMemberInPrivateClass

        private class SerializableClassWithPropertyOfAnInterfaceType
        {
            private IMyInterface myValue;

            /// <summary>
            /// Constructor used to deserialise the class.
            /// </summary>
            public SerializableClassWithPropertyOfAnInterfaceType()
            {
            }

            /// <summary>
            /// Constructor used to get test object of known state.
            /// </summary>
            /// <param name="dummyArg"></param>
            public SerializableClassWithPropertyOfAnInterfaceType(bool dummyArg)
            {
                myValue = new SerializableClassWithAnInterface(dummyArg);
            }

            public SerializableClassWithPropertyOfAnInterfaceType(IMyInterface myValue)
            {
                this.myValue = myValue;
            }

            public IMyInterface PropertyOne
            {
                get { return myValue; }
                set { myValue = value; }
            }
        }

        public class SerializableClassWithNestedArrayProperty
        {
            private SerializableClassWithProperties[] propertyOne;

            /// <summary>
            /// Constructor used to deserialise the class.
            /// </summary>
            public SerializableClassWithNestedArrayProperty()
            {
            }

            /// <summary>
            /// Constructor used to get test object of known state.
            /// </summary>
            /// <param name="dummyArg"></param>
            public SerializableClassWithNestedArrayProperty(bool dummyArg)
            {
                propertyOne = new SerializableClassWithProperties[]
                                  {
                                      new SerializableClassWithProperties(1),
                                      new SerializableClassWithProperties(2)
                                  };
            }

            public SerializableClassWithProperties[] PropertyOne
            {
                get { return propertyOne; }
                set { propertyOne = value; }
            }
        }

        public class SerializableClassWithGenericListProperty
        {
            private List<SerializableClassWithProperties> propertyA = new List<SerializableClassWithProperties>();

            public SerializableClassWithGenericListProperty()
            {
            }

            public SerializableClassWithGenericListProperty(bool dummyArg)
            {
                propertyA.Add(new SerializableClassWithProperties(1));
                propertyA.Add(new SerializableClassWithProperties(2));
            }

            public List<SerializableClassWithProperties> PropertyA
            {
                get { return propertyA; }
                set { propertyA = value; }
            }
        }

#pragma warning restore UnusedMemberInPrivateClass
    }
}