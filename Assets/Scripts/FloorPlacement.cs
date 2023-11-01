using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FloorPlacement : MonoBehaviour
{

    public ARRaycastManager raycastManager;
    public GameObject objectToPlace;


    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            //Raycast al piso mami
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            List<ARRaycastHit> hits = new List<ARRaycastHit>();

            if (raycastManager.Raycast(ray, hits, TrackableType.PlaneWithinPolygon))
            {
                //Obtener planou detectao
                Pose hitPose = hits[0].pose;
                Debug.Log("Plano detectao" + hitPose.position);

                //Colocar el mueble
                Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                Debug.Log("Obj instantiadou en " + hitPose.position);
            }

            Debug.Log("Nada pa");
        }
    }

}