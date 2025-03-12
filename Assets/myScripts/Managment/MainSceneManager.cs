using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    GameObject[] enemies;
    private List<Transform> enemiesPrefabs; // Список активных врагов
    private List<Vector3> enemyPositions; // Список для хранения позиций уничтоженных врагов
    private List<Quaternion> enemyRotations; // Список для хранения вращений уничтоженных врагов
    public Transform enemyPrefab;

    void Start()
    {
        enemiesPrefabs = new List<Transform>();
        enemyPositions = new List<Vector3>();
        enemyRotations = new List<Quaternion>();

        // Ищем все объекты на сцене с тегом "Enemies"
        enemies = GameObject.FindGameObjectsWithTag("Enemies");
        foreach (var item in enemies)
        {
            enemiesPrefabs.Add(item.transform);
            // Сохраняем позицию и вращение для восстановления
            Vector3 position = item.transform.position; // Доступ здесь вызовет ошибку, так как объект был уничтожен. Допустим, враг уничтожается другим скриптом.
            Quaternion rotation = item.transform.rotation;
            enemyPositions.Add(position);
            enemyRotations.Add(rotation);
        }
    }

    void Update()
    {
        // Создаем список для сохранения индексов уничтоженных врагов
        List<int> deadEnemies = new List<int>();
        // Проверяем наличие уничтоженных объектов
        for (int i = 0; i <= enemiesPrefabs.Count - 1; i++)
        {
            // Проверяем, существует ли все еще объект по ссылке
            if (enemiesPrefabs[i] == null) // Если объект уничтожен
            {
                deadEnemies.Add(i);// Сохраняем индекс уничтоженного врага
            }
        }
        // Восстанавливаем уничтоженных врагов
        foreach (var index in deadEnemies)
        {
            StartCoroutine(RespawnEnemy(index)); // Передаем индекс для восстановления
            enemiesPrefabs.RemoveAt(index); // Удаляем ссылку на уничтоженного врага
        }
        //// Проверяем наличие уничтоженных объектов
        //for (int i = 0; i <= enemiesPrefabs.Count - 1; i++)
        //{
        //    // Проверяем, существует ли все еще объект по ссылке
        //    if (enemiesPrefabs[i] == null) // Если объект уничтожен
        //    {
        //        foreach (var item in enemiesPrefabs)
        //        {
        //            Debug.Log(item);
        //            if (item == null)
        //            {
        //                StartCoroutine(RespawnEnemy(i)); // Передаем индекс для восстановления
        //            }
        //        }                
        //    }
        //}
    }

    private IEnumerator RespawnEnemy(int index)
    {
        yield return new WaitForSeconds(2f); // Ждем 5 секунд

        // Восстанавливаем объект
        Transform newEnemy = Instantiate(enemyPrefab, enemyPositions[index], enemyRotations[index]);
        newEnemy.tag = "Enemies"; // Устанавливаем тег вновь созданного объекта
        enemiesPrefabs.Add (newEnemy);
    }
}
