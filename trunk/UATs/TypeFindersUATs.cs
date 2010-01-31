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
using NSerializer.Exceptions;
using NSerializer.Framework.Types;
using NUnit.Framework;


namespace NSerializer.UATs
{
    [TestFixture]
    public class TypeFindersUATs
    {
        private TypeFinder typeFinder;

        [SetUp]
        public void SetUp()
        {
            var seedAssembly = GetType().Assembly;

            var typeFinderConduit = new TypeFinderConduit();

            var typesCache = new CachedTypesFinder(typeFinderConduit);
            ITypeFinder genericTypeFindeer = new GenericTypeFinder(typesCache, typeFinderConduit);
            ITypeFinder typeInAssemblyFinder = new TypeInAssemblyFinder(seedAssembly, typesCache, typeFinder);
            ITypeFinder typeInReferencedAssembliesFinder = new TypeInReferencedAssemblyFinder(seedAssembly, typesCache, typeFinder);

            typeFinder = new TypeFinder(typesCache, genericTypeFindeer, typeInAssemblyFinder,
                                        typeInReferencedAssembliesFinder);
            typeFinderConduit.SetTarget(typeFinder);
        }

        [Test]
        public void Get_ThrowsException_IfTypeNotFound()
        {
            Assert.Throws<UnableToReadXMLTextException>(() => typeFinder.GetType("NSerializer.UATs.BasicUsage"), "Unable to find type 'NSerializer.UATs.BasicUsage'.");
        }

        [Test]
        public void BasicUsage()
        {
            Assert.IsNotNull(typeFinder.GetType("NSerializer.UATs.TypeFindersUATs"));
        }

        [Test]
        public void FindsTypeInReferencedAssembly100000TimesInLessThanOneSecond()
        {
            var startTime = DateTime.Now;

            for (var count = 0; count < 100000; count++)
            {
                Assert.IsNotNull(typeFinder.GetType("System.Int32"));
            }

            var elapsedTime = DateTime.Now - startTime;
            Assert.IsTrue(elapsedTime < TimeSpan.FromSeconds(1));
        }
    }
}