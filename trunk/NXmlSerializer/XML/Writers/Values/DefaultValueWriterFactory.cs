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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using NSerializer.Framework;
using NSerializer.Framework.Types;
using NSerializer.Framework.Writers;
using NSerializer.Framework.Writers.Values;
using NSerializer.XML.Document;
using NSerializer.XML.Writers.Members;


namespace NSerializer.XML.Writers.Values
{
    public class DefaultValueWriterFactory
    {
        private readonly IDocumentWriter document;
        private readonly IApplicationObjectsRepository repository;
        private readonly ITypeNamesCache typeNamesCache;

        public DefaultValueWriterFactory(IDocumentWriter document, IApplicationObjectsRepository repository,
                                         ITypeNamesCache typeNamesCache)
        {
            this.document = document;
            this.repository = repository;
            this.typeNamesCache = typeNamesCache;

            if (repository == null)
            {
                this.repository = new NullApplicationObjectRepository();
            }
        }

        public IObjectWriter Create()
        {
            var valueWriterConduit = new ValueWriterConduit();

            IValuesCache valuesCache = new ValuesCache();
            IMemberWriter memberWriter = new MemberWriter(document, valueWriterConduit);

            var objectWriter = new ValueWriter(document,
                                               new IObjectWriter[]
                                                   {
                                                       new NotSupportedTypesWriter(),
                                                       new NullValueWriter(document),
                                                       new ObjectReferenceWriter(document, valuesCache),
                                                       new AppObjectWriter(document, repository,
                                                                           valuesCache, typeNamesCache),
                                                       new ListWriter(typeof (List<>), document,
                                                                      valuesCache, valueWriterConduit,
                                                                      typeNamesCache),
                                                       new DictionaryWriter(document, valuesCache,
                                                                            valueWriterConduit, typeNamesCache),
                                                       new ArrayOfDoublesXmlWriter(document, valuesCache,
                                                                                   typeNamesCache),
                                                       new ArrayOfObjectsWriter(document, valuesCache,
                                                                                valueWriterConduit,
                                                                                typeNamesCache),
                                                       new EnumXmlWriter(document, typeNamesCache),
                                                       new CustomTypeXmlWriter<Int32>(document, "Int32"),
                                                       new CustomTypeXmlWriter<Int64>(document, "Int64"),
                                                       new CustomTypeXmlWriter<UInt32>(document, "UInt32"),
                                                       new CustomTypeXmlWriter<UInt64>(document, "UInt64"),
                                                       new DoubleXmlWriter(document),
                                                       new CustomTypeXmlWriter<Single>(document, "single"),
                                                       new CustomTypeXmlWriter<String>(document, "string"),
                                                       new CustomTypeXmlWriter<Boolean>(document, "bool"),
                                                       new CustomTypeXmlWriter<Char>(document, "char"),
                                                       new CustomTypeXmlWriter<Guid>(document, "guid"),
                                                       new CustomTypeXmlWriter<TimeSpan>(document, "timespan"),
                                                       new CustomTypeXmlWriter<DateTime>(document, "datetime"),
                                                       new CustomTypeXmlWriter<Version>(document, "version"),
                                                       new PrimitiveValueTypeXmlWriter(document),
                                                       new ValueTypeWriter(document, memberWriter,
                                                                           typeNamesCache),
                                                       new ListWriter(typeof (BindingList<>), document,
                                                                      valuesCache,
                                                                      valueWriterConduit, typeNamesCache),
                                                       new ListWriter(typeof (ArrayList), document,
                                                                      valuesCache,
                                                                      valueWriterConduit, typeNamesCache),
                                                       new ClassWriter(document, valuesCache, memberWriter,
                                                                       valueWriterConduit, typeNamesCache)
                                                   },
                                               new IBaseTypeMembersWriter[]
                                                   {
                                                       new ListWriter(typeof (List<>), document,
                                                                      valuesCache, valueWriterConduit,
                                                                      typeNamesCache),
                                                       new DictionaryWriter(document, valuesCache,
                                                                            valueWriterConduit, typeNamesCache),
                                                       new ListWriter(typeof (ArrayList), document,
                                                                      valuesCache, valueWriterConduit,
                                                                      typeNamesCache),
                                                       new ListWriter(typeof (BindingList<>), document,
                                                                      valuesCache, valueWriterConduit,
                                                                      typeNamesCache),
                                                       new ClassBaseWriter(document, memberWriter,
                                                                           valueWriterConduit, typeNamesCache)
                                                   });

            valueWriterConduit.SetTarget(objectWriter, objectWriter);

            return objectWriter;
        }
    }
}