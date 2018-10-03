using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HideTrackerInEditor))]
public class CustomHideTrackerGUIEditor : Editor {

    bool showToggleHolder = false;

    public override void OnInspectorGUI()
    {
        HideTrackerInEditor hideTracker = (HideTrackerInEditor)target;

        showToggleHolder = EditorGUILayout.Toggle("Show Targets", showToggleHolder);

        if (GUILayout.Button("UpdateObjects"))
        {
            hideTracker.UpdateTargetObjects();
            hideTracker.UpdateInput(showToggleHolder);
        }
    }
}
