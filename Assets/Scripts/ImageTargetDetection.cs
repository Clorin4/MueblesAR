using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SimpleImageTarget : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private TMPro.TextMeshProUGUI infoTextUI;
    [SerializeField] private GameObject PanelText;

    [System.Serializable]
    public class ImagePrefabPair
    {
        public string imageName;
        public GameObject prefabToPlace;
        public Vector3 finalScale = Vector3.one * 0.5f;
        public Vector3 finalRotationEuler = Vector3.zero;

        [TextArea]
        public string infoText; // Texto informativo a mostrar
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
                if (trackedImage.trackingState == TrackingState.Tracking)
                {
                    if (!spawnedPrefabs.ContainsKey(imageName))
                    {
                        GameObject spawned = Instantiate(pair.prefabToPlace, trackedImage.transform);
                        spawned.transform.localPosition = new Vector3(0f, 0.05f, 0f);
                        spawned.transform.localRotation = Quaternion.Euler(pair.finalRotationEuler);
                        spawned.transform.localScale = Vector3.zero;

                        spawnedPrefabs[imageName] = spawned;
                        StartCoroutine(ScaleUp(spawned, pair.finalScale, 0.25f));

                        // Mostrar texto
                        if (infoTextUI != null)
                        {
                            PanelText.SetActive(true);
                            infoTextUI.text = pair.infoText;
                            infoTextUI.gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (spawnedPrefabs.ContainsKey(imageName))
                    {
                        Destroy(spawnedPrefabs[imageName]);
                        spawnedPrefabs.Remove(imageName);

                        // Ocultar texto
                        if (infoTextUI != null)
                        {
                            PanelText.SetActive(false);
                            infoTextUI.gameObject.SetActive(false);
                        }
                    }
                }

                break;
            }
        }
    }


    IEnumerator ScaleUp(GameObject obj, Vector3 targetScale, float duration)
    {
        Vector3 initialScale = obj.transform.localScale;
        float time = 0f;

        while (time < duration)
        {
            obj.transform.localScale = Vector3.Lerp(initialScale, targetScale, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        obj.transform.localScale = targetScale;
    }
}
