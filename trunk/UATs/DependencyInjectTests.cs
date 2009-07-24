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
using System.Reflection;
using NDependencyInjection;
using NDependencyInjection.Exceptions;
using NDependencyInjection.interfaces;
using NSerializer.Framework;
using NUnit.Framework;


namespace NSerializer.UATs
{
    [TestFixture]
    [Ignore("Work in progress")]
    public class DependencyInjectTests : SerializeContext
    {
        [Test]
        public void ApplicationScopeObjects_MembersAreNotSerialized()
        {
            ISystemDefinition writeTimeSystem = new SystemDefinition();
            NDependencyInjectAdapter writeTimeAdapter = new NDependencyInjectAdapter(writeTimeSystem);
            writeTimeSystem.HasInstance(new ClassB(1234))
                .Provides<IClassB>();
            ClassA writeObject = new ClassA(writeTimeSystem.Get<IClassB>());
            string xmlText = SerializeAsXml(writeObject, writeTimeAdapter);

            Assert.IsFalse(xmlText.Contains("1234"));

            ISystemDefinition readTimeSystem = new SystemDefinition();
            NDependencyInjectAdapter readTimeAdapter = new NDependencyInjectAdapter(readTimeSystem);
            ClassB readTimeAppObject = new ClassB(3);
            readTimeSystem.HasInstance(readTimeAppObject)
                .Provides<IClassB>();
            ClassA readObject = ReadXmlText<ClassA>(xmlText, readTimeAdapter, null, null);

            Assert.AreSame(readTimeAppObject, readObject.Value);
            Assert.AreEqual(3, readObject.Value.ValueA);
        }

        private class NDependencyInjectAdapter : IApplicationObjectsRepository
        {
            private readonly MethodInfo getMethodInfo;
            private readonly ISystemDefinition system;

            public NDependencyInjectAdapter(ISystemDefinition system)
            {
                this.system = system;
                getMethodInfo = system.GetType().GetMethod("Get");
            }

            public object Get(Type type)
            {
                MethodInfo genericMethodInfo = getMethodInfo.MakeGenericMethod(new Type[] {type});
                return genericMethodInfo.Invoke(system, new object[0]);
            }

            public bool HasType(Type type)
            {
                bool hasType = true;
                try
                {
                    MethodInfo genericMethodInfo = getMethodInfo.MakeGenericMethod(new Type[] {type});
                    genericMethodInfo.Invoke(system, new object[0]);
                }
                catch (TargetInvocationException exception)
                {
                    if (exception.InnerException != null &&
                        exception.InnerException.GetType() == typeof (UnknownTypeException))
                    {
                        hasType = false; // >>> hack waiting more info from NDependencyInjection
                    }
                    else
                    {
                        throw;
                    }
                }
                return hasType;
            }

            public void Initialize()
            {
                throw new NotImplementedException();
            }
        }

        private class ClassA
        {
            private readonly IClassB value;

            public ClassA(IClassB value)
            {
                this.value = value;
            }

            public IClassB Value
            {
                get { return value; }
            }
        }

        private class ClassB : IClassB
        {
            private readonly int valueA;

            public ClassB(int valueA)
            {
                this.valueA = valueA;
            }

            public int ValueA
            {
                get { return valueA; }
            }
        }

        private interface IClassB
        {
            int ValueA { get; }
        }
    }
}