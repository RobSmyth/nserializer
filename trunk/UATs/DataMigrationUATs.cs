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
using NSerializer.Types;
using NSerializer.UATs.Contexts;
using NUnit.Framework;


namespace NSerializer.UATs
{
    [TestFixture]
    public class DataMigrationUATs : SerializeContext
    {
        [Test]
        [Ignore("Being replaced by new migration builder - to ensure this test is replaced")]
        public void NewFieldsAdded()
        {
            var v1a = new Version1_ClassA(7, 11);

            var xmlText = SerializeAsXml(v1a);

            var v2a = ReadXmlText<Version2_ClassA>(xmlText, null, null);

            Assert.AreEqual(7, v2a.Field1);
            Assert.AreEqual(11, v2a.Field2);
            Assert.AreEqual(0, v2a.Field3);
        }
    }

    internal class Version1_ClassA
    {
        private int field1;
        private int field2;

        public Version1_ClassA(int field1, int field2)
        {
            this.field1 = field1;
            this.field2 = field2;
        }
    }

    internal class Version1_ClassB
    {
    }

    internal class Version2_ClassA
    {
        private readonly int field1;
        private readonly int field2;
        private readonly int field3;

        public Version2_ClassA(int field1, int field2, int field3)
        {
            this.field1 = field1;
            this.field3 = field3;
            this.field2 = field2;
        }

        public int Field3
        {
            get { return field3; }
        }

        public int Field2
        {
            get { return field2; }
        }

        public int Field1
        {
            get { return field1; }
        }
    }

    internal class Version2_ClassC
    {
    }

    public class V1CA_To_V2CA_ReaderMappingTypeFinder : IReaderMappingTypeFinder
    {
        public bool CanHandle(string name)
        {
            return name == "NSerializer.UATs.Version1_ClassA";
        }

        public Type Get(string name)
        {
            return typeof (Version2_ClassA);
        }
    }
}


namespace MyNamespace
{
    public class MyClass
    {
    }

    public class SampleReaderMappingTypeFinder : IReaderMappingTypeFinder
    {
        public bool CanHandle(string name)
        {
            return name == "MyNewNamespace.MyClass";
        }

        public Type Get(string name)
        {
            return typeof (MyClass);
        }
    }
}