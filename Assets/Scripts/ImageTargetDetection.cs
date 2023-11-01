using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTargetDetection : MonoBehaviour
{
    [SerializeField] ARTrackedImageManager trackedImageManager;
    [SerializeField] GameObject objectToPlace;

    private GameObject targetImageObject;

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImageChanged;
    }


    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImageChanged;
    }


    void OnTrackedImageChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach(var trackedImage in eventArgs.added)
        {
            if(trackedImage.referenceImage.name == "Cloro")
            {
                targetImageObject = trackedImage.gameObject;
            }
            
        }
        foreach (var trackedImage in eventArgs.updated)
        {
            //Acciones addicionaless en img act
            
        }
        foreach (var trackedImage in eventArgs.removed)
        {
            //acciones cuando imagen eliminada
        }
    }


}
