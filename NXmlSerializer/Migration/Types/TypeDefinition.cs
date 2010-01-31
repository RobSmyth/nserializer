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
using System.Collections.Generic;
using NSerializer.Migration.Fields;


namespace NSerializer.Migration.Types
{
    internal class TypeDefinition<T> : ITypeDefinition
    {
        private readonly Dictionary<string, IFieldDefinition> fieldDefinitions =
            new Dictionary<string, IFieldDefinition>();

        private readonly Dictionary<string, int> matchingNames = new Dictionary<string, int>();
        private readonly ITypeDefinition parentTypeDefinition;
        private string alias = string.Empty;

        public TypeDefinition(ITypeDefinition parentTypeDefinition)
        {
            this.parentTypeDefinition = parentTypeDefinition;
        }

        public void SetAlias(string nameAlias)
        {
            if (alias != string.Empty)
            {
                throw new InvalidOperationException("Cannot assign type alias twice in same scope");
            }
            alias = nameAlias;
        }

        public void AddName(string matchingTypeName)
        {
            matchingNames.Add(matchingTypeName, 0);
        }

        public bool HasFieldDefinition(string fieldName)
        {
            return fieldDefinitions.ContainsKey(fieldName);
        }

        public void CreateFieldDefinition(string fieldName, IFieldDefinition parentFieldDefinition)
        {
            fieldDefinitions.Add(fieldName, new FieldDefinition(parentFieldDefinition));
        }

        public IFieldDefinition FindFieldDefinition(string fieldName)
        {
            IFieldDefinition fieldDefinition;
            if (HasFieldDefinition(fieldName))
            {
                fieldDefinition = fieldDefinitions[fieldName];
            }
            else
            {
                fieldDefinition = parentTypeDefinition.FindFieldDefinition(fieldName);
            }
            return fieldDefinition;
        }

        public bool Matches(string name)
        {
            var matches = matchingNames.ContainsKey(name) || name == alias || typeof (T).Name == name;
            if (!matches && parentTypeDefinition != null)
            {
                parentTypeDefinition.Matches(name);
            }
            return matches;
        }

        public string GetTypeName()
        {
            return typeof (T).FullName;
        }

        public string GetMappedName()
        {
            return string.IsNullOrEmpty(alias) ? typeof (T).FullName : alias;
        }

        public IFieldDefinition GetFieldDefinition(string name)
        {
            throw new NotImplementedException();
        }
    }
}