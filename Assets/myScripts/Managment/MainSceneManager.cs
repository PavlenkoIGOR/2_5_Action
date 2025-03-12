using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    GameObject[] enemies;
    private List<Transform> enemiesPrefabs; // ������ �������� ������
    private List<Vector3> enemyPositions; // ������ ��� �������� ������� ������������ ������
    private List<Quaternion> enemyRotations; // ������ ��� �������� �������� ������������ ������
    public Transform enemyPrefab;

    void Start()
    {
        enemiesPrefabs = new List<Transform>();
        enemyPositions = new List<Vector3>();
        enemyRotations = new List<Quaternion>();

        // ���� ��� ������� �� ����� � ����� "Enemies"
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        foreach (var item in enemies)
        {
            enemiesPrefabs.Add(item.transform);
            // ��������� ������� � �������� ��� ��������������
            Vector3 position = item.transform.position; // ������ ����� ������� ������, ��� ��� ������ ��� ���������. ��������, ���� ������������ ������ ��������.
            Quaternion rotation = item.transform.rotation;
            enemyPositions.Add(position);
            enemyRotations.Add(rotation);
        }
    }

    void Update()
    {
        // ������� ������ ��� ���������� �������� ������������ ������
        List<int> deadEnemies = new List<int>();
        // ��������� ������� ������������ ��������
        for (int i = 0; i <= enemiesPrefabs.Count - 1; i++)
        {
            // ���������, ���������� �� ��� ��� ������ �� ������
            if (enemiesPrefabs[i] == null) // ���� ������ ���������
            {
                deadEnemies.Add(i);// ��������� ������ ������������� �����
            }
        }
        // ��������������� ������������ ������
        foreach (var index in deadEnemies)
        {
            StartCoroutine(RespawnEnemy(index)); // �������� ������ ��� ��������������
            enemiesPrefabs.RemoveAt(index); // ������� ������ �� ������������� �����
        }
        //// ��������� ������� ������������ ��������
        //for (int i = 0; i <= enemiesPrefabs.Count - 1; i++)
        //{
        //    // ���������, ���������� �� ��� ��� ������ �� ������
        //    if (enemiesPrefabs[i] == null) // ���� ������ ���������
        //    {
        //        foreach (var item in enemiesPrefabs)
        //        {
        //            Debug.Log(item);
        //            if (item == null)
        //            {
        //                StartCoroutine(RespawnEnemy(i)); // �������� ������ ��� ��������������
        //            }
        //        }                
        //    }
        //}
    }

    private IEnumerator RespawnEnemy(int index)
    {
        yield return new WaitForSeconds(2f); // ���� 5 ������

        // ��������������� ������
        Transform newEnemy = Instantiate(enemyPrefab, enemyPositions[index], enemyRotations[index]);
        newEnemy.tag = "Enemies"; // ������������� ��� ����� ���������� �������
        enemiesPrefabs.Add (newEnemy);
    }
}
