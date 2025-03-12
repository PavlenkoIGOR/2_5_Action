using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SphereMarker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.transform.name == "Terrain")
                {
                    // Если луч пересекает terrain, создаем сферу в этой точке
                    GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                    sphere.transform.position = hit.point;
                    // Удаляем сферу через 2 секунды после ее создания
                    Destroy(sphere, 2.0f);
                }
            }
        }
    }
}
