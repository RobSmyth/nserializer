# Overview #

_This project was moved from [Sourceforge](http://nxmlserializer.wiki.sourceforge.net/) and was known as NXmlSerializer._

NSerializer provides a framework for serializing .Net objects to and from XML without need the need for custom code. It is intended to be able serialize well designed objects, written without considering serialization, without code modification. NSerializer is intended for XML persistence.

You can find documentation, and quick start, [here](QuickStart.md).
## Features ##
  * File [migration](Migration.md).
  * Object state serialization by serializing object private fields.
  * Serializes interface type references.
  * Allows polymorphism. Automatically serializes derived class instances.
  * Circular instance references support.
  * Supports multiple instances of an object. Multiple instances are serialized by reference to avoid XML bloat.
  * Large native [data types supported](SupportedDataTypes.md).
  * Deserialize classes with parameterized constructor. Default constuctors are not mandatory.
  * Integrates with both application and document context factories/repository for object creation and/or dependency injection (e.g. [NDependencyInjection](http://code.google.com/p/ndependencyinjection/)).

## Benefits ##
  * File [migration](Migration.md).
  * Enables Test Driven Development (TDD) as code using interfaces can be mocked (see NMock) and generally has better encapsulation.
  * Polymorphism allows better, encapsulated code. For example if you have a collection of animals the collection can have an instance of a cat and an instance of a dog rather than just instances of animals with the type being a property. It frees you to write better code.
  * Supports good design. e.g. serialize state by fields, no need for default consturctors, and support for polymorphism.
  * Minimal code, no custom code, and can be used without class attributes. Automatically reconstructs objects with references to application scope objects by using application's existing factories or repositories (optional).