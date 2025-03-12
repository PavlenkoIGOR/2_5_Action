using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IRaycastable currentRaycastable;
    private IRaycastable lastRaycastable;  // Для отслеживания последнего объекта с обводкой
    [SerializeField] private Camera camera;
    [SerializeField] private LayerMask layerMask;

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Выполняем Raycast для проверки наведения курсора на объект
        if (Physics.Raycast(ray, out hit, 100, layerMask))
        {
            currentRaycastable = hit.collider.GetComponent<IRaycastable>();
            if (currentRaycastable != null)
            {
                if (lastRaycastable != currentRaycastable) // Проверяем, если это другой объект
                {
                    if (lastRaycastable != null) // Выключаем обводку у предыдущего объекта
                    {
                        lastRaycastable.SwitchOffOutline();
                    }
                    currentRaycastable.SwitchOutline(); // Включаем обводку для текущего объекта
                    lastRaycastable = currentRaycastable; // Обновляем последний объект
                }
            }
        }
        else
        {
            // Если курсор не на объекте с обводкой
            if (lastRaycastable != null)
            {
                lastRaycastable.SwitchOffOutline(); // Отключаем обводку
                lastRaycastable = null; // Сбрасываем последний объект
            }
        }
    }
}
