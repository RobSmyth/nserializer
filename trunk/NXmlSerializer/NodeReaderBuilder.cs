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
using NDependencyInjection.interfaces;
using NSerializer.Framework;
using NSerializer.Framework.Readers;
using NSerializer.Framework.Readers.Values;
using NSerializer.XML.Readers.Members;
using NSerializer.XML.Readers.Values;


namespace NSerializer
{
    public class NodeReaderBuilder : ISubsystemBuilder
    {
        public void Build(ISystemDefinition system)
        {
            system.HasSingleton<ReadObjectsCache>()
                .Provides<IReadObjectsCache>();

            system.HasCollection(
                new CustomTypeReaderBuilder<String>("string"),
                new CustomTypeReaderBuilder<Int32>("Int32"),
                new CustomTypeReaderBuilder<Int64>("Int64"),
                new CustomTypeReaderBuilder<UInt32>("UInt32"),
                new CustomTypeReaderBuilder<UInt64>("UInt64"),
                new ObjectReaderBuilder<DoubleReader>(),
                new CustomTypeReaderBuilder<Single>("single"),
                new CustomTypeReaderBuilder<Boolean>("bool"),
                new CustomTypeReaderBuilder<Char>("char"),
                new ObjectReaderBuilder<PrimitiveValueTypeReader>(),
                new ObjectReaderBuilder<EnumReader>(),
                new ObjectReaderBuilder<GuidReader>(),
                new ObjectReaderBuilder<TimeSpanReader>(),
                new ObjectReaderBuilder<DateTimeReader>(),
                new ObjectReaderBuilder<ObjectReferenceReader>(),
                new ObjectReaderBuilder<ArrayOfDoublesReader>(),
                new ObjectReaderBuilder<ArrayOfObjectsReader>(),
                new ObjectReaderBuilder<ListReader>(),
                new ObjectReaderBuilder<DictionaryReader>(),
                new ObjectReaderBuilder<AppObjectReader>(),
                new ObjectReaderBuilder<MetaDataTypeNameReader>(),
                new ObjectReaderBuilder<ClassReader>(),
                new ObjectReaderBuilder<ValueTypeReader>(),
                new ObjectReaderBuilder<VersionReader>(),
                new ObjectReaderBuilder<NullReader>()
                )
                .Provides<IObjectReader[]>();

            system.HasCollection(
                new MemberReaderBuilder<FieldReader>() 
                )
                .Provides<IMemberReader[]>();

            system.HasSingleton<MemberReader>()
                .Provides<IMemberReader>();

            system.HasSingleton<NodeReader>()
                .Provides<IObjectReader>()
                .Provides<IBaseTypeMembersReader>()
                .Provides<NodeReader>();
        }
    }
}