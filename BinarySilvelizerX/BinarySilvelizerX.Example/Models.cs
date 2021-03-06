﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using BinarySilvelizerX.Attributes;
using BinarySilvelizerX.Core;
using BinarySilvelizerX.Interfaces;
using BinarySilvelizerX.Utils;

// ReSharper disable UnusedMember.Global

namespace BinarySilvelizerX.Example
{
    public class Beer
    {
        public string Brand { get; set; }

        [BFIgnored]
        public byte SortCount { get; set; }

        [BFLength(2)]
        public List<SortContainer> Sort { get; set; }

        public float Alcohol { get; set; }

        public string Brewery { get; set; }

        public ushort Crc { get; set; }

        public byte WeirdNumber { get; set; }
        public byte WeirdNumber23 { get; set; }

        public byte WeirdNumber2 { get; set; }
        public byte WeirdNumber24 { get; set; }

        [BFEncoding(TextUtils.CodePage.Utf32)]
        public string TerminatedString { get; set; }

        public Color Color { get; set; }
    }

    [Serializable]
    public class SortContainer
    {
        public byte NameLength { get; set; }

        [BFLength("NameLength")]
        public string Name { get; set; }
    }

    public class PacketFactory : ISubtypeFactory
    {
        public bool TryGetType(object key, out Type type)
        {
            if (Equals(key, Opcodes.Packet3))
            {
                type = typeof(FactoryDerModel);
                return true;
            }

            if (Equals(key, Opcodes.Packet4))
            {
                type = typeof(FactoryDerModel);
                return true;
            }
            type = null;
            return false;
        }
    }

    public class UniversalPacket : ByteModel<UniversalPacket>
    {
        public Opcodes Opcode { get; set; }

        [BFSubtype(nameof(Opcode), Opcodes.Packet1, typeof(DerivedModel1))]
        [BFSubtype(nameof(Opcode), Opcodes.Packet2, typeof(DerivedModel2))]
        [BFSubtypeFactory(nameof(Opcode), typeof(PacketFactory))]
        [BFSubtypeDefault(typeof(DefaultDerModel))]
        public RootModel Data { get; set; }
    }

    [SerializationMode(SerializationAccessorMode.OnlyBoth)] //Just as example
    public class DerivedModel1 : RootModel
    {
        public string HelloWorld { get; set; }
    }

    public class DerivedModel2 : RootModel
    {
        public byte SampleByte { get; set; }
    }

    public class DefaultDerModel : RootModel
    {
        public byte AnotherByte { get; set; }
    }

    public class FactoryDerModel : RootModel
    {
        public int SampleInt { get; set; }
    }

    public class RootModel
    {
        public int BaseInt { get; set; }
    }

    public enum Opcodes
    {
        Packet1,
        Packet2,
        Packet3,
        Packet4,
        Packet5
    }

    public class RecursiveModel : ByteModel<RecursiveModel>
    {
        public int Int { get; set; }
        public IPModel IP { get; set; }
    }

    public class IPModel : ByteModel<IPModel>
    {
        public IPAddress Address { get; set; }
    }
}