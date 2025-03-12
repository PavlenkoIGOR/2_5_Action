using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] float DoorDistnaceMove;
    [SerializeField] float moveSpeed = 1.0f; // �������� �������� �����
    private NavMeshObstacle _navMeshObstacle;

    private bool _isOpen;

    private void Start()
    {
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    public void Operate()
    {

        StartCoroutine(MoveDoor(_isOpen ? -1 : 1)); // �������� �����������
        _isOpen = !_isOpen;
    }

    IEnumerator MoveDoor(int direction)
    {
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(startPos.x, startPos.y, startPos.z + (direction * DoorDistnaceMove)); // ������� ������� �� ��� Z

        // ���������� ���������, ���� �� ��������� ������� �������
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            // ������� ������� �����
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null; // ���� ���������� �����
        }
        // ���������, ��� ����� ������������ ����������� � ������� �������
        transform.position = targetPos;

        // ���� ����� ������ �������, ��������� NavMeshObstacle
        if (_isOpen)
        {
            _navMeshObstacle.enabled = false; // ��������� ���������� ���������
        }
        else
        {
            _navMeshObstacle.enabled = true; // ��������� ������
        }
    }
}
