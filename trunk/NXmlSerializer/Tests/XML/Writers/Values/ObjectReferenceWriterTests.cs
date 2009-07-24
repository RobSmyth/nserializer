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
using NSerializer.Framework;
using NSerializer.Framework.Document;
using NSerializer.Framework.Writers;
using NSerializer.Framework.Writers.Values;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Writers.Values
{
    [TestFixture]
    public class ObjectReferenceWriterTests : NXmlTestFixtureBase
    {
        private IDocumentWriter document;
        private IValuesCache valuesCache;
        private IObjectWriter writer;

        protected override void SetUp()
        {
            base.SetUp();

            valuesCache = NewMock<IValuesCache>();
            document = NewMock<IDocumentWriter>();

            writer = new ObjectReferenceWriter(document, valuesCache);
        }

        [Test]
        public void CanWrite_ReturnsFalse_IfObjectNotInCache()
        {
            Expect.Once.On(valuesCache).Method("Contains").Will(Return.Value(false));
            Assert.IsFalse(writer.CanWrite(this, GetType()));
        }

        [Test]
        public void CanWrite_ReturnsTrue_IfObjectFoundInCache()
        {
            Expect.Once.On(valuesCache).Method("Contains").Will(Return.Value(true));
            Assert.IsTrue(writer.CanWrite(this, GetType()));
        }

        [Test]
        public void Write_BuiltsXmlNode()
        {
            var parentNode = NewMock<INodeWriter>();
            var referenceNode = CreateXmlNode("objref");

            Expect.Once.On(document).Method("CreateObjectRefernceElement").With(parentNode).Will(
                Return.Value(referenceNode));
            Expect.Once.On(valuesCache).Method("GetID").With(this).Will(Return.Value(7));
            Expect.Once.On(referenceNode).Method("AddAttribute").With("ID", 7);

            Expect.Once.On(referenceNode).Method("Dispose").WithNoArguments();

            writer.Write(this, parentNode, parentNode.GetType());
        }
    }
}