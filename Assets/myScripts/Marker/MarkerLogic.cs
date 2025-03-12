using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerLogic : MonoBehaviour 
{ 
    [SerializeField] GameObject _markerPrefab;
    public GameObject marker;
    void CreateMarker(Vector3 position)
    {
        if (marker == null)
        {
            marker = Instantiate(_markerPrefab, position, Quaternion.identity);
            RemoveMarker();
        }
        else
        {
            DestroyImmediate(marker, true); // Удаляем маркер
            marker = Instantiate(_markerPrefab, position, Quaternion.identity);
        }
    }
    void RemoveMarker()
    {
        if (marker != null)
        {
            StartCoroutine(DestroyMarkerPrefabAfterDelay(marker, 0.10f));
            marker = null;
        }
    }

    IEnumerator DestroyMarkerPrefabAfterDelay(GameObject marker, float delay)
    {
        yield return new WaitForSeconds(delay); // Ждем указанное время
        if (marker != null) // Проверяем, не был ли объект уже удален
        {
            DestroyImmediate(marker, true); // Удаляем маркер
        }
    }
}
