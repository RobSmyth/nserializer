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
using NSerializer.Framework.Types;


namespace NSerializer.Migration
{
    internal class TypeNameMapper : ITypeNameMapper
    {
        private readonly IMigrationDefinition migrationDefinition;

        public TypeNameMapper(IMigrationDefinition migrationDefinition)
        {
            this.migrationDefinition = migrationDefinition;
        }

        public bool CanHandle(Type type)
        {
            return migrationDefinition.HasTypeDefinition(type);
        }

        public string GetTypeName(Type type)
        {
            return migrationDefinition.GetTypeDefinition(type).GetMappedName();
        }
    }
}