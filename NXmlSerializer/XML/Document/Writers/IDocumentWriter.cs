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
using NSerializer.Framework.Document;
using NSerializer.XML.Document.Writers;


namespace NSerializer.XML.Document
{
    public interface IDocumentWriter : ISerialiserDocument
    {
        INodeWriter RootNode { get; }
        void EndWrite();
        void BeginWrite(TextWriter writer);

        INodeWriter CreateElement(string name, ISerialiserNode parentNode);

        INodeWriter CreateObjectRefernceElement(ISerialiserNode parentNode);
        INodeWriter CreateTypedElement(string name, string typeName, ISerialiserNode parentNode);
        INodeWriter CreateClassElement(string typeName, int objectID, ISerialiserNode parentNode);
        INodeWriter CreateMembersElement(ISerialiserNode parentNode);
        INodeWriter CreateBaseClassElement(string typeName, ISerialiserNode parentNode);
        INodeWriter CreateAppObjectElement(string typeName, int objectID, ISerialiserNode parentNode);
        INodeWriter CreateValueElement(string typeName, ISerialiserNode parentNode);
        INodeWriter CreateListElement(string typeName, int valueID, ISerialiserNode parentNode);
        INodeWriter CreateItemsElement(INodeWriter parentNode);
        INodeWriter CreateListElement(string typeName, ISerialiserNode parentNode);
        INodeWriter CreateNullValueElement(ISerialiserNode parentNode);
        INodeWriter CreateArrayElement(string typeName, int valueID, ISerialiserNode parentNode);
    }
}