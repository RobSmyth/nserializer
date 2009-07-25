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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NSerializer.TestAssembly1;
using NUnit.Framework;


namespace NSerializer.UATs
{
    [TestFixture]
    public class SerializeUATs : SerializeContext
    {
        [Test]
        public void CanSerializeClassUsingPrivateFields()
        {
            var classUnderTest = new ClassUsingFieldSerialization(11);
            classUnderTest.ChangeValues(2);

            var xmlText = SerializeAsXml(classUnderTest);
            var testedClass = ReadXmlText<ClassUsingFieldSerialization>(xmlText);

            Assert.IsNotNull(testedClass);
            Assert.AreEqual(25, testedClass.Total);
            Assert.AreEqual("02312d4a-93da-4310-b5d7-4a25fd8f1de8", testedClass.FieldC.ToString());
            Assert.AreEqual(TimeSpan.FromMinutes(1.25), testedClass.FieldD);
            Assert.AreEqual(new DateTime(2008, 12, 31, 23, 59, 59), testedClass.FieldE);
            Assert.AreEqual(1, testedClass.FieldF.Count);
            Assert.AreEqual(42, testedClass.FieldF["abc"]);
        }

        [Test]
        public void CanSerializeInheritedClassUsingFieldSerialization()
        {
            var xmlText = SerializeAsXml(new ClassD1(7));
            var testedClass = ReadXmlText<ClassD1>(xmlText);

            Assert.AreEqual(7, testedClass.ParameterA1);
            Assert.AreEqual(7, testedClass.ParameterA2);
        }

        [Test]
        public void XmlTextSizeForClassInheritedFromAGenericList()
        {
            var objectToWrite = new ClassE {1, 2};
            var xmlText = SerializeAsXml(objectToWrite);
            Assert.Less(xmlText.Length, 2000);
        }

        [Test]
        public void CanSerialiseClassInheritedFromAGenericList()
        {
            var objectToWrite = new ClassE {1, 2, 3};
            var xmlText = SerializeAsXml(objectToWrite);

            var testedClass = ReadXmlText<ClassE>(xmlText);
            Assert.AreEqual(3, testedClass.Count);
            Assert.AreEqual(1, testedClass[0]);
            Assert.AreEqual(2, testedClass[1]);
            Assert.AreEqual(3, testedClass[2]);
        }

        [Test]
        public void XmlTextSizeForClassInheritedFromAGenericDictionary()
        {
            var xmlText = SerializeAsXml(new ClassF(true));
            Assert.Less(xmlText.Length, 2000);
        }

        [Test]
        public void CanSerialiseClassInheritedFromAGenericDictionary()
        {
            var xmlText = SerializeAsXml(new ClassF(true));
            var testedClass = ReadXmlText<ClassF>(xmlText);
            Assert.AreEqual(2, testedClass.Count);
            Assert.AreEqual(1, testedClass["one"]);
            Assert.AreEqual(2, testedClass["two"]);
        }

        [Test]
        public void CanSerialiseClassInheritedFromABindingList()
        {
            var sourceObject = new ClassG();
            sourceObject.Add(1);
            sourceObject.Add(2);
            var xmlText = SerializeAsXml(sourceObject);

            var testedClass = ReadXmlText<ClassG>(xmlText);
            Assert.AreEqual(2, testedClass.Count);
            Assert.AreEqual(1, testedClass[0]);
            Assert.AreEqual(2, testedClass[1]);
        }

        [Test]
        public void CanSerialiseClassInheritedFromAnArraList()
        {
            var sourceObject = new ClassH();
            sourceObject.Add(1);
            sourceObject.Add("two");
            var xmlText = SerializeAsXml(sourceObject);
            Assert.Less(xmlText.Length, 2000);

            var testedClass = ReadXmlText<ClassH>(xmlText);
            Assert.AreEqual(2, testedClass.Count);
            Assert.AreEqual(1, testedClass[0]);
            Assert.AreEqual("two", testedClass[1]);
        }

        [Test]
        public void CanSerializeWithFieldSerializationAndConstructorWithParameters()
        {
            var xmlText = SerializeAsXml(new ClassA(7, new ClassB(11)));
            var readObject = ReadXmlText<ClassA>(xmlText);

            Assert.IsNotNull(readObject);
            Assert.AreEqual(7, readObject.ParamaterA);
            Assert.AreEqual(11, readObject.ParameterB.ParameterA);
        }

        [Test]
        public void CanSerialiseFieldDataTypes()
        {
            var sourceObject = new ClassI
                                   {
                                       A = true,
                                       B = 7,
                                       C = 123.456D,
                                       D = 13,
                                       E = 79.346F,
                                       F = 'x',
                                       G = -1,
                                       H = -7,
                                       I = 23,
                                       J = 42
                                   };
            var xmlText = SerializeAsXml(sourceObject);
            Assert.Less(xmlText.Length, 2500, xmlText);

            var testedClass = ReadXmlText<ClassI>(xmlText);
            Assert.AreEqual(true, testedClass.A);
            Assert.AreEqual(7, testedClass.B);
            Assert.AreEqual(123.456D, testedClass.C);
            Assert.AreEqual(13, testedClass.D);
            Assert.AreEqual(79.346F, testedClass.E);
            Assert.AreEqual('x', testedClass.F);
            Assert.AreEqual(-1, testedClass.G);
            Assert.AreEqual(-7, testedClass.H);
            Assert.AreEqual(23, testedClass.I);
            Assert.AreEqual(42, testedClass.J);
        }

#pragma warning disable UnusedMemberInPrivateClass
#pragma warning disable 168

        private class ClassA
        {
            private readonly int paramaterA;
            private readonly ClassB parameterB;

            public ClassA(int paramaterA, ClassB parameterB)
            {
                this.paramaterA = paramaterA;
                this.parameterB = parameterB;
            }

            public int ParamaterA
            {
                get { return paramaterA; }
            }

            public ClassB ParameterB
            {
                get { return parameterB; }
            }
        }

        private class ClassB
        {
            private readonly int parameterA;

            public ClassB(int parameterA)
            {
                this.parameterA = parameterA;
            }

            public int ParameterA
            {
                get { return parameterA; }
            }
        }

        private class ClassD1 : ClassD2
        {
            private readonly int parameterA1;

            public ClassD1(int parameterA) : base(parameterA)
            {
                parameterA1 = parameterA;
            }

            public int ParameterA1
            {
                get { return parameterA1; }
            }
        }

        private abstract class ClassD2
        {
            private readonly int parameterA2;

            public ClassD2(int parameterA)
            {
                parameterA2 = parameterA;
            }

            public int ParameterA2
            {
                get { return parameterA2; }
            }
        }

        private class ClassE : List<int>
        {
        }

        private class ClassF : Dictionary<string, int>
        {
            public ClassF()
            {
            }

            public ClassF(bool dummayParameter)
            {
                Add("one", 1);
                Add("two", 2);
            }
        }

        private class ClassG : BindingList<int>
        {
        }

        private class ClassH : ArrayList
        {
        }

        private class ClassI
        {
            private bool a = false;
            private int b = 0;
            private double c = 0.0;
            private Single d = 0;
            private float e = 0.0F;
            private char f = 'a';
            private Int32 g = 0;
            private Int64 h = 0;
            private UInt32 i = 0;
            private UInt64 j = 0;

            public bool A
            {
                get { return a; }
                set { a = value; }
            }

            public int B
            {
                get { return b; }
                set { b = value; }
            }

            public double C
            {
                get { return c; }
                set { c = value; }
            }

            public Single D
            {
                get { return d; }
                set { d = value; }
            }

            public float E
            {
                get { return e; }
                set { e = value; }
            }

            public char F
            {
                get { return f; }
                set { f = value; }
            }

            public int G
            {
                get { return g; }
                set { g = value; }
            }

            public long H
            {
                get { return h; }
                set { h = value; }
            }

            public uint I
            {
                get { return i; }
                set { i = value; }
            }

            public ulong J
            {
                get { return j; }
                set { j = value; }
            }
        }

#pragma warning restore 168
#pragma warning restore UnusedMemberInPrivateClass
    }
}