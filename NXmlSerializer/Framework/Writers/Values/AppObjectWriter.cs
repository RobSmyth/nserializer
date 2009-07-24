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
using NSerializer.TypeFinders;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class AppObjectWriter : IObjectWriter
    {
        private readonly IInstanceRepository appObjectRepository;
        private readonly IDocumentWriter ownerDocument;
        private readonly ITypeNamesCache typeNamesCache;
        private readonly IValuesCache valuesCache;

        public AppObjectWriter(IDocumentWriter ownerDocument, IInstanceRepository appObjectRepository,
                               IValuesCache valuesCache, ITypeNamesCache typeNamesCache)
        {
            this.ownerDocument = ownerDocument;
            this.appObjectRepository = appObjectRepository;
            this.valuesCache = valuesCache;
            this.typeNamesCache = typeNamesCache;
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return appObjectRepository.HasType(referencedAsType);
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            var objectID = valuesCache.Add(instance);
            var typeName = typeNamesCache.GetTypeName(referencedAsType);

            using (ownerDocument.CreateAppObjectElement(typeName, objectID, parentNode))
            {
            }
        }
    }
}