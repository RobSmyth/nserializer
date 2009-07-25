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
using NSerializer.Framework.Readers;


namespace NSerializer.XML.Readers.Values
{
    internal class DoubleReader : IObjectReader
    {
        public bool CanRead(INXmlElementReader nodeReader)
        {
            return nodeReader.Name == "double";
        }

        public object Get(INXmlElementReader nodeReader)
        {
            object value;

            var data = nodeReader.GetPayload().Trim();
            switch (data)
            {
                case "max":
                    {
                        value = double.MaxValue;
                        break;
                    }
                case "min":
                    {
                        value = double.MinValue;
                        break;
                    }
                case "posinfinity":
                    {
                        value = double.PositiveInfinity;
                        break;
                    }
                case "neginfinity":
                    {
                        value = double.NegativeInfinity;
                        break;
                    }
                case "nan":
                    {
                        value = double.NaN;
                        break;
                    }
                default:
                    {
                        try
                        {
                            value = double.Parse(data);
                        }
                        catch (Exception)
                        {
                            throw new NXmlReaderFormatException(
                                string.Format("Unable to read double value '{0}'.", data));
                        }
                        break;
                    }
            }

            return value;
        }
    }
}