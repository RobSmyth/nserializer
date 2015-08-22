
# Migration #
Product feature changes and code refactoring often change the data saved. New data may be added, the data structure may change, or types may change name. NSerializer is being extended to provide data migration to allow a product to maintain compatibility with files saved by prior version of the product.

Data migration is to allow an application to read a prior version's file and convert it to the current application objects. This is a work in progress, not all migration scenarios are supported.

## Supported Migration ##

Currently NSerializer supports migration with the following changes to application data types:

  * Namespace
  * Type name / type substitution
  * New fields
  * Field name
  * Deleted fields
  * Field type changes (conversion)

## File version management ##

### Not supported versions ###

Each file saved using NSerializer includes the application's version. At some point very old versions are not supported. The cut-off version, if any, is given in the rules. The serializer throw a FileVersionNotSupportedException exception if the file version is prior to this version.

#### Example ####

Migration rules:

```
rules.From(new Version(1,5))
  .NoMigrationRequired()
  .AllPriorVersions().NotSupported();
```

## Type migration ##

### Aliases ###

Assigning an alias to a type means that a type's name and/or namespace can be changed without breaking file compatibility. This is because the serializer only serialized the alias and not the type's name.

Example:

```
rules.ForType<Foo>()
  .UseAlias("FooAlias");
```

If Foo is renamed then refactoring will rename the type in the rule. It is best to make the alias name different from the original type name so that refactoring tools, like Resharper, will not change the alias.

So after refactoring the rule will be:

```
rules.ForType<RenamedFoo>()
  .UseAlias("FooAlias");
```

### Name and/or namespace changed ###

Simple refactoring or type name changes should not break compatibility so consider using an alias first. But if an alias was not used it is necessary to link the type to the old type name.

#### Example ####

```
namespace Example
{
  class Foo
  {
    private int myField;
      :
  }
  }
```

Current version code:

```
namespace NSerializer.Example
{
  class Bar
  {
    private int myField;
      :
  }
}
```

Migration rules:

```
rules.ForType<NewFoo>()
  .MatchesTypeName("NSerializer.Example.Bar");
```

## Fields migration ##

### New fields ###

Currently supported. No configuration required. New fields with have null or default values.

#### Example ####

Prior (saved) version code:

```
class Foo
{
  private int myField;
    :
}
```

Current version code:

```
class Foo
{
  private int myField;
  private int myNewField;
    :
}
```

### Name changed ###

Supported.

#### Example ####

Prior (saved) version code:

```
class Foo
{
  private int myField;
    :
}
```

Current version code:

```
class Foo
{
  private int myRenamedField;
    :
}
```

Migration rules:

```
rules.ForType<Foo>()
  .Field("myField").RenamedTo("myRenamedField");
```

### Deleted or ignored ###

Supported.

#### Example ####

Prior (saved) version code:

```
class Foo
{
  private int myField;
    :
}
```

Current version code:

```
class Foo
{
}
```

Migration rules:

```
rules.ForType<Foo>()
  .Field("myField").Ignore();
```

### Type conversion ###

Sometimes a type may be changed to a more compact type (e.g. change from int to byte) to reduce file size. To support this and more complex conversions a custom type converter can be set for any field. A converter is a class that implements the IMigrationConverter interface.

#### Example ####

Prior (saved) version code:

```
class Foo
{
  private int myField;
    :
}
```

Current version code:

```
class Foo
{
  private byte myField;
    :
}
```

Migration rules:

```
rules.ForType<Foo>()
  .Field("myField").ConvertUsing(new ToByteConverter());


class ToByteConverter : IMigrationConverter
{
  public object Convert(object value)
  {
    return System.Convert.ToByte(value);
  }
}
```

# Links #
  * [Data conversion](http://en.wikipedia.org/wiki/Data_conversion)
  * [Data transformation](http://en.wikipedia.org/wiki/Data_transformation)