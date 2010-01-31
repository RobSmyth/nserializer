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
using NSerializer.Framework.Types;


namespace NSerializer.XML.Readers.Members
{
    public class DestinationType : ITargetType
    {
        private readonly Type type;
        private readonly ITypeFinder typeFinder;

        public DestinationType(Type type, ITypeFinder typeFinder)
        {
            this.type = type;
            this.typeFinder = typeFinder;
        }

        public DestinationType BaseType
        {
            get { return new DestinationType(type.BaseType, typeFinder); }
        }

        public string FullName
        {
            get { return type.FullName; }
        }

        public bool IsArray
        {
            get { return type.IsArray; }
        }

        public FieldInfo GetField(string fieldName, BindingFlags bindingFlags)
        {
            return type.GetField(fieldName, bindingFlags);
        }

        public Type GetTargetType()
        {
            return type;
        }

        public DestinationType GetElementType()
        {
            return new DestinationType(type.GetElementType(), typeFinder);
        }

        public ITargetType MakeArrayType()
        {
            return new DestinationType(type.MakeArrayType(), typeFinder);
        }
    }

    public class DestinationType<T> : DestinationType
    {
        public DestinationType(ITypeFinder typeFinder)
            : base(typeof(T), typeFinder)
        { }
    }
}