using System.Collections.Generic;
using Data.BaseClasses.Scriptable;
using UnityEditor;
using UnityEngine;

namespace Misc.CustomEditors
{
    [CustomEditor(typeof(ScriptableEntity),true)]
    public class EntityEditor : Editor
    {
        public static Dictionary<Hex, ScriptableEntity> FormIDMap = new Dictionary<Hex, ScriptableEntity>();

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ScriptableEntity scriptableEntity = (ScriptableEntity)target;

            Hex currentFormID = scriptableEntity.formID;

            EditorGUI.BeginChangeCheck();
            scriptableEntity.formID = (Hex)EditorGUILayout.LongField("FormID", (long)currentFormID);

            if (EditorGUI.EndChangeCheck())
            {
                // Request a repaint of the Inspector window
                EditorUtility.SetDirty(target);
                Repaint();
            }

            if (GUILayout.Button("Generate Unique ID") && scriptableEntity.formID == Hex.EmptyHex)
            {
                scriptableEntity.formID = EntityIDGenerator.GenerateUniqueID();
                // Request a repaint of the Inspector window
                EditorUtility.SetDirty(target);
                Repaint();
            }
        }
        
    }
    public static class EntityIDGenerator
    {
        private static Hex _currentID = Hex.MinValue;

        public static Hex GenerateUniqueID()
        {
            while (EntityEditor.FormIDMap.ContainsKey(_currentID))
            {
                _currentID = _currentID + Hex.Create(1); // Increment the current ID until it's unique
            }
        
            return _currentID;
        }

        public static void Reset()
        {
            _currentID = Hex.MinValue; // Reset the current ID to Hex.MinValue
        }
    }
}