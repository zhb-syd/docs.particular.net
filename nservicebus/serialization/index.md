---
title: Serialization
summary: How instances of .NET classes are serialized onto the transport.
component: Core
reviewed: 2016-08-23
related:
 - samples/pipeline/multi-serializer
 - samples/serializers
---

NServiceBus takes instances of .NET objects (messages, events and commands) and then sends/receives them over a specified [Transport](/nservicebus/transports/). As part of this the object need to be serialized and deserialized. To achieve this NServiceBus uses **Serializers**.


### Supported Serializers

 * [XmlSerializer](xml.md)
 * [JsonSerializer/BsonSerializer](json.md)
 * [NewtonsoftSerializer](newtonsoft.md)
 * [BinarySerializer](binary.md) (deprecated in Versions 6 and above)


### Using an existing serializer

The default serializer used in NServiceBus projects is the custom [XmlSerializer](xml.md).

The pages dedicated to particular Serializers show how to configure the endpoint to use each of them. Unless explicitly configured otherwise, NServiceBus will use XmlSerializer for serializing and deserializing all messages.

NOTE: The same Serializer must be used by the sending endpoint to serialize messages and by receiving endpoint to deserialize them, unless additional deserializers are specified.

In order to register community run serializer or custom serializer, refer to the [Custom serializers - Register the serializer](/nservicebus/serialization/custom-serializer.md#register-the-serializer) section.


partial:additionaldeserializers


### Community run serializers

There are several community run Serializers that can be seen on the list of [NServiceBus Extensions](/platform/extensions.md#serializers).
