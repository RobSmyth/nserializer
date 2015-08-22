

# Quick Start #

## Serialize ##

```
INXmlDocument xmlDocument = new NXmlDocument();
NXmlWriter xmlWriter = new NXmlWriter(xmlDocument, null);
xmlWriter.Write(value);
 
StringBuilder xmlOutput = new StringBuilder();
StringWriter stringWriter = new StringWriter(xmlOutput);
xmlDocument.Save(stringWriter);
 
string xmlText = xmlResult.ToString();

To serialize text to a file use the following code fragment.

StreamWriter writer = new StreamWriter(fileName);
writer.Write(xmlText);
```

## Deserialize ##

```
private T ReadXmlText<T>(string xmlText)
{
    StringReader stringReader = new StringReader(xmlText);
    NXmlReader reader = new NXmlReader(stringReader, typeSeedAssembly, null, null);
    return reader.Read<T>();
}
```

## Testing Class Serializing ##

The NUnit test fixture is an example of how to test if a class can be serialized using NXmlSerializer. Replace the 'MyClassUnderTest' with the class being tested. This test fixture assumes the class does not contain application or document scope objects.

```
[TestFixture]
public class SampleTestFixture
{
    private Assembly typeSeedAssembly;
 
    [SetUp]
    public void SetUp()
    {
        typeSeedAssembly = typeof (MyClassUnderTest).Assembly;
    }
 
    [Test]
    public void CanSerialize()
    {
        MyClassUnderTest sourceObject = new MyClassUnderTest(3);    // the class you are testing
 
        string xmlText = Serialize(sourceObject);
        MyClassUnderTest readObject = ReadXmlText<MyClassUnderTest>(xmlText);
 
        Assert.AreEqual(3, readObject.A);  // something you can validate on the derserialized object
    }
 
    private static string Serialize(object value)
    {
        INXmlDocument xmlDocument = new NXmlDocument();
        NXmlWriter xmlWriter = new NXmlWriter(xmlDocument, null);
        xmlWriter.Write(value);
 
        StringBuilder xmlResult = new StringBuilder();
        xmlDocument.Save(new StringWriter(xmlResult));
        return xmlResult.ToString();
    }
 
    private T ReadXmlText<T>(string xmlText)
    {
        StringReader stringReader = new StringReader(xmlText);
        NXmlReader reader = new NXmlReader(stringReader, typeSeedAssembly, null, null);
        return reader.Read<T>();
    }
}
 
public class MyClassUnderTest
{
    private int a;
 
    public MyClassUnderTest(int a)
    {
        this.a = a;
    }
 
    public int A
    {
        get { return a; }
    }
}
```


## Types Serialized ##

By default all found types are serialized using field values. If prevent a class, field, or property being serialized it can be marked with the NXmlIgnore or XmlIgnore attribute. Classes, structures, and enums are all serialized. A class's base classes are automatically serialized as part of the classes state.

Types known to be supported:

  * Enums
  * Class types
  * Generic class types
  * Structure types
  * Primitive data types
  * TimeSpan
  * DateTime
  * ArrayList
  * List<>
  * Dictionary<>
  * Guid
  * Arrays (single dimension)
  * Version


Serialization had been optimised, to reduce the size of XML, for the types listed below.

  * System.String
  * System.Int32
  * System.Int64
  * System.UInt32
  * System.UInt64
  * System.Double
  * System.Single
  * System.Boolean
  * System.Char
  * System.Version
  * System.Collections.Generic.List<>
  * System.ComponentModel.BindingList<>
  * System.Collections.ArrayList
  * double[.md](.md)


## Class Constructors ##

When deserializing classes with multiple constructors the deserializer will use the constructor with the fewest parameters. Null values are passed for all parameters.

## Repository Integration ##

NXmlSerializer can use application factories, repositories, or dependency injection frameworks (e.g. NDependencyInjection) for application and/or document context objects. This is implemented by providing an object implementing the IInstanceRepository interface to NXmlSerializer read and write methods.

See Repository Integration for more information.

## Building The Code ##

The Visual Studio project uses the MSBuild Community Tasks 'AssemblyInfo' and 'SvnVersion' targets to generate the AssemblyInfo.cs file to synchronise the build number to the subversion revision. Download it here.

You will also want NUnit to run the tests.

Related links

  * MSBuild Community Tasks
  * NUnit