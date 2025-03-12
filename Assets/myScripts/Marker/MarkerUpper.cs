using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerUpper : MonoBehaviour
{
    //[SerializeField] Transform UpperMarkerQuad;
    [SerializeField] private float minScale = 0.40f; // Минимальный масштаб
    [SerializeField] private float maxScale = 1.5f; // Максимальный масштаб
    [SerializeField] private float scaleSpeed = 2.0f; // Скорость изменения масштаба
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform != null)
        {
            transform.Rotate(new Vector3(0, 0, 2.0f));
            
            // Вычисляем новый масштаб
            float scale = Mathf.PingPong(Time.time * scaleSpeed, maxScale - minScale) + minScale;

            // Применяем новый масштаб
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
