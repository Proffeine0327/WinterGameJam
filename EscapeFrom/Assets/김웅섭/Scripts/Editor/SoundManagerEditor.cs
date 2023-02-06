using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(SoundManager))]
public class SoundManagerEditor : Editor
{
    SerializedProperty _keys;
    SerializedProperty _values;

    private void OnEnable()
    {
        _keys = serializedObject.FindProperty("keys");
        _values = serializedObject.FindProperty("values");
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("Clips", GUI.skin.window);
        {
            for (int i = 0; i < _keys.arraySize; i++)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        EditorGUILayout.PrefixLabel($"Element {i}:");

                        EditorGUI.BeginChangeCheck();
                        var input = EditorGUILayout.DelayedTextField(_keys.GetArrayElementAtIndex(i).stringValue);
                        if (EditorGUI.EndChangeCheck())
                        {
                            if (!(target as SoundManager).clipDictionary.ContainsKey(input))
                            {
                                _keys.GetArrayElementAtIndex(i).stringValue = input;
                                serializedObject.ApplyModifiedProperties();
                            }
                        }

                        if(GUILayout.Button("-", GUILayout.Width(20)))
                        {
                            _keys.DeleteArrayElementAtIndex(i);
                        }
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(_values.GetArrayElementAtIndex(i).FindPropertyRelative("clips"));
                    serializedObject.ApplyModifiedProperties();
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.Space();

                var buttonStyle = new GUIStyle();
                buttonStyle.fixedWidth = 20;
                buttonStyle.fixedHeight = 20;
                buttonStyle.normal.background = Texture2D.blackTexture;

                if (GUILayout.Button(EditorGUIUtility.IconContent("d_Toolbar Plus@2x"), buttonStyle))
                {
                    SoundManagerAddWindow.Open((target as SoundManager).clipDictionary.Keys.ToList(), (key) => {
                        _keys.GetArrayElementAtIndex(_keys.arraySize++).stringValue = key;
                        _values.arraySize++;
                        serializedObject.ApplyModifiedProperties();
                    });
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        GUILayout.EndVertical();

        EditorGUILayout.PropertyField(serializedObject.FindProperty("masterVolume"));
        serializedObject.ApplyModifiedProperties();
    }
}
#endif