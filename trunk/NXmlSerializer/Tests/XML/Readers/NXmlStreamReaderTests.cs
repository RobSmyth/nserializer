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
using NUnit.Framework;


namespace NSerializer.Tests.XML.Readers
{
    [TestFixture]
    public class NXmlStreamReaderTests : NXmlTestFixtureBase
    {
        protected override void SetUp()
        {
        }

        [Test]
        public void ReadWord_SkipsWhitespaces()
        {
            var xml = @"
   abc def  <123>  _

456
".Replace("'", "\"");
            var stream = new MemoryStream(Encoding.ASCII.GetBytes(xml));
            var reader = new XmlStreamReader(stream);

            Assert.AreEqual("abc", reader.ReadWord());
            Assert.AreEqual("def", reader.ReadWord());
            Assert.AreEqual("123", reader.ReadWord());
            Assert.AreEqual("_", reader.ReadWord());
            Assert.AreEqual("456", reader.ReadWord());
            Assert.AreEqual("", reader.ReadWord());
        }
    }
}