using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IRaycastable currentRaycastable;
    private IRaycastable lastRaycastable;  // ��� ������������ ���������� ������� � ��������
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask layerMask;

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // ��������� Raycast ��� �������� ��������� ������� �� ������
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            currentRaycastable = hit.collider.GetComponent<IRaycastable>();
            if (currentRaycastable != null)
            {
                if (lastRaycastable != currentRaycastable) // ���������, ���� ��� ������ ������
                {
                    if (lastRaycastable != null) // ��������� ������� � ����������� �������
                    {
                        lastRaycastable.SwitchOffOutline();
                    }
                    currentRaycastable.SwitchOutline(); // �������� ������� ��� �������� �������
                    lastRaycastable = currentRaycastable; // ��������� ��������� ������
                }
            }
        }
        else
        {
            // ���� ������ �� �� ������� � ��������
            if (lastRaycastable != null)
            {
                lastRaycastable.SwitchOffOutline(); // ��������� �������
                lastRaycastable = null; // ���������� ��������� ������
            }
        }
    }
}
