﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Lex.Db.Serialization
{
  class ListSerializers<T>
  {
    static readonly Action<DataWriter, T> _serializer = Serializers.GetWriter<T>();
    static readonly Func<DataReader, T> _deserializer = Serializers.GetReader<T>();

    internal static void WriteHashSet(DataWriter writer, HashSet<T> value)
    {
      writer.Write(value.Count);

      foreach (var i in value)
        _serializer(writer, i);
    }

    internal static HashSet<T> ReadHashSet(DataReader reader)
    {
      var result = new HashSet<T>();

      var count = reader.ReadInt32();
      for (int i = 0; i < count; ++i)
        result.Add(_deserializer(reader));

      return result;
    }

    internal static void WriteList(DataWriter writer, List<T> value)
    {
      writer.Write(value.Count);

      foreach (var i in value)
        _serializer(writer, i);
    }

    internal static List<T> ReadList(DataReader reader)
    {
      var count = reader.ReadInt32();
      var result = new List<T>(count);

      for (int i = 0; i < count; ++i)
        result.Add(_deserializer(reader));

      return result;
    }

    internal static void WriteArray(DataWriter writer, T[] value)
    {
      writer.Write(value.Length);

      foreach (var i in value)
        _serializer(writer, i);
    }

    internal static T[] ReadArray(DataReader reader)
    {
      var count = reader.ReadInt32();
      var result = new T[count];

      for (int i = 0; i < count; ++i)
        result[i] = _deserializer(reader);

      return result;
    }
  }
}