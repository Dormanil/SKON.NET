//-----------------------------------------------------------------------
// <copyright file="SKONObject.cs" company="SpaceKrakens">
//     MIT Licence
//     Copyright (C) 2016 SpaceKrakens
// </copyright>
//-----------------------------------------------------------------------

namespace SKON
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// All value types a SKONObject could be, including complex values.
    /// </summary>
    public enum ValueType
    {
        /// <summary>
        /// An empty value.
        /// </summary>
        EMPTY,

        /// <summary>
        /// A string value.
        /// </summary>
        STRING,

        /// <summary>
        /// An integer value.
        /// </summary>
        INTEGER,

        /// <summary>
        /// A double value.
        /// </summary>
        DOUBLE,

        /// <summary>
        /// A boolean value.
        /// </summary>
        BOOLEAN,

        /// <summary>
        /// A DateTime value.
        /// </summary>
        DATETIME,

        /// <summary>
        /// A (sub-) map value.
        /// </summary>
        MAP,

        /// <summary>
        /// An array value.
        /// </summary>
        ARRAY
    }

    /// <summary>
    /// A SKONObject. Every value in a SKON file is handled internally as a SKONObject.
    /// </summary>
    public class SKONObject
    {
        /// <summary>
        /// Backing string value.
        /// </summary>
        private readonly string stringValue;

        /// <summary>
        /// Backing integer value.
        /// </summary>
        private readonly int intValue;

        /// <summary>
        /// Backing float value.
        /// </summary>
        private readonly double doubleValue;

        /// <summary>
        /// Backing boolean value.
        /// </summary>
        private readonly bool booleanValue;

        /// <summary>
        /// Backing DateTime value.
        /// </summary>
        private readonly DateTime dateTimeValue;

        /// <summary>
        /// Backing string-SKONObject dictionary of Map key-value pairs.
        /// </summary>
        private readonly Dictionary<string, SKONObject> mapValues;

        /// <summary>
        /// Backing array of Array values.
        /// </summary>
        private readonly List<SKONObject> arrayValues;

        /// <summary>
        /// Initialises a new instance of the <see cref="SKONObject"/> class.
        /// Constructs an empty SKONObject.
        /// </summary>
        public SKONObject()
        {
            this.Type = ValueType.EMPTY;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SKONObject"/> class.
        /// Constructs a SKONObject holding a string value.
        /// </summary>
        /// <param name="stringValue">The string value.</param>
        public SKONObject(string stringValue)
        {
            this.stringValue = stringValue;
            this.Type = ValueType.STRING;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SKONObject"/> class.
        /// Constructs a SKONObject holding an integer value.
        /// </summary>
        /// <param name="intValue">The integer value.</param>
        public SKONObject(int intValue)
        {
            this.intValue = intValue;
            this.Type = ValueType.INTEGER;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SKONObject"/> class.
        /// Constructs a SKONObject holding a double value.
        /// </summary>
        /// <param name="doubleValue">The double value.</param>
        public SKONObject(double doubleValue)
        {
            this.doubleValue = doubleValue;
            this.Type = ValueType.DOUBLE;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SKONObject"/> class.
        /// Constructs a SKONObject holding a boolean value.
        /// </summary>
        /// <param name="booleanValue">The boolean value.</param>
        public SKONObject(bool booleanValue)
        {
            this.booleanValue = booleanValue;
            this.Type = ValueType.BOOLEAN;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SKONObject"/> class.
        /// Constructs a SKONObject holding a DateTime value.
        /// </summary>
        /// <param name="dateTimeValue">The DateTime value.</param>
        public SKONObject(DateTime dateTimeValue)
        {
            this.dateTimeValue = dateTimeValue;
            this.Type = ValueType.DATETIME;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SKONObject"/> class.
        /// Constructs a SKONObject holding a Map.
        /// </summary>
        /// <param name="mapValues">The key-value pairs inside that Map.</param>
        public SKONObject(Dictionary<string, SKONObject> mapValues)
        {
            this.mapValues = mapValues;
            this.Type = ValueType.MAP;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SKONObject"/> class.
        /// Constructs a SKONObject holding an Array.
        /// </summary>
        /// <param name="arrayValues">The SKONObject values making up that Array.</param>
        public SKONObject(IEnumerable<SKONObject> arrayValues)
        {
            this.arrayValues = new List<SKONObject>(arrayValues);
            this.Type = ValueType.ARRAY;
        }

        /// <summary>
        /// Gets a value indicating whether this SKONObject is empty or not.
        /// </summary>
        public bool IsEmpty => this.Type == ValueType.EMPTY;

        /// <summary>
        /// Gets the collection of string keys, if this SKONObject is a Map, or an empty ICollection, if it isn't.
        /// </summary>
        public ICollection<string> Keys => (ICollection<string>)this.mapValues?.Keys ?? new List<string>();

        /// <summary>
        /// Gets the length of the array, should this SKONObject be one, or negative 1, if it isn't.
        /// </summary>
        public int Length => this.arrayValues?.Count ?? -1;

        /// <summary>
        /// Gets the type of this SKONObject.
        /// </summary>
        public ValueType Type { get; internal set; }

        /// <summary>
        /// Gets the string value of this SKONObject, should it be a string.
        /// </summary>
        public string String => this.Type == ValueType.STRING ? this.stringValue : null;

        /// <summary>
        /// Gets the integer value of this SKONObject, should it be an integer.
        /// </summary>
        public int? Int
        {
            get
            {
                if (this.Type == ValueType.INTEGER)
                {
                    return this.intValue;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the double value of this SKONObject, should it be a double.
        /// </summary>
        public double? Double
        {
            get
            {
                if (this.Type == ValueType.DOUBLE)
                {
                    return this.doubleValue;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the boolean value of this SKONObject, should it be a boolean.
        /// </summary>
        public bool? Boolean
        {
            get
            {
                if (this.Type == ValueType.BOOLEAN)
                {
                    return this.booleanValue;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the DateTime value of this SKONObject, should it be a DateTime.
        /// </summary>
        public DateTime? DateTime
        {
            get
            {
                if (this.Type == ValueType.DATETIME)
                {
                    return this.dateTimeValue;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the values of the SKONObject array.
        /// </summary>
        public ICollection<SKONObject> Values => this.arrayValues ?? new List<SKONObject>();

        /// <summary>
        /// Gets an empty SKONObject.
        /// </summary>
        internal static SKONObject Empty => new SKONObject();

        /// <summary>
        /// Gets or sets the SKONObject value at the integer index i for a SKONObject Array.
        /// </summary>
        /// <param name="i">The index to get a SKONObject for.</param>
        /// <returns>The SKONObject found at the given index or an empty SKONObject, should the index be out of bounds.</returns>
        public SKONObject this[int i]
        {
            get
            {
                if (i >= 0 && i < this.arrayValues?.Count)
                {
                    return this.arrayValues[i];
                }

                return Empty;
            }

            set
            {
                if (i < 0 || i >= this.arrayValues?.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(i), i, "The given index was out of bounds!");
                }

                if (this.Type == ValueType.ARRAY)
                {
                    this.arrayValues[i] = value;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("This SKONObject is not an array! Index {0} cannot be set to value {1}!", i, value));
                }
            }
        }

        /// <summary>
        /// Gets the SKONObject value paired with the string key for a SKONObject Map.
        /// </summary>
        /// <param name="key">The paired string key to get a SKONObject for.</param>
        /// <returns>The SKONObject paired with the given key or an empty SKONObject, should the key be not in the list of keys.</returns>
        public SKONObject this[string key]
        {
            get
            {
                if (this.mapValues?.ContainsKey(key) ?? false)
                {
                    return this.mapValues[key];
                }

                return Empty;
            }

            set
            {
                if (this.Type == ValueType.MAP)
                {
                    this.mapValues[key] = value;
                }
                else
                {
                    throw new InvalidOperationException(string.Format("This SKONObject is not a map! Adding value {0} to key {1} isn't possible!", value, key));
                }
            }
        }

        /// <summary>
        /// Implicitly converts a string into a SKONObject.
        /// </summary>
        /// <param name="str">
        /// The string.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(string str)
        {
            return new SKONObject(str);
        }

        /// <summary>
        /// Implicitly converts an integer into a SKONObject.
        /// </summary>
        /// <param name="i">
        /// The integer.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(int i)
        {
            return new SKONObject(i);
        }

        /// <summary>
        /// Implicitly converts a double into a SKONObject.
        /// </summary>
        /// <param name="d">
        /// The double.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(double d)
        {
            return new SKONObject(d);
        }

        /// <summary>
        /// Implicitly converts a boolean into a SKONObject.
        /// </summary>
        /// <param name="b">
        /// The boolean.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(bool b)
        {
            return new SKONObject(b);
        }

        /// <summary>
        /// Implicitly converts a DateTime into a SKONObject.
        /// </summary>
        /// <param name="dt">
        /// The DateTime.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(DateTime dt)
        {
            return new SKONObject(dt);
        }

        /// <summary>
        /// Implicitly converts a string array into a SKONObject Array of strings.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(string[] array)
        {
            return new SKONObject(new List<string>(array).ConvertAll((value) => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts a string list into a SKONObject Array of strings.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(List<string> list)
        {
            return new SKONObject(list.ConvertAll(value => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts an integer array into a SKONObject Array of integers.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(int[] array)
        {
            return new SKONObject(new List<int>(array).ConvertAll((value) => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts an integer list into a SKONObject Array of integers.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(List<int> list)
        {
            return new SKONObject(list.ConvertAll(value => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts a double array into a SKONObject Array of doubles.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(double[] array)
        {
            return new SKONObject(new List<double>(array).ConvertAll((value) => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts a double list into a SKONObject Array of doubles.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(List<double> list)
        {
            return new SKONObject(list.ConvertAll(value => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts a boolean array into a SKONObject Array of booleans.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(bool[] array)
        {
            return new SKONObject(new List<bool>(array).ConvertAll((value) => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts a boolean list into a SKONObject Array of booleans.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(List<bool> list)
        {
            return new SKONObject(list.ConvertAll(value => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts a DateTime array into a SKONObject Array of DateTimes.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(DateTime[] array)
        {
            return new SKONObject(new List<DateTime>(array).ConvertAll((value) => (SKONObject)value));
        }

        /// <summary>
        /// Implicitly converts a DateTime list into a SKONObject Array of DateTimes.
        /// </summary>
        /// <param name="list">
        /// The list.
        /// </param>
        /// <returns>
        /// The newly converted SKONObject.
        /// </returns>
        public static implicit operator SKONObject(List<DateTime> list)
        {
            return new SKONObject(list.ConvertAll(value => (SKONObject)value));
        }

        /// <summary>
        /// Adds a SKONObject to the Array.
        /// </summary>
        /// <param name="value">
        /// The value to add.
        /// </param>
        /// <returns>
        /// True if it succeeds, false if there is no array.
        /// </returns>
        public bool Add(SKONObject value)
        {
            if (this.arrayValues == null)
            {
                return false;
            }

            this.arrayValues.Add(value);
            return true;
        }

        /// <summary>
        /// Checks to see if this SKONObject contains the key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// True, if it exists, false if not or not a map.
        /// </returns>
        public bool ContainsKey(string key)
        {
            return this.mapValues?.ContainsKey(key) ?? false;
        }

        /// <summary>
        /// Checks to see if this SKONObject contains all keys in the given array.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <returns>
        /// True, if all exist, false if any are missing or not a map.
        /// </returns>
        public bool AllPresent(params string[] keys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                if (!this.ContainsKey(keys[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Checks to see if this SKONObject contains all keys in the given List of strings.
        /// </summary>
        /// <param name="keys">
        /// The keys.
        /// </param>
        /// <returns>
        /// True, if all exist, false if any are missing or not a map.
        /// </returns>
        public bool AllPresent(List<string> keys)
        {
            for (int i = 0; i < keys.Count; i++)
            {
                if (!this.ContainsKey(keys[i]))
                {
                    return false;
                }
            }

            return true;
        }
        
        /// <summary>
        /// Gets the value of the SKONObject Map paired with the given key as the desired data type.
        /// </summary>
        /// <typeparam name="T">The expected data type for the desired value.</typeparam>
        /// <param name="key">The key to search the respective value for.</param>
        /// <param name="defaultValue">A default value, should the key not exist or the SKONObject value not be able to be converted into the proper type.</param>
        /// <returns>Either the proper value or the default value.</returns>
        public T Get<T>(string key, T defaultValue)
        {
            if (!(this.mapValues?.ContainsKey(key) ?? false))
            {
                return defaultValue;
            }

            Type sourceType = typeof(T);
            MethodInfo[] ops = sourceType.GetMethods();
                
            for (int i = 0; i < ops.Length; i++)
            {
                MethodInfo op = ops[i];
                if (op.ReturnType == typeof(T) && (op.Name == "op_Implicit" || op.Name == "op_Explicit"))
                {
                    return (T)op.Invoke(null, new[] { this.mapValues[key] });
                }
            }

            return defaultValue;
        }

        /// <summary>
        /// Tries to convert the value paired with the given key into the desired data type.
        /// </summary>
        /// <typeparam name="T">The expected data type for the desired value.</typeparam>
        /// <param name="key">The key to search the respective value for.</param>
        /// <param name="result">The variable to set to the proper value. Gets set to the default value, should the operation fail.</param>
        /// <returns>True if it succeeds, otherwise false.</returns>        
        public bool TryGet<T>(string key, out T result)
        {
            if (this.mapValues?.ContainsKey(key) ?? false)
            {
                Type sourceType = typeof(T);
                MethodInfo[] ops = sourceType.GetMethods();

                for (int i = 0; i < ops.Length; i++)
                {
                    MethodInfo op = ops[i];
                    if (op.ReturnType != typeof(T) || (op.Name != "op_Implicit" && op.Name != "op_Explicit"))
                    {
                        continue;
                    }

                    result = (T)op.Invoke(null, new[] { this.mapValues[key] });
                    return true;
                }
            }

            result = default(T);
            return false;
        }

        /// <summary>
        /// Returns the value of this SKONObject as a string.
        /// </summary>
        /// <returns>A string description of this SKONObject.</returns>
        public override string ToString()
        {
            switch (this.Type)
            {
                case ValueType.EMPTY:
                    return "Empty";
                case ValueType.STRING:
                    return this.stringValue;
                case ValueType.INTEGER:
                    return this.intValue.ToString();
                case ValueType.DOUBLE:
                    return this.doubleValue.ToString();
                case ValueType.BOOLEAN:
                    return this.booleanValue.ToString();
                case ValueType.DATETIME:
                    return this.dateTimeValue.ToString();
                case ValueType.MAP:
                    return this.mapValues.ToString();
                case ValueType.ARRAY:
                    return this.arrayValues.ToString();
                default:
                    return "Invalid type!";
            }
        }
    }
}
