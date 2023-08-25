using System;
using UnityEditor;
using UnityEngine;

namespace Misc.CustomEditors
{
    [CustomPropertyDrawer(typeof(Hex))]
    public class HexPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            SerializedProperty intValueProperty = property.FindPropertyRelative("value");

            // Get the current integer value of the Hex
            int intValue = intValueProperty.intValue;

            // Draw the Hex value using a text field
            string hexString = EditorGUI.TextField(position, label, intValue.ToString("X8"));

            // Normalize the input string to uppercase
            hexString = hexString.ToUpper();

            // Remove all non-hexadecimal characters except the "x" character (for hexadecimal notation)
            string hexCharacters = "0123456789ABCDEFX";
            string validHex = "";
            foreach (char c in hexString)
            {
                if (hexCharacters.Contains(c.ToString()))
                {
                    validHex += c;
                }
                else
                {
                    // Replace invalid characters with "F"
                    validHex += "F";
                }
            }
            

            // Ensure the string has at most 8 characters
            if (validHex.Length > 8)
            {
                validHex = validHex.Substring(0, 8);
            }

            // Pad the left side with zeros
            validHex = validHex.PadLeft(8, '0');

            // Attempt to parse the hexadecimal part and update the serialized property
            if (Hex.TryParse(validHex, out Hex newHexValue))
            {
                intValueProperty.intValue = newHexValue.Value;
            }

            EditorGUI.EndProperty();
        }
    }
}