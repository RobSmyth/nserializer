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
using System.Text;
using NSerializer.Framework;
using NSerializer.Migration;
using NSerializer.XML.Document;
using NSerializer.XML.Document.Writers;
using NUnit.Framework;


namespace NSerializer.UATs.Contexts
{
    public abstract class SerializeContext
    {
        protected Assembly typeSeedAssembly;

        [SetUp]
        public virtual void SetUp()
        {
            typeSeedAssembly = GetType().Assembly;
        }

        protected T ReadXmlText<T>(string xmlText)
        {
            return ReadXmlText<T>(xmlText, null, null, null);
        }

        protected T ReadXmlText<T>(string xmlText, IApplicationObjectsRepository appObjectRepository,
                                   IDocumentObjectsRepository docObjectRepository)
        {
            return ReadXmlText<T>(xmlText, appObjectRepository, docObjectRepository, null);
        }

        protected T ReadXmlText<T>(string xmlText, IApplicationObjectsRepository appObjectRepository,
                                   IDocumentObjectsRepository docObjectRepository,
                                   IMigrationRulesBuilder migrationRulesBuilder)
        {
            var stringReader = new StringReader(xmlText);
            var reader = new NXmlReader(typeSeedAssembly, appObjectRepository, docObjectRepository,
                                        migrationRulesBuilder);
            return reader.Read<T>(GetStream(xmlText));
        }

        protected static string SerializeAsXml(object value)
        {
            return SerializeAsXml(value, null);
        }

        protected static string SerializeAsXml(object value, IApplicationObjectsRepository repository)
        {
            return SerializeAsXml(value, repository, null);
        }

        protected static string SerializeAsXml(object value, IApplicationObjectsRepository repository,
                                               IMigrationRulesBuilder migrationRulesBuilder)
        {
            var xmlResult = new StringBuilder();
            IDocumentWriter document = new NXmlDocumentWriter();
            var xmlWriter = new NXmlWriter(document, repository, migrationRulesBuilder);
            xmlWriter.Write(value, new StringWriter(xmlResult));
            return xmlResult.ToString();
        }

        protected static void AssertArraysAreEqual<T>(T[] expected, T[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (var index = 0; index < expected.Length; index++)
            {
                Assert.AreEqual(expected[index], actual[index]);
            }
        }

        protected MemoryStream GetStream(string xmlText)
        {
            return new MemoryStream(Encoding.ASCII.GetBytes(xmlText));
        }
    }
}