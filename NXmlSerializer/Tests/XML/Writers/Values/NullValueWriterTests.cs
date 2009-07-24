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

using NMock2;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.Framework.Writers.Values;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NSerializer.XML.Writers.Members;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class NullValueWriterTests : NXmlTestFixtureBase
    {
        private IDocumentWriter ownerDocument;
        private IObjectWriter writer;

        protected override void SetUp()
        {
            base.SetUp();

            ownerDocument = NewMock<IDocumentWriter>();

            writer = new NullValueWriter(ownerDocument);
        }

        [Test]
        public void CanWrite_ReturnsFalse_IfAnObject()
        {
            Assert.IsFalse(writer.CanWrite(this, GetType()));
        }

        [Test]
        public void CanWrite_ReturnsTrue_IfValueIsNull()
        {
            Assert.IsTrue(writer.CanWrite(null, new NullType()));
        }

        [Test]
        public void Write_BuiltsXmlNode()
        {
            var parentNode = NewMock<INodeWriter>();
            var enumNode = CreateXmlNode("null");

            Expect.Once.On(ownerDocument).Method("CreateNullValueElement").With(parentNode).Will(Return.Value(enumNode));
            Expect.Once.On(enumNode).Method("Dispose").WithNoArguments();

            writer.Write(null, parentNode, parentNode.GetType());
        }
    }
}