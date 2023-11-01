using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARPlaneDetection : MonoBehaviour
{
    public ARPlaneManager planeManager;


    private void Start()
    {
        if (planeManager = null)
        {
            planeManager = FindAnyObjectByType<ARPlaneManager>();
        }
        if (planeManager != null)
        {
            planeManager.planesChanged += OnPlanesChanged;
        }
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        //Activeit o disactiveit ver planos según la detección
        foreach (var plane in eventArgs.added)
        {
            TogglePlaneVis(plane, true);
        }
        foreach (var plane in eventArgs.updated)
        {
            TogglePlaneVis(plane, true);
        }
        foreach (var plane in eventArgs.removed)
        {
            TogglePlaneVis(plane, true);
        }
    }

    void TogglePlaneVis(ARPlane plane, bool shouldShow)
    {
        plane.gameObject.SetActive(shouldShow);
    }

}
