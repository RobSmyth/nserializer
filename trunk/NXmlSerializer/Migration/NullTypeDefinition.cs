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
using NSerializer.Migration.Fields;
using NSerializer.Migration.Types;


namespace NSerializer.Migration
{
    internal class NullTypeDefinition : ITypeDefinition
    {
        private readonly string typeName;

        public NullTypeDefinition(string typeName)
        {
            this.typeName = typeName;
        }

        public void SetAlias(string alias)
        {
            throw new NotImplementedException();
        }

        public void AddName(string matchingTypeName)
        {
            throw new NotImplementedException();
        }

        public bool HasFieldDefinition(string fieldName)
        {
            throw new NotImplementedException();
        }

        public void CreateFieldDefinition(string fieldName, IFieldDefinition parentFieldDefinition)
        {
            throw new NotImplementedException();
        }

        public IFieldDefinition FindFieldDefinition(string fieldName)
        {
            throw new NotImplementedException();
        }

        public bool Matches(string name)
        {
            return typeName == name;
        }

        public string GetTypeName()
        {
            return typeName;
        }

        public IFieldDefinition GetFieldDefinition(string fieldName)
        {
            return new NullFieldDefinition(fieldName);
        }

        public string GetMappedName()
        {
            return typeName;
        }
    }
}