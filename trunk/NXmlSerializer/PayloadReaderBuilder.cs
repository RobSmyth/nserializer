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

using NDependencyInjection.interfaces;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Readers;
using NSerializer.Framework.Types;
using NSerializer.XML.Readers;
using NSerializer.XML.Readers.Values;


namespace NSerializer
{
    public class PayloadReaderBuilder : ISubsystemBuilder
    {
        private readonly IMetaData metaData;
        private readonly ITypeFinder typeFinder;

        public PayloadReaderBuilder(IMetaData metaData, ITypeFinder typeFinder)
        {
            this.metaData = metaData;
            this.typeFinder = typeFinder;
        }

        public void Build(ISystemDefinition system)
        {
            system.HasSingleton<ReadObjectsCache>()
                .Provides<IReadObjectsCache>();

            system.HasSubsystem(new NodeReaderBuilder())
                .Provides<NodeReader>();

            system.HasSingleton<DefaultValueReader>()
                .Provides<IObjectReader>();

            system.HasInstance(metaData)
                .Provides<IMetaData>()
                .Provides<MetaData>();

            system.HasInstance(typeFinder)
                .Provides<ITypeFinder>();

            system.HasSingleton<PayloadReader>()
                .Provides<PayloadReader>();
        }
    }
}