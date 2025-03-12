using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] float DoorDistnaceMove;
    [SerializeField] float moveSpeed = 1.0f; // Скорость движения двери
    private NavMeshObstacle _navMeshObstacle;

    private bool _isOpen;

    private void Start()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    public void Operate()
    {

        StartCoroutine(MoveDoor(_isOpen ? -1 : 1)); // Передаем направление
        _isOpen = !_isOpen;
    }

    IEnumerator MoveDoor(int direction)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y, startPos.z + (direction * DoorDistnaceMove)); // Целевая позиция по оси Z

        // Продолжаем двигаться, пока не достигнем целевой позиции
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            // Смещаем позицию двери
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null; // Ждем следующего кадра
        }
        // Убедитесь, что дверь окончательно установлена в целевой позиции
        transform.position = targetPos;

        // Если дверь теперь открыта, отключите NavMeshObstacle
        if (_isOpen)
        {
            _navMeshObstacle.enabled = false; // Позволяет персонажам проходить
        }
        else
        {
            _navMeshObstacle.enabled = true; // Блокирует проход
        }
    }
}
