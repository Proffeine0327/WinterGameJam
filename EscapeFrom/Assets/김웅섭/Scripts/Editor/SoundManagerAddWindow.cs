using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class SoundManagerAddWindow : EditorWindow
{
    public static void Open(List<string> keys, Action<string> action)
    {
        var window = EditorWindow.CreateInstance<SoundManagerAddWindow>();
        window.action = action;
        window.keys = keys;

        window.ShowAsDropDown(new Rect(EditorGUIUtility.GUIToScreenPoint(Event.current.mousePosition), Vector2.zero), new Vector2(200, 20));
    }

    string input;
    private Action<string> action;
    private List<string> keys;

    private void OnGUI() 
    {
        EditorGUILayout.BeginHorizontal();
        input = EditorGUILayout.TextField(input);

        EditorGUI.BeginDisabledGroup(keys.Contains(input));
        if(GUILayout.Button("Add"))
        {
            action.Invoke(input);
            this.Close();
        }  
        EditorGUI.EndDisabledGroup();

        if(Event.current.keyCode == KeyCode.Return && !keys.Contains(input))
        {
            action.Invoke(input);
            this.Close(); 
        }
        EditorGUILayout.EndHorizontal();
    }
}
#endif
