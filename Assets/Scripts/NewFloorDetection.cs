using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class NewFloorDetection : MonoBehaviour
{
    [SerializeField] ARRaycastManager raycastManager;
    [SerializeField] ARPlaneManager planeManager;
    [SerializeField] GameObject objectToPlace1;
    [SerializeField] GameObject objectToPlace2;
    [SerializeField] GameObject objectToPlace3;

    private bool objectPlaced = false;
    private GameObject placeeObject;
    private int numMueble = 1;

    private void OnEnable()
    {
        planeManager.planesChanged += OnPlanesChanged;
    }

    private void OnDisable()
    {
        planeManager.planesChanged -= OnPlanesChanged;
    }

    public void Es1()
    {
        numMueble = 1;
        RemovePlaceObjectAndResetPlaneDetection();
    }

    public void Es2()
    {
        numMueble = 2;
        RemovePlaceObjectAndResetPlaneDetection();
    }

    public void Es3()
    {
        numMueble = 3;
        RemovePlaceObjectAndResetPlaneDetection();
    }

    void OnPlanesChanged(ARPlanesChangedEventArgs eventArgs)
    {
        if (!objectPlaced)
        {
            // Destruir el mueble anterior si existe
            if (placeeObject != null)
            {
                Destroy(placeeObject);
            }

            foreach (var plane in eventArgs.added)
            {
                GameObject objectToPlace = GetObjectToPlace();
                if (objectToPlace != null)
                {
                    placeeObject = Instantiate(objectToPlace, plane.center, Quaternion.identity);
                    objectPlaced = true;
                    break;
                }
            }
        }
    }

    GameObject GetObjectToPlace()
    {
        GameObject objectToPlace = null;

        if (numMueble == 1)
        {
            objectToPlace = objectToPlace1;
        }
        else if (numMueble == 2)
        {
            objectToPlace = objectToPlace2;
        }
        else if (numMueble == 3)
        {
            objectToPlace = objectToPlace3;
        }

        return objectToPlace;
    }

    public void RemovePlaceObjectAndResetPlaneDetection()
    {
        StartCoroutine(RemoveAndResetCoroutine());
    }

    IEnumerator RemoveAndResetCoroutine()
    {
        if (placeeObject != null)
        {
            Destroy(placeeObject);
            objectPlaced = false;
            DisablePlaneMeshes(); 

            // Desactiva la detección de planos
            planeManager.enabled = false;
            yield return new WaitForSeconds(0.1f);

            // Vuelve a activar la detección de planos.
            planeManager.enabled = true;
            yield return new WaitForSeconds(1.0f);

            EnablePlaneMeshes(); //IMPORTANTE CHECAR ESTO!!!!!!!!

        }
    }

    public void EnablePlaneMeshes()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(true); //Activamo
        }
    }

    public void DisablePlaneMeshes()
    {
        foreach (var plane in planeManager.trackables)
        {
            plane.gameObject.SetActive(false); // Desactivamo la malla
        }
    }
}
