using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraEditor))]
public class CameraEditorGUI : Editor {

    bool showToggleHolder = false;

    public override void OnInspectorGUI()
    {
        CameraEditor editor = (CameraEditor)target;

        showToggleHolder = EditorGUILayout.Toggle("Turn on script:", showToggleHolder);

        if (GUILayout.Button("Update Toggle"))
        {
            editor.UpdateToggle(showToggleHolder);
        }
    }
}
