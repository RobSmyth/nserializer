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
using System.IO;
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Types;
using NSerializer.Migration;
using NSerializer.XML.Document;
using NSerializer.XML.Writers.Values;


namespace NSerializer
{
    public class NXmlWriter
    {
        private readonly IApplicationObjectsRepository appObjectRepository;
        private readonly IDocumentWriter document;
        private readonly IMigrationRulesBuilder migrationRulesBuilder;

        public NXmlWriter(IDocumentWriter document, IApplicationObjectsRepository appObjectRepository)
            : this(document, appObjectRepository, null)
        {
        }

        public NXmlWriter(IDocumentWriter document, IApplicationObjectsRepository appObjectRepository,
                          IMigrationRulesBuilder migrationRulesBuilder)
        {
            this.document = document;
            this.appObjectRepository = appObjectRepository;
            this.migrationRulesBuilder = migrationRulesBuilder ?? new NullMigrationRulesBuilder();
        }

        /// <summary>
        /// Write object/value to XML to string writer.
        /// Project site: http://code.google.com/p/nserializer/
        /// </summary>
        /// <param name="value">
        /// Object instance to write.
        /// </param>
        public void Write(object value, TextWriter writer)
        {
            if (appObjectRepository != null)
            {
                appObjectRepository.Initialize();
            }

            var version = value.GetType().Assembly.GetName().Version;

            var typeNameMapper =
                new MigrationDefinitionFactory(version, migrationRulesBuilder).Create().GetTypeNameMapper();

            var typeNamesCache = new TypeNamesCache(typeNameMapper);
            var valueWriterFactory = new DefaultValueWriterFactory(document, appObjectRepository,
                                                                   typeNamesCache);
            var objectWriter = valueWriterFactory.Create();

            document.BeginWrite(writer);

            var payload = new Payload(value);
            objectWriter.Write(payload, document.RootNode, payload.GetType());

            WriteMetadata(version, typeNamesCache);

            document.EndWrite();
        }

        private void WriteMetadata(Version targetVersion, ITypeNamesCache typeNamesCache)
        {
            var valueWriterFactory = new DefaultValueWriterFactory(document, appObjectRepository,
                                                                   new NullTypeNamesCache());
            var metaDataWriter = valueWriterFactory.Create();
            var metaData = new MetaData(typeNamesCache.Names, targetVersion);
            metaDataWriter.Write(metaData, document.RootNode, metaData.GetType());
        }
    }
}