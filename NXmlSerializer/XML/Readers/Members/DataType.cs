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

// Project site: http://code.google.com/p/nserializer/

using System;
using System.Reflection;
using NSerializer.Migration;
using NSerializer.Migration.Fields;


namespace NSerializer.XML.Readers.Members
{
    internal class DataType : IDataType
    {
        private readonly IMigrationDefinition migrationDefiniton;
        private readonly Type type;

        public DataType(Type type, IMigrationDefinition migrationDefiniton)
        {
            this.type = type;
            this.migrationDefiniton = migrationDefiniton;
        }

        public IDataType BaseType
        {
            get { return new DataType(type.BaseType, migrationDefiniton); }
        }

        public string FullName
        {
            get { return type.FullName; }
        }

        public bool IsArray
        {
            get { return type.IsArray; }
        }

        public IField GetField(string fieldName)
        {
            IFieldDefinition fieldDefinition = new NullFieldDefinition(fieldName);
            if (migrationDefiniton != null)
            {
                var typeDefinition = migrationDefiniton.GetTypeDefinition(type);
                if (typeDefinition != null)
                {
                    fieldDefinition = typeDefinition.GetFieldDefinition(fieldName);

                    if (fieldDefinition.Ignored)
                    {
                        return new IgnoredField();
                    }

                    fieldName = fieldDefinition.Name;
                }
            }

            return
                new Field(
                    type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance),
                    fieldDefinition);
        }

        public Type GetTargetType()
        {
            return type;
        }

        public IDataType GetElementType()
        {
            return new DataType(type.GetElementType(), migrationDefiniton);
        }

        public IDataType MakeArrayType()
        {
            return new DataType(type.MakeArrayType(), migrationDefiniton);
        }

        public bool Equals(DataType other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.type, type);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DataType)) return false;
            return Equals((DataType) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((type != null ? type.GetHashCode() : 0)*397);
            }
        }

        public static implicit operator Type(DataType rhs)
        {
            return rhs.type;
        }

        public static bool operator ==(Type lhs, DataType rhs)
        {
            return lhs == rhs.type;
        }

        public static bool operator !=(Type lhs, DataType rhs)
        {
            return !(lhs == rhs);
        }
    }
}