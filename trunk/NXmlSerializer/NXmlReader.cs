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
using System.Reflection;
using NSerializer.Exceptions;
using NSerializer.Framework;
using NSerializer.Framework.Types;
using NSerializer.Migration;
using NSerializer.XML.Readers;
using NSerializer.XML.Readers.Members;


namespace NSerializer
{
    public class NXmlReader
    {
        private readonly IApplicationObjectsRepository appObjectRepository;
        private readonly IDocumentObjectsRepository docObjectRepository;
        private readonly IMigrationRulesBuilder migrationRulesBuilder;
        private readonly Assembly typeSeedAssembly;

        /// <summary>
        /// XML text reader constructor.
        /// Project site: http://code.google.com/p/nserializer/
        /// </summary>
        /// <param name="typeSeedAssembly">
        /// Seed assembly used for type discovery. 
        /// All types used in the XML file must be in this assembly or referenced assemblies.
        /// The reader will recursively search referenced assemblies. Only used if the xmlNodeReader
        /// parameter is null.
        /// </param>
        public NXmlReader(Assembly typeSeedAssembly)
            : this(typeSeedAssembly, null, null, null)
        {
        }

        /// <summary>
        /// XML text reader constructor.
        /// Project site: http://code.google.com/p/nserializer/
        /// </summary>
        /// <param name="typeSeedAssembly">
        /// Seed assembly used for type discovery. 
        /// All types used in the XML file must be in this assembly or referenced assemblies.
        /// The reader will recursively search referenced assemblies. Only used if the xmlNodeReader
        /// parameter is null.
        /// </param>
        /// <param name="appObjectRepository">
        /// Optional external repository. Used to provide instances of application scope objects.
        /// </param>
        /// <param name="docObjectRepository">
        /// Optional external repository. Used to provide instances of document scope objects.
        /// </param>
        public NXmlReader(Assembly typeSeedAssembly, IApplicationObjectsRepository appObjectRepository,
                          IDocumentObjectsRepository docObjectRepository)
            : this(typeSeedAssembly, appObjectRepository, docObjectRepository, null)
        {
        }

        /// <summary>
        /// XML text reader constructor.
        /// Project site: http://code.google.com/p/nserializer/
        /// </summary>
        /// <param name="typeSeedAssembly">
        /// Seed assembly used for type discovery. 
        /// All types used in the XML file must be in this assembly or referenced assemblies.
        /// The reader will recursively search referenced assemblies. Only used if the xmlNodeReader
        /// parameter is null.
        /// </param>
        /// <param name="appObjectRepository">
        /// Optional external repository. Used to provide instances of application scope objects.
        /// </param>
        /// <param name="docObjectRepository">
        /// Optional external repository. Used to provide instances of document scope objects.
        /// </param>
        /// <param name="migrationRulesBuilder">
        /// Optional migration rules builder.
        /// </param>
        public NXmlReader(Assembly typeSeedAssembly, IApplicationObjectsRepository appObjectRepository,
                          IDocumentObjectsRepository docObjectRepository, IMigrationRulesBuilder migrationRulesBuilder)
        {
            this.typeSeedAssembly = typeSeedAssembly;
            this.appObjectRepository = appObjectRepository ?? new NullApplicationObjectRepository();
            this.docObjectRepository = docObjectRepository ?? new NullDocumentObjectRepository();
            this.migrationRulesBuilder = migrationRulesBuilder ?? new NullMigrationRulesBuilder();
        }

        /// <summary>
        /// Read XML text written using NSerializer.
        /// Project site: http://code.google.com/p/nserializer/
        /// </summary>
        public T Read<T>(Stream inputStream)
        {
            appObjectRepository.Initialize();
            docObjectRepository.Initialize();

            T valueRead;

            try
            {
                ITypeFinder typeFinder = new DefaultTypeFinder(typeSeedAssembly);

                var metaDataReader = new MetaDataReader(typeFinder);
                var metaData = metaDataReader.Read(new XmlStreamReader(inputStream));

                var migrationDefinition =
                    new MigrationDefinitionFactory(metaData.PayloadVersion, migrationRulesBuilder).Create();
                typeFinder = migrationDefinition.GetTypeMigrator(typeFinder);

                var payloadReader = new PayloadReader(new ReaderNameAliasingTypeFinder(typeFinder,
                                                                                      metaData.TypeNames),
                                                      appObjectRepository, docObjectRepository);
                var payLoad = payloadReader.Read(new XmlStreamReader(inputStream));

                valueRead = (T) payLoad.Target;
            }
            catch (NSerializerException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new UnableToReadXMLTextException("Error while reading XML text.", exception);
            }

            return valueRead;
        }
    }
}