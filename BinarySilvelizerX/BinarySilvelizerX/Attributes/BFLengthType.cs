﻿using System;

namespace BinarySilvelizerX.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BFLengthTypeAttribute : Attribute
    {
        public Type Type { get; }

        /// <summary>
        /// Use this constructor to define which length type of the collection/string must be used.
        /// </summary>
        /// <param name="type">Length type</param>
        public BFLengthTypeAttribute(Type type)
        {
            Type = type;
        }
    }
}