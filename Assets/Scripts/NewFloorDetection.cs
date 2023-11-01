using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class NewFloorDetection : MonoBehaviour
{

    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] GameObject objectToPlace;

    //private bool floorDetected = false;
    private bool objectPlaced = false;
    private GameObject placeeObject;

    private void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }


    private void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    
    void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        if(!objectPlaced)
        {
            foreach (var plane in eventArgs.added)
            {
                placeeObject = Instantiate(objectToPlace, plane.center, Quaternion.identity);
                objectPlaced = true;
                break;
            }
        }

            
        
    }


    public void RemovePlaceObject()
    {
        if(placeeObject != null)
        {
            Destroy(placeeObject);
            objectPlaced = false;
        }
    }
}
