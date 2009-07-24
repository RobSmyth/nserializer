#region Copyright

/*---------------------------------------------------------------------------
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in compliance
 * with the License. You may obtain a copy of the License at
 * 
 * http://www.mozilla.org/MPL/
 * 
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See the
 * License for the specific language governing rights and limitations under 
 * the License.
 * 
 * The Initial Developer of the Original Code is Robert Smyth.
 * Portions created by Robert Smyth are Copyright (C) 2008.
 * 
 * All Rights Reserved.
 *---------------------------------------------------------------------------*/

#endregion

using System;
using System.Collections.Generic;
using System.Reflection;
using NXmlSerializer.TypeHandling;
using NXmlSerializer.XML;
using NXmlSerializer.XML.Exceptions;


namespace NXmlSerializer.TypeHandling
{
    public class TypeAccessor
    {
        private readonly FieldInfo[] fields;
        private readonly PropertyInfo[] properties;
        private readonly TypeOptions options;
        private readonly Type type;

        public TypeAccessor(Type type, TypeOptions options)
        {
            this.type = type;
            this.options = options;

            fields = GetFields();
            properties = GetProperties();
        }

        public MemberInfo[] GetMembers()
        {
            List<MemberInfo> memberInfos = new List<MemberInfo>();

            memberInfos.AddRange(properties);
            memberInfos.AddRange(fields);

            return memberInfos.ToArray();
        }

        public ConstructorInfo GetConstructor()
        {
            ConstructorInfo constructor = FindDefaultConstructor();
            if (constructor == null)
            {
                constructor = FindConstructorWithParametersMatchingFields();
            }
            if (constructor == null)
            {
                throw new UnableToReadXMLTextException(string.Format("Unable to find suitable constructor for type '{0}'.", type.FullName));
            }

            return constructor;
        }

        private ConstructorInfo FindConstructorWithParametersMatchingFields()
        {
            ConstructorInfo bestConstructor = null;

            // TODO: Old implementation was defective.
            ConstructorInfo[] constructors = GetConstructors();
            foreach (ConstructorInfo constructor in constructors)
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
            List<FieldInfo> filteredFields = new List<FieldInfo>();
            FieldInfo[] foundFields = options.UsesFields
                                          ?
                                              type.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                             BindingFlags.NonPublic)
                                          :
                                              new FieldInfo[0];

            foreach (FieldInfo fieldInfo in foundFields)
            {
                if (fieldInfo.GetCustomAttributes(typeof (NXmlIgnoreAttribute), false).Length == 0 &&
                    !IsAnEvent(fieldInfo))
                {
                    filteredFields.Add(fieldInfo);
                }
            }

            return filteredFields.ToArray();
        }

        private PropertyInfo[] GetProperties()
        {
            return options.UsePublicProperties
                       ?
                           type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                       :
                           new PropertyInfo[0];
        }

        private static bool IsAnEvent(FieldInfo fieldInfo)
        {
            return typeof(MulticastDelegate).IsAssignableFrom(fieldInfo.FieldType);
        }
    }
}