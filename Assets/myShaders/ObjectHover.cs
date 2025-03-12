using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHover : MonoBehaviour
{
    public Material defaultMaterial; // �������� ��������
    public Material outlineMaterial;  // �������� � ��������

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
        _renderer.material = outlineMaterial; // ������ �� �������� � ��������
    }

    private void OnMouseExit()
    {
        _renderer.material = defaultMaterial; // ������ ������� �� �������� ��������
    }
}
