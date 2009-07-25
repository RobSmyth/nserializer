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
using System.Reflection;
using NSerializer.Exceptions;
using NSerializer.Framework;


namespace NSerializer.Types
{
    public class TypeAccessor
    {
        private readonly FieldInfo[] fields;
        private readonly PropertyInfo[] properties;
        private readonly IInstanceRepository repository;
        private readonly Type type;

        public TypeAccessor(Type type, IInstanceRepository repository)
        {
            this.type = type;
            this.repository = repository;

            fields = GetFields();
            properties = GetProperties();
        }

        public MemberInfo[] GetMembers()
        {
            var memberInfos = new List<MemberInfo>();

            memberInfos.AddRange(properties);
            memberInfos.AddRange(fields);

            return memberInfos.ToArray();
        }

        public object GetInstance()
        {
            object instance;
            if (repository.HasType(type))
            {
                instance = repository.Get(type);
            }
            else
            {
                var constructor = GetConstructor();

                var parameters = GetConstructorParameterValues(constructor);
                try
                {
                    instance = constructor.Invoke(parameters);
                }
                catch (Exception exception)
                {
                    throw new UnableToReadXMLTextException(
                        string.Format("Exception thrown from contructor of type '{0}'.", type.FullName), exception);
                }
            }

            if (instance == null)
            {
                Exception exception = new UnableToReadXMLTextException(
                    string.Format("Unable to create instance of type '{0}'.", type.FullName));
                throw exception;
            }

            return instance;
        }

        public ConstructorInfo GetConstructor()
        {
            var constructor = FindDefaultConstructor();
            if (constructor == null)
            {
                constructor = FindConstructorWithParametersMatchingFields();
            }
            if (constructor == null)
            {
                throw new UnableToReadXMLTextException(
                    string.Format("Unable to find suitable constructor for type '{0}'.", type.FullName));
            }

            return constructor;
        }

        private static object[] GetConstructorParameterValues(ConstructorInfo constructor)
        {
            var parameters = new List<object>();
            foreach (var parameter in constructor.GetParameters())
            {
                parameters.Add(null);
            }
            return parameters.ToArray();
        }

        private ConstructorInfo FindConstructorWithParametersMatchingFields()
        {
            ConstructorInfo bestConstructor = null;

            // TODO: Old implementation was defective.
            var constructors = GetConstructors();
            foreach (var constructor in constructors)
            {
                bestConstructor = constructor;
            }

            return bestConstructor;
        }

        private ConstructorInfo[] GetConstructors()
        {
            return type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        }

        private ConstructorInfo FindDefaultConstructor()
        {
            return type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                                       null, new Type[0], null);
        }

        private FieldInfo[] GetFields()
        {
            var filteredFields = new List<FieldInfo>();
            var foundFields = type.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                             BindingFlags.NonPublic);

            foreach (var fieldInfo in foundFields)
            {
                if (fieldInfo.GetCustomAttributes(typeof (NSerializerIgnoreAttribute), false).Length == 0 &&
                    !IsAnEvent(fieldInfo))
                {
                    filteredFields.Add(fieldInfo);
                }
            }

            return filteredFields.ToArray();
        }

        private PropertyInfo[] GetProperties()
        {
            return new PropertyInfo[0];
        }

        private static bool IsAnEvent(FieldInfo fieldInfo)
        {
            return typeof (MulticastDelegate).IsAssignableFrom(fieldInfo.FieldType);
        }
    }
}