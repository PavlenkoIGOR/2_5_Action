using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : WeaponCharacteristicsAndLogic, IWeapon
{
    private float _shotLife = 2.0f;

    private float _lastShotTime = 0;
    private bool _isFiring = false;

    public void Shoot()
    {

        // Создадим пулю
        GameObject plasmaShot_L = Instantiate(BulletPrefab, StartFiringPosition.position, Quaternion.identity);
        plasmaShot_L.transform.up = StartFiringPosition.forward;

        // Получим Rigidbody из созданной пули
        Rigidbody rb = plasmaShot_L.GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Bullet prefab must have a Rigidbody component.");
            return; // Если Rigidbody отсутствует, прерываем выполнение
        }

        // Устанавливаем направление полета
        Vector3 fireDirection = StartFiringPosition.forward; // Используем forward из StartFiringPosition
        rb.AddForce(fireDirection * BulletSpeed, ForceMode.Impulse);

        // Рисуем луч для визуализации
        Ray ray = new Ray(StartFiringPosition.position, fireDirection);
        Debug.DrawRay(StartFiringPosition.position, fireDirection * 10, Color.red);
        Destroy(plasmaShot_L, 2.0f);
        Debug.Log("Shoot");
    }
    // Метод для начала или прекращения стрельбы
    public override void WeaponFire(bool isFiring, Transform trgt)
    {
        _isFiring = isFiring;

        if (_isFiring)
        {
            transform.LookAt(trgt);
            // Проверяем, прошло ли достаточно времени с последнего выстрела
            if (Time.time >= _lastShotTime + FiringTemp)
            {
                Shoot(); // Выполняем выстрел
                _lastShotTime = Time.time; // Обновляем время последнего выстрела
            }
            Debug.Log("is firing");
        }
        else
        {
            Debug.Log("is not firing");
        }
    }
}