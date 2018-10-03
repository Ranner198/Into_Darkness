using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class HideTrackerInEditor : MonoBehaviour
{

    private bool isPlaying;
    private Renderer renderer;

    public GameObject[] targets;


    public void UpdateInput(bool show)
    {

        UpdateTargetObjects();

        isPlaying = EditorApplication.isPlaying;

        //Game Is Playing 
        if (isPlaying || show)
        {
            HideObjects();
        }

        if (!isPlaying && !show)
        {
            ShowObjects();
        }
    }

    public void UpdateTargetObjects()
    {
        targets = GameObject.FindGameObjectsWithTag("Tracking");
    }

    public void ShowObjects()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            renderer = targets[i].GetComponent<Renderer>();
            if (renderer.enabled)
                renderer.enabled = false;
        }
    }

    public void HideObjects()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            renderer = targets[i].GetComponent<Renderer>();
            if (!renderer.enabled)
                renderer.enabled = true;
        }
    }
}