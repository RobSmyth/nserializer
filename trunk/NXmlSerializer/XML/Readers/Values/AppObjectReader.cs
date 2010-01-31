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

using NSerializer.Framework;
using NSerializer.Framework.Readers;
using NSerializer.Framework.Types;


namespace NSerializer.XML.Readers.Values
{
    internal class AppObjectReader : IObjectReader
    {
        private readonly IApplicationObjectsRepository appObjectRepository;
        private readonly IReadObjectsCache readObjects;
        private readonly ITypeFinder typeFinder;

        public AppObjectReader(IReadObjectsCache readObjects, IApplicationObjectsRepository appObjectRepository,
                               ITypeFinder typeFinder)
        {
            this.readObjects = readObjects;
            this.appObjectRepository = appObjectRepository;
            this.typeFinder = typeFinder;
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "appObject";
        }

        public object Get(INXmlElementReader nodeReader)
        {
            var typeName = nodeReader.Attributes.Get("type");
            var type = typeFinder.Get(typeName);

            var instance = appObjectRepository.Get(type.GetTargetType());
            readObjects.Add(nodeReader.Attributes.GetInteger("ID"), instance);

            return instance;
        }
    }
}