using System;
using System.Collections;
using UnityEngine;

namespace Misc
{
    /// <summary>
    /// Represents an immutable hexadecimal value.
    /// </summary>
    [Serializable]
    public struct Hex : IEquatable<Hex>, IComparable<Hex>, IEnumerable
    {
        [SerializeField]
        private int value;
        public static Hex MaxValue { get; private set; }
        public static Hex MinValue { get; private set; }
        /// <summary>
        /// Gets the integer value of the Hex struct.
        /// </summary>
        public int Value => value;

        /// <summary>
        /// Initializes a new instance of the Hex struct with the specified integer value.
        /// </summary>
        /// <param name="value">The integer value to represent as a hexadecimal.</param>
        public Hex(int value)
        {
            this.value = value;
        }
        // Add a static method to create a new Hex instance with the specified value.
        public static Hex Create(int value)
        {
            return new Hex(value);
        }
        /// <summary>
        /// Represents an empty or default Hex value (0x00000000).
        /// </summary>
        public static Hex EmptyHex { get; } = new Hex(MinValue);

        public static Hex FilledHex { get; } = new Hex(MaxValue);
        
        static Hex()
        {
            // Initialize MaxValue and MinValue using a private int field.
            int maxValueInt = unchecked((int)0xFFFFFFFF);
            int minValueInt = 0x00000000;
            MaxValue = new Hex(maxValueInt);
            MinValue = new Hex(minValueInt);
        }
        
        /// <summary>
        /// Implicitly converts an integer to a Hex value.
        /// </summary>
        /// <param name="intValue">The integer value to convert.</param>
        /// <returns>The corresponding Hex value.</returns>
        public static implicit operator Hex(int intValue)
        {
            return new Hex(intValue);
        }

        /// <summary>
        /// Implicitly converts a Hex value to an integer.
        /// </summary>
        /// <param name="hex">The Hex value to convert.</param>
        /// <returns>The integer representation of the Hex value.</returns>
        public static implicit operator int(Hex hex)
        {
            return hex.value;
        }

        /// <summary>
        /// Attempts to parse a hexadecimal string into a Hex value.
        /// </summary>
        /// <param name="hexString">The hexadecimal string to parse.</param>
        /// <param name="hexValue">When this method returns, contains the parsed Hex value
        /// if the conversion succeeded, or default Hex value if the conversion failed.</param>
        /// <returns>True if the conversion was successful; otherwise, false.</returns>
        public static bool TryParse(string hexString, out Hex hexValue)
        {
            hexValue = default;

            if (string.IsNullOrEmpty(hexString))
                return false;

            if (hexString.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
                hexString = hexString.Substring(2);

            if (int.TryParse(hexString, System.Globalization.NumberStyles.HexNumber, null, out int intValue))
            {
                hexValue = new Hex(intValue);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts the Hex value to a hexadecimal string representation.
        /// </summary>
        /// <returns>The hexadecimal string representation of the value.</returns>
        public override string ToString()
        {
            return value.ToString("X8");
        }

        /// <summary>
        /// Adds two Hex values and returns the result as a new Hex.
        /// </summary>
        /// <param name="hex1">The first Hex value.</param>
        /// <param name="hex2">The second Hex value.</param>
        /// <returns>The sum of the two Hex values.</returns>
        public static Hex operator +(Hex hex1, Hex hex2)
        {
            return new Hex(hex1.value + hex2.value);
        }

        /// <summary>
        /// Subtracts hex2 from hex1 and returns the result as a new Hex.
        /// </summary>
        /// <param name="hex1">The Hex value to subtract from.</param>
        /// <param name="hex2">The Hex value to subtract.</param>
        /// <returns>The result of the subtraction as a new Hex value.</returns>
        public static Hex operator -(Hex hex1, Hex hex2)
        {
            return new Hex(hex1.value - hex2.value);
        }

        /// <summary>
        /// Compares two Hex values for equality.
        /// </summary>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        /// <returns>True if the Hex values are equal; otherwise, false.</returns>
        public static bool operator ==(Hex hex1, Hex hex2)
        {
            return hex1.value == hex2.value;
        }

        /// <summary>
        /// Compares two Hex values for inequality.
        /// </summary>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        /// <returns>True if the Hex values are not equal; otherwise, false.</returns>
        public static bool operator !=(Hex hex1, Hex hex2)
        {
            return hex1.value != hex2.value;
        }

        /// <summary>
        /// Compares two Hex values to determine if the first value is greater than the second value.
        /// </summary>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        /// <returns>True if the first Hex value is greater than the second value; otherwise, false.</returns>
        public static bool operator >(Hex hex1, Hex hex2)
        {
            return hex1.value > hex2.value;
        }

        /// <summary>
        /// Compares two Hex values to determine if the first value is less than the second value.
        /// </summary>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        /// <returns>True if the first Hex value is less than the second value; otherwise, false.</returns>
        public static bool operator <(Hex hex1, Hex hex2)
        {
            return hex1.value < hex2.value;
        }

        /// <summary>
        /// Compares two Hex values to determine if the first value is greater than or equal to the second value.
        /// </summary>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        /// <returns>True if the first Hex value is greater than or equal to the second value; otherwise, false.</returns>
        public static bool operator >=(Hex hex1, Hex hex2)
        {
            return hex1.value >= hex2.value;
        }

        /// <summary>
        /// Compares two Hex values to determine if the first value is less than or equal to the second value.
        /// </summary>
        /// <param name="hex1">The first Hex value to compare.</param>
        /// <param name="hex2">The second Hex value to compare.</param>
        /// <returns>True if the first Hex value is less than or equal to the second value; otherwise, false.</returns>
        public static bool operator <=(Hex hex1, Hex hex2)
        {
            return hex1.value <= hex2.value;
        }

        /// <summary>
        /// Gets the number of hexadecimal digits required to represent the value.
        /// </summary>
        /// <returns>The number of hexadecimal digits.</returns>
        public int GetHexDigitCount()
        {
            return (int)Math.Ceiling(Math.Log(value + 1, 16));
        }

        /// <summary>
        /// Gets the value represented as a byte array in little-endian format.
        /// </summary>
        /// <returns>The byte array representing the value.</returns>
        public byte[] GetBytes()
        {
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte)((value >> (i * 8)) & 0xFF);
            }
            return bytes;
        }

        /// <summary>
        /// Returns the minimum of two Hex values.
        /// </summary>
        /// <param name="hex1">The first Hex value.</param>
        /// <param name="hex2">The second Hex value.</param>
        /// <returns>The minimum Hex value.</returns>
        public static Hex Min(Hex hex1, Hex hex2)
        {
            return new Hex(Math.Min(hex1.value, hex2.value));
        }

        /// <summary>
        /// Returns the maximum of two Hex values.
        /// </summary>
        /// <param name="hex1">The first Hex value.</param>
        /// <param name="hex2">The second Hex value.</param>
        /// <returns>The maximum Hex value.</returns>
        public static Hex Max(Hex hex1, Hex hex2)
        {
            return new Hex(Math.Max(hex1.value, hex2.value));
        }

        /// <summary>
        /// Returns the absolute value of a Hex value.
        /// </summary>
        /// <param name="hex">The Hex value.</param>
        /// <returns>The absolute value of the Hex value.</returns>
        public static Hex Abs(Hex hex)
        {
            return new Hex(Math.Abs(hex.value));
        }

        /// <summary>
        /// Compares the current Hex value to another Hex value.
        /// </summary>
        /// <param name="other">The other Hex value to compare.</param>
        /// <returns>
        /// A positive value if the current Hex value is greater than <paramref name="other"/>,
        /// zero if they are equal, and a negative value if the current Hex value is less than <paramref name="other"/>.
        /// </returns>
        public int CompareTo(Hex other)
        {
            return value.CompareTo(other.value);
        }

        /// <summary>
        /// Determines whether the current Hex value is equal to another Hex value.
        /// </summary>
        /// <param name="other">The other Hex value to compare.</param>
        /// <returns>True if the current Hex value is equal to <paramref name="other"/>; otherwise, false.</returns>
        public bool Equals(Hex other)
        {
            return value == other.value;
        }

        /// <summary>
        /// Determines whether the current Hex value is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns>True if the current Hex value is equal to <paramref name="obj"/>; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            return obj is Hex hex && Equals(hex);
        }
        

        /// <summary>
        /// Gets the hash code for the current Hex value.
        /// </summary>
        /// <returns>The hash code for the current Hex value.</returns>
        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
