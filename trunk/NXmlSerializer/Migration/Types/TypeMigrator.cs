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
using NSerializer.TypeFinders;


namespace NSerializer.Migration.Types
{
    internal class TypeMigrator : ITypeMigrator
    {
        private readonly IMigrationDefinition migrationDefinition;
        private readonly ITypeFinder typeFinder;

        public TypeMigrator(ITypeFinder typeFinder, IMigrationDefinition migrationDefinition)
        {
            this.typeFinder = typeFinder;
            this.migrationDefinition = migrationDefinition;
        }

        public Type Get(string typeName)
        {
            var typeDefinition = migrationDefinition.GetTypeDefinition(typeName);
            if (typeDefinition != null)
            {
                typeName = typeDefinition.GetTypeName();
            }
            return typeFinder.Get(typeName);
        }

        public IFieldMigrator GetFieldMigrator()
        {
            return new FieldMigrator(migrationDefinition);
        }
    }
}