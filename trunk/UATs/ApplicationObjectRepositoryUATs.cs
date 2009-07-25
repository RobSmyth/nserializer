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
using System.Collections.Generic;
using NSerializer.Framework;
using NSerializer.UATs.Contexts;
using NUnit.Framework;


namespace NSerializer.UATs
{
    [TestFixture]
    public class ApplicationObjectRepositoryUATs : SerializeContext
    {
        [Test]
        public void CanSerializeClassWithFieldFromExternalRepository()
        {
            var writeTimeRespository = new TestRepository();
            var writeTimeInstance = new ClassB(7);
            writeTimeRespository.Add(writeTimeInstance.GetType(), writeTimeInstance);
            var xmlText = SerializeAsXml(new ClassA(writeTimeInstance), writeTimeRespository);

            var readTimeRepository = new TestRepository();
            var readTimeInstance = new ClassB(23);
            readTimeRepository.Add(readTimeInstance.GetType(), readTimeInstance);
            var readClass = ReadXmlText<ClassA>(xmlText, readTimeRepository, null, null);
            Assert.AreSame(readTimeInstance, readClass.Parameter);
            Assert.AreEqual(23, readClass.Parameter.Parameter);
        }

        [Test]
        [Ignore("Work in progress")]
        public void CanSetStateOfExternalInstance()
        {
            var writeTimeRespository = new TestRepository();
            var writeTimeInstance = new ClassB(3);
            var xmlText = SerializeAsXml(new ClassA(writeTimeInstance), writeTimeRespository);

            var readTimeRepository = new TestRepository();
            var readTimeInstance = new ClassB(23);
            readTimeRepository.Add(readTimeInstance.GetType(), readTimeInstance);
            var readClass = ReadXmlText<ClassA>(xmlText, readTimeRepository, null, null);
            Assert.AreSame(readTimeInstance, readClass.Parameter);
            Assert.AreEqual(3, readClass.Parameter.Parameter);
        }

        private class ClassA
        {
            private readonly ClassB parameter;

            public ClassA(ClassB parameter)
            {
                this.parameter = parameter;
            }

            public ClassB Parameter
            {
                get { return parameter; }
            }
        }

        private class ClassB
        {
            private readonly int parameter;

            public ClassB(int parameter)
            {
                this.parameter = parameter;
            }

            public int Parameter
            {
                get { return parameter; }
            }
        }

        private class TestRepository : IApplicationObjectsRepository
        {
            private readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();

            public void Initialize()
            {
            }

            public void Add(Type type, object instance)
            {
                instances.Add(type, instance);
            }

            object IInstanceRepository.Get(Type type)
            {
                return instances[type];
            }

            bool IInstanceRepository.HasType(Type type)
            {
                var hasType = instances.ContainsKey(type);
                return hasType;
            }
        }
    }
}