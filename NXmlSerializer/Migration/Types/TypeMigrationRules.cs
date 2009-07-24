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

using NSerializer.Migration.Fields;


namespace NSerializer.Migration.Types
{
    internal class TypeMigrationRules<T> : ITypeMigrationRules
    {
        private readonly ITypeDefinition typeDefinition;

        public TypeMigrationRules(ITypeDefinition typeDefinition)
        {
            this.typeDefinition = typeDefinition;
        }

        public ITypeMigrationRules UseAlias(string alias)
        {
            typeDefinition.SetAlias(alias);
            return this;
        }

        public IFieldRules Field(string fieldName)
        {
            if (!typeDefinition.HasFieldDefinition(fieldName))
            {
                typeDefinition.CreateFieldDefinition(fieldName, typeDefinition.FindFieldDefinition(fieldName));
            }
            return new FieldRules(typeDefinition.FindFieldDefinition(fieldName), this);
        }

        public ITypeMigrationRules MatchesTypeName(string typeName)
        {
            typeDefinition.AddName(typeName);
            return this;
        }
    }
}