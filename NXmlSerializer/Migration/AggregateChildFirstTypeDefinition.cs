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
using NSerializer.Migration.Fields;
using NSerializer.Migration.Types;


namespace NSerializer.Migration
{
    internal class AggregateChildFirstTypeDefinition : ITypeDefinition
    {
        private readonly ITypeDefinition childTypeDefinition;
        private readonly ITypeDefinition typeDefinition;

        public AggregateChildFirstTypeDefinition(ITypeDefinition typeDefinition, ITypeDefinition childTypeDefinition)
        {
            this.typeDefinition = typeDefinition;
            this.childTypeDefinition = childTypeDefinition;
        }

        public void SetAlias(string alias)
        {
            typeDefinition.SetAlias(alias);
        }

        public void AddName(string matchingTypeName)
        {
            typeDefinition.AddName(matchingTypeName);
        }

        public bool HasFieldDefinition(string fieldName)
        {
            return childTypeDefinition.HasFieldDefinition(fieldName) || typeDefinition.HasFieldDefinition(fieldName);
        }

        public void CreateFieldDefinition(string fieldName, IFieldDefinition parentFieldDefinition)
        {
            typeDefinition.CreateFieldDefinition(fieldName, parentFieldDefinition);
        }

        public IFieldDefinition FindFieldDefinition(string fieldName)
        {
            return typeDefinition.FindFieldDefinition(fieldName);
        }

        public bool Matches(string name)
        {
            if (childTypeDefinition.Matches(name))
            {
                name = childTypeDefinition.GetTypeName();
            }
            return typeDefinition.Matches(name);
        }

        public string GetTypeName()
        {
            return typeDefinition.GetTypeName();
        }

        public IFieldDefinition GetFieldDefinition(string name)
        {
            return typeDefinition.GetFieldDefinition(name);// >>> always returns something e.g. NullFieldDefintion
        }

        public string GetMappedName()
        {
            var topMappedName = typeDefinition.GetMappedName();
            if (string.IsNullOrEmpty(topMappedName)) // >>> will not work as type definition will return alias or type name
            {
                return topMappedName;                
            }
            return childTypeDefinition.GetMappedName();
        }
    }
}