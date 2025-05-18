using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SimpleImageTarget : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;

    [System.Serializable]
    public class ImagePrefabPair
    {
        public string imageName;          // Nombre exacto en la XRReferenceImageLibrary
        public GameObject prefabToPlace;  // Prefab que se mostrará cuando se detecte la imagen
    }

    [SerializeField] private List<ImagePrefabPair> imagePrefabPairs;

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            HandleImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.updated)
        {
            HandleImage(trackedImage);
        }

        foreach (var trackedImage in eventArgs.removed)
        {
            string imageName = trackedImage.referenceImage.name;
            if (spawnedPrefabs.ContainsKey(imageName))
            {
                Destroy(spawnedPrefabs[imageName]);
                spawnedPrefabs.Remove(imageName);
            }
        }
    }

    void HandleImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;

        foreach (var pair in imagePrefabPairs)
        {
            if (pair.imageName == imageName)
            {
                if (!spawnedPrefabs.ContainsKey(imageName))
                {
                    GameObject spawned = Instantiate(pair.prefabToPlace, trackedImage.transform);
                    spawned.transform.localPosition = Vector3.zero;
                    spawned.transform.localRotation = Quaternion.identity;
                    spawned.transform.localScale = Vector3.one * 0.01f;  // Escala 0.5
                    spawnedPrefabs[imageName] = spawned;
                }
                else
                {
                    spawnedPrefabs[imageName].SetActive(true);
                    spawnedPrefabs[imageName].transform.position = trackedImage.transform.position;
                }
                break;
            }
        }
    }
}
