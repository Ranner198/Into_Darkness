using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class HideTrackerInEditor : MonoBehaviour
{

    private bool isPlaying, holder, show_Objects;
    private Renderer renderer;

    public GameObject[] targets;

    void Awake()
    {
        isPlaying = EditorApplication.isPlaying;

        if (isPlaying)
        {
            if (show_Objects)
                holder = true;
            UpdateInput(true);
        }

        if (!isPlaying) {
            if (show_Objects != holder)
                show_Objects = !show_Objects;
        }
    }

    public void UpdateInput(bool show)
    {
        show_Objects = show;
        UpdateTargetObjects();

        //Game Is Playing 
        if (!show)
        {
            ShowObjects();
        }

        if (show)
        {
            HideObjects();
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