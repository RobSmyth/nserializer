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
using System.Collections.Generic;
using NSerializer.Framework.Types;


namespace NSerializer.Framework.Document
{
    public class MetaData : IMetaData
    {
        private readonly DateTime dateTimeCreated;
        private readonly string machineName;
        private readonly Version serializerVersion;
        private readonly Version targetVersion;
        private readonly MetaDataTypeName[] metaDataTypeNames;

        public MetaData(MetaDataTypeName[] metaDataTypeNames, Version targetVersion)
        {
            this.metaDataTypeNames = metaDataTypeNames;
            this.targetVersion = targetVersion;

            machineName = Environment.MachineName;
            dateTimeCreated = DateTime.Now;
            serializerVersion = GetType().Assembly.GetName().Version;
        }

        public string[] TypeNames
        {
            get
            {
                var names = new List<string>();
                foreach (var typeName in metaDataTypeNames)
                {
                    names.Add(typeName.Name);
                }
                return names.ToArray();
            }
        }

        public Version PayloadVersion
        {
            get { return targetVersion; }
        }

        public string MachineName
        {
            get { return machineName; }
        }

        public DateTime DateTimeCreated
        {
            get { return dateTimeCreated; }
        }

        public Version SerializerVersion
        {
            get { return serializerVersion; }
        }
    }
}