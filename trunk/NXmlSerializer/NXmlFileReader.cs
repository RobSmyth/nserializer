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

using System.IO;
using System.Reflection;
using NSerializer.Framework;


namespace NSerializer
{
    public class NXmlFileReader
    {
        private readonly IApplicationObjectsRepository appObjectRepository;
        private readonly IDocumentObjectsRepository docObjectRepository;
        private readonly Assembly typeSeedAssembly;

        /// <summary>
        /// Load from file reader constructor.
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
        public NXmlFileReader(Assembly typeSeedAssembly, IApplicationObjectsRepository appObjectRepository,
                              IDocumentObjectsRepository docObjectRepository)
        {
            this.typeSeedAssembly = typeSeedAssembly;
            this.appObjectRepository = appObjectRepository;
            this.docObjectRepository = docObjectRepository;
        }

        /// <summary>
        /// Load from file.
        /// </summary>
        /// <param name="fileName">
        /// File name of file holding XML to read.
        /// </param>
        public T Load<T>(string fileName)
        {
            var streamReader = new StreamReader(fileName);
            var stringReader = new StringReader(streamReader.ReadToEnd());

            var reader = new NXmlReader(stringReader, typeSeedAssembly, appObjectRepository, docObjectRepository);
            var value = reader.Read<T>();

            streamReader.Close();

            return value;
        }
    }
}