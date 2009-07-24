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
using System.Text;
using NSerializer.XML.Readers;
using NSerializer.XML.Readers.Values;
using NUnit.Framework;


namespace NSerializer.Tests.XML.Readers
{
    [TestFixture]
    public class NodeAttributesReaderTests : NXmlTestFixtureBase
    {
        private NodeAttributesReader reader;

        protected override void SetUp()
        {
            reader = new NodeAttributesReader();
        }

        [Test]
        public void Test()
        {
            var xml = @" type='myType'   thingy='another type' >
   abc def  <123>
".Replace("'", "\"");
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(xml));

            reader.Read(new XmlStreamReader(stream));

            Assert.AreEqual("myType", reader.Get("type"));
            Assert.AreEqual("another type", reader.Get("thingy"));
        }
    }
}