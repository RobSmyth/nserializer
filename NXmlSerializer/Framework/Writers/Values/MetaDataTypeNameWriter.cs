using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NSerializer.Framework.Types;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.Framework.Writers.Values
{
    public class MetaDataTypeNameWriter : IObjectWriter
    {
        private readonly IDocumentWriter ownerDocument;

        public MetaDataTypeNameWriter(IDocumentWriter ownerDocument)
        {
            this.ownerDocument = ownerDocument;
        }

        public bool CanWrite(object instance, Type referencedAsType)
        {
            return instance.GetType() == typeof (MetaDataTypeName);
        }

        public void Write(object instance, INodeWriter parentNode, Type referencedAsType)
        {
            var typeName = (MetaDataTypeName)instance;

            using (var node = ownerDocument.CreateElement("typename", parentNode))
            {
                node.AddAttribute("typeid", typeName.Id);
                node.AddAttribute("name", typeName.Name);
            }
        }
    }
}