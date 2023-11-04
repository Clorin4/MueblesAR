using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private int numMueble = 0;

    public Sprite mueble1;
    public Sprite mueble2;
    public Sprite mueble3;
    public Image miniatura;

    private void Update()
    {
        switch (numMueble)
        {
            case 1:
                miniatura.sprite = mueble1;
                break;
            case 2:
                miniatura.sprite = mueble2;
                break;
            case 3:
                miniatura.sprite = mueble3;
                break;
            default:
                //ola
                break;
        }
    }

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

            // Desactiva la detecci�n de planos
            planeManager.enabled = false;
            yield return new WaitForSeconds(1.0f);

            // Vuelve a activar la detecci�n de planos.
            planeManager.enabled = true;
            EnablePlaneMeshes(); 

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
