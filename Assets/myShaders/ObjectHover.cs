using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHover : MonoBehaviour
{
    public Material defaultMaterial; // Основной материал
    public Material outlineMaterial;  // Материал с обводкой

    private Renderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        if (_renderer != null)
        {
            //Debug.Log("asdasdasda00");
        }
    }

    private void OnMouseEnter()
    {
        _renderer.material = outlineMaterial; // Меняем на материал с обводкой
    }

    private void OnMouseExit()
    {
        _renderer.material = defaultMaterial; // Меняем обратно на основной материал
    }
}
