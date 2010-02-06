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
using NSerializer.Migration.Fields;
using NSerializer.XML.Readers.Members;


namespace NSerializer.Migration.Types
{
    internal class MigrationTypeFinder : ITypeFinder
    {
        private readonly IMigrationDefinition migrationDefinition;
        private readonly ITypeFinder typeFinder;

        public MigrationTypeFinder(ITypeFinder typeFinder, IMigrationDefinition migrationDefinition)
        {
            this.typeFinder = typeFinder;
            this.migrationDefinition = migrationDefinition;
        }

        public IDataType GetType(string typeName)
        {
            var typeDefinition = migrationDefinition.GetTypeDefinition(typeName);
            if (typeDefinition != null)
            {
                //Console.WriteLine("was {0}", typeName);//>>>
                typeName = typeDefinition.GetTypeName();
                //Console.WriteLine("\tnow {0}", typeName);//>>>
            }
            //Console.WriteLine("get {0}", typeName);//>>>
            return typeFinder.GetType(typeName);
        }
    }
}