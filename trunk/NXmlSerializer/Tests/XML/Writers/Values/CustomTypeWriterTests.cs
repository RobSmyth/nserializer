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
using NMock2;
using NSerializer.Framework.Document;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NSerializer.XML.Writers.Values;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class CustomTypeWriterTests : NXmlTestFixtureBase
    {
        private IDocumentWriter ownerDocument;
        private CustomTypeXmlWriter<Guid> writer;

        protected override void SetUp()
        {
            base.SetUp();

            ownerDocument = NewMock<IDocumentWriter>();

            writer = new CustomTypeXmlWriter<Guid>(ownerDocument, "myType");
        }

        [Test]
        public void CanWrite_IsFalse_IfAnInteger()
        {
            var value = 1;
            Assert.IsFalse(writer.CanWrite(value, value.GetType()));
        }

        [Test]
        public void CanWrite_IsTrue_IfAGuid()
        {
            var value = Guid.NewGuid();
            Assert.IsTrue(writer.CanWrite(value, value.GetType()));
        }

        [Test]
        public void Write_BuiltsXmlNode_IfAGuid()
        {
            var parentNode = NewMock<INodeWriter>();
            var node = CreateXmlNode("array");

            var value = new Guid("{13412D4A-93DA-2310-A5D7-4A25FE8F1DE8}");

            Expect.Once.On(ownerDocument).Method("CreateElement").With("myType", parentNode).Will(Return.Value(node));
            Expect.Once.On(node).Method("AddAttribute").With("value", "13412D4A-93DA-2310-A5D7-4A25FE8F1DE8".ToLower());
            Expect.Once.On(node).Method("Dispose").WithNoArguments();

            writer.Write(value, parentNode, parentNode.GetType());
        }
    }
}