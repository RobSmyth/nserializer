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

using System.Reflection;
using NSerializer.Framework;
using NSerializer.Framework.Readers;
using NSerializer.TypeFinders;


namespace NSerializer.XML.Readers.Values
{
    public class DefaultValueReader : IObjectReader
    {
        private readonly NodeReader reader;

        public DefaultValueReader(ITypeFinder typeFinder, IApplicationObjectsRepository appObjectRepository,
                                  IDocumentObjectsRepository docObjectRepository)
        {
            reader = new NodeReader(typeFinder, appObjectRepository, docObjectRepository);
        }

        public DefaultValueReader(Assembly seedAssembly, IApplicationObjectsRepository appObjectRepository,
                                  IDocumentObjectsRepository docObjectRepository)
        {
            reader = new NodeReader(new DefaultTypeFinder(seedAssembly), appObjectRepository, docObjectRepository);
        }

        public bool CanRead(INXmlElementReader nodeReader)
        {
            return reader.CanRead(nodeReader);
        }

        public object Get(INXmlElementReader nodeReader)
        {
            return reader.Get(nodeReader);
        }
    }
}