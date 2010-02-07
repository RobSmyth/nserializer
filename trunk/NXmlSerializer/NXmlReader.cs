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
using NDependencyInjection;
using NDependencyInjection.interfaces;
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
        private readonly ISystemDefinition system;

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
            : this(typeSeedAssembly, null)
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
        public NXmlReader(Assembly typeSeedAssembly, ISubsystemBuilder pluginsBuilder)
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
                          IDocumentObjectsRepository docObjectRepository, ISubsystemBuilder dependencyInjectionBuilder)
            : this(typeSeedAssembly, appObjectRepository, docObjectRepository, null, dependencyInjectionBuilder)
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
                          IDocumentObjectsRepository docObjectRepository, IMigrationRulesBuilder migrationRulesBuilder,
                          ISubsystemBuilder pluginsBuilder)
        {
            system = new SystemDefinition();

            system.HasInstance(appObjectRepository ?? new NullApplicationObjectRepository())
                .Provides<IApplicationObjectsRepository>();

            system.HasInstance(docObjectRepository ?? new NullDocumentObjectRepository())
                .Provides<IDocumentObjectsRepository>();

            system.HasInstance(migrationRulesBuilder ?? new NullMigrationRulesBuilder())
                .Provides<IMigrationRulesBuilder>();

            new DefaultPluginsBuilder().Build(system);

            if (pluginsBuilder != null)
            {
                system = system.CreateSubsystem(pluginsBuilder);
            }

            system.HasSubsystem(new ReaderBuilder(typeSeedAssembly))
                .Provides<IDataTypeFactory>()
                .Provides<ITypeFinder>();

            system.HasSubsystem(new MetadataReaderBuilder())
                .Provides<MetaDataReader>();
        }

        /// <summary>
        /// Read XML text written using NSerializer.
        /// Project site: http://code.google.com/p/nserializer/
        /// </summary>
        public T Read<T>(Stream inputStream)
        {
            system.Get<IApplicationObjectsRepository>().Initialize();
            system.Get<IDocumentObjectsRepository>().Initialize();

            T valueRead;

            try
            {
                var typeFinder = system.Get<ITypeFinder>();
                var metaDataReader = system.Get<MetaDataReader>();

                var metaData = metaDataReader.Read(new XmlStreamReader(inputStream));

                system.HasSubsystem(new MigrationDefinitionFactoryBuilder(metaData))
                    .Provides<MigrationDefinitionFactory>();

                var migrationDefinition = system.Get<MigrationDefinitionFactory>().Create();
                system.Get<IDataTypeFactory>().SetMigration(migrationDefinition);
                typeFinder = migrationDefinition.GetTypeMigrator(typeFinder);

                system.HasSubsystem(new PayloadReaderBuilder(metaData,
                                                             new ReaderNameAliasingTypeFinder(typeFinder, metaData)))
                    .Provides<PayloadReader>();

                var payLoad = system.Get<PayloadReader>().Read(new XmlStreamReader(inputStream));

                valueRead = (T) payLoad.Target;
            }
            catch (NSerializerException)
            {
                throw;
            }
            catch (FileVersionNotSupportedException)
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